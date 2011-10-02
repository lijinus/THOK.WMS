using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using THOK.WMS.BLL;

public partial class Code_BasicInfo_Company : BasePage
{
    Company objCom = new Company();
    DataSet dsCom;
    DataTable organizationTable = null;
    Comparison obj = new Comparison();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = obj.GetItems("COM_TYPE");
            //this.ddlComType.Items.Clear();
            ds.Tables[0].DefaultView.Sort = "VALUE ASC";
            this.ddlComType.DataSource = ds.Tables[0].DefaultView;
            this.ddlComType.DataTextField = "TEXT";
            this.ddlComType.DataValueField = "VALUE";
            this.ddlComType.DataBind();

            dsCom = objCom.GetCompanyInfo();
            organizationTable = objCom.GetDWV_IORG_ORGANIZATION();

            if (dsCom.Tables[0].Rows.Count == 1)
            {
                this.txtComCode.Text = dsCom.Tables[0].Rows[0]["COM_CODE"].ToString();
                this.txtComName.Text = dsCom.Tables[0].Rows[0]["COM_NAME"].ToString();
                this.txtUnifiedCode.Text = dsCom.Tables[0].Rows[0]["UNIFIEDCODE"].ToString();
                this.txtCapacity.Text = dsCom.Tables[0].Rows[0]["CAPACITY"].ToString();
                this.txtSortLine.Text = dsCom.Tables[0].Rows[0]["SORTLINE"].ToString();
                this.txtUpdatedTime.Text = dsCom.Tables[0].Rows[0]["UPDATEDTIME"].ToString();
                this.ddlComType.SelectedValue = dsCom.Tables[0].Rows[0]["COM_TYPE"].ToString();

                if (organizationTable.Rows.Count == 1)
                {
                    this.txtUP_CODE.Text = organizationTable.Rows[0]["UP_CODE"].ToString();
                    this.txtSTORE_ROOM_AREA.Text = organizationTable.Rows[0]["STORE_ROOM_AREA"].ToString();
                    this.txtSTORE_ROOM_NUM.Text = organizationTable.Rows[0]["STORE_ROOM_NUM"].ToString();

                }


            }
            else
            {
                this.txtUpdatedTime.Text = System.DateTime.Now.ToString();
            }
            if(this.hdnXGQX.Value=="1")
            {
                this.btnSave.Enabled = true;
            }
        }
        else
        {
            dsCom = objCom.GetCompanyInfo();
            organizationTable = objCom.GetDWV_IORG_ORGANIZATION();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlComType.SelectedIndex == 0)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请选择机构类型！");
                return;
            }
            Dictionary<string, string> pramaters = new Dictionary<string, string>();
            pramaters["ORGANIZATION_CODE"] = this.txtComCode.Text.Trim().Replace("\'", "\''");
            pramaters["ORGANIZATION_NAME"] = this.txtComName.Text.Trim().Replace("\'", "\''");
            pramaters["ORGANIZATION_TYPE"] = this.ddlComType.SelectedValue;
            pramaters["UP_CODE"] = this.txtUP_CODE.Text.Trim().Replace("\'", "\''");
            pramaters["N_ORGANIZATION_CODE"] = this.txtUnifiedCode.Text.Trim().Replace("\'", "\''");
            pramaters["STORE_ROOM_AREA"] = this.txtSTORE_ROOM_AREA.Text.Trim().Replace("\'", "\''");
            pramaters["STORE_ROOM_NUM"] = this.txtSTORE_ROOM_NUM.Text.Trim().Replace("\'", "\''");
            pramaters["STORE_ROOM_CAPACITY"] = this.txtCapacity.Text.Trim().Replace("\'", "\''");
            pramaters["SORTING_NUM"] = this.txtSortLine.Text.Trim().Replace("\'", "\''");
            if (this.organizationTable.Rows.Count == 0)
            {
                objCom.InsertDWV_IORG_ORGANIZATION(pramaters);
            }
            else
            {
                objCom.UpdateDWV_IORG_ORGANIZATION(pramaters);
            }
            
            if (this.dsCom.Tables[0].Rows.Count == 0) //添加
            {
                objCom.COM_CODE = this.txtComCode.Text.Trim().Replace("\'", "\''");
                objCom.COM_NAME = this.txtComName.Text.Trim().Replace("\'", "\''");
                objCom.COM_TYPE = this.ddlComType.SelectedValue;
                objCom.UNIFIEDCODE = this.txtUnifiedCode.Text.Trim().Replace("\'", "\''");
                objCom.CAPACITY = Convert.ToDecimal(this.txtCapacity.Text);
                objCom.SORTLINE = Convert.ToInt32(this.txtSortLine.Text);
                objCom.UPDATEDTIME = System.DateTime.Now;
                objCom.Insert();
                dsCom = objCom.GetCompanyInfo();
                JScript.Instance.ShowMessage(this.UpdatePanel1,"公司信息保存成功！");
            }
            else //修改
            {
                objCom.COM_CODE = this.txtComCode.Text.Trim().Replace("\'", "\''");
                objCom.COM_NAME = this.txtComName.Text.Trim().Replace("\'", "\''");
                objCom.COM_TYPE = this.ddlComType.SelectedItem.Value;
                objCom.UNIFIEDCODE = this.txtUnifiedCode.Text.Trim().Replace("\'", "\''");
                objCom.CAPACITY = Convert.ToDecimal(this.txtCapacity.Text);
                objCom.SORTLINE = Convert.ToInt32(this.txtSortLine.Text);
                objCom.UPDATEDTIME = System.DateTime.Now;
                objCom.Update();
                this.txtUpdatedTime.Text = System.DateTime.Now.ToString();
                dsCom = objCom.GetCompanyInfo();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "公司信息保存成功！");
            }
        }catch(Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1,exp.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../mainpage.aspx");
    }
    protected void txtComName_TextChanged(object sender, EventArgs e)
    {

    }
}
