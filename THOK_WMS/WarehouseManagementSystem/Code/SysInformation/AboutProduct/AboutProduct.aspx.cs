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
using System.Data.SqlClient;
using THOK.System.BLL;


public partial class main_Default : System.Web.UI.Page
{
    SysSystemParameter getParam = new SysSystemParameter();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = getParam.GetVersionParameter();
        }
        catch (SqlException exp)
        {
            Session["ModuleName"] = "关于模块";
            Session["FunctionName"] = "Page_Load";
            Session["ExceptionalType"] = exp.GetType().FullName;
            Session["ExceptionalDescription"] = exp.Message;
            Response.Redirect("MistakesPage.aspx");
        }

        LabVersion.Font.Size = FontUnit.Smaller;
        labCompany.Font.Size = FontUnit.Smaller;
        labCopyrigth.Font.Size = FontUnit.Smaller;
        labCompanyTelephone.Font.Size = FontUnit.Smaller;
        labCompanyFax.Font.Size = FontUnit.Smaller;
        labCompanyAddress.Font.Size = FontUnit.Smaller;
        labCompanyEmail.Font.Size = FontUnit.Smaller;
        lbtnCompanyEmail.Font.Size = FontUnit.Smaller;
        labCompanyWeb.Font.Size = FontUnit.Smaller;
        lbtnCompanyWeb.Font.Size = FontUnit.Smaller;
        lbtnQuit.Font.Size = FontUnit.Smaller;

        if (ds.Tables[0].Rows.Count > 0)
        {
           // LabSoftWareName.Text = ds.Tables[0].Rows[0][0].ToString();
            //LabSoftWareName.Font.Size = FontUnit.Smaller;
            LabVersion.Text = "软件版本:"+ds.Tables[0].Rows[0][1].ToString();
            LabVersion.Font.Size = FontUnit.Smaller;
            this.lblSoftwareName.Text = "软件名称:" + ds.Tables[0].Rows[0][0].ToString();
            this.lblSoftwareName.Font.Size = FontUnit.Smaller;
            
            labCompany.Text = "公司名称:"+ds.Tables[0].Rows[0][2].ToString();
            labCompany.Font.Size = FontUnit.Smaller;
            labCopyrigth.Text = ds.Tables[0].Rows[0][3].ToString();
            labCopyrigth.Font.Size = FontUnit.Smaller;
            labCompanyTelephone.Text = "公司电话:"+ds.Tables[0].Rows[0][4].ToString();
            labCompanyTelephone.Font.Size = FontUnit.Smaller;
            labCompanyFax.Text = "公司传真:"+ds.Tables[0].Rows[0][5].ToString();
            labCompanyFax.Font.Size = FontUnit.Smaller;
            labCompanyAddress.Text = "公司地址:"+ds.Tables[0].Rows[0][6].ToString();
            labCompanyAddress.Font.Size = FontUnit.Smaller;
            labCompanyEmail.Text = "公司邮件:";
            labCompanyEmail.Font.Size = FontUnit.Smaller;
            lbtnCompanyEmail.Text = ds.Tables[0].Rows[0][7].ToString();
            lbtnCompanyEmail.Font.Size = FontUnit.Smaller;
            labCompanyWeb.Text = "公司网址:";
            labCompanyWeb.Font.Size = FontUnit.Smaller;
            lbtnCompanyWeb.Text = ds.Tables[0].Rows[0][8].ToString();
            lbtnCompanyWeb.Font.Size = FontUnit.Smaller;
            
        }
    }



    protected void lbtnCompanyWeb_Click(object sender, EventArgs e)
    {
        string strScript = @"<SCRIPT LANGUAGE='javascript'> " + "window.open ('http://" + lbtnCompanyWeb.Text + "','_blank')" + "</SCRIPT>";
        Page.RegisterStartupScript("a1", strScript);
    }
    protected void lbtnCompanyEmail_Click(object sender, EventArgs e)
    {
        string strScript = @"<SCRIPT LANGUAGE='javascript'> " + "window.open ('mailto:" + lbtnCompanyEmail.Text + "','newwindow')" + "</SCRIPT>";
        Page.RegisterStartupScript("a1", strScript);
    }
    protected void lbtnSoftWareName_Click(object sender, EventArgs e)
    {

    }


    protected void lbtnVersion_Click(object sender, EventArgs e)
    {

    }

    protected void lbtnQuit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../../MainPage.aspx");
    }
}
