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

public partial class Code_Statistic_StorageQueryPage :BasePage
{
    string filter;
    string file = " AND (BEGINNING>0 OR ENTRYAMOUNT>0 OR DELIVERYAMOUNT>0 OR PROFITAMOUNT>0 OR LOSSAMOUNT>0 OR ENDING>0 OR SELLMOUNT>0 OR CELLLOSEMOUNT>0 OR LOSEMOUNT>0)";
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
            Warehouse objHouse = new Warehouse();
            DataSet dsTemp = objHouse.QueryAllWarehouse();
            this.ddlWarehouse.DataSource = dsTemp.Tables[0].DefaultView;
            this.ddlWarehouse.DataTextField = "WH_NAME";
            this.ddlWarehouse.DataValueField = "WH_CODE";
            this.ddlWarehouse.DataBind();
            this.txtStartDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            this.txtEndDate.Text = this.txtStartDate.Text;
            //
            
            filter = string.Format(" WH_CODE='{0}' and  (SettleDate>='{1}' and SettleDate<='{2}')", this.ddlWarehouse.SelectedValue, this.txtStartDate.Text,this.txtEndDate.Text);
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
        filter = filter + file;
        pager.RecordCount = objBalance.GetStorageGeneralRowCount(filter);
        dsBalance = objBalance.QueryStorageGeneralAccount(pageIndex, pageSize, filter);
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
        filter = string.Format(" WH_CODE='{0}' and  (SettleDate>='{1}' and SettleDate<='{2}')", this.ddlWarehouse.SelectedValue, this.txtStartDate.Text, this.txtEndDate.Text);
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
                                            " 'top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=880px;dialogHeight=550px')",e.Row.Cells[1].Text, e.Row.Cells[2].Text);
            e.Row.Cells[0].Controls.Add(lnkBtn);


            if (e.Row.RowIndex % 2 == 0)
            {
                e.Row.BackColor =System.Drawing.Color.FromName(Session["grid_OddRowColor"].ToString());
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

    //查询库存卷烟信息
    protected void btnQueryStock_Click(object sender, EventArgs e)
    {
        Response.Redirect("RealTimeStock.aspx");
    }

    //查询卷烟库存信息
    protected void btnQueryProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("RealTimeProduct.aspx");
    }

    //导出execl打印
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            filter = filter + file;
            dt = objBalance.QueryStorageGeneralExecl(filter);
            
            if (dt.Rows.Count == 0)
            {
                JScript.Instance.ShowMessage(this, "没有数据导出");
            }
            else
            {
                string fileName = this.Export(dt);
                string path = Request.PhysicalApplicationPath + "Excel\\" + fileName + ".xls";
                bool flag = Excel.ResponseFile(Page.Request, Page.Response, "\\Excel\\" + fileName + ".xls", path, 1024000);//ResponseFile(Page.Request, Page.Response, "\\Excel\\" + fileName + ".xls", path, 1024000);
                FileInfo filele = new FileInfo(path);
                filele.Delete();
                if (flag)
                {
                    JScript.Instance.ShowMessage(this, "导出成功！");
                }
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }

    public string Export(DataTable dt)
    {
        XlsDocument xls = new XlsDocument();
        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "库存总账表";
        xls.FileName = fileName;
        int rowIndex = 1;
        Worksheet sheet = xls.Workbook.Worksheets.Add("库存总账表");
        Cells cells = sheet.Cells;
        sheet.Cells.Merge(1, 1, 1, 2);
        Cell cell = cells.Add(1, 1, "库存总账表");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 1, "日期");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 2, "产品编码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 3, "产品名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 4, "期初量(件)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 5, "期初量(条)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 6, "入库量(件)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 7, "入库量(条)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 8, "出库量(件)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 9, "出库量(条)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 10, "盘盈量(件)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 11, "盘盈量(条)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 12, "盘亏量(件)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 13, "盘亏量(条)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 14, "日结量(件)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 15, "日结量(条)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 16, "损烟量(件)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 17, "损烟量(条)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 18, "可销量(件)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 19, "可销量(条)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 20, "库存量(件)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 21, "库存量(条)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        foreach (DataRow row in dt.Rows)
        {
            cells.Add(rowIndex + 2, 1, "" + row["SETTLEDATE"] + "");
            cells.Add(rowIndex + 2, 2, "" + row["PRODUCTCODE"] + "");
            cells.Add(rowIndex + 2, 3, "" + row["PRODUCTNAME"] + "");
            cells.Add(rowIndex + 2, 4, "" + row["BEGINNING"] + "");
            cells.Add(rowIndex + 2, 5, "" + row["BEGINNING_TIAO"] + "");
            cells.Add(rowIndex + 2, 6, "" + row["ENTRYAMOUNT"] + "");
            cells.Add(rowIndex + 2, 7, "" + row["ENTRY_TIAO"] + "");
            cells.Add(rowIndex + 2, 8, "" + row["DELIVERYAMOUNT"] + "");
            cells.Add(rowIndex + 2, 9, "" + row["DELIVERY_TIAO"] + "");
            cells.Add(rowIndex + 2, 10, "" + row["PROFITAMOUNT"] + "");
            cells.Add(rowIndex + 2, 11, "" + row["PROFIT_TIAO"] + "");
            cells.Add(rowIndex + 2, 12, "" + row["LOSSAMOUNT"] + "");
            cells.Add(rowIndex + 2, 13, "" + row["LOSS_TIAO"] + "");
            cells.Add(rowIndex + 2, 14, "" + row["ENDING"] + "");
            cells.Add(rowIndex + 2, 15, "" + row["ENDING_TIAO"] + "");
            cells.Add(rowIndex + 2, 16, "" + row["LOSEMOUNT"] + "");
            cells.Add(rowIndex + 2, 17, "" + row["LOSEMOUNT_TIAO"] + "");
            cells.Add(rowIndex + 2, 18, "" + row["CELLLOSEMOUNT"] + "");
            cells.Add(rowIndex + 2, 19, "" + row["CELLLOSEMOUNT_TIAO"] + "");
            cells.Add(rowIndex + 2, 20, "" + row["SELLMOUNT"] + "");
            cells.Add(rowIndex + 2, 21, "" + row["SELLMOUNT_TIAO"] + "");
            rowIndex++;
        }

        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        string file = System.Web.HttpContext.Current.Server.MapPath("~/Excel/");
        xls.Save(file);
        //xls.Send();
        return fileName;

    }
}
