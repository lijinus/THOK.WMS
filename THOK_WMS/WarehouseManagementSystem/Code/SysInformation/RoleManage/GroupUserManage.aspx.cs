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

public partial class Code_SysInfomation_RoleManage_GroupUserManage : System.Web.UI.Page
{
    string GroupID = "";
    string GroupName = "";
    SysUser objUser = new SysUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["GroupID"] != null)
        {
            GroupID = Request.QueryString["GroupID"].ToString();
            GroupName = Request.QueryString["GroupName"].ToString();
            this.Label1.Text = "用户组" + GroupName + "成员设置";
            this.dgUser.DataSource = objUser.GetAllUser();
            this.dgUser.DataBind();
            this.Title = "用户组" + GroupName + "成员设置";
        }
    }
    protected void dgUser_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            CheckBox chk=new CheckBox();
            chk.Text="加入"+GroupName;
            e.Item.Cells[2].Controls.Add(chk);
            if (e.Item.Cells[1].Text.Replace("&bsp;", "").Trim() ==GroupName.Trim())
            {
                chk.Checked = true;
            }
        }
        e.Item.Cells[3].Visible = false;
    }

    /// <summary>
    /// 保存当前组用户
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string users = "-1,";
        foreach (DataGridItem item in dgUser.Items)
        {
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                CheckBox chk=(CheckBox)item.Cells[2].Controls[1];
                if (chk.Checked)
                {
                    users += item.Cells[3].Text + ",";
                }
            }
        }
        users += "-1";

        string sql = "update sys_UserList set GroupID="+GroupID+" where UserID in ("+users+")";
        if (objUser.AddUserToGroup(sql))
        {
            JScript.Instance.ShowMessage(this, "添加成功！");
        }

    }
}
