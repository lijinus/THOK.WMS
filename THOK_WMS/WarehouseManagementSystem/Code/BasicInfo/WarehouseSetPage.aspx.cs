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

public partial class Code_BasicInfo_Warehouse : System.Web.UI.Page
{
    Warehouse objHouse = new Warehouse();
    WarehouseArea objArea = new WarehouseArea();
    WarehouseShelf objShelf = new WarehouseShelf();
    WarehouseCell objCell = new WarehouseCell();
    DataSet dsHouse;
    DataSet dsArea;
    DataSet dsShelf;
    DataSet dsCell;
    int pageIndexShelf = 1;
    int pageSizeShelf = 10;
    int pageIndexCell = 1;
    int pageSizeCell = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            LoadHouseTree();
            if (this.tvWarehouse.Nodes.Count > 0)
            {
                this.hdnWarehouseCode.Value = tvWarehouse.Nodes[0].Value;
                this.lblCurrentNode.Text = tvWarehouse.Nodes[0].Text;
                this.tvWarehouse.Nodes[0].Selected = true;
                this.btnNewArea.Enabled = true;
                GridDataBind();
            }
        }
        else
        {
            LoadHouseTree();
            GridDataBind();
        }
    }


    protected void GridDataBind()
    {
        BindWarehouse();
        if (this.hdnShelfCode.Value != "")
        {
            dsArea = objArea.QueryAreaByWHCODE(this.hdnWarehouseCode.Value);
            this.dgArea.DataSource = dsArea.Tables[0];
            this.dgArea.DataBind();

            dsShelf = objShelf.QueryShelfByAreaCode(this.hdnAreaCode.Value);
            this.dgShelf.DataSource = dsShelf.Tables[0].DefaultView;
            this.dgShelf.DataBind();

            dsCell = objCell.QueryWarehouseCell("SHELFCODE='" + this.hdnShelfCode.Value + "'");
            dgCell.DataSource = dsCell.Tables[0].DefaultView;
            dgCell.DataBind();
        }

        if (this.hdnAreaCode.Value != "" && this.hdnShelfCode.Value=="")
        {
            dsArea = objArea.QueryAreaByWHCODE(this.hdnWarehouseCode.Value);
            this.dgArea.DataSource = dsArea.Tables[0];
            this.dgArea.DataBind();

            dsShelf = objShelf.QueryShelfByAreaCode(this.hdnAreaCode.Value);
            this.dgShelf.DataSource = dsShelf.Tables[0].DefaultView;
            this.dgShelf.DataBind();

            dsCell = objCell.QueryWarehouseCell("AREACODE='" + this.hdnAreaCode.Value + "'");
            dgCell.DataSource = dsCell.Tables[0].DefaultView;
            dgCell.DataBind();
        }


        if (this.hdnWarehouseCode.Value != "" && this.hdnAreaCode.Value=="")
        {
            dsArea = objArea.QueryAreaByWHCODE(this.hdnWarehouseCode.Value);
            this.dgArea.DataSource = dsArea.Tables[0];
            this.dgArea.DataBind();

            dsShelf = objShelf.QueryShelfByWHCODE(this.hdnWarehouseCode.Value);
            this.dgShelf.DataSource = dsShelf.Tables[0].DefaultView;
            this.dgShelf.DataBind();

            dsCell = objCell.QueryWarehouseCell("WH_CODE='" + this.hdnWarehouseCode.Value + "'");
            dgCell.DataSource = dsCell.Tables[0].DefaultView;
            dgCell.DataBind();
        }
    }

    protected void BindWarehouse()
    {
        dsHouse = objHouse.QueryAllWarehouse();
        this.dgHouse.DataSource = dsHouse.Tables[0];
        this.dgHouse.DataBind();
    }

    protected void BindArea()
    {
        dsArea = objArea.QueryAreaByWHCODE(this.hdnWarehouseCode.Value);
        this.dgArea.DataSource = dsArea.Tables[0];
        this.dgArea.DataBind();
        if (dsArea.Tables[0].Rows.Count > 0)
        {
            this.hdnAreaCode.Value = dsArea.Tables[0].Rows[0]["AREACODE"].ToString();
        }
        else
        {
            this.hdnAreaCode.Value = "";
        }
        BindShelf();
    }

    protected void BindShelf()
    {
        dsShelf = objShelf.QueryShelfByAreaCode(this.hdnAreaCode.Value);
        this.dgShelf.DataSource = dsShelf.Tables[0];
        this.dgShelf.DataBind();
        if (dsShelf.Tables[0].Rows.Count > 0)
        {
            this.hdnShelfCode.Value = dsShelf.Tables[0].Rows[0]["SHELFCODE"].ToString();
        }
        else
        {
            this.hdnShelfCode.Value = "";
        }
        BindCell();
    }

    protected void BindCell()
    {
        dsCell = objCell.QueryWarehouseCell("SHELFCODE='" + this.hdnShelfCode.Value + "'");
        this.dgCell.DataSource = dsCell.Tables[0];
        this.dgCell.DataBind();
    }

    #region 加载仓库树结构
    protected void LoadHouseTree()
    {
        this.tvWarehouse.Nodes.Clear();
        BindWarehouse();
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
                    nodeHouse.ChildNodes.Add(new TreeNode("库区：" + r["AREANAME"].ToString(), r["AREACODE"].ToString()));
                }
            }
        }

        dsTemp = objShelf.QueryAllShelf();
        foreach (DataRow r2 in dsTemp.Tables[0].Rows)
        {
            TreeNode nodeArea = tvWarehouse.FindNode(r2["WH_CODE"].ToString() + "/" + r2["AREACODE"].ToString());
            if (nodeArea != null)
            {
                nodeArea.ChildNodes.Add(new TreeNode("货架：" + r2["SHELFNAME"].ToString(), r2["SHELFCODE"].ToString()));
                //nodeArea.ExpandAll();
            }

        }

        dsTemp = objCell.QueryAllCell();
        foreach (DataRow r3 in dsTemp.Tables[0].Rows)
        {
            TreeNode nodeShelf = tvWarehouse.FindNode(r3["WH_CODE"].ToString() +"/"+r3["AREACODE"].ToString()+ "/"+r3["SHELFCODE"].ToString());
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
                //nodeShelf.ChildNodes.Add(new TreeNode("货位：" + r3["CELLNAME"].ToString(), r3["CELLCODE"].ToString()));
            }
        }

        //tvWarehouse.ExpandDepth = 2;
    }

    #endregion

    #region 选中节点事件
    protected void tvWarehouse_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.lblCurrentNode.Text = tvWarehouse.SelectedNode.Text;

        if (tvWarehouse.SelectedNode.Depth == 0)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Value;
            this.hdnAreaCode.Value = "";
            this.hdnShelfCode.Value = "";
            this.btnNewArea.Enabled = true;
            this.btnNewShelf.Enabled = false;
            this.btnNewCell.Enabled = false;

        }
        if (tvWarehouse.SelectedNode.Depth ==1)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Parent.Value;
            this.hdnAreaCode.Value = tvWarehouse.SelectedNode.Value;
            this.hdnShelfCode.Value = "";
            this.btnNewArea.Enabled = false;
            this.btnNewShelf.Enabled = true;
            this.btnNewCell.Enabled = false;
        }
        if (tvWarehouse.SelectedNode.Depth == 2)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Parent.Parent.Value;
            this.hdnAreaCode.Value = tvWarehouse.SelectedNode.Parent.Value;
            this.hdnShelfCode.Value = tvWarehouse.SelectedNode.Value;
            this.btnNewArea.Enabled = false;
            this.btnNewShelf.Enabled = false;
            this.btnNewCell.Enabled = true;
        }
        else if (tvWarehouse.SelectedNode.Depth == 3)
        {
            this.hdnWarehouseCode.Value = tvWarehouse.SelectedNode.Parent.Parent.Parent.Value;
            this.hdnAreaCode.Value = tvWarehouse.SelectedNode.Parent.Parent.Value;
            this.hdnShelfCode.Value = tvWarehouse.SelectedNode.Parent.Value;
            this.btnNewArea.Enabled = false;
            this.btnNewShelf.Enabled = false;
            this.btnNewCell.Enabled = false;
        }

        GridDataBind();
    }
    #endregion

    #region DataGrid绑定事件
    protected void dgHouse_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.Cells[0].Text = "操作";
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkModify = new LinkButton();
                lnkModify.Text = "修改 ";
                lnkModify.OnClientClick = "OpenEditWarehouse('"+e.Item.Cells[2].Text+"')";


                LinkButton lnkDel = new LinkButton();
                lnkDel.Text = " 删除";
                lnkDel.CommandName = "Delete";
                lnkDel.OnClientClick = "return DeleteConfirm()";
                //lnkDel.Click+=new EventHandler(lnkDel_Click);
                e.Item.Cells[0].Controls.Add(lnkModify);
                e.Item.Cells[0].Controls.Add(lnkDel);

                if (e.Item.Cells[5].Text == "0")
                {
                    e.Item.Cells[5].Text="主存库";
                }
                else
                {
                    e.Item.Cells[5].Text = "暂存库";
                }

                if (e.Item.Cells[9].Text == "0")
                {
                    e.Item.Cells[9].Text = "未启用";
                }
                else
                {
                    e.Item.Cells[9].Text = "启用";
                }
            }
        }
        catch
        {
        }
    }
    protected void dgArea_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.Cells[0].Text = "操作";
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkModify = new LinkButton();
                lnkModify.Text = "修改 ";
                lnkModify.OnClientClick = "OpenEditArea('"+e.Item.Cells[1].Text+"')";

                LinkButton lnkDel = new LinkButton();
                lnkDel.Text = " 删除";
                lnkDel.CommandName = "Delete";
                lnkDel.OnClientClick = "return DeleteConfirm()";
                e.Item.Cells[0].Controls.Add(lnkModify);
                e.Item.Cells[0].Controls.Add(lnkDel);

                if (e.Item.Cells[6].Text == "0")
                {
                    e.Item.Cells[6].Text = "未启用";
                }
                else
                {
                    e.Item.Cells[6].Text = "启用";
                }
            }
        }
        catch
        {
        }
    }
    protected void dgShelf_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.Cells[0].Text = "操作";
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkModify = new LinkButton();
                lnkModify.Text = "修改 ";
                lnkModify.OnClientClick = "OpenEditShelf('"+e.Item.Cells[1].Text+"')";

                LinkButton lnkDel = new LinkButton();
                lnkDel.Text = " 删除";
                lnkDel.CommandName = "Delete";
                lnkDel.OnClientClick = "return DeleteConfirm()";
                e.Item.Cells[0].Controls.Add(lnkModify);
                e.Item.Cells[0].Controls.Add(lnkDel);

                if (e.Item.Cells[7].Text == "0")
                {
                    e.Item.Cells[7].Text = "未启用";
                }
                else
                {
                    e.Item.Cells[7].Text = "启用";
                }
            }
        }
        catch
        {
        }
    }
    protected void dgCell_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.Cells[0].Text = "操作";
                for (int i = 0; i < e.Item.Cells.Count; i++)
                {
                    e.Item.Cells[i].Attributes.Add("style", "position:relative; top:expression(this.offsetParent.scrollTop);z-index:999;");
                }
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkModify = new LinkButton();
                lnkModify.Text = "修改 ";
                lnkModify.OnClientClick = "OpenEditCell('" + e.Item.Cells[1].Text + "')";
                LinkButton lnkDel = new LinkButton();
                lnkDel.Text = " 删除";
                lnkDel.CommandName = "Delete";
                lnkDel.OnClientClick = "return DeleteConfirm()";
                e.Item.Cells[0].Controls.Add(lnkModify);
                e.Item.Cells[0].Controls.Add(lnkDel);

                if (e.Item.Cells[8].Text == "0")
                {
                    e.Item.Cells[8].Text = "未启用";
                }
                else
                {
                    e.Item.Cells[8].Text = "启用";
                }
            }
        }
        catch
        {
        }
    }

    #endregion

    #region 删除
    protected void dgHouse_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string whcode = dsHouse.Tables[0].Rows[e.Item.ItemIndex]["WH_CODE"].ToString();
            int count = objArea.QueryAreaByWHCODE(whcode).Tables[0].Rows.Count;
            if (count > 0)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, whcode + "还有下属库区，不能删除！");
                return;
            }
            else
            {
                objHouse.Delete(whcode);
                GridDataBind();
                LoadHouseTree();
            }
        }
    }


    protected void dgArea_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string whcode = dsArea.Tables[0].Rows[e.Item.ItemIndex]["WH_CODE"].ToString();
            string areaCode = dsArea.Tables[0].Rows[e.Item.ItemIndex]["AREACODE"].ToString();
            int areaid=Convert.ToInt32(dsArea.Tables[0].Rows[e.Item.ItemIndex]["AREA_ID"].ToString());
            int count = objShelf.QueryShelfByAreaCode(areaCode).Tables[0].Rows.Count;
            if (count > 0)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, areaCode + "库区还有下属货架，不能删除！");
                return;
            }
            else
            {
                objArea.Delete(areaid);
                GridDataBind();

                TreeNode nodeArea = tvWarehouse.FindNode(whcode + "/" + areaCode);
                if (nodeArea != null)
                {
                    nodeArea.Parent.ChildNodes.Remove(nodeArea);
                }
                //LoadHouseTree();
            }
        }
    }

    protected void dgShelf_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string whcode = dsShelf.Tables[0].Rows[e.Item.ItemIndex]["WH_CODE"].ToString();
            string areacode = dsShelf.Tables[0].Rows[e.Item.ItemIndex]["AREACODE"].ToString();
            string shelfCode = dsShelf.Tables[0].Rows[e.Item.ItemIndex]["SHELFCODE"].ToString();
            int shelfid = Convert.ToInt32(dsShelf.Tables[0].Rows[e.Item.ItemIndex]["SHELF_ID"].ToString());
            int count = objCell.QueryWarehouseCell("SHELFCODE='"+shelfCode+"'").Tables[0].Rows.Count;
            if (count > 0)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, shelfCode + "货架还有下属货位，不能删除！");
                return;
            }
            else
            {
                objShelf.Delete(shelfid);
                GridDataBind();

                TreeNode nodeShelf = tvWarehouse.FindNode(whcode + "/" + areacode + "/" + shelfCode);
                if (nodeShelf != null)
                {
                    nodeShelf.Parent.ChildNodes.Remove(nodeShelf);
                }
                //LoadHouseTree();
            }
        }
    }

    protected void dgCell_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                //JScript.Instance.ShowMessage(this.UpdatePanel1, dsCell.Tables[0].Rows[e.Item.ItemIndex]["QUANTITY"].ToString());

                if (dsCell.Tables[0].Rows[e.Item.ItemIndex]["QUANTITY"].ToString()!= "")
                {
                    decimal qty = Convert.ToDecimal(dsCell.Tables[0].Rows[e.Item.ItemIndex]["QUANTITY"].ToString());
                    if (qty > 0)
                    {
                        JScript.Instance.ShowMessage(this.UpdatePanel1, "该货位正在使用，不能删除！");
                        return;
                    }
                }

                string whcode = dsCell.Tables[0].Rows[e.Item.ItemIndex]["WH_CODE"].ToString();
                string areacode = dsCell.Tables[0].Rows[e.Item.ItemIndex]["AREACODE"].ToString();
                string shelfCode = dsCell.Tables[0].Rows[e.Item.ItemIndex]["SHELFCODE"].ToString();
                string cellcode = dsCell.Tables[0].Rows[e.Item.ItemIndex]["CELLCODE"].ToString();
                int cellid = Convert.ToInt32(dsCell.Tables[0].Rows[e.Item.ItemIndex]["CELL_ID"].ToString());
                objCell.Delete(cellid);
                GridDataBind();

                TreeNode nodeCell = tvWarehouse.FindNode(whcode + "/" + areacode + "/" + shelfCode + "/" + cellcode);
                if (nodeCell != null)
                {
                    nodeCell.Parent.ChildNodes.Remove(nodeCell);
                }
               // LoadHouseTree();
            }
        }
        catch (Exception E)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, E.Message);
        }
    }
    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../mainpage.aspx");
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        GridDataBind();
        if (this.hdnDepth.Value == "0")
        {
            foreach (DataRow row in dsHouse.Tables[0].Rows)
            {
                TreeNode fNode = tvWarehouse.FindNode(row["WH_CODE"].ToString());
                if (fNode == null)
                {
                    TreeNode node = new TreeNode(row["WH_NAME"].ToString(), row["WH_CODE"].ToString());
                    node.ImageUrl = "../../images/leftmenu/in_warehouse.gif";
                    tvWarehouse.Nodes.Add(node);
                }
            }
        }
        if (this.hdnDepth.Value == "1")
        {
            DataSet dsTemp = objArea.QueryAreaByWHCODE(this.hdnWarehouseCode.Value);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsTemp.Tables[0].Rows)
                {
                    TreeNode fNode = tvWarehouse.FindNode(r["WH_CODE"].ToString()+"/"+r["AREACODE"].ToString());
                    if (fNode == null)
                    {
                        TreeNode nodeHouse = tvWarehouse.FindNode(r["WH_CODE"].ToString());

                        if (nodeHouse != null)
                        {
                            nodeHouse.ChildNodes.Add(new TreeNode("库区：" + r["AREANAME"].ToString(), r["AREACODE"].ToString()));
                        }
                    }
                }
            }
        }
        if (this.hdnDepth.Value == "2")
        {
            DataSet dsTemp = objShelf.QueryShelfByAreaCode(this.hdnAreaCode.Value);
            foreach (DataRow r2 in dsTemp.Tables[0].Rows)
            {
                TreeNode fNode = tvWarehouse.FindNode(r2["WH_CODE"].ToString()+"/"+r2["AREACODE"].ToString()+"/"+r2["SHELFCODE"]);
                if (fNode == null)
                {
                    TreeNode nodeArea = tvWarehouse.FindNode(r2["WH_CODE"].ToString() + "/" + r2["AREACODE"].ToString());
                    if (nodeArea != null)
                    {
                        nodeArea.ChildNodes.Add(new TreeNode("货架：" + r2["SHELFNAME"].ToString(), r2["SHELFCODE"].ToString()));
                        //nodeArea.ExpandAll();
                    }
                }
            }


        }
        if (this.hdnDepth.Value == "3")
        {
            DataSet dsTemp = objCell.QueryAllCell();
            foreach (DataRow r3 in dsTemp.Tables[0].Rows)
            {
                TreeNode fNode= tvWarehouse.FindNode(r3["WH_CODE"].ToString() + "/" + r3["AREACODE"].ToString() + "/" + r3["SHELFCODE"].ToString()+"/"+r3["CELLCODE"]);
                if (fNode == null)
                {
                    TreeNode nodeShelf = tvWarehouse.FindNode(r3["WH_CODE"].ToString() + "/" + r3["AREACODE"].ToString() + "/" + r3["SHELFCODE"].ToString());
                    if (nodeShelf != null)
                    {
                        nodeShelf.ChildNodes.Add(new TreeNode("货位：" + r3["CELLNAME"].ToString(), r3["CELLCODE"].ToString()));
                    }
                }
            }
        }
    }

    #region 货架 货位 分页
    //货架
    protected void pager1_PageChanging(object src, PageChangingEventArgs e)
    {

    }

    //货位
    protected void pager2_PageChanging(object src, PageChangingEventArgs e)
    {

    }
    #endregion
}
