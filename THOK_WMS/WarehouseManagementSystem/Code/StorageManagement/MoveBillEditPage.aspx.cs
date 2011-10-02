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

public partial class Code_StorageManagement_MoveBillEditPage :BasePage
{
    MoveBillMaster billMaster = new MoveBillMaster();
    MoveBillDetail billDetail = new MoveBillDetail();
    EntryAllot objAllot = new EntryAllot();
    DataSet dsDetail;
    DataSet dsMaster;
    int pageIndex = 1; //明细分页
    int pageSize = 10;
    int totalCount = 0;
    WarehouseCell warecell = new WarehouseCell();
    #region LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            warecell.UpdateCell();
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
        foreach (DataRow row in dsDetail.Tables[0].Rows)
        {
            qty += Convert.ToDecimal(row["QUANTITY"]);
        }
        this.txtTotalQty.Text = qty.ToString();
        //this.txtTotalAmount.Text = amount.ToString();
    }
    #endregion

    #region 保存汇总
    protected void btnSave_Click(object sender, EventArgs e)    
    {
        try
        {
            if (this.txtBillTypeCode.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择移位单据类型");
                return;
            }
            if (this.txtWHCODE.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择移位的仓库");
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
                JScript.Instance.ShowMessage(this, "移位单汇总信息保存成功");
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
                JScript.Instance.ShowMessage(this, "移位单汇总信息修改成功");
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
                    DataTable cc = billDetail.QueryAllCell().Tables[0];//已经选过的货位排除掉
                    string IN_CELLCODE = "''";
                    string OUT_CELLCODE = "''";

                    for (int i = 0; i < cc.Rows.Count; i++)
                    {
                        IN_CELLCODE += ",'" + cc.Rows[i][1] + "'";
                        OUT_CELLCODE += ",'" + cc.Rows[i][0] + "'";
                    }

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
                    tb1.ID = "out_cellcode";//货位编号（出）
                    tb1.Text = e.Item.Cells[2].Text.Replace("&nbsp;", "");
                    tb1.Attributes.Add("style", "width:80px;");
                    tb1.Attributes.Add("class", "GridInputAlignLeft");
                    e.Item.Cells[2].Controls.Add(tb1);
                    Button btn1 = new Button();
                    btn1.CssClass = "ButtonBrowse2";
                    btn1.OnClientClick = string.Format("SelectCell2('{0},{1},{2},{3},{4},{5},{6}','','QUANTITY>0 AND CELLCODE NOT IN ({7})')"
                                                      , tb1.ClientID
                                                      , tb1.ClientID.Replace("code", "name")
                                                      , tb1.ClientID.Replace("out_cellcode", "") + "cellproductcode"
                                                      , tb1.ClientID.Replace("out_cellcode", "") + "cellproductname"
                                                      , tb1.ClientID.Replace("out_cellcode", "") + "cellunitcode"
                                                      , tb1.ClientID.Replace("out_cellcode", "") + "cellunitname"
                                                      , tb1.ClientID.Replace("out_cellcode", "") + "cellquantity"
                                                      , OUT_CELLCODE.Replace("'"[0], '"'));
                    e.Item.Cells[2].Controls.Add(btn1);

                    TextBox tb2 = new TextBox();
                    tb2.ID = "out_cellname";//货位名称（出）
                    tb2.Text = e.Item.Cells[3].Text.Replace("&nbsp;", "");
                    tb2.Attributes.Add("style", "width:70px;");
                    tb2.Attributes.Add("class", "GridInput");
                    tb2.Attributes.Add("onfocus", "CannotEdit(this)");
                    e.Item.Cells[3].Controls.Add(tb2);


                    TextBox tb5 = new TextBox();
                    tb5.ID = "cellproductcode";
                    tb5.Text = e.Item.Cells[6].Text.Replace("&nbsp;", "");
                    tb5.Attributes.Add("style", "width:70px;");
                    tb5.Attributes.Add("class", "GridInput");
                    tb5.Attributes.Add("onfocus", "CannotEdit(this)");
                    e.Item.Cells[6].Controls.Add(tb5);

                    TextBox tb6 = new TextBox();
                    tb6.ID = "cellproductname";
                    tb6.Text = e.Item.Cells[7].Text.Replace("&nbsp;", "");
                    tb6.Attributes.Add("style", "width:99%;");
                    tb6.Attributes.Add("class", "GridInput");
                    tb6.Attributes.Add("onfocus", "CannotEdit(this)");
                    e.Item.Cells[7].Controls.Add(tb6);

                    TextBox tb7 = new TextBox();
                    tb7.ID = "cellunitcode";
                    tb7.Text = e.Item.Cells[8].Text.Replace("&nbsp;", "");
                    tb7.Attributes.Add("style", "width:60px;");
                    tb7.Attributes.Add("class", "GridInput");
                    tb7.Attributes.Add("onfocus", "CannotEdit(this)");
                    e.Item.Cells[8].Controls.Add(tb7);

                    TextBox tb8 = new TextBox();
                    tb8.ID = "cellunitname";
                    tb8.Text = e.Item.Cells[9].Text.Replace("&nbsp;", "");
                    tb8.Attributes.Add("style", "width:60px;");
                    tb8.Attributes.Add("class", "GridInput");
                    tb8.Attributes.Add("onfocus", "CannotEdit(this)");
                    e.Item.Cells[9].Controls.Add(tb8);

                    TextBox tb9 = new TextBox();
                    tb9.ID = "cellquantity";
                    tb9.Text = e.Item.Cells[10].Text;
                    tb9.Attributes.Add("style", "width:60px;");
                    tb9.Attributes.Add("class", "GridInputAlignRight");
                    tb9.Attributes.Add("onblur", "IsNumber(this,'数量')");
                    e.Item.Cells[10].Controls.Add(tb9);


                    TextBox tb3 = new TextBox();
                    tb3.ID = "in_cellcode";//货位编号（入）
                    tb3.Text = e.Item.Cells[4].Text.Replace("&nbsp;", "");
                    tb3.Attributes.Add("style", "width:80px;");
                    tb3.Attributes.Add("class", "GridInputAlignLeft");
                    e.Item.Cells[4].Controls.Add(tb3);
                    Button btn2 = new Button();
                    btn2.CssClass = "ButtonBrowse2";
                    string max_quantity = "30";
                    if (objAllot.QueryJian2(tb5.Text.ToString()).Tables[0].Rows.Count != 0)
                    {
                        max_quantity = objAllot.QueryJian2(tb5.Text.ToString()).Tables[0].Rows[0][1].ToString();
                        string product = "'" + tb5.Text.ToString()+"'";
                        btn2.OnClientClick = string.Format("SelectCell2('{0},{1}','((QUANTITY< " + max_quantity + " AND CURRENTPRODUCT={3}) OR QUANTITY=0) AND ISACTIVE=1 AND CELLCODE NOT IN ({2}) ')"
                                                      , tb3.ClientID
                                                      , tb3.ClientID.Replace("code", "name"), IN_CELLCODE.Replace("'"[0], '"'),product.Replace("'"[0],'"'));
                    }
                    else
                    {
                        btn2.OnClientClick = string.Format("SelectCell2('{0},{1}','QUANTITY=0 AND ISACTIVE=1 AND CELLCODE NOT IN ({2})')"
                                                          , tb3.ClientID
                                                          , tb3.ClientID.Replace("code", "name"), IN_CELLCODE.Replace("'"[0],'"'));
                    }
                    e.Item.Cells[4].Controls.Add(btn2);

                    TextBox tb4 = new TextBox();
                    tb4.ID = "in_cellname";//货位名称（入）
                    tb4.Text = e.Item.Cells[5].Text.Replace("&nbsp;", "");
                    tb4.Attributes.Add("style", "width:70px;");
                    tb4.Attributes.Add("class", "GridInput");
                    tb4.Attributes.Add("onfocus", "CannotEdit(this)");
                    e.Item.Cells[5].Controls.Add(tb4);

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
            billDetail.OUT_CELLCODE = ((TextBox)(dgDetail.Items[i].Cells[2].Controls[0])).Text;
            billDetail.IN_CELLCODE = ((TextBox)(dgDetail.Items[i].Cells[4].Controls[0])).Text;
            billDetail.PRODUCTCODE =((TextBox)(dgDetail.Items[i].Cells[6].Controls[0])).Text;
            billDetail.UNITCODE = ((TextBox)(dgDetail.Items[i].Cells[8].Controls[0])).Text;
            billDetail.QUANTITY = Convert.ToDecimal(((TextBox)(dgDetail.Items[i].Cells[10].Controls[0])).Text);
            billDetail.STATUS = "0";
            billDetail.Update();
            if (objAllot.QueryJian2(billDetail.PRODUCTCODE.ToString()).Tables[0].Rows.Count > 0)
            {
                string MaxPiece = objAllot.QueryJian2(billDetail.PRODUCTCODE.ToString()).Tables[0].Rows[0][1].ToString();
                objAllot.UpdateCell(MaxPiece, billDetail.UNITCODE.ToString(), billDetail.IN_CELLCODE.ToString());
            }
        }
        LoadDetail();
        JScript.Instance.ShowMessage(this, "明细保存成功");
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

    #region 新增移位单
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("MoveBillEditPage.aspx");
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

    #region 审核 反向审核
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        billMaster.Validate(this.txtBillNo.Text, Session["EmployeeCode"].ToString());
        this.ddlBillState.SelectedValue = "2";
        this.btnSave.Enabled = false;
        this.btnCreateDetail.Enabled = false;
        this.btnSaveDetail.Enabled = false;
        this.btnDeleteDetail.Enabled = false;
        this.btnValidate.Enabled = false;
        this.btnReverseValidate.Enabled = true;
        JScript.Instance.ShowMessage(this, "审核成功");
    }
    protected void btnReverseValidate_Click(object sender, EventArgs e)
    {
        billMaster.Rev_Validate(this.txtBillNo.Text);
        this.ddlBillState.SelectedValue = "1";
        this.btnSave.Enabled = true;
        this.btnCreateDetail.Enabled = true;
        this.btnSaveDetail.Enabled = true;
        this.btnDeleteDetail.Enabled = true;
        this.btnValidate.Enabled = true;
        this.btnReverseValidate.Enabled = false;
        JScript.Instance.ShowMessage(this, "审核已经取消");
    }
    #endregion

    #region 返回
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MoveBillPage.aspx?back=true");
    }
    #endregion

    protected void btnCreateDetail_Click(object sender, EventArgs e)
    {
        if (this.hdnOpFlag.Value == "0")
        {
            if (this.txtBillTypeCode.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择移位单据类型");
                return;
            }
            if (this.txtWHCODE.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择移位的仓库");
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
        billDetail.OUT_CELLCODE = "";
        billDetail.IN_CELLCODE = "";
        billDetail.PRODUCTCODE = "";
        billDetail.QUANTITY = 0.00M;
        billDetail.STATUS = "0";
        billDetail.UNITCODE = "";
        billDetail.Insert();

        LoadDetail();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if(this.hdnOpFlag.Value=="1")
        {
            Response.Redirect("MoveBillChartPage.aspx?BILLNO="+this.txtBillNo.Text.Trim());
        }
        else
        {
            Response.Redirect("MoveBillChartPage.aspx");
        }
        
    }
    protected void dgDetail_PreRender(object sender, EventArgs e)
    {
        for (int i = 0; i < dgDetail.Items.Count; i++)
        {
            billDetail.ID = Convert.ToInt32(dsDetail.Tables[0].Rows[i]["ID"]);
            billDetail.BILLNO = this.txtBillNo.Text;
            billDetail.OUT_CELLCODE = ((TextBox)(dgDetail.Items[i].Cells[2].Controls[0])).Text;
            billDetail.IN_CELLCODE = ((TextBox)(dgDetail.Items[i].Cells[4].Controls[0])).Text;
            billDetail.PRODUCTCODE = ((TextBox)(dgDetail.Items[i].Cells[6].Controls[0])).Text;
            billDetail.UNITCODE = ((TextBox)(dgDetail.Items[i].Cells[8].Controls[0])).Text;
            billDetail.QUANTITY = Convert.ToDecimal(((TextBox)(dgDetail.Items[i].Cells[10].Controls[0])).Text);
            billDetail.STATUS = "0";
            billDetail.Update();
            if (objAllot.QueryJian2(billDetail.PRODUCTCODE.ToString()).Tables[0].Rows.Count > 0)
            {
                string MaxPiece = objAllot.QueryJian2(billDetail.PRODUCTCODE.ToString()).Tables[0].Rows[0][1].ToString();
                objAllot.UpdateCell(MaxPiece, billDetail.UNITCODE.ToString(), billDetail.IN_CELLCODE.ToString());
            }
        }
        LoadDetail();
    }
}
