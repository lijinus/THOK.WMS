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

public partial class Code_SysInfomation_RoleManage_GroupUserList : BasePage
{
    //string GroupID = "";

    SysUser objUser = new SysUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["GroupID"] != null)
        {
            ViewState["GroupID"] = Request.QueryString["GroupID"].ToString();
            if (this.Request.QueryString["GroupName"] != null)
            {
                this.Label1.Text = "用户组 <font color='Gray'>" + this.Request.QueryString["GroupName"].ToString() + "</font>  成员";
            }
        }
        else
        {
            ViewState["GroupID"] = "-1";
        }
        GridDataBind();
    }
    protected void dgGroupUser_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int  UserID =Convert.ToInt32( e.Item.Cells[0].Text);
        int rowCount = dgGroupUser.Items.Count;
        if(objUser.DeleteUserFromGroup(UserID))
        {
            this.dgGroupUser.DataSource = objUser.GetGroupUser(Convert.ToInt32(ViewState["GroupID"]));
            if (rowCount == 1)
            {
                if (dgGroupUser.CurrentPageIndex == 0)
                {

                }
                else
                {
                    dgGroupUser.CurrentPageIndex = dgGroupUser.CurrentPageIndex - 1;
                }
            }
            this.dgGroupUser.DataBind();
        }
    }
    protected void dgGroupUser_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        this.dgGroupUser.CurrentPageIndex = e.NewPageIndex;
        GridDataBind();
    }

    public void GridDataBind()
    {
        this.dgGroupUser.DataSource = objUser.GetGroupUser(Convert.ToInt32(ViewState["GroupID"]));
        this.dgGroupUser.DataBind();
    }
    protected void dgGroupUser_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem || e.Item.ItemType==ListItemType.Header)
        {
            e.Item.Cells[0].Visible = false;
        }
    }
}
