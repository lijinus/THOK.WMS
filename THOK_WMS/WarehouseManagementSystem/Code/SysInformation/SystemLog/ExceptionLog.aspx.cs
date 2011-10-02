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
using THOK.System.BLL;
using System.Drawing;

public partial class Code_SysInformation_SystemLog_ExceptionLog : BasePage
{
    int pageIndex = 1;
    int pageSize = 15;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "1=1";
    DataSet dsLog;
    string PrimaryKey = "ExceptionalLogID";
    string OrderByFields = "ExceptionalLogID desc";
    ExceptionLog objLog = new ExceptionLog();

    #region 窗体加裁
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
                if (this.btnDelete.Enabled)
                {
                    this.btnDeleteAll.Enabled = true;
                }
                totalCount = objLog.GetRowCount(filter);
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
                totalCount = objLog.GetRowCount(filter);
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
        dsLog = objLog.GetOperatorLogList(pageIndex, pageSize, filter, OrderByFields);
        if (dsLog.Tables[0].Rows.Count == 0)
        {
            dsLog.Tables[0].Rows.Add(dsLog.Tables[0].NewRow());
            gvMain.DataSource = dsLog;
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
            this.gvMain.DataSource = dsLog.Tables[0];
            this.gvMain.DataBind();
        }

        ViewState["pageIndex"] = pageIndex;
        ViewState["totalCount"] = totalCount;
        ViewState["pageCount"] = pageCount;
        ViewState["filter"] = filter;
        ViewState["OrderByFields"] = OrderByFields;
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
            chk.Text = "";
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
            e.Row.Cells[0].Controls.Add(chk);
        }
    }
    #endregion

    #region 删除日志
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvMain.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvMain.Rows[i].Cells[0].Controls[0];
                if (chk.Enabled && chk.Checked)
                {
                    dsLog.Tables[0].Rows[i].Delete();
                }
            }
            objLog.Delete(dsLog);
            totalCount = objLog.GetRowCount(filter);
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

    #region 清空日志
    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        objLog.Clear();
        pageIndex = 1;
        pager.RecordCount = 0;
        GridDataBind();
    }
    #endregion

    #region 查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string start = "1900-01-01";
        string end = System.DateTime.Now.AddDays(1).ToString();
        try
        {
            if (this.txtDateStart.Text.Trim().Length > 0)
            {
                start = Convert.ToDateTime(this.txtDateStart.Text.Trim()).ToString();
            }
            if (this.txtDateEnd.Text.Trim().Length > 0)
            {
                end = Convert.ToDateTime(this.txtDateEnd.Text.Trim()).AddDays(1).ToString();
            }
        }
        catch
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, "输入时间格式不正确！");
            return;
        }

        filter = string.Format("{0} like '{1}%' and (CatchTime>='{2}' and CatchTime<'{3}')", this.ddl_Field.SelectedValue, this.txtKeyWords.Text.Trim().Replace("'", ""), start, end);
        ViewState["filter"] = filter;
        if (rbASC.Checked)
        {
            OrderByFields = this.ddl_Field.SelectedValue + " asc ";
        }
        else
        {
            OrderByFields = this.ddl_Field.SelectedValue + " desc ";
        }

        totalCount = objLog.GetRowCount(filter);
        pageIndex = 1;
        pager.CurrentPageIndex = 1;
        pager.RecordCount = totalCount;
        GridDataBind();
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

    #region 退出
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../../MainPage.aspx");
    }
    #endregion
}
