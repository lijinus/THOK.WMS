using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using org.in2bits.MyXls;
using THOK.WMS;

public partial class Code_Statistic_RealTimeStock :BasePage
{
    int pageIndex = 1;
    int pageSize = 5;
    int totalCount = 0;
    int pageCount = 0;
    StockBLL stockBll = new StockBLL();
    DataTable stockDt;
    string  filter = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            stockDt = stockBll.RealTimeStock();
            string num = stockDt.Compute("SUM(QUANTITY)", "").ToString();
            this.lblNumber.Text = num;
        }
        GetDataBind(stockDt);
    }


    public void GetDataBind(DataTable dt)
    {
        gvStock.DataSource = dt;
        gvStock.DataBind();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (this.txtAreaName.Text.ToString().Trim() == "")
        {
            stockDt = stockBll.RealTimeStock();
        }
        else
        {
            filter = "AREANAME like '" + this.txtAreaName.Text.ToString().Trim() + "%'";
            stockDt = stockBll.Query(filter); 
        }
        GetDataBind(stockDt);

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            if (this.txtAreaName.Text.ToString().Trim() == "")
            {
                dt = stockBll.RealTimeStock();
            }
            else
            {
                filter = "AREANAME like '" + this.txtAreaName.Text.ToString().Trim() + "%'";
                dt = stockBll.Query(filter);
            }

            if (dt.Rows.Count == 0)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "没有数据导出");
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
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }


    public string Export(DataTable dt)
    {
        XlsDocument xls = new XlsDocument();
        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "库区库存";
        xls.FileName = fileName;
        int rowIndex = 1;
        Worksheet sheet = xls.Workbook.Worksheets.Add("库区库存");
        Cells cells = sheet.Cells;
        sheet.Cells.Merge(1, 1, 1, 2);
        Cell cell = cells.Add(1, 1, "库区库存");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 1, "产品代码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 2, "产品名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 3, "单位编码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 4, "单位名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 5, "库存数量");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 6, "库区名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        foreach (DataRow row in dt.Rows)
        {
            cells.Add(rowIndex + 2, 1, "" + row["CURRENTPRODUCT"] + "");
            cells.Add(rowIndex + 2, 2, "" + row["PRODUCTNAME"] + "");
            cells.Add(rowIndex + 2, 3, "" + row["UNITCODE"] + "");
            cells.Add(rowIndex + 2, 4, "" + row["UNITNAME"] + "");
            cells.Add(rowIndex + 2, 5, "" + row["QUANTITY"] + "");
            cells.Add(rowIndex + 2, 6, "" + row["AREANAME"] + "");

            rowIndex++;
        }

        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        string file = System.Web.HttpContext.Current.Server.MapPath("~/Excel/");
        xls.Save(file);
        //xls.Send();
        return fileName;

    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("StorageQueryPage.aspx");
    }
}
