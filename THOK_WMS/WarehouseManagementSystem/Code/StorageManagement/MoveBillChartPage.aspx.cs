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
public partial class Code_StorageManagement_MoveBillChartPage : BasePage
{
    MoveBillMaster billMaster = new MoveBillMaster();
    MoveBillDetail billDetail = new MoveBillDetail();
    Warehouse objHouse = new Warehouse();
    WarehouseArea objArea = new WarehouseArea();
    WarehouseShelf objShelf = new WarehouseShelf();
    WarehouseCell objCell = new WarehouseCell();
    DataSet dsHouse;
    DataSet dsArea;
    DataSet dsShelf;
    DataSet dsCell;
    DataTable tableCell;
    DataTable tableDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //LoadHouseTree();
            tableCell = objCell.QueryAllCell().Tables[0];
            //ShowCell(tableCell);
            ShowCellChart(tableCell);

            if (Request.QueryString["BILLNO"] != null)
            {
                this.billNo.Value = Request.QueryString["BILLNO"];
            }
            tableDetail = billDetail.QueryByBillNo(this.billNo.Value).Tables[0];
            GridDataBind();
            Session["tableDetail"] = tableDetail;
        }
        else
        {
            tableCell = objCell.QueryAllCell().Tables[0];
            //ShowCell(tableCell);
            ShowCellChart(tableCell);

            tableDetail = (DataTable)Session["tableDetail"];
            GridDataBind();
        }
    }

    protected void GridDataBind()
    {
        this.moveList.DataSource = tableDetail;
        this.moveList.DataBind();
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
        foreach (DataRow r3 in dsTemp.Tables[0].Rows)
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
                nodeCell.NavigateUrl = "javascript:return false;";
                nodeShelf.ChildNodes.Add(nodeCell);
            }
        }
    }

    #endregion

    #region 显示货位图表
    protected void ShowCell(DataTable tableCell)
    {
        this.pnlCell.Controls.Clear();
        for (int i = 0; i < tableCell.Rows.Count; i++)
        {
            Panel p = new Panel();
            p.Attributes.Add("class", "panel");
            Table table = new Table();
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();

            int iUpper = 30; //货位存量上限
            int iCurrent = 0;

            string a = tableCell.Rows[i]["QUANTITY"].ToString();
            decimal d = Convert.ToDecimal(a);
            iCurrent = Convert.ToInt32(d);   //当前存货量(件)

            int iRemain = iUpper - iCurrent;
            //decimal d = Decimal.Divide(iRemain, iUpper);
            //int top = (int)Math.Floor((Math.Round(d, 2)*80));
            int top = 90 / iUpper * iRemain;



            if (tableCell.Rows[i]["ISACTIVE"].ToString() == "0")
            {
                tc.Text = string.Format("{0}<br/>未启用" + "{1}<BR/>{2}{3}"
                       , tableCell.Rows[i]["CELLNAME"].ToString()
                       , tableCell.Rows[i]["C_PRODUCTNAME"].ToString()
                       , tableCell.Rows[i]["QUANTITY"].ToString(), tableCell.Rows[i]["UNITNAME"].ToString());
                tc.Attributes.Add("style", "background:#fbfbfb url(../../images/bg_cell.jpg) no-repeat 0px " + top.ToString() + "px; color:#cccccc;");
            }
            else
            {
                tc.Text = string.Format("{0}<br/>" + "{1}<BR/><font color='red'>{2}</font>{3}"
                                       , tableCell.Rows[i]["CELLNAME"].ToString()
                                       , tableCell.Rows[i]["C_PRODUCTNAME"].ToString()
                                       , tableCell.Rows[i]["QUANTITY"].ToString(), tableCell.Rows[i]["UNITNAME"].ToString());
                tc.Attributes.Add("style", "background:#fbfbfb url(../../images/bg_cell.jpg) no-repeat 0px " + top.ToString() + "px;");
            }

            tc.Attributes.Add("class", "cell");
            table.CellPadding = 2;
            table.CellSpacing = 2;
            tr.Controls.Add(tc);
            table.Controls.Add(tr);
            p.Controls.Add(table);



            this.pnlCell.Controls.Add(p);
        }
    }
    #endregion

    #region 显示货位图表
    protected void ShowCellChart(DataTable tableCell)
    {
        this.pnlCell.Controls.Clear();
        DataView dv = tableCell.DefaultView;
        dv.Sort = "AREACODE,SHELFCODE,LAYER_NO desc,CELLCODE";
        string layer = "0";
        Table tableShelf = new Table();
        tableShelf.Attributes.Add("style", "width:3700px;");
        TableRow trShelf = new TableRow();
        //tableShelf.CellPadding = 2;
        //tableShelf.CellSpacing = 2;

        for (int i = 0; i < tableCell.Rows.Count; i++)
        {

            TableCell tc = new TableCell();

            /////
            if (dv[i]["LAYER_NO"].ToString() != layer)
            {
                layer = dv[i]["LAYER_NO"].ToString();
                trShelf.Controls.Add(new TableCell());
                trShelf = new TableRow();
                tableShelf.Controls.Add(trShelf);
                pnlCell.Controls.Add(tableShelf);
            }

            int iUpper = Convert.ToInt32(dv[i]["MAX_QUANTITY"]);//30; //货位存量上限
            int iCurrent = 0;

            string a = dv[i]["QUANTITY"].ToString();
            decimal d = Convert.ToDecimal(a);
            iCurrent = Convert.ToInt32(d);   //当前存货量(件)

            int iRemain = iUpper - iCurrent;
            //decimal d = Decimal.Divide(iRemain, iUpper);
            //int top = (int)Math.Floor((Math.Round(d, 2)*80));
            int top = 0;
            if (iUpper == 30)
            {
                top = 90 / iUpper * iRemain;
                tc.Attributes.Add("class", "cell");
            }
            else
            {
                top = 150 / iUpper * iRemain;
                tc.Attributes.Add("class", "cell2");
            }


            Panel pCell = new Panel(); pCell.Attributes.Add("style", "height:88px;position:relative; cursor:move;");
            Label lblContent = new Label();
            pCell.Controls.Add(lblContent);
            pCell.Attributes.Add("cellcode", dv[i]["CELLCODE"].ToString());
            pCell.Attributes.Add("cellname", dv[i]["CELLNAME"].ToString());
            pCell.Attributes.Add("productcode", dv[i]["CURRENTPRODUCT"].ToString());
            pCell.Attributes.Add("productname", dv[i]["C_PRODUCTNAME"].ToString());
            pCell.Attributes.Add("quantity", dv[i]["QUANTITY"].ToString());
            pCell.Attributes.Add("unitcode", dv[i]["UNITCODE"].ToString());
            pCell.Attributes.Add("unitname", dv[i]["UNITNAME"].ToString());
            if (dv[i]["ISACTIVE"].ToString() == "0")
            {
                lblContent.Text = string.Format("<br/>{0}<br/>未启用" + "{1}<BR/>{2}{3}"
                       , dv[i]["CELLNAME"].ToString()
                       , dv[i]["C_PRODUCTNAME"].ToString()
                       , dv[i]["QUANTITY"].ToString(), dv[i]["UNITNAME"].ToString());
                tc.Attributes.Add("style", "background:#fbfbfb url(../../images/bg_cell.jpg) no-repeat 0px " + top.ToString() + "px; color:#cccccc;");
            }
            else
            {
                if (dv[i]["ISLOCKED"].ToString() == "1")
                {
                    lblContent.Text = string.Format("<br/>{0}<br/>" + "{1}<BR/><font color='red'>{2}</font>{3}<BR/>被锁定"
                       , dv[i]["CELLNAME"].ToString()
                       , dv[i]["C_PRODUCTNAME"].ToString()
                       , dv[i]["QUANTITY"].ToString(), dv[i]["UNITNAME"].ToString());
                    tc.Attributes.Add("style", "background:#fbfbfb url(../../images/bg_cell.jpg) no-repeat 0px " + top.ToString() + "px;");
                }
                else
                {
                    lblContent.Text = string.Format("<br/>{0}<br/>" + "{1}<BR/><font color='red'>{2}</font>{3}"
                       , dv[i]["CELLNAME"].ToString()
                       , dv[i]["C_PRODUCTNAME"].ToString()
                       , dv[i]["QUANTITY"].ToString(), dv[i]["UNITNAME"].ToString());
                    tc.Attributes.Add("style", "background:#fbfbfb url(../../images/bg_cell.jpg) no-repeat 0px " + top.ToString() + "px;");
                }
            }
            tc.Controls.Add(pCell);
            if (d > 0.00M)
            {
                pCell.Attributes.Add("onmousedown", "MouseDownToMove(this)");
                pCell.Attributes.Add("onmousemove", "MouseMoveToMove(this)");
                pCell.Attributes.Add("onmouseup", "MouseUpToMove(this)");
            }
            else
            {
                pCell.Attributes.Add("onmousedown", "MoveIn(this)");
            }
            pCell.Attributes.Add("onmouseover", "MouseOverFun(this)");
            trShelf.Controls.Add(tc);
        }
    }
    #endregion

    #region 节点选中事件
    protected void tvWarehouse_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (tvWarehouse.SelectedNode != null)
        {
            if (tvWarehouse.SelectedNode.Depth == 0)
            {
                tableCell = objCell.QueryWarehouseCell("WH_CODE='" + tvWarehouse.SelectedNode.Value + "'").Tables[0];
            }
            else if (tvWarehouse.SelectedNode.Depth == 1)
            {
                tableCell = objCell.QueryWarehouseCell("AREACODE='" + tvWarehouse.SelectedNode.Value + "'").Tables[0];
            }
            else if (tvWarehouse.SelectedNode.Depth == 2)
            {
                tableCell = objCell.QueryWarehouseCell("SHELFCODE='" + tvWarehouse.SelectedNode.Value + "'").Tables[0];
            }
            //else if (tvWarehouse.SelectedNode.Depth == 3)
            //{
            //    tableCell = objCell.QueryWarehouseCell("CELLCODE='" + tvWarehouse.SelectedNode.Value + "'").Tables[0];
            //}
        }
        ShowCellChart(tableCell);
    }
    #endregion


    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string cellcode_out = this.cellcode_out.Value;  //移出货位
        string cellname_out = this.cellname_out.Value;
        decimal cellquantity_out = Convert.ToDecimal(this.cellquantity_out.Value);
        string productcode_out = this.productcode_out.Value;
        string productname_out = this.productname_out.Value;
        string unitcode = this.unitcode.Value;
        string unitname = this.unitname.Value;

        string cellcode_in = this.cellcode_in.Value;  //移入货位
        string cellname_in = this.cellname_in.Value;
        decimal cellquantity_in = Convert.ToDecimal(this.cellquantity_in.Value);
        string productcode_in = this.productcode_in.Value;
        string productname_in = this.productname_in.Value;

        if (productcode_in != productcode_out && cellquantity_in > 0.00M)
        {
            JScript.Instance.ShowMessage(this,"产品不同，不能移入");
            return;
        }
        DataRow newRow = tableDetail.NewRow();
        newRow["OUT_CELLCODE"] = cellcode_out;
        newRow["OUT_CELLNAME"] = cellname_out;
        newRow["IN_CELLCODE"] = cellcode_in;
        newRow["IN_CELLNAME"] = cellname_in;
        newRow["PRODUCTCODE"] = productcode_out;
        newRow["PRODUCTNAME"] = productname_out;
        newRow["UNITCODE"] = unitcode;
        newRow["UNITNAME"] = unitname;
        newRow["QUANTITY"] = cellquantity_out;
        tableDetail.Rows.Add(newRow);
        Session["tableDetail"] = tableDetail;
        GridDataBind();
    }


    protected void btnCreateBill_Click(object sender, EventArgs e)
    {
        //JScript.Instance.ShowMessage(this, this.moveList.Items.Count.ToString());
        string billNo = this.billNo.Value;
        if (this.billNo.Value == "")
        {
            billNo = billMaster.GetNewBillNo();
            billMaster.BILLNO = billNo;
            billMaster.BILLDATE = System.DateTime.Now;
            billMaster.BILLTYPE = "";
            billMaster.STATUS = "1";
            billMaster.OPERATEPERSON = Session["EmployeeCode"].ToString();
            billMaster.Insert();
        }
        else
        {
            DataSet ds = billDetail.QueryByBillNo(billNo);
            for(int k=0;k<ds.Tables[0].Rows.Count;k++)
            {
                ds.Tables[0].Rows[k].Delete();
            }
            billDetail.Delete(ds);
        }
        for (int i = 0; i < moveList.Items.Count; i++)
        {
            billDetail.BILLNO = billNo;
            billDetail.OUT_CELLCODE = moveList.Items[i].Cells[0].Text;
            billDetail.PRODUCTCODE = moveList.Items[i].Cells[2].Text;
            billDetail.IN_CELLCODE = moveList.Items[i].Cells[4].Text;
            billDetail.UNITCODE = moveList.Items[i].Cells[6].Text;
            billDetail.QUANTITY = Convert.ToDecimal(moveList.Items[i].Cells[8].Text);
            billDetail.STATUS = "0";
            billDetail.Insert();
        }

        Response.Redirect("MoveBillEditPage.aspx?BILLNO=" + billNo);
    }


    protected void moveList_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {

        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnk = new LinkButton();
            lnk.Text = "删除";
            lnk.CommandName = "Delete";
            e.Item.Cells[9].Controls.Add(lnk);
        }
    }

    protected void moveList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        int index = e.Item.ItemIndex;
        tableDetail.Rows.RemoveAt(index);
        Session["tableDetail"] = tableDetail;
        GridDataBind();
    }
}
