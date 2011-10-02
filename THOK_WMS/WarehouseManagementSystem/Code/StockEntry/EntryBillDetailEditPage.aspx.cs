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
using THOK.WMS.Upload.Bll;

public partial class Code_StockEntry_EntryBillDetailEdit : BasePage
{
    EntryBillDetail billDetail = new EntryBillDetail();
    UpdateUploadBll updateBll = new UpdateUploadBll();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                DataSet dsDetail = billDetail.QueryByID(Request.QueryString["ID"]);
                this.txtID.Text = dsDetail.Tables[0].Rows[0]["ID"].ToString();
                this.txtBillNo.Text = dsDetail.Tables[0].Rows[0]["BILLNO"].ToString();
                this.txtProductCode.Text = dsDetail.Tables[0].Rows[0]["PRODUCTCODE"].ToString();
                this.txtProductName.Text = dsDetail.Tables[0].Rows[0]["PRODUCTNAME"].ToString();
                this.txtUnitCode.Text = dsDetail.Tables[0].Rows[0]["UNITCODE"].ToString();
                this.txtUnitName.Text = dsDetail.Tables[0].Rows[0]["UNITNAME"].ToString();
                this.txtPrice.Text = dsDetail.Tables[0].Rows[0]["PRICE"].ToString();
                this.txtQuantity.Text = dsDetail.Tables[0].Rows[0]["QUANTITY"].ToString();
                this.txtInputQuantity.Text = dsDetail.Tables[0].Rows[0]["INPUTQUANTITY"].ToString();
                this.txtTotalAmount.Text = dsDetail.Tables[0].Rows[0]["TOTALAMOUNT"].ToString();
                this.txtMemo.Text = dsDetail.Tables[0].Rows[0]["MEMO"].ToString();
                this.btnSave.Enabled = true;
            }
            else if (Request.QueryString["BILLNO"] != null)
            {
                this.txtBillNo.Text = Request.QueryString["BILLNO"].ToString().Trim();
                this.txtUnitCode.Text = Request.QueryString["UNITCODE"].ToString().Trim();
                this.txtUnitName.Text = Request.QueryString["UNITNAME"].ToString().Trim();
                this.btnSave.Enabled = true;
                this.txtID.Text = "";
            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        { 
            if (this.txtProductCode.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择产品");
                return;
            }
            if (this.txtUnitCode.Text.Length == 0)
            {
                JScript.Instance.ShowMessage(this, "请选择产品单位");
                return;
            }
            Double num = Convert.ToDouble(this.txtQuantity.Text.ToString().Trim());
            if (num > 999999.99 || num <= 0)
            {
                JScript.Instance.ShowMessage(this, "请输入0.00 - 999999.99范围数字！");
                return;
            }

            if (this.txtID.Text.Trim() == "") //新增
            {
                DataSet dsDetail=billDetail.QueryByBillNo(this.txtBillNo.Text);
                DataRow[] rows = dsDetail.Tables[0].Select("PRODUCTCODE='"+this.txtProductCode.Text+"' AND UNITCODE='"+this.txtUnitCode.Text+"'");
                if (rows.Length == 1)
                {
                    billDetail.ID = rows[0]["ID"].ToString();
                    billDetail.BILLNO = this.txtBillNo.Text;
                    billDetail.PRODUCTCODE = this.txtProductCode.Text;
                    billDetail.UNITCODE = this.txtUnitCode.Text;
                    billDetail.PRICE = Convert.ToDecimal(this.txtPrice.Text);
                    billDetail.QUANTITY = Convert.ToDecimal(this.txtQuantity.Text)+Convert.ToInt32(Convert.ToDecimal(rows[0]["QUANTITY"].ToString()));
                    billDetail.INPUTQUANTITY = billDetail.QUANTITY;
                    billDetail.MEMO = this.txtMemo.Text;
                    billDetail.Update();
                    updateBll.InsertDetail("DWV_IWMS_IN_STORE_BILL_DETAIL", rows[0]["ID"].ToString(), this.txtBillNo.Text, this.txtProductCode.Text, Convert.ToDecimal(this.txtQuantity.Text), false, "WMS_IN_BILLDETAIL");
                }
                else
                {
                    string id = DateTime.Now.ToString("yyMMddHHmmssfff").Substring(1,14);
                    billDetail.ID = id;
                    billDetail.BILLNO = this.txtBillNo.Text;
                    billDetail.PRODUCTCODE = this.txtProductCode.Text;
                    billDetail.UNITCODE = this.txtUnitCode.Text;
                    billDetail.PRICE = Convert.ToDecimal(this.txtPrice.Text);
                    billDetail.QUANTITY = Convert.ToDecimal(this.txtQuantity.Text);
                    billDetail.INPUTQUANTITY = Convert.ToDecimal(this.txtInputQuantity.Text);
                    billDetail.MEMO = this.txtMemo.Text;
                    billDetail.Insert();
                    updateBll.InsertDetail("DWV_IWMS_IN_STORE_BILL_DETAIL",id , this.txtBillNo.Text, this.txtProductCode.Text, Convert.ToDecimal(this.txtQuantity.Text), true, "WMS_IN_BILLDETAIL");
                }
            }
            else
            {
                billDetail.ID = this.txtID.Text;
                billDetail.BILLNO = this.txtBillNo.Text;
                billDetail.PRODUCTCODE = this.txtProductCode.Text;
                billDetail.UNITCODE = this.txtUnitCode.Text;
                billDetail.PRICE = Convert.ToDecimal(this.txtPrice.Text);
                billDetail.QUANTITY = Convert.ToDecimal(this.txtQuantity.Text);
                billDetail.INPUTQUANTITY = Convert.ToDecimal(this.txtInputQuantity.Text);
                billDetail.MEMO = this.txtMemo.Text;
                billDetail.Update();
                updateBll.InsertDetail("DWV_IWMS_IN_STORE_BILL_DETAIL", this.txtID.Text, this.txtBillNo.Text, this.txtProductCode.Text, Convert.ToDecimal(this.txtQuantity.Text), false, "WMS_IN_BILLDETAIL");
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
            return;
        }
        JScript.Instance.RegisterScript(this, "window.close();");
    }
}
