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
using THOK.WMS.Allot;
using System.Drawing;
using System.Collections.Generic;
using THOK.WMS.Upload;
using THOK.WMS.Upload.Bll;

public partial class Code_StockEntry_EntryAutoAllotPage : BasePage
{
    EntryBillMaster billMaster = new EntryBillMaster();
    EntryBillDetail billDetail = new EntryBillDetail();
    UpdateUploadBll updateBll = new UpdateUploadBll();
    DataSet dsDetail;
    EntryAllot objAllot = new EntryAllot();
    DataTable tableAllotment; //分配结果
    DataTable tableAvailableCell1;//可分配的零烟货位
    DataTable tableAvailableCell;//可分配的货位
    DataTable tableAvailableCell2;//可分配暂存烟道储位
    WarehouseCell warecell = new WarehouseCell();
    //EntryOrderAllot cAllot = new EntryOrderAllot();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        warecell.UpdateCellEx();
        warecell.UpdateCell();
        this.lbError.Items.Clear();
        this.lbError.Items.Add("错误指令:");
        Response.ExpiresAbsolute = System.DateTime.Now;
        if (!IsPostBack)
        {
            dsDetail = billDetail.QueryByBillNo(Session["BillNoList"].ToString());
            this.dgDetail.DataSource = dsDetail.Tables[0];
            this.dgDetail.DataBind();
            GridRowSpan(dgDetail, 1);
            Session["tableAllotment"] = null;
        }
        else
        {
            dsDetail = billDetail.QueryByBillNo(Session["BillNoList"].ToString());
            this.dgDetail.DataSource = dsDetail.Tables[0];
            this.dgDetail.DataBind();
            GridRowSpan(dgDetail, 1);

            if (Session["tableAllotment"] != null)
            {
                tableAllotment = (DataTable)Session["tableAllotment"];
                for (int i = 0; i < dgResult.Items.Count; i++)
                {
                    TextBox textCode = (TextBox)dgResult.Items[i].Cells[6].Controls[0];
                    tableAllotment.Rows[i]["CELLCODE"] = textCode.Text;
                    TextBox textName = (TextBox)dgResult.Items[i].Cells[7].Controls[0];
                    tableAllotment.Rows[i]["CELLNAME"] = textName.Text;
                    TextBox textAllotQty = (TextBox)dgResult.Items[i].Cells[8].Controls[0];
                    tableAllotment.Rows[i]["ALLOTQUANTITY"] = Convert.ToDecimal(textAllotQty.Text);
                }
                this.dgResult.DataSource = tableAllotment;
                this.dgResult.DataBind();
                GridRowSpan(dgResult, 1);
            }
        }
    }

    #region 执行分配
    protected void btnAllot_Click(object sender, EventArgs e)
    {
        EntryOrderAllot callot = new EntryOrderAllot(dsDetail);
        callot.Allot();
       
        foreach (string error in callot.errorlist)
        {
            this.lbError.Items.Add(error);
        }
        
        if (this.lbError.Items.Count==1)
        {
            tableAllotment = callot.tableAllotment;
            this.dgResult.DataSource = tableAllotment;
            this.dgResult.DataBind();
            //ShowAllotedCell();
            GridRowSpan(dgResult, 1);
            this.pnlResult1.Visible = true;
            this.pnlResult.Visible = true;
            this.btnSaveAllotment.Enabled = true;
            Session["tableAllotment"] = tableAllotment;
            JScript.Instance.ShowMessage(this, "入库单已经分配，请点击保存分配结果按钮，将分配数据保存到数据库！");
        }
        else
        {
            this.lbError.Visible = true;
        }
    }
    #endregion

    #region 分配指定卷烟(已分离)
    //protected void AllotAssignedCell(DataTable tableAvailableCell, DataTable tableAvailableCell1,DataTable tableAvailableCell2, DataRow rowToAllot, decimal remainQuantity, decimal tiao)
    //{
    //    int entryDetailID = Convert.ToInt32(rowToAllot["ID"]);
    //    string entryBillNO = rowToAllot["BILLNO"].ToString();
    //    string entryProductCode = rowToAllot["PRODUCTCODE"].ToString();
    //    string entryProductName = rowToAllot["PRODUCTNAME"].ToString();
    //    string entryUnitCode = rowToAllot["UNITCODE"].ToString().Trim();
    //    string entryUnitName = rowToAllot["UNITNAME"].ToString().Trim();
    //        DataRow[] rows = tableAvailableCell.Select("ASSIGNEDPRODUCT='" + entryProductCode + "'AND AVAILABLE=MAX_QUANTITY");//指定卷烟货位为空
    //        DataRow[] rows0 = tableAvailableCell.Select("ASSIGNEDPRODUCT='" + entryProductCode + "'AND AVAILABLE>0");//指定卷烟还可以存烟
    //        DataRow[] rows1 = tableAvailableCell1.Select("ASSIGNEDPRODUCT='" + entryProductCode + "'");//零烟
    //        //DataRow[] rows3 = tableAvailableCell2.Select("AVAILABLE>0 ");//暂存
    //        DataRow[] rows3 = tableAvailableCell2.Select("AVAILABLE=MAX_QUANTITY ");
    //        decimal availableQuantity = 0.00M;
    //        decimal availableQtyTiao = 0.00M;
    //        if (tiao > 0)
    //        {
    //            if (rows1.Length > 0)//零烟分配*****
    //            {
    //                tiao = rowAllotedTiao(rows1, tiao, rowToAllot, availableQtyTiao);
    //                if (tiao > 0)
    //                {
    //                    tiao = AllotUnAssignedCellTiao(tableAvailableCell, tableAvailableCell1, rowToAllot, tiao);
    //                }
    //                if (tiao > 0)
    //                {
    //                        this.lbError.Items.Add("零烟货位已满，请添加货位或暂停入库！");
    //                }
    //            }
    //            else
    //            {
    //                if (tiao > 0)
    //                {
    //                    tiao = AllotUnAssignedCellTiao(tableAvailableCell, tableAvailableCell1, rowToAllot, tiao);
    //                }
    //                if (tiao > 0)
    //                {
    //                        this.lbError.Items.Add("零烟货位已满，请添加货位或暂停入库！");
    //                }
    //            }
    //        }
    //        if (remainQuantity > 0)//件烟分配
    //        {
    //            if (rows.Length > 0)
    //            {
    //                remainQuantity = rowAllotedJian(rows, remainQuantity, rowToAllot, availableQuantity);
    //                if (remainQuantity > 0)
    //                {
    //                    if (rows0.Length > 0)
    //                    {
    //                        remainQuantity = rowAllotedJian(rows0, remainQuantity, rowToAllot, availableQuantity);
    //                    }
    //                    if (remainQuantity > 0)
    //                    {
    //                        remainQuantity = AllotUnAssignedCell(tableAvailableCell, tableAvailableCell1, rowToAllot, remainQuantity);
    //                    }
    //                }
    //                if (remainQuantity > 0)
    //                {
    //                    if (rows3.Length > 0)//暂存件烟区
    //                    {
    //                        remainQuantity = rowAllotedJian(rows3, remainQuantity, rowToAllot, availableQuantity);
    //                    }
    //                    else
    //                    {
    //                        this.lbError.Items.Add("件烟货位已满，请添加货位或暂停入库！");
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                if (rows0.Length > 0)
    //                {
    //                    remainQuantity = rowAllotedJian(rows0, remainQuantity, rowToAllot, availableQuantity);
    //                }
    //                if(remainQuantity>0)
    //                {
    //                    remainQuantity = AllotUnAssignedCell(tableAvailableCell, tableAvailableCell1, rowToAllot, remainQuantity);
    //                    if (remainQuantity > 0)
    //                    {
    //                        if (rows3.Length > 0)//暂存件烟区
    //                        {
    //                            remainQuantity = rowAllotedJian(rows3, remainQuantity, rowToAllot, availableQuantity);
    //                        }
    //                        else
    //                        {
    //                            //this.labError.InnerText = labError.InnerText + "暂存区条烟货位已满，请添加货位或暂停入库！";
    //                            this.lbError.Items.Add("件烟货位已满，请添加货位或暂停入库！");
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //}
    //#endregion

    //#region 分配无指定卷烟件烟货位
    //protected decimal AllotUnAssignedCell(DataTable tableAvailableCell, DataTable tableAvailableCell1, DataRow rowToAllot, decimal remainQuantity)
    //{
    //    int entryDetailID = Convert.ToInt32(rowToAllot["ID"]);
    //    string entryBillNO = rowToAllot["BILLNO"].ToString();
    //    string entryProductCode = rowToAllot["PRODUCTCODE"].ToString();
    //    string entryProductName = rowToAllot["PRODUCTNAME"].ToString();
    //    string entryUnitCode = rowToAllot["UNITCODE"].ToString().Trim();
    //    string entryUnitName = rowToAllot["UNITNAME"].ToString().Trim();
    //    decimal availableQuantity = 0.00M;
    //    //查询出的货位货位数量为0，不用考虑有货物的货位
    //    DataRow[] rowsAlloted = tableAvailableCell.Select("(ASSIGNEDPRODUCT='' or ASSIGNEDPRODUCT IS NULL ) AND CURRENTPRODUCT='" + entryProductCode + "'", "");        
    //    //2已经分配过当前入库产品的货位（针对非空货位分配）
    //    DataRow[] rowsAlloted1 = tableAvailableCell.Select("(ASSIGNEDPRODUCT='' or ASSIGNEDPRODUCT IS NULL) AND (CURRENTPRODUCT=''or CURRENTPRODUCT IS NULL)AND AVAILABLE=MAX_QUANTITY ");
    //    //指定卷烟的货位已放满，查找指定卷烟这排货架是否也放满，未满就分配这个货架的空货位
    //    string shelfCode = warecell.QueryShelfCode(entryProductCode);
    //    DataRow[] rowsAlloted2 = tableAvailableCell.Select("(ASSIGNEDPRODUCT='' or ASSIGNEDPRODUCT IS NULL) AND (CURRENTPRODUCT=''or CURRENTPRODUCT IS NULL)AND AVAILABLE=MAX_QUANTITY AND SHELFCODE IN(" + shelfCode + ")");
    //    //DataRow[] rowsAllotedTiao1 = tableAvailableCell1.Select("(ASSIGNEDPRODUCT='' or ASSIGNEDPRODUCT IS NULL) AND (PRODUCTCODE=''or PRODUCTCODE IS NULL)");
    //    //1有货位未指定卷烟且货位为空,将卷烟分给该货位
    //    DataRow[] rowsNull = tableAvailableCell.Select("ASSIGNEDPRODUCT<>'" + entryProductCode + "' AND (CURRENTPRODUCT=''or CURRENTPRODUCT IS NULL)");
    //    DataRow[] rowsNull1 = tableAvailableCell.Select("ASSIGNEDPRODUCT<>'" + entryProductCode + "'AND CURRENTPRODUCT='" + entryProductCode + "'", "");
    //    //4没有指定卷烟的货位满了，如果有货位有指定卷烟但货位为空，将卷烟分给该货位
    //    //if (entryUnitName == "件")
    //    //{  暂存区

    //    //指定卷烟货位已满，判断指定卷烟所在的货架是否还有空货位
    //    if (remainQuantity > 0)
    //    {
    //        if (rowsAlloted2.Length > 0)
    //        {
    //            remainQuantity = rowAllotedJian(rowsAlloted2, remainQuantity, rowToAllot, availableQuantity);
    //        }
    //    }

    //    if (remainQuantity > 0)
    //    {
    //        if (rowsAlloted1.Length > 0)
    //        {
    //            remainQuantity = rowAllotedJian(rowsAlloted1, remainQuantity, rowToAllot, availableQuantity);
    //        }
    //    }
    //    if (remainQuantity > 0)
    //    {
    //        if (rowsAlloted.Length > 0)
    //        {
    //            remainQuantity = rowAllotedJian(rowsAlloted, remainQuantity, rowToAllot, availableQuantity);
    //        }
    //    }
    //    if (remainQuantity > 0)
    //    {
    //        if (rowsNull.Length > 0)
    //        {
    //            remainQuantity = rowAllotedJian(rowsNull, remainQuantity, rowToAllot, availableQuantity);
    //        }
    //    }
    //    if (remainQuantity > 0)
    //    {
    //        if (rowsNull1.Length > 0)
    //        {
    //            remainQuantity = rowAllotedJian(rowsNull1, remainQuantity, rowToAllot, availableQuantity);
    //        }
    //    }
    //    return remainQuantity;
    //}
    //#endregion

    //#region 分配无指定卷烟条烟货位
    //protected decimal AllotUnAssignedCellTiao(DataTable tableAvailableCell, DataTable tableAvailableCell1, DataRow rowToAllot, decimal tiao)
    //{
    //    int entryDetailID = Convert.ToInt32(rowToAllot["ID"]);
    //    string entryBillNO = rowToAllot["BILLNO"].ToString();
    //    string entryProductCode = rowToAllot["PRODUCTCODE"].ToString();
    //    string entryProductName = rowToAllot["PRODUCTNAME"].ToString();
    //    string entryUnitName = rowToAllot["UNITNAME"].ToString().Trim();
    //    decimal availableQtyTiao = 0.00M;
    //    //查询出的货位货位数量为0，不用考虑有货物的货位
    //    DataRow[] rowsAllotedTiao = tableAvailableCell1.Select("(ASSIGNEDPRODUCT='' or ASSIGNEDPRODUCT IS NULL ) AND CURRENTPRODUCT='" + entryProductCode + "'", "");
    //    //2已经分配过当前入库产品的货位（针对非空货位分配）
    //    DataRow[] rowsAllotedTiao1 = tableAvailableCell1.Select("(ASSIGNEDPRODUCT='' or ASSIGNEDPRODUCT IS NULL) AND (CURRENTPRODUCT=''or CURRENTPRODUCT IS NULL)AND AVAILABLE=MAX_QUANTITY");
    //    //1有货位未指定卷烟且货位为空,将卷烟分给该货位
    //    DataRow[] rowsNullTiao = tableAvailableCell1.Select("ASSIGNEDPRODUCT<>'" + entryProductCode + "'AND AVAILABLE=MAX_QUANTITY");
    //    //4没有指定卷烟的货位满了，如果有货位有指定卷烟但货位为空，将卷烟分给该货位
    //    if (tiao > 0)
    //    {
    //        if (rowsAllotedTiao1.Length > 0)
    //        {
    //            tiao = rowAllotedTiao(rowsAllotedTiao1, tiao, rowToAllot, availableQtyTiao);
    //        }
    //    }
    //    if (tiao > 0)
    //    {
    //        if (rowsAllotedTiao.Length > 0)
    //        {
    //            tiao = rowAllotedTiao(rowsAllotedTiao, tiao, rowToAllot, availableQtyTiao);
    //        }
    //    }
    //    if (tiao > 0)
    //    {
    //        if (rowsNullTiao.Length > 0)
    //        {
    //            tiao = rowAllotedTiao(rowsNullTiao, tiao, rowToAllot, availableQtyTiao);
    //        }
    //    }
    //    return tiao;
    //}

    //#region 件烟
    //protected decimal rowAllotedJian(DataRow[] rowsAlloted, decimal quantity, DataRow rowToAllot, decimal availableQuantity)
    //{
    //    string PRODUCTCODE=rowToAllot["PRODUCTCODE"].ToString();
    //    string UNITCODE = objAllot.QueryProductCode(PRODUCTCODE).Tables[0].Rows[0][0].ToString();
    //    string UNITNAME = objAllot.QueryUnitName(UNITCODE).Tables[0].Rows[0][0].ToString();
    //    object MAX_QUANTITY = objAllot.QueryJian2(PRODUCTCODE).Tables[0].Rows[0][1].ToString();
    //    for (int i = 0; i < rowsAlloted.Length; i++)
    //    {
    //        if (quantity == 0)
    //        {
    //            break;
    //        }
    //        DataRow newRow = tableAllotment.NewRow();
    //        newRow["BILLNO"] = rowToAllot["BILLNO"].ToString();
    //        newRow["DETAILID"] = Convert.ToInt32(rowToAllot["ID"]);
    //        newRow["PRODUCTCODE"] = rowToAllot["PRODUCTCODE"].ToString();
    //        newRow["PRODUCTNAME"] = rowToAllot["PRODUCTNAME"].ToString();
    //        newRow["UNITCODE"] = UNITCODE;
    //        newRow["UNITNAME"] = UNITNAME;
    //        newRow["CELL_ID"] = rowsAlloted[i]["CELL_ID"];
    //        newRow["SHELFCODE"] = rowsAlloted[i]["SHELFCODE"];
    //        newRow["CELLCODE"] = rowsAlloted[i]["CELLCODE"];
    //        newRow["CELLNAME"] = rowsAlloted[i]["CELLNAME"];
    //        if (MAX_QUANTITY == null || MAX_QUANTITY == "")
    //        {
    //            newRow["MAX_QUANTITY"] = rowsAlloted[i]["MAX_QUANTITY"];
    //        }
    //        else
    //        {
    //            newRow["MAX_QUANTITY"] = MAX_QUANTITY;
    //        }
    //        if (decimal.Parse(rowsAlloted[i]["AVAILABLE"].ToString()) >=decimal.Parse(rowsAlloted[i]["MAX_QUANTITY"].ToString()))
    //        {
    //            availableQuantity = Convert.ToDecimal(newRow["MAX_QUANTITY"].ToString());
    //        }
    //        else
    //        {
    //            availableQuantity = Convert.ToDecimal(rowsAlloted[i]["AVAILABLE"]);
    //        }
    //        if (availableQuantity > 0)
    //        {
    //            if (availableQuantity >= quantity)
    //            {
    //                newRow["ALLOTQUANTITY"] = quantity;
    //                rowsAlloted[i]["AVAILABLE"] = availableQuantity - quantity;
    //                rowsAlloted[i]["PRODUCTCODE"] = rowToAllot["PRODUCTCODE"].ToString();
    //                rowsAlloted[i]["ALLOTQUANTITY"] = quantity +Convert.ToDecimal(rowsAlloted[i]["ALLOTQUANTITY"]);
    //                quantity = 0.00M;
    //            }
    //            else
    //            {
    //                newRow["ALLOTQUANTITY"] = availableQuantity;
    //                quantity = quantity - availableQuantity;
    //                rowsAlloted[i]["AVAILABLE"] = 0.00M;
    //                rowsAlloted[i]["ALLOTQUANTITY"] = Convert.ToDecimal(rowsAlloted[i]["ALLOTQUANTITY"]) + availableQuantity;
    //                rowsAlloted[i]["PRODUCTCODE"] = rowToAllot["PRODUCTCODE"].ToString();
    //            }
    //            tableAllotment.Rows.Add(newRow);
    //        }
    //    }
    //    return quantity;
    //}
    //#endregion

    //#region 条烟
    //protected decimal rowAllotedTiao(DataRow[] rowsAlloted, decimal quantity, DataRow rowToAllot, decimal availableQuantity)
    //{
    //    string PRODUCTCODE = rowToAllot["PRODUCTCODE"].ToString();
    //    string UNITCODE = objAllot.QueryProductCode(PRODUCTCODE).Tables[0].Rows[0][1].ToString();
    //    string UNITNAME = objAllot.QueryUnitName(UNITCODE).Tables[0].Rows[0][0].ToString();
    //    object MAX_QUANTITY = objAllot.QueryJian2(PRODUCTCODE).Tables[0].Rows[0][1].ToString();
    //    for (int i = 0; i < rowsAlloted.Length; i++)
    //    {
    //        if (quantity == 0)
    //        {
    //            break;
    //        }
    //        DataRow newRow = tableAllotment.NewRow();
    //        newRow["BILLNO"] = rowToAllot["BILLNO"].ToString();
    //        newRow["DETAILID"] = Convert.ToInt32(rowToAllot["ID"]);
    //        newRow["PRODUCTCODE"] = PRODUCTCODE;
    //        newRow["PRODUCTNAME"] = rowToAllot["PRODUCTNAME"].ToString();
    //        newRow["UNITCODE"] = UNITCODE;
    //        newRow["UNITNAME"] = UNITNAME;
    //        newRow["CELL_ID"] = rowsAlloted[i]["CELL_ID"];
    //        newRow["SHELFCODE"] = rowsAlloted[i]["SHELFCODE"];
    //        newRow["CELLCODE"] = rowsAlloted[i]["CELLCODE"];
    //        newRow["CELLNAME"] = rowsAlloted[i]["CELLNAME"];
    //        newRow["MAX_QUANTITY"] = rowsAlloted[i]["MAX_QUANTITY"];
    //        availableQuantity = Convert.ToDecimal(rowsAlloted[i]["AVAILABLE"]);
    //        if (availableQuantity > 0)
    //        {
    //            if (availableQuantity >= quantity)
    //            {
    //                newRow["ALLOTQUANTITY"] = quantity;
    //                rowsAlloted[i]["AVAILABLE"] = availableQuantity - quantity;
    //                rowsAlloted[i]["ALLOTQUANTITY"] = quantity + Convert.ToDecimal(rowsAlloted[i]["ALLOTQUANTITY"]);
    //                rowsAlloted[i]["PRODUCTCODE"] = rowToAllot["PRODUCTCODE"].ToString();
    //                quantity = 0.00M;
    //            }
    //            else
    //            {
    //                newRow["ALLOTQUANTITY"] = availableQuantity;
    //                quantity = quantity - availableQuantity;
    //                rowsAlloted[i]["AVAILABLE"] = 0.00M;
    //                rowsAlloted[i]["ALLOTQUANTITY"] = Convert.ToDecimal(rowsAlloted[i]["ALLOTQUANTITY"]) + availableQuantity;
    //                rowsAlloted[i]["PRODUCTCODE"] = rowToAllot["PRODUCTCODE"].ToString();
    //            }
    //            tableAllotment.Rows.Add(newRow);
    //        }
    //    }
    //    return quantity;
    //}
    //#endregion


    #endregion

    #region 分配结果图表(未使用)
    protected void ShowAllotedCell()
    {
        for (int i = 0; i < tableAllotment.Rows.Count; i++)
        {
            Panel p = new Panel();
            p.Attributes.Add("style", "float:left;");
            Table table = new Table();
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            tc.Text = tableAllotment.Rows[i]["CELLNAME"].ToString() + "<br/>" + tableAllotment.Rows[i]["PRODUCTNAME"].ToString() + "<br/>" + tableAllotment.Rows[i]["ALLOTQUANTITY"].ToString() + "件";
            tc.Attributes.Add("class", "cell");
            tr.Controls.Add(tc);
            table.Controls.Add(tr);
            p.Controls.Add(table);
            this.pnlAllotedCell.Controls.Add(p);
        }
    }    
    #endregion

    #region 保存分配结果
    protected void btnSaveAllotment_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgResult.Items.Count; i++)
        {
            TextBox textCode = (TextBox)dgResult.Items[i].Cells[6].Controls[0];
            tableAllotment.Rows[i]["CELLCODE"] = textCode.Text;
            TextBox textName = (TextBox)dgResult.Items[i].Cells[7].Controls[0];
            tableAllotment.Rows[i]["CELLNAME"] = textName.Text;
            TextBox textAllotQty = (TextBox)dgResult.Items[i].Cells[8].Controls[0];
            tableAllotment.Rows[i]["ALLOTQUANTITY"] = Convert.ToDecimal(textAllotQty.Text);
        }
        DataTable table = objAllot.GetEmptyAllotTable();
        foreach (DataRow row in tableAllotment.Rows)
        {
            DataRow newRow = table.NewRow();
            newRow["BILLNO"] = row["BILLNO"];
            newRow["PRODUCTCODE"] = row["PRODUCTCODE"];
            newRow["CELLCODE"] = row["CELLCODE"];
            newRow["QUANTITY"] = row["ALLOTQUANTITY"];
            newRow["UNITCODE"] = row["UNITCODE"].ToString();
            newRow["UNITNAME"] = row["UNITNAME"];
            newRow["INPUTQUANTITY"] = "0"; 
            newRow["STATUS"] = "0";
            table.Rows.Add(newRow);
            objAllot.UpdateCell(row["MAX_QUANTITY"].ToString(), row["UNITCODE"].ToString(), row["CELLCODE"].ToString());
        }
        try
        {
            objAllot.SaveAllotment(table);
            updateBll.InsertBull(table, "DWV_IWMS_IN_STORE_BILL", "DWV_IWMS_IN_STORE_BILL_DETAIL", "DWV_IWMS_IN_BUSI_BILL", Session["EmployeeCode"].ToString());
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "出错了！原因：" + exp.Message);
            return;
        }
       
        billMaster.UpdateAlloted(Session["BillNoList"].ToString(), Session["EmployeeCode"].ToString());
        updateBll.inUpdateAllot(Session["EmployeeCode"].ToString(), Session["BillNoList"].ToString());
        this.btnSaveAllotment.Enabled = false;
        this.btnAllot.Enabled = false;
        Session["BillNoList"] = "";
        JScript.Instance.ShowMessage(this, "分配结果保存成功");
        Response.Redirect("EntryAllotPage.aspx");
    }    
    #endregion

    #region Grid绑定
    protected void dgDetail_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["grid_OddRowColor"] != null && Session["grid_EvenRowColor"] != null)
            {
                if (e.Item.ItemIndex % 2 == 0)
                {
                    e.Item.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Item.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
                }
            }
        }
    }
    protected void dgResult_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        //已经分配的货位排除掉
        string CELLCODE = "''";
        for (int i = 0; i < tableAllotment.Rows.Count; i++)
        {
            CELLCODE += ",'" + tableAllotment.Rows[i]["CELLCODE"] + "'";
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["grid_OddRowColor"] != null && Session["grid_EvenRowColor"] != null)
            {
                if (e.Item.ItemIndex % 2 == 0)
                {
                    e.Item.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Item.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
                }
            }
            TextBox tb1 = new TextBox();
            tb1.ID = "cellcode";//货位编号
            tb1.Text = e.Item.Cells[6].Text.Replace("&nbsp;", "");
            //tb1.Attributes.Add("style", "WIDTH:110PX;");
            tb1.Width = 110;
            tb1.Attributes.Add("class", "GridInputAlignLeft");
            tb1.Attributes.Add("onfocus", "CannotEdit(this)");
            e.Item.Cells[6].Controls.Add(tb1);
            Button btn1 = new Button();
            btn1.CssClass = "ButtonBrowse2";
            //没有分配烟的货位
            // btn1.OnClientClick = string.Format("SelectDialog3('{0},{1}','WMS_WH_CELL','CELLCODE,CELLNAME','','','(MAX_QUANTITY-QUANTITY>0) AND ASSIGNEDPRODUCT="+e.Item.Cells[2].Text+")')", tb1.ClientID, tb1.ClientID.Replace("code", "name"));
            btn1.OnClientClick = string.Format("SelectDialog3('{0},{1}','WMS_WH_CELL','CELLCODE,CELLNAME','','','QUANTITY=0 AND FROZEN_IN_QTY=0 AND ISACTIVE=1 AND CELLCODE NOT IN ({2}) ')"
                                                , tb1.ClientID
                                                , tb1.ClientID.Replace("code", "name")
                                                , CELLCODE.Replace("'"[0], '"'));
            e.Item.Cells[6].Controls.Add(btn1);

            TextBox tb2 = new TextBox();
            tb2.ID = "cellname";//货位名称
            tb2.Width = 100;
            tb2.Text = e.Item.Cells[7].Text.Replace("&nbsp;", "");
            tb2.Attributes.Add("class", "GridInputAlignLeft");
            tb2.Attributes.Add("onfocus", "CannotEdit(this)");
            e.Item.Cells[7].Controls.Add(tb2);


            TextBox tb3 = new TextBox();
            tb3.ID = "quantity";//分配数量
            tb3.Text = e.Item.Cells[8].Text;
            tb3.Attributes.Add("style", "width:60px;");
            tb3.Attributes.Add("class", "GridInputAlignRight");
            tb3.Attributes.Add("onfocus", "CannotEdit(this)");
            e.Item.Cells[8].Controls.Add(tb3);
        }
    } 
    #endregion

    #region Grid列合并
    protected void GridRowSpan(DataGrid grid, int colIndex)
    {
        if (grid.Items.Count >= 2)
        {
            int start = 0;
            int end = 0;
            for (int i = 1; i < grid.Items.Count; i++)
            {
                if (grid.Items[i].Cells[colIndex].Text == grid.Items[i - 1].Cells[colIndex].Text)
                {
                    end = i;
                    if (i < grid.Items.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        int span = end - start + 1;
                        grid.Items[start].Cells[colIndex].RowSpan = span;
                        for (int k = start + 1; k <= end; k++)
                        {
                            grid.Items[k].Cells[colIndex].Visible = false;
                        }
                    }
                }
                else
                {
                    int span = end - start + 1;
                    grid.Items[start].Cells[colIndex].RowSpan = span;
                    for (int k = start + 1; k <= end; k++)
                    {
                        grid.Items[k].Cells[colIndex].Visible = false;
                    }

                    start = end + 1;
                    end++;
                }
            }
        }
    }
    #endregion

    protected void btnSwitchDetail_Click(object sender, EventArgs e)
    {
        if (this.pnlDetail.Visible == true)
        {
            this.pnlDetail.Visible = false;
            this.btnSwitchDetail.CssClass = "switch_down";
        }
        else
        {
            this.pnlDetail.Visible = true;
            this.btnSwitchDetail.CssClass = "switch_up";
        }
    }

    
    protected void dgResult_PreRender(object sender, EventArgs e)
    {
        if (tableAllotment.Rows.Count > 0)
        {
            for (int i = 0; i < dgResult.Items.Count; i++)
            {
                TextBox textCode = (TextBox)dgResult.Items[i].Cells[6].Controls[0];
                tableAllotment.Rows[i]["CELLCODE"] = textCode.Text;
                TextBox textName = (TextBox)dgResult.Items[i].Cells[7].Controls[0];
                tableAllotment.Rows[i]["CELLNAME"] = textName.Text;
                TextBox textAllotQty = (TextBox)dgResult.Items[i].Cells[8].Controls[0];
                tableAllotment.Rows[i]["ALLOTQUANTITY"] = Convert.ToDecimal(textAllotQty.Text);
            }
        }
        Session["tableAllotment"] = tableAllotment;
        this.dgResult.DataSource = tableAllotment;
        this.dgResult.DataBind();
        GridRowSpan(dgResult, 1);
    }
}
