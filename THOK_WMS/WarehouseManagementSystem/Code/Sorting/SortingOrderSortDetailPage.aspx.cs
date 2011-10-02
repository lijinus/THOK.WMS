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

public partial class Code_Sorting_SortingOrderSortDetailPage : System.Web.UI.Page
{
    string file = "1=1";
    SortingOrderStateBll stbll = new SortingOrderStateBll();
    DataTable dsMaster;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["date"] != null && Request.QueryString["linecode"] != null && Request.QueryString["sortingcode"] != null)
        {
            file = string.Format("DELIVER_LINE_CODE='{0}' AND ORDER_DATE ='{1}' AND SORT_DATE='{1}' AND A.SORTING_CODE='{2}'", Request.QueryString["linecode"], Request.QueryString["date"], Request.QueryString["sortingcode"]);

        }
        else
        {
            file = string.Format("DELIVER_LINE_CODE='{0}' AND ORDER_DATE ='{1}' AND SORT_DATE='{1}' AND A.SORTING_CODE='{2}'", Request.QueryString["linecode"], Request.QueryString["date"], Request.QueryString["sortingcode"]);
        }
        this.GridDataBind();
    }

    #region 数据源绑定

    void GridDataBind()
    {
        dsMaster = stbll.QuerySort(file);
        if (dsMaster.Rows.Count == 0)
        {
            dsMaster.Rows.Add(dsMaster.NewRow());
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
            dgDetail.DataSource = dsMaster;
            dgDetail.DataBind();
        }
    }
    #endregion
}
