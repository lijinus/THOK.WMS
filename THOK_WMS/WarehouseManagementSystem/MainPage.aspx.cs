/*================================================================
 * 
 * Copyright (c) 2007,天海欧康(厦门)科技有限公司, All rights reserved.
 * 
 * FileName   : MainPage.aspx.cs
 * Author     : 陈振
 * functions  : 快速通道页
 * CreateDate : 2007/8/23
 * ChangeDate : 2007/11/17
 * 
=================================================================*/
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
using System.Drawing;
using System.Data.SqlClient;
using THOK.System.BLL;
using THOK.WMS.BLL;
public partial class MainPage : System.Web.UI.Page
{
    #region 变量
    DataSet ds;
    DataTable dtDestopItem;
    SysUser getUser = new SysUser();
    Table tb;
    Panel pl;
    Hashtable GlobalMenuTitle;//菜单标题
    Hashtable GlobalMenuLink;//菜单链接地址
    Hashtable GlobalMenuParent;//菜单父标题


    int iTableCount;
    int iCount = 0;
    int iCountColor = 0;
    #endregion
    #region 页面加载事件
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["IsFirstLogin"] != null)
        //{
        //    if (Session["IsFirstLogin"].ToString() == "1")
        //    {
        //    }
        //    else
        //    {
        if (!IsPostBack)
        {
            GC.Collect();
            Response.ExpiresAbsolute = DateTime.Now;
            try
            {
                string str = "<script> " +
                             "try " +
                             "{ " +
                             " var nav=window.parent.frames.Navigation.document.getElementById('labNavigation');" +
                             " nav.innerText='快速通道';" +
                             "} " +
                             "catch(e) " +
                             "{ " +
                             "} " +
                             "</script>";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), DateTime.Now.ToLongTimeString(), str);


                #region 提醒
                bool show = false;
                Alarm objAlarm = new Alarm();
                DataSet dsRemind=objAlarm.GetRemindList();
                if (dsRemind.Tables[0].Rows.Count > 0)
                {
                    show = true;
                    TableRow tr = new TableRow();
                    TableCell tc = new TableCell();
                    tc.Text = string.Format("<a href='code/StorageManagement/StorageRemindPage.aspx'>有:<font color='red'>{0}条</font>库存预警信息</a>", dsRemind.Tables[0].Rows.Count);
                    tr.Controls.Add(tc);
                    this.tblRemind.Controls.Add(tr);
                }
                
                if (show)
                {
                    this.pnlRemind.Visible = true;
                }
                else
                {
                    this.pnlRemind.Visible = false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                //Session["ModuleName"] = "MainPage.aspx";
                //Session["FunctionName"] = "Page_Load事件";
                //Session["ExceptionalType"] = ex.GetType().FullName;
                //Session["ExceptionalDescription"] = ex.Message;
                //Response.Redirect("Common/MistakesPage.aspx");
            }
        }
        //}
        if (Session["UserID"] != null)
        {
            GetDestopItemByUserID(Session["UserID"].ToString());
        }
        else
        {
            Response.Redirect("~/SessionTimeOut.aspx");
        }
        CreatePage();
        //}
        //else
        //{
        //    Response.Redirect("~/SessionTimeOut.aspx");
        //}
    }
    #endregion
    #region 创建页面
    private void CreatePage()
    {
        ////////
        GlobalMenuTitle = new Hashtable();
        GlobalMenuLink = new Hashtable();
        GlobalMenuParent = new Hashtable();
        tb = new Table();
        //Session["IsFirstLogin"] = "2";
        tb = CreateTable(iTableCount);
        tb.Attributes.Add("border", "0");
        tb.Attributes.Add("bordercolor", "#ffffff");
        tb.Attributes.Add("frame", "void");
        tb.Attributes.Add("cellpadding", "0");
        tb.Attributes.Add("cellspacing", "0");
        tb.Attributes.Add("align", "center");
        pl = new Panel();

        int i = 0, j = 0;
        if (dtDestopItem.Rows.Count > 0)
        {
            foreach (DataRow dr in dtDestopItem.Rows)
            {
                ImageButton im = new ImageButton();
                im.ID = "im" + i.ToString() + i.ToString() + j.ToString();
                im.ImageUrl = "images/" + dr["DestopImage"].ToString();
                im.Click += new ImageClickEventHandler(im_Click);
                GlobalMenuTitle.Add(im.ID, dr["MenuTitle"].ToString());
                GlobalMenuLink.Add(im.ID, dr["MenuUrl"].ToString());
                GlobalMenuParent.Add(im.ID, dr["MenuParent"].ToString());
                tb.Rows[i].Cells[j].Controls.Add(im);
                tb.Rows[i].Cells[j].Controls.Add(new LiteralControl("<br>" + dr["MenuTitle"].ToString()));
                j++;
                if (j > 4)
                {
                    j = 0; i++;
                }
            }
        }
        pl.Controls.Add(tb);
        form1.Controls.Add(pl);
    }
    #endregion
    #region 链接事件
    protected void im_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ImageButtonID = (ImageButton)sender;
        string strScript = "<script> ";
        strScript += "var nav=window.parent.frames.Navigation.document.getElementById(\'labNavigation\');";
        strScript += " nav.innerText=\'" + GlobalMenuParent[ImageButtonID.ID].ToString() + ">>" + GlobalMenuTitle[ImageButtonID.ID].ToString() + "\';";
        strScript += "window.open(\'" + GlobalMenuLink[ImageButtonID.ID].ToString() + "\',\'_self\')";
        strScript += "</script>";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), DateTime.Now.ToLongTimeString(), strScript);
    }
    private void GetDestopItemByUserID(string UserID)
    {
        dtDestopItem = getUser.GetUserQuickDesktop(Convert.ToInt32(UserID)).Tables[0];//dc.GetQuickDestop(Convert.ToInt32(UserID));
        iTableCount = dtDestopItem.Rows.Count;
    }
    #endregion
    #region 创建表格
    /// <summary>
    /// 创建表格
    /// </summary>
    /// <param name="iRowCount"></param>
    /// <returns></returns>
    private Table CreateTable(int iTableCount)
    {
        for (int i = 0; i < iTableCount / 5 + 1; i++)
        {
            TableRow row = CreateTableRow();
            row.Attributes.Add("align", "center");
            for (int j = 0; j < 5; j++)
            {
                row.Cells.Add(CreateTableCell(i, j));
            }
            tb.Rows.Add(row);
        }
        return tb;
    }
    #endregion 创建表格
    #region 创建Table的Row
    /// <summary>
    /// 创建Table的Row
    /// </summary>
    /// <returns></returns>
    private TableRow CreateTableRow()
    {
        TableRow CreateTableRow = new TableRow();
        //CreateTableRow.Attributes.Add("style","height:200px;width:200px");
        return CreateTableRow;
    }
    #endregion 创建Table的Row
    #region 创建Table的cell
    /// <summary>
    /// 创建Table的Cell
    /// </summary>
    /// <returns></returns>
    private TableCell CreateTableCell(int i, int j)
    {
        TableCell CreateTableCell = new TableCell();
        CreateTableCell.Attributes.Add("style", "height:120px;width:150px");
        CreateTableCell.ID = i.ToString() + j.ToString();
        int iID = Convert.ToInt32(CreateTableCell.ID);
        if (iCount < iTableCount)
        {
            //switch (iCountColor)
            //{
            //    case 0:
            //        CreateTableCell.Attributes.Add("bgcolor ", "");
            //        break;
            //    case 1:
            //        CreateTableCell.Attributes.Add("bgcolor ", "");
            //        break;
            //    case 2:
            //        CreateTableCell.Attributes.Add("bgcolor ", "");
            //        break;
            //    case 3:
            //        CreateTableCell.Attributes.Add("bgcolor ", "");
            //        break;
            //    default:
            //        CreateTableCell.Attributes.Add("bgcolor ", "");
            //        iCountColor = 0;
            //        break;
            //}
            CreateTableCell.Attributes.Add("onMouseOver", "SetNewColor(this);");
            CreateTableCell.Attributes.Add("onMouseOut", "SetOldColor(this);");
            iCountColor++;
        }
        iCount++;
        return CreateTableCell;
    }
    #endregion
}
