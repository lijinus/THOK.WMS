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

public partial class Code_Statistic_ProductGeneralAccountPage :BasePage
{
    string filter;
    int pageIndex = 1;
    int pageSize = 15;
    Balance objBalance = new Balance();
    DataSet dsBalance;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //
            pager.PageSize = pageSize;
            this.txtStartDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            this.txtEndDate.Text = this.txtStartDate.Text;
            //
            filter = string.Format("(SettleDate>='{0}' and SettleDate<='{1}')", this.txtStartDate.Text, this.txtEndDate.Text);
        }
        else
        {
            filter = ViewState["filter"].ToString();
            pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
        }
        GridDataBind();
    }

    private void GridDataBind()
    {
        pager.RecordCount = objBalance.GetRowCount(filter);
        dsBalance = objBalance.QueryDailyBalance(pageIndex, pageSize, filter);
        if (dsBalance.Tables[0].Rows.Count == 0)
        {
            dsBalance.Tables[0].Rows.Add(dsBalance.Tables[0].NewRow());
            gvStorage.DataSource = dsBalance;
            gvStorage.DataBind();
            int columnCount = gvStorage.Rows[0].Cells.Count;
            gvStorage.Rows[0].Cells.Clear();
            gvStorage.Rows[0].Cells.Add(new TableCell());
            gvStorage.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvStorage.Rows[0].Cells[0].Text = "没有符合以上条件的数据,请重新查询 ";
            gvStorage.Rows[0].Visible = true;

        }
        else
        {
            this.gvStorage.DataSource = dsBalance.Tables[0];
            this.gvStorage.DataBind();
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
    #endregion

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        filter = string.Format("   (SettleDate>='{0}' and SettleDate<='{1}')", this.txtStartDate.Text, this.txtEndDate.Text);
        GridDataBind();
    }


    protected void gvStorage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //CheckBox chk = new CheckBox();
            //chk.Attributes.Add("style", "text-align:center;word-break:keep-all; white-space:nowrap");
            //chk.ID = "checkAll";
            //chk.Attributes.Add("onclick", "checkboxChange(this,'gvMain',1);");
            //chk.Text = "操作";
            //e.Row.Cells[1].Controls.Add(chk);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ////CheckBox chk = new CheckBox();
            ////Label lblEdit = new Label();
            ////e.Row.Cells[1].Controls.Add(chk);
            LinkButton lnkBtn = new LinkButton();
            lnkBtn.Attributes.Add("style", " text-align:center;word-break:keep-all; white-space:nowrap");
            lnkBtn.Text = "库存明细";
            ////lnkBtn.OnClientClick = string.Format("javascript:window.open('StorageDetailQueryPage.aspx?date={0}&productcode={1}','_blank');return false;", e.Row.Cells[1].Text, e.Row.Cells[2].Text);
            lnkBtn.OnClientClick = string.Format("javascript:window.showModalDialog('StorageDetailQueryPage.aspx?date={0}&productcode={1}',''," +
                                            " 'top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=880px;dialogHeight=550px')", e.Row.Cells[1].Text, e.Row.Cells[2].Text);
            e.Row.Cells[0].Controls.Add(lnkBtn);


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
}
