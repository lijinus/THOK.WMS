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
using System.Threading;
using THOK.WMS.Upload.Bll;
using THOK.WMS.Upload;

public partial class Code_StorageManagement_DailyBalancePage :BasePage
{
    string filter;
    int pageIndex = 1;
    int pageSize = 15;

    string filter2="1=1";
    int pageIndex2 = 1;
    int pageSize2 = 15;
    UploadBll updateBll = new UploadBll();
    Balance objBalance = new Balance();
    DataSet dsList;
    DataSet dsBalance;
    WarehouseCell warecell = new WarehouseCell();
   
    private static Thread thread = null;
    public event ScheduleEventHandler OnSchedule = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        warecell.UpdateCellEx();
        warecell.UpdateCell();
        this.OnSchedule += new ScheduleEventHandler(schedule_OnSchedule);
        if (!IsPostBack)
        {
            Warehouse objHouse = new Warehouse();
            DataSet dsTemp = objHouse.QueryAllWarehouse();
            this.ddlWarehouse.DataSource = dsTemp.Tables[0].DefaultView;
            this.ddlWarehouse.DataTextField = "WH_NAME";
            this.ddlWarehouse.DataValueField = "WH_CODE";
            this.ddlWarehouse.DataBind();
            this.txtDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            //filter = string.Format(" WH_CODE='{0}' and  SettleDate='{1}'", this.ddlWarehouse.SelectedValue,this.txtDate.Text);
            filter = string.Format(" WH_CODE='{0}'", this.ddlWarehouse.SelectedValue);

            ViewState["filter2"] = filter2;
            ViewState["pageIndex2"] = pageIndex2;
            pager.PageSize = pageSize;
            pager2.PageSize = pageSize2;
        }
        else
        {
            filter = ViewState["filter"].ToString();
            pageIndex = Convert.ToInt32(ViewState["pageIndex"]);

            filter2 = ViewState["filter2"].ToString();
            pageIndex2 = Convert.ToInt32(ViewState["pageIndex2"]);
        }
        GridDataBind();
        
    }

    /// <summary>
    /// 数据下载和数据优化过程状态返回事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void schedule_OnSchedule(object sender, ScheduleEventArgs e)
    {
        Session["OptimizeStatus"] = e.ToString();
        System.Diagnostics.Debug.WriteLine(e.ToString());
    }

    private void GridDataBind()
    {
        pager.RecordCount = objBalance.GetBalanceListRowCount(filter);
        dsList = objBalance.QueryBalanceList(pageIndex, pageSize, filter);
        if (dsList.Tables[0].Rows.Count == 0)
        {
            dsList.Tables[0].Rows.Add(dsList.Tables[0].NewRow());
            gvList.DataSource = dsList;
            gvList.DataBind();
            int columnCount = gvList.Rows[0].Cells.Count;
            gvList.Rows[0].Cells.Clear();
            gvList.Rows[0].Cells.Add(new TableCell());
            gvList.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvList.Rows[0].Cells[0].Text = "还未进行日结";
            gvList.Rows[0].Visible = true;

        }
        else
        {
            this.gvList.DataSource = dsList.Tables[0];
            this.gvList.DataBind();
        }
        ViewState["pageIndex"] = pageIndex;
        ViewState["filter"] = filter;
    }

    # region 分页控件 页码changing事件
    protected void pager_PageChanging(object src, PageChangingEventArgs e)
    {
        pager.CurrentPageIndex = e.NewPageIndex;
        
        pageIndex = pager.CurrentPageIndex;
        ViewState["pageIndex"] = pageIndex;
        GridDataBind();
    }

    protected void pager2_PageChanging(object src, PageChangingEventArgs e)
    {
        pager2.CurrentPageIndex = e.NewPageIndex;

        pageIndex2 = pager2.CurrentPageIndex;
        ViewState["pageIndex2"] = pageIndex2;
        DetailDataBind();
    }
    #endregion


    protected void btnExecBalance_Click(object sender, EventArgs e)
    {
        try
        {
            objBalance.ExecBalance(this.ddlWarehouse.SelectedValue, Convert.ToDateTime(this.txtDate.Text),"0");
            updateBll.InsertBusiStock();
            updateBll.UpdateDayReckno(Session["EmployeeCode"].ToString());
            GridDataBind();
            Session["OptimizeStatus"] = "<root><status>CONTINUE</status><message></message></root>";

            JScript.Instance.RegisterScript(Page, "post=true;");
            try
            {
                UploadDate uData = new UploadDate();
                uData.UploadInfoData();
                Session["OptimizeStatus"] = "<root><status>CONTINUE</status><message></message></root>";
            }
            catch (Exception exp)
            {
                Session["OptimizeStatus"] = "<root><status>ERROR</status><message></message></root>";
            }
            this.Panel1.Visible = true;
            this.pnlList.Visible = false;
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }


    protected void btnQuery_Click(object sender, EventArgs e)
    {
        filter = string.Format(" WH_CODE='{0}'",this.ddlWarehouse.SelectedValue);
        GridDataBind();
    }

    protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //*********************2011-03-02  没有日结量的不显示************************
        filter2 = string.Format("WH_CODE='{0}' AND SETTLEDATE='{1}'AND (ENDING<>'{2}'or BEGINNING<>'0'or ENTRYAMOUNT<>'0'or DELIVERYAMOUNT<>'0' or PROFITAMOUNT<>'0' or LOSSAMOUNT<>'0')", this.ddlWarehouse.SelectedValue, gvList.Rows[e.NewEditIndex].Cells[2].Text, "0.00");
        //filter2 = string.Format("WH_CODE='{0}' AND SETTLEDATE='{1}'",this.ddlWarehouse.SelectedValue,gvList.Rows[e.NewEditIndex].Cells[2].Text);
        pageIndex2 = 1;
        DetailDataBind();
        this.pnlBalance.Visible = true;
        this.pnlList.Visible = false;
        this.lblDate.Text = gvList.Rows[e.NewEditIndex].Cells[2].Text;
        this.lblHouse.Text = this.ddlWarehouse.SelectedItem.Text;
    }

    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            objBalance.ExecBalance(this.ddlWarehouse.SelectedValue, Convert.ToDateTime(gvList.Rows[e.RowIndex].Cells[2].Text.ToString()), "1");
            updateBll.UpdateDayReckno(Session["EmployeeCode"].ToString());
           
            JScript.Instance.ShowMessage(this, "重新日结完成");
            GridDataBind();
            Session["OptimizeStatus"] = "<root><status>CONTINUE</status><message></message></root>";

            JScript.Instance.RegisterScript(Page, "post=true;");
            try
            {
                UploadDate uData = new UploadDate();
                uData.UploadInfoData();
                Session["OptimizeStatus"] = "<root><status>CONTINUE</status><message></message></root>";
            }
            catch (Exception exp)
            {
                Session["OptimizeStatus"] = "<root><status>ERROR</status><message></message></root>";
            }
            this.Panel1.Visible = true;
            this.pnlList.Visible = false;
            
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }

    protected void DetailDataBind()
    {
        pager2.RecordCount = objBalance.GetRowCount(filter2);
        dsBalance = objBalance.QueryDailyBalance(pageIndex2, pageSize2, filter2);
        if (dsList.Tables[0].Rows.Count == 0)
        {
            dsBalance.Tables[0].Rows.Add(dsList.Tables[0].NewRow());
            gvBalance.DataSource = dsBalance;
            gvBalance.DataBind();
            int columnCount = gvBalance.Rows[0].Cells.Count;
            gvBalance.Rows[0].Cells.Clear();
            gvBalance.Rows[0].Cells.Add(new TableCell());
            gvBalance.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvBalance.Rows[0].Cells[0].Text = "";
            gvBalance.Rows[0].Visible = true;

        }
        else
        {
            this.gvBalance.DataSource = dsBalance.Tables[0];
            this.gvBalance.DataBind();
        }
        ViewState["pageIndex2"] = pageIndex2;
        ViewState["filter2"] = filter2;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.pnlBalance.Visible = false;
        this.pnlList.Visible = true;
    }
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].ColumnSpan = 2;
            e.Row.Cells[1].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            LinkButton lnkBtn = (LinkButton)e.Row.Cells[1].Controls[0];
            lnkBtn.OnClientClick = "return confirm('确认要重新日结？')";
            //lnkBtn.Attributes.Add("style", " text-align:center;word-break:keep-all; white-space:nowrap");
            //预警信息
            if (e.Row.Cells[8].Text.ToString() != "&nbsp;")
            {
                decimal quantityEnding = decimal.Parse(e.Row.Cells[8].Text.ToString()) / 5;
                if (quantityEnding >= 6000 && quantityEnding < 6500)
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
                }
                else if (quantityEnding >= 6500 && quantityEnding < 6900)
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Orange;
                }
                else if (quantityEnding >= 6900)
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                }
            }
            //if (e.Row.RowIndex % 2 == 0)
            //{
            //    e.Row.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
            //}
            //else
            //{
            //    e.Row.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
            //}
            //e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f5f5f5',this.style.fontWeight=''; this.style.cursor='hand';");
            ////当鼠标离开的时候 将背景颜色还原的以前的颜色
            //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

        }
    }
    protected void gvBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex % 2 == 0)
            {
                e.Row.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
            }
            else
            {
                e.Row.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f5f5f5',this.style.fontWeight=''; this.style.cursor='hand';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

        }
    }

    protected void btnZhangYan_Click(object sender, EventArgs e)
    {
        Session["OptimizeStatus"] = "<root><status>CONTINUE</status><message></message></root>";
      
        JScript.Instance.RegisterScript(Page, "post=true;");
        try
        {
            UploadDate uData = new UploadDate();
            uData.UploadInfoData();
            Session["OptimizeStatus"] = "<root><status>CONTINUE</status><message></message></root>";
        }
        catch (Exception exp)
        {
            Session["OptimizeStatus"] = "<root><status>ERROR</status><message></message></root>";
        }
        this.Panel1.Visible = true;
        this.pnlList.Visible = false;
    } 
}
