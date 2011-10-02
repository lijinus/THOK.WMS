using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using THOK.WMS.BLL;
using System.Drawing;
using THOK.WMS.Upload;
using THOK.WMS.Upload.Bll;
public partial class Code_StockEntry_EntryManualAllotPage : BasePage
{
    #region 变量
    Warehouse objHouse = new Warehouse();
    WarehouseArea objArea = new WarehouseArea();
    WarehouseShelf objShelf = new WarehouseShelf();
    WarehouseCell objCell = new WarehouseCell();
    DataSet dsHouse;
    DataSet dsArea;
    DataSet dsShelf;
    DataSet dsCell;


    EntryBillMaster billMaster = new EntryBillMaster();
    EntryBillDetail billDetail = new EntryBillDetail();
    UpdateUploadBll updateBll = new UpdateUploadBll();
    DataSet dsMaster;
    DataSet dsDetail;
    int pageIndex = 1;
    int pageSize = 5;
    EntryAllot objAllot = new EntryAllot();
    //DataTable tableAllotment;
    DataTable tableNullCell;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ExpiresAbsolute = System.DateTime.Now;
        dgDetail.PageSize = pageSize;
        pager.PageSize = pageSize;
        if (!IsPostBack)
        {
            dsDetail = billDetail.QueryByBillNo(Session["BillNoList"].ToString());
            DataColumn newCol = new DataColumn("ALLOTEDQTY", System.Type.GetType("System.Decimal"));
            newCol.DefaultValue = 0.00M;
            dsDetail.Tables[0].Columns.Add(newCol);//

            this.dgDetail.DataSource = dsDetail.Tables[0];
            this.dgDetail.DataBind();
            GridRowSpan(dgDetail, 1);

            Session["dsDetail"] = dsDetail;
            tableNullCell = objAllot.GetNullCell().Tables[0];
            Session["tableNullCell"] = tableNullCell;
            //tableAllotment = tableNullCell.Clone();
            //Session["tableAllotment"] = tableAllotment;
            LoadHouseTree();
            ShowCell(tableNullCell);
            ViewState["pageIndex"] = pageIndex;

            
            pager.CurrentPageIndex = pageIndex;
            pager.RecordCount = dsDetail.Tables[0].Rows.Count;
        }
        else
        {
            pageIndex = Convert.ToInt32(ViewState["pageIndex"].ToString());
            pager.CurrentPageIndex = pageIndex;

            dsDetail = (DataSet)(Session["dsDetail"]);            
            this.dgDetail.CurrentPageIndex = pageIndex-1;
            this.dgDetail.DataSource = dsDetail.Tables[0];
            this.dgDetail.DataBind();
            tableNullCell = (DataTable)(Session["tableNullCell"]);
            ShowCell(tableNullCell);
            //tableAllotment = (DataTable)(Session["tableAllotment"]);
            GridRowSpan(dgDetail, 1);
        }
    }

    #region 加载仓库树结构
    protected void LoadHouseTree()
    {
        //tvWarehouse.Attributes.Add("onclick", "postBackByObject()");
        this.tvWarehouse.Nodes.Clear();
        dsHouse = objHouse.QueryAllWarehouse();
        foreach (DataRow row in dsHouse.Tables[0].Rows)
        {
            TreeNode node = new TreeNode(row["WH_NAME"].ToString(), row["WH_CODE"].ToString());
            node.Target = "frame";

            node.ImageUrl = "../../images/leftmenu/in_warehouse.gif";
            tvWarehouse.Nodes.Add(node);
        }

        DataSet dsTemp = objArea.QueryAllArea();
        if (dsTemp.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow r in dsTemp.Tables[0].Rows)
            {
                TreeNode nodeHouse = tvWarehouse.FindNode(r["WH_CODE"].ToString());

                if (nodeHouse != null)
                {
                    nodeHouse.ExpandAll();
                    TreeNode nodeArea = new TreeNode("库区：" + r["AREANAME"].ToString(), r["AREACODE"].ToString());
                    nodeArea.ToolTip = r["AREA_ID"].ToString();
                    nodeArea.Target = "frame";
                    nodeHouse.ChildNodes.Add(nodeArea);
                }
            }
        }

        dsTemp = objShelf.QueryAllShelf();
        foreach (DataRow r2 in dsTemp.Tables[0].Rows)
        {
            TreeNode nodeArea = tvWarehouse.FindNode(r2["WH_CODE"].ToString() + "/" + r2["AREACODE"].ToString());
            if (nodeArea != null)
            {
                TreeNode nodeShelf = new TreeNode("货架：" + r2["SHELFNAME"].ToString(), r2["SHELFCODE"].ToString());
                nodeShelf.ToolTip = r2["SHELF_ID"].ToString();
                nodeArea.ChildNodes.Add(nodeShelf);
            }

        }

        dsTemp = objCell.QueryAllCell();
        foreach (DataRow r3 in tableNullCell.Rows)
        {
            TreeNode nodeShelf = tvWarehouse.FindNode(r3["WH_CODE"].ToString() + "/" + r3["AREACODE"].ToString() + "/" + r3["SHELFCODE"].ToString());
            if (nodeShelf != null)
            {
                if (!IsPostBack)
                {
                    nodeShelf.CollapseAll();
                }
                TreeNode nodeCell = new TreeNode("货位：" + r3["CELLNAME"].ToString(), r3["CELLCODE"].ToString());
                nodeCell.Text = string.Format("货位：{0}", r3["CELLNAME"].ToString());

                nodeCell.ToolTip = r3["CELL_ID"].ToString();
                nodeShelf.ChildNodes.Add(nodeCell);
            }
        }
    }

    #endregion

    #region 显示货位图表
    protected void ShowCell(DataTable tableCell)
    {
        this.pnlNullCell.Controls.Clear();
        for (int i = 0; i < tableCell.Rows.Count; i++)
        {
            if (tvWarehouse.SelectedNode != null)
            {
                if (tvWarehouse.SelectedNode.Depth == 0)
                {
                    if (tableNullCell.Rows[i]["WH_CODE"].ToString() != tvWarehouse.SelectedNode.Value)
                    {
                        continue;
                    }
                }
                else if (tvWarehouse.SelectedNode.Depth == 1)
                {
                    if (tableNullCell.Rows[i]["AREACODE"].ToString() != tvWarehouse.SelectedNode.Value)
                    {
                        continue;
                    }
                }
                else if (tvWarehouse.SelectedNode.Depth == 2)
                {
                    if (tableNullCell.Rows[i]["SHELFCODE"].ToString() != tvWarehouse.SelectedNode.Value)
                    {
                        continue;
                    }
                }
                else if (tvWarehouse.SelectedNode.Depth == 3)
                {
                    if (tableNullCell.Rows[i]["CELLCODE"].ToString() != tvWarehouse.SelectedNode.Value)
                    {
                        continue;
                    }
                }
            }
            Panel p = new Panel();
            p.Attributes.Add("class", "panel");
            p.Attributes.Add("onmouseover", "MouseOverFun(this)");
            p.Attributes.Add("name", i.ToString());

            int MAX_QUANTITY;
            //DataTable max = objAllot.QueryJian2(tableNullCell.Rows[i]["PRODUCTCODE"].ToString()).Tables[0];
            //if (max.Rows.Count > 0)
            //{
            //    MAX_QUANTITY = Convert.ToInt32(max.Rows[0][1]);
            //}
            //else
            //{
            //    MAX_QUANTITY = 30;
            //}
            int iUpper = Convert.ToInt32(tableNullCell.Rows[i]["MAX_QUANTITY"]);//MAX_QUANTITY; //货位存量上限
            int iCurrent = 0;
            if (tableCell.Rows[i]["ALLOTQUANTITY"] != null)
            {
                string a = tableCell.Rows[i]["ALLOTQUANTITY"].ToString();
                decimal d = Convert.ToDecimal(a);
                iCurrent = Convert.ToInt32(d);   //当前存货量(件)
            }
            int iRemain = iUpper - iCurrent;

            Table table = new Table();
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            tc.Text = string.Format("{0}<br/>可分配{1}{4}" + "<BR/>已分配{2}<font color='red'>{3}</font>{4}"
                                   , tableCell.Rows[i]["CELLNAME"].ToString()
                                   , iUpper.ToString()
                                   , tableCell.Rows[i]["PRODUCTNAME"].ToString()
                                   , tableCell.Rows[i]["ALLOTQUANTITY"].ToString()
                                   ,tableCell.Rows[i]["CELLUNIT"].ToString());
            tc.Attributes.Add("class", "cell");
            table.CellPadding = 2;
            table.CellSpacing = 2;
            tr.Controls.Add(tc);
            table.Controls.Add(tr);
            p.Controls.Add(table);

            //decimal d = Decimal.Divide(iRemain, iUpper);
            //int top = (int)Math.Floor((Math.Round(d, 2)*80));
            int top = 90 / iUpper * iRemain;
            tc.Attributes.Add("style", "background:#fbfbfb url(../../images/bg_cell.jpg) no-repeat 0px " + top.ToString() + "px;");
            this.pnlNullCell.Controls.Add(p);
        }
    }
    #endregion

    #region Gird行拖动分配
    protected void btnAllot_Click(object sender, EventArgs e)
    {
        int detailIndex = Convert.ToInt32(this.hdnDetailRow.Value);
        int cellIndex = Convert.ToInt32(this.hdnCellRow.Value);
        //JScript.Instance.ShowMessage(this, pageIndex.ToString());
        detailIndex = (pageIndex-1) * pageSize + detailIndex;
        
        //int detailID = Convert.ToInt32(dsDetail.Tables[0].Rows[detailIndex]["ID"].ToString());
        string BillNo = dsDetail.Tables[0].Rows[detailIndex]["BILLNO"].ToString();
        string ProductCode = dsDetail.Tables[0].Rows[detailIndex]["PRODUCTCODE"].ToString();
        string ProductName = dsDetail.Tables[0].Rows[detailIndex]["PRODUCTNAME"].ToString();
        string UnitCode = dsDetail.Tables[0].Rows[detailIndex]["UNITCODE"].ToString();
        string UnitName = dsDetail.Tables[0].Rows[detailIndex]["UNITNAME"].ToString();
        decimal quantity = Convert.ToDecimal(dsDetail.Tables[0].Rows[detailIndex]["INPUTQUANTITY"].ToString());
        //decimal availableQuantity =decimal.Parse( objAllot.QueryJian2(ProductCode).Tables[0].Rows[0][1].ToString());
        decimal availableQuantity = decimal.Parse(tableNullCell.Rows[cellIndex]["AVAILABLE"].ToString());
        if (availableQuantity == 0)
        {
            JScript.Instance.ShowMessage(this, tableNullCell.Rows[cellIndex]["CELLNAME"].ToString()+" 货位已满");
            return;
        }
        string allotedProductCode = tableNullCell.Rows[cellIndex]["PRODUCTCODE"].ToString();
        string allotedProductName = tableNullCell.Rows[cellIndex]["PRODUCTNAME"].ToString();
        if (allotedProductCode != "" && allotedProductCode != ProductCode)
        {
            JScript.Instance.ShowMessage(this, tableNullCell.Rows[cellIndex]["CELLNAME"].ToString()+" 已分配了" + allotedProductName);
            return;
        }

        decimal remainQuantity = 0.00M;
        if (availableQuantity >= quantity)
        {
            dsDetail.Tables[0].Rows[detailIndex]["INPUTQUANTITY"] = 0.00M;
            dsDetail.Tables[0].Rows[detailIndex]["ALLOTEDQTY"] = Convert.ToDecimal(dsDetail.Tables[0].Rows[detailIndex]["ALLOTEDQTY"])+quantity;
            tableNullCell.Rows[cellIndex]["ALLOTQUANTITY"] = quantity;
            tableNullCell.Rows[cellIndex]["AVAILABLE"] = availableQuantity - quantity;
        }
        else
        {
            tableNullCell.Rows[cellIndex]["ALLOTQUANTITY"] = availableQuantity;
            remainQuantity = quantity - availableQuantity;
            tableNullCell.Rows[cellIndex]["AVAILABLE"] = 0.00M;
            dsDetail.Tables[0].Rows[detailIndex]["INPUTQUANTITY"] = remainQuantity;
            dsDetail.Tables[0].Rows[detailIndex]["ALLOTEDQTY"] = Convert.ToDecimal(dsDetail.Tables[0].Rows[detailIndex]["ALLOTEDQTY"]) + availableQuantity;
        }
        tableNullCell.Rows[cellIndex]["PRODUCTCODE"] = ProductCode;
        tableNullCell.Rows[cellIndex]["PRODUCTNAME"] = ProductName;
        tableNullCell.Rows[cellIndex]["BILLNO"] = BillNo;
        tableNullCell.Rows[cellIndex]["UNITCODE"] = UnitCode;
        tableNullCell.Rows[cellIndex]["UNITNAME"] = UnitName;

        Session["dsDetail"] = dsDetail;
        Session["tableNullCell"] = tableNullCell;
        ShowCell(tableNullCell);
        this.dgDetail.DataSource = dsDetail.Tables[0];
        this.dgDetail.DataBind();
        GridRowSpan(dgDetail, 1);

        decimal totalRemain = 0.00M;
        foreach (DataRow r in dsDetail.Tables[0].Rows)
        {
            totalRemain += Convert.ToDecimal(r["INPUTQUANTITY"]);
        }
        if (totalRemain > 0.00M)
        {
            this.btnSaveAllotment.Enabled = false;
        }
        else
        {
            this.btnSaveAllotment.Enabled = true;
        }

    }
    #endregion

    #region 明细绑定
    protected void dgDetail_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                e.Item.Cells[3].Attributes.Add("title", "拖动到货位进行分配");
                e.Item.Cells[3].Attributes.Add("onmousedown", "MouseDownToMove(this)");
                e.Item.Cells[3].Attributes.Add("onmousemove", "MouseMoveToMove(this)");
                e.Item.Cells[3].Attributes.Add("onmouseup", "MouseUpToMove(this)");
                e.Item.Cells[3].Attributes.Add("name", e.Item.ItemIndex.ToString());
                if (e.Item.ItemIndex % 2 == 0)
                {
                   // e.Item.Attributes.Add("style", "background-color:" + Color.FromName(Session["grid_OddRowColor"].ToString()) + ";");
                    e.Item.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Item.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
                }

                if (Convert.ToDecimal(e.Item.Cells[4].Text) > 0)
                {
                    e.Item.Cells[3].Attributes.Add("style", " position:relative; cursor:move;");
                }
            }
        }
        catch { }
    }
    
    #endregion

    #region Grid列合并

    protected void GridRowSpan(DataGrid grid, int colIndex)
    {
        if (grid.Items.Count >= 2)
        {
            int start = 0;
            int end = 0;
            for (int i = 1; i < grid.Items.Count; i++)
            {
                if (grid.Items[i].Cells[colIndex].Text == grid.Items[i - 1].Cells[colIndex].Text)
                {
                    end = i;
                    if (i < grid.Items.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        int span = end - start + 1;
                        grid.Items[start].Cells[colIndex].RowSpan = span;
                        for (int k = start + 1; k <= end; k++)
                        {
                            grid.Items[k].Cells[colIndex].Visible = false;
                        }
                    }
                }
                else
                {
                    int span = end - start + 1;
                    grid.Items[start].Cells[colIndex].RowSpan = span;
                    for (int k = start + 1; k <= end; k++)
                    {
                        grid.Items[k].Cells[colIndex].Visible = false;
                    }

                    start = end + 1;
                    end++;
                }
            }
        }
    }
    
    #endregion

    #region 保存分配结果
    protected void btnSaveAllotment_Click(object sender, EventArgs e)
    {
        DataTable table = objAllot.GetEmptyAllotTable();
        foreach (DataRow row in tableNullCell.Rows)
        {
            if (Convert.ToDecimal(row["ALLOTQUANTITY"]) > 0.00M)
            {
                DataRow newRow = table.NewRow();
                newRow["BILLNO"] = row["BILLNO"];
                newRow["PRODUCTCODE"] = row["PRODUCTCODE"];
                newRow["CELLCODE"] = row["CELLCODE"];
                newRow["QUANTITY"] = row["ALLOTQUANTITY"];
                newRow["INPUTQUANTITY"] = 0;
                newRow["STATUS"] = "0";
                newRow["UNITCODE"] = row["UNITCODE"];
                newRow["UNITNAME"]=row["UNITNAME"];
                table.Rows.Add(newRow);
                if (objAllot.QueryJian2(row["PRODUCTCODE"].ToString()).Tables[0].Rows.Count > 0)
                {
                    string MAX_QUANTITY = row["MAX_QUANTITY"].ToString();//objAllot.QueryJian2(row["PRODUCTCODE"].ToString()).Tables[0].Rows[0][1].ToString();
                    objAllot.UpdateCell(MAX_QUANTITY, row["UNITCODE"].ToString(), row["CELLCODE"].ToString());
                }
            }
            else
            {
                continue;
            }
        }
        objAllot.SaveAllotment(table);
        updateBll.InsertBull(table, "DWV_IWMS_IN_STORE_BILL", "DWV_IWMS_IN_STORE_BILL_DETAIL", "DWV_IWMS_IN_BUSI_BILL", Session["EmployeeCode"].ToString());
        updateBll.inUpdateAllot(Session["EmployeeCode"].ToString(), Session["BillNoList"].ToString());
        billMaster.UpdateAlloted(Session["BillNoList"].ToString(), Session["EmployeeCode"].ToString());

        Session["BillNoList"] = "";
        JScript.Instance.ShowMessage(this, "分配结果保存成功");
        Response.Redirect("EntryAllotPage.aspx");
    }    
    #endregion

    #region 重新分配
    protected void btnReAllot_Click(object sender, EventArgs e)
    {
        dsDetail = billDetail.QueryByBillNo(Session["BillNoList"].ToString());
        DataColumn newCol = new DataColumn("ALLOTEDQTY", System.Type.GetType("System.Decimal"));
        newCol.DefaultValue = 0.00M;
        dsDetail.Tables[0].Columns.Add(newCol);//

        this.dgDetail.DataSource = dsDetail.Tables[0];
        this.dgDetail.DataBind();
        GridRowSpan(dgDetail, 1);

        Session["dsDetail"] = dsDetail;
        tableNullCell = objAllot.GetNullCell().Tables[0];
        Session["tableNullCell"] = tableNullCell;
        ShowCell(tableNullCell);
        this.btnSaveAllotment.Enabled = false;
    }    
    #endregion

    # region 分页
    protected void pager_PageChanging(object src, PageChangingEventArgs e)
    {
        pager.CurrentPageIndex = e.NewPageIndex;
        pageIndex = pager.CurrentPageIndex;
        ViewState["pageIndex"] = pageIndex;

        dgDetail.CurrentPageIndex = pageIndex - 1;
        this.dgDetail.DataSource = dsDetail.Tables[0];
        this.dgDetail.DataBind();
        GridRowSpan(dgDetail, 1);

    }

    protected void dgDetail_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        //this.dgDetail.CurrentPageIndex = e.NewPageIndex;
        //pageIndex = e.NewPageIndex;
        ////JScript.Instance.ShowMessage(this, pageIndex.ToString());
        //ViewState["pageIndex"] = pageIndex;
        //this.dgDetail.DataSource = dsDetail.Tables[0];
        //this.dgDetail.DataBind();
        //GridRowSpan(dgDetail, 1);
    }

    #endregion

    #region 节点选中事件
    protected void tvWarehouse_SelectedNodeChanged(object sender, EventArgs e)
    {
        ShowCell(tableNullCell);
    }
    #endregion
}
