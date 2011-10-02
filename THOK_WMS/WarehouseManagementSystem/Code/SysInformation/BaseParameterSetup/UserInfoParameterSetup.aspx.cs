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
using THOK.System.BLL;

public partial class Code_SysInfomation_UserInfoParameterSetup_UserInfoParameterSetup : BasePage
{

    //SystemInfo dc = new SystemInfo();
    SysSystemParameter objParam = new SysSystemParameter();
    DataSet dsParam;

    public string SingleLineColor="";
    public string DoubleLineColor="";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            //JScript.Instance.NavigationInfo(this, "用户信息参数设置");
            //用户信息设置 
            this.btnUpdate.Attributes.Add("style", "display:none;");
            this.lbID.Attributes.Add("style", "border:0;");
        }
        UserSysParDataBind();
        if (Session["IsUseGlobalParameter"] == "1")
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

    /*********************************************************************************************/

    #region 用户信息设置

    /// <summary>
    /// GridView 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSystemParameter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvSystemParameter.PageIndex = e.NewPageIndex;
        this.UserSysParDataBind();
        //SetTabIndexScript();
    }

    /// <summary>
    /// 用户设置　数据绑定
    /// </summary>
    protected void UserSysParDataBind()
    {
        dsParam = objParam.GetAllOptionParameter();
        this.gvSystemParameter.DataSource = dsParam.Tables[0];// dc.GetSysParDropDownListItems();
        this.gvSystemParameter.DataBind();


        this.ddlParameterName.DataSource = objParam.GetSystemParameterName();
        this.ddlParameterName.DataTextField = "ParameterName";
        this.ddlParameterName.DataValueField = "Description";
        this.ddlParameterName.DataBind();
        ddlParameterName.Items.Insert(0, new ListItem("可选参数名称"));
        this.ddlParameterName.Attributes.Add("onchange", "DDLParameterNameChanged();");
    }


    

    //protected void SetTabIndexScript()
    //{
    //    string strScript = string.Format(" \n secBoard({0}); \n"
    //        ////     "  var nav=window.parent.frames.Navigation.document.getElementById('labNavigation'); "+
    //        ////     "  var ary=nav.innerText.split('>>'); "+
    //        ////"  nav.innerText=ary[0]+'>>'+ ary[1]+ '>>"+strOP+"'"
    //                                     , ViewState["TabIndex"].ToString());
    //    JScript.Instance.RegisterScript(this, strScript);
    //}
    protected void gvSystemParameter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Width = 60;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = new CheckBox();
            chk.Attributes.Add("style", " font-weight:bold; text-align:center;word-break:keep-all; white-space:nowrap");
            chk.ID = "checkAll";
            chk.Attributes.Add("onclick", "checkboxChange(this);");
            chk.Text = "操作";
            e.Row.Cells[0].Controls.Add(chk);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex % 2 == 0)
            {
                e.Row.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
            }
            else
            {
                e.Row.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
            }
            e.Row.Cells[0].Attributes.Add("style", "word-break:keep-all; white-space:nowrap");

            if (e.Row.Cells[5].Text == "1")
            {
                e.Row.Cells[5].Text = "可用";
            }
            else
            {
                e.Row.Cells[5].Text = "不可用";
            }

            CheckBox chk = new CheckBox();
            LinkButton lkbtn = new LinkButton();
            lkbtn.CommandName = "Edit";
            lkbtn.ID = e.Row.ID;
            lkbtn.Text = " 编辑 ";
            e.Row.Cells[0].Controls.Add(chk);
            e.Row.Cells[0].Controls.Add(lkbtn);
        }
    }

    ////void btn_Click(object sender, EventArgs e)
    ////{
    ////    LinkButton lBtn = (LinkButton)sender;
    ////    int rowIndex = Int32.Parse(lBtn.ID);
    ////    this.lbID.Text = gvSystemParameter.Rows[rowIndex].Cells[0].Text;
    ////    this.txtParameterName.Text = gvSystemParameter.Rows[rowIndex].Cells[1].Text;
    ////    this.txtParameterValue.Text = gvSystemParameter.Rows[rowIndex].Cells[2].Text;
    ////    this.txtParameterText.Text = gvSystemParameter.Rows[rowIndex].Cells[3].Text;
    ////    this.txtDescription.Text = gvSystemParameter.Rows[rowIndex].Cells[4].Text.Replace("&nbsp;", "");
    ////    this.ddlState.SelectedValue = gvSystemParameter.Rows[rowIndex].Cells[5].Text;
    ////    this.btnInsert.Attributes.Add("style","display:none");
    ////    this.btnUpdate.Attributes.Add("style", "display:block;");
    ////}

    ////public void lBtn_Click(object sender, EventArgs e)
    ////{
    ////    LinkButton lBtn = (LinkButton)sender;
    ////    int rowIndex = Int32.Parse(lBtn.ID);
    ////    this.lbID.Text = gvSystemParameter.Rows[rowIndex].Cells[0].Text;
    ////    this.txtParameterName.Text = gvSystemParameter.Rows[rowIndex].Cells[1].Text;
    ////    this.txtParameterValue.Text = gvSystemParameter.Rows[rowIndex].Cells[2].Text;
    ////    this.txtParameterText.Text = gvSystemParameter.Rows[rowIndex].Cells[3].Text;
    ////    this.txtDescription.Text = gvSystemParameter.Rows[rowIndex].Cells[4].Text.Replace("&nbsp;", "");
    ////    this.ddlState.SelectedValue = gvSystemParameter.Rows[rowIndex].Cells[5].Text;
    ////    this.btnInsert.Visible = false;
    ////    this.btnUpdate.Visible = true;
    ////}

    protected void gvSystemParameter_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int pageIndex = gvSystemParameter.PageIndex;
        int pageSize=gvSystemParameter.PageSize;
        int rowIndex = e.NewEditIndex+pageIndex*pageSize;
        DataRow dr = dsParam.Tables[0].Rows[rowIndex];
        this.txtParameterName.Text = dr["ParameterName"].ToString();
        this.txtParameterValue.Text = dr["ParameterValue"].ToString();
        this.txtParameterText.Text = dr["ParameterText"].ToString();
        this.txtDescription.Text = dr["Description"].ToString();
        this.ddlState.SelectedValue = dr["State"].ToString();
        this.lbID.Text = dr["SystemParameterID"].ToString();
        //this.btnInsert.Visible = false;
        //this.btnUpdate.Visible = true;
        this.btnInsert.Attributes.Add("style", "display:none");
        this.btnUpdate.Attributes.Add("style", "display:block;");
    }

    /// <summary>
    /// 新增参数选项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            bool ok = true;
            if (this.txtParameterText.Text.Trim().Length == 0)
            {
                ok = false;
                txtParameterText.Focus();
            }
            if (this.txtParameterValue.Text.Trim().Length == 0)
            {
                ok = false;
                txtParameterValue.Focus();
            }
            if (this.txtParameterName.Text.Trim().Length == 0)
            {
                ok = false;
                this.txtParameterName.Focus();
            }
            if (!ok)
            {
                //JScript.Instance.ShowMessage(this, "请填写未填项");
                Response.Write("<script>alert(\"请填写未填项！\");</script>");

                //this.Page.RegisterStartupScript("QueryAlert", "<script language='javascript'>var strMessage='请填写未填项';DoResult();</script>");
                UserSysParDataBind();
                return;
            }
            if (this.txtDescription.Text.Length > 100)
            {
                Response.Write("<script>alert(\"参数描述内容长度超过最大长度！\");</script>");
                //this.Page.RegisterStartupScript("QueryAlert", "<script language='javascript'>var strMessage='参数描述内容长度超过最大长度！';DoResult();</script>");
                UserSysParDataBind();
                this.txtDescription.Focus();
                return;
            }


            //SysSystemParameter objParam = objParam;
            objParam.ParameterName = this.txtParameterName.Text.Trim().Replace("'", "''");
            objParam.ParameterValue = this.txtParameterValue.Text.Trim().Replace("'", "''");
            objParam.ParameterText = this.txtParameterText.Text.Trim().Replace("'", "''");
            objParam.Description = this.txtDescription.Text.Trim().Replace("'", "''");
            objParam.State = Int32.Parse(this.ddlState.SelectedValue);

            if (!objParam.IsExist(this.txtParameterName.Text.Trim(), this.txtParameterValue.Text.Trim()))
            {

                if (objParam.InsertOptionParameter(objParam))
                {
                    txtParameterName.Text = string.Empty;
                    txtParameterValue.Text=string.Empty;
                    txtParameterText.Text=string.Empty;
                    txtDescription.Text = string.Empty;
                    UserSysParDataBind();
                    JScript.Instance.ShowMessage(this, "新增成功");
                    
                }
                else
                {
                    JScript.Instance.ShowMessage(this, "新增失败");
                }
            }
            else
            {
                Response.Write("<script>alert(\"该参数名称相应参数值已经存在！！\");</script>");
                //this.Page.RegisterStartupScript("QueryAlert", "<script language='javascript'>var strMessage='该参数名称相应参数值已经存在！';DoResult();</script>");


                UserSysParDataBind();
            }
            
        }
    
        catch (SqlException exp)
        {
            Session["ModuleName"] = "用户信息参数设置";
            Session["FunctionName"] = "btnInsert_Click";
            Session["ExceptionalType"] = exp.GetType().FullName;
            Session["ExceptionalDescription"] = exp.Message;
            Response.Redirect("MistakesPage.aspx");
        }
    }

    /// <summary>
    /// 修改参数选项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bool ok = true;
        if (this.txtParameterText.Text.Trim().Length == 0)
        {
            ok = false;
            txtParameterText.Focus();
        }
        if (this.txtParameterValue.Text.Trim().Length == 0)
        {
            ok = false;
            txtParameterValue.Focus();
        }
        if (this.txtParameterName.Text.Trim().Length == 0)
        {
            ok = false;
            this.txtParameterName.Focus();
        }
        if (!ok)
        {
            JScript.Instance.ShowMessage(this, "参数描述内容长度超过最大长度！");
            return;
        }
        if (this.txtDescription.Text.Length > 100)
        {
            Response.Write("<script>alert(\"参数描述内容长度超过最大长度！\");</script>");
            //this.Page.RegisterStartupScript("QueryAlert", "<script language='javascript'>var strMessage='参数描述内容长度超过最大长度！';DoResult();</script>");
            this.txtDescription.Focus();
            return;
        }
        
        objParam.SystemParameterID = Convert.ToInt32(this.lbID.Text);
        objParam.ParameterName = this.txtParameterName.Text.Trim().Replace("'", "''");
        objParam.ParameterValue = this.txtParameterValue.Text.Trim().Replace("'", "''");
        objParam.ParameterText = this.txtParameterText.Text.Trim().Replace("'", "''");
        objParam.Description = this.txtDescription.Text.Trim().Replace("'", "''");
        objParam.State = Int32.Parse(this.ddlState.SelectedValue);
        if (!objParam.IsExist(this.txtParameterName.Text.Trim(), this.txtParameterValue.Text.Trim(), objParam.SystemParameterID))
        {
            try
            {
                if (objParam.UpdateOptionParameter(objParam))
                {
                    txtParameterName.Text = string.Empty;
                    txtParameterValue.Text = string.Empty;
                    txtParameterText.Text = string.Empty;
                    txtDescription.Text = string.Empty;
                    UserSysParDataBind();
                    JScript.Instance.ShowMessage(this, "修改成功！");
                }
                else
                {
                    UserSysParDataBind();
                    JScript.Instance.ShowMessage(this, "修改失败！");
                }
            }
            catch (SqlException exp)
            {

                Session["ModuleName"] = "用户信息参数设置";
                Session["FunctionName"] = "btnDelete_Click";
                Session["ExceptionalType"] = exp.GetType().FullName;
                Session["ExceptionalDescription"] = exp.Message;
                Response.Redirect("~/Common/MistakesPage.aspx");
            }

        }
        else
        {
            UserSysParDataBind();
            JScript.Instance.ShowMessage(this, "参数值重复！");
            this.btnInsert.Attributes.Add("style", "display:none");
            this.btnUpdate.Attributes.Add("style", "display:block");
        }

    }





    protected void btnDelete_Click(object sender, EventArgs e)
    {
        bool bDeleteSucess = false;
        try
        {
            for (int i = 0; i < gvSystemParameter.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvSystemParameter.Rows[i].Cells[0].Controls[0];
                if (chk.Enabled && chk.Checked)
                {
                    int rowIndex = gvSystemParameter.PageSize * gvSystemParameter.PageIndex + i;
                    dsParam.Tables[0].Rows[rowIndex].Delete();
                }
            }
            objParam.DeleteOptionParameter(dsParam);
            bDeleteSucess = true;
            UserSysParDataBind();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }

        if (bDeleteSucess)
        {
            UserSysParDataBind();
            JScript.Instance.ShowMessage(this, "删除成功！");
        }
        else
        {
            UserSysParDataBind();
            JScript.Instance.ShowMessage(this, "删除失败！");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ////Sys_SystemParameter objParam = new Sys_SystemParameter();

        objParam.ParameterName = this.txtParameterName.Text.Trim();
        objParam.ParameterText = this.txtParameterText.Text.Trim();
        objParam.ParameterValue = this.txtParameterValue.Text.Trim();
        objParam.Description = this.txtDescription.Text.Trim();
        objParam.State = Convert.ToInt32(this.ddlState.SelectedValue);
        try
        {
            this.gvSystemParameter.DataSource = objParam.SearchParameter(objParam);//dc.SearchSystemParameter(objParam);
        }
        catch (SqlException exp)
        {
            Session["ModuleName"] = "用户信息参数设置";
            Session["FunctionName"] = "btnSearch_Click";
            Session["ExceptionalType"] = exp.GetType().FullName;
            Session["ExceptionalDescription"] = exp.Message;
            Response.Redirect("MistakesPage.aspx");
        }

        this.gvSystemParameter.DataBind();
    }

    #endregion


    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../../MainPage.aspx");
    }
}
