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

public partial class NavigationPage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        NavigationBind();
    }
    protected void NavigationBind()
    {
        //if (Session["IsFirstLogin"].ToString() == "1")
        //{
        //    this.labNavigation.Text = "快速通道";
        //}
        //else
        //{
        //    try
        //    {
        if(Request["PG"]==null)
        {
                this.labNavigation.Text = "快速通道";
        }
        else
        {
            this.labNavigation.Text = Request["PG"].ToString();
        }
            //}
            //catch(Exception e)
            //{
            //    Session["ModuleName"] = "导航页模块";
            //    Session["FunctionName"] = "NavigationBind()";
            //    Session["ExceptionalType"] = e.GetType().FullName;
            //    Session["ExceptionalDescription"] = e.Message;
            //    Response.Redirect("Common/MistakesPage.aspx");
            //}
        //}
    }
    public string getColorValue(string s)
    {
        return s;
    }
}