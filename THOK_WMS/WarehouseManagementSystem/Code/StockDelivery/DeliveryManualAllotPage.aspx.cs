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
using THOK.WMS.Upload.Bll;

public partial class Code_StockDelivery_DeliveryManualAllotPage : BasePage
{
    #region 变量
    //Warehouse objHouse = new Warehouse();
    //WarehouseArea objArea = new WarehouseArea();
    //WarehouseShelf objShelf = new WarehouseShelf();
    //WarehouseCell objCell = new WarehouseCell();
    //DataSet dsHouse;
    //DataSet dsArea;
    //DataSet dsShelf;
    //DataSet dsCell;


    DeliveryBillMaster billMaster = new DeliveryBillMaster();
    DeliveryBillDetail billDetail = new DeliveryBillDetail();
    UpdateUploadBll updateBll = new UpdateUploadBll();
    //DataSet dsMaster;
    DataSet dsDetail;
    int pageIndex = 1;
    int pageSize = 15;
    DeliveryAllot objAllot = new DeliveryAllot();
    DataTable tableAllotment;

    DataTable tableMaster;
    DataTable tableDetail;
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

            if (Session["tableAllotment"] == null)
            {
                tableAllotment = objAllot.GetEmptyAllotmentTable();
                Session["tableAllotment"] = tableAllotment;
            }
            else
            {
                tableAllotment = (DataTable)(Session["tableAllotment"]);
            }
           // LoadHouseTree();

            ViewState["pageIndex"] = pageIndex;

            pager.CurrentPageIndex = pageIndex;
            pager.RecordCount = dsDetail.Tables[0].Rows.Count;
        }
        else
        {
            pageIndex = Convert.ToInt32(ViewState["pageIndex"].ToString());
            pager.CurrentPageIndex = pageIndex;

            dsDetail = (DataSet)(Session["dsDetail"]);
            this.dgDetail.CurrentPageIndex = pageIndex - 1;
            this.dgDetail.DataSource = dsDetail.Tables[0];
            this.dgDetail.DataBind();
            GridRowSpan(dgDetail, 1);

            tableAllotment = (DataTable)(Session["tableAllotment"]);
        }
        tableAllotment.DefaultView.Sort = "BILLNO DESC";
        this.dgAllotment.DataSource = tableAllotment;
        this.dgAllotment.DataBind();
        GridRowSpan(dgAllotment, 0);
    }

    #region 加载仓库树结构
    //protected void LoadHouseTree()
    //{
    //    //tvWarehouse.Attributes.Add("onclick", "postBackByObject()");
    //    this.tvWarehouse.Nodes.Clear();
    //    dsHouse = objHouse.QueryAllWarehouse();
    //    foreach (DataRow row in dsHouse.Tables[0].Rows)
    //    {
    //        TreeNode node = new TreeNode(row["WH_NAME"].ToString(), row["WH_CODE"].ToString());
    //        node.Target = "frame";

    //        node.ImageUrl = "../../images/leftmenu/in_warehouse.gif";
    //        tvWarehouse.Nodes.Add(node);
    //    }

    //    DataSet dsTemp = objArea.QueryAllArea();
    //    if (dsTemp.Tables[0].Rows.Count > 0)
    //    {
    //        foreach (DataRow r in dsTemp.Tables[0].Rows)
    //        {
    //            TreeNode nodeHouse = tvWarehouse.FindNode(r["WH_CODE"].ToString());

    //            if (nodeHouse != null)
    //            {
    //                nodeHouse.ExpandAll();
    //                TreeNode nodeArea = new TreeNode("库区：" + r["AREANAME"].ToString(), r["AREACODE"].ToString());
    //                nodeArea.ToolTip = r["AREA_ID"].ToString();
    //                nodeArea.Target = "frame";
    //                nodeHouse.ChildNodes.Add(nodeArea);
    //            }
    //        }
    //    }

    //    dsTemp = objShelf.QueryAllShelf();
    //    foreach (DataRow r2 in dsTemp.Tables[0].Rows)
    //    {
    //        TreeNode nodeArea = tvWarehouse.FindNode(r2["WH_CODE"].ToString() + "/" + r2["AREACODE"].ToString());
    //        if (nodeArea != null)
    //        {
    //            TreeNode nodeShelf = new TreeNode("货架：" + r2["SHELFNAME"].ToString(), r2["SHELFCODE"].ToString());
    //            nodeShelf.ToolTip = r2["SHELF_ID"].ToString();
    //            nodeArea.ChildNodes.Add(nodeShelf);
    //        }

    //    }

    //    //dsTemp = objCell.QueryAllCell();
    //    //foreach (DataRow r3 in tableNullCell.Rows)
    //    //{
    //    //    TreeNode nodeShelf = tvWarehouse.FindNode(r3["WH_CODE"].ToString() + "/" + r3["AREACODE"].ToString() + "/" + r3["SHELFCODE"].ToString());
    //    //    if (nodeShelf != null)
    //    //    {
    //    //        if (!IsPostBack)
    //    //        {
    //    //            nodeShelf.CollapseAll();
    //    //        }
    //    //        TreeNode nodeCell = new TreeNode("货位：" + r3["CELLNAME"].ToString(), r3["CELLCODE"].ToString());
    //    //        nodeCell.Text = string.Format("货位：{0}", r3["CELLNAME"].ToString());

    //    //        nodeCell.ToolTip = r3["CELL_ID"].ToString();
    //    //        nodeShelf.ChildNodes.Add(nodeCell);
    //    //    }
    //    //}
    //}

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

                if (e.Item.ItemIndex % 2 == 0)
                {
                    e.Item.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Item.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
                }

                if (Convert.ToDecimal(e.Item.Cells[4].Text) > 0)
                {
                    //Button btn = new Button();
                    //btn.CssClass = "button2";
                    //btn.Text = "分配";
                    ////btn.OnClientClick = "return allot();";
                    //btn.Click+=new EventHandler(btn_Click);
                    //e.Item.Cells[8].Controls.Add(btn);
                }
                else
                {
                    e.Item.Cells[8].Text = "全部分配";
                }

            }
        }
        catch { }
    }

    protected void btn_Click(object sender, EventArgs e)
    {

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
        for (int i = 0; i < dgAllotment.Items.Count; i++)
        {
            tableAllotment.Rows[i]["CELLCODE"] = ((TextBox)dgAllotment.Items[i].Cells[3].Controls[0]).Text;
            tableAllotment.Rows[i]["CELLNAME"] = ((TextBox)dgAllotment.Items[i].Cells[4].Controls[0]).Text;
            tableAllotment.Rows[i]["QUANTITY"] = Convert.ToDecimal(((TextBox)dgAllotment.Items[i].Cells[5].Controls[0]).Text);
        }

        foreach (DataRow rowDetail in dsDetail.Tables[0].Rows)
        {
            string billNo = rowDetail["BILLNO"].ToString();
            string productCode = rowDetail["PRODUCTCODE"].ToString();
            string productName = rowDetail["PRODUCTNAME"].ToString();
            decimal quantity = Convert.ToDecimal(rowDetail["QUANTITY"]);
            DataRow[] rowsAllot = tableAllotment.Select("BILLNO='" + billNo + "' AND PRODUCTCODE='" + productCode + "'");
            decimal total = 0.00M;
            foreach (DataRow rDetail in rowsAllot)
            {
                total = total + Convert.ToDecimal(rDetail["QUANTITY"]);
            }
            if (quantity != total)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, billNo + "出库单" + productName + "分配数量不符");
                return;
            }
            else
            {
                continue;
            }
        }

        Merge(tableAllotment);
        objAllot.SaveAllotment(tableMaster, tableDetail);
        updateBll.InsertBull(tableDetail, "DWV_IWMS_OUT_STORE_BILL", "DWV_IWMS_OUT_STORE_BILL_DETAIL", "DWV_IWMS_OUT_BUSI_BILL", Session["EmployeeCode"].ToString());
        billMaster.UpdateAlloted(Session["BillNoList"].ToString(), Session["EmployeeCode"].ToString());
        updateBll.outUpdateAudot(Session["EmployeeCode"].ToString(), Session["BillNoList"].ToString());
        Session["BillNoList"] = null;
        JScript.Instance.ShowMessage(this.UpdatePanel1, "入库分配保存成功");
        Response.Redirect("DeliveryAllotPage.aspx");
    }
    #endregion

    #region 分配结果拼盘
    protected void Merge(DataTable tableAllotment)
    {
        tableMaster = objAllot.GetEmptyAllotMasterTable();
        tableDetail = objAllot.GetEmptyAllotDetailTable();
        string[] aryBillNo = Session["BillNoList"].ToString().Split(',');

        DataRow newMasterRow = tableMaster.NewRow();
        DataRow newDetailRow = tableDetail.NewRow();
        for (int i = 0; i < aryBillNo.Length; i++)//逐单拼盘
        {
            int taskid = 1;
            DataRow[] rows = tableAllotment.Select("BILLNO='" + aryBillNo[i] + "'", "QUANTITY DESC");
            foreach (DataRow rAllot in rows)
            {
                bool isMerged = false;
                decimal detailQuantity = Convert.ToDecimal(rAllot["QUANTITY"]);
                if (taskid == 1)//出库单开始合并
                {
                    newMasterRow = tableMaster.NewRow();
                    newMasterRow["TASKID"] = taskid.ToString();
                    newMasterRow["BILLNO"] = rAllot["BILLNO"];
                    newMasterRow["TOTALQUANTITY"] = rAllot["QUANTITY"];
                    newMasterRow["ISMERGED"] = "0";
                    newMasterRow["STATUS"] = "0";
                    tableMaster.Rows.Add(newMasterRow);


                    newDetailRow = tableDetail.NewRow();
                    newDetailRow["TASKID"] = taskid.ToString();
                    newDetailRow["BILLNO"] = rAllot["BILLNO"];
                    newDetailRow["DETAILID"] = rAllot["DETAILID"];
                    newDetailRow["PRODUCTCODE"] = rAllot["PRODUCTCODE"];
                    newDetailRow["CELLCODE"] = rAllot["CELLCODE"];
                    newDetailRow["QUANTITY"] = rAllot["QUANTITY"];
                    newDetailRow["OUTPUTQUANTITY"] = rAllot["QUANTITY"];
                    newDetailRow["PALLETID"] = rAllot["PALLETID"];
                    newDetailRow["NEWPALLETID"] = rAllot["PALLETID"];
                    newDetailRow["STATUS"] = "0";
                    tableDetail.Rows.Add(newDetailRow);
                    taskid++;
                    continue;
                }

                if (detailQuantity < 0.00M)//条烟，拆件烟，与上个托盘合并
                {
                    tableMaster.Rows[tableMaster.Rows.Count - 1]["ISMERGED"] = "1";
                    newDetailRow = tableDetail.NewRow();
                    newDetailRow["TASKID"] = tableMaster.Rows[tableMaster.Rows.Count - 1]["TASKID"];
                    newDetailRow["BILLNO"] = rAllot["BILLNO"];
                    newDetailRow["DETAILID"] = rAllot["DETAILID"];
                    newDetailRow["PRODUCTCODE"] = rAllot["PRODUCTCODE"];
                    newDetailRow["CELLCODE"] = rAllot["CELLCODE"];
                    newDetailRow["QUANTITY"] = rAllot["QUANTITY"];
                    newDetailRow["OUTPUTQUANTITY"] = rAllot["QUANTITY"];
                    newDetailRow["PALLETID"] = rAllot["PALLETID"];
                    newDetailRow["NEWPALLETID"] = tableDetail.Rows[tableDetail.Rows.Count - 1]["NEWPALLETID"];
                    newDetailRow["STATUS"] = "0";
                    tableDetail.Rows.Add(newDetailRow);
                    continue;
                }
                else
                {
                    isMerged = false;

                    foreach (DataRow rMaster in tableMaster.Rows)
                    {
                        if (rMaster["BILLNO"].ToString() != rAllot["BILLNO"].ToString())//不同单，不合并
                        {
                            continue;
                        }
                        decimal masterQuantity = Convert.ToDecimal(rMaster["TOTALQUANTITY"]);
                        if (masterQuantity + detailQuantity >= 30.00M)// 超过一托盘的数量
                        {
                            continue;
                        }
                        else
                        {
                            rMaster["TOTALQUANTITY"] = Convert.ToDecimal(rMaster["TOTALQUANTITY"]) + detailQuantity;
                            rMaster["ISMERGED"] = "1";
                            newDetailRow = tableDetail.NewRow();
                            newDetailRow["TASKID"] = rMaster["TASKID"];
                            newDetailRow["BILLNO"] = rAllot["BILLNO"];
                            newDetailRow["DETAILID"] = rAllot["DETAILID"];
                            newDetailRow["PRODUCTCODE"] = rAllot["PRODUCTCODE"];
                            newDetailRow["CELLCODE"] = rAllot["CELLCODE"];
                            newDetailRow["QUANTITY"] = rAllot["QUANTITY"];
                            newDetailRow["OUTPUTQUANTITY"] = rAllot["QUANTITY"];
                            newDetailRow["PALLETID"] = rAllot["PALLETID"];
                            newDetailRow["NEWPALLETID"] = tableDetail.Rows[tableDetail.Rows.Count - 1]["NEWPALLETID"];
                            newDetailRow["STATUS"] = "0";
                            tableDetail.Rows.Add(newDetailRow);
                            isMerged = true;
                            break;
                        }
                    }
                    if (!isMerged)//与已拼盘的明细不能再拼盘，则新建托盘
                    {

                        newMasterRow = tableMaster.NewRow();
                        newMasterRow["TASKID"] = taskid.ToString();
                        newMasterRow["BILLNO"] = rAllot["BILLNO"];
                        newMasterRow["TOTALQUANTITY"] = rAllot["QUANTITY"];
                        newMasterRow["ISMERGED"] = "0";
                        newMasterRow["STATUS"] = "0";
                        tableMaster.Rows.Add(newMasterRow);


                        newDetailRow = tableDetail.NewRow();
                        newDetailRow["TASKID"] = taskid.ToString();
                        newDetailRow["BILLNO"] = rAllot["BILLNO"];
                        newDetailRow["DETAILID"] = rAllot["DETAILID"];
                        newDetailRow["PRODUCTCODE"] = rAllot["PRODUCTCODE"];
                        newDetailRow["CELLCODE"] = rAllot["CELLCODE"];
                        newDetailRow["QUANTITY"] = rAllot["QUANTITY"];
                        newDetailRow["OUTPUTQUANTITY"] = rAllot["QUANTITY"];
                        newDetailRow["PALLETID"] = rAllot["PALLETID"];
                        newDetailRow["NEWPALLETID"] = rAllot["PALLETID"];
                        newDetailRow["STATUS"] = "0";
                        tableDetail.Rows.Add(newDetailRow);
                        taskid++;
                        continue;
                    }
                }
            }
        }

        //this.dgAllotMaster.DataSource = tableMaster;
        //this.dgAllotMaster.DataBind();
        //this.dgAllotDetail.DataSource = tableDetail;
        //this.dgAllotDetail.DataBind();
        //GridRowSpan(dgAllotDetail, 1);
        Session["tableMaster"] = tableMaster;
        Session["tableDetail"] = tableDetail;

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

        tableAllotment = objAllot.GetEmptyAllotmentTable();
        Session["tableAllotment"] = tableAllotment;
       
        //this.btnSaveAllotment.Enabled = false;
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
       
    }
    #endregion

    #region 出库明细分配
    protected void dgDetail_EditCommand(object source, DataGridCommandEventArgs e)
    {
        for (int i = 0; i < dgAllotment.Items.Count; i++)
        {
            tableAllotment.Rows[i]["CELLCODE"] = ((TextBox)dgAllotment.Items[i].Cells[3].Controls[0]).Text;
            tableAllotment.Rows[i]["CELLNAME"] = ((TextBox)dgAllotment.Items[i].Cells[4].Controls[0]).Text;
            tableAllotment.Rows[i]["QUANTITY"] = Convert.ToDecimal(((TextBox)dgAllotment.Items[i].Cells[5].Controls[0]).Text);
        }

        if (e.CommandName == "Edit")
        {
            int index = e.Item.ItemIndex;
            DataRow newAllotRow = tableAllotment.NewRow();
            newAllotRow["BILLNO"] = dsDetail.Tables[0].Rows[index + (pageIndex - 1)]["BILLNO"];
            newAllotRow["DETAILID"] = dsDetail.Tables[0].Rows[index + (pageIndex - 1)]["ID"];
            newAllotRow["PRODUCTCODE"] = dsDetail.Tables[0].Rows[index + (pageIndex - 1)]["PRODUCTCODE"];
            newAllotRow["PRODUCTNAME"] = dsDetail.Tables[0].Rows[index + (pageIndex - 1)]["PRODUCTNAME"];
            newAllotRow["UNITCODE"] = dsDetail.Tables[0].Rows[index + (pageIndex - 1)]["UNITCODE"];
            newAllotRow["UNITNAME"] = dsDetail.Tables[0].Rows[index + (pageIndex - 1)]["UNITNAME"];
            newAllotRow["CELLCODE"] = "";
            newAllotRow["CELLNAME"] = "";
            newAllotRow["QUANTITY"] = dsDetail.Tables[0].Rows[index + (pageIndex - 1)]["QUANTITY"];
            newAllotRow["PALLETID"] = "";
            newAllotRow["STATUS"] = "0";
            tableAllotment.Rows.Add(newAllotRow);
        }
        tableAllotment.DefaultView.Sort = "BILLNO DESC";
        this.dgAllotment.DataSource = tableAllotment;
        Session["tableAllotment"] = tableAllotment;
        this.dgAllotment.DataBind();
        GridRowSpan(dgAllotment, 0);
    }
    #endregion

    #region 分配结果绑定
    protected void dgAllotment_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string CELLCODE = "''";
                for (int i = 0; i < tableAllotment.Rows.Count; i++)
                {
                    CELLCODE += ",'" + tableAllotment.Rows[i]["CELLCODE"] + "'";
                }
                if (e.Item.ItemIndex % 2 == 0)
                {
                    e.Item.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Item.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
                }

                TextBox tb1 = new TextBox();
                tb1.ID = "cellcode";//货位编号
                tb1.Text = e.Item.Cells[3].Text.Replace("&nbsp;", ""); ;
                //tb1.Attributes.Add("style", "WIDTH:110PX;");
                tb1.Width = 110;
                tb1.Attributes.Add("class", "GridInputAlignLeft");
                e.Item.Cells[3].Controls.Add(tb1);
                Button btn1 = new Button();
                btn1.CssClass = "ButtonBrowse2";
                //btn1.Text = "...";
                btn1.OnClientClick = string.Format("SelectDialog3('{0},{1},{2}','WMS_WH_CELL','CELLCODE,CELLNAME,QUANTITY','CURRENTPRODUCT','{3}','CELLCODE NOT IN ({4})')"
                                                    , tb1.ClientID, tb1.ClientID.Replace("code", "name")
                                                    , tb1.ClientID.Replace("code", "quantity")
                                                    , e.Item.Cells[1].Text
                                                    , CELLCODE.Replace("'"[0], '"'));
                e.Item.Cells[3].Controls.Add(btn1);

                TextBox tb2 = new TextBox();
                tb2.ID = "cellname";//货位名称
                tb2.Width = 100;
                tb2.Text = e.Item.Cells[4].Text.Replace("&nbsp;", "");
                tb2.Attributes.Add("class", "GridInputAlignLeft");
                tb2.Attributes.Add("onfocus", "CannotEdit(this)");
                e.Item.Cells[4].Controls.Add(tb2);

                TextBox box = new TextBox();
                box.ID = "cellquantity";//货位存量
                box.Text = "0.00";
                box.ForeColor = Color.White;
                box.Attributes.Add("style","position:absolute;width:35px;");
                box.Attributes.Add("class", "GridInput");
                //tb2.Attributes.Add("style", "width:90px;");
                e.Item.Cells[4].Controls.Add(box);


                TextBox tb3 = new TextBox();
                tb3.ID = "quantity";//分配数量
                tb3.Text = e.Item.Cells[5].Text;
                tb3.Attributes.Add("style", "width:60px;");
                tb3.Attributes.Add("class", "GridInputAlignRight");
                //tb3.Attributes.Add("onfocus", "CannotEdit(this)");
                e.Item.Cells[5].Controls.Add(tb3);
            }
        }
        catch { }
    }

    #endregion

    #region 分配结果删除
    protected void dgAllotment_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int index = e.Item.ItemIndex;
            tableAllotment.Rows.RemoveAt(index);
            this.dgAllotment.DataSource = tableAllotment;
            Session["tableAllotment"] = tableAllotment;
            this.dgAllotment.DataBind();
            GridRowSpan(dgAllotment, 0);
        }
    }    
    #endregion

    protected void dgAllotment_PreRender(object sender, EventArgs e)
    {
        if (tableAllotment.Rows.Count > 0)
        {
            for (int i = 0; i < dgAllotment.Items.Count; i++)
            {
                tableAllotment.Rows[i]["CELLCODE"] = ((TextBox)dgAllotment.Items[i].Cells[3].Controls[0]).Text;
                tableAllotment.Rows[i]["CELLNAME"] = ((TextBox)dgAllotment.Items[i].Cells[4].Controls[0]).Text;
                tableAllotment.Rows[i]["QUANTITY"] = Convert.ToDecimal(((TextBox)dgAllotment.Items[i].Cells[5].Controls[0]).Text);
            }
        }
        Session["tableAllotment"] = tableAllotment;
        this.dgAllotment.DataSource = tableAllotment;
        this.dgAllotment.DataBind();
    }
}
