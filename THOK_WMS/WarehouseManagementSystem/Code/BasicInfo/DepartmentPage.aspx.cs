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
using THOK.WMS.Download.Bll;

public partial class Code_BasicInfo_Department : BasePage
{
    int pageIndex = 1;
    int pageSize = 15;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "1=1";
    DataSet dsDept;
    string PrimaryKey = "DEPTCODE";
    string OrderByFields = "DEPTCODE";
    Department objDept = new Department();

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
                totalCount = objDept.GetRowCount(filter);
                pager.RecordCount = totalCount;
                GridDataBind();
                Comparison obj=new Comparison();
                DataSet dsActive = obj.GetItems("DEPT_ISACTIVE");
                dsActive.Tables[0].DefaultView.Sort = "VALUE DESC";
                this.ddlActive.Items.Clear();
                this.ddlActive.DataSource = dsActive.Tables[0].DefaultView;
                this.ddlActive.DataTextField = "TEXT";
                this.ddlActive.DataValueField = "VALUE";
                this.ddlActive.DataBind();
            }
            else
            {
                pageCount = Convert.ToInt32(ViewState["pageCount"]);
                pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
                totalCount = Convert.ToInt32(ViewState["totalCount"]);
                filter = ViewState["filter"].ToString();
                OrderByFields = ViewState["OrderByFields"].ToString();

                totalCount = objDept.GetRowCount(filter);
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
        dsDept = objDept.QueryDepartment(pageIndex, pageSize, filter, OrderByFields);
        if (dsDept.Tables[0].Rows.Count == 0)
        {
            dsDept.Tables[0].Rows.Add(dsDept.Tables[0].NewRow());
            gvMain.DataSource = dsDept;
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
            this.gvMain.DataSource = dsDept.Tables[0];
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
                e.Row.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
            }
            else
            {
                e.Row.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
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
            Comparison obj = new Comparison();
            DataSet ds = obj.GetItems("DEPT_ISACTIVE");
            DataRow[] rows = ds.Tables[0].Select("VALUE='" + e.Row.Cells[4].Text + "'");
            if (rows.Length == 1)
            {
                e.Row.Cells[4].Text = rows[0]["TEXT"].ToString();
            }

           // e.Row.Cells[4].Text = ddlActive.Items.FindByValue(e.Row.Cells[4].Text).Text;

            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f5f5f5',this.style.fontWeight=''; this.style.cursor='hand';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
        }
    }
    #endregion

    #region 数据编辑
    protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hdnOpFlag.Value = "1";
        ViewState["OpFlag"] = "1";
        this.txtDeptCode.ReadOnly = true;
        this.txtDeptCode.Attributes.Add("onblur", "return true");
        SwitchView(1);
        this.txtDeptCode.Text = dsDept.Tables[0].Rows[e.NewEditIndex]["DEPTCODE"].ToString();
        this.txtDeptName.Text = dsDept.Tables[0].Rows[e.NewEditIndex]["DEPTNAME"].ToString();
        this.txtLeader.Text = dsDept.Tables[0].Rows[e.NewEditIndex]["DEPTLEADER"].ToString();
        this.txtEmployeeName.Text = dsDept.Tables[0].Rows[e.NewEditIndex]["EMPLOYEENAME"].ToString();
        this.txtWareCode.Text = dsDept.Tables[0].Rows[e.NewEditIndex]["WARECODE"].ToString();
        this.txtWH_Name.Text = dsDept.Tables[0].Rows[e.NewEditIndex]["WH_NAME"].ToString();
        this.txtMemo.Text = dsDept.Tables[0].Rows[e.NewEditIndex]["MEMO"].ToString();
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

            totalCount = objDept.GetRowCount(filter);
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

    #region 新增
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        ClearData();
        this.hdnOpFlag.Value = "0";
        ViewState["OpFlag"] = "0";
        SwitchView(1);
        this.txtDeptCode.Text = objDept.GetNewDeptCode();
        this.txtDeptCode.ReadOnly = false;
        this.txtDeptCode.Attributes.Add("onblur", string.Format("return UniqueValidate('BI_DEPARTMENT','DEPTCODE',this.value,'1=1')"));
    }
    #endregion

    #region 删除
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvMain.Rows.Count; i++)
            {
                if (gvMain.Rows[0].Cells[0].Text == "没有符合以上条件的数据,请重新查询 ")
                {
                    break;
                }
                else
                {
                    CheckBox chk = (CheckBox)gvMain.Rows[i].Cells[0].Controls[0];
                    if (chk.Enabled && chk.Checked)
                    {
                        dsDept.Tables[0].Rows[i].Delete();
                    }
                }
            }
            objDept.Delete(dsDept);
            totalCount = objDept.GetRowCount(filter);
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
                objDept.DEPTCODE = this.txtDeptCode.Text;
                objDept.DEPTNAME = this.txtDeptName.Text.Trim().Replace("\'", "\''");
                objDept.DEPTLEADER = this.txtLeader.Text;
                objDept.ISACTIVE = this.ddlActive.SelectedValue;
                objDept.WARECODE = this.txtWareCode.Text;
                objDept.MEMO = this.txtMemo.Text.Trim().Replace("\'", "\''");
                objDept.Insert();

                totalCount = objDept.GetRowCount(filter);
                pager.RecordCount = totalCount;
                GridDataBind();
                SwitchView(0);
                JScript.Instance.ShowMessage(this.UpdatePanel1, "部门添加成功！");
            }
            else//修改
            {
                objDept.DEPTCODE = this.txtDeptCode.Text;
                objDept.DEPTNAME = this.txtDeptName.Text.Trim().Replace("\'", "\''");
                objDept.DEPTLEADER = this.txtLeader.Text;
                objDept.ISACTIVE = this.ddlActive.SelectedValue;
                objDept.WARECODE = this.txtWareCode.Text;
                objDept.MEMO = this.txtMemo.Text.Trim().Replace("\'", "\''");
                objDept.Update();

                GridDataBind();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "部门修改成功！");
                SwitchView(0);
            }
        }
        catch (Exception exp)
        {
            if (exp.Message.Contains("违反了 PRIMARY KEY 约束"))
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "部门编码重复！");
            }
            else
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
            }
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
        this.txtDeptCode.Text = "";
        this.txtDeptName.Text = "";
        this.txtLeader.Text = "";
        this.txtEmployeeName.Text = "";
        this.txtWareCode.Text = "";
        this.txtWH_Name.Text = "";
        this.txtMemo.Text = "";
        this.ddlActive.SelectedIndex = 0;
    }
    #endregion

    #region 退出
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../MainPage.aspx");
    }
    #endregion

    #region 下载营销系统数据

    protected void btnDownDept_Click(object sender, EventArgs e)
    {
        try
        {
            DownDeptBll deptBll = new DownDeptBll();
            bool flag = deptBll.DownDeptInfo();
            if (flag)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "下载成功！");
            }
            else
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "没有更多的部门信息可下载！");
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }

        GridDataBind();
    }
    #endregion
}
