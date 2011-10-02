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
using System.IO;
using org.in2bits.MyXls;
using THOK.WMS;

public partial class Code_Sorting_SortingRouteDetailPage : System.Web.UI.Page
{
    string file = "1=1";
    SortingRouteBll route = new SortingRouteBll();
    DataSet dsMaster;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            file = string.Format("ORDER_DATE='' AND SORTING_CODE !=''");
            
            this.GridDataBind();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }

    #region 数据源绑定

    void GridDataBind()
    {
        dsMaster = route.QuerySortingRouteDetail(file);
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
            dgDetail.DataSource = dsMaster;
            dgDetail.DataBind();
        }
    }
    #endregion

    //导出Execl
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string sortingcode = this.txtSortingCode.Text.Trim();
            string datetime = this.txtKeyWords.Text.Trim();

            if (sortingcode == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请选择分拣线！");
                return;
            }
            if (datetime == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请输入日期！");
                return;
            }

            datetime = Convert.ToDateTime(datetime).Date.ToString("yyyyMMdd");
            file = string.Format("ORDER_DATE='{0}' AND SORTING_CODE='{1}'", datetime, sortingcode);

            DataTable dt = new DataTable();
            dt = route.QuerySortingRouteDetail(file).Tables[0];

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
        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "分拣线汇总表";
        xls.FileName = fileName;
        int rowIndex = 1;
        Worksheet sheet = xls.Workbook.Worksheets.Add("分拣线汇总表");
        Cells cells = sheet.Cells;
        sheet.Cells.Merge(1, 1, 1, 2);
        Cell cell = cells.Add(1, 1, "分拣线汇总表");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 1, "产品代码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 2, "产品名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 3, "数量(件)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 4, "数量(条)");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 5, "分拣线编码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 6, "分拣线名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        foreach (DataRow row in dt.Rows)
        {
            cells.Add(rowIndex + 2, 1, "" + row["PRODUCTCODE"] + "");
            cells.Add(rowIndex + 2, 2, "" + row["PRODUCTNAME"] + "");
            cells.Add(rowIndex + 2, 3, "" + row["QTY_JIAN"] + "");
            cells.Add(rowIndex + 2, 4, "" + row["QTY_TIAO"] + "");
            cells.Add(rowIndex + 2, 5, "" + row["SORTING_CODE"] + "");
            cells.Add(rowIndex + 2, 6, "" + row["SORTING_NAME"] + "");
            rowIndex++;
        }

        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        string file = System.Web.HttpContext.Current.Server.MapPath("~/Excel/");
        xls.Save(file);
        //xls.Send();
        return fileName;

    }

    //返回
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("SortingRoutePage.aspx");
    }

    //查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            string sortingcode = this.txtSortingCode.Text.Trim();
            string datetime = this.txtKeyWords.Text.Trim();
            
            if (sortingcode == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请选择分拣线！");
                return;
            }
            if (datetime == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请输入日期！");
                return;
            }
            datetime = Convert.ToDateTime(datetime).Date.ToString("yyyyMMdd");
            file =string.Format( "ORDER_DATE='{0}' AND SORTING_CODE='{1}'",datetime,sortingcode);
            this.GridDataBind();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
}
