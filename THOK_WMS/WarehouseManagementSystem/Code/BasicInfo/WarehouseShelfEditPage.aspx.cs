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

public partial class Code_BasicInfo_WarehouseShelfEdit : System.Web.UI.Page
{
    WarehouseShelf objShelf = new WarehouseShelf();
    DataSet dsShelf;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["SHELF_ID"] != null)
            {
                dsShelf=objShelf.QueryShelfByID(Convert.ToInt32(Request.QueryString["SHELF_ID"]));
                this.txtShelfID.Text = dsShelf.Tables[0].Rows[0]["SHELF_ID"].ToString();
                this.txtWhCode.Text=dsShelf.Tables[0].Rows[0]["WH_CODE"].ToString();
                this.txtAreaCode.Text=dsShelf.Tables[0].Rows[0]["AREACODE"].ToString();
                this.txtShelfCode.Text=dsShelf.Tables[0].Rows[0]["SHELFCODE"].ToString();
                this.txtShelfName.Text=dsShelf.Tables[0].Rows[0]["SHELFNAME"].ToString();
                this.ddlActive.SelectedValue=dsShelf.Tables[0].Rows[0]["ISACTIVE"].ToString();
                this.txtAreaType.Text = dsShelf.Tables[0].Rows[0]["AREATYPE"].ToString();
                this.txtCellRows.Text=dsShelf.Tables[0].Rows[0]["CELLROWS"].ToString();
                this.txtCellCols.Text=dsShelf.Tables[0].Rows[0]["CELLCOLS"].ToString();
                this.txtImgX.Text=dsShelf.Tables[0].Rows[0]["IMG_X"].ToString();
                this.txtImgY.Text=dsShelf.Tables[0].Rows[0]["IMG_Y"].ToString();
                this.txtMemo.Text=dsShelf.Tables[0].Rows[0]["MEMO"].ToString();

                this.txtShelfCode.ReadOnly = true;
                this.btnDelete.Enabled = true;
            }
            else if (Request.QueryString["WHCODE"] != null && Request.QueryString["AREACODE"] != null)
            {

                this.txtWhCode.Text = Request.QueryString["WHCODE"];
                this.txtAreaCode.Text = Request.QueryString["AREACODE"];
                this.txtAreaType.Text=Request.QueryString["AREATYPE"];
                this.txtCellRows.Text = "3";
                this.txtCellCols.Text = "40";
                this.txtImgX.Text = "0.00";
                this.txtImgY.Text = "0.00";
                this.txtShelfCode.Text = objShelf.GetNewShelfCode(Request.QueryString["AREACODE"]);
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtShelfID.Text.Trim().Length == 0)
            {
                objShelf.WH_CODE = this.txtWhCode.Text;
                objShelf.AREACODE = this.txtAreaCode.Text;
                objShelf.SHELFCODE = this.txtShelfCode.Text.Trim().Replace("\'", "\''");
                objShelf.SHELFNAME = this.txtShelfName.Text.Trim().Replace("\'", "\''");
                objShelf.ISACTIVE = this.ddlActive.SelectedValue;
                objShelf.AREATYPE = this.txtAreaType.Text.Trim();
                objShelf.CELLROWS = Convert.ToInt32(this.txtCellRows.Text);
                objShelf.CELLCOLS = Convert.ToInt32(this.txtCellCols.Text);
                if (this.txtImgX.Text.Trim().Length > 0)
                {
                    objShelf.IMG_X = Convert.ToDouble(this.txtImgX.Text);
                }
                if (this.txtImgY.Text.Trim().Length > 0)
                {
                    objShelf.IMG_Y = Convert.ToDouble(this.txtImgY.Text);
                }
                objShelf.MEMO = this.txtMemo.Text.Trim().Replace("\'", "\''");
                objShelf.Insert();
                //this.btnContinue.Enabled = true;
                //this.btnSave.Enabled = false;
                JScript.Instance.RegisterScript(this, "ReloadParent();");
            }
            else
            {
                objShelf.SHELF_ID = Convert.ToInt32(this.txtShelfID.Text);
                objShelf.WH_CODE = this.txtWhCode.Text;
                objShelf.AREACODE = this.txtAreaCode.Text;
                objShelf.SHELFCODE = this.txtShelfCode.Text.Trim().Replace("\'", "\''");
                objShelf.SHELFNAME = this.txtShelfName.Text.Trim().Replace("\'", "\''");
                objShelf.ISACTIVE = this.ddlActive.SelectedValue;
                objShelf.AREATYPE = this.txtAreaType.Text.Trim();
                objShelf.CELLROWS = Convert.ToInt32(this.txtCellRows.Text);
                objShelf.CELLCOLS = Convert.ToInt32(this.txtCellCols.Text);
                if (this.txtImgX.Text.Trim().Length > 0)
                {
                    objShelf.IMG_X = Convert.ToDouble(this.txtImgX.Text);
                }
                if (this.txtImgY.Text.Trim().Length > 0)
                {
                    objShelf.IMG_Y = Convert.ToDouble(this.txtImgY.Text);
                }
                objShelf.MEMO = this.txtMemo.Text.Trim().Replace("\'", "\''");
                objShelf.Update();
                this.btnContinue.Enabled = true;
                JScript.Instance.RegisterScript(this, "UpdateParent();");
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
        //JScript.Instance.RegisterScript(this, "window.close();");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        JScript.Instance.RegisterScript(this, "window.close();");
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        this.txtShelfID.Text = "";
        this.txtShelfCode.Text = objShelf.GetNewShelfCode(this.txtAreaCode.Text);
        this.txtShelfName.Text = "";
        this.ddlActive.SelectedIndex = 0;
        this.txtMemo.Text = "";
        this.btnSave.Enabled = true;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string whcode = this.txtWhCode.Text;
        string areacode = this.txtAreaCode.Text;
        string shelfCode = this.txtShelfCode.Text;
        int shelfid = Convert.ToInt32(this.txtShelfID.Text);
        WarehouseCell objCell = new WarehouseCell();
        int count = objCell.QueryWarehouseCell("SHELFCODE='" + shelfCode + "'").Tables[0].Rows.Count;
        if (count > 0)
        {
            JScript.Instance.ShowMessage(this, shelfCode + "货架还有下属货位，不能删除！");
            return;
        }
        else
        {
            objShelf.Delete(shelfid);
            this.txtShelfID.Text = "";
            this.txtShelfCode.Text = objShelf.GetNewShelfCode(this.txtAreaCode.Text);
            this.txtShelfName.Text = "";
            this.ddlActive.SelectedIndex = 0;
            this.txtMemo.Text = "";
            this.txtAreaType.Text = "";
            this.btnSave.Enabled = true;
            this.btnDelete.Enabled = false;
            string path = whcode + "/" + areacode + "/" + shelfCode;
            JScript.Instance.RegisterScript(this, "RefreshParent('" + path + "');");
        }
    }
}
