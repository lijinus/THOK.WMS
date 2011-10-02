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

public partial class Code_BasicInfo_UnitPage : BasePage
{
    int pageIndex = 1;
    int pageSize = 15;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "1=1";
    DataSet dsUnit;
    string PrimaryKey = "UNITCODE";
    string OrderByFields = "UNITCODE";
    THOK.WMS.BLL.Unit objUnit = new THOK.WMS.BLL.Unit();

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
                totalCount = objUnit.GetRowCount(filter);
                pager.RecordCount = totalCount;
                GridDataBind();
                Comparison obj = new Comparison();
                DataSet dsTemp = obj.GetItems("UNIT_ISDEFAULT");
                this.ddlDefault.Items.Clear();
                this.ddlDefault.DataSource = dsTemp.Tables[0].DefaultView;
                this.ddlDefault.DataTextField = "TEXT";
                this.ddlDefault.DataValueField = "VALUE";
                this.ddlDefault.DataBind();
                dsTemp = obj.GetItems("UNIT_ISACTIVE");
                this.ddlActive.Items.Clear();
                dsTemp.Tables[0].DefaultView.Sort = "VALUE DESC";
                this.ddlActive.DataSource = dsTemp.Tables[0].DefaultView;
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

                totalCount = objUnit.GetRowCount(filter);
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
        dsUnit = objUnit.QueryUnit(pageIndex, pageSize, filter, OrderByFields);
        if (dsUnit.Tables[0].Rows.Count == 0)
        {
            dsUnit.Tables[0].Rows.Add(dsUnit.Tables[0].NewRow());
            gvMain.DataSource = dsUnit;
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
            this.gvMain.DataSource = dsUnit.Tables[0];
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
            DataSet ds = obj.GetItems("UNIT_ISDEFAULT");
            DataRow[] isdefaultRows = ds.Tables[0].Select("VALUE='" + e.Row.Cells[4].Text + "'");
            if (isdefaultRows.Length == 1)
            {
                e.Row.Cells[4].Text = isdefaultRows[0]["TEXT"].ToString();
            }
            DataSet isactiveEs = obj.GetItems("UNIT_ISACTIVE");
            DataRow[] isactiveRows = isactiveEs.Tables[0].Select("VALUE='" + e.Row.Cells[5].Text + "'");
            if (isactiveRows.Length == 1)
            {
                e.Row.Cells[5].Text = isactiveRows[0]["TEXT"].ToString();
            }
            //e.Row.Cells[4].Text = ddlDefault.Items.FindByValue(e.Row.Cells[4].Text).Text;
            //e.Row.Cells[5].Text = this.ddlActive.Items.FindByValue(e.Row.Cells[5].Text).Text;

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
        this.txtID.Text = dsUnit.Tables[0].Rows[e.NewEditIndex]["ID"].ToString();
        this.txtUNITCODE.Text = dsUnit.Tables[0].Rows[e.NewEditIndex]["UNITCODE"].ToString();
        this.txtUNITNAME.Text = dsUnit.Tables[0].Rows[e.NewEditIndex]["UNITNAME"].ToString();
        this.ddlDefault.SelectedValue = dsUnit.Tables[0].Rows[e.NewEditIndex]["ISDEFAULT"].ToString();
        this.ddlActive.SelectedValue = dsUnit.Tables[0].Rows[e.NewEditIndex]["ISACTIVE"].ToString();
        this.txtSTANDARDRATE.Text = dsUnit.Tables[0].Rows[e.NewEditIndex]["STANDARDRATE"].ToString();
        this.txtMEMO.Text = dsUnit.Tables[0].Rows[e.NewEditIndex]["MEMO"].ToString();
        this.txtUNITCODE.Attributes.Add("onblur", "");
        this.txtUNITCODE.ReadOnly = true;
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

            totalCount = objUnit.GetRowCount(filter);
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
        this.txtUNITCODE.Text = objUnit.GetNewUnitCode();
        this.txtUNITCODE.Attributes.Add("onblur", string.Format("return UniqueValidate('WMS_UNIT','UNITCODE',this.value,'1=1')"));
        this.txtUNITCODE.ReadOnly = false;
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
                        dsUnit.Tables[0].Rows[i].Delete();
                    }
                }
            }
            objUnit.Delete(dsUnit);
            totalCount = objUnit.GetRowCount(filter);
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
                objUnit.UNITCODE = this.txtUNITCODE.Text;
                objUnit.UNITNAME = this.txtUNITNAME.Text;
                objUnit.ISDEFAULT = this.ddlDefault.SelectedValue;
                objUnit.ISACTIVE = this.ddlActive.SelectedValue;
                objUnit.STANDARDRATE = Convert.ToDouble(this.txtSTANDARDRATE.Text);
                objUnit.MEMO = this.txtMEMO.Text;
                objUnit.Insert();

                totalCount = objUnit.GetRowCount(filter);
                pager.RecordCount = totalCount;
                GridDataBind();
                SwitchView(0);
                JScript.Instance.ShowMessage(this.UpdatePanel1, "单位添加成功！");
            }
            else//修改
            {
                objUnit.ID = Convert.ToInt32(this.txtID.Text);
                objUnit.UNITCODE = this.txtUNITCODE.Text;
                objUnit.UNITNAME = this.txtUNITNAME.Text;
                objUnit.ISDEFAULT = this.ddlDefault.SelectedValue;
                objUnit.ISACTIVE = this.ddlActive.SelectedValue;
                objUnit.STANDARDRATE = Convert.ToDouble(this.txtSTANDARDRATE.Text);
                objUnit.MEMO = this.txtMEMO.Text;
                objUnit.Update();

                GridDataBind();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "单位修改成功！");
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
        this.txtID.Text = "";
        this.txtUNITCODE.Text = "";
        this.txtUNITNAME.Text = "";
        this.ddlDefault.SelectedIndex = 0;
        this.ddlActive.SelectedIndex = 0;
        this.txtSTANDARDRATE.Text = "";
        this.txtMEMO.Text = "";
    }
    #endregion

    #region 退出
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../MainPage.aspx");
    }
    #endregion

    #region 从营销系统下载计量单位
    
    protected void btnDown_Click(object sender, EventArgs e)
    {
        try
        {
            DownUnitBll objbll = new DownUnitBll();
            bool tag = objbll.DownUnitInfo();
            if (!tag)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "没有更多单位数据下载！");
            }
            else
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "下载完成！");
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    #endregion
}
