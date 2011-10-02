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
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Data.SqlClient;
using THOK.Util;
using THOK.System.BLL;
public partial class Code_SysInfomation_SystemInf_Default :  BasePage
{
    SysSystemParameter objParam = new SysSystemParameter();
    public string DoubleLineColor;
    public string SingleLineColor;
    public string[] HeadFont=new string[4];
    public string[] TableFont=new string[4];
    
    protected void Page_Load(object sender, EventArgs e)
    {
        txtSys_PageCount.Attributes.Add("onkeypress", "if(!/[0-9]/.test(String.fromCharCode(event.keyCode)))event.keyCode=0");
        txtSys_PageCount.Attributes.Add("onpaste", "return   false");
        txtSys_PageCount.Attributes.Add("ondragenter", "return   false");

        //限制txtSys_BufferCache输入的文本
        //txtSys_BufferCache.Attributes.Add("onkeypress", "if(!/[0-9]/.test(String.fromCharCode(event.keyCode)))event.keyCode=0");
        //txtSys_BufferCache.Attributes.Add("onpaste", "return   false");
        //txtSys_BufferCache.Attributes.Add("ondragenter", "return   false");
        //禁止修改文本（字体设置文本框）
        this.txtGrid_ContentFont.Attributes.Add("onfocus", "LostFocus(this);");
        this.txtGrid_ColumnTitleFont.Attributes.Add("onfocus", "LostFocus(this);");

        
        if (!IsPostBack)
        {
            ddlGrid_DisplayRowColorBind();
            ControlsDataBind();
        }

        if (Session["IsUseGlobalParameter"].ToString() == "1")
        {
            DoubleLineColor = Session["grid_EvenRowColor"].ToString();//dc.GetFieldValue("sys_SystemParameter", "ParameterValue", "ParameterName", "grid_EvenRowColor");
            SingleLineColor = Session["grid_OddRowColor"].ToString();//dc.GetFieldValue("sys_SystemParameter", "ParameterValue", "ParameterName", "grid_OddRowColor");
        }
        else
        {
            DoubleLineColor = "";
            SingleLineColor = "";
        }
    }

    #region 数据绑定   2007-09-25 Authored by Huang
    public void ControlsDataBind()
    {
        try
        {
            #region 分页设置
            this.txtSys_PageCount.Text = Session["sys_PageCount"].ToString();
            this.ddl_ShowPageIndex.SelectedValue = Session["pager_ShowPageIndex"].ToString();
            #endregion

            this.txtGrid_ColumnTitleFont.Text = Session["grid_ColumnTitleFont"].ToString();
            for (int i = 0; i < 4; i++)
            {
                HeadFont[i] = this.txtGrid_ColumnTitleFont.Text.Split(',')[i];
            }

            this.txtGrid_ContentFont.Text = Session["grid_ContentFont"].ToString();
            for (int i = 0; i < 4; i++)
            {

                TableFont[i] = this.txtGrid_ContentFont.Text.Split(',')[i];
            }

            DataSet dsAlign = objParam.GetOptionParameter("ddl_AlignMode");
            this.ddlGrid_ContentTextAlign.DataSource = dsAlign.Tables[0];
            this.ddlGrid_ContentTextAlign.DataTextField = "ParameterText";
            this.ddlGrid_ContentTextAlign.DataValueField = "ParameterValue";
            this.ddlGrid_ContentTextAlign.DataBind();
            this.ddlGrid_ContentTextAlign.SelectedValue = Session["grid_ContentTextAlign"].ToString();

            this.ddlGrid_ColumnTextAlign.DataSource = dsAlign.Tables[0];
            this.ddlGrid_ColumnTextAlign.DataTextField = "ParameterText";
            this.ddlGrid_ColumnTextAlign.DataValueField = "ParameterValue";
            this.ddlGrid_ColumnTextAlign.DataBind();
            this.ddlGrid_ColumnTextAlign.SelectedValue = Session["grid_ColumnTextAlign"].ToString();

            this.ddlGrid_NumberColumnAlign.DataSource = dsAlign.Tables[0];
            this.ddlGrid_NumberColumnAlign.DataTextField = "ParameterText";
            this.ddlGrid_NumberColumnAlign.DataValueField = "ParameterValue";
            this.ddlGrid_NumberColumnAlign.DataBind();
            this.ddlGrid_NumberColumnAlign.SelectedValue = Session["grid_NumberColumnAlign"].ToString();


            this.ddlGrid_MoneyColumnAlign.DataSource = dsAlign.Tables[0];
            this.ddlGrid_MoneyColumnAlign.DataTextField = "ParameterText";
            this.ddlGrid_MoneyColumnAlign.DataValueField = "ParameterValue";
            this.ddlGrid_MoneyColumnAlign.DataBind();
            this.ddlGrid_MoneyColumnAlign.SelectedValue = Session["grid_MoneyColumnAlign"].ToString();

            DataSet dsWhether = objParam.GetOptionParameter("ddl_Whether");

            this.ddlGrid_IsRefreshBeforeAdd.DataSource = dsWhether.Tables[0];
            this.ddlGrid_IsRefreshBeforeAdd.DataTextField = "ParameterText";
            this.ddlGrid_IsRefreshBeforeAdd.DataValueField = "ParameterValue";
            this.ddlGrid_IsRefreshBeforeAdd.DataBind();
            if (Session["grid_IsRefreshBeforeAdd"].ToString() == "1")
            {
                this.ddlGrid_IsRefreshBeforeAdd.SelectedValue = "1";
            }
            else
            {
                this.ddlGrid_IsRefreshBeforeAdd.SelectedValue = "0";
            }


            this.ddlGrid_IsRefreshBeforeUpdate.DataSource = dsWhether.Tables[0];
            this.ddlGrid_IsRefreshBeforeUpdate.DataTextField = "ParameterText";
            this.ddlGrid_IsRefreshBeforeUpdate.DataValueField = "ParameterValue";
            this.ddlGrid_IsRefreshBeforeUpdate.DataBind();
            if (Session["grid_IsRefreshBeforeUpdate"].ToString() == "1")
            {
                this.ddlGrid_IsRefreshBeforeUpdate.SelectedValue = "1";
            }
            else
            {
                this.ddlGrid_IsRefreshBeforeUpdate.SelectedValue = "0";
            }

            this.ddlGrid_IsRefreshBeforeDelete.DataSource = dsWhether.Tables[0];
            this.ddlGrid_IsRefreshBeforeDelete.DataTextField = "ParameterText";
            this.ddlGrid_IsRefreshBeforeDelete.DataValueField = "ParameterValue";
            this.ddlGrid_IsRefreshBeforeDelete.DataBind();
            if (Session["grid_IsRefreshBeforeDelete"].ToString() == "1")
            {
                this.ddlGrid_IsRefreshBeforeDelete.SelectedValue = "1";
            }
            else
            {
                this.ddlGrid_IsRefreshBeforeDelete.SelectedValue = "0";
            }


            this.ddlGrid_SelectMode.DataSource = objParam.GetOptionParameter("ddl_SelectMode").Tables[0];
            this.ddlGrid_SelectMode.DataTextField = "ParameterText";
            this.ddlGrid_SelectMode.DataValueField = "ParameterValue";
            this.ddlGrid_SelectMode.DataBind();
            if (Session["grid_SelectMode"].ToString() == "1")//(drPar["ParameterValue"].ToString() == "0")
            {
                this.ddlGrid_SelectMode.SelectedValue = "0";
            }
            else
            {
                this.ddlGrid_SelectMode.SelectedValue = "1";
            }

            this.ddlPrintForm.DataSource = objParam.GetOptionParameter("ddl_PrintForm").Tables[0] ;
            this.ddlPrintForm.DataTextField = "ParameterText";
            this.ddlPrintForm.DataValueField = "ParameterValue";
            this.ddlPrintForm.DataBind();
            if (Session["sys_PrintForm"].ToString() == "1")//(drPar["ParameterValue"].ToString() == "0")
            {
                this.ddlPrintForm.SelectedValue = "1";
            }
            else
            {
                this.ddlPrintForm.SelectedValue = "0";
            }

        }
        catch (Exception e)
        {
            Session["ModuleName"] = "系统信息参数设置";
            Session["FunctionName"] = "ControlsDataBind";
            Session["ExceptionalType"] = e.GetType().FullName;
            Session["ExceptionalDescription"] = e.Message;
            Response.Redirect("~/Common/MistakesPage.aspx");
        }


        if (Session["IsUseGlobalParameter"].ToString() == "1")
        {
            DoubleLineColor = Session["grid_EvenRowColor"].ToString();//DoubleLineColor = dc.GetFieldValue("sys_SystemParameter", "ParameterValue", "ParameterName", "grid_EvenRowColor");
            SingleLineColor = Session["grid_OddRowColor"].ToString();//SingleLineColor = dc.GetFieldValue("sys_SystemParameter", "ParameterValue", "ParameterName", "grid_OddRowColor");
        }
        else
        {
            DoubleLineColor = "";
            SingleLineColor = "";
        }
        for (int i = 0; i < 4; i++)
        {
            HeadFont[i] = this.txtGrid_ColumnTitleFont.Text.Split(',')[i];
            TableFont[i] = this.txtGrid_ContentFont.Text.Split(',')[i];

        }
        if (HeadFont[3] == "加粗")
            HeadFont[3] = "bold";
        if (TableFont[3] == "加粗")
            TableFont[3] = "bolder";

    }
    #endregion  初始化结束

    #region 保存设置的数据   2007-09-26
    public void SaveData()
    {
        //个人显示设置,WUQH添加
        Session["sys_PageCount"] = this.txtSys_PageCount.Text;
        Session["grid_ColumnTitleFont"] = this.txtGrid_ColumnTitleFont.Text;
        Session["grid_ContentFont"] = this.txtGrid_ContentFont.Text;
        Session["grid_ColumnTextAlign"] = this.ddlGrid_ColumnTextAlign.SelectedValue;
        Session["grid_ContentTextAlign"] = this.ddlGrid_ContentTextAlign.SelectedValue;
        Session["grid_NumberColumnAlign"] = this.ddlGrid_NumberColumnAlign.SelectedValue;
        Session["grid_MoneyColumnAlign"] = this.ddlGrid_MoneyColumnAlign.SelectedValue;
        Session["grid_SelectMode"] = this.ddlGrid_SelectMode.SelectedValue;
        Session["grid_OddRowColor"] = this.ddlGrid_DisplayRowColor.SelectedValue;
        Session["grid_EvenRowColor"] = this.ddlGrid_DisplayRowColorEven.SelectedValue;
        Session["grid_IsRefreshBeforeAdd"] = this.ddlGrid_IsRefreshBeforeAdd.SelectedValue;
        Session["grid_IsRefreshBeforeUpdate"] = this.ddlGrid_IsRefreshBeforeUpdate.SelectedValue;
        Session["grid_IsRefreshBeforeDelete"] = this.ddlGrid_IsRefreshBeforeDelete.SelectedValue;
        Session["sys_PrintForm"] = this.ddlPrintForm.SelectedValue;
        Session["pager_ShowPageIndex"] = this.ddl_ShowPageIndex.SelectedValue;
        
        StoredProcParameter param = new StoredProcParameter();
        param.Names.Add("UserID");
        param.Names.Add("sys_PageCount");
        param.Names.Add("grid_ColumnTitleFont");
        param.Names.Add("grid_ContentFont");
        param.Names.Add("grid_ColumnTextAlign");
        param.Names.Add("grid_ContentTextAlign");
        param.Names.Add("grid_NumberColumnAlign");
        param.Names.Add("grid_MoneyColumnAlign");
        param.Names.Add("grid_SelectMode");
        param.Names.Add("grid_OddRowColor");
        param.Names.Add("grid_EvenRowColor");
        param.Names.Add("grid_IsRefreshBeforeAdd");
        param.Names.Add("grid_IsRefreshBeforeUpdate");
        param.Names.Add("grid_IsRefreshBeforeDelete");
        param.Names.Add("sys_PrintForm");
        param.Names.Add("pager_ShowPageIndex");
        param.Values.Add(Session["UserID"].ToString());
        param.Values.Add(this.txtSys_PageCount.Text);
        param.Values.Add(this.txtGrid_ColumnTitleFont.Text);
        param.Values.Add(this.txtGrid_ContentFont.Text);
        param.Values.Add(this.ddlGrid_ColumnTextAlign.SelectedValue);
        param.Values.Add(this.ddlGrid_ContentTextAlign.SelectedValue);
        param.Values.Add(this.ddlGrid_NumberColumnAlign.SelectedValue);
        param.Values.Add(this.ddlGrid_MoneyColumnAlign.SelectedValue);
        param.Values.Add(this.ddlGrid_SelectMode.SelectedValue);
        param.Values.Add(this.ddlGrid_DisplayRowColor.SelectedValue);
        param.Values.Add(this.ddlGrid_DisplayRowColorEven.SelectedValue);
        param.Values.Add(this.ddlGrid_IsRefreshBeforeAdd.SelectedValue);
        param.Values.Add(this.ddlGrid_IsRefreshBeforeUpdate.SelectedValue);
        param.Values.Add(this.ddlGrid_IsRefreshBeforeDelete.SelectedValue);
        param.Values.Add(this.ddlPrintForm.SelectedValue);
        param.Values.Add(this.ddl_ShowPageIndex.SelectedValue);
        for (int i = 0; i < 16; i++)
        {
            param.Types.Add(DbType.String);
        }
        bool bSaveSucess = false;
        try
        {
            objParam.UpdateSystemParameter("sys_UserSysInfoAddNew", param);
            bSaveSucess = true;
        }
        catch (SqlException e)
        {
            Session["ModuleName"] = " 系统信息参数设置";
            Session["FunctionName"] = "ControlsDataBind";
            Session["ExceptionalType"] = e.GetType().FullName;
            Session["ExceptionalDescription"] = e.Message;
            Response.Redirect("~/Common/MistakesPage.aspx");
        }
        if (bSaveSucess)
        {
            JScript.Instance.ShowMessage(this, "保存成功！");
        }
        else
        {
            JScript.Instance.ShowMessage(this, "保存失败！");
        }
        if (Session["IsUseGlobalParameter"].ToString() == "1")
        {
            DoubleLineColor = Session["grid_EvenRowColor"].ToString();//DoubleLineColor = dc.GetFieldValue("sys_SystemParameter", "ParameterValue", "ParameterName", "grid_EvenRowColor");
            SingleLineColor = Session["grid_OddRowColor"].ToString();//SingleLineColor = dc.GetFieldValue("sys_SystemParameter", "ParameterValue", "ParameterName", "grid_OddRowColor");
        }
        else
        {
            DoubleLineColor = "";
            SingleLineColor = "";
        }
        for (int i = 0; i < 4; i++)
        {
            HeadFont[i] = this.txtGrid_ColumnTitleFont.Text.Split(',')[i];
            TableFont[i] = this.txtGrid_ContentFont.Text.Split(',')[i];
        }
        if (HeadFont[3] == "加粗")
            HeadFont[3] = "bold";
        if (TableFont[3] == "加粗")
            TableFont[3] = "bolder";
    }
    #endregion 


    

    #region ddlGrid_DisplayRowColor数据绑定
    protected void ddlGrid_DisplayRowColorBind()
    {
        ddlGrid_DisplayRowColor.Items.Clear();
        ddlGrid_DisplayRowColorEven.Items.Clear();
        string[] colorArray = Enum.GetNames(typeof(System.Drawing.KnownColor));
        foreach (string color in colorArray)
        {
            ListItem item = new ListItem(color);
            item.Attributes.Add("style", "color:" + color);
            ListItem item2 = new ListItem(color);
            item2.Attributes.Add("style", "color:" + color);

            ddlGrid_DisplayRowColor.Items.Add(item);
            ddlGrid_DisplayRowColorEven.Items.Add(item2);
        }
        int row;
        for (row = 0; row < ddlGrid_DisplayRowColor.Items.Count - 1; row++)
        {
            ddlGrid_DisplayRowColor.Items[row].Attributes.Add("style", "background-color:" + ddlGrid_DisplayRowColor.Items[row].Value);
            ddlGrid_DisplayRowColorEven.Items[row].Attributes.Add("style", "background-color:" + ddlGrid_DisplayRowColorEven.Items[row].Value);
        }
        ddlGrid_DisplayRowColor.BackColor = Color.FromName(ddlGrid_DisplayRowColor.SelectedItem.Text);
        ddlGrid_DisplayRowColorEven.BackColor = Color.FromName(ddlGrid_DisplayRowColorEven.SelectedItem.Text);

        ddlGrid_DisplayRowColor.SelectedValue = Session["grid_OddRowColor"].ToString();
        ddlGrid_DisplayRowColorEven.SelectedValue = Session["grid_EvenRowColor"].ToString();
    }
    #endregion


    #region 事件处理



    /// </summary>
    /// <param name="ddl">DropDownList对象</param>
    /// <returns></returns>
    protected string Ddlselectvalue(DropDownList ddl)
    {
        if (ddl.SelectedValue == "是")
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }
    protected string Ddlselectvalue1(DropDownList ddl)
    {
        if (ddl.SelectedValue == "整行选中")
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();   //新的保存方法  
        ddlGrid_DisplayRowColorBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/MainPage.aspx");
    }
    protected void btnGrid_ColumnTitleFont_Click(object sender, EventArgs e)
    {
        string strTitleFont = this.txtGrid_ColumnTitleFont.Text;
        string strScript = @"<SCRIPT LANGUAGE='javascript'> " + "var strTemp=window.showModalDialog ('../../../Common/FontSelectModalFrame.aspx','" + strTitleFont + "','top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=600px;dialogHeight=200px');" + "if (strTemp==undefined) strTemp=document.all.txtGrid_ColumnTitleFont.value;document.all.txtGrid_ColumnTitleFont.value=strTemp;" + "</SCRIPT>";
        Page.RegisterStartupScript("a1", strScript);
        for (int i = 0; i < 4; i++)
        {
            HeadFont[i] = this.txtGrid_ColumnTitleFont.Text.Split(',')[i];
            TableFont[i] = this.txtGrid_ContentFont.Text.Split(',')[i];
            
        }
        if (HeadFont[3] == "加粗")
            HeadFont[3] = "bold";
        if (TableFont[3] == "加粗")
            TableFont[3] = "bolder";
        ddlGrid_DisplayRowColorBind();
    }
    protected void btnGrid_ContentFont_Click(object sender, EventArgs e)
    {
        string strContentFont = this.txtGrid_ContentFont.Text;
        string strScript = @"<SCRIPT LANGUAGE='javascript'> " + "var strTemp=window.showModalDialog ('../../../Common/FontSelectModalFrame.aspx','" + strContentFont + "','top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=600px;dialogHeight=200px');" + "if (strTemp==undefined) strTemp=document.all.txtGrid_ContentFont.value;document.all.txtGrid_ContentFont.value=strTemp;" + "</SCRIPT>";
        Page.RegisterStartupScript("a1", strScript);
        for (int i = 0; i < 4; i++)
        {
            HeadFont[i] = this.txtGrid_ColumnTitleFont.Text.Split(',')[i];
            TableFont[i] = this.txtGrid_ContentFont.Text.Split(',')[i];
        }
        if (HeadFont[3] == "加粗")
            HeadFont[3] = "bold";
        if (TableFont[3] == "加粗")
            TableFont[3] = "bolder";
        ddlGrid_DisplayRowColorBind();
    }

    #endregion

    protected void btnReSet_Click(object sender, EventArgs e)
    {
        bool boolupdate = false;
        try
        {
            boolupdate = objParam.ResetUserParameter(Convert.ToInt32(Session["UserID"].ToString()));
        }
        catch (Exception exp)
        {
            Session["ModuleName"] = "系统信息参数设置";
            Session["FunctionName"] = "LinkButton1_Click";
            Session["ExceptionalType"] = exp.GetType().FullName;
            Session["ExceptionalDescription"] = exp.Message;
            Response.Redirect("~admin/Common/MistakesPage.aspx");
        }

        if (boolupdate)
        {
            JScript.Instance.ShowMessage(this, "用户的系统参数成功恢复为默认值！");
        }
        else
        {
            JScript.Instance.ShowMessage(this, "用户的系统参数恢复失败！");
        }
        ControlsDataBind();
        ddlGrid_DisplayRowColorBind();
    }
}
