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
using System.Text;

public partial class Code_StorageManagement_CheckBillByCell :BasePage
{
    public string div01display = "block";
    public string div02display = "none";
    Warehouse objHouse = new Warehouse();
    WarehouseArea objArea = new WarehouseArea();
    WarehouseShelf objShelf = new WarehouseShelf();
    WarehouseCell objCell = new WarehouseCell();
    DataSet dsHouse;
    string cellCodes = "''";//移动货位
    string filter = "1=1";
    int pageIndex = 1;
    int pageSize = 10;
    WarehouseCell warecell = new WarehouseCell();
    protected void Page_Load(object sender, EventArgs e)
    {
        warecell.UpdateCellEx();
        warecell.UpdateCell();
        if (!IsPostBack)
        {

            LoadHouseTree();
            if (this.tvWarehouse.Nodes.Count > 0)
            {
                this.hdnWarehouseCode.Value = tvWarehouse.Nodes[0].Value;
                this.lblCurrentNode.Text = tvWarehouse.Nodes[0].Text;
                this.tvWarehouse.Nodes[0].Selected = true;
                pager.PageSize = pageSize;
                ViewState["pageIndex"] = pageIndex;
                Change();
                
            }
        }
        else
        {
            filter = ViewState["filter"].ToString();
            if (ViewState["cellCodes"] != null && ViewState["cellCodes"] != "")
            {
                cellCodes = ViewState["cellCodes"].ToString();
            }
            pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
            DataGridBind();
        }
    }


    #region 加载仓库树结构
    protected void LoadHouseTree()
    {
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
    }

    #endregion

    #region 选中节点事件
    protected void tvWarehouse_SelectedNodeChanged(object sender, EventArgs e)
    {
       ViewState["cellCodes"] = "";
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
            filter = "WH_CODE='" + this.hdnWarehouseCode.Value + "'";
            ViewState["filter"] = filter;
            ViewState["pageIndex"] = 1;
            
        }
        if (tvWarehouse.SelectedNode.Depth == 1)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Parent.Value;
            this.hdnAreaCode.Value = tvWarehouse.SelectedNode.Value;
            this.hdnShelfCode.Value = "";
            filter = "AREACODE='" + this.hdnAreaCode.Value + "'";
            ViewState["filter"] = filter;
            ViewState["pageIndex"] = 1;

        }
        if (tvWarehouse.SelectedNode.Depth == 2)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Parent.Parent.Value;
            this.hdnAreaCode.Value = tvWarehouse.SelectedNode.Parent.Value;
            this.hdnShelfCode.Value = tvWarehouse.SelectedNode.Value;
            filter = "SHELFCODE='" + this.hdnShelfCode.Value + "'";
            ViewState["filter"] = filter;
            ViewState["pageIndex"] = 1;

            TreeNode nodeShelf = tvWarehouse.FindNode(hdnWarehouseCode.Value + "/" + hdnAreaCode.Value + "/" + hdnShelfCode.Value);
            nodeShelf.CollapseAll();
            if (nodeShelf.ChildNodes.Count == 0)
            {
                DataSet dsTemp = objCell.QueryWarehouseCell("SHELFCODE='"+this.hdnShelfCode.Value+"'AND ISACTIVE=1");
                foreach (DataRow r3 in dsTemp.Tables[0].Rows)
                {
                    
                    if (nodeShelf != null)
                    {
                        if (!IsPostBack)
                        {
                            nodeShelf.CollapseAll();
                        }
                        TreeNode nodeCell = new TreeNode("货位：" + r3["CELLNAME"].ToString(), r3["CELLCODE"].ToString());
                        if (r3["UNITNAME"].ToString().Contains("条"))
                        {
                            nodeCell.Text = string.Format("货位({0})：<font color='#1E7ACE'>{1}</font>", r3["UNITNAME"].ToString(), r3["CELLNAME"].ToString()); 
                        }
                        else if (r3["UNITNAME"].ToString().Contains("件"))
                        {
                            nodeCell.Text = string.Format("货位({0})：<font color='#000000'>{1}</font>", r3["UNITNAME"].ToString(), r3["CELLNAME"].ToString());
                        }
                        else
                        {
                            nodeCell.Text = string.Format("货位({0})：<font color='RED'>{1}</font>", r3["UNITNAME"].ToString(), r3["CELLNAME"].ToString());
                        }
                        nodeCell.ToolTip = r3["CELL_ID"].ToString();
                        nodeCell.NavigateUrl = "javascript:undo()";
                        nodeShelf.ChildNodes.Add(nodeCell);
                    }
                }
            }

        }
        else if (tvWarehouse.SelectedNode.Depth == 3)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Parent.Parent.Parent.Value;
            this.hdnAreaCode.Value = tvWarehouse.SelectedNode.Parent.Parent.Value;
            this.hdnShelfCode.Value = tvWarehouse.SelectedNode.Parent.Value;
        }
        DataGridBind();
    }
    #endregion

    protected void DataGridBind()
    {
        pager.RecordCount = objCell.GetRowCount(filter);
        pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
        this.dgCell.DataSource = objCell.QueryWarehouseCell(filter,pageIndex,pageSize).Tables[0];
        this.dgCell.DataBind();
    }

    #region DataGrid绑定事件
    protected void dgCell_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                //for (int i = 0; i < dgCell.Items.Count; i++)
                //{
                //    e.Item.Attributes.Add("style", " z-index:10;    position:relative;   top:expression(this.offsetParent.scrollTop);");
                //    e.Item.Attributes.Add("class", "GridHeader2");
                //}
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int sn = e.Item.ItemIndex + 1;
                e.Item.Cells[0].Text = sn.ToString();
                if (Session["grid_EvenRowColor"] != null && e.Item.ItemType == ListItemType.Item)
                {
                    e.Item.Attributes.Add("style", string.Format("word-break:keep-all; white-space:nowrap; background-color:{0};", Session["grid_EvenRowColor"].ToString()));
                }
                if (Session["grid_OddRowColor"] != null && e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    e.Item.Attributes.Add("style", string.Format("word-break:keep-all; white-space:nowrap; background-color:{0};", Session["grid_OddRowColor"].ToString()));
                }

                e.Item.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='WhiteSmoke',this.style.fontWeight=''; this.style.cursor='hand';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

            }
        }
        catch
        {
        }
    }

    #endregion


    #region  下一步按钮
    protected void btnNext1_Click(object sender, EventArgs e)
    {
        DataTable tableCell;
        if (cellCodes != "" && cellCodes!="''")
        {
            tableCell = objCell.QueryWarehouseCell(filter).Tables[0];
        }
        else
        {
            StringBuilder selectedCellCode = new StringBuilder();
            selectedCellCode.Append("''");
            StringBuilder selectedShelfCode = new StringBuilder();
            selectedShelfCode.Append("''");
            foreach (TreeNode node in tvWarehouse.Nodes)
            {
                if (node.ChildNodes.Count > 0)
                {
                    foreach (TreeNode nodeArea in node.ChildNodes)
                    {
                        if (nodeArea.ChildNodes.Count > 0)
                        {
                            foreach (TreeNode nodeShelf in nodeArea.ChildNodes)
                            {
                                if (nodeShelf.Checked)
                                {
                                    selectedShelfCode.Append(",'" + nodeShelf.Value + "'");
                                }
                                else
                                {
                                    if (nodeShelf.ChildNodes.Count > 0)
                                    {
                                        foreach (TreeNode nodeCell in nodeShelf.ChildNodes)
                                        {
                                            if (nodeCell.Checked)
                                            {
                                                selectedCellCode.Append(",'" + nodeCell.Value + "'");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Session["selectedCellCode"] = selectedCellCode.ToString();
            Session["selectedShelfCode"] = selectedShelfCode.ToString();
            tableCell = objCell.QueryWarehouseCell("CELLCODE IN (" + selectedCellCode.ToString() + ") or SHELFCODE IN (" + selectedShelfCode.ToString() + ")").Tables[0];
        }
        this.dgSelectedCell.DataSource = tableCell;
        this.dgSelectedCell.DataBind();
        if (tableCell.Rows.Count == 0)
        {
            this.btnSave.Enabled = false;
        }
        else
        {
            this.btnSave.Enabled = true;
        }
        div01display = "none";
        div02display = "block";
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CheckBillMaster billMaster = new CheckBillMaster();
        DataTable tableCell;
        if (cellCodes != "" && cellCodes != "''")
        {
            tableCell = objCell.QueryWarehouseCell(filter).Tables[0];
        }
        else
        {
            tableCell = objCell.QueryWarehouseCell("CELLCODE IN (" + Session["selectedCellCode"].ToString() + ") or SHELFCODE IN (" + Session["selectedShelfCode"].ToString() + ") and quantity>0").Tables[0];
        }
        if (tableCell.Rows.Count == 0)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, "您所选的货位为空，请重新选择！");
        }
        else
        {
            billMaster.BatchInsertBill(tableCell, Session["EmployeeCode"].ToString());
            JScript.Instance.ShowMessage(this.UpdatePanel1, "盘点单已经生成");
        }
    }

    # region 分页控件 页码changing事件
    protected void pager_PageChanging(object src, PageChangingEventArgs e)
    {
        pager.CurrentPageIndex = e.NewPageIndex;
        pageIndex = pager.CurrentPageIndex;
        ViewState["pageIndex"] = pageIndex;
        DataGridBind();
    }
    #endregion

   
}
