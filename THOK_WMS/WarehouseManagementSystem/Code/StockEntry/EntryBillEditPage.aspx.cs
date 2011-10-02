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
using THOK.WMS.Upload;
using THOK.WMS.Upload.Bll;
public partial class Code_StockEntry_EntryBillEdit :BasePage
{
    EntryBillMaster billMaster = new EntryBillMaster();
    EntryBillDetail billDetail = new EntryBillDetail();
    UpdateUploadBll updateBll = new UpdateUploadBll();
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
            this.txtTotalAmount.Text = dsMaster.Tables[0].Rows[0]["TOTALINPUTAMOUNT"].ToString();
            this.txtTotalQty.Text = dsMaster.Tables[0].Rows[0]["TOTALINPUTQUANTITY"].ToString();
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
            CountTotal();
        }
    }

    protected void CountTotal()
    {
        decimal qty = 0.00M;
        decimal amount = 0.00M;
        foreach (DataRow row in dsDetail.Tables[0].Rows)
        {
            qty += Convert.ToDecimal(row["INPUTQUANTITY"]);
            amount += Convert.ToDecimal(row["INPUTQUANTITY"]) * Convert.ToDecimal(row["PRICE"]);
        }
        this.txtTotalQty.Text = qty.ToString();
        this.txtTotalAmount.Text = amount.ToString();
    }
    #endregion

    #region 保存汇总
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtBillTypeCode.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择入库单据类型");
                return;
            }
            if (this.txtWHCODE.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择入库的仓库");
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
                updateBll.InsertBillMaster("DWV_IWMS_IN_STORE_BILL", this.txtBillNo.Text, this.txtBillTypeCode.Text, "1", Session["EmployeeCode"].ToString(), true);                
                this.hdnOpFlag.Value = "1";
                this.ddlBillState.SelectedValue = "1";
                JScript.Instance.ShowMessage(this, "入库单汇总信息保存成功");
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
                updateBll.InsertBillMaster("DWV_IWMS_IN_STORE_BILL", this.txtBillNo.Text, this.txtBillTypeCode.Text, "1", Session["EmployeeCode"].ToString(), false);
                JScript.Instance.ShowMessage(this, "入库单汇总信息修改成功");
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
                chk.Text = "操作";
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
                LinkButton lnkBtn = new LinkButton();
                lnkBtn.Attributes.Add("style", " text-align:center;word-break:keep-all; white-space:nowrap");
                lnkBtn.Text = "修改";
                lnkBtn.OnClientClick = string.Format("return OpenEdit({0})",e.Item.Cells[1].Text);
                e.Item.Cells[0].Controls.Add(lnkBtn);


                //TextBox tb1 = new TextBox();
                //tb1.ID = "productcode";//产品编号
                //tb1.Text = e.Item.Cells[2].Text;
                //tb1.Attributes.Add("style", "width:80px;");
                //tb1.Attributes.Add("class", "GridInputAlignLeft");
                //e.Item.Cells[2].Controls.Add(tb1);
                //Button btn1 = new Button();
                //btn1.CssClass = "ButtonBrowse2";
                ////btn1.Text = "...";
                //btn1.OnClientClick = string.Format("SelectDialog2('{0},{1}','WMS_PRODUCT','PRODUCTCODE,PRODUCTNAME')", tb1.ClientID, tb1.ClientID.Replace("code", "name"));
                //e.Item.Cells[2].Controls.Add(btn1);
     
                //TextBox tb2 = new TextBox();
                //tb2.ID = "productname";//产品名称
                //tb2.Text = e.Item.Cells[3].Text;
                //tb2.Attributes.Add("style", "width:150px;");
                //tb2.Attributes.Add("class", "GridInput");
                //tb2.Attributes.Add("onfocus", "CannotEdit(this)");
                //e.Item.Cells[3].Controls.Add(tb2);


                //TextBox tb3 = new TextBox();
                //tb3.ID = "unitcode";
                //tb3.Text = e.Item.Cells[4].Text;
                //tb3.Attributes.Add("style", "width:50px;");
                //tb3.Attributes.Add("class", "GridInputAlignLeft");
                //e.Item.Cells[4].Controls.Add(tb3);
                //Button btn2 = new Button();
                //btn2.CssClass = "ButtonBrowse2";
                ////btn2.Text = "...";
                //btn2.OnClientClick = string.Format("SelectDialog2('{0},{1}','WMS_UNIT','UNITCODE,UNITNAME')", tb3.ClientID, tb3.ClientID.Replace("code", "name"));
                //e.Item.Cells[4].Controls.Add(btn2);

                //TextBox tb4 = new TextBox();
                //tb4.ID = "unitname";
                //tb4.Text = e.Item.Cells[5].Text;
                //tb4.Attributes.Add("style", "width:60px;");
                //tb4.Attributes.Add("class", "GridInput");
                //tb4.Attributes.Add("onfocus", "CannotEdit(this)");
                //e.Item.Cells[5].Controls.Add(tb4);

                //TextBox tb5 = new TextBox();
                //tb5.ID = "quantity";
                //tb5.Text = e.Item.Cells[6].Text;
                //tb5.Attributes.Add("style", "width:60px;");
                //tb5.Attributes.Add("class", "GridInputAlignRight");
                //tb5.Attributes.Add("onblur", "IsNumber(this,'数量')");
                //e.Item.Cells[6].Controls.Clear();
                //e.Item.Cells[6].Controls.Add(tb5);

                //TextBox tb6 = new TextBox();
                //tb6.ID = "price";
                //tb6.Text = e.Item.Cells[7].Text;
                //tb6.Attributes.Add("style", "width:60px;");
                //tb6.Attributes.Add("class", "GridInputAlignRight");
                //tb6.Attributes.Add("onblur", "IsNumber(this,'单价')");
                //e.Item.Cells[7].Controls.Clear();
                //e.Item.Cells[7].Controls.Add(tb6);
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

    #region 新增入库单
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("EntryBillEditPage.aspx");
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


    #region 返回
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EntryBillPage.aspx?back=true");
    }
    #endregion

    #region 新增明细时，先总汇保存
    protected void btnCreateDetail_Click(object sender, EventArgs e)
    {
        if (this.hdnOpFlag.Value == "0")
        {
            if (this.txtWHCODE.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择入库的仓库");
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
            updateBll.InsertBillMaster("DWV_IWMS_IN_STORE_BILL", this.txtBillNo.Text, this.txtBillTypeCode.Text, "1", Session["EmployeeCode"].ToString(), true);
            this.hdnOpFlag.Value = "1";
            this.ddlBillState.SelectedValue = "1";
            JScript.Instance.RegisterScript(this, "OpenNew()");
        }
    }
    #endregion
}
