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
using System.Text;
using THOK.System.BLL;
public partial class Webparts_THOKMenu : System.Web.UI.UserControl
{
    SysGroup getGroup = new SysGroup();
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["G_User"] != null)
            {
                int iGroupID = int.Parse(Session["GroupID"].ToString());
                dt = getGroup.GetGroupRole(iGroupID,"WMS").Tables[0];
                string preModuleName = "";
                string preSubModuleName = "";
                Table tbModule = null;
                Panel pSubModule = null;
                int index = 0;
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
                        tbModule = this.CreateModuleTable(preModuleName, ParentImage);
                        tbModule.ID = "table" + index.ToString();
                        this.plMenu.Controls.Add(tbModule);
                        pSubModule = new Panel();
                       // pSubModule.Height = 50; 
                        pSubModule.ID = "div" + index.ToString();
                        pSubModule.Attributes.Add("style", "display:none;");
                        tbModule.Attributes.Add("onclick", "Display('" + index.ToString() + "');");
                        this.plMenu.Controls.Add(pSubModule);
                        pSubModule.Controls.Add(CreateSubModuleTable(preModuleName, preSubModuleName, url, image));
                        index++;
                    }
                    else
                    {
                        if (preSubModuleName != currentSubModuleName)
                        {
                            preSubModuleName = currentSubModuleName;
                            pSubModule.Controls.Add(CreateSubModuleTable(preModuleName, preSubModuleName, url, image));
                        }
                    }
                }

                tbModule = CreateModuleTable("退出系统", "");
                tbModule.ID = "table" + index.ToString();
                pSubModule = new Panel();
                this.plMenu.Controls.Add(tbModule);
                pSubModule = new Panel();
                pSubModule.ID = "div" + index.ToString();
                pSubModule.Attributes.Add("style", "display:none;");
                pSubModule.Controls.Add(CreateLogoutTable());
                tbModule.Attributes.Add("onclick", "Display('" +index.ToString() + "');");
                this.plMenu.Controls.Add(pSubModule);


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
        catch(Exception exp)
        {
        }
    }

    public Table CreateModuleTable(string ModuleName ,string ParentImage)
    {
        Table tbl = new Table();
        //tbl.Attributes.Add("class", "Menu");
        tbl.Attributes.Add("cellspacing", "0");
        tbl.Attributes.Add("cellpadding","0");
        tbl.Attributes.Add("style", "width: 100%; height: 28px; padding:2 5 3 2;  cursor:hand; color:#000000;background-image:url(images/leftmenu/button.jpg)");
        //tbl.Attributes.Add("style", "width: 100%; height: 28px; padding:2 5 3 2; border-right: buttonshadow 1px solid; border-top: #f5f5f5 1px solid; border-left: #f5f5f5 1px solid; border-bottom: buttonshadow 1px solid; background-color:Transparent; cursor:hand; color:#000000;");
        TableRow tr = new TableRow();
        TableCell tdImg = new TableCell();
        TableCell td = new TableCell();
        Image img = new Image();
        img.Attributes.Add("style", "vertical-align: middle; border:0;hspace:3;");//width:20px;height:20px;
        if (ParentImage == "")
        {
            img.ImageUrl = "~/images/leftmenu/exit.gif";
        }
        else
        {
            img.ImageUrl = "~/images/leftmenu/" + ParentImage;
        }
        tdImg.Attributes.Add("style", "width:5%; text-align:right;");//FILTER:progid:DXImageTransform.Microsoft.Gradient(GradientType=1, StartColorStr=#ffffff, EndColorStr=buttonface);
        tdImg.Controls.Add(img);
        td.Text ="&nbsp;&nbsp;"+ ModuleName;
       // td.Attributes.Add("style", "text-align:left; FILTER:progid:DXImageTransform.Microsoft.Gradient(GradientType=1, StartColorStr=buttonface, EndColorStr=white);");   //#91D6FA
        tr.Controls.Add(tdImg);
        tr.Controls.Add(td);
        tbl.Controls.Add(tr);
        return tbl;
    }

    public Table CreateSubModuleTable(string ModuleName,string SubModuleName,string url,string image)
    {
        Table tbl = new Table();
        tbl.Attributes.Add("class", "Option");
        //tbl.Attributes.Add("style", "width: 100%; height: 24px; padding:2 5 3 16; border: 1 1 1 1 solid #ffffff; background-color:white;");
        TableRow tr = new TableRow();
        TableCell td = new TableCell();
        TableCell tdImg = new TableCell();
        tdImg.Attributes.Add("style", "width:15%; text-align:right;");
        Image img = new Image();
        img.ImageUrl = "~/images/leftmenu/"+image;
        tdImg.Controls.Add(img);
        //td.Text ="<a href='"+url+"'  target='mainFrame'>"+ SubModuleName+"</a>";
        string nav = string.Format("window.open(\"NavigationPage.aspx?PG={0}>>{1}\",\"Navigation\")", ModuleName, SubModuleName);
        td.Text = string.Format("<a href='{0}' onclick='{2}'  target='mainFrame'>{1}</a>",url,SubModuleName,nav);
        //td.Attributes.Add("onclick", string.Format("window.open('NavigationPage.aspx?PG={0}>>{1}','Navigation')",ModuleName,SubModuleName));
        tr.Controls.Add(tdImg);
        tr.Controls.Add(td);
        tbl.Controls.Add(tr);
        return tbl;
    }

    public Table CreateLogoutTable()
    {
        Table tbl = new Table();
        tbl.Attributes.Add("class", "Option");
        //tbl.Attributes.Add("style", "width: 100%; height: 24px; padding:2 5 3 16; border: 1 1 1 1 solid #ffffff; background-color: white;");
        TableRow trCancellation = new TableRow();
        TableRow trWithdrawal = new TableRow();

        TableCell tdCancellation = new TableCell();
        TableCell tdWithdrawal = new TableCell();

        TableCell tdCancellationImg = new TableCell();
        TableCell tdWithdrawalImg = new TableCell();

        tdCancellationImg.Attributes.Add("style", "width:15%; text-align:right;");
        tdWithdrawalImg.Attributes.Add("style", "width:15%;text-align:right;");

        Image imgCancellation = new Image();
        imgCancellation.ImageUrl = "~/images/leftmenu/15.gif";
        Image imgWithdrawal = new Image();
        imgWithdrawal.ImageUrl = "~/images/leftmenu/15.gif";

        tdCancellationImg.Controls.Add(imgCancellation);
        tdWithdrawalImg.Controls.Add(imgWithdrawal);

        tdCancellation.Text = "<a href='#'>注销</a>";
        tdCancellation.Attributes.Add("onclick", "Logout();");
        tdWithdrawal.Text = "<a href='#'>退出</a>";
        tdWithdrawal.Attributes.Add("onclick","Exit();");

        trCancellation.Controls.Add(tdCancellationImg);
        trCancellation.Controls.Add(tdCancellation);
        trWithdrawal.Controls.Add(tdWithdrawalImg);
        trWithdrawal.Controls.Add(tdWithdrawal);
        tbl.Controls.Add(trCancellation);
        tbl.Controls.Add(trWithdrawal);
        return tbl;
    }
}
