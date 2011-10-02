/****************************************************** 
FileName:FontForm
Copyright (c) 2004-2007 天海欧康科技信息（厦门）有限公司技术开发部
Writer:施建新


create Date:2007/09/18
Rewriter:施建新


Rewrite Date:2007/09/17
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
using System.Reflection;
using System.Drawing;
using System.Collections.Generic;

///<summary>
///Module ID：<模块编号，可以引用系统设计中的模块编号>
///Depiction：获取字体相关信息公共模块



///Author：施建新
///Create Date：2007-09-17
///</summary> 
public partial class FontSelectModal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlFontColorBind();
        if (!IsPostBack)
        {
            BindText();
        }
    }
    /// <summary>
    /// 绑定ddlFontColor数据
    /// </summary>
    protected void ddlFontColorBind()
    {
        string[] strColorArray = Enum.GetNames(typeof(System.Drawing.KnownColor));
        foreach (string strColor in strColorArray)
        {
            ListItem itemColor = new ListItem(strColor, strColor);
            itemColor.Attributes.Add("style", "color:" + strColor);

            ddlFontColor.Items.Add(itemColor);
        }
        int intRow;
        for (intRow = 0; intRow < ddlFontColor.Items.Count - 1; intRow++)
        {
            ddlFontColor.Items[intRow].Attributes.Add("style", "background-color:" + ddlFontColor.Items[intRow].Value);
        }
        ddlFontColor.BackColor = Color.FromName(ddlFontColor.SelectedItem.Text);
    }
    /// <summary>
    /// 绑定ddlFont数据
    /// </summary>
    protected void BindText()
    {
        System.Drawing.Text.InstalledFontCollection font;
        font = new System.Drawing.Text.InstalledFontCollection();
        foreach (System.Drawing.FontFamily family in font.Families)
        {
            ListItem liText = new ListItem(family.Name, family.Name);
            ddlFont.Items.Add(liText); 
        }
    }
    protected void btnFontOK_Click(object sender, EventArgs e)
    {
        string strFontSelect = ddlFont.SelectedValue + "," + ddlFontColor.SelectedValue+","+ddlFontSize.SelectedValue+","+ddlFontIncrease.SelectedValue;
        Response.Write("<script   language=javascript>window.returnValue=" + "\'" + strFontSelect + "\'" + ";window.close()</script>");
    }
    protected void btnFontRetrue_Click(object sender, EventArgs e)
    {
        Response.Write("<script   language=javascript>window.close()</script>");
    }
}
