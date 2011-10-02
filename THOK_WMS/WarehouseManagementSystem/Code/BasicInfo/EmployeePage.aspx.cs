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

public partial class Code_BasicInfo_Employee : BasePage
{
    int pageIndex = 1;
    int pageSize = 15;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "1=1";
    DataSet dsEmp;
    string PrimaryKey = "EMPLOYEECODE";
    string OrderByFields = "EMPLOYEECODE";
    Employee objEmp = new Employee();

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
                totalCount = objEmp.GetRowCount(filter);
                pager.RecordCount = totalCount;
                GridDataBind();

                Comparison obj = new Comparison();
                DataSet dsTemp = obj.GetItems("EMP_SEX");
                this.ddlSex.Items.Clear();
                this.ddlSex.DataSource = dsTemp.Tables[0].DefaultView;
                this.ddlSex.DataTextField = "TEXT";
                this.ddlSex.DataValueField = "VALUE";
                this.ddlSex.DataBind();
                dsTemp = obj.GetItems("EMP_STATUS");
                this.ddlStatus.Items.Clear();
                this.ddlStatus.DataSource = dsTemp.Tables[0].DefaultView;
                this.ddlStatus.DataTextField = "TEXT";
                this.ddlStatus.DataValueField = "VALUE";
                this.ddlStatus.DataBind();
            }
            else
            {
                pageCount = Convert.ToInt32(ViewState["pageCount"]);
                pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
                totalCount = Convert.ToInt32(ViewState["totalCount"]);
                filter = ViewState["filter"].ToString();
                OrderByFields = ViewState["OrderByFields"].ToString();

                totalCount = objEmp.GetRowCount(filter);
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
        dsEmp = objEmp.QueryEmployee(pageIndex, pageSize, filter, OrderByFields);
        if (dsEmp.Tables[0].Rows.Count == 0)
        {
            dsEmp.Tables[0].Rows.Add(dsEmp.Tables[0].NewRow());
            gvMain.DataSource = dsEmp;
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
            this.gvMain.DataSource = dsEmp.Tables[0];
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
            DataSet ds = obj.GetItems("EMP_STATUS");
            DataRow[] rows = ds.Tables[0].Select("VALUE='" + e.Row.Cells[7].Text + "'");
            if (rows.Length == 1)
            {
                e.Row.Cells[7].Text = rows[0]["TEXT"].ToString();
            }
            DataSet sex = obj.GetItems("EMP_SEX");
            DataRow[] sexrows = sex.Tables[0].Select("VALUE='" + e.Row.Cells[5].Text + "'");
            if (sexrows.Length == 1)
            {
                e.Row.Cells[5].Text = sexrows[0]["TEXT"].ToString();
            }
            //e.Row.Cells[5].Text = ddlSex.Items.FindByValue(e.Row.Cells[5].Text).Text;

            //e.Row.Cells[6].Text = ddlStatus.Items.FindByValue(e.Row.Cells[6].Text).Text;

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
        SwitchView(1);
        this.txtEmployeeCode.Text = dsEmp.Tables[0].Rows[e.NewEditIndex]["EMPLOYEECODE"].ToString();
        this.txtEmployeeName.Text = dsEmp.Tables[0].Rows[e.NewEditIndex]["EMPLOYEENAME"].ToString();
        this.txtDeptName.Text = dsEmp.Tables[0].Rows[e.NewEditIndex]["DEPTNAME"].ToString();
        this.txtDeptCode.Text = dsEmp.Tables[0].Rows[e.NewEditIndex]["DEPTCODE"].ToString();
        this.txtPosition.Text = dsEmp.Tables[0].Rows[e.NewEditIndex]["POSITION"].ToString();
        this.ddlSex.SelectedValue = dsEmp.Tables[0].Rows[e.NewEditIndex]["SEX"].ToString();
        this.ddlStatus.SelectedValue = dsEmp.Tables[0].Rows[e.NewEditIndex]["STATUS"].ToString();
        this.txtTel.Text = dsEmp.Tables[0].Rows[e.NewEditIndex]["TEL"].ToString();
        this.txtMemo.Text = dsEmp.Tables[0].Rows[e.NewEditIndex]["MEMO"].ToString();
        this.txtEmployeeCode.ReadOnly = true;
        this.txtEmployeeCode.Attributes.Add("onblur", "return true");
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

            totalCount = objEmp.GetRowCount(filter);
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
        this.txtEmployeeCode.Text = objEmp.GetNewEmployeeCode();
        this.txtEmployeeCode.ReadOnly = false;
        this.txtEmployeeCode.Attributes.Add("onblur", string.Format("return UniqueValidate('BI_EMPLOYEE','EMPLOYEECODE',this.value,'1=1')"));
    }
    #endregion

    #region 删除
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string empId = "";
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
                        empId += "'" + dsEmp.Tables[0].Rows[i]["EMPLOYEECODE"].ToString() + "',";
                        dsEmp.Tables[0].Rows[i].Delete();
                    }
                }
            }
            objEmp.Delete(dsEmp);
            if (empId != "")
            {
                new RemoteInterfaceDal().DeleteDWV_IORG_PERSON(empId.Substring(0,empId.Length-1));
            }
            
            totalCount = objEmp.GetRowCount(filter);
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
                objEmp.EMPLOYEECODE = this.txtEmployeeCode.Text;
                objEmp.EMPLOYEENAME = this.txtEmployeeName.Text.Trim().Replace("\'", "\''");
                objEmp.DEPTCODE = this.txtDeptCode.Text;
                objEmp.POSITION = this.txtPosition.Text.Trim().Replace("\'", "\''");
                objEmp.SEX = this.ddlSex.SelectedValue;
                objEmp.STATUS = this.ddlStatus.SelectedValue;
                objEmp.Tel = this.txtTel.Text.Trim().Replace("\'", "\''");
                objEmp.MEMO = this.txtMemo.Text.Trim().Replace("\'", "\''");
                objEmp.Insert();
                new RemoteInterfaceDal().SaveDWV_IORG_PERSON(txtEmployeeCode.Text);
                totalCount = objEmp.GetRowCount(filter);
                pager.RecordCount = totalCount;
                GridDataBind();
                SwitchView(0);
                JScript.Instance.ShowMessage(this.UpdatePanel1, "员工添加成功！");
            }
            else//修改
            {
                objEmp.EMPLOYEECODE = this.txtEmployeeCode.Text;
                objEmp.EMPLOYEENAME = this.txtEmployeeName.Text.Trim().Replace("\'", "\''");
                objEmp.DEPTCODE = this.txtDeptCode.Text;
                objEmp.POSITION = this.txtPosition.Text.Trim().Replace("\'", "\''");
                objEmp.SEX = this.ddlSex.SelectedValue;
                objEmp.STATUS = this.ddlStatus.SelectedValue;
                objEmp.Tel = this.txtTel.Text.Trim().Replace("\'", "\''");
                objEmp.MEMO = this.txtMemo.Text.Trim().Replace("\'", "\''");
                objEmp.Update();
                new RemoteInterfaceDal().SaveDWV_IORG_PERSON(txtEmployeeCode.Text);
                GridDataBind();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "员工修改成功！");
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
        this.txtEmployeeCode.Text = "";
        this.txtEmployeeName.Text = "";
        this.txtDeptName.Text = "";
        this.txtDeptCode.Text = "";
        this.txtPosition.Text = "";
        this.ddlSex.SelectedIndex =0 ;
        this.ddlStatus.SelectedIndex = 0;
        this.txtTel.Text = "";
        this.txtMemo.Text = "";
    }
    #endregion

    #region 退出
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../MainPage.aspx");
    }
    #endregion
}
