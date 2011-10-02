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

public partial class Code_StockDelivery_DeliveryBillDetailEditPage : BasePage
{
    DeliveryBillDetail billDetail = new DeliveryBillDetail();
    DeliveryAllot billallot = new DeliveryAllot();//2010.12.8添加
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
                this.txtOutputQuantity.Text = dsDetail.Tables[0].Rows[0]["OUTPUTQUANTITY"].ToString();
                this.txtTotalAmount.Text = dsDetail.Tables[0].Rows[0]["TOTALAMOUNT"].ToString();
                this.txtMemo.Text = dsDetail.Tables[0].Rows[0]["MEMO"].ToString();
                this.btnSave.Enabled = true;
            }
            else if (Request.QueryString["BILLNO"] != null)
            {
                this.txtBillNo.Text = Request.QueryString["BILLNO"];
                this.txtUnitCode.Text = Request.QueryString["UNITCODE"];
                this.txtUnitName.Text = Request.QueryString["UNITNAME"];
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
            double num = Convert.ToDouble(this.txtQuantity.Text.ToString().Trim());
            if (num > 999999.99 || num <= 0)
            {
                JScript.Instance.ShowMessage(this, "请输入0.00 - 999999.99范围数字！");
                return;
            }

            //判断是否有库存2010.12.8
            DataTable dt = billallot.GetQuantity(this.txtProductCode.Text.Trim());
            
            DataTable ratedt = billallot.Rate(this.txtUnitCode.Text.Trim());
            decimal rate = Convert.ToDecimal(ratedt.Rows[0]["STANDARDRATE"].ToString().Trim());
            decimal quantity = Convert.ToDecimal(this.txtQuantity.Text.Trim().ToString()) * rate;
            decimal piece = Convert.ToDecimal(dt.Rows[0]["QUANTITY"].ToString());
            if (piece == 0)
            {
                JScript.Instance.ShowMessage(this, "没有此货物");
                return;
            }
            if (quantity > piece)
            {
                JScript.Instance.ShowMessage(this, "库存不足");
                return;
            }
            
            if (this.txtID.Text.Trim() == "") //新增
            {
                DataSet dsDetail = billDetail.QueryByBillNo(this.txtBillNo.Text);
                DataRow[] rows = dsDetail.Tables[0].Select("PRODUCTCODE='" + this.txtProductCode.Text + "' AND UNITCODE='" + this.txtUnitCode.Text + "'");
                if (rows.Length == 1)//同一种类型烟
                {
                    billDetail.ID = rows[0]["ID"].ToString();
                    billDetail.BILLNO = this.txtBillNo.Text;
                    billDetail.PRODUCTCODE = this.txtProductCode.Text;
                    billDetail.UNITCODE = this.txtUnitCode.Text;
                    billDetail.PRICE = Convert.ToDecimal(this.txtPrice.Text);
                    billDetail.QUANTITY = Convert.ToDecimal(this.txtQuantity.Text) + Convert.ToInt32(Convert.ToDecimal(rows[0]["QUANTITY"].ToString()));
                    billDetail.OUTPUTQUANTITY = billDetail.QUANTITY;
                    billDetail.MEMO = this.txtMemo.Text;
                    billDetail.Update();
                    updateBll.InsertDetail("DWV_IWMS_OUT_STORE_BILL_DETAIL", rows[0]["ID"].ToString(), this.txtBillNo.Text, this.txtProductCode.Text, Convert.ToDecimal(this.txtQuantity.Text), false, "WMS_OUT_BILLDETAIL");
                }
                else//不同种烟的新增
                {
                    string id = DateTime.Now.ToString("yyMMddHHmmssfff").Substring(1, 14);
                    billDetail.ID = id;
                    billDetail.BILLNO = this.txtBillNo.Text;
                    billDetail.PRODUCTCODE = this.txtProductCode.Text;
                    billDetail.UNITCODE = this.txtUnitCode.Text;
                    billDetail.PRICE = Convert.ToDecimal(this.txtPrice.Text);
                    billDetail.QUANTITY = Convert.ToDecimal(this.txtQuantity.Text);
                    billDetail.OUTPUTQUANTITY = Convert.ToDecimal(this.txtOutputQuantity.Text);
                    billDetail.MEMO = this.txtMemo.Text;
                    billDetail.Insert();
                    updateBll.InsertDetail("DWV_IWMS_OUT_STORE_BILL_DETAIL", id, this.txtBillNo.Text, this.txtProductCode.Text, Convert.ToDecimal(this.txtQuantity.Text), true, "WMS_OUT_BILLDETAIL");
                }
            }
            else//修改
            {
                billDetail.ID = this.txtID.Text;
                billDetail.BILLNO = this.txtBillNo.Text;
                billDetail.PRODUCTCODE = this.txtProductCode.Text;
                billDetail.UNITCODE = this.txtUnitCode.Text;
                billDetail.PRICE = Convert.ToDecimal(this.txtPrice.Text);
                billDetail.QUANTITY = Convert.ToDecimal(this.txtQuantity.Text);
                billDetail.OUTPUTQUANTITY = Convert.ToDecimal(this.txtOutputQuantity.Text);
                billDetail.MEMO = this.txtMemo.Text;
                billDetail.Update();
                updateBll.InsertDetail("DWV_IWMS_OUT_STORE_BILL_DETAIL", this.txtID.Text, this.txtBillNo.Text, this.txtProductCode.Text, Convert.ToDecimal(this.txtQuantity.Text), false, "WMS_OUT_BILLDETAIL");
            }

        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
        JScript.Instance.RegisterScript(this, "window.close();");
    }
}
