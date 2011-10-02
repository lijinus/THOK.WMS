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

public partial class Code_StorageManagement_CheckBillByProduct :BasePage
{
    public string div01display = "block";
    public string div02display = "none";
    Product objProduct = new Product();
    Warehouse objHouse = new Warehouse();
    WarehouseArea objArea = new WarehouseArea();
    WarehouseShelf objShelf = new WarehouseShelf();
    WarehouseCell objCell = new WarehouseCell();
    DataSet dsHouse;
    //DataSet dsArea;
    //DataSet dsShelf;
    //DataSet dsCell;
    string filter = "1=1";
    int pageIndex = 1;
    int pageSize = 10;
    WarehouseCell warecell = new WarehouseCell();
    protected void Page_Load(object sender, EventArgs e)
    {
        objCell.UpdateCellEx();
        objCell.UpdateCell();
        if (!IsPostBack)
        {
            pager.PageSize = pageSize;
            ViewState["filter"] = filter;
            ViewState["pageIndex"] = pageIndex;
            DataGridBind();
        }
        else
        {
            filter = ViewState["filter"].ToString();
            pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
            DataGridBind();
        }
    }


    protected void DataGridBind()
    {
        this.dgProduct.DataSource = objProduct.QueryProduct(1, 1500, "1=1", "PRODUCTCODE");
        this.dgProduct.DataBind();

        filter = ViewState["filter"].ToString();
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

                e.Item.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#e8e8e7',this.style.fontWeight=''; this.style.cursor='hand';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

            }
        }
        catch
        {
        }
    }

    #endregion


    protected void btnNext1_Click(object sender, EventArgs e)
    {
        DataTable tableCell;
        StringBuilder selectedCellCode = new StringBuilder();
        selectedCellCode.Append("''");

        Session["selectedCellCode"] = selectedCellCode.ToString();
        tableCell = objCell.QueryWarehouseCell("CELLCODE IN (" + selectedCellCode.ToString() + ")").Tables[0];
        this.dgSelectedCell.DataSource = tableCell;
        this.dgSelectedCell.DataBind();
        div01display = "none";
        div02display = "block";
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        CheckBillMaster billMaster = new CheckBillMaster();
        DataTable tableCell = objCell.QueryWarehouseCell("CELLCODE IN (" + Session["selectedCellCode"].ToString() + ")").Tables[0];
        billMaster.BatchInsertBill(tableCell, Session["EmployeeCode"].ToString());
        Response.Redirect("CheckBillConfirmPage.aspx");
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


    protected void dgProduct_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                CheckBox chk = new CheckBox();
                chk.Attributes.Add("style", "text-align:center;word-break:keep-all; white-space:nowrap");
                chk.ID = "checkAll";
                chk.Attributes.Add("onclick", "checkboxChange(this,'dgProduct',0);");

                e.Item.Cells[0].Controls.Add(chk);
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chk = new CheckBox();
                Label lblEdit = new Label();
                e.Item.Cells[0].Controls.Add(chk);

                e.Item.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#e8e8e7',this.style.fontWeight=''; this.style.cursor='hand';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

            }
        }
        catch
        {
        }
    }
    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        string productFilter = string.Format("PRODUCTCODE LIKE '{0}%' OR PRODUCTNAME LIKE '%{0}%'",this.keywords.Text.Trim());
        this.dgProduct.DataSource = objProduct.QueryProduct(1, 1500, productFilter, "PRODUCTCODE");
        this.dgProduct.DataBind();
    }
}
