using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using THOK.WMS;
using THOK.WMS.Download.Bll;


public partial class Code_StockEntry_Server : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["searchText"] != null)
        {
            if (Request.QueryString["searchText"].ToString().Trim().Length > 0)
            {
                #region
                DataTable dt = new DataTable();
                DownProductBll product = new DownProductBll();
                dt = product.GetProductCode(Request.QueryString["searchText"]);
                string returnText = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        returnText += dt.Rows[i][0].ToString() + "\n";
                    }
                }

                Response.Write(returnText);
                #endregion
            }
        }
    }
}