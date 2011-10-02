/****************************************************** 
FileName:BasicParSet
Copyright (c) 2004-2007 天海欧康科技信息（厦门）有限公司技术开发部
Writer:黄庆凤



create Date:2007/09/29
Rewriter：黄庆凤
Rewrite Date:2007/09/29
Impact:
Main Content（Function Name、parameters、returns）



******************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// 脚本提示信息
/// </summary>
public class JScript
{
    public static JScript Instance = new JScript();
	public JScript()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    static int p = 0;
    /// <summary>
    /// 信息提示框


    /// </summary>
    /// <param name="page">Page 对象</param>
    /// <param name="Message">提示信息</param>
    public void ShowMessage(Page page,string Message)
    {
        string str = "<script> "+
                     " alert('" + Message.Replace("'", "").Replace("\r", "").Replace("\n", "").Replace("\t", "") + "');\n"+
                     "</script>";
        page.ClientScript.RegisterStartupScript(page.GetType(), DateTime.Now.ToLongTimeString(), str);
    }

    /// <summary>
    /// 信息提示框
    /// </summary>
    /// <param name="p">UpdatePanel</param>
    /// <param name="Message">提示内容</param>
    public void ShowMessage(UpdatePanel p, string Message)
    {
        Message = Message.Replace("'", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");
        ScriptManager.RegisterStartupScript(p, typeof(UpdatePanel), "test", string.Format("alert('{0}');", Message), true);
    }

    public void RegisterScript(Page page, string ScriptContent)
    {
        
        string str = "<script> \n " +ScriptContent+" \n </script>";
        //page.RegisterStartupScript("s1",str);
        page.ClientScript.RegisterStartupScript(page.GetType(), DateTime.Now.ToLongTimeString(), str);
    }
    public void RegisterScript(UpdatePanel p, string ScriptContent)
    {

        string str = "<script> \n " + ScriptContent + " \n </script>";
        ScriptManager.RegisterClientScriptBlock(p, typeof(UpdatePanel), "key", str, true);
    }

    //陈添
    public void NavigationInfo(Page page, string operation,string NewPageUrl,bool IsModal)
    {
        string strScript = "<script> ";
        if (!IsModal)
        {
            strScript += " var nav=window.parent.frames.Navigation.document.getElementById(\'labNavigation\');";
            strScript += " var ary=nav.innerText.split(\'>>\');";
            strScript += " nav.innerText=ary[0]+\'>>\'+ary[1]+ \'>>" + operation + "\';";
        }
        strScript += "window.open(\'" + NewPageUrl + "\',\'_self\');";
        strScript+= "</script>";
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), DateTime.Now.ToLongTimeString(), strScript);
    }
    //陈添
    public void BackToTwoLevel(Page page,string NewPageUrl, bool IsModal)
    {
        string strScript = "<script> ";
        if (!IsModal)
        {
            strScript += " var nav=window.parent.frames.Navigation.document.getElementById(\'labNavigation\');";
            strScript += " var ary=nav.innerText.split(\'>>\');";
            strScript += " nav.innerText=ary[0]+\'>>\'+ary[1];";
        }
        strScript += "window.open(\'" + NewPageUrl + "\',\'_self\');";
        strScript += "</script>";
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), DateTime.Now.ToLongTimeString(), strScript);
    }
    //施加
    public void SaveBack(Page page, string strText,/* string NewPageUrl,*/ bool IsModal)
    {
        string strScript = strText;
        if (!IsModal)
        {
            strScript += " var nav=window.parent.frames.Navigation.document.getElementById(\'labNavigation\');";
            strScript += " var ary=nav.innerText.split(\'>>\');";
            strScript += " nav.innerText=ary[0]+\'>>\'+ary[1];";
        }
        //strScript += "window.open(\'" + NewPageUrl + "\',\'_self\');";
        strScript += "</script>";
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), DateTime.Now.ToLongTimeString(), strScript);
    }
    //陈添
    public void RegisterAjaxScript(Page page, string strControlID, string strRequestDate, string strProcessUrl,string strGetRequestDate,int iRow, int iCell,string strErrMsg)
    {
        string strtime2 = DateTime.Now.Millisecond.ToString() + p.ToString();
        string strScript = "";
        strScript += "<script>";
        strScript += "var xmlHttp;";
        strScript += "function createxmlhttp()";
        strScript += "{";
        strScript += "if (window.ActiveXObject)";
        strScript += "{";
        strScript += "xmlHttp = new ActiveXObject(\"Microsoft.XMLHTTP\");";
        strScript += "}";
        strScript += "if (window.XMLHttpRequest)";
        strScript += "{";
        strScript += "xmlHttp = new XMLHttpRequest();";
        strScript += "}";
        strScript += "}";
        strScript += "function " + strRequestDate + "(obj2)";
        strScript += "{";
        strScript += "obj=obj2;";
        strScript += "createxmlhttp();";
        strScript += " var url;";
        string strtime = DateTime.Now.Millisecond.ToString() + DateTime.Now.Minute.ToString();
        strScript += "  url=\"" + strProcessUrl + "\"+document.form1." + strControlID + ".value+" + "\"&time=\"+" + strtime + ";";
        strScript += "  xmlHttp.open(\"GET\",url,true);";
        strScript += "  xmlHttp.onreadystatechange=" + strGetRequestDate + ";";
        strScript += "  xmlHttp.send(null);";

        strScript += "}";

        strScript += "function " + strGetRequestDate+"()";
        strScript += "{";

        strScript += "  if (xmlHttp.readyState==4)";
        strScript += "      {";
        strScript += "          if(xmlHttp.status==200)";
        strScript += "              {";
        strScript += "                  var arrPar=xmlHttp.responseText.split(\',\');";
        strScript += "                  if(arrPar[0]==\"1\")";
        strScript += "                      {";
        strScript += "                      obj.bSave=\"0\";";
        strScript += "                  document.getElementById(\'" + iRow.ToString() + (iCell + 1).ToString() + "\')" + ".innerHTML=\'" + strErrMsg + "\';";
        strScript += "                          document.getElementById(\'" + strControlID + "\').className=\'UserNameError\';";
        strScript += "                      }";
        strScript += "                  if(arrPar[0]==\"0\")";
        strScript += "                      {";
        strScript += "                      obj.bSave=\"1\";";
        strScript += "                  document.getElementById(\'" + iRow.ToString() + (iCell + 1).ToString() + "\')" + ".innerHTML=\'\';";
        strScript += "                          document.getElementById(\'" + strControlID + "\').className=\'UserNameCorrect\';";
        strScript += "                      }";
        strScript += "               }";
        strScript += "      }";
        strScript += "}";
        strScript += "</script>";
        p++;
        //page.ClientScript.RegisterClientScriptBlock(page.GetType(), DateTime.Now.ToLongTimeString(), strScript);
        page.RegisterClientScriptBlock(strtime2, strScript);
    }
    //陈添
    public void RegisterAjaxScript(Page page, string strControlID, string strRequestDate, string strProcessUrl, string strGetRequestDate, int iRow, int iCell, string strErrMsg,string OldID)
    {
        string strtime2 = DateTime.Now.Millisecond.ToString()+p.ToString();
        string strScript = "";
        strScript += "<script>";
        strScript += "var xmlHttp;";
        strScript += "var oldID="+OldID+";";
        strScript += "var obj;";
        strScript += "function createxmlhttp()";
        strScript += "{";
        strScript += "if (window.ActiveXObject)";
        strScript += "{";
        strScript += "xmlHttp = new ActiveXObject(\"Microsoft.XMLHTTP\");";
        strScript += "}";
        strScript += "if (window.XMLHttpRequest)";
        strScript += "{";
        strScript += "xmlHttp = new XMLHttpRequest();";
        strScript += "}";
        strScript += "}";
        strScript += "function " + strRequestDate+"(obj2)";
        strScript += "{";
        strScript += "obj=obj2;";
        strScript += "createxmlhttp();";
        strScript += " var url;";
        string strtime = DateTime.Now.Millisecond.ToString() + DateTime.Now.Minute.ToString();
        strScript += "  url=\"" + strProcessUrl + "\"+document.form1." + strControlID + ".value+" + "\"&time=\"+" + strtime + ";";
        strScript += "  xmlHttp.open(\"GET\",url,true);";
        strScript += "  xmlHttp.onreadystatechange=" + strGetRequestDate + ";";
        strScript += "  xmlHttp.send(null);";

        strScript += "}";

        strScript += "function " + strGetRequestDate + "()";
        strScript += "{";

        strScript += "  if (xmlHttp.readyState==4)";
        strScript += "      {";
        strScript += "          if(xmlHttp.status==200)";
        strScript += "              {";
        strScript += "                  var arrPar=xmlHttp.responseText.split(\',\');";
        strScript += "                  if(arrPar[0]==\"1\"&&oldID!=arrPar[1])";
        strScript += "                      {";
        strScript += "                      obj.bSave=\"0\";";
        strScript += "                  document.getElementById(\'" + iRow.ToString() + (iCell + 1).ToString() + "\')" + ".innerHTML=\'" + strErrMsg + "\';";
        strScript += "                          document.getElementById(\'" + strControlID + "\').className=\'UserNameError\';";
        strScript += "                      }";
        strScript += "                  if(arrPar[0]==\"0\")";
        strScript += "                      {";
        strScript += "                      obj.bSave=\"1\";";
        strScript += "                  document.getElementById(\'" + iRow.ToString() + (iCell + 1).ToString() + "\')" + ".innerHTML=\'\';";
        strScript += "                          document.getElementById(\'" + strControlID + "\').className=\'UserNameCorrect\';";
        strScript += "                      }";
        strScript += "               }";
        strScript += "      }";
        strScript += "}";
        strScript += "</script>";
        p++;
        //page.ClientScript.RegisterClientScriptBlock(page.GetType(), rd.Next(10000).ToString(), strScript);
        page.RegisterClientScriptBlock(strtime2, strScript);
    }
}
