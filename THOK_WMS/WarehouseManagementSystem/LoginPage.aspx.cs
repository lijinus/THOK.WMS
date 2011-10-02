/****************************************************** 
FileName:Login  
Copyright (c) 2004-2007 天海欧康科技信息（厦门）有限公司技术开发部
Writer:施建新
create Date:2007/08/16
Rewriter:黄庆凤
Rewrite Date:2008/12/03
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
using System.Data.SqlClient;
using THOK.System.BLL;
using THOK.WMS.BLL;

///<summary>
///Module ID：<模块编号，可以引用系统设计中的模块编号>
///Depiction：系统登录模块
///Author：施建新
///Create Date：2007-08-16
///</summary>
public partial class LoginPage : System.Web.UI.Page
{
    Encryption encrypObject = new Encryption();
    OperatorLog setLog = new OperatorLog();
    SysUser getUser = new SysUser();
    protected void Page_Load(object sender, EventArgs e)
    {
       // ibtnConCode.ImageUrl = "LoginCheckPicture.aspx";
        //this.txtConCode.Attributes.Add("onblur", "GotoLogin();");
        if (Request.QueryString["Logout"] != null)
        {
            string strUserName;
            if (Session["G_user"] != null)
            {
                strUserName = Session["G_user"].ToString();
            }
            else
            {
                strUserName = "";
            }
            HttpContext.Current.Cache.Remove(strUserName);
            GC.Collect();
        }

    }

    //#region 判断验证码

    ///// <summary>
    ///// 判断验证码

    ///// </summary>
    ///// <returns></returns>
    //protected bool Check_Code()
    //{
    //    if (String.Compare(Session["CheckCode"].ToString(), txtConCode.Text, true) == 0)  //Request.Cookies["CheckCode"].Value
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    //#endregion

    #region 控件事件
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        if (txtUserName.Text.Trim() != "")
        {
            try
            {
                string key = txtUserName.Text.ToLower();               
                string UserCache = Convert.ToString(Cache[key]);

                if (UserCache == null || UserCache == string.Empty || Cache[key].ToString() == Page.Request.UserHostAddress)
                {
                    encrypObject.EncryptString = txtPassWord.Text.Trim();
                    DataSet dstUserList = getUser.GetUserInfo(txtUserName.Text.Trim());
                    if (dstUserList.Tables.Count > 0 && dstUserList.Tables[0].Rows.Count > 0)
                    {
                        if (dstUserList.Tables[0].Rows[0]["UserPassword"].ToString().Trim() == encrypObject.EncryptMD5())
                        {
                            //Session["SkinFileName"] = dstUserList.Tables[0].Rows[0]["ConfigName"].ToString();
                            Session["UserID"] = dstUserList.Tables[0].Rows[0]["UserID"].ToString();
                            Session["GroupID"] = dstUserList.Tables[0].Rows[0]["GroupID"].ToString();
                            Session["G_user"] = txtUserName.Text.ToLower();
                            Session["Client_IP"] = Page.Request.UserHostAddress;
                            //Session["IsFirstLogin"] = "1";
                            string EmployeeCode = dstUserList.Tables[0].Rows[0]["EmployeeCode"].ToString();
                            if (EmployeeCode != "")
                            {
                                Session["EmployeeCode"] = EmployeeCode;
                                Employee getEmp = new Employee();
                                DataSet dt = getEmp.QueryEmployee(1, 1, "EMPLOYEECODE='" + EmployeeCode + "'", "EMPLOYEECODE");
                                if (dt.Tables[0].Rows.Count == 1)
                                {
                                    Session["EmployeeName"] = dt.Tables[0].Rows[0]["EMPLOYEENAME"].ToString();
                                }
                                else
                                {
                                    Session["EmployeeName"] = "";
                                }
                            }
                            else
                            {
                                Session["EmployeeCode"] = EmployeeCode;
                                Session["EmployeeName"] = "";
                            }

                            //个人显示设置,WUQH添加
                            Session["sys_PageCount"] = dstUserList.Tables[0].Rows[0]["sys_PageCount"].ToString();
                            Session["grid_ColumnTitleFont"] = dstUserList.Tables[0].Rows[0]["grid_ColumnTitleFont"].ToString();
                            Session["grid_ContentFont"] = dstUserList.Tables[0].Rows[0]["grid_ContentFont"].ToString();

                            Session["grid_ColumnTextAlign"] = dstUserList.Tables[0].Rows[0]["grid_ColumnTextAlign"].ToString();
                            Session["grid_ContentTextAlign"] = dstUserList.Tables[0].Rows[0]["grid_ContentTextAlign"].ToString();
                            Session["grid_NumberColumnAlign"] = dstUserList.Tables[0].Rows[0]["grid_NumberColumnAlign"].ToString();
                            Session["grid_MoneyColumnAlign"] = dstUserList.Tables[0].Rows[0]["grid_MoneyColumnAlign"].ToString();
                            Session["grid_SelectMode"] = dstUserList.Tables[0].Rows[0]["grid_SelectMode"].ToString();
                            Session["grid_IsRefreshBeforeAdd"] = dstUserList.Tables[0].Rows[0]["grid_IsRefreshBeforeAdd"].ToString();
                            Session["grid_IsRefreshBeforeUpdate"] = dstUserList.Tables[0].Rows[0]["grid_IsRefreshBeforeUpdate"].ToString();
                            Session["grid_IsRefreshBeforeDelete"] = dstUserList.Tables[0].Rows[0]["grid_IsRefreshBeforeDelete"].ToString();

                            Session["grid_OddRowColor"] = dstUserList.Tables[0].Rows[0]["grid_OddRowColor"].ToString();
                            Session["grid_EvenRowColor"] = dstUserList.Tables[0].Rows[0]["grid_EvenRowColor"].ToString();
                            Session["sys_PrintForm"] = dstUserList.Tables[0].Rows[0]["sys_PrintForm"].ToString();
                            string pager_ShowPageIndex= dstUserList.Tables[0].Rows[0]["pager_ShowPageIndex"].ToString();
                            if (pager_ShowPageIndex == "")
                            {
                                Session["pager_ShowPageIndex"] = true;
                            }
                            else
                            {
                                Session["pager_ShowPageIndex"] = dstUserList.Tables[0].Rows[0]["pager_ShowPageIndex"].ToString();
                            }
                            Session.Timeout = int.Parse(dstUserList.Tables[0].Rows[0]["SessionTimeOut"].ToString());

                            TimeSpan stLogin = new TimeSpan(0, 0, System.Web.HttpContext.Current.Session.Timeout, 0, 0);
                            HttpContext.Current.Cache.Insert(key, Page.Request.UserHostAddress, null, DateTime.MaxValue, stLogin, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                            //if (!Check_Code())
                            //{
                            //    // Response.Write("<script>alert(\"验证码错误，请输入正确的验证码.\");</script>");
                            //    labMessage.Text = "验证码错误，请输入正确的验证码!";
                            //    return;
                            //}
                           // setLog.InsertOperationLog(System.DateTime.Now, this.txtUserName.Text.Trim(), "登录页面", "登录(成功)");
                            string strScript = @"<SCRIPT LANGUAGE='javascript'> " + "window.opener=null;window.open ('MDIPage.aspx','newwindow','top=0,left=0,depended=no,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=yes');window.opener=null;window.close();" + "</SCRIPT>";
                            //string strScript = @"<SCRIPT LANGUAGE='javascript'> " + "window.opener=null;window.open ('MDIPage.aspx','_blank','height=680,width=1014,top=0,left=0,depended=no,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=yes');window.close();" + "</SCRIPT>";
                            Page.RegisterStartupScript("a1", strScript);
                        }
                        else
                        {
                            setLog.InsertOperationLog(System.DateTime.Now, this.txtUserName.Text.Trim(), "登录页面", "登录(用户密码有误)");
                            labMessage.Text = "对不起,您输入的密码有误!";
                        }
                    }
                    else
                    {
                        labMessage.Text = "对不起,您输入的用户名不存在!";
                    }
                }
                else
                {
                    labMessage.Text = "对不起,该帐号已经有人登录!请与管理员联系!";
                }

            }
            catch (Exception exp)
            {
                Session["ModuleName"] = "登录界面";
                Session["FunctionName"] = "btnLogin_Click";
                Session["ExceptionalType"] = exp.GetType().FullName;
                Session["ExceptionalDescription"] = exp.Message;
                Response.Redirect("Common/MistakesPage.aspx");
            }
        }
        else
        {
            labMessage.Text = "请输入用户名!";
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        string strScript = @"<SCRIPT LANGUAGE='javascript'> " + "window.opener=null;window.close();" + "</SCRIPT>";
        Page.RegisterStartupScript("a2", strScript);
    }
#endregion
}
