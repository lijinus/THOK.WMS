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
using THOK.WMS.BLL;
using System.Drawing;
using org.in2bits.MyXls;
using THOK.WMS;

public partial class Code_Statistic_DeliveryBillQueryPage : BasePage
{
    int pageIndex = 1;
    int pageSize = 5;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "1=1";
    //string PrimaryKey = "ID";
    string OrderByFields = "BILLNO desc";
    DeliveryBillMaster billMaster = new DeliveryBillMaster();
    DeliveryBillDetail billDetail = new DeliveryBillDetail();
    DeliveryAllot objAllot = new DeliveryAllot();
    BillExcelBLL billExcel = new BillExcelBLL();
    DataSet dsDetail;
    DataSet dsMaster;
    DataSet dsAllotment;
    int pageIndex2 = 1;
    int pageSize2 = 5;
    //int totalCount2 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pager.PageSize = pageSize;
                //pager2.PageSize = pageSize2;
                totalCount = billMaster.GetRowCount(filter);
                pager.RecordCount = totalCount;
                GridDataBind();
            }
            else
            {
                pageCount = Convert.ToInt32(ViewState["pageCount"]);
                pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
                totalCount = Convert.ToInt32(ViewState["totalCount"]);
                filter = ViewState["filter"].ToString();
                OrderByFields = ViewState["OrderByFields"].ToString();
                totalCount = billMaster.GetRowCount(filter);
                pageIndex2 = Convert.ToInt32(ViewState["pageIndex2"]);

                pager.RecordCount = totalCount;
                GridDataBind();

            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }

    #region 数据源绑定
    void GridDataBind()
    {
        dsMaster = billMaster.QueryDeliveryBillMaster(pageIndex, pageSize, filter, OrderByFields);
        if (dsMaster.Tables[0].Rows.Count == 0)
        {
            dsMaster.Tables[0].Rows.Add(dsMaster.Tables[0].NewRow());
            gvMain.DataSource = dsMaster;
            gvMain.DataBind();
            int columnCount = gvMain.Rows[0].Cells.Count;
            gvMain.Rows[0].Cells.Clear();
            gvMain.Rows[0].Cells.Add(new TableCell());
            gvMain.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvMain.Rows[0].Cells[0].Text = "没有符合以上条件的数据";
            gvMain.Rows[0].Visible = true;
        }
        else
        {
            if (!IsPostBack)
            {
                LoadBill(dsMaster.Tables[0].Rows[0]["BILLNO"].ToString());
                this.lblBillNo.Text = dsMaster.Tables[0].Rows[0]["BILLNO"].ToString();
            }
            this.gvMain.DataSource = dsMaster.Tables[0];
            this.gvMain.DataBind();
        }

        ViewState["pageIndex"] = pageIndex;
        ViewState["totalCount"] = totalCount;
        ViewState["pageCount"] = pageCount;
        ViewState["filter"] = filter;
        ViewState["OrderByFields"] = OrderByFields;

        ViewState["pageIndex2"] = pageIndex2;
        DetailDataBind();
    }

    void DetailDataBind()
    {
        dsDetail = billDetail.QueryByBillNo(this.lblBillNo.Text, pageIndex2, pageSize2);
        //pager2.RecordCount = billDetail.GetRowCount("BILLNO='" + this.lblBillNo.Text + "'");
        //pager2.CurrentPageIndex = pageIndex2;
        if (dsDetail.Tables[0].Rows.Count == 0)
        {
            this.hdnDetailRowIndex.Value = "0";
            this.lblMsg.Visible = true;
        }
        else
        {
            this.lblMsg.Visible = false;
        }
        this.dgDetail.DataSource = dsDetail.Tables[0];
        this.dgDetail.DataBind();

        dsAllotment = objAllot.QueryAllotment(1, 10000, "BILLNO='" + this.lblBillNo.Text + "'", "BILLNO,ID");
        this.dgAllotment.DataSource = dsAllotment.Tables[0];
        this.dgAllotment.DataBind();
        GridRowSpan(dgAllotment, 0);
    }
    #endregion

    #region 主表GridView绑定
    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
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

            Comparison obj = new Comparison();
            DataSet dsStatus = obj.GetItems("DeliveryBillMaster_STATUS");
            DataRow[] rows = dsStatus.Tables[0].Select("VALUE='" + e.Row.Cells[5].Text + "'");
            if (rows.Length == 1)
            {
                e.Row.Cells[5].Text = rows[0]["TEXT"].ToString();
            }

            if (e.Row.RowIndex == Convert.ToInt32(this.hdnRowIndex.Value))
            {
                e.Row.Cells[0].Text = "<img src=../../images/arrow01.gif />";
            }
            e.Row.Attributes.Add("onclick", string.Format("selectRow('gvMain',{0});", e.Row.RowIndex));
            e.Row.Attributes.Add("style", "cursor:pointer;");
        }
    }
    #endregion

    #region GridView行选择，加载明细
    protected void btnReload_Click(object sender, EventArgs e)
    {
        int i = Convert.ToInt32(this.hdnRowIndex.Value);
        this.lblBillNo.Text = dsMaster.Tables[0].Rows[i]["BILLNO"].ToString();
        LoadBill(this.lblBillNo.Text);
    }
    #endregion

    #region 加载明细
    protected void LoadBill(string BillNo)
    {
        pageIndex2 = 1;
        ViewState["pagerIndex2"] = pageIndex2;
        dsDetail = billDetail.QueryByBillNo(BillNo, pageIndex2, pageSize2);
        //pager2.RecordCount = billDetail.GetRowCount("BILLNO='" + this.lblBillNo.Text + "'");
        this.dgDetail.DataSource = dsDetail.Tables[0];
        this.dgDetail.DataBind();

        if (this.dsDetail.Tables[0].Rows.Count == 0)
        {
            this.lblMsg.Visible = true;
        }
        else
        {
            this.lblMsg.Visible = false;
        }

        dsAllotment = objAllot.QueryAllotment(1, 10000, "BILLNO='" + this.lblBillNo.Text + "'", "BILLNO,ID");
        this.dgAllotment.DataSource = dsAllotment.Tables[0];
        this.dgAllotment.DataBind();
        GridRowSpan(dgAllotment, 0);
        CountTotal();
    }

    protected void CountTotal()
    {
        decimal qty = 0.00M;
        decimal amount = 0.00M;
        foreach (DataRow row in dsDetail.Tables[0].Rows)
        {
            qty += Convert.ToDecimal(row["QUANTITY"]);
            amount += Convert.ToDecimal(row["QUANTITY"]) * Convert.ToDecimal(row["PRICE"]);
        }
    }
    #endregion


    #region 明细数据绑定
    protected void dgDetail_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex % 2 == 0)
                {
                    e.Item.BackColor = System.Drawing.Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Item.BackColor = System.Drawing.Color.FromName(Session["grid_EvenRowColor"].ToString());
                }
                int sn = (pageIndex2 - 1) * pageSize2 + e.Item.ItemIndex + 1;
                e.Item.Cells[0].Text = sn.ToString();
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "数据绑定出错：" + exp.Message);
        }
    }

    protected void dgAllotment_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex % 2 == 0)
                {
                    e.Item.BackColor = System.Drawing.Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Item.BackColor = System.Drawing.Color.FromName(Session["grid_EvenRowColor"].ToString());
                }
                //int sn = (pageIndex3 - 1) * pageSize3 + e.Item.ItemIndex + 1;
                //e.Item.Cells[0].Text = sn.ToString();

                Comparison obj = new Comparison();
                DataSet ds = obj.GetItems("DELIVERYALLOTDETAIL_STATUS");
                DataRow[] rows = ds.Tables[0].Select("VALUE='" + e.Item.Cells[8].Text + "'");
                if (rows.Length == 1)
                {
                    e.Item.Cells[8].Text = rows[0]["TEXT"].ToString();
                }
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "数据绑定出错：" + exp.Message);
        }
    }

    #endregion

    #region 按字段查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            filter = string.Format("{0} like '{1}%'", this.ddl_Field.SelectedValue, this.txtKeyWords.Text.Trim().Replace("'", ""));
            ViewState["filter"] = filter;
            if (rbASC.Checked)
            {
                OrderByFields = this.ddl_Field.SelectedValue + " asc ";
            }
            else
            {
                OrderByFields = this.ddl_Field.SelectedValue + " desc ";
            }

            totalCount = billMaster.GetRowCount(filter);
            pageIndex = 1;
            pager.CurrentPageIndex = 1;
            pager.RecordCount = totalCount;
            this.hdnRowIndex.Value = "0";
            GridDataBind();
            this.lblBillNo.Text = dsMaster.Tables[0].Rows[0]["BILLNO"].ToString();
            LoadBill(this.lblBillNo.Text);
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    #endregion


    # region 分页控件 页码changing事件
    protected void pager_PageChanging(object src, PageChangingEventArgs e)
    {
        pager.CurrentPageIndex = e.NewPageIndex;
        pager.RecordCount = totalCount;
        pageIndex = pager.CurrentPageIndex;
        ViewState["pageIndex"] = pageIndex;
        hdnRowIndex.Value = "0";
        GridDataBind();
        this.lblBillNo.Text = dsMaster.Tables[0].Rows[0]["BILLNO"].ToString();
        LoadBill(this.lblBillNo.Text);
    }

    //protected void pager2_PageChanging(object src, PageChangingEventArgs e)
    //{
    //    pager2.CurrentPageIndex = e.NewPageIndex;
    //    // pager2.RecordCount = totalCount2;
    //    pageIndex2 = pager2.CurrentPageIndex;
    //    ViewState["pageIndex2"] = pageIndex2;
    //    GridDataBind();
    //}
    #endregion

    #region 退出
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../MainPage.aspx");
    }
    #endregion


    protected void btnQuery2_Click(object sender, EventArgs e)
    {
        try
        {
            filter = Session["filter"].ToString();
            totalCount = billMaster.GetRowCount(filter);
            pageIndex = 1;
            pager.CurrentPageIndex = 1;
            pager.RecordCount = totalCount;
            this.hdnRowIndex.Value = "0";
            GridDataBind();
            this.lblBillNo.Text = dsMaster.Tables[0].Rows[0]["BILLNO"].ToString();
            LoadBill(this.lblBillNo.Text);
        }
        catch (Exception exp)
        {
            //JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }

    #region Grid列合并
    protected void GridRowSpan(DataGrid grid, int colIndex)
    {
        if (grid.Items.Count >= 2)
        {
            int start = 0;
            int end = 0;
            for (int i = 1; i < grid.Items.Count; i++)
            {
                if (grid.Items[i].Cells[colIndex].Text == grid.Items[i - 1].Cells[colIndex].Text)
                {
                    end = i;
                    if (i < grid.Items.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        int span = end - start + 1;
                        grid.Items[start].Cells[colIndex].RowSpan = span;
                        for (int k = start + 1; k <= end; k++)
                        {
                            grid.Items[k].Cells[colIndex].Visible = false;
                        }
                    }
                }
                else
                {
                    int span = end - start + 1;
                    grid.Items[start].Cells[colIndex].RowSpan = span;
                    for (int k = start + 1; k <= end; k++)
                    {
                        grid.Items[k].Cells[colIndex].Visible = false;
                    }

                    start = end + 1;
                    end++;
                }
            }
        }
    }
    #endregion

    #region 导出Excel
    //出库单明细导出
    protected void btnDeliveryBillExcel_Click(object sender, EventArgs e)
    {
        try
        {
            filter = "*";
            string tableName = "V_WMS_OUT_BILLDETAIL";
            DataTable dt = billExcel.QueryBillDetail(filter, tableName, this.lblBillNo.Text.ToString().Trim());
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

    protected string Export(DataTable dt)
    {
        XlsDocument xls = new XlsDocument();
        //string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "出库单";
        string fileName = this.lblBillNo.Text.ToString() + "出库单";
        xls.FileName = fileName;
        int rowIndex = 1;
        Worksheet sheet = xls.Workbook.Worksheets.Add("出库单");
        Cells cells = sheet.Cells;
        sheet.Cells.Merge(1, 1, 1, 9);
        Cell cell = cells.Add(1, 1, "出库单");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 1, "单据号");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 2, "产品代码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 3, "产品名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 4, "产品价格");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 5, "入库量");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 6, "实际入库量");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 7, "单位编码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 8, "单位名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 9, "备注");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        foreach (DataRow row in dt.Rows)
        {
            cells.Add(rowIndex + 2, 1, "" + row["BILLNO"] + "");
            cells.Add(rowIndex + 2, 2, "" + row["PRODUCTCODE"] + "");
            cells.Add(rowIndex + 2, 3, "" + row["PRODUCTNAME"] + "");
            cells.Add(rowIndex + 2, 4, "" + row["PRICE"] + "");
            cells.Add(rowIndex + 2, 5, "" + row["QUANTITY"] + "");
            cells.Add(rowIndex + 2, 6, "" + row["OUTPUTQUANTITY"] + "");
            cells.Add(rowIndex + 2, 7, "" + row["UNITCODE"] + "");
            cells.Add(rowIndex + 2, 8, "" + row["UNITNAME"] + "");
            cells.Add(rowIndex + 2, 9, "" + row["MEMO"] + "");
            rowIndex++;
        }

        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        string file = System.Web.HttpContext.Current.Server.MapPath("~/Excel/");
        xls.Save(file);
        //xls.Send();
        return fileName;

    }


    //出库单分配明细导出
    protected void btnAllotExcel_Click(object sender, EventArgs e)
    {
        try
        {
            filter = "DELIVERYALLOTDETAIL_STATUS";
            string tableName = "V_WMS_OUT_ALLOTDETAIL";
            DataTable dt = billExcel.QueryBillAllot(tableName, this.lblBillNo.Text, filter);
            if (dt.Rows.Count == 0)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "没有数据导出");
            }
            else
            {
                string fileName = this.ExportAllot(dt);
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

    protected string ExportAllot(DataTable dt)
    {
        XlsDocument xls = new XlsDocument();
        //string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "出库单";
        string fileName = this.lblBillNo.Text.ToString() + "入库单明细分配";
        xls.FileName = fileName;
        int rowIndex = 1;
        Worksheet sheet = xls.Workbook.Worksheets.Add("入库单明细分配");
        Cells cells = sheet.Cells;
        sheet.Cells.Merge(1, 1, 1, 10);
        Cell cell = cells.Add(1, 1, "" + this.lblBillNo.Text + "" + "入库单明细分配");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 1, "单据号");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 2, "产品代码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 3, "产品名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 4, "货位编码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 5, "货位名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 6, "入库量");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 7, "实际入库量");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 8, "单位编码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 9, "单位名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 10, "状态");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        foreach (DataRow row in dt.Rows)
        {
            cells.Add(rowIndex + 2, 1, "" + row["BILLNO"] + "");
            cells.Add(rowIndex + 2, 2, "" + row["PRODUCTCODE"] + "");
            cells.Add(rowIndex + 2, 3, "" + row["PRODUCTNAME"] + "");
            cells.Add(rowIndex + 2, 4, "" + row["CELLCODE"] + "");
            cells.Add(rowIndex + 2, 5, "" + row["CELLNAME"] + "");
            cells.Add(rowIndex + 2, 6, "" + row["QUANTITY"] + "");
            cells.Add(rowIndex + 2, 7, "" + row["OUTPUTQUANTITY"] + "");
            cells.Add(rowIndex + 2, 8, "" + row["UNITCODE"] + "");
            cells.Add(rowIndex + 2, 9, "" + row["UNITNAME"] + "");
            cells.Add(rowIndex + 2, 10, "" + row["TEXT"] + "");
            rowIndex++;
        }

        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        string file = System.Web.HttpContext.Current.Server.MapPath("~/Excel/");
        xls.Save(file);
        //xls.Send();
        return fileName;
    }


    #endregion
}
