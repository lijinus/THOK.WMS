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
using THOK.WMS;

public partial class Code_Sorting_SortingOrderStatePage : System.Web.UI.Page
{
    int pageIndex = 1; //明细分页
    int pageSize = 15;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "1=1";
    DataSet dsMoveDetail;
    DataSet dsMoveMaster;
    DataSet dsDetail;
    DataSet dsMaster;
    DataSet dsAllotment;
    string datetime = "";
    SortingOrderStateBll stbll = new SortingOrderStateBll();

    #region LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               // pager.PageSize = pageSize;
                //totalCount = stbll.GetRowCount(filter);
               // pager.RecordCount = totalCount;
                GridDataBind();

            }
            else
            {
                //pageCount = Convert.ToInt32(ViewState["pageCount"]);
                //pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
                //totalCount = Convert.ToInt32(ViewState["totalCount"]);
                //filter = ViewState["filter"].ToString();
                //totalCount = stbll.GetRowCount(filter);
               // pager.RecordCount = totalCount;
                GridDataBind();
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }
    #endregion

    #region 数据源绑定

    void GridDataBind()
    {
        dsMaster = stbll.QuerySortingState(datetime, filter);
        if (dsMaster.Tables[0].Rows.Count == 0)
        {
            dsMaster.Tables[0].Rows.Add(dsMaster.Tables[0].NewRow());
            dgDetail.DataSource = dsMaster;
            dgDetail.DataBind();

            int columnCount = dgDetail.Rows[0].Cells.Count;
            dgDetail.Rows[0].Cells.Clear();
            dgDetail.Rows[0].Cells.Add(new TableCell());
            dgDetail.Rows[0].Cells[0].ColumnSpan = columnCount;
            dgDetail.Rows[0].Cells[0].Text = "没有符合以上条件的数据";
            dgDetail.Rows[0].Visible = true;

        }
        else
        {
            if (!IsPostBack)
            {
                // LoadBill(dsMaster.Tables[0].Rows[0]["BILLNO"].ToString());
                //this.lblBillNo.Text = dsMaster.Tables[0].Rows[0]["BILLNO"].ToString();
            }
            //this.gvMain.DataSource = dsMaster.Tables[0];
            //this.gvMain.DataBind();
            dgDetail.DataSource = dsMaster;
            dgDetail.DataBind();
        }

        ViewState["pageIndex"] = pageIndex;
        ViewState["totalCount"] = totalCount;
        ViewState["pageCount"] = pageCount;
        ViewState["filter"] = filter;
    }
    #endregion

    #region 查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        { 
            if (this.ddl_Field.SelectedValue == "SORT_DATE")
            {
                datetime = Convert.ToDateTime(this.txtKeyWords.Text.Trim()).ToString("yyyyMMdd");
                filter = "0";
            }
            else
            {
                filter = string.Format("{0} like '%{1}%'", this.ddl_Field.SelectedValue, this.txtKeyWords.Text.ToString());
            }
            GridDataBind();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "数据查询出错" + exp.Message);
        }
    }
    #endregion


    # region 分页控件 页码changing事件
    //protected void pager_PageChanging(object src, PageChangingEventArgs e)
    //{
    //    //pager.CurrentPageIndex = e.NewPageIndex;
    //    //pager.RecordCount = totalCount;
    //    //pageIndex = pager.CurrentPageIndex;
    //    ViewState["pageIndex"] = pageIndex;
    //    GridDataBind();
    //}
    #endregion


    #region 退出
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../MainPage.aspx");

    }
    #endregion


    #region 明细绑定
    protected void dgDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox chk = new CheckBox();
                chk.Attributes.Add("style", "text-align:center;word-break:keep-all; white-space:nowrap");
                chk.ID = "checkAll";
                chk.Attributes.Add("onclick", "checkboxChange(this,'dgDetail',0);");
                chk.Text = "操作";
                e.Row.Cells[0].Controls.Add(chk);
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkBtn = new LinkButton();
                lnkBtn.Attributes.Add("style", " text-align:center;word-break:keep-all; white-space:nowrap");
                lnkBtn.Text = "库存明细";
                ////lnkBtn.OnClientClick = string.Format("javascript:window.open('StorageDetailQueryPage.aspx?date={0}&productcode={1}','_blank');return false;", e.Row.Cells[1].Text, e.Row.Cells[2].Text);
               // string s = string.Format("sortingcode={0}&date={1}&linecode={2}", e.Row.Cells[1].Text, e.Row.Cells[2].Text, e.Row.Cells[3].Text);
                lnkBtn.OnClientClick = string.Format("javascript:window.showModalDialog('SortingOrderSortDetailPage.aspx?sortingcode={0}&date={1}&linecode={2}',''," +
                                                " 'top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=880px;dialogHeight=550px')", e.Row.Cells[1].Text, e.Row.Cells[3].Text,e.Row.Cells[4].Text);
                e.Row.Cells[12].Controls.Add(lnkBtn);

                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.FromName(Session["grid_EvenRowColor"].ToString());
                }
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f5f5f5',this.style.fontWeight=''; this.style.cursor='hand';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CheckBox chk = new CheckBox();
                //chk.AutoPostBack = true;
                //e.Row.Cells[0].Attributes.Add("onclick", "javascript:document.getElementById('lblQuantitySum').text = this.QUANTITY.TEXT;");
                e.Row.Cells[0].Controls.Add(chk);   
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "数据绑定出错：" + exp.Message);
        }
    }
    #endregion


    #region 分配和取消送货人
    //分配
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if (txtEmployeeName.Text.Trim() != "")
        {
            try
            {
                CheckBox chk = new CheckBox();
                string RouteCode = "";
                bool hasSelected = false;
                string orderdate = "";
                if (dgDetail.Rows.Count > 0)
                {
                    for (int i = 0; i < dgDetail.Rows.Count; i++)
                    {
                        chk = (CheckBox)dgDetail.Rows[i].Cells[0].Controls[0];
                        if (dgDetail.Rows[i].Cells[11].Text.Trim() != "&nbsp;")
                            continue;
                        if (chk.Checked)
                        {
                            RouteCode += dgDetail.Rows[i].Cells[4].Text.ToString() + ",";
                            //orderdate += dgDetail.Items[i].Cells[4].Text.ToString() + ",";
                            hasSelected = true;
                        }
                    }
                    if (hasSelected)
                    {
                        string EmployeeCode = this.txtEmployeeCode.Text.Trim();//送货员编码
                        //获取线路id
                        RouteCode = RouteCode.Substring(0, RouteCode.Length - 1);
                        string RouteCodeId = UtinString.StringMake(RouteCode);
                        //获取客户编码
                        DataTable custCodeTable = stbll.GetCustByLine(RouteCodeId);
                        string custlist = UtinString.StringMake(custCodeTable, "CUST_CODE");
                        string custCode = UtinString.StringMake(custlist);
                        stbll.UpdateSortStatus(custCode, EmployeeCode);//确认送货员
                        GridDataBind();
                    }
                    else
                    {
                        JScript.Instance.ShowMessage(this, "请选择要分配的单据,或选择的单据已分配！");
                    }
                }
                else
                {
                    JScript.Instance.ShowMessage(this, "无数据可以分配！");
                }
            }
            catch (Exception exp)
            {
                JScript.Instance.ShowMessage(this, exp.Message);
            }
        }
        else
        {
            JScript.Instance.ShowMessage(this, "分拣线名称不能为空！");
        }
    }
    //取消
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = new CheckBox();
            string RouteCode = "";
            bool hasSelected = false;
            string orderdate = "";
            if (dgDetail.Rows.Count > 0)
            {
                for (int i = 0; i < dgDetail.Rows.Count; i++)
                {
                    //if (dgDetail.Rows[i].Cells[10].Text.ToString()!="")
                    //    continue;
                    chk = (CheckBox)dgDetail.Rows[i].Cells[0].Controls[0];
                    if (chk.Checked)
                    {
                        RouteCode += dgDetail.Rows[i].Cells[4].Text.ToString() + ",";
                        //orderdate += dgDetail.Items[i].Cells[4].Text.ToString() + ",";
                        hasSelected = true;
                    }
                }
                if (hasSelected)
                {
                    RouteCode = RouteCode.Substring(0, RouteCode.Length - 1);
                    string RouteCodeId = UtinString.StringMake(RouteCode);
                    //获取客户编码
                    DataTable custCodeTable = stbll.GetCustByLine(RouteCodeId);
                    string custlist = UtinString.StringMake(custCodeTable, "CUST_CODE");
                    string custCode = UtinString.StringMake(custlist);
                    stbll.UpdateSortStatus(custCode, null);//取消送货员
                    GridDataBind();
                }
                else
                {
                    JScript.Instance.ShowMessage(this, "请选择要分配的单据,或选择的单据已分配！");
                }
            }
            else
            {
                JScript.Instance.ShowMessage(this, "无数据可以分配！");
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }

    #endregion

}
