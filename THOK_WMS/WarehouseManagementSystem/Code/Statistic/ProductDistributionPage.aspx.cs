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
using org.in2bits.MyXls;
using System.IO;
using THOK.WMS;


public partial class Code_Statistic_ProductDistributionPage : BasePage
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
        objCell.UpdateCellEx();
        objCell.UpdateCell();
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
            filter = string.Format("CURRENTPRODUCT IS NOT NULL AND CURRENTPRODUCT<>'' AND  WH_CODE='{0}' and AREACODE='{1}'", this.ddlWarehouse.SelectedValue,this.ddlArea.SelectedValue);
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
            gvStorage.Rows[0].Cells[0].Text = "没有符合以上条件的数据,请重新查询 ";
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
        try
        {
            filter = string.Format("CURRENTPRODUCT IS NOT NULL AND CURRENTPRODUCT<>'' and  WH_CODE='{0}' and AREACODE='{1}' and (CURRENTPRODUCT like '{2}%' or C_PRODUCTNAME LIKE '{2}%')"
                                                 , this.ddlWarehouse.SelectedValue
                                                 , this.ddlArea.SelectedValue
                                                 , this.txtKeywords.Text.Trim());
            pageIndex = 1;
            pager.CurrentPageIndex = 1;
            GridDataBind();
        }
        catch( Exception exp)
        {

        }
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
                e.Row.BackColor = System.Drawing.Color.FromName(Session["grid_OddRowColor"].ToString());
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.FromName(Session["grid_EvenRowColor"].ToString());
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

    //protected void btnProduct_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("EnterAndDeliveryBillQuery.aspx");
    //}


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            WarehouseCell objCell = new WarehouseCell();
            DataTable dt = objCell.QueryProductDistribution(filter);
            if (dt.Rows.Count == 0)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "当前实时库存为空！");
            }
            else
            {
                string fileName = this.Export(dt);
                string path = Request.PhysicalApplicationPath + "Excel\\" + fileName + ".xls";
                bool flag = Excel.ResponseFile(Page.Request, Page.Response, "\\Excel\\" + fileName + ".xls", path, 1024000);//ResponseFile(Page.Request, Page.Response, "\\Excel\\" + fileName + ".xls", path, 1024000);
                FileInfo file = new FileInfo(path);
                file.Delete();
                if (flag)
                {
                    JScript.Instance.ShowMessage(this.UpdatePanel1, "导出成功！");
                }
            }
       
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1,exp.Message);
            //throw;
        }
    }

    private string Export(DataTable dt)
    {
        XlsDocument xls = new XlsDocument();
        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "库存信息";
        xls.FileName = fileName;
        int rowIndex = 1;
        Worksheet sheet = xls.Workbook.Worksheets.Add("产品货位库存");
        Cells cells = sheet.Cells;
        sheet.Cells.Merge(1, 1, 1, 8);
        Cell cell = cells.Add(1, 1, "产品库存");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 1, "产品代码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 2, "产品名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 3, "货位编码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 4, "货位名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 5, "库存数量");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 6, "单位编码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 7, "单位名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 8, "入库日期");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        foreach (DataRow row in dt.Rows)
        {
            cells.Add(rowIndex + 2, 1, "" + row["CURRENTPRODUCT"] + "");
            cells.Add(rowIndex + 2, 2, "" + row["C_PRODUCTNAME"] + "");
            cells.Add(rowIndex + 2, 3, "" + row["CELLCODE"] + "");
            cells.Add(rowIndex + 2, 4, "" + row["CELLNAME"] + "");
            cells.Add(rowIndex + 2, 5, "" + row["QUANTITY"] + "");
            cells.Add(rowIndex + 2, 6, "" + row["UNITCODE"] + "");
            cells.Add(rowIndex + 2, 7, "" + row["UNITNAME"] + "");
            cells.Add(rowIndex + 2, 8, "" + row["INPUTDATE"] + "");
            rowIndex++;
        }

        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        string file = System.Web.HttpContext.Current.Server.MapPath("~/Excel/");
        xls.Save(file);
        //xls.Send();
        return fileName;
    }

}
