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
using THOK.WMS;

public partial class Code_Statistic_EnterAndDeliveryBillQuery : System.Web.UI.Page
{
    int pageIndex = 1;
    int pageSize = 5;
    int totalCount = 0;
    int pageCount = 0;
    StockBLL objStock = new StockBLL();
    DataSet dsMaster;
    DataSet dsDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        dsMaster = objStock.QueryMaster(pageIndex, pageSize);
        GridDataBind(dsMaster);
    }

    protected void GridDataBind(DataSet dsMaster)
    {
        //dsMaster = objStock.QueryMaster(pageIndex, pageSize);
        if (dsMaster.Tables[0].Rows.Count == 0)
        {
            dsMaster.Tables[0].Rows.Add(dsMaster.Tables[0].NewRow());
            gvMain.DataSource = dsMaster;
            gvMain.DataBind();
            int columnCount = gvMain.Rows[0].Cells.Count;
            gvMain.Rows[0].Cells.Add(new TableCell());
            gvMain.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvMain.Rows[0].Cells[0].Text = "没有符合以上条件的数据";
            gvMain.Rows[0].Visible = true;
        }
        else
        {
            if(!IsPostBack)
            {
                this.lblBillNo.Text=dsMaster.Tables[0].Rows[0]["BILLNO"].ToString();
                LoadBill(dsMaster.Tables[0].Rows[0]["BILLNO"].ToString());

            }
            gvMain.DataSource = dsMaster;
            gvMain.DataBind();
        }
        ViewState["pageIndex"] = pageIndex;
        ViewState["totalCount"] = totalCount;
        ViewState["pageCount"] = pageCount;
        //ViewState["filter"] = filter;
        //ViewState["OrderByFields"] = OrderByFields;

        //ViewState["pageIndex2"] = pageIndex2;
        DetailDataBind();

       
        //    this.gvMain.DataSource = dsMaster.Tables[0];
        //    this.gvMain.DataBind();
        //}

        //ViewState["pageIndex"] = pageIndex;
        //ViewState["totalCount"] = totalCount;
        //ViewState["pageCount"] = pageCount;
        //ViewState["filter"] = filter;
        //ViewState["OrderByFields"] = OrderByFields;

        //ViewState["pageIndex2"] = pageIndex2;
        //DetailDataBind();
    }

    void DetailDataBind()
    {
        try
        {
            dsDetail = objStock.QueryDetail(pageIndex, pageSize, this.lblBillNo.Text);
        //    pager2.RecordCount = billDetail.GetRowCount("BILLNO='" + this.lblBillNo.Text + "'");
        //    pager2.CurrentPageIndex = pageIndex2;
        //    if (dsDetail.Tables[0].Rows.Count == 0)
        //    {
        //        this.hdnDetailRowIndex.Value = "0";
        //        this.lblMsg.Visible = true;
        //    }
        //    else
        //    {
        //        this.lblMsg.Visible = false;
        //    }
            this.dgDetail.DataSource = dsDetail.Tables[0];
            this.dgDetail.DataBind();

        //    dsAllotment = objAllot.QueryAllotment(1, 10000, "BILLNO='" + this.lblBillNo.Text + "'", "BILLNO,ID");
        //    this.dgAllotment.DataSource = dsAllotment.Tables[0];
        //    this.dgAllotment.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

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
            DataSet dsStatus = obj.GetItems("ENTRYBILLMASTER_STATUS");
            DataRow[] rows = dsStatus.Tables[0].Select("VALUE='" + e.Row.Cells[4].Text + "'");
            if (rows.Length == 1)
            {
                e.Row.Cells[4].Text = rows[0]["TEXT"].ToString();
            }

            //if (e.Row.RowIndex == Convert.ToInt32(this.hdnRowIndex.Value))
            //{
            //    e.Row.Cells[0].Text = "<img src=../../images/arrow01.gif />";
            //}
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
        //pageIndex2 = 1;
        //ViewState["pagerIndex2"] = pageIndex2;
        dsDetail = objStock.QueryDetail(pageIndex, pageSize, this.lblBillNo.Text);
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

        //dsAllotment = objAllot.QueryAllotment(1, 10000, "BILLNO='" + this.lblBillNo.Text + "'", "BILLNO,ID");
        //this.dgAllotment.DataSource = dsAllotment.Tables[0];
        //this.dgAllotment.DataBind();
        //CountTotal();
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
        //try
        //{
        //    if (e.Item.ItemType == ListItemType.Header)
        //    {

        //    }
        //    else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        if (e.Item.ItemIndex % 2 == 0)
        //        {
        //            e.Item.BackColor = System.Drawing.Color.FromName(Session["grid_OddRowColor"].ToString());
        //        }
        //        else
        //        {
        //            e.Item.BackColor = System.Drawing.Color.FromName(Session["grid_EvenRowColor"].ToString());
        //        }
        //        int sn = (pageIndex2 - 1) * pageSize2 + e.Item.ItemIndex + 1;
        //        e.Item.Cells[0].Text = sn.ToString();
        //    }
        //}
        //catch (Exception exp)
        //{
        //    JScript.Instance.ShowMessage(this, "数据绑定出错：" + exp.Message);
        //}
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
                int sn = e.Item.ItemIndex + 1;
                e.Item.Cells[0].Text = sn.ToString();

                //Comparison obj = new Comparison();
                //DataSet ds = obj.GetItems("ENTRYALLOT_STATUS");
                //DataRow[] rows = ds.Tables[0].Select("VALUE='" + e.Item.Cells[8].Text + "'");
                //if (rows.Length == 1)
                //{
                //    e.Item.Cells[8].Text = rows[0]["TEXT"].ToString();
                //}
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "数据绑定出错：" + exp.Message);
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
        //GridDataBind();
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

    protected void btnQuery_Click(object sender, EventArgs e)
    {

    }
}
