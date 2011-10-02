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

public partial class Code_StorageManagement_CheckBillEditPage :BasePage
{
    CheckBillMaster billMaster = new CheckBillMaster();
    CheckBillDetail billDetail = new CheckBillDetail();
    DataSet dsDetail;
    DataSet dsMaster;
    int pageIndex = 1; //明细分页
    int pageSize = 10;
    int totalCount = 0;

    #region LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pager.PageSize = pageSize;
                if (Request.QueryString["BILLNO"] != null)
                {
                    LoadBill(Request.QueryString["BILLNO"]);
                    this.hdnOpFlag.Value = "1";
                }
                else
                {
                    this.lblMsg.Visible = true;
                    this.txtBillNo.Text = billMaster.GetNewBillNo();
                    this.txtOperatePerson.Text = Session["EmployeeCode"].ToString();
                    this.txtEmployeeName.Text = Session["EmployeeName"].ToString();
                    this.txtBillDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    Warehouse objHouse = new Warehouse();
                    DataSet dsHouse = objHouse.QueryAllWarehouse();
                    DataRow[] rows = dsHouse.Tables[0].Select("WH_TYPE='0'");
                    if (rows.Length == 1)
                    {
                        this.txtWHCODE.Text = rows[0]["WH_CODE"].ToString();
                        this.txtWHNAME.Text = rows[0]["WH_NAME"].ToString();
                    }
                    LoadDetail();
                }
                ViewState["pageIndex"] = pageIndex;
                ViewState["totalCount"] = totalCount;
            }
            else
            {
                pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
                totalCount = Convert.ToInt32(ViewState["totalCount"]);
                LoadDetail();
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }
    #endregion

    #region 加载单据汇总及明细
    protected void LoadBill(string BillNo)
    {

        dsMaster = billMaster.QueryByBillNo(BillNo);
        if (dsMaster.Tables[0].Rows.Count == 1)
        {
            this.txtBillNo.Text = dsMaster.Tables[0].Rows[0]["BILLNO"].ToString();
            this.txtBillDate.Text = Convert.ToDateTime(dsMaster.Tables[0].Rows[0]["BILLDATE"].ToString()).ToString("yyyy-MM-dd");
            this.txtMemo.Text = dsMaster.Tables[0].Rows[0]["MEMO"].ToString();
            this.ddlBillState.SelectedValue = dsMaster.Tables[0].Rows[0]["STATUS"].ToString();
            if (dsMaster.Tables[0].Rows[0]["VALIDATEDATE"].ToString() != "")
            {
                this.txtValidateDate.Text = Convert.ToDateTime(dsMaster.Tables[0].Rows[0]["VALIDATEDATE"].ToString()).ToString("yyyy-MM-dd");
            }
            this.txtValidatePerson.Text = dsMaster.Tables[0].Rows[0]["VALIDATEPERSON"].ToString();
            this.txtBillTypeCode.Text = dsMaster.Tables[0].Rows[0]["TYPECODE"].ToString();
            this.txtBillTypeName.Text = dsMaster.Tables[0].Rows[0]["BILLTYPE"].ToString();
            this.txtOperatePerson.Text = dsMaster.Tables[0].Rows[0]["OPERATORCODE"].ToString();
            this.txtEmployeeName.Text = dsMaster.Tables[0].Rows[0]["OPERATEPERSON"].ToString();
            this.txtWHCODE.Text = dsMaster.Tables[0].Rows[0]["WH_CODE"].ToString();
            this.txtWHNAME.Text = dsMaster.Tables[0].Rows[0]["WH_NAME"].ToString();
            this.lblUnitCode.Text = dsMaster.Tables[0].Rows[0]["DEFAULTUNIT"].ToString();
            this.lblUnitName.Text = dsMaster.Tables[0].Rows[0]["UNITNAME"].ToString();
        }
        LoadDetail();
    }

    protected void LoadDetail()
    {
        string BillNo = this.txtBillNo.Text;
        dsDetail = billDetail.QueryByBillNo(BillNo, pageIndex, pageSize);
        totalCount = billDetail.GetRowCount("BILLNO='" + this.txtBillNo.Text + "'");
        pager.RecordCount = totalCount;
        this.dgDetail.DataSource = dsDetail.Tables[0];
        this.dgDetail.DataBind();
        if (dsDetail.Tables[0].Rows.Count == 0)
        {
            this.lblMsg.Visible = true;
        }
        else
        {
            this.lblMsg.Visible = false;
        }
        CountTotal();
    }
    #endregion

    #region 计算总量与金额
    protected void CountTotal()
    {
        decimal qty = 0.00M;
        decimal diff = 0.00M;
        foreach (DataRow row in dsDetail.Tables[0].Rows)
        {
            qty += Convert.ToDecimal(row["RECORDQUANTITY"]);
            diff += Convert.ToDecimal(row["DIFF_QTY"]);
        }
        this.txtTotalQty.Text = qty.ToString();
        this.txtTotalDiffQty.Text = diff.ToString();
    }
    #endregion

    #region 保存汇总
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtBillTypeCode.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择盘点单据类型");
                return;
            }
            if (this.txtWHCODE.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择盘点的仓库");
                return;
            }
            if (this.hdnOpFlag.Value == "0")
            {
                billMaster.BILLNO = this.txtBillNo.Text;
                billMaster.BILLDATE = Convert.ToDateTime(this.txtBillDate.Text);
                billMaster.BILLTYPE = this.txtBillTypeCode.Text;
                billMaster.OPERATEPERSON = Session["EmployeeCode"].ToString();
                billMaster.STATUS = "1";
                billMaster.WH_CODE = this.txtWHCODE.Text;
                billMaster.MEMO = this.txtMemo.Text.Trim().Replace("\'", "\''");
                billMaster.Insert();
                this.hdnOpFlag.Value = "1";
                this.ddlBillState.SelectedValue = "1";
                JScript.Instance.ShowMessage(this, "盘点单汇总信息保存成功");
            }
            else
            {
                billMaster.BILLNO = this.txtBillNo.Text;
                billMaster.BILLDATE = Convert.ToDateTime(this.txtBillDate.Text);
                billMaster.BILLTYPE = this.txtBillTypeCode.Text;
                billMaster.OPERATEPERSON = this.txtOperatePerson.Text;
                billMaster.STATUS = "1";
                billMaster.WH_CODE = this.txtWHCODE.Text;
                billMaster.MEMO = this.txtMemo.Text.Trim().Replace("\'", "\''");
                billMaster.Update();
                JScript.Instance.ShowMessage(this, "盘点单汇总信息修改成功");
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
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
                CheckBox chk = new CheckBox();
                chk.Attributes.Add("style", "text-align:center;word-break:keep-all; white-space:nowrap");
                chk.ID = "checkAll";
                chk.Attributes.Add("onclick", "checkboxChange(this,'dgDetail',0);");
                //chk.Text = "选择";
                e.Item.Cells[0].Controls.Add(chk);
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
                e.Item.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f5f5f5',this.style.fontWeight=''; this.style.cursor='hand';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CheckBox chk = new CheckBox();
                Label lblEdit = new Label();
                e.Item.Cells[0].Controls.Add(chk);



                TextBox tb1 = new TextBox();
                tb1.ID = "cellcode";//货位编号
                tb1.Text = e.Item.Cells[2].Text.Replace("&nbsp;", "");
                tb1.Attributes.Add("style", "width:85px;");
                tb1.Attributes.Add("class", "GridInputAlignLeft");
                e.Item.Cells[2].Controls.Add(tb1);
                Button btn1 = new Button();
                btn1.CssClass = "ButtonBrowse2";
                btn1.OnClientClick = string.Format("SelectCell('{0},{1},{2},{3},{4},{5},{6}')"
                                                  , tb1.ClientID
                                                  , tb1.ClientID.Replace("code", "name")
                                                  , tb1.ClientID.Replace("cellcode", "") + "cellproductcode"
                                                  , tb1.ClientID.Replace("cellcode", "") + "cellproductname"
                                                  , tb1.ClientID.Replace("cellcode", "") + "cellunitcode"
                                                  , tb1.ClientID.Replace("cellcode", "") + "cellunitname"
                                                  , tb1.ClientID.Replace("cellcode", "") + "recordquantity");
                e.Item.Cells[2].Controls.Add(btn1);

                TextBox tb2 = new TextBox();
                tb2.ID = "cellname";//
                tb2.Text = e.Item.Cells[3].Text.Replace("&nbsp;", "");
                tb2.Attributes.Add("style", "width:70px;");
                tb2.Attributes.Add("class", "GridInput");
                tb2.Attributes.Add("onfocus", "CannotEdit(this)");
                e.Item.Cells[3].Controls.Add(tb2);


                TextBox tb3 = new TextBox();
                tb3.ID = "cellproductcode";
                tb3.Text = e.Item.Cells[4].Text.Replace("&nbsp;", "");
                tb3.Attributes.Add("style", "width:60px;");
                tb3.Attributes.Add("class", "GridInput");
                tb3.Attributes.Add("onfocus", "CannotEdit(this)");
                e.Item.Cells[4].Controls.Add(tb3);

                TextBox tb4 = new TextBox();
                tb4.ID = "cellproductname";
                tb4.Text = e.Item.Cells[5].Text.Replace("&nbsp;", "");
                tb4.Attributes.Add("style", "width:99%;");
                tb4.Attributes.Add("class", "GridInput");
                tb4.Attributes.Add("onfocus", "CannotEdit(this)");
                e.Item.Cells[5].Controls.Add(tb4);

                TextBox tb5 = new TextBox();
                tb5.ID = "cellunitcode";
                tb5.Text = e.Item.Cells[6].Text.Replace("&nbsp;", "");
                tb5.Attributes.Add("style", "width:50px;");
                tb5.Attributes.Add("class", "GridInput");
                tb5.Attributes.Add("onfocus", "CannotEdit(this)");
                e.Item.Cells[6].Controls.Add(tb5);

                TextBox tb6 = new TextBox();
                tb6.ID = "cellunitname";
                tb6.Text = e.Item.Cells[7].Text.Replace("&nbsp;", "");
                tb6.Attributes.Add("style", "width:50px;");
                tb6.Attributes.Add("class", "GridInput");
                tb6.Attributes.Add("onfocus", "CannotEdit(this)");
                e.Item.Cells[7].Controls.Add(tb6);

                TextBox tb7 = new TextBox();
                tb7.ID = "recordquantity";
                tb7.Text = e.Item.Cells[8].Text;
                tb7.Attributes.Add("style", "width:50px; text-align:right;");
                tb7.Attributes.Add("class", "GridInput");
                //tb7.Attributes.Add("onblur", "IsNumber(this,'数量')");
                tb7.Attributes.Add("onfocus", "CannotEdit(this)");
                e.Item.Cells[8].Controls.Add(tb7);

            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "数据绑定出错：" + exp.Message);
        }
    }
    #endregion

    #region 保存明细 删除明细
    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgDetail.Items.Count; i++)
        {
            billDetail.ID = Convert.ToInt32(dsDetail.Tables[0].Rows[i]["ID"]);
            billDetail.BILLNO = this.txtBillNo.Text;
            billDetail.CELLCODE = ((TextBox)(dgDetail.Items[i].Cells[2].Controls[0])).Text;
            billDetail.PRODUCTCODE = ((TextBox)(dgDetail.Items[i].Cells[4].Controls[0])).Text;
            billDetail.UNITCODE = ((TextBox)(dgDetail.Items[i].Cells[6].Controls[0])).Text;
            billDetail.RECORDQUANTITY = Convert.ToDecimal(((TextBox)(dgDetail.Items[i].Cells[8].Controls[0])).Text);
            billDetail.COUNTQUANTITY = billDetail.RECORDQUANTITY;
            billDetail.STATUS = "0";
            billDetail.Update();
            JScript.Instance.ShowMessage(this, "明细保存成功");
        }
        LoadDetail();
    }


    protected void btnDeleteDetail_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < dgDetail.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)dgDetail.Items[i].Cells[0].Controls[0];
                if (chk.Enabled && chk.Checked)
                {
                    dsDetail.Tables[0].Rows[i].Delete();
                }
            }
            billDetail.Delete(dsDetail);
            LoadBill(this.txtBillNo.Text);
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }
    #endregion

    #region 新增盘点单
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("CheckBillEditPage.aspx");
    }
    #endregion

    # region 分页控件 页码changing事件
    protected void pager_PageChanging(object src, PageChangingEventArgs e)
    {
        pager.CurrentPageIndex = e.NewPageIndex;
        pager.RecordCount = totalCount;
        pageIndex = pager.CurrentPageIndex;
        ViewState["pageIndex"] = pageIndex;
        LoadBill(this.txtBillNo.Text);
    }
    #endregion

    //#region 审核 反向审核
    //protected void btnValidate_Click(object sender, EventArgs e)
    //{
    //    if (this.hdnOpFlag.Value == "0")
    //    {
    //        JScript.Instance.ShowMessage(this, "入库单据未录入");
    //        return;
    //    }
    //    billMaster.Validate(this.txtBillNo.Text, Session["EmployeeCode"].ToString());
    //    this.ddlBillState.SelectedValue = "2";
    //    this.btnCreateDetail.Enabled = false;
    //    this.btnDeleteDetail.Enabled = false;
    //    this.btnSaveDetail.Enabled = false;
    //    this.btnSave.Enabled = false;
    //    this.btnValidate.Enabled = false;
    //    this.btnReverseValidate.Enabled = true;
    //    JScript.Instance.ShowMessage(this, "审核成功");
    //}
    //protected void btnReverseValidate_Click(object sender, EventArgs e)
    //{
    //    billMaster.Rev_Validate(this.txtBillNo.Text);
    //    this.ddlBillState.SelectedValue = "1";
    //    this.btnCreateDetail.Enabled = true;
    //    this.btnDeleteDetail.Enabled = true;
    //    this.btnSaveDetail.Enabled = true;
    //    this.btnSave.Enabled = true;
    //    this.txtValidateDate.Text = "";
    //    this.txtValidatePerson.Text = "";

    //    this.btnValidate.Enabled = true;
    //    this.btnReverseValidate.Enabled = false;
    //    JScript.Instance.ShowMessage(this, "审核已经取消");
    //}
    //#endregion

    #region 返回
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CheckBillPage.aspx?back=true");
    }
    #endregion

    protected void btnCreateDetail_Click(object sender, EventArgs e)
    {
        if (this.hdnOpFlag.Value == "0")
        {
            if (this.txtBillTypeCode.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择盘点单据类型");
                return;
            }
            if (this.txtWHCODE.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择盘点的仓库");
                return;
            }
            billMaster.BILLNO = this.txtBillNo.Text;
            billMaster.BILLDATE = Convert.ToDateTime(this.txtBillDate.Text);
            billMaster.BILLTYPE = this.txtBillTypeCode.Text;
            billMaster.OPERATEPERSON = Session["EmployeeCode"].ToString();
            billMaster.STATUS = "1";
            billMaster.WH_CODE = this.txtWHCODE.Text;
            billMaster.MEMO = this.txtMemo.Text.Trim().Replace("\'", "\''");
            billMaster.Insert();
            this.hdnOpFlag.Value = "1";
            this.ddlBillState.SelectedValue = "1";
        }

        billDetail.BILLNO = this.txtBillNo.Text;
        billDetail.CELLCODE = "";
        billDetail.PRODUCTCODE = "";
        billDetail.RECORDQUANTITY= 0.00M;
        billDetail.STATUS = "0";
        billDetail.UNITCODE = "";
        billDetail.Insert();

        LoadDetail();
    }
}
