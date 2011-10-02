using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
/// <summary>
/// BasePage 的摘要说明
//----WMS系统操作权限代码
//----0:增加
//----1:删除
//----2:修改
//----3:查询
//----4:导出
//----5:打印
//----6:审核
//----7:分配
//----8:分配确认
//----9:入库到货，出库出货确认
//----10：结算
/// </summary>
public class BasePage : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Session["IsUseGlobalParameter"] = "1";
            if (Request.QueryString["SubModuleCode"] != null)
            {
                Session["SubModuleCode"] = Request.QueryString["SubModuleCode"];
            }
            if (Request.QueryString["Type"] != null)
            {
                Session["News_Type"] = Request.QueryString["Type"];
            }
            if (Session["G_user"] != null)
            {
                //入库单分配权限控制【一次只允许一个用户进行分配】
                if (Session["SubModuleCode"] != null && Session["SubModuleCode"].ToString() != "MNU_M00B_00D" && Application["MNU_M00B_00D"] != null && Application["MNU_M00B_00D"].ToString() == Session["G_user"].ToString())
                {
                    Application["MNU_M00B_00D"] = null;
                }
                //出库单分配权限控制【一次只允许一个用户进行分配】
                if (Session["SubModuleCode"] != null && Session["SubModuleCode"].ToString() != "MNU_M00E_00D" && Application["MNU_M00E_00D"]!=null && Application["MNU_M00E_00D"].ToString() == Session["G_user"].ToString())
                {
                    Application["MNU_M00E_00D"] = null;
                }
                //移位单生成权限控制【一次只允许一个用户进行生成移位单】
                if (Session["SubModuleCode"] != null && Session["SubModuleCode"].ToString() != "MNU_M00D_00G" && Application["MNU_M00D_00G"] != null && Application["MNU_M00D_00G"].ToString() == Session["G_user"].ToString())
                {
                    Application["MNU_M00D_00G"] = null;
                }
                
            }
            else
            {
                Response.Redirect("~/SessionTimeOut.aspx", false);
            }
            
        }
        catch
        {
        }
    }


    protected void Page_PreLoad(object sender, EventArgs e)
    {
        #region 权限控制
        try
        {
            if (Session["SubModuleCode"] != null)
            {
                DataTable dtOP = (DataTable)(Session["DT_UserOperation"]);
                DataRow[] drs = dtOP.Select(string.Format("SubModuleCode='{0}'", Session["SubModuleCode"].ToString()));
                foreach (DataRow dr in drs)
                {
                    int op = int.Parse(dr["OperatorCode"].ToString());
                    switch (op)
                    {
                        case 0:
                            if ((Button)Page.FindControl("btnCreate") != null)
                            {
                                ((Button)Page.FindControl("btnCreate")).Enabled = true;
                            }
                            break;
                        case 1:
                            if ((Button)Page.FindControl("btnDelete")!=null)
                            {
                                ((Button)Page.FindControl("btnDelete")).Enabled = true;
                            }
                             break;
                        case 2:
                            if ((HiddenField)Page.FindControl("hdnXGQX") != null)
                            {
                                ((HiddenField)Page.FindControl("hdnXGQX")).Value = "1";
                            } 
                            break;
                        case 3: break;
                        case 4:
                            if ((Button)Page.FindControl("btnExport")!=null)
                            {
                                ((Button)Page.FindControl("btnExport")).Enabled = true;
                            }break;
                        case 5:
                            if ((Button)Page.FindControl("btnPrint")!=null)
                            {
                                ((Button)Page.FindControl("btnPrint")).Enabled = true;
                            }break;
                        case 6:
                            if ((Button)Page.FindControl("btnValidate") != null)
                            {
                                ((Button)Page.FindControl("btnValidate")).Enabled = true;
                            }
                            //if ((Button)Page.FindControl("btnReverseValidate")!=null)
                            //{
                            //    ((Button)Page.FindControl("btnReverseValidate")).Enabled = true;
                            //}
                            if ((HiddenField)Page.FindControl("hdnXGQX") != null)
                            {
                                ((HiddenField)Page.FindControl("hdnXGQX")).Value = "1";
                            } break;
                        default: break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            JScript.Instance.ShowMessage(Page, ex.Message);
        }

        #endregion    
    }
}
