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
using THOK.WMS.BLL;
using System.Text;

public partial class Code_StorageManagement_CheckBillByChanged : BasePage
{
    public string div01display = "block";
    public string div02display = "none";
    Warehouse objHouse = new Warehouse();
    WarehouseArea objArea = new WarehouseArea();
    WarehouseShelf objShelf = new WarehouseShelf();
    WarehouseCell objCell = new WarehouseCell();
    DataSet dsHouse;
    string cellCodes = "''";//异动货位
    string filter = "1=1";
    int pageIndex = 1;
    int pageSize = 10;
    protected void Page_Load(object sender, EventArgs e)
    {
        objCell.UpdateCellEx();
        objCell.UpdateCell();
        if (!IsPostBack)
        {
            ViewState["pageIndex"] = pageIndex;
            btnChange_Click();
        }
        else
        {
            if (ViewState["filter"] != null && ViewState["filter"] != "")
            {
                filter = ViewState["filter"].ToString();
            }
            if (ViewState["cellCodes"] != null && ViewState["cellCodes"] != "")
            {
                cellCodes = ViewState["cellCodes"].ToString();
            }
            pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
            DataGridBind();
        }
    }
    protected void DataGridBind()
    {
        pager.RecordCount = objCell.GetRowCount(filter);
        pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
        this.dgCell.DataSource = objCell.QueryWarehouseCell(filter, pageIndex, pageSize).Tables[0];
        this.dgCell.DataBind();
    }
    #region DataGrid绑定事件
    protected void dgCell_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                for (int i = 0; i < dgCell.Items.Count; i++)
                {
                    e.Item.Attributes.Add("style", " z-index:10;    position:relative;   top:expression(this.offsetParent.scrollTop);");
                    e.Item.Attributes.Add("class", "GridHeader2");
                }
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int sn = e.Item.ItemIndex + 1;
                e.Item.Cells[0].Text = sn.ToString();
                if (Session["grid_EvenRowColor"] != null && e.Item.ItemType == ListItemType.Item)
                {
                    e.Item.Attributes.Add("style", string.Format("word-break:keep-all; white-space:nowrap; background-color:{0};", Session["grid_EvenRowColor"].ToString()));
                }
                if (Session["grid_OddRowColor"] != null && e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    e.Item.Attributes.Add("style", string.Format("word-break:keep-all; white-space:nowrap; background-color:{0};", Session["grid_OddRowColor"].ToString()));
                }

                e.Item.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='WhiteSmoke',this.style.fontWeight=''; this.style.cursor='hand';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

            }
        }
        catch
        {
        }
    }

    #endregion


    #region  下一步按钮
    protected void btnNext1_Click(object sender, EventArgs e)
    {
        DataTable tableCell=null;
        if (cellCodes != "" && cellCodes != "''")
        {
            tableCell = objCell.QueryWarehouseCell(filter).Tables[0];
            this.dgSelectedCell.DataSource = tableCell;
            this.dgSelectedCell.DataBind();
            this.btnSave.Enabled = true;
        }
        else
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, "没有异动货位，请重新盘点！");
            this.btnSave.Enabled = false;
        }
        div01display = "none";
        div02display = "block";
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CheckBillMaster billMaster = new CheckBillMaster();
        DataTable tableCell=null;
        if (cellCodes != "" && cellCodes != "''")
        {
            tableCell = objCell.QueryWarehouseCell(filter).Tables[0];
            billMaster.BatchInsertBill(tableCell, Session["EmployeeCode"].ToString());
            JScript.Instance.ShowMessage(this.UpdatePanel1, "盘点单已经生成");
        }
        else
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, "没有异动货位，请重新盘点！");
        }
    }

    # region 分页控件 页码changing事件
    protected void pager_PageChanging(object src, PageChangingEventArgs e)
    {
        pager.CurrentPageIndex = e.NewPageIndex;
        pageIndex = pager.CurrentPageIndex;
        ViewState["pageIndex"] = pageIndex;
        DataGridBind();
    }
    #endregion

    #region 异动盘点
    protected void btnChange_Click()
    {
        DataTable cellCodeIn = objCell.InChangeCell(DateTime.Now).Tables[0];
        for (int i = 0; i < cellCodeIn.Rows.Count; i++)
        {
            cellCodes += ",'" + cellCodeIn.Rows[i]["CELLCODE"] + "'";
        }
        DataTable cellCodeOut = objCell.OutChangeCell(DateTime.Now).Tables[0];
        for (int i = 0; i < cellCodeOut.Rows.Count; i++)
        {
            cellCodes += ",'" + cellCodeOut.Rows[i]["CELLCODE"] + "'";
        }
        DataTable cellCodeMove = objCell.MoveChangeCell(DateTime.Now).Tables[0];
        for (int i = 0; i < cellCodeMove.Rows.Count; i++)
        {
            cellCodes += ",'" + cellCodeMove.Rows[i]["OUT_CELLCODE"] + "'";
            cellCodes += ",'" + cellCodeMove.Rows[i]["IN_CELLCODE"] + "'";
        }
        if (cellCodes == "''")
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, "没有货位发生变动");
            this.btnNext1.Visible = false;
        }
        else
        {
            filter = string.Format("CELLCODE IN ({0})  AND QUANTITY>0", cellCodes.Replace("'"[0], '"')).Replace('"', "'"[0]);
            ViewState["filter"] = filter;
            ViewState["cellCodes"] = cellCodes;
            DataGridBind();
        }
    }
    #endregion
}
