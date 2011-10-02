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
using THOK.System.BLL;

public partial class Code_SysInfomation_PwdModify_PwdModify :BasePage// System.Web.UI.Page
{
    Encryption encrypObject = new Encryption();
    SysUser objUser = new SysUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtUserName.Text = Session["G_user"].ToString();
        }
        catch (Exception ex)
        {
            txtUserName.Text = "";
        }
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        //DatabaseOperater dbOperator = new DatabaseOperater();
        
        //dbOperator.GetModelName = "sys_UserListlogin";
        //dbOperator.GetStrProcParaValue = txtUserName.Text + "," + encrypObject.EncryptMD5(); ;
        encrypObject.EncryptString = txtOldPwd.Text;
        DataSet ds = objUser.GetUserInfo(this.txtUserName.Text.Trim());//dbOperator.SelectData();

        if (ds.Tables[0].Rows[0]["UserPassword"].ToString() == encrypObject.EncryptMD5())
        {
            encrypObject.EncryptString=txtAckPwd.Text;
            if (objUser.ChangePassword(this.txtUserName.Text.Trim(),encrypObject.EncryptMD5()))
            {
                Response.Redirect("ModifyPwdSuccess.aspx");
            }
            else
                Response.Write("<script>alert(\"密码修改失败!\")</script>");
        }
        else
        {
            JScript.Instance.ShowMessage(this,"原密码错误!");
        }

        
    }
    public string getColorValue(string s)
    {
        if (Session["IsUseGlobalParameter"].ToString() == "1")
            return s;
        else return "";
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("../../../MainPage.aspx");
    }
}
