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

public partial class Admin_WebUserControl_THOKTreeMenu : System.Web.UI.UserControl
{
    SysGroup getGroup = new SysGroup();
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["G_User"] != null)
                {
                    int iGroupID = int.Parse(Session["GroupID"].ToString());
                    dt = getGroup.GetGroupRole(iGroupID,"WMS").Tables[0];
                    string preModuleName = "";
                    string preSubModuleName = "";
                    //Table tbModule = null;
                    TreeNode nodeModule = null;
                    //TreeNode nodeSubModule = null;
                    int index = 0;
                    sTreeMenu.Nodes[0].ChildNodes.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        string currentModuleName = dr["MenuParent"].ToString();
                        string currentSubModuleName = dr["MenuTitle"].ToString();
                        string url = dr["MenuUrl"].ToString();
                        string image = dr["MenuImage"].ToString();
                        string ParentImage = dr["ParentImage"].ToString();
                        if (preModuleName != currentModuleName)
                        {
                            preModuleName = currentModuleName;
                            preSubModuleName = currentSubModuleName;
                            string parentCode = dr["MenuCode"].ToString().Substring(0, 8);
                            //tbModule = this.CreateModuleTable(preModuleName, ParentImage);
                            nodeModule = this.CreateModuleNode(preModuleName, ParentImage);
                            nodeModule.SelectAction = TreeNodeSelectAction.Expand;
                            this.sTreeMenu.Nodes[0].ChildNodes.Add(nodeModule);
                            nodeModule.ChildNodes.Add(CreateSubModuleNode(preModuleName, preSubModuleName, url, image));
                            index++;
                        }
                        else
                        {
                            if (preSubModuleName != currentSubModuleName)
                            {
                                preSubModuleName = currentSubModuleName;
                                nodeModule.ChildNodes.Add(CreateSubModuleNode(preModuleName, preSubModuleName, url, image));
                                //pSubModule.Controls.Add(CreateSubModuleTable(preModuleName, preSubModuleName, url, image)); 
                            }
                        }
                    }

                    nodeModule = CreateModuleNode("退出系统", "");
                    nodeModule.Collapse();
                    nodeModule.SelectAction = TreeNodeSelectAction.Expand;
                    this.sTreeMenu.Nodes[0].ChildNodes.Add(nodeModule);
                    nodeModule.ChildNodes.Add(CreateLogoutNode());
                    nodeModule.ChildNodes.Add(CreateExitNode());


                    //操作权限保存Sesion中（ModuleID,OperatorCode,MenuCode)
                    dt.Columns.Remove("MenuImage");
                    dt.Columns.Remove("MenuUrl");
                    dt.Columns.Remove("MenuTitle");
                    dt.Columns.Remove("MenuParent");
                    Session["DT_UserOperation"] = dt;
                }
                else
                {
                }
            }
        }
        catch (Exception exp)
        {
        }
    }

    public TreeNode CreateModuleNode(string ModuleName, string ParentImage)
    {
        TreeNode node = new TreeNode();
        node.Text = ModuleName;
        if (ParentImage == "")
        {
            node.ImageUrl= "~/images/leftmenu/exit.gif";
        }
        else
        {
            node.ImageUrl = "~/images/leftmenu/" + ParentImage;
            //if (ModuleName == "系统维护")
            //{
                node.Collapse();
            //}
        }
        return node;
    }

    public TreeNode CreateSubModuleNode(string ModuleName, string SubModuleName, string url, string image)
    {
        TreeNode node = new TreeNode();
        node.Text = SubModuleName;
        node.ImageUrl = "~/images/leftmenu/" + image;
        node.NavigateUrl = string.Format("javascript:Navigate('{0}','{1}','{2}')",ModuleName,SubModuleName,url);//"../" + url;
        return node;
    }

    public TreeNode CreateLogoutNode()
    {
        TreeNode node = new TreeNode();
        node.Text = "注销";
        node.ImageUrl = "~/images/leftmenu/15.gif";
        node.NavigateUrl = "javascript:Logout();";
        return node;
    }

    public TreeNode CreateExitNode()
    {
        TreeNode node = new TreeNode();
        node.Text = "退出";
        node.ImageUrl ="~/images/leftmenu/15.gif";
        node.NavigateUrl = "Javascript:Exit();";
        return node;
    }
    protected void lnkExpand_Click(object sender, EventArgs e)
    {
        this.sTreeMenu.ExpandAll();
    }
    protected void lnkCollapse_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < sTreeMenu.Nodes[0].ChildNodes.Count; i++)
        {
            this.sTreeMenu.Nodes[0].ChildNodes[i].CollapseAll();
        }
    }
}
