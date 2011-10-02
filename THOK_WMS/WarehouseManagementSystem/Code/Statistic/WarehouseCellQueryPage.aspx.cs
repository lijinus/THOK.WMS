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

public partial class Code_Statistic_WarehouseCellQueryPage :BasePage
{
    Warehouse objHouse = new Warehouse();
    WarehouseArea objArea = new WarehouseArea();
    WarehouseShelf objShelf = new WarehouseShelf();
    WarehouseCell objCell = new WarehouseCell();
    
    DataSet dsHouse;
    DataSet dsArea;
    DataSet dsShelf;
    DataSet dsCell;
    DataTable tableCell;
    protected void Page_Load(object sender, EventArgs e)
    {
        objCell.UpdateCellEx();
        objCell.UpdateCell();
        if (!IsPostBack)
        {
            LoadHouseTree();
            tableCell = objCell.QueryAllCell().Tables[0];
            //ShowCell(tableCell);
            ShowCellChart(tableCell);
        }
        else
        {
            tableCell = objCell.QueryAllCell().Tables[0];
            //ShowCell(tableCell);
            ShowCellChart(tableCell);
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
                nodeCell.NavigateUrl = "javascript:function(){return false;}";
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
        dv.Sort="AREACODE,SHELFCODE,LAYER_NO desc,CELLCODE";
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



            if (dv[i]["ISACTIVE"].ToString() == "0")
            {
                tc.Text = string.Format("{0}<br/>未启用" + "{1}<BR/>{2}{3}"
                       , dv[i]["CELLNAME"].ToString()
                       , dv[i]["C_PRODUCTNAME"].ToString()
                       , dv[i]["QUANTITY"].ToString(), dv[i]["UNITNAME"].ToString());
                tc.Attributes.Add("style", "background:#fbfbfb url(../../images/bg_cell.jpg) no-repeat 0px " + top.ToString() + "px; color:#cccccc;");
            }
            else
            {
                if (dv[i]["ISLOCKED"].ToString() == "1")
                {
                    tc.Text = string.Format("{0}<br/>" + "{1}<BR/><font color='red'>{2}</font>{3}<BR/>被锁定"
                       , dv[i]["CELLNAME"].ToString()
                       , dv[i]["C_PRODUCTNAME"].ToString()
                       , dv[i]["QUANTITY"].ToString(), dv[i]["UNITNAME"].ToString());
                    tc.Attributes.Add("style", "background:#fbfbfb url(../../images/bg_cell.jpg) no-repeat 0px " + top.ToString() + "px;");
                }
                else
                {
                    if ( dv[i]["C_PRODUCTNAME"].ToString() != "" || decimal.Parse( dv[i]["QUANTITY"].ToString()) != 0 || decimal.Parse(dv[i]["FROZEN_IN_QTY"].ToString()) != 0 || decimal.Parse(dv[i]["FROZEN_OUT_QTY"].ToString()) != 0)
                    {
                        tc.Text = string.Format("{0}<br/>" + "{1}<BR/><font color='red'>{2}</font>{3}"
                           , dv[i]["CELLNAME"].ToString()
                           , dv[i]["C_PRODUCTNAME"].ToString()
                           , dv[i]["QUANTITY"].ToString(), dv[i]["UNITNAME"].ToString());
                        tc.Attributes.Add("style", "background:#fbfbfb url(../../images/bg_cell.jpg) no-repeat 0px " + top.ToString() + "px;");
                    }
                    else 
                    {
                        tc.Text = string.Format("{0}<br/>" + "{1}<BR/><font color='red'>{2}</font>{3}"
                          , dv[i]["CELLNAME"].ToString()
                          , dv[i]["A_PRODUCTNAME"].ToString()
                          , dv[i]["QUANTITY"].ToString(), dv[i]["UNITNAME"].ToString());
                        if (dv[i]["A_PRODUCTNAME"].ToString() == "")
                        {
                            tc.Attributes.Add("style", "background:#fbfbfb url(../../images/bg_cell.jpg) no-repeat 0px " + top.ToString() + "px;");
                        }
                        else 
                        {
                            tc.Attributes.Add("style", "background:green url(../../images/bg_cell.jpg) no-repeat 0px " + top.ToString() + "px;");
                        }
                    }
                }
            }

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
                tableCell = objCell.QueryWarehouseCell("WH_CODE='"+tvWarehouse.SelectedNode.Value+"'").Tables[0];
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
}
