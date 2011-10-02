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

public partial class Navigation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string PersonName = "";
            if (Session["EmployeeName"] != null)
            {
                PersonName = Session["EmployeeName"].ToString();
            }
            string strScript = "    var timerRunning = false;\n " +
                                 "   function stopclock (){ \n" +
                                 "   if(timerRunning) \n" +
                                 "   clearTimeout(timerID);\n" +
                                 "   timerRunning = false; \n" +
                                 "   }\n " +
                                 "   function showtime () { \n" +
                                 "   var now = new Date(); \n" +
                                 "   var year=now.getYear(); \n " +
                                 "   var month=now.getMonth();\n" +
                                 "   var day=now.getDate();\n" +
                                 "   var hours = now.getHours(); \n" +
                                 "   var minutes = now.getMinutes(); \n" +
                                 "   var seconds = now.getSeconds() \n" +
                                 "   var timeValue=\" 当前登录用户: " + Session["G_user"].ToString()+"   "+PersonName+"  "+Session["Client_IP"].ToString() + "      \" \n" +
                                 "   timeValue +=  \"当前日期:\" +year+ \"年\"+ (month+1)+\"月\" +day+\"日\"  \n" +
                                 "   timeValue +=\"     \"+ ((hours >12) ? hours -12 :hours) \n" +
                                 "   timeValue += ((minutes < 10) ? \":0\" : \":\") + minutes \n" +
                                 "   timeValue += ((seconds < 10) ? \":0\" : \":\") + seconds \n" +
                                 "   timeValue += (hours >= 12) ? \" PM\" : \" AM\" \n" +
                                 "   window.status = timeValue; \n" +
                                 "   timerID = setTimeout(\"showtime()\",1000); \n" +
                                 "   timerRunning = true; \n" +
                                 "   } \n" +
                                 "   function startclock () { \n" +
                                 "     stopclock(); \n" +
                                 "     showtime(); \n" +
                                 "   } \n";
            JScript.Instance.RegisterScript(this, strScript);
        }
        catch (Exception exp)
        {
        }
    }
}
