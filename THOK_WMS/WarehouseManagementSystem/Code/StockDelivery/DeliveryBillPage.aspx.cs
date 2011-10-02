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
using THOK.WMS.Upload.Bll;
using THOK.WMS.Download.Bll;
using THOK.WMS;

public partial class Code_StockDelivery_DeliveryBillPage : BasePage
{
    int pageIndex = 1;
    int pageSize = 8;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "(STATUS='1' or STATUS='0')";
    string PrimaryKey = "ID";
    string OrderByFields = "BILLNO desc";
    DeliveryBillMaster billMaster = new DeliveryBillMaster();
    DeliveryBillDetail billDetail = new DeliveryBillDetail();
    //DownOutBillBLL outbill = new DownOutBillBLL();
    DownOutBillBll downbll = new DownOutBillBll();
    UpdateUploadBll updateBll = new UpdateUploadBll();
    DataSet dsDetail;
    DataSet dsMaster;
    int pageIndex2 = 1;
    int pageSize2 = 10;
    //int totalCount2 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pager.PageSize = pageSize;
               // pager2.PageSize = pageSize2;
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
       // pager2.RecordCount = billDetail.GetRowCount("BILLNO='" + this.lblBillNo.Text + "'");
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
    }
    #endregion

    #region 主表GridView绑定
    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = new CheckBox();
            chk.Attributes.Add("style", "text-align:center;word-break:keep-all; white-space:nowrap");
            chk.ID = "checkAll";
            chk.Attributes.Add("onclick", "checkboxChange(this,'gvMain',1);");
            chk.Text = "操作";
            e.Row.Cells[1].Controls.Add(chk);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("style", "background-color:#f8f8f8;");
            CheckBox chk = new CheckBox();
            Label lblEdit = new Label();
            e.Row.Cells[1].Controls.Add(chk);
            LinkButton lnkBtn = new LinkButton();
            lnkBtn.Attributes.Add("style", " text-align:center;word-break:keep-all; white-space:nowrap");
            lnkBtn.Text = "编辑";
            lnkBtn.OnClientClick = string.Format("javascript:window.open('DeliveryBillEditPage.aspx?BILLNO={0}&time={1}','_self');return false;", e.Row.Cells[2].Text, System.DateTime.Now);
            e.Row.Cells[1].Controls.Add(lnkBtn);


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

    #region 点击新增
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeliveryBillEditPage.aspx");
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
                    e.Item.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Item.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
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

    #endregion

    #region 按字段查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            filter = string.Format("(STATUS='1' or STATUS='0') and {0} like '{1}%'", this.ddl_Field.SelectedValue, this.txtKeyWords.Text.Trim().Replace("'", ""));
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
            this.hdnRowIndex.Value = "1";
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

    #region 删除单据
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string billid = "";
            if (gvMain.Rows[0].Cells[0].Text == "没有符合以上条件的数据")
            {
                
            }
            else
            {
                for (int i = 0; i < gvMain.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvMain.Rows[i].Cells[1].Controls[0];
                    if (chk.Enabled && chk.Checked)
                    {
                        if (dsMaster.Tables[0].Rows[i]["STATUS"].ToString() == "2")
                        {
                            JScript.Instance.ShowMessage(this.UpdatePanel1, "已审核的入库单不能删除");
                            return;
                        }
                        billid += dsMaster.Tables[0].Rows[i]["BILLNO"].ToString() + ",";
                        dsMaster.Tables[0].Rows[i].Delete();
                    }
                }
                billMaster.Delete(dsMaster);
                billid = billid.Substring(0, billid.Length - 1);
                string billidList = UtinString.StringMake(billid);
                //outbill.DeleteOutStoreBill(billidList);//
                updateBll.deleteOutBill(billidList);
                totalCount = billMaster.GetRowCount(filter);
                pager.RecordCount = totalCount;
                if (pageIndex > pager.PageCount)
                {
                    pageIndex = pager.PageCount;
                }
                GridDataBind();
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    #endregion

    //# region 分页控件 页码changing事件
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
    //#endregion

    #region 自动下载
    
    //protected void btnAutoDown_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //DownBillTypeBLL billtype = new DownBillTypeBLL();
    //        //DownCustomerBLL customer = new DownCustomerBLL();
    //        //DownSupplierBLL supplier = new DownSupplierBLL();
    //        //DownUnitBLL unitbll = new DownUnitBLL();
    //        //DownProductBLL product = new DownProductBLL();
    //        //DownProductClassBLL productclass = new DownProductClassBLL();
             
    //        //billtype.GetBillTypeInfo();
    //        //customer.GetCustomerInfo();
    //        //supplier.GetSupplierInfo();
    //        //unitbll.GetUnitInfo();
    //        //product.GetProductInfo();
    //        //productclass.GetProductClassInfo();
                     
    //        //outbill.GetOutBillInfoAuto();
    //        bool tag = outbill.DownOutBillInfoAuto();
    //        if (!tag)
    //        {
    //            JScript.Instance.ShowMessage(this.UpdatePanel1, "没有单据可以下载！");
    //        }
    //        else
    //        {
    //            JScript.Instance.ShowMessage(this.UpdatePanel1, "自动下载出库单完成！");
    //        }
    //    }
    //    catch (Exception exp)
    //    {
    //        JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
    //    }
    //}
    #endregion

    #region 手动下载

    protected void btnManualDown_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeliveryBillDownManual.aspx");
    }
    #endregion

    //分拣线下载
    protected void btnUnite_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeliveryBillDownUnite.aspx");
    }

    //合单
    protected void btnUniteBill_Click(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = new CheckBox();
            int selectedNum = 0;
            string billNoList = "";
            string billNo = billMaster.GetNewBillNo();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            bool flag = false;
            decimal quantity = 0.00M;
            for (int i = 0; i < gvMain.Rows.Count; i++)
            {
                chk = (CheckBox)gvMain.Rows[i].Cells[1].Controls[0];
                if (chk.Checked)
                {
                    billNoList += gvMain.Rows[i].Cells[2].Text.ToString() + ",";
                    quantity += Convert.ToDecimal(gvMain.Rows[i].Cells[6].Text.ToString());
                    selectedNum++;
                }
            }
            if (selectedNum > 1)
            {
                try
                {
                    billNoList = UtinString.StringMake(billNoList);
                    downbll.GetByOutBill(billNo, date, Session["EmployeeCode"].ToString(), billNoList);//插入数仓表
                    downbll.zyGetOutTable(billNo, date, Session["EmployeeCode"].ToString(), billNoList);//插入中烟表
                    downbll.DeleteOutBillInfo();//清除3天之前合单过且没有作业的数据
                    JScript.Instance.ShowMessage(this.UpdatePanel1, "合单完成！");
                    GridDataBind();
                }
                catch (Exception exp)
                {
                    JScript.Instance.ShowMessage(this.UpdatePanel1, "合单出错！原因：" + exp.Message);
                }
            }
            else
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "合单必须选择两个或两个以上的单据！");
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
}
