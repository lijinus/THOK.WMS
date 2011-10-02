using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using THOK.WMS;
using THOK.WMS.BLL;
using THOK.WMS.Download.Bll;

public partial class Code_StockEntry_EntryBillDownManual : BasePage
{
    int pageIndex = 1;
    int pageSize = 5;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "IS_IMPORT='0'";
    string PrimaryKey = "ID";
    string OrderByFields = "BILLNO desc";
    int pageIndex2 = 1;
    int pageSize2 = 5;
    string queryStartDate = "";
    string queryEndDate = "";
    DownInBillBll inbill = new DownInBillBll();
    DownProductBll bll = new DownProductBll();
    DataTable masterdt = new DataTable();
    DataTable detaildt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //pager.PageSize = pageSize;
                //pager2.PageSize = pageSize2;
               //totalCount = inbill.GetInBillMasterCount();
                //pager.RecordCount = totalCount;
                BindInBillMaster();
              //btnQuery_Click(null, null);
            }
            else
            {
                //pageCount = Convert.ToInt32(ViewState["pageCount"]);
                //pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
                //totalCount = Convert.ToInt32(ViewState["totalCount"]);
                //filter = ViewState["filter"].ToString();
                //OrderByFields = ViewState["OrderByFields"].ToString();
                //totalCount = inbill.GetInBillMasterCount();
                //pageIndex2 = Convert.ToInt32(ViewState["pageIndex2"]);

                //pager.RecordCount = totalCount;
                BindInBillMaster();
               // btnQuery_Click(null, null);
            }
        }

        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }

    public void BindInBillMaster()
    {
        masterdt = inbill.GetInBillMaster(pageIndex, pageSize);
        if (masterdt.Rows.Count == 0)
        {
            masterdt.Rows.Add(masterdt.NewRow());
            gvMaster.DataSource = masterdt;
            gvMaster.DataBind();
            int columnCount = gvMaster.Rows[0].Cells.Count;
            gvMaster.Rows[0].Cells.Clear();
            gvMaster.Rows[0].Cells.Add(new TableCell());
            gvMaster.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvMaster.Rows[0].Cells[0].Text = "暂时无未下载数据数据";
            gvMaster.Rows[0].Visible = true;
        }
        else
        {
            if (!IsPostBack)
            {
                //this.LoadBill(masterdt.Rows[0]["BILLNO"].ToString());
                this.lblBillNo.Text = masterdt.Rows[0]["BILLNO"].ToString();
            }
            this.lblBillNo.Text = masterdt.Rows[0]["BILLNO"].ToString();
            gvMaster.DataSource = masterdt;
            gvMaster.DataBind();
        }

        ViewState["pageIndex"] = pageIndex;
        ViewState["totalCount"] = totalCount;
        ViewState["pageCount"] = pageCount;
        ViewState["filter"] = filter;
        ViewState["OrderByFields"] = OrderByFields;
        ViewState["pageIndex2"] = pageIndex2;
        BindInBillDetail();
        
    }

    public void BindInBillDetail()
    {
        detaildt = inbill.GetInBillDetail(PrimaryKey, pageIndex2, pageSize2, OrderByFields, this.lblBillNo.Text);
        if (detaildt.Rows.Count == 0)
        {
            this.hdnDetailRowIndex.Value = "0";
            this.lblMsg.Visible = true;
        }
        else
        {
            this.lblMsg.Visible = false;
        }
        this.dgDetail.DataSource = detaildt;
        this.dgDetail.DataBind();
    }

    #region 绑定主表
    
    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = new CheckBox();
            chk.Attributes.Add("style", "text-align:center;word-break:keep-all; white-space:nowrap");
            chk.ID = "checkAll";
            chk.Attributes.Add("onclick", "checkboxChange(this,'gvMaster',1);");
            chk.Text = "操作";
            e.Row.Cells[1].Controls.Add(chk);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("style", "background-color:#f8f8f8;");
            CheckBox chk = new CheckBox();
            e.Row.Cells[1].Controls.Add(chk);
            
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

            if (e.Row.RowIndex == Convert.ToInt32(this.hdnRowIndex.Value))
            {
                e.Row.Cells[0].Text = "<img src=../../images/arrow01.gif />";
            }
            e.Row.Attributes.Add("onclick", string.Format("selectRow('gvMaster',{0});", e.Row.RowIndex));
            e.Row.Attributes.Add("style", "cursor:pointer;");
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

    #region GridView行选择，加载明细

    protected void btnReload_Click(object sender, EventArgs e)
    {
        int i = Convert.ToInt32(this.hdnRowIndex.Value);
        //this.lblBillNo.Text = dsMaster.Tables[0].Rows[i]["BILLNO"].ToString();
        this.lblBillNo.Text = masterdt.Rows[i]["BILLNO"].ToString();
        LoadBill(this.lblBillNo.Text);
    }
    #endregion

    #region 加载明细
    protected void LoadBill(string billno)
    {
        pageIndex2 = 1;
        ViewState["pagerIndex2"] = pageIndex2;
        DataTable detailtable = new DataTable();
        detaildt = inbill.GetInBillDetail(PrimaryKey, pageIndex2, pageSize2, OrderByFields, this.lblBillNo.Text);
        //detailtable = inbill.InBillDetail(billno);
        //pager2.RecordCount = inbill.GetInBillDetailCount(this.lblBillNo.Text);
        this.dgDetail.DataSource = detaildt;
        this.dgDetail.DataBind();

        if (detaildt.Rows.Count == 0)
        {
            this.lblMsg.Visible = true;
        }
        else
        {
            this.lblMsg.Visible = false;
        }
        //CountTotal();
    }

    //protected void CountTotal()
    //{
    //    decimal qty = 0.00M;
    //    decimal amount = 0.00M;
    //    foreach (DataRow row in dsDetail.Tables[0].Rows)
    //    {
    //        qty += Convert.ToDecimal(row["QUANTITY"]);
    //        amount += Convert.ToDecimal(row["QUANTITY"]) * Convert.ToDecimal(row["PRICE"]);
    //    }
    //}
    #endregion

    #region 下载选中的单据

    protected void btnDown_Click(object sender, EventArgs e)
    {
        CheckBox chk = new CheckBox();
        string billnolist = "";
        bool hasSelected = false;
        if (gvMaster.Rows.Count >= 1)
        {
            for (int i = 0; i < gvMaster.Rows.Count; i++)
            {
                chk = (CheckBox)gvMaster.Rows[i].Cells[1].Controls[0];
                if (chk.Checked)
                {
                    billnolist += gvMaster.Rows[i].Cells[2].Text.ToString() + ",";
                    hasSelected = true;
                }
            }
            if (hasSelected)
            {
                bll.DownProductInfo();//下载产品
                billnolist = billnolist.Substring(0, billnolist.Length - 1);
                string inbillno = UtinString.StringMake(billnolist);
                inbill.GetInBillManual(inbillno, Session["EmployeeCode"].ToString());
                JScript.Instance.ShowMessage(this.UpdatePanel1, "已选择的单据下载完成！");
            }
            else
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请选择要下载的单据！");
            }
        }
        else
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, "没有数据可以下载！");
        }
    }
    #endregion

    #region 下载一定日期内单据

    protected void btnSelectDown_Click(object sender, EventArgs e)
    {
        try
        {
            string startDate = this.txtStartDate.Text.Trim();
            string endDate = this.txtEndDate.Text.Trim();
            if (startDate == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请输入开始日期！");
                return;
            }
            if (endDate == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请输入结束日期！");
                return;
            }
            bll.DownProductInfo();//下载产品信息
            endDate = Convert.ToDateTime(endDate).AddDays(1).Date.ToString("yyyy-MM-dd");
            startDate = startDate.Substring(0, 4) + startDate.Substring(5, 2) + startDate.Substring(8, 2);
            endDate = endDate.Substring(0, 4) + endDate.Substring(5, 2) + endDate.Substring(8, 2);
            bool tag = inbill.GetInBill(startDate, endDate, Session["EmployeeCode"].ToString());
            if (!tag)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "选择的日期内，没有单据可以下载！");
            }
            else
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "下载完成！");
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
        
    }
    #endregion

    #region 查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            queryStartDate = this.txtStartDate.Text.Trim();
            queryEndDate = this.txtEndDate.Text.Trim();
            if (queryStartDate == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请输入开始日期！");
                return;
            }
            if (queryEndDate == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请输入结束日期！");
                return;
            }
            DateTime tiem = Convert.ToDateTime(queryEndDate).AddDays(1);
            queryEndDate = tiem.Date.ToString("yyyy-MM-dd");
            this.BindInBillMaster();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EntryBillPage.aspx?back=true");       
    }
}
