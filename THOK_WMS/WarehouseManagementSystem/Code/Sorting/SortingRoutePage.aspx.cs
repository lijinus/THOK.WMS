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
using THOK.WMS.Download.Bll;
using THOK.WMS;

public partial class Code_StockDelivery_DeliveryBillEditPage : BasePage
{

    int pageIndex = 1; //明细分页
    int pageSize = 15;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "1=1";
    DataSet dsMoveDetail;
    DataSet dsMoveMaster;
    DataSet dsDetail;
    DataSet dsMaster;
    DataSet dsAllotment;
    SortingRouteBll route = new SortingRouteBll();
    SortingOrderBll orderbll = new SortingOrderBll();
    DownRouteBll langRoutebll = new DownRouteBll();
    DownReachBll langReachbll = new DownReachBll();
    DownCustomerBll langCustbll = new DownCustomerBll();
    DownOrgBll orgbll = new DownOrgBll();     
    DownDistBll distbll = new DownDistBll();
    DownOrdDistBll orgdistbll = new DownOrdDistBll();

    #region LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GridDataBind();
            }
            else
            {
                pageCount = Convert.ToInt32(ViewState["pageCount"]);
                pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
                totalCount = Convert.ToInt32(ViewState["totalCount"]);
                filter = ViewState["filter"].ToString();
               
                GridDataBind();
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }
    #endregion

    #region 数据源绑定

    void GridDataBind()
    {
        dsMaster = route.QuerySortingRoute(pageIndex, pageSize, filter,">=");
        if (dsMaster.Tables[0].Rows.Count == 0)
        {
            dsMaster.Tables[0].Rows.Add(dsMaster.Tables[0].NewRow());
            dgDetail.DataSource = dsMaster;
            dgDetail.DataBind();

            int columnCount = dgDetail.Items.Count;
            dgDetail.Items[0].Cells.Clear();
            dgDetail.Items[0].Cells.Add(new TableCell());
            dgDetail.Items[0].Cells[0].ColumnSpan = columnCount;
            dgDetail.Items[0].Cells[0].Text = "没有符合以上条件的数据";
            dgDetail.Items[0].Visible = true;
        }
        else
        {
            if (!IsPostBack)
            {
                // LoadBill(dsMaster.Tables[0].Rows[0]["BILLNO"].ToString());
                //this.lblBillNo.Text = dsMaster.Tables[0].Rows[0]["BILLNO"].ToString();
            }
            //this.gvMain.DataSource = dsMaster.Tables[0];
            //this.gvMain.DataBind();
            dgDetail.DataSource = dsMaster;
            dgDetail.DataBind();
        }
        this.LblQuantity.Text = route.QuerySortingQuantity().Rows[0]["QUANTITY"].ToString()+" 条";
        this.LBlAmount.Text = route.QuerySortingQuantity().Rows[0]["AMOUNT"].ToString() + " 元";
        ViewState["pageIndex"] = pageIndex;
        ViewState["totalCount"] = totalCount;
        ViewState["pageCount"] = pageCount;
        ViewState["filter"] = filter;
    }
    #endregion

    #region 查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            string txtWey = "";
            if (this.ddl_Field.SelectedValue == "ORDER_DATE")
            {
                string startDate = this.txtKeyWords.Text.ToString();
                startDate = startDate.Substring(0, 4) + startDate.Substring(5, 2) + startDate.Substring(8, 2);
                txtWey = startDate;
            }
            else
                txtWey = this.txtKeyWords.Text.ToString();
            filter = string.Format(" A.{0} like '%{1}%'", this.ddl_Field.SelectedValue, txtWey);
            //totalCount = route.GetRowCount(filter,">=");
            pageIndex = 1;
           // pager.CurrentPageIndex = 1;
           // pager.RecordCount = totalCount;
            // this.hdnRowIndex.Value = "1";
            GridDataBind();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "数据查询出错" + exp.Message);
        }
    }
    #endregion

    #region 明细绑定
    protected void dgDetail_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                //HtmlInputCheckBox chk = new HtmlInputCheckBox();
                CheckBox chk = new CheckBox();
                chk.Attributes.Add("style", "text-align:center;word-break:keep-all; white-space:nowrap");
                chk.ID = "checkAll";
                chk.Attributes.Add("onclick", "checkboxChange(this,'dgDetail',0);");
                //chk.Text = "操作";
                //e.Item.Cells[0].Controls.Add(chk);
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex % 2 == 0)
                {
                    e.Item.BackColor =System.Drawing.Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Item.BackColor =System.Drawing.Color.FromName(Session["grid_EvenRowColor"].ToString());
                }
                e.Item.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f5f5f5',this.style.fontWeight=''; this.style.cursor='hand';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

               // CheckBox chk = new CheckBox();
               // chk.AutoPostBack = true;
               // e.Item.Cells[0].Attributes.Add("onclick", "javascript:document.getElementById('lblQuantitySum').text = this.QUANTITY.TEXT;");
               // e.Item.Cells[0].Controls.Add(chk);   
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "数据绑定出错：" + exp.Message);
        }
    }
    #endregion

    # region 分页控件 页码changing事件
    //protected void pager_PageChanging(object src, PageChangingEventArgs e)
    //{
    //    pager.CurrentPageIndex = e.NewPageIndex;
    //    pager.RecordCount = totalCount;
    //    pageIndex = pager.CurrentPageIndex;
    //    ViewState["pageIndex"] = pageIndex;
    //    GridDataBind();
    //}
    #endregion

    #region 确认分配
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if (txtSortingCode.Text.Trim() != "")
        {
            try
            {
                //HtmlInputCheckBox chk = new HtmlInputCheckBox();
                CheckBox chk = new CheckBox();
                string RouteCode = "";
                bool hasSelected = false;
                string orderdate = "";
                if (dgDetail.Items.Count > 0)
                {
                    for (int i = 0; i < dgDetail.Items.Count; i++)
                    {
                        chk = (CheckBox)dgDetail.Items[i].Cells[0].FindControl("chkExport");
                        if (dgDetail.Items[i].Cells[8].Text.ToString().Equals("已分配"))
                            break;
                        if (chk.Checked)
                        {
                            RouteCode += dgDetail.Items[i].Cells[1].Text.ToString() + ",";
                            orderdate += dgDetail.Items[i].Cells[4].Text.ToString() + ",";
                            hasSelected = true;
                        }
                    }
                    if (hasSelected)
                    {
                        string SortingCode = this.txtSortingCode.Text.Trim();//分拣线编号
                        //获取线路id
                        RouteCode = RouteCode.Substring(0, RouteCode.Length - 1);
                        string RouteCodeId = UtinString.StringMake(RouteCode);
                        //获取时间
                        orderdate = orderdate.Substring(0, orderdate.Length - 1);
                        string orderDateCode = UtinString.StringMake(orderdate);

                        string strErr = orderbll.UpdateOrderSoringGroupInfo(SortingCode, RouteCodeId,orderDateCode);//修改线路表，主表，明细表确认

                        if (strErr == "true")
                            strErr = "线路分配已经完成！";
                        else
                            strErr = "线路分配失败！原因：" + strErr;
                        JScript.Instance.ShowMessage(this, strErr);
                        GridDataBind();
                    }
                    else
                    {
                        JScript.Instance.ShowMessage(this, "请选择要分配的单据,或选择的单据已分配！");
                    }
                }
                else
                {
                    JScript.Instance.ShowMessage(this, "无数据可以分配！");
                }
            }
            catch (Exception exp)
            {
                JScript.Instance.ShowMessage(this, exp.Message);
            }
        }
        else
        {
            JScript.Instance.ShowMessage(this, "分拣线名称不能为空！");
        }
    }

    #endregion

    #region 退出
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../MainPage.aspx");

    }
    #endregion

    #region 取消分配
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            //HtmlInputCheckBox chk = new HtmlInputCheckBox();
            CheckBox chk = new CheckBox();
            string RouteCode = "";
            string orderdate = "";
            bool hasSelected = false;
            if (dgDetail.Items.Count > 0)
            {
                for (int i = 0; i < dgDetail.Items.Count; i++)
                {
                    chk = (CheckBox)dgDetail.Items[i].Cells[0].FindControl("chkExport");
                    if (chk.Checked)
                    {
                        RouteCode += dgDetail.Items[i].Cells[1].Text.ToString() + ",";
                        orderdate += dgDetail.Items[i].Cells[4].Text.ToString() + ",";
                        hasSelected = true;
                    }
                }
                if (hasSelected)
                {
                    RouteCode = RouteCode.Substring(0, RouteCode.Length - 1);
                    string SortingCode = this.txtSortingCode.Text.Trim();
                    RouteCode = UtinString.StringMake(RouteCode);

                    //获取时间
                    orderdate = orderdate.Substring(0, orderdate.Length - 1);
                    string orderDateCode = UtinString.StringMake(orderdate);

                    string strErr = orderbll.CancelAllotOrder(RouteCode,orderDateCode);//取消分配线路表，主表，明细表确认

                    if (strErr == "true")
                        strErr = "线路取消分配已经完成！";
                    else
                        strErr = "线路取消分配失败！原因：" + strErr;
                    JScript.Instance.ShowMessage(this, strErr);
                    GridDataBind();
                }
                else
                {
                    JScript.Instance.ShowMessage(this, "请选择要取消分配的单据,或选择的单据未分配！");
                }
            }
            else
            {
                JScript.Instance.ShowMessage(this, "无数据可以分配！");
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }
    #endregion

    #region 送货线路和送货区域下载

    protected void btnDown_Click(object sender, EventArgs e)
    {
        try
        {
            string strRoute = "";
            string strReach = "";
            string strCust = "";
            string strOrg = "";
            //orgdistbll.DownOrgDistBillInfo();
            bool langroute = langRoutebll.DownRouteInfo();
            if (langroute == false)
                strRoute = "无送货线路数据可下载！请联系营销系统人员！";
            bool langreach = langReachbll.DownReachInfo();
            if (langreach == false)
                strReach = "无送货区域数据可下载！请联系营销系统人员！";
            bool langCust = langCustbll.DownCustomerInfo();
            if (langCust == false)
                strCust = "无新客户信息下载！";
            bool langOrg = orgbll.DownOrgInfo();
            if (langOrg == false)
                strOrg = "无单位数据可下载！请联系营销系统人员";
            distbll.DownDistInfo();            
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "数据下载出错,原因：" + exp.Message);
        }
        JScript.Instance.ShowMessage(this, "送货线路、送货区域和客户信息数据下载完成！");
        GridDataBind();
    }
    #endregion

    #region 导出execl数据
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = route.QuerySortingRoute(pageIndex, pageSize, filter, ">=").Tables[0];
            //if (this.txtAreaName.Text.ToString().Trim() == "")
            //{
            //    dt = stockBll.RealTimeStock();
            //}
            //else
            //{
            //    filter = "AREANAME like '" + this.txtAreaName.Text.ToString().Trim() + "%'";
            //    dt = stockBll.Query(filter);
            //}

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
        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "线路分配";
        xls.FileName = fileName;
        int rowIndex = 1;
        Worksheet sheet = xls.Workbook.Worksheets.Add("线路分配");
        Cells cells = sheet.Cells;
        sheet.Cells.Merge(1, 1, 1, 2);
        Cell cell = cells.Add(1, 1, "实时库存");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 1, "送货线路编码");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 2, "送货线路名称");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 3, "订单日期");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 4, "订单明细");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 5, "总数量");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        cell = cells.Add(2, 6, "总金额");
        cell.Font.Bold = true;
        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        foreach (DataRow row in dt.Rows)
        {
            cells.Add(rowIndex + 2, 1, "" + row["DELIVER_LINE_CODE"] + "");
            cells.Add(rowIndex + 2, 2, "" + row["DELIVER_LINE_NAME"] + "");
            cells.Add(rowIndex + 2, 3, "" + row["ORDER_DATE"] + "");
            cells.Add(rowIndex + 2, 4, "" + row["ORDERQUANTITY"] + "");
            cells.Add(rowIndex + 2, 5, "" + row["QUANTITY"] + "");
            cells.Add(rowIndex + 2, 6, "" + row["AMOUNT"] + "");

            rowIndex++;
        }

        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        string file = System.Web.HttpContext.Current.Server.MapPath("~/Excel/");
        xls.Save(file);
        //xls.Send();
        return fileName;

    }
    #endregion

    #region 测试
    //选中单据累计数量
    protected void chkExport_CheckedChanged(object sender, EventArgs e)
    {
        decimal quantity = 0.00M;
        if (dgDetail.Items.Count > 0)
        {
            bool hasSelected = false;
            CheckBox chk = new CheckBox();
            for (int i = 0; i < dgDetail.Items.Count; i++)
            {
                chk = (CheckBox)dgDetail.Items[i].Cells[0].FindControl("chkExport");
                if (chk.Checked)
                {
                    quantity += Convert.ToDecimal((dgDetail.Items[i].Cells[6].Text));
                    hasSelected = true;
                }
            }
            if (hasSelected)
            {
                this.lblQuantitySum.Text = quantity + "";
            }
            else
                this.lblQuantitySum.Text = "";
        }
    }

    //查询未分配的线路
    protected void btnWei_Click(object sender, EventArgs e)
    {
        dsMaster = route.QuerySortingRoute(pageIndex, pageSize, filter,"=");
        if (dsMaster.Tables[0].Rows.Count == 0)
        {
            dsMaster.Tables[0].Rows.Add(dsMaster.Tables[0].NewRow());
            dgDetail.DataSource = dsMaster;
            dgDetail.DataBind();

            int columnCount = dgDetail.Items.Count;
            dgDetail.Items[0].Cells.Clear();
            dgDetail.Items[0].Cells.Add(new TableCell());
            dgDetail.Items[0].Cells[0].ColumnSpan = columnCount;
            dgDetail.Items[0].Cells[0].Text = "没有符合以上条件的数据";
            dgDetail.Items[0].Visible = true;

        }
        else
        {
            if (!IsPostBack)
            {
                // LoadBill(dsMaster.Tables[0].Rows[0]["BILLNO"].ToString());
                //this.lblBillNo.Text = dsMaster.Tables[0].Rows[0]["BILLNO"].ToString();
            }
            //this.gvMain.DataSource = dsMaster.Tables[0];
            //this.gvMain.DataBind();
            dgDetail.DataSource = dsMaster;
            dgDetail.DataBind();
        }
        //this.lblQuantitySum.Text = "0.00";
        this.LblQuantity.Text = route.QuerySortingQuantity().Rows[0]["QUANTITY"].ToString() + " 条";
        this.LBlAmount.Text = route.QuerySortingQuantity().Rows[0]["AMOUNT"].ToString() + " 元";
        int count = route.GetRowCount(filter, "=");
        CheckBox chk = new CheckBox();
        for (int i = 0; i < count; i++)
        {
            chk = (CheckBox)dgDetail.Items[i].Cells[0].FindControl("chkExport");
            chk.AutoPostBack = false;
        }
    }

    //查询已分配的线路
    protected void btnYi_Click(object sender, EventArgs e)
    {
        dsMaster = route.QuerySortingRoute(pageIndex, pageSize, filter,">");
        if (dsMaster.Tables[0].Rows.Count == 0)
        {
            dsMaster.Tables[0].Rows.Add(dsMaster.Tables[0].NewRow());
            dgDetail.DataSource = dsMaster;
            dgDetail.DataBind();

            int columnCount = dgDetail.Items.Count;
            dgDetail.Items[0].Cells.Clear();
            dgDetail.Items[0].Cells.Add(new TableCell());
            dgDetail.Items[0].Cells[0].ColumnSpan = columnCount;
            dgDetail.Items[0].Cells[0].Text = "没有符合以上条件的数据";
            dgDetail.Items[0].Visible = true;

        }
        else
        {
            if (!IsPostBack)
            {
                // LoadBill(dsMaster.Tables[0].Rows[0]["BILLNO"].ToString());
                //this.lblBillNo.Text = dsMaster.Tables[0].Rows[0]["BILLNO"].ToString();
            }
            //this.gvMain.DataSource = dsMaster.Tables[0];
            //this.gvMain.DataBind();
            dgDetail.DataSource = dsMaster;
            dgDetail.DataBind();
        }
        //this.lblQuantitySum.Text = "0.00";
        this.LblQuantity.Text = route.QuerySortingQuantity().Rows[0]["QUANTITY"].ToString() + " 条";
        this.LBlAmount.Text = route.QuerySortingQuantity().Rows[0]["AMOUNT"].ToString() + " 元";
        this.btnCreate.Visible = false;
        this.BtnClose.Visible = false;
       // this.BtnDelete.Visible = true;
        int count = route.GetRowCount(filter,">");
        CheckBox chk = new CheckBox();
        for (int i = 0; i < dgDetail.Items.Count; i++)
        {
            chk = (CheckBox)dgDetail.Items[i].Cells[0].FindControl("chkExport");
            chk.AutoPostBack = false;
        }
        
    }

    //取消分配,有BUG 不能使用
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = new CheckBox();
            string RouteCode = "";
            string orderdate = "";
            bool hasSelected = false;
            if (dgDetail.Items.Count > 0)
            {
                for (int i = 0; i < dgDetail.Items.Count; i++)
                {
                    //if (dgDetail.Items[i].Cells[8].Text.ToString().Equals("未分配"))
                    //    continue;
                    chk = (CheckBox)dgDetail.Items[i].Cells[0].FindControl("chkExport");
                    if (chk.Checked)
                    {
                        RouteCode += dgDetail.Items[i].Cells[1].Text.ToString() + ",";
                        orderdate += dgDetail.Items[i].Cells[4].Text.ToString() + ",";
                        hasSelected = true;
                    }
                }
                if (hasSelected)
                {
                    RouteCode = RouteCode.Substring(0, RouteCode.Length - 1);
                    string SortingCode = this.txtSortingCode.Text.Trim();
                    RouteCode = UtinString.StringMake(RouteCode);

                    //获取时间
                    orderdate = orderdate.Substring(0, orderdate.Length - 1);
                    string orderDateCode = UtinString.StringMake(orderdate);

                    string strErr = orderbll.CancelAllotOrder(RouteCode, orderDateCode);//取消分配线路表，主表，明细表确认

                    if (strErr == "true")
                        strErr = "线路取消分配已经完成！";
                    else
                        strErr = "线路取消分配失败！原因：" + strErr;
                    JScript.Instance.ShowMessage(this, strErr);
                    this.btnYi_Click(null, null);
                }
                else
                {
                    JScript.Instance.ShowMessage(this, "请选择要取消分配的单据,或选择的单据未分配！");
                }
            }
            else
            {
                JScript.Instance.ShowMessage(this, "无数据可以分配！");
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }
    #endregion

    //选中进行汇总查询，并打印
    protected void btnCollect_Click(object sender, EventArgs e)
    {
        //string fileinfo = "";
        //try
        //{
        //    CheckBox chk = new CheckBox();
        //    string RouteCode = "";
        //    bool hasSelected = false;
        //    string orderdate = "";
        //    if (dgDetail.Items.Count > 0)
        //    {
        //        for (int i = 0; i < dgDetail.Items.Count; i++)
        //        {
        //            chk = (CheckBox)dgDetail.Items[i].Cells[0].FindControl("chkExport");
        //            if (chk.Checked)
        //            {
        //                RouteCode += dgDetail.Items[i].Cells[1].Text.ToString() + ",";
        //                orderdate += dgDetail.Items[i].Cells[4].Text.ToString() + ",";
        //                hasSelected = true;
        //            }
        //        }
        //        if (hasSelected)
        //        {
        //            //获取线路id
        //            RouteCode = RouteCode.Substring(0, RouteCode.Length - 1);
        //            string RouteCodeId = LangChaoUtinString.StringMake(RouteCode);
        //            //获取时间
        //            orderdate = orderdate.Substring(0, orderdate.Length - 1);
        //            string orderDateCode = LangChaoUtinString.StringMake(orderdate);

        //            fileinfo = string.Format("DELIVER_LINE_CODE IN({0}) AND ORDER_DATE IN({1})", RouteCodeId, orderDateCode);
        //            Session["routefile"] = fileinfo;
        //            Response.Redirect("SortingRouteDetailPage.aspx");
        //        }
        //        else
        //        {
        //            JScript.Instance.ShowMessage(this, "请选择单据！");
        //        }
        //    }
        //    else
        //    {
        //        JScript.Instance.ShowMessage(this, "无数据可以选择！");
        //    }
        //}
        //catch (Exception exp)
        //{
        //    JScript.Instance.ShowMessage(this, exp.Message);
        //}


        Response.Redirect("SortingRouteDetailPage.aspx");
    }
}
