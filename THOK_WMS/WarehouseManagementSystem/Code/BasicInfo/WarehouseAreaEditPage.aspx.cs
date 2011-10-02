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

public partial class Code_BasicInfo_WarehouseAreaEdit : System.Web.UI.Page
{
    WarehouseArea objArea = new WarehouseArea();
    DataSet dsArea;
    Warehouse objWhrehouse = new Warehouse();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["AREA_ID"] != null)
            {
                dsArea = objArea.QueryAreaByID(Convert.ToInt32(Request.QueryString["AREA_ID"]));
                if (this.dsArea.Tables[0].Rows.Count > 0)
                {
                    this.txtAreaID.Text = dsArea.Tables[0].Rows[0]["AREA_ID"].ToString();
                    this.txtWhCode.Text = dsArea.Tables[0].Rows[0]["WH_CODE"].ToString();
                    this.txtAreaType.Text = dsArea.Tables[0].Rows[0]["AREATYPE"].ToString();
                    this.txtAreaCode.Text = dsArea.Tables[0].Rows[0]["AREACODE"].ToString();
                    this.txtAreaName.Text = dsArea.Tables[0].Rows[0]["AREANAME"].ToString();
                    this.txtShortName.Text = dsArea.Tables[0].Rows[0]["SHORTNAME"].ToString();
                    this.ddlActive.SelectedValue = dsArea.Tables[0].Rows[0]["ISACTIVE"].ToString();
                    this.txtMemo.Text = dsArea.Tables[0].Rows[0]["MEMO"].ToString();
                    this.txtAreaCode.ReadOnly = true;
                    this.btnDelete.Enabled = true;
                }
            }
            else if (Request.QueryString["WHCODE"] != null)
            {
                this.txtWhCode.Text = Request.QueryString["WHCODE"];
                this.txtAreaCode.Text = objArea.GetNewAreaCode(this.txtWhCode.Text);
                if (objArea.QueryNewAreaType(Request.QueryString["WHCODE"].ToString()).Tables[0].Rows[0][0] == null || objArea.QueryNewAreaType(Request.QueryString["WHCODE"].ToString()).Tables[0].Rows[0][0].ToString() =="")
                {
                    this.txtAreaType.Text ="0";
                }
                else
                {
                    this.txtAreaType.Text = objArea.QueryNewAreaType(Request.QueryString["WHCODE"].ToString()).Tables[0].Rows[0][0].ToString();
                }
               
            }
        }
        else
        {

        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (this.txtAreaID.Text.Trim().Length == 0)//新增
        {
            objArea.WH_CODE = this.txtWhCode.Text;
            objArea.AREACODE = this.txtAreaCode.Text;
            objArea.AREANAME = this.txtAreaName.Text.Trim().Replace("\'", "\''");
            objArea.SHORTNAME = this.txtShortName.Text.Trim().Replace("\'", "\''");
            objArea.ISACTIVE = this.ddlActive.SelectedValue;
            objArea.AREATYPE = this.txtAreaType.Text;
            objArea.MEMO = this.txtMemo.Text.Trim().Replace("\'", "\''");
            objArea.Insert();

            this.txtAreaID.Text = "";
            this.txtAreaCode.Text = objArea.GetNewAreaCode(this.txtWhCode.Text);
            this.txtAreaName.Text = "";
            this.txtShortName.Text = "";
            this.ddlActive.SelectedIndex = 0;
            this.txtMemo.Text = "";
            this.txtAreaCode.ReadOnly = false;
            this.btnDelete.Enabled = false;

            JScript.Instance.RegisterScript(this, "ReloadParent();");
        }
        else
        {
            objArea.AREA_ID = Convert.ToInt32(this.txtAreaID.Text);
            objArea.WH_CODE = this.txtWhCode.Text;
            objArea.AREACODE = this.txtAreaCode.Text;
            objArea.AREANAME = this.txtAreaName.Text.Trim().Replace("\'", "\''");
            objArea.SHORTNAME = this.txtShortName.Text.Trim().Replace("\'", "\''");
            objArea.ISACTIVE = this.ddlActive.SelectedValue;
            objArea.AREATYPE = this.txtAreaType.Text.Trim();
            objArea.MEMO = this.txtMemo.Text.Trim().Replace("\'", "\''");
            objArea.Update();
            JScript.Instance.RegisterScript(this, "UpdateParent();");
        }
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        JScript.Instance.RegisterScript(this, "window.close();");
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string whcode = this.txtWhCode.Text;
        string areaCode = this.txtAreaCode.Text;
        int areaid = Convert.ToInt32(this.txtAreaID.Text);
        WarehouseShelf objShelf=new WarehouseShelf();
        int count = objShelf.QueryShelfByAreaCode(areaCode).Tables[0].Rows.Count;
        if (count > 0)
        {
            JScript.Instance.ShowMessage(this, areaCode + "库区还有下属货架，不能删除！");
            return;
        }
        else
        {
            objArea.Delete(areaid);
            this.txtAreaID.Text = "";
            this.txtAreaCode.Text = objArea.GetNewAreaCode(this.txtWhCode.Text);
            this.txtAreaName.Text = "";
            this.txtShortName.Text = "";
            this.ddlActive.SelectedIndex=0;
            this.txtAreaType.Text = "";
            this.txtMemo.Text = "";
            this.txtAreaCode.ReadOnly = false;
            this.btnDelete.Enabled = false;
            //JScript.Instance.ShowMessage(this, "库区删除成功！");
            string path = whcode + "/" + areaCode;
            JScript.Instance.RegisterScript(this, "RefreshParent('" + path + "');");
        }
    }
    
}
