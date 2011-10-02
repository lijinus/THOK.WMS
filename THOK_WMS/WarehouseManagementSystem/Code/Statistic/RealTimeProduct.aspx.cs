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
using org.in2bits.MyXls;
using System.IO;
using THOK.WMS;
using THOK.WMS.BLL;

public partial class Code_Statistic_RealTimeProduct : System.Web.UI.Page
{

    DataTable stockDt;
    StockBLL stockBll = new StockBLL();
    string file = "1=1";
    WarehouseCell warecell = new WarehouseCell();
    protected void Page_Load(object sender, EventArgs e)
    {
        warecell.UpdateCellEx();
        warecell.UpdateCell();
        if (!IsPostBack)
        {
            //stockDt = stockBll.QueryStockProduct(file);
            //string num = stockDt.Compute("SUM(QUANTITY)", "").ToString();
            //this.lblNumber.Text = num;
        }
        GetDataBind();
    }

    public void GetDataBind()
    {
        stockDt = stockBll.QueryStockProduct(file);
        gvStock.DataSource = stockDt;
        gvStock.DataBind();
    }
    
    //查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            file = string.Format(" {0} like '{1}%'", this.ddl_Field.SelectedValue, this.txtAreaName.Text.Trim());
            this.GetDataBind();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "数据查询出错" + exp.Message);
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = stockBll.QueryStockProduct(file);

            if (dt.Rows.Count == 0)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "没有数据导出");
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
        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "卷烟库存";
        xls.FileName = fileName;
        int rowIndex = 1;
        Worksheet sheet = xls.Workbook.Worksheets.Add("卷烟库存");
        Cells cells = sheet.Cells;
        sheet.Cells.Merge(1, 1, 1, 2);
        Cell cell = cells.Add(1, 1, "卷烟库存");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 1, "产品代码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 2, "产品名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 3, "库存数量(件)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 4, "库存数量(条)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 5, "库存数量(总支)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        foreach (DataRow row in dt.Rows)
        {
            cells.Add(rowIndex + 2, 1, "" + row["CURRENTPRODUCT"] + "");
            cells.Add(rowIndex + 2, 2, "" + row["C_PRODUCTNAME"] + "");
            cells.Add(rowIndex + 2, 3, "" + row["QUY_JIAN"] + "");
            cells.Add(rowIndex + 2, 4, "" + row["QUY_TIAO"] + "");
            cells.Add(rowIndex + 2, 5, "" + row["QUY_ZHI"] + "");
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
