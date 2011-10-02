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
using THOK.WMS.Upload.Bll;

public partial class Code_StockDelivery_DeliveryAllotmentConfirmPage : BasePage
{
    int pageIndex = 1;
    int pageSize = 5;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "(STATUS='3' or STATUS='4' or STATUS='5')";
    string PrimaryKey = "ID";
    string OrderByFields = "BILLNO desc";
    DeliveryBillMaster billMaster = new DeliveryBillMaster();
    DeliveryBillDetail billDetail = new DeliveryBillDetail();
    DeliveryAllot objAllot = new DeliveryAllot();
    MoveBillMaster billMoveMaster = new MoveBillMaster();
    MoveBillDetail billMoveDetail = new MoveBillDetail();
    UpdateUploadBll updateBll = new UpdateUploadBll();
    DataSet dsMoveDetail;
    DataSet dsMoveMaster;
    DataSet dsDetail;
    DataSet dsMaster;
    DataSet dsAllotment;
    int pageIndex2 = 1;
    int pageSize2 = 5;
    int pageIndex3 = 1;
    int pageSize3 = 5;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BillType objType = new BillType();
                DataSet dsTemp = objType.QueryBillType(1, 100, "BUSINESS='2'", "TYPECODE");
                DataRow newRow = dsTemp.Tables[0].NewRow();
                newRow["TYPECODE"] = "2";
                newRow["TYPENAME"] = "所有出库单";
                dsTemp.Tables[0].Rows.InsertAt(newRow, 0);
                this.ddlBillType.DataSource = dsTemp.Tables[0].DefaultView;
                this.ddlBillType.DataTextField = "TYPENAME";
                this.ddlBillType.DataValueField = "TYPECODE";
                this.ddlBillType.DataBind();
                pager.PageSize = pageSize;
                pager2.PageSize = pageSize2;
                pager3.PageSize = pageSize3;
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
                // LoadBill(dsMaster.Tables[0].Rows[0]["BILLNO"].ToString());
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
        ViewState["pageIndex3"] = pageIndex3;
        DetailDataBind();
    }

    void DetailDataBind()
    {
        dsDetail = billDetail.QueryByBillNo(this.lblBillNo.Text, pageIndex2, pageSize2);
        pager2.RecordCount = billDetail.GetRowCount("BILLNO='" + this.lblBillNo.Text + "'");
        pager2.CurrentPageIndex = pageIndex2;
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

        dsAllotment = objAllot.QueryAllotment(pageIndex3, pageSize3, "BILLNO='" + this.lblBillNo.Text + "'", "BILLNO,ID");
        pager3.RecordCount = objAllot.GetRowCount("BILLNO='" + this.lblBillNo.Text + "'");
        pager3.CurrentPageIndex = pageIndex3;
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
            CheckBox chk = new CheckBox();
            chk.Attributes.Add("style", "text-align:center;word-break:keep-all; white-space:nowrap");
            chk.ID = "checkAll";
            chk.Attributes.Add("onclick", "checkboxChange(this,'gvMain',1);");
            chk.Text = "选择";
            e.Row.Cells[1].Controls.Add(chk);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = new CheckBox();
            Label lblEdit = new Label();
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

            Comparison obj = new Comparison();
            DataSet dsStatus = obj.GetItems("DeliveryBillMaster_STATUS");
            DataRow[] rows = dsStatus.Tables[0].Select("VALUE='" + e.Row.Cells[7].Text + "'");
            if (rows.Length == 1)
            {
                e.Row.Cells[7].Text = rows[0]["TEXT"].ToString();
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
        pager2.RecordCount = billDetail.GetRowCount("BILLNO='" + this.lblBillNo.Text + "'");
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

        dsAllotment = objAllot.QueryAllotment(pageIndex3, pageSize3, "BILLNO='" + this.lblBillNo.Text + "'", "BILLNO,ID");
        pager3.RecordCount = objAllot.GetRowCount("BILLNO='" + this.lblBillNo.Text + "'");
        pager3.CurrentPageIndex = pageIndex3;
        this.dgAllotment.DataSource = dsAllotment.Tables[0];
        this.dgAllotment.DataBind();
        GridRowSpan(dgAllotment,0);
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
                    e.Item.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Item.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
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

    #region 查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            string start = "1900-01-01";
            string end = System.DateTime.Now.ToString("yyyy-MM-dd");
            if (this.txtStartDate.Text.Trim().Length > 0)
            {
                start = this.txtStartDate.Text.Trim();
            }
            if (this.txtEndDate.Text.Trim().Length > 0)
            {
                end = this.txtEndDate.Text.Trim();
            }
            filter = string.Format("(STATUS='3' or STATUS='4' or STATUS='5') AND BILLDATE BETWEEN '{0}' AND '{1}' AND BILLNO LIKE '{2}%' AND MEMO LIKE '%{3}%' AND TYPECODE LIKE '{4}%'"
                                      , start, end, this.txtBillNo.Text.Trim(), this.txtMemo.Text.Trim(), ddlBillType.SelectedValue);
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

    protected void pager2_PageChanging(object src, PageChangingEventArgs e)
    {
        pager2.CurrentPageIndex = e.NewPageIndex;
        // pager2.RecordCount = totalCount2;
        pageIndex2 = pager2.CurrentPageIndex;
        ViewState["pageIndex2"] = pageIndex2;
        GridDataBind();
    }

    protected void pager3_PageChanging(object src, PageChangingEventArgs e)
    {
        pager3.CurrentPageIndex = e.NewPageIndex;
        // pager2.RecordCount = totalCount2;
        pageIndex3 = pager3.CurrentPageIndex;
        ViewState["pageIndex3"] = pageIndex3;
        GridDataBind();
    }
    #endregion

    #region 退出
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../MainPage.aspx");
    }
    #endregion

    #region 确认分配  取消确认
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvMain.Rows[0].Cells[0].Text != "没有符合以上条件的数据")
            {
                for (int i = 0; i < gvMain.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvMain.Rows[i].Cells[1].Controls[0];
                    if (chk.Enabled && chk.Checked)
                    {
                        if (dsMaster.Tables[0].Rows[i]["STATUS"].ToString() == "5")
                        {
                            JScript.Instance.ShowMessage(this.UpdatePanel1, "执行作业的出库单不能再进行分配确认");
                            return;
                        }
                        billMaster.ConfirmAllotment(dsMaster.Tables[0].Rows[i]["BILLNO"].ToString(), Session["EmployeeCode"].ToString());
                        updateBll.outConfirmAudot(Session["EmployeeCode"].ToString(), dsMaster.Tables[0].Rows[i]["BILLNO"].ToString());
                    }
                }
                GridDataBind();
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    protected void btnReverseConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvMain.Rows[0].Cells[0].Text != "没有符合以上条件的数据")
            {
                for (int i = 0; i < gvMain.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvMain.Rows[i].Cells[1].Controls[0];
                    if (chk.Enabled && chk.Checked)
                    {
                        if (dsMaster.Tables[0].Rows[i]["STATUS"].ToString() == "5")
                        {
                            JScript.Instance.ShowMessage(this.UpdatePanel1, "执行作业的出库单不能取消确认");
                            return;
                        }
                        billMaster.Rev_ConAllotment(dsMaster.Tables[0].Rows[i]["BILLNO"].ToString());
                        updateBll.outCancelAudot(dsMaster.Tables[0].Rows[i]["BILLNO"].ToString());
                    }
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

    #region 取消分配（删除分配结果）
    protected void btnCancelAllotment_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvMain.Rows[0].Cells[0].Text != "没有符合以上条件的数据")
            {
                for (int i = 0; i < gvMain.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvMain.Rows[i].Cells[1].Controls[0];
                    if (chk.Enabled && chk.Checked)
                    {
                        if (dsMaster.Tables[0].Rows[i]["STATUS"].ToString() == "4")
                        {
                            JScript.Instance.ShowMessage(this.UpdatePanel1, "分配已经确认的出库单不能取消分配");
                            return;
                        }
                        else if (dsMaster.Tables[0].Rows[i]["STATUS"].ToString() == "5")
                        {
                            JScript.Instance.ShowMessage(this.UpdatePanel1, "执行作业的出库单不能取消分配");
                            return;
                        }
                        else if (dsMaster.Tables[0].Rows[i]["STATUS"].ToString() == "6")
                        {
                            JScript.Instance.ShowMessage(this.UpdatePanel1, "已完成的出库单不能取消分配");
                            return;
                        }

                        if (dsMaster.Tables[0].Rows[i]["STATUS"].ToString() == "3")
                        {
                            dsMoveMaster = billMoveMaster.QueryByBillNo(dsMaster.Tables[0].Rows[i]["BILLNO"].ToString() + "M");
                            for (int j = 0; j < dsMoveMaster.Tables[0].Rows.Count; j++)//获取到与此出库单同时产生的移库单，删除之
                            {
                                if (dsMoveMaster.Tables[0].Rows[j]["STATUS"].ToString() == "2")
                                {
                                    JScript.Instance.ShowMessage(this.UpdatePanel1, "已审核的移位单不能删除");
                                    return;
                                }
                                else
                                {
                                    dsMoveMaster.Tables[0].Rows[j].Delete();
                                    billMoveMaster.Delete(dsMoveMaster);
                                }
                            }
                            objAllot.DeleteAllotment(dsMaster.Tables[0].Rows[i]["BILLNO"].ToString());
                            updateBll.outDeleteAudot(dsMaster.Tables[0].Rows[i]["BILLNO"].ToString());
                        }

                    }
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
}
