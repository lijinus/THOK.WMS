/****************************************************** 
FileName:MistakesPage
Copyright (c) 2004-2007 天海欧康科技信息（厦门）有限公司技术开发部
Writer:施建新

create Date:2007/11/01
Rewriter:施建新

Rewrite Date:2007/11/01
Impact:
Main Content（Function Name、parameters、returns）

******************************************************/
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
using THOK.System.BLL;

///<summary>
///Module ID：<模块编号，可以引用系统设计中的模块编号>
///Depiction：出错界面

///Author：施建新
///Create Date：2007-11-01
///</summary>
public partial class MistakesPage : System.Web.UI.Page
{
    //ExceptionLog explogObject = new ExceptionLog();
    ExceptionLog setLog = new ExceptionLog();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //ViewState["BackUrl"] = Request.UrlReferrer.ToString();
                try
                {
                    string strModuleName, strFunctionName, strExceptionalType, strExceptionalDescription;
                    strModuleName = Session["ModuleName"].ToString();
                    strFunctionName = Session["FunctionName"].ToString();
                    strExceptionalType = Session["ExceptionalType"].ToString();
                    strExceptionalDescription = Session["ExceptionalDescription"].ToString();
                    labModuleName.Text = strModuleName;
                    labFunctionName.Text = strFunctionName;
                    labExceptionalType.Text = strExceptionalType;
                    labExceptionalDescription.Text = strExceptionalDescription;

                    ////explogObject.ModuleName = strModuleName;
                    ////explogObject.FunctionName = strFunctionName;
                    ////explogObject.ExceptionalType = strExceptionalType;
                    ////explogObject.ExceptionalDescription = strExceptionalDescription;
                    ////explogObject.ExceptionLogHandling();

                    setLog.ModuleName = Session["ModuleName"].ToString();
                    setLog.FunctionName = Session["FunctionName"].ToString();
                    setLog.ExceptionalType = Session["ExceptionalType"].ToString();
                    setLog.ExceptionalDescription = Session["ExceptionalDescription"].ToString();
                    setLog.CatchTime = System.DateTime.Now;
                    setLog.Insert(setLog);
                }
                catch (Exception exp)
                {
                    JScript.Instance.ShowMessage(this, exp.Message);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}
