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

public partial class Code_BasicInfo_WarehouseCellEdit : System.Web.UI.Page
{
    WarehouseCell objCell = new WarehouseCell();
    DataSet dsCell;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtAreaType.Visible = false;
        if (!IsPostBack)
        {

            Comparison obj = new Comparison();
            DataSet dsTemp = obj.GetItems("CELL_LAYERNO");
            this.ddlLayer.Items.Clear();
            this.ddlLayer.DataSource = dsTemp.Tables[0].DefaultView;
            this.ddlLayer.DataTextField = "TEXT";
            this.ddlLayer.DataValueField = "VALUE";
            this.ddlLayer.DataBind();
            if (Request.QueryString["CELL_ID"] != null)
            {
                dsCell = objCell.QueryWarehouseCell("CELL_ID=" + Request.QueryString["CELL_ID"]);
                this.txtCELLID.Text = dsCell.Tables[0].Rows[0]["CELL_ID"].ToString();
                this.txtShelfCode.Text = dsCell.Tables[0].Rows[0]["SHELFCODE"].ToString();
                this.txtCellCode.Text = dsCell.Tables[0].Rows[0]["CELLCODE"].ToString();
                this.txtCellName.Text = dsCell.Tables[0].Rows[0]["CELLNAME"].ToString();
                this.ddlActive.SelectedValue= dsCell.Tables[0].Rows[0]["ISACTIVE"].ToString();
                this.txtMaxQty.Text = dsCell.Tables[0].Rows[0]["MAX_QUANTITY"].ToString();
                this.ddlLayer.SelectedValue = dsCell.Tables[0].Rows[0]["LAYER_NO"].ToString();
                this.txtAssignedProductCode.Text = dsCell.Tables[0].Rows[0]["ASSIGNEDPRODUCT"].ToString();
                this.txtAssignedProductName.Text = dsCell.Tables[0].Rows[0]["A_PRODUCTNAME"].ToString();
                this.txtUnitCode.Text = dsCell.Tables[0].Rows[0]["UNITCODE"].ToString();
                this.txtUnitName.Text = dsCell.Tables[0].Rows[0]["UNITNAME"].ToString();
                this.txtPalletID.Text = dsCell.Tables[0].Rows[0]["PALLETID"].ToString();
                this.ddlVirtual.SelectedValue = dsCell.Tables[0].Rows[0]["ISVIRTUAL"].ToString();
                this.txtEGroup.Text = dsCell.Tables[0].Rows[0]["ELECTRICGROUP"].ToString();
                this.txtECom.Text = dsCell.Tables[0].Rows[0]["ELECTRICCOM"].ToString();
                this.txtEAddress.Text = dsCell.Tables[0].Rows[0]["ELECTRICADDRESS"].ToString();
                this.txtAreaType.Text = dsCell.Tables[0].Rows[0]["AREATYPE"].ToString();
                this.txtCellCode.ReadOnly = true;
                this.btnDelete.Enabled = true;

            }
            else if (Request.QueryString["SHELFCODE"] != null)
            {
                this.txtShelfCode.Text = Request.QueryString["SHELFCODE"];
                this.txtAreaType.Text = Request.QueryString["AREATYPE"];
                //this.txtEGroup.Text = "0";
                //this.txtECom.Text = "0";
                //this.txtEAddress.Text = "0";
                this.txtCellCode.Text = objCell.GetNewCellCode(Request.QueryString["SHELFCODE"]);
            }
        }
        else
        {

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtCELLID.Text.Trim().Length == 0)//新增
            {
                objCell.SHELFCODE = this.txtShelfCode.Text;
                objCell.CELLCODE = this.txtCellCode.Text;
                objCell.CELLNAME = this.txtCellName.Text;
                objCell.ISACTIVE = this.ddlActive.SelectedValue;
                objCell.AREATYPE = this.txtAreaType.Text;
                if (this.txtMaxQty.Text.Trim().Length > 0)
                {
                    objCell.MAX_QUANTITY = Convert.ToDecimal(this.txtMaxQty.Text);
                }
                objCell.LAYER_NO = this.ddlLayer.SelectedValue;
                objCell.ASSIGNEDPRODUCT = this.txtAssignedProductCode.Text;
                objCell.UNITCODE = this.txtUnitCode.Text;
                objCell.ISVIRTUAL = this.ddlVirtual.SelectedValue;
                objCell.PALLETID = this.txtPalletID.Text;

                if (this.txtEGroup.Text.Trim().Length > 0)
                {
                    objCell.ELECTRICGROUP = Convert.ToInt32(this.txtEGroup.Text);
                }

                if (this.txtECom.Text.Trim().Length > 0)
                {
                    objCell.ELECTRICCOM = Convert.ToInt32(this.txtECom.Text);
                }

                if (this.txtEAddress.Text.Trim().Length > 0)
                {
                    objCell.ELECTRICADDRESS = Convert.ToInt32(this.txtEAddress.Text);
                }

                objCell.Insert();
                this.btnSave.Enabled = false;
                JScript.Instance.RegisterScript(this, "ReloadParent();");
            }
            else//修改
            {
                objCell.CELL_ID = Convert.ToInt32(this.txtCELLID.Text);
                objCell.SHELFCODE = this.txtShelfCode.Text;
                objCell.CELLCODE = this.txtCellCode.Text;
                objCell.CELLNAME = this.txtCellName.Text;
                objCell.ISACTIVE = this.ddlActive.SelectedValue;
                objCell.AREATYPE = this.txtAreaType.Text;
                if (this.txtMaxQty.Text.Trim().Length > 0)
                {
                    objCell.MAX_QUANTITY = Convert.ToDecimal(this.txtMaxQty.Text);
                }
                objCell.LAYER_NO = this.ddlLayer.SelectedValue;
                objCell.ASSIGNEDPRODUCT = this.txtAssignedProductCode.Text;
                objCell.UNITCODE = this.txtUnitCode.Text;
                objCell.ISVIRTUAL = this.ddlVirtual.SelectedValue;
                objCell.PALLETID = this.txtPalletID.Text;

                if (this.txtEGroup.Text.Trim().Length > 0)
                {
                    objCell.ELECTRICGROUP = Convert.ToInt32(this.txtEGroup.Text);
                }
                if (this.txtAssignedProductName.Text.Trim() == "" || this.txtAssignedProductName.Text == string.Empty)
                {
                    objCell.ASSIGNEDPRODUCT = null;
                }
                if (this.txtECom.Text.Trim().Length > 0)
                {
                    objCell.ELECTRICCOM = Convert.ToInt32(this.txtECom.Text);
                }

                if (this.txtEAddress.Text.Trim().Length > 0)
                {
                    objCell.ELECTRICADDRESS = Convert.ToInt32(this.txtEAddress.Text);
                }
                objCell.Update();
                JScript.Instance.RegisterScript(this, "UpdateParent();");
            }
            this.btnContinue.Enabled = true;
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        JScript.Instance.RegisterScript(this, "window.close();");
    }


    protected void btnContinue_Click(object sender, EventArgs e)
    {
        this.txtCELLID.Text ="";
        //this.txtShelfCode.Text = dsCell.Tables[0].Rows[0]["SHELFCODE"].ToString();
        this.txtCellCode.Text = objCell.GetNewCellCode(this.txtShelfCode.Text);
        this.txtCellName.Text = "";
        this.ddlActive.SelectedIndex=0;
        this.txtAreaType.Text = "";
        //this.txtMaxQty.Text = "0";
        //this.ddlLayer.SelectedValue = dsCell.Tables[0].Rows[0]["LAYER_NO"].ToString();
        this.txtAssignedProductCode.Text = "";
        this.txtAssignedProductName.Text = "";

        this.txtPalletID.Text = "";
        this.ddlVirtual.SelectedIndex=0;
        this.txtEGroup.Text = "";
        this.txtECom.Text = "";
        this.txtEAddress.Text = "";

        this.btnSave.Enabled = true;
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        dsCell = objCell.QueryWarehouseCell("CELL_ID=" + this.txtCELLID.Text);
        if (dsCell.Tables[0].Rows[0]["QUANTITY"].ToString() != "")
        {
            decimal qty = Convert.ToDecimal(dsCell.Tables[0].Rows[0]["QUANTITY"].ToString());
            if (qty > 0)
            {
                JScript.Instance.ShowMessage(this, "该货位正在使用，不能删除！");
                return;
            }
        }

        string whcode = dsCell.Tables[0].Rows[0]["WH_CODE"].ToString();
        string areacode = dsCell.Tables[0].Rows[0]["AREACODE"].ToString();
        string shelfCode = dsCell.Tables[0].Rows[0]["SHELFCODE"].ToString();
        string cellcode = dsCell.Tables[0].Rows[0]["CELLCODE"].ToString();
        int cellid = Convert.ToInt32(this.txtCELLID.Text);
        objCell.Delete(cellid);
        this.txtCELLID.Text = "";
        //this.txtShelfCode.Text = dsCell.Tables[0].Rows[0]["SHELFCODE"].ToString();
        this.txtCellCode.Text = objCell.GetNewCellCode(this.txtShelfCode.Text);
        this.txtCellName.Text = "";
        this.ddlActive.SelectedIndex = 0;
        this.txtAreaType.Text = "";
        //this.txtMaxQty.Text = "0";
        //this.ddlLayer.SelectedValue = dsCell.Tables[0].Rows[0]["LAYER_NO"].ToString();
        this.txtAssignedProductCode.Text = "";
        this.txtAssignedProductName.Text = "";

        this.txtPalletID.Text = "";
        this.ddlVirtual.SelectedIndex = 0;
        this.txtEGroup.Text = "";
        this.txtECom.Text = "";
        this.txtEAddress.Text = "";

        this.btnSave.Enabled = true;
        this.btnDelete.Enabled = false;
        string path = whcode + "/" + areacode + "/" + shelfCode+"/"+cellcode;
        JScript.Instance.RegisterScript(this, "RefreshParent('" + path + "');");

    }
}
