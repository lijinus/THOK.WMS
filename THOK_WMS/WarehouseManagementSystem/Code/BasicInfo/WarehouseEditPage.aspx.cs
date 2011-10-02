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

public partial class Code_BasicInfo_WarehouseEdit : System.Web.UI.Page
{
    Warehouse objHouse = new Warehouse();
    DataSet dsHouse;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["WH_CODE"] != null)
            {
                dsHouse = objHouse.QueryWarehouseByCode(Request.QueryString["WH_CODE"]);
                this.txtWHID.Text= dsHouse.Tables[0].Rows[0]["WH_ID"].ToString();
                this.txtWhCode.Text = dsHouse.Tables[0].Rows[0]["WH_CODE"].ToString();
                this.txtWhName.Text = dsHouse.Tables[0].Rows[0]["WH_NAME"].ToString();
                this.txtShortName.Text = dsHouse.Tables[0].Rows[0]["SHORTNAME"].ToString();
                this.txtDEFAULTUNIT.Text = dsHouse.Tables[0].Rows[0]["DEFAULTUNIT"].ToString();
                this.txtUNITNAME.Text = dsHouse.Tables[0].Rows[0]["UNITNAME"].ToString();
                this.ddlType.SelectedValue = dsHouse.Tables[0].Rows[0]["WH_TYPE"].ToString();
                this.txtArea.Text = dsHouse.Tables[0].Rows[0]["WH_AREA"].ToString();
                this.txtCapacity.Text = dsHouse.Tables[0].Rows[0]["CAPACITY"].ToString();
                this.ddlActive.SelectedValue = dsHouse.Tables[0].Rows[0]["ISACTIVE"].ToString();
                this.txtMemo.Text = dsHouse.Tables[0].Rows[0]["MEMO"].ToString();
                this.txtWhCode.ReadOnly = true;
                this.btnDelete.Enabled = true;
                this.ddlType.Enabled = false;
            }
            else
            {
                this.txtWhCode.Text = objHouse.GetNewCode("0");
                this.txtCapacity.Text = "0";
                this.txtArea.Text = "0.00";
                this.ddlType.Enabled = true;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.txtWHID.Text.Trim().Length == 0)//新增
        {
            objHouse.WH_CODE = this.txtWhCode.Text;
            objHouse.WH_NAME = this.txtWhName.Text.Trim().Replace("\'", "\''");
            objHouse.SHORTNAME = this.txtShortName.Text.Trim().Replace("\'", "\''");
            objHouse.ISACTIVE = this.ddlActive.SelectedValue;
            if (this.txtArea.Text.Trim().Length>0)
            {
                objHouse.WH_AREA = Convert.ToDecimal(this.txtArea.Text);
            }
            if (this.txtCapacity.Text.Trim().Length > 0)
            {
                objHouse.CAPACITY = Convert.ToInt32(this.txtCapacity.Text);
            }
            objHouse.WH_TYPE = this.ddlType.SelectedValue;
            objHouse.DEFAULTUNIT = this.txtDEFAULTUNIT.Text;
            objHouse.Insert();
            JScript.Instance.RegisterScript(this, "ReloadParent();");
        }
        else
        {
            objHouse.WH_ID = Convert.ToInt32(this.txtWHID.Text);
            objHouse.WH_CODE = this.txtWhCode.Text;
            objHouse.WH_NAME = this.txtWhName.Text.Trim().Replace("\'", "\''");
            objHouse.SHORTNAME = this.txtShortName.Text.Trim().Replace("\'", "\''");
            objHouse.ISACTIVE = this.ddlActive.SelectedValue;
            if (this.txtArea.Text.Trim().Length > 0)
            {
                objHouse.WH_AREA = Convert.ToDecimal(this.txtArea.Text);
            }
            if (this.txtCapacity.Text.Trim().Length > 0)
            {
                objHouse.CAPACITY = Convert.ToInt32(this.txtCapacity.Text);
            }
            objHouse.WH_TYPE = this.ddlType.SelectedValue;
            objHouse.DEFAULTUNIT = this.txtDEFAULTUNIT.Text;
            objHouse.Update();
            JScript.Instance.RegisterScript(this, "UpdateParent();");
        }

        JScript.Instance.RegisterScript(this, "window.close();");
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        JScript.Instance.RegisterScript(this, "window.close();");
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string whcode = this.txtWhCode.Text;
        WarehouseArea objArea = new WarehouseArea();
        int count = objArea.QueryAreaByWHCODE(whcode).Tables[0].Rows.Count;
        if (count > 0)
        {
            JScript.Instance.ShowMessage(this, whcode + "还有下属库区，不能删除！");
            return;
        }
        else
        {
            objHouse.Delete(whcode);

            this.txtWHID.Text = "";
            this.txtWhCode.Text = objHouse.GetNewCode(this.ddlType.SelectedValue);
            this.txtWhName.Text = "";
            this.txtShortName.Text = "";
            this.txtDEFAULTUNIT.Text = "";
            this.txtUNITNAME.Text = "";
            this.ddlType.SelectedIndex=0;
            this.txtArea.Text = "0";
            this.txtCapacity.Text = "0";
            this.ddlActive.SelectedIndex=0;
            this.txtMemo.Text = "";
            this.txtWhCode.ReadOnly = false;
            this.btnDelete.Enabled = false;
            string path = whcode;
            JScript.Instance.RegisterScript(this, "RefreshParent('" + path + "');");
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtWhCode.Text = objHouse.GetNewCode(this.ddlType.SelectedValue);
    }
}
