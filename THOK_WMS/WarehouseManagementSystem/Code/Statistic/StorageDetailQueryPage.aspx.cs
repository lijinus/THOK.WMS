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

public partial class Code_Statistic_StorageDetailQueryPage :BasePage
{
    Balance objBalance = new Balance();
    DataSet dsBalance;
    int pageIndex = 1;
    int pageSize = 10000;
    string filter;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Request.QueryString["date"] != null)
            {
                filter = string.Format("BILLDATE='{0}' AND PRODUCTCODE='{1}'", Request.QueryString["date"], Request.QueryString["productcode"]);
                //objBalance.QueryStorageDetail(pageIndex, pageSize, filter);
            }
        }
        else
        {
            filter = ViewState["filter"].ToString();
        }
        GridDataBind();
    }

    private void GridDataBind()
    {
        pager.RecordCount = objBalance.GetStorageDetailRowCount(filter);
        dsBalance = objBalance.QueryStorageDetail(pageIndex, pageSize, filter);
        if (dsBalance.Tables[0].Rows.Count == 0)
        {
            dsBalance.Tables[0].Rows.Add(dsBalance.Tables[0].NewRow());
            gvStorage.DataSource = dsBalance;
            gvStorage.DataBind();
            int columnCount = gvStorage.Rows[0].Cells.Count;
            gvStorage.Rows[0].Cells.Clear();
            gvStorage.Rows[0].Cells.Add(new TableCell());
            gvStorage.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvStorage.Rows[0].Cells[0].Text = "没有当日入库、出库、或盘点盈亏的记录";
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

    protected void gvStorage_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
