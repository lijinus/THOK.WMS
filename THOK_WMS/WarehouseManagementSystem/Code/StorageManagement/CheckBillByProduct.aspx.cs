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

public partial class Code_StorageManagement_CheckBillByProduct : BasePage
{
    public string div01display = "block";
    public string div02display = "none";
    Product objProduct = new Product();
    DataSet dsProduct = new DataSet();
    //public int leftIndex = 0;
    //public int rightIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dsProduct = objProduct.QueryProduct(1, 10000, "1=1", "PRODUCTCODE");
            DataColumn newCol = new DataColumn("Selected", System.Type.GetType("System.String"));
            newCol.DefaultValue = "F";
            dsProduct.Tables[0].Columns.Add(newCol);
            GridDataBind();
            Session["dsProduct"] = dsProduct;
        }
        else
        {
            dsProduct = (DataSet)Session["dsProduct"];
            GridDataBind();
        }
    }

    void GridDataBind()
    {
        DataView dvLeft = dsProduct.Tables[0].DefaultView;
        dvLeft.RowFilter = "Selected='F'";
        if (this.rbCode.Checked)
        {
            dvLeft.Sort = "PRODUCTCODE";
        }
        else
        {
           dvLeft.Sort = "PRODUCTNAME";
        }
        
        if (Convert.ToInt32(this.hdnLeftRowIndex.Value) >= dvLeft.Count)
        {
            int c = dvLeft.Count - 1;
            this.hdnLeftRowIndex.Value = c.ToString();
        }
        if (dvLeft.Count > 0 && Convert.ToInt32(this.hdnLeftRowIndex.Value) < 0)
        {
            this.hdnLeftRowIndex.Value = "0";
        }
        this.dgProduct.DataSource = dvLeft;
        this.dgProduct.DataBind();



        DataView dvRight = dsProduct.Tables[0].DefaultView;
        dvRight.RowFilter = "Selected='T'";
        if (this.rbCode.Checked)
        {
            dvRight.Sort = "PRODUCTCODE";
        }
        else
        {
            dvRight.Sort = "PRODUCTNAME";
        }
        if (Convert.ToInt32(this.hdnRightRowIndex.Value) >= dvRight.Count)
        {
            int c = dvRight.Count - 1;
            this.hdnRightRowIndex.Value = c.ToString();
        }
        if (dvRight.Count > 0 && Convert.ToInt32(this.hdnRightRowIndex.Value) < 0)
        {
            this.hdnRightRowIndex.Value = "0";
        }
        this.dgSelected.DataSource = dvRight;
        this.dgSelected.DataBind();
    }

    //全部隐藏（加到左边栏）
    protected void btnAllToLeft_Click(object sender, EventArgs e)
    {
        foreach (DataRow dr in dsProduct.Tables[0].Rows)
        {
            dr["Selected"] = "F";
        }

        GridDataBind();
    }
    //全部显示（加到右边栏）
    protected void btnAllToRight_Click(object sender, EventArgs e)
    {
        foreach (DataRow dr in dsProduct.Tables[0].Rows)
        {
            dr["Selected"] = "T";
        }
        GridDataBind();
    }

    //单项加到左栏
    protected void btnToLeft_Click(object sender, EventArgs e)
    {
        if (dgSelected.Items.Count == 0)
        {
            return;
        }

        string productcode = this.dgSelected.Items[Convert.ToInt32(this.hdnRightRowIndex.Value)].Cells[1].Text;
        DataRow[] rows = dsProduct.Tables[0].Select("PRODUCTCODE='" + productcode + "'");
        if (rows.Length == 1)
        {
            rows[0]["Selected"] = "F";
        }
        GridDataBind();
    }

    //单项加到右栏
    protected void btnToRight_Click(object sender, EventArgs e)
    {
        if (this.dgProduct.Items.Count == 0)
        {
            return;
        }
        string productcode = this.dgProduct.Items[Convert.ToInt32(this.hdnLeftRowIndex.Value)].Cells[1].Text;
        DataRow[] rows = dsProduct.Tables[0].Select("PRODUCTCODE='"+productcode+"'");
        if (rows.Length == 1)
        {
            rows[0]["Selected"] = "T";
        }
        GridDataBind();
    }



    protected void dgProduct_ItemDataBound(object sender, DataGridItemEventArgs e) 
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            for (int i = 0; i < dgProduct.Items.Count; i++)
            {
                e.Item.Attributes.Add("style", " z-index:10;    position:relative;   top:expression(this.offsetParent.scrollTop);");
            }
        }
        else
        {
            if (e.Item.ItemIndex == Convert.ToInt32(this.hdnLeftRowIndex.Value))
            {
                e.Item.Cells[0].Text = "<img src=../../images/arrow01.gif />";
            }
            e.Item.Attributes.Add("onclick", string.Format("selectRow('dgProduct',{0});", e.Item.ItemIndex));
            e.Item.Attributes.Add("style", "cursor:hand;");
        }
    }



    protected void dgSelected_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
           e.Item.Attributes.Add("style", " z-index:10;    position:relative;   top:expression(this.offsetParent.scrollTop);");
        }
        else
        {
            if (e.Item.ItemIndex == Convert.ToInt32(this.hdnRightRowIndex.Value))
            {
                e.Item.Cells[0].Text = "<img src=../../images/arrow01.gif />";
            }
            e.Item.Attributes.Add("onclick", string.Format("selectRow('dgSelected',{0});", e.Item.ItemIndex));
            e.Item.Attributes.Add("style", "cursor:hand;");
        }
    }



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
                    e.Item.Attributes.Add("style", string.Format("cursor:hand; word-break:keep-all; white-space:nowrap; background-color:{0};", Session["grid_EvenRowColor"].ToString()));
                }
                if (Session["grid_OddRowColor"] != null && e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    e.Item.Attributes.Add("style", string.Format("cursor:hand; word-break:keep-all; white-space:nowrap; background-color:{0};", Session["grid_OddRowColor"].ToString()));
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


    protected void btnNext2_Click(object sender, EventArgs e)
    {
        //div01display = "none";
        //div02display = "block";
        WarehouseCell objCell = new WarehouseCell();
        CheckBillMaster billMaster = new CheckBillMaster();
        DataTable tableCell = objCell.QueryWarehouseCell("CURRENTPRODUCT IN (" + Session["selectedProductCode"].ToString() + ")").Tables[0];
        billMaster.BatchInsertBill(tableCell, Session["EmployeeCode"].ToString());
        JScript.Instance.ShowMessage(this.UpdatePanel1, "盘点单已经生成");
    }

    protected void btnNext1_Click(object sender, EventArgs e)
    {
        DataView dvRight = ((DataSet)Session["dsProduct"]).Tables[0].DefaultView;
        dvRight.RowFilter = "Selected='T'";
        StringBuilder selectedProductCode = new StringBuilder();
        selectedProductCode.Append("'0'");
        foreach (DataRowView rowView in dvRight)
        {
            selectedProductCode.Append(",'"+rowView["PRODUCTCODE"]+"'");
        }
        WarehouseCell objCell = new WarehouseCell();
        DataTable tableCell = objCell.QueryWarehouseCell("CURRENTPRODUCT IN ("+selectedProductCode.ToString()+")").Tables[0];
        this.dgSelectedCell.DataSource = tableCell.DefaultView;
        this.dgSelectedCell.DataBind();
        Session["selectedProductCode"] = selectedProductCode.ToString();
        div01display = "none";
        div02display = "block";
        if (tableCell.Rows.Count == 0)
        {
            this.btnNext2.Enabled = false;
        }
        else
        {
            this.btnNext2.Enabled = true;
        }
    }


    protected void rbName_CheckedChanged(object sender, EventArgs e)
    {

        GridDataBind();
    }
}
