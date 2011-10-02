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
public partial class Code_BasicInfo_BatchAssignedProduct : System.Web.UI.Page
{
    Warehouse objHouse = new Warehouse();
    WarehouseArea objArea = new WarehouseArea();
    WarehouseShelf objShelf = new WarehouseShelf();
    WarehouseCell objCell = new WarehouseCell();
    DataSet dsHouse;
    DataTable dsCell;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            LoadHouseTree();
            if (tvWarehouse.Nodes.Count >0)
            {
                this.hdnWarehouseCode.Value = tvWarehouse.Nodes[0].Value;
                this.tvWarehouse.Nodes[0].Selected = true;
                Change();
            }
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
        Change();
    }

    protected void Change()
    {
        if (tvWarehouse.SelectedNode.Depth == 0)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Value;
            this.hdnAreaCode.Value = "";
            this.hdnShelfCode.Value = "";
        }
        if (tvWarehouse.SelectedNode.Depth == 1)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Parent.Value;
            this.hdnAreaCode.Value = tvWarehouse.SelectedNode.Value;
            this.hdnShelfCode.Value = "";
        }
        if (tvWarehouse.SelectedNode.Depth == 2)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Parent.Parent.Value;
            this.hdnAreaCode.Value = tvWarehouse.SelectedNode.Parent.Value;
            this.hdnShelfCode.Value = tvWarehouse.SelectedNode.Value;
            TreeNode nodeShelf = tvWarehouse.FindNode(hdnWarehouseCode.Value + "/" + hdnAreaCode.Value + "/" + hdnShelfCode.Value);
            //nodeShelf.CollapseAll(); 
            nodeShelf.Expand();
            if (nodeShelf.ChildNodes.Count == 0)
            {
                DataSet dsTemp = objCell.QueryWarehouseCell("SHELFCODE='" + this.hdnShelfCode.Value + "'");
                foreach (DataRow r3 in dsTemp.Tables[0].Rows)
                {

                    if (nodeShelf != null)
                    {
                        if (!IsPostBack)
                        {
                            nodeShelf.CollapseAll();
                        }
                        TreeNode nodeCell = new TreeNode("货位：" + r3["CELLNAME"].ToString(), r3["CELLCODE"].ToString());
                        if ( r3["UNITNAME"].ToString().Contains("条"))
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
                        //nodeCell.NavigateUrl = "javascript:undo()";
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
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
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
        dsCell = objCell.QueryWarehouseCell("CELLCODE IN (" + selectedCellCode.ToString() + ") or SHELFCODE IN (" + selectedShelfCode.ToString() + ")").Tables[0];
        for (int i = 0; i < dsCell.Rows.Count; i++)
        {
            objCell.CELL_ID = int.Parse(dsCell.Rows[i]["CELL_ID"].ToString());
            objCell.ISACTIVE = this.ddlActive.SelectedValue;
            objCell.ASSIGNEDPRODUCT = this.txtAssignedProductCode.Text;
            objCell.UNITCODE = this.txtUnitCode.Text;
            objCell.ISVIRTUAL = this.ddlVirtual.SelectedValue;
            objCell.UpdateBatch();
        }
        if (dsCell.Rows.Count > 0)
        {
            Response.Write("<script>alert('修改成功！')</script>");
        }
        else
        {
            Response.Write("<script>alert('请选择货位！')</script>");
        }
        JScript.Instance.RegisterScript(this, "UpdateParent();");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        JScript.Instance.RegisterScript(this, "window.close();");
    }
}
