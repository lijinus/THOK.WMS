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
using System.Xml;
using THOK.WMS.BLL;
using System.Drawing;

public partial class Common_QueryDialog : System.Web.UI.Page
{
    XmlDocument xmlDoc = new XmlDocument();
    XmlNode nodeTable;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hideTableName.Value = Request.QueryString["TableName"];
        }
        xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + string.Format("Code\\TableXML\\{0}.xml", this.hideTableName.Value));
        nodeTable = xmlDoc.SelectSingleNode("TABLE");

        #region 字段控件
        int iColor = 0;
        this.tblQuery.Controls.Clear();
        foreach (XmlNode node in nodeTable.ChildNodes)
        {
            
            if (node.Attributes["DisplayWhenQuery"].Value == "T")
            {

                TableRow tr = new TableRow();
                if (iColor % 2 == 1)
                {
                    if (Session["grid_OddRowColor"] != null)
                    {
                        tr.Attributes.Add("style", string.Format("height: 25px; background-color:{0};", Session["grid_OddRowColor"].ToString()));
                    }
                    iColor++;
                }
                else
                {
                    if (Session["grid_EvenRowColor"] != null)
                    {
                        tr.Attributes.Add("style", string.Format("height: 25px; background-color:{0};", Session["grid_EvenRowColor"].ToString()));
                    }
                    iColor++;
                }
                tblQuery.Controls.Add(tr);
                TableCell tc1 = new TableCell();
                tc1.Width = 80;
                tc1.Attributes.Add("class", "tdTitle");
                TableCell tc2 = new TableCell();
                tc1.Text = node.ChildNodes[1].InnerText;
                if (node.ChildNodes[5].InnerText == "DropdownList")
                {
                    DropDownList ddl = new DropDownList();
                    ddl.ID = node.ChildNodes[4].InnerText;
                    Comparison objCom = new Comparison();
                    DataSet dsTemp=objCom.GetItems(node.ChildNodes[3].InnerText);
                    DataRow newRow = dsTemp.Tables[0].NewRow();
                    newRow["VALUE"]="";
                    newRow["TEXT"] = "全部";
                    dsTemp.Tables[0].Rows.InsertAt(newRow, 0);
                    ddl.DataSource = dsTemp;
                    ddl.DataTextField = "TEXT";
                    ddl.DataValueField = "VALUE";
                    ddl.DataBind();
                    tc2.Controls.Add(ddl);
                    tr.Controls.Add(tc1);
                    tr.Controls.Add(tc2);
                }

                else  if (node.ChildNodes[3].InnerText.Contains("SelectDialog2"))
                {
                    TextBox text = new TextBox();
                    text.Width = 165;
                    text.Attributes.Add("class", "TextBox");
                    text.ID = node.ChildNodes[4].InnerText;
                    tc2.Controls.Add(text);

                    Button btn = new Button();
                    btn.Attributes.Add("class", "ButtonBrowse2");
                    //btn.Text = "...";
                    tc2.Controls.Add(btn);
                    btn.OnClientClick = "return " + node.ChildNodes[3].InnerText;

                    tr.Controls.Add(tc1);
                    tr.Controls.Add(tc2);

                }

                else if (node.ChildNodes[2].InnerText == "DateTime")
                {
                    TextBox text = new TextBox();
                    text.Attributes.Add("class", "TextBox");
                    text.Width = 70;
                    if (node.ChildNodes[3].InnerText == "Now")
                    {
                        text.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        text.Attributes.Add("onfocus", "setday(this)");
                    }
                    text.ID = node.ChildNodes[4].InnerText;//"txt_" + node.ChildNodes[0].InnerText;

                    TextBox text2 = new TextBox();
                    text2.Attributes.Add("class", "TextBox");
                    text2.Width = 70;
                    if (node.ChildNodes[3].InnerText == "Now")
                    {
                        text2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        text2.Attributes.Add("onfocus", "setday(this)");
                    }
                    text2.ID = node.ChildNodes[4].InnerText+"2";


                    tc2.Controls.Add(text); 
                    Label lbl=new Label();
                    lbl.Text=" 至 ";
                    tc2.Controls.Add(lbl);
                    tc2.Controls.Add(text2);
                    tr.Controls.Add(tc1);
                    tr.Controls.Add(tc2);
                }
                else if (node.ChildNodes[5].InnerText == "MultiLineTextBox")
                {
                    TextBox text = new TextBox();
                    text.Width = 165;
                    text.TextMode = TextBoxMode.MultiLine;
                    text.Rows = 2;
                    text.ID = node.ChildNodes[4].InnerText;// "txt_" + node.ChildNodes[0].InnerText;
                    tc2.Controls.Add(text);
                    tr.Controls.Add(tc1);
                    tr.Controls.Add(tc2);
                }
                else
                {
                    tr.Controls.Add(tc1);
                    tr.Controls.Add(tc2);
                    TextBox text = new TextBox();
                    text.Width = 165;
                    text.Attributes.Add("class", "TextBox");
                    text.ID = node.ChildNodes[4].InnerText;//"txt_" + node.ChildNodes[0].InnerText;
                    tc2.Controls.Add(text);
                }
            }
        }
        #endregion
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbFilter = new System.Text.StringBuilder("1=1");

        foreach (XmlNode node in nodeTable.ChildNodes)
        {

            if (node.Attributes["DisplayWhenQuery"].Value == "T")
            {

                if (node.ChildNodes[5].InnerText == "DropdownList")
                {
                    DropDownList ddl = (DropDownList)(tblQuery.FindControl(node.ChildNodes[4].InnerText));
                    sbFilter.Append(" and " + node.ChildNodes[0].InnerText + " like '%"+ddl.SelectedValue+"%'");
                }

                else if (node.ChildNodes[2].InnerText == "DateTime")
                {
                    string start = "1900-01-01";
                    string end = "3000-01-01";
                    TextBox text = (TextBox)(tblQuery.FindControl(node.ChildNodes[4].InnerText));
                    if (text.Text != "")
                    {
                        start = text.Text;
                        sbFilter.Append(" and " + node.ChildNodes[0].InnerText + ">='" + start + "'  ");
                    }
                    TextBox text2 = (TextBox)(tblQuery.FindControl(node.ChildNodes[4].InnerText+"2"));
                    if (text2.Text != "")
                    {
                        end = text2.Text;
                        sbFilter.Append(" and " + node.ChildNodes[0].InnerText + "<='" + end + "'");
                    }

                }
                else
                {
                    TextBox text = (TextBox)(tblQuery.FindControl(node.ChildNodes[4].InnerText));
                    if (text.Text.Trim().Length > 0)
                    {
                        sbFilter.Append(" and " + node.ChildNodes[0].InnerText + " like '%" + text.Text.Trim() + "%'");
                    }
                }
            }
        }
        Session["filter"] = sbFilter.ToString();
        JScript.Instance.RegisterScript(this, string.Format(@"ReturnValue(""{0}"")",sbFilter.ToString()));
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        JScript.Instance.RegisterScript(this, "window.opener=null;window.close();");
    }
}
