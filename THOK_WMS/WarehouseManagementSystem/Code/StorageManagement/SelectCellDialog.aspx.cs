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

public partial class Code_StorageManagement_SelectCellDialog : BasePage
{
    Warehouse objHouse = new Warehouse();
    WarehouseArea objArea = new WarehouseArea();
    WarehouseShelf objShelf = new WarehouseShelf();
    WarehouseCell objCell = new WarehouseCell();
    MoveBillDetail billDetail = new MoveBillDetail();
    DataSet dsHouse;
    DataSet dsArea;
    DataSet dsShelf;
    DataSet dsCell;
    string filter = "1=1";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["entryFliter"] != "" && Request.QueryString["entryFliter"] != null)
        {
            filter += " AND " + Request.QueryString["entryFliter"];
        }
        else if (Request.QueryString["outFliter"] != "" && Request.QueryString["outFliter"] != null)
        {
            filter += " AND " + Request.QueryString["outFliter"];
        }
        if (!IsPostBack)
        { 
            LoadHouseTree();
            if (this.tvWarehouse.Nodes.Count > 0)
            {
                this.hdnWarehouseCode.Value = tvWarehouse.Nodes[0].Value;
                this.lblCurrentNode.Text = tvWarehouse.Nodes[0].Text;
                this.tvWarehouse.Nodes[0].Selected = true;
                Change();
            }
        }
        else
        {
            Change();
        }
    }


    #region 加载仓库树结构
    protected void LoadHouseTree()
    {
        tvWarehouse.Attributes.Add("onclick", "postBackByObject()");
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

        //dsTemp = objCell.QueryAllCell();
        //foreach (DataRow r3 in dsTemp.Tables[0].Rows)
        //{
        //    TreeNode nodeShelf = tvWarehouse.FindNode(r3["WH_CODE"].ToString() + "/" + r3["AREACODE"].ToString() + "/" + r3["SHELFCODE"].ToString());
        //    if (nodeShelf != null)
        //    {
        //        if (!IsPostBack)
        //        {
        //            nodeShelf.CollapseAll();
        //        }
        //        TreeNode nodeCell = new TreeNode("货位：" + r3["CELLNAME"].ToString(), r3["CELLCODE"].ToString());
        //        if ("条" == r3["UNITNAME"].ToString())
        //        {
        //            nodeCell.Text = "货位(条)：<font color='#1E7ACE'>" + r3["CELLNAME"].ToString() + "</font>";
        //        }
        //        else if ("件" == r3["UNITNAME"].ToString())
        //        {
        //            nodeCell.Text = "货位(件)：<font color='#000000'>" + r3["CELLNAME"].ToString() + "</font>";
        //        }
        //        else
        //        {
        //            nodeCell.Text = string.Format("货位({0})：<font color='RED'>{1}</font>", r3["UNITNAME"].ToString(), r3["CELLNAME"].ToString());
        //        }
        //        nodeCell.ToolTip = r3["CELL_ID"].ToString();
        //        nodeShelf.ChildNodes.Add(nodeCell);
        //    }
        //}
    }

    #endregion

    #region 选中节点事件
    protected void tvWarehouse_SelectedNodeChanged(object sender, EventArgs e)
    {
        Change();
    }

    protected void Change()
    {
        this.lblCurrentNode.Text = tvWarehouse.SelectedNode.Text;

        if (tvWarehouse.SelectedNode.Depth == 0)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Value;
            this.hdnAreaCode.Value = "";
            this.hdnShelfCode.Value = "";
            filter += " AND WH_CODE='" + this.hdnWarehouseCode.Value + "'";
            this.dgCell.DataSource = objCell.QueryWarehouseCell(filter.Replace('"',"'"[0])).Tables[0];
            this.dgCell.DataBind();
        }
        if (tvWarehouse.SelectedNode.Depth == 1)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Parent.Value;
            this.hdnAreaCode.Value = tvWarehouse.SelectedNode.Value;
            this.hdnShelfCode.Value = "";
            filter += " AND AREACODE='" + this.hdnAreaCode.Value + "'";
            this.dgCell.DataSource = objCell.QueryWarehouseCell(filter.Replace('"', "'"[0])).Tables[0];
            this.dgCell.DataBind();
        }
        if (tvWarehouse.SelectedNode.Depth == 2)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Parent.Parent.Value;
            this.hdnAreaCode.Value = tvWarehouse.SelectedNode.Parent.Value;
            this.hdnShelfCode.Value = tvWarehouse.SelectedNode.Value;
            //filter = "SHELFCODE='" + this.hdnShelfCode.Value + "'";
            filter += " AND SHELFCODE='" + this.hdnShelfCode.Value + "'";
            this.dgCell.DataSource = objCell.QueryWarehouseCell(filter.Replace('"', "'"[0])).Tables[0];
            this.dgCell.DataBind();

        }
        ////else if (tvWarehouse.SelectedNode.Depth == 3)
        ////{
        ////    this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Parent.Parent.Parent.Value;
        ////    this.hdnAreaCode.Value = tvWarehouse.SelectedNode.Parent.Parent.Value;
        ////    this.hdnShelfCode.Value = tvWarehouse.SelectedNode.Parent.Value;
        //// }
    }
    #endregion

    #region DataGrid绑定事件
    protected void dgCell_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (Session["grid_EvenRowColor"] != null && e.Item.ItemType == ListItemType.Item)
                {
                    e.Item.Attributes.Add("style", string.Format("word-break:keep-all; white-space:nowrap; background-color:{0};", Session["grid_EvenRowColor"].ToString()));
                }
                if (Session["grid_OddRowColor"] != null && e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    e.Item.Attributes.Add("style", string.Format("word-break:keep-all; white-space:nowrap; background-color:{0};", Session["grid_OddRowColor"].ToString()));
                }
                string Value = "";
                for (int n = 0; n < e.Item.Cells.Count; n++)
                {
                    Value = Value + e.Item.Cells[n].Text.Replace("&nbsp;","") + "|";
                }
                e.Item.Attributes.Add("Title", "双击取值");
                e.Item.Attributes.Add("ondblclick", string.Format("ReturnValue('{0}');", Value));
                e.Item.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#e8e8e7',this.style.fontWeight=''; this.style.cursor='hand';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

            }
        }
        catch
        {
        }
    }

    #endregion
}
