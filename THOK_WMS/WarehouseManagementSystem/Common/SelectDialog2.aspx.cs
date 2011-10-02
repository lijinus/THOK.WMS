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
using System.Text;
using System.Data.SqlClient;
using THOK.System.BLL;
using THOK.Util;

public partial class Common_SelectDialog2 : System.Web.UI.Page
{
    int pageIndex = 1;
    int pageSize = 10;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "1=1";
    string TableName = "";
    string TableView = "";
    string ReturnField = "";
    DataTable dtResult;
    Hashtable htFields;
    string queryFields;
    string PrimaryKey = "";
    string orderBy = "";
    //int ReturnCellIndex = -1;
    //SqlParameter[] aryParameter;
    StoredProcParameter param = new StoredProcParameter();
    int[] cellIndex;
    string[] aryReturn;

    DialogData dialog = new DialogData();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["hiddenIndex"] = "a";//Grid中隐藏列
                TableName = Request.QueryString["TableName"];
                ReturnField = Request.QueryString["ReturnField"];
                if (Request.QueryString["targetControls"] != null)
                {
                    this.hideTargetControls.Value = Request.QueryString["targetControls"];
                }
                aryReturn = ReturnField.Split(new string[] { "," }, StringSplitOptions.None);
                cellIndex = new int[aryReturn.Length];

                if (Request.QueryString["filterField"] != null)
                {
                    filter = Request.QueryString["filterField"] + string.Format(" like '%{0}%'",Request.QueryString["filterValue"]);
                }
                if (Request.QueryString["sqlFilter"] != "" && Request.QueryString["sqlFilter"] != null) 
                {
                    filter += " AND " + Request.QueryString["sqlFilter"].Replace('"',"'"[0]);
                }

                htFields = new Hashtable();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + string.Format("Code\\TableXML\\{0}.xml", TableName));
                StringBuilder fieldList = new StringBuilder();
                XmlNode nodeTable = xmlDoc.SelectSingleNode("TABLE");
                PrimaryKey = nodeTable.Attributes["PrimaryKey"].InnerText;
                orderBy = nodeTable.Attributes["OrderBy"].InnerText;
                int hiddenIndex = 0;
                foreach(XmlNode node in nodeTable.ChildNodes)
                {
                    if (node.Attributes["DisplayWhenSelect"].Value == "T")
                    {

                        fieldList.Append(node.ChildNodes[0].InnerText + ",");
                        htFields.Add(node.ChildNodes[0].InnerText, node.ChildNodes[1].InnerText);
                        if (node.Attributes["HIDDEN"].Value == "T")
                        {
                            //ViewState["hidden"] = node.ChildNodes[0].InnerText + ",";
                            ViewState["hiddenIndex"] = ViewState["hiddenIndex"].ToString() + "," + hiddenIndex.ToString();
                        }
                        else
                        {
                            this.ddl_Field.Items.Add(new ListItem(node.ChildNodes[1].InnerText, node.ChildNodes[0].InnerText));
                        }
                        //this.ddl_Field.Items.Add(new ListItem(node.ChildNodes[1].InnerText, node.ChildNodes[0].InnerText));
                    }
                    hiddenIndex++;
                }
                fieldList.Remove(fieldList.Length-1, 1);
                queryFields = fieldList.ToString();

                if (nodeTable.Attributes["ViewName"] != null && nodeTable.Attributes["ViewName"].InnerText != "")
                {
                    ViewState["tableView"] = nodeTable.Attributes["ViewName"].InnerText;
                    TableView = nodeTable.Attributes["ViewName"].InnerText;
                }
                else
                {
                    ViewState["tableView"] = TableName;
                    TableView = TableName;
                }

                this.InitParam();
                totalCount = dialog.GetRowCount(TableView,filter);//DataAccess.Instance.GetRowCount(TableView, filter);
                dtResult = dialog.GetData("cp_DataQuery",param).Tables[0];//dtResult = DataAccess.Instance.ExecuteProcedure("cp_DataQuery", aryParameter);
                this.dgResult.DataSource = this.dtResult;
                this.dgResult.DataBind();
                Paging();

                ViewState["htFields"] = htFields;
                ViewState["pageCount"] = pageCount;
                ViewState["pageIndex"] = pageIndex;
                ViewState["totalCount"] = totalCount;
                ViewState["filter"] = filter;
                ViewState["tableName"] = TableName;
                ViewState["ReturnField"] = ReturnField;
                ViewState["queryFields"] = queryFields;
                ViewState["PK"] = PrimaryKey;
                ViewState["OrderBy"] = orderBy;
            }
            else
            {
                htFields = (Hashtable)(ViewState["htFields"]);
                pageCount = Convert.ToInt32(ViewState["pageCount"]);
                pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
                totalCount = Convert.ToInt32(ViewState["totalCount"]);
                filter = ViewState["filter"].ToString();
                TableName = ViewState["tableName"].ToString();
                TableView = ViewState["tableView"].ToString();
                ReturnField =ViewState["ReturnField"].ToString();
                queryFields = ViewState["queryFields"].ToString();
                PrimaryKey = ViewState["PK"].ToString();
                orderBy = ViewState["OrderBy"].ToString();
                aryReturn = ViewState["ReturnField"].ToString().Split(new string[] { "," }, StringSplitOptions.None);
                cellIndex = new int[aryReturn.Length];
            }

        }
        catch (Exception exp)
        {
            //string strJScript = string.Format("alert(\"提示\", \"{0}\",\"确定\",\"\");",exp.Message);
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }


    #region 翻页处理
    public void Paging()
    {
        try
        {
            pageCount = totalCount / pageSize;
            if (totalCount % pageSize > 0)
                pageCount += 1;
            if (pageCount == 0)
                pageCount = 1;
            ddl_PageIndex.Items.Clear();
            for (int i = 1; i <= pageCount; i++)
            {
                ddl_PageIndex.Items.Add(i.ToString());
            }

            ChangeButtonState();
            this.lblCurrentPage.Text = pageIndex.ToString();
            this.lblPageCount.Text = pageCount.ToString();
            this.lblTotalCount.Text = totalCount.ToString();
            ViewState["pageCount"] = pageCount.ToString();
        }
        catch
        {

        }
    }
    #endregion

    #region 翻页后 link Button Enable改变
    public void ChangeButtonState()
    {
        if (pageCount <= 1)
        {
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            return;
        }
        if (pageIndex == 1)
        {
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.btnNext.Enabled = true;
            this.btnLast.Enabled = true;
        }
        else if (pageIndex == pageCount)
        {
            this.btnFirst.Enabled = true;
            this.btnPrevious.Enabled = true;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
        }
        else
        {
            this.btnFirst.Enabled = true;
            this.btnPrevious.Enabled = true;
            this.btnNext.Enabled = true;
            this.btnLast.Enabled = true;
        }
    }
    #endregion

    #region 翻页
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        pageIndex = 1;
        ChangePage();
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        pageIndex -= 1;
        ChangePage();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        pageIndex += 1;
        ChangePage();
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        pageIndex = pageCount;
        ChangePage();
    }

    void ChangePage()
    {
        ViewState["pageIndex"] = pageIndex;
        this.lblCurrentPage.Text = pageIndex.ToString();
        this.ddl_PageIndex.SelectedIndex = pageIndex - 1;
        ChangeButtonState();


        this.InitParam();
        totalCount = dialog.GetRowCount(TableView, filter);//DataAccess.Instance.GetRowCount(TableView, filter);
        dtResult = dialog.GetData("cp_DataQuery", param).Tables[0];//dtResult = DataAccess.Instance.ExecuteProcedure("cp_DataQuery", aryParameter);
        this.dgResult.DataSource = this.dtResult;
        this.dgResult.DataBind();
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        pageIndex = ddl_PageIndex.SelectedIndex + 1;
        ViewState["pageIndex"] = pageIndex;
        ChangePage();
    }
    #endregion

    protected void dgResult_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            for (int k = 0; k < e.Item.Cells.Count; k++)
            {
                if (ViewState["hiddenIndex"].ToString().Contains(k.ToString()))
                {
                    e.Item.Cells[k].Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Header)
            {
               // e.Item.Attributes.Add("style", "height:22px; background-image:url(../images/nav_b5.gif);  word-break:keep-all; white-space:nowrap;");
                for (int i = 0; i < e.Item.Cells.Count; i++)
                {
                    string fieldname = e.Item.Cells[i].Text.Trim();
                    //if (fieldname == ReturnField)
                    //{
                    //    ReturnCellIndex = i;
                    //}
                    for (int k = 0; k < aryReturn.Length; k++)
                    {
                        if (aryReturn[k] == fieldname)
                        {
                            cellIndex[k] = i;
                            break;
                        }
                    }

                    e.Item.Cells[i].Text = htFields[fieldname].ToString();
                    //e.Item.Cells[i].Attributes.Add("style", "padding-left:1em; padding-right:1em;");
                    e.Item.Cells[i].Attributes.Add("style", "word-break:keep-all; white-space:nowrap");
                }
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (Session["grid_EvenRowColor"] != null && e.Item.ItemType == ListItemType.Item)
                {
                    e.Item.Attributes.Add("style", string.Format("word-break:keep-all; white-space:nowrap; background-color:{0};", Session["grid_EvenRowColor"].ToString()));
                }

                if (Session["grid_OddRowColor"] != null && e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    e.Item.Attributes.Add("style", string.Format("word-break:keep-all; white-space:nowrap; background-color:{0};", Session["grid_OddRowColor"].ToString()));
                }
                string Value = "";
                for (int n = 0; n < cellIndex.Length; n++)
                {
                    Value = Value + e.Item.Cells[cellIndex[n]].Text + "|";
                }
                e.Item.Attributes.Add("Title","双击取值");
                e.Item.Attributes.Add("ondblclick", string.Format("ReturnValue('{0}');",Value));
                e.Item.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#e8e8e7',this.style.fontWeight=''; this.style.cursor='hand';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                for (int j = 0; j < e.Item.Cells.Count; j++)
                {
                    e.Item.Cells[j].Attributes.Add("style", "height: 26px; padding-left:1em; padding-right:1em;word-break:keep-all; white-space:nowrap");
                }
            }
        }
        catch (Exception exp)
        {
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            pageIndex = 1;
            //TableView = ViewState["tableView"].ToString();
            filter = string.Format(" {0} like '{1}%' ", this.ddl_Field.SelectedValue, this.txtKeywords.Text.Trim());
            if (Request.QueryString["sqlFilter"] != "" && Request.QueryString["sqlFilter"] != null)
            {
                filter += " AND " + Request.QueryString["sqlFilter"].Replace('"',"'"[0]);
            }
            ViewState["filter"] = filter;
            this.InitParam();
            totalCount = dialog.GetRowCount(TableView, filter);//DataAccess.Instance.GetRowCount(TableView, filter);
            dtResult = dialog.GetData("cp_DataQuery", param).Tables[0];//dtResult = DataAccess.Instance.ExecuteProcedure("cp_DataQuery", aryParameter);
            this.dgResult.DataSource = this.dtResult;
            this.dgResult.DataBind();
            Paging();  
        }
        catch (Exception exp)
        {

        }
    }

    #region 参数
    private void InitParam()
    {
        param.Names.Add("pageIndex");
        param.Names.Add("pageSize");
        param.Names.Add("filter");
        param.Names.Add("orderBy");
        param.Names.Add("PrimaryKey");
        param.Names.Add("TableViewName");
        param.Names.Add("QueryFields");
        param.Values.Add(pageIndex.ToString());
        param.Values.Add(pageSize.ToString());
        param.Values.Add(filter);
        param.Values.Add(ReturnField);
        param.Values.Add(PrimaryKey);
        param.Values.Add(TableView);
        param.Values.Add(queryFields);
        param.Types.Add(DbType.Int32);
        param.Types.Add(DbType.Int32);
        param.Types.Add(DbType.String);
        param.Types.Add(DbType.String);
        param.Types.Add(DbType.String);
        param.Types.Add(DbType.String);
        param.Types.Add(DbType.String);
    }
    #endregion
}
