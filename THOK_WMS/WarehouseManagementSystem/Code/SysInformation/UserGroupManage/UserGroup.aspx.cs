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
using System.Drawing;
using THOK.System.BLL;

public partial class Code_SysInformation_UserGroupManage_UserGroup : BasePage
{
    int pageIndex = 1;
    int pageSize = 15;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "1=1";
    DataSet dsGroup;
    string PrimaryKey = "GroupID";
    string OrderByFields = "GroupName";
    SysGroup objGroup= new SysGroup();

    #region 窗体加载
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["sys_PageCount"] != null)
            {
                pageSize = Convert.ToInt32(Session["sys_PageCount"].ToString());
                pager.PageSize = pageSize;
            }
            if (Session["pager_ShowPageIndex"] != null)
            {
                pager.ShowPageIndex = Convert.ToBoolean(Session["pager_ShowPageIndex"].ToString());
            }

            if (!IsPostBack)
            {
                totalCount = objGroup.GetRowCount(filter);
                pager.RecordCount = totalCount;
                GridDataBind();
            }
            else
            {
                pageCount = Convert.ToInt32(ViewState["pageCount"]);
                pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
                totalCount = Convert.ToInt32(ViewState["totalCount"]);
                filter = ViewState["filter"].ToString();
                OrderByFields = ViewState["OrderByFields"].ToString();

                totalCount = objGroup.GetRowCount(filter);
                GridDataBind();
            }

        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    #endregion

    #region 数据源绑定
    void GridDataBind()
    {
        dsGroup =objGroup.GetGroupList(pageIndex, pageSize, filter, OrderByFields);
        if (dsGroup.Tables[0].Rows.Count == 0)
        {
            dsGroup.Tables[0].Rows.Add(dsGroup.Tables[0].NewRow());
            gvMain.DataSource = dsGroup;
            gvMain.DataBind();
            int columnCount = gvMain.Rows[0].Cells.Count;
            gvMain.Rows[0].Cells.Clear();
            gvMain.Rows[0].Cells.Add(new TableCell());
            gvMain.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvMain.Rows[0].Cells[0].Text = "没有符合以上条件的数据,请重新查询 ";
            gvMain.Rows[0].Visible = true;

        }
        else
        {
            this.gvMain.DataSource = dsGroup.Tables[0];
            this.gvMain.DataBind();
        }

        ViewState["pageIndex"] = pageIndex;
        ViewState["totalCount"] = totalCount;
        ViewState["pageCount"] = pageCount;
        ViewState["filter"] = filter;
        ViewState["OrderByFields"] = OrderByFields;
    }
    #endregion

    #region 显示切换
    private void SwitchView(int index)
    {
        if (index == 0)
        {
            this.pnlList.Visible = true;
            this.pnlEdit.Visible = false;
        }
        else
        {
            this.pnlList.Visible = false;
            this.pnlEdit.Visible = true;
        }
    }
    #endregion

    #region GridView绑定
    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = new CheckBox();
            chk.Attributes.Add("style", " font-weight:bold; text-align:center;word-break:keep-all; white-space:nowrap");
            chk.ID = "checkAll";
            chk.Attributes.Add("onclick", "checkboxChange(this,'gvMain',0);");
            chk.Text = "操作";
            e.Row.Cells[0].Controls.Add(chk);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex % 2 == 0)
            {
                e.Row.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
            }
            else
            {
                e.Row.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
            }
            e.Row.Cells[0].Attributes.Add("style", "word-break:keep-all; white-space:nowrap");

            CheckBox chk = new CheckBox();
            LinkButton lkbtn = new LinkButton();
            lkbtn.CommandName = "Edit";
            lkbtn.ID = e.Row.ID;
            lkbtn.Text = " 编辑 ";
            if (this.hdnXGQX.Value == "0")
            {
                lkbtn.Enabled = false;
            }
            e.Row.Cells[0].Controls.Add(chk);
            e.Row.Cells[0].Controls.Add(lkbtn);
        }
    }
    #endregion

    #region 数据编辑
    protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hdnOpFlag.Value = "1";
        ViewState["OpFlag"] = "1";
        SwitchView(1);
        this.txtGroupID.Text = dsGroup.Tables[0].Rows[e.NewEditIndex]["GroupID"].ToString();
        this.txtGroupName.Text = dsGroup.Tables[0].Rows[e.NewEditIndex]["GroupName"].ToString();
        this.txtMemo.Text = dsGroup.Tables[0].Rows[e.NewEditIndex]["Memo"].ToString();
    }
    #endregion

    #region 按字段查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            filter = string.Format("{0} like '{1}%'", this.ddl_Field.SelectedValue, this.txtKeyWords.Text.Trim().Replace("'", ""));
            ViewState["filter"] = filter;
            if (rbASC.Checked)
            {
                OrderByFields = this.ddl_Field.SelectedValue + " asc ";
            }
            else
            {
                OrderByFields = this.ddl_Field.SelectedValue + " desc ";
            }

            totalCount = objGroup.GetRowCount(filter);
            pageIndex = 1;
            pager.CurrentPageIndex = 1;
            pager.RecordCount = totalCount;
            GridDataBind();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    #endregion

    #region 新增用户
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        ClearData();
        this.hdnOpFlag.Value = "0";
        ViewState["OpFlag"] = "0";
        SwitchView(1);
    }
    #endregion

    #region 删除用户组
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvMain.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvMain.Rows[i].Cells[0].Controls[0];
                if (chk.Enabled && chk.Checked)
                {
                    int membercount=objGroup.GetGroupMemberCount(Convert.ToInt32(dsGroup.Tables[0].Rows[i]["GroupID"]));
                    if (membercount > 0)
                    {
                        JScript.Instance.ShowMessage(this.UpdatePanel1, gvMain.Rows[i].Cells[1].Text+"用户组还有用户存在，请调整后再删除！");
                        return;
                    }
                    dsGroup.Tables[0].Rows[i].Delete();
                }
            }
            objGroup.Delete(dsGroup);
            totalCount = objGroup.GetRowCount(filter);
            pager.RecordCount = totalCount;
            if (pageIndex > pager.PageCount)
            {
                pageIndex = pager.PageCount;
            }
            GridDataBind();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    #endregion

    # region 分页控件 页码changing事件
    protected void pager_PageChanging(object src, PageChangingEventArgs e)
    {
        pager.CurrentPageIndex = e.NewPageIndex;
        pager.RecordCount = totalCount;
        pageIndex = pager.CurrentPageIndex;
        ViewState["pageIndex"] = pageIndex;
        GridDataBind();
    }
    #endregion

    #region 数据保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["OpFlag"].ToString() == "0")//新增
            {
                DataSet dsTemp = objGroup.GetGroupList(1, 10, string.Format("GroupName='{0}'", this.txtGroupName.Text.Trim()), OrderByFields);
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    JScript.Instance.ShowMessage(this.UpdatePanel1, "该用户组名称已经存在！");
                    return;
                }
                DataRow newRow = dsGroup.Tables[0].NewRow();
                newRow["GroupName"] = this.txtGroupName.Text.Trim();
                newRow["Memo"] = this.txtMemo.Text.Trim(); 
                dsGroup.Tables[0].Rows.Add(newRow);
                objGroup.Insert(dsGroup);
                totalCount = objGroup.GetRowCount(filter);
                pager.RecordCount = totalCount;
                GridDataBind();
                SwitchView(0);
                JScript.Instance.ShowMessage(this.UpdatePanel1, "数据添加成功！");
            }
            else//修改
            {
                foreach (DataRow dr in dsGroup.Tables[0].Rows)
                {
                    if (dr["GroupID"].ToString() == this.txtGroupID.Text.Trim())
                    {
                        DataSet dsTemp = objGroup.GetGroupList(1, 10, string.Format("GroupID<>{0} and GroupName='{1}'", this.txtGroupID.Text, this.txtGroupName.Text.Trim()), OrderByFields);
                        if (dsTemp.Tables[0].Rows.Count > 0)
                        {
                            JScript.Instance.ShowMessage(this.UpdatePanel1, "该用户组名称已经存在！");
                            return;
                        }
                        dr["GroupName"] = this.txtGroupName.Text.Trim();
                        dr["Memo"] = this.txtMemo.Text.Trim();
                        break;
                    }
                }
                objGroup.Update(dsGroup);
                GridDataBind();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "数据修改成功！");
                SwitchView(0);
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    #endregion

    #region 取消
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearData();
        SwitchView(0);
    }

    protected void ClearData()
    {
        this.txtGroupID.Text = "";
        this.txtGroupName.Text = "";
        this.txtMemo.Text = "";
    }
    #endregion

    #region 退出
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../../MainPage.aspx");
    }
    #endregion
}
