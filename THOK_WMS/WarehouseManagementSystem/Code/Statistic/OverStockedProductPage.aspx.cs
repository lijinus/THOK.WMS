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
using THOK.WMS.BLL;

public partial class Code_Statistic_OverStockedProductPage : BasePage
{
    string filter = "CURRENTPRODUCT IS NOT NULL AND CURRENTPRODUCT<>''";
    int pageIndex = 1;
    int pageSize = 15;
    WarehouseCell objCell = new WarehouseCell();
    Warehouse objHouse = new Warehouse();
    WarehouseArea objArea = new WarehouseArea();
    DataSet dsProduct;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //
            pager.PageSize = pageSize;

            DataSet dsTemp = objHouse.QueryAllWarehouse();
            this.ddlWarehouse.DataSource = dsTemp.Tables[0].DefaultView;
            this.ddlWarehouse.DataTextField = "WH_NAME";
            this.ddlWarehouse.DataValueField = "WH_CODE";
            this.ddlWarehouse.DataBind();
            dsTemp = objArea.QueryAreaByWHCODE(this.ddlWarehouse.Items[0].Value);
            this.ddlArea.DataSource = dsTemp.Tables[0].DefaultView;
            this.ddlArea.DataTextField = "AREANAME";
            this.ddlArea.DataValueField = "AREACODE";
            this.ddlArea.DataBind();
            //
            filter = string.Format("CURRENTPRODUCT IS NOT NULL AND CURRENTPRODUCT<>'' AND  WH_CODE='{0}' and AREACODE='{1}' and DateDiff(day,inputdate,getdate())>90"
                           , this.ddlWarehouse.SelectedValue, this.ddlArea.SelectedValue);
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
        pager.RecordCount = objCell.GetRowCount(filter);
        dsProduct = objCell.QueryProductDistribution(pageIndex, pageSize, filter);
        if (dsProduct.Tables[0].Rows.Count == 0)
        {
            dsProduct.Tables[0].Rows.Add(dsProduct.Tables[0].NewRow());
            gvStorage.DataSource = dsProduct;
            gvStorage.DataBind();
            int columnCount = gvStorage.Rows[0].Cells.Count;
            gvStorage.Rows[0].Cells.Clear();
            gvStorage.Rows[0].Cells.Add(new TableCell());
            gvStorage.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvStorage.Rows[0].Cells[0].Text = "没有入库超过90天的产品 ";
            gvStorage.Rows[0].Visible = true;

        }
        else
        {
            this.gvStorage.DataSource = dsProduct.Tables[0];
            this.gvStorage.DataBind();
        }
        GridRowSpan(0);
        GridRowSpan(1);
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
        filter = string.Format("CURRENTPRODUCT IS NOT NULL AND CURRENTPRODUCT<>'' and  WH_CODE='{0}' and AREACODE='{1}'and DateDiff(day,inputdate,getdate())>90"
                      , this.ddlWarehouse.SelectedValue, this.ddlArea.SelectedValue);
        pageIndex = 1;
        pager.CurrentPageIndex = 1;
        GridDataBind();
    }


    protected void gvStorage_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GridRowSpan(int CellIndex)
    {
        if (this.gvStorage.Rows.Count >= 2)
        {
            int start = 0;
            int end = 0;
            for (int i = 1; i < gvStorage.Rows.Count; i++)
            {
                if (gvStorage.Rows[i].Cells[CellIndex].Text == gvStorage.Rows[i - 1].Cells[CellIndex].Text)
                {
                    end = i;
                    if (i < gvStorage.Rows.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        int span = end - start + 1;
                        gvStorage.Rows[start].Cells[CellIndex].RowSpan = span;
                        for (int k = start + 1; k <= end; k++)
                        {
                            gvStorage.Rows[k].Cells[CellIndex].Visible = false;
                        }
                    }
                }
                else
                {
                    int span = end - start + 1;
                    gvStorage.Rows[start].Cells[CellIndex].RowSpan = span;
                    for (int k = start + 1; k <= end; k++)
                    {
                        gvStorage.Rows[k].Cells[CellIndex].Visible = false;
                    }

                    start = end + 1;
                    end++;
                }
            }
        }
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsTemp = objArea.QueryAreaByWHCODE(this.ddlWarehouse.SelectedValue);
        this.ddlArea.DataSource = dsTemp.Tables[0].DefaultView;
        this.ddlArea.DataTextField = "AREANAME";
        this.ddlArea.DataValueField = "AREACODE";
        this.ddlArea.DataBind();
    }
}
