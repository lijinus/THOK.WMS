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

public partial class Admin_Navigation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        NavigationBind();
    }
    protected void NavigationBind()
    {

        if (Request["PG"] == null)
        {
            this.labNavigation.Text = "快速通道";
        }
        else
        {
            this.labNavigation.Text = Request["PG"].ToString().Replace("[","").Replace("]","");
        }
    }
}
