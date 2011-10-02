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
using System.Drawing;
using System.Collections.Generic;
using THOK.WMS.Allot.Bll;
using THOK.WMS.Allot;
using THOK.WMS.Upload.Bll;
public partial class Code_StockDelivery_DeliveryAutoAllotPage : System.Web.UI.Page
{
    DeliveryBillMaster billMaster = new DeliveryBillMaster();
    DeliveryBillDetail billDetail = new DeliveryBillDetail();
    MoveBillMaster billMoveMaster = new MoveBillMaster();
    MoveBillDetail billMoveDetail = new MoveBillDetail();
    UpdateUploadBll updateBll = new UpdateUploadBll();
    DataSet dsDetail;
    DeliveryAllot objAllot = new DeliveryAllot();
    THOK.WMS.BLL.EntryAllot objAllotE = new THOK.WMS.BLL.EntryAllot();
    DataTable tableAllotment; //分配结果
    DataTable tableMaster;//拼盘主表
    DataTable tableDetail;//拼盘明细表
    DataTable tableMoveDetail;//移库单信息表
    DataTable tableMoveMaster;//移库单主表
    WarehouseCell warecell = new WarehouseCell();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        warecell.UpdateCellEx();
        warecell.UpdateCell();
        this.lbError.Items.Clear();
        lbError.Items.Add("错误指令:");
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
            if (Session["BillNoList"] == null || Session["BillNoList"].ToString() == "")
            {
                Response.Redirect("DeliveryAllotPage.aspx");
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
                    this.dgResult.DataSource = tableAllotment;
                    this.dgResult.DataBind();
                }
            }
        }
    }
     
    #region 执行分配
    protected void btnAllot_Click(object sender, EventArgs e)
    {
        try
        {
            objAllot.UpdateBhDate();//在出库自动分配的时候修改分拣区的时间
            DeliveryOrderAllot cAllot = new DeliveryOrderAllot(dsDetail);
            cAllot.Allot();
            tableAllotment = cAllot.tableAllotment;
            tableMoveDetail = cAllot.tableMoveDetail;

            foreach (string row in cAllot.errorlist)
            {
                this.lbError.Items.Add(row);
            }

            if (this.lbError.Items.Count == 1)
            {
                this.dgResult.DataSource = tableAllotment;
                this.dgResult.DataBind();
                if (tableMoveDetail.Rows.Count > 0)
                {
                    this.dgMoveDetail.DataSource = tableMoveDetail;
                    this.dgMoveDetail.DataBind();
                }
                else
                {
                    dgMoveDetail.Visible = false;
                }

                GridRowSpan(dgResult, 1);
                GridRowSpan(dgMoveDetail, 1);
                this.pnlResult.Visible = true;
                this.pnlMoveDetail.Visible = true;
                this.btnSaveAllotment.Enabled = true;
                Session["tableAllotment"] = tableAllotment;
                Merge(tableAllotment, tableMoveDetail);
            }
            if (this.lbError.Items.Count != 1)
            {
                this.lbError.Visible = true;
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, exp.Message);
        }
    }
    #endregion


    #region 保存分配结果
    protected void btnSaveAllotment_Click(object sender, EventArgs e)
    {
        tableMaster = (DataTable)Session["tableMaster"];
        tableDetail = (DataTable)Session["tableDetail"];
        tableMoveDetail = (DataTable)Session["MoveDetail"];
        tableMoveMaster = (DataTable)Session["MoveMaster"];
        if (tableMaster.Rows.Count==0)
        {
            JScript.Instance.ShowMessage(this, "不存在出库单");
        }
        else
        {
            objAllot.SaveAllotment(tableMaster, tableDetail, tableMoveDetail, tableMoveMaster);
            updateBll.InsertBull(tableDetail, "DWV_IWMS_OUT_STORE_BILL", "DWV_IWMS_OUT_STORE_BILL_DETAIL", "DWV_IWMS_OUT_BUSI_BILL", Session["EmployeeCode"].ToString());
            billMaster.UpdateAlloted(Session["BillNoList"].ToString(), Session["EmployeeCode"].ToString());
            updateBll.outUpdateAudot(Session["EmployeeCode"].ToString(), Session["BillNoList"].ToString());
            Session["BillNoList"] = null;
            JScript.Instance.ShowMessage(this, "出库分配保存成功");
            Response.Redirect("DeliveryAllotPage.aspx");
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

    #region 明细绑定
    protected void dgDetail_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.ItemIndex % 2 == 0)
            {
                e.Item.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
            }
            else
            {
                e.Item.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
            }
            e.Item.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f5f5f5',this.style.fontWeight=''; this.style.cursor='hand';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
        }
    }
    protected void dgResult_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.ItemIndex % 2 == 0)
            {
                e.Item.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
            }
            else
            {
                e.Item.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
            }
            e.Item.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f5f5f5',this.style.fontWeight=''; this.style.cursor='hand';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
        }
    }
    #endregion

    #region 自动生成的移位单明细
    protected void dgMoveDetail_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex % 2 == 0)
                {
                    e.Item.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
                }
                else
                {
                    e.Item.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
                }
                e.Item.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f5f5f5',this.style.fontWeight=''; this.style.cursor='hand';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this, "数据绑定出错：" + exp.Message);
        }
    }
    #endregion

    #region 分配结果拼盘
    protected void Merge(DataTable tableAllotment, DataTable tableMoveDetail)
    {
        DataTable tableMaster = objAllot.GetEmptyAllotMasterTable();
        DataTable tableDetail = objAllot.GetEmptyAllotDetailTable();
        DataTable MoveMaster = objAllot.GetEmptyMoveMasterTable();
        DataTable MoveDetail = objAllot.GetEmptyMoveDetailTable();
        string[] aryBillNo = Session["BillNoList"].ToString().Split(',');
        DataRow newMasterRow = tableMaster.NewRow();
        DataRow newDetailRow = tableDetail.NewRow();
        DataRow newMoveMasterRow = MoveMaster.NewRow();
        DataRow newMoveDetailRow = MoveDetail.NewRow();
        for (int i = 0; i < aryBillNo.Length; i++)//逐单拼盘
        {
            int taskid = 1;
            int master = 1;
            DataRow[] rows = tableAllotment.Select("BILLNO='" + aryBillNo[i] + "'", "QUANTITY DESC");
            DataRow[] rows0 = tableMoveDetail.Select("BILLNO='" + aryBillNo[i] + "M" + "'", "QUANTITY DESC");
            foreach (DataRow rAllot0 in rows0)
            {
                decimal detailQuantity = Convert.ToDecimal(rAllot0["QUANTITY"]);
                if (master <= 1)//主表只插入一次
                {
                    newMoveMasterRow = MoveMaster.NewRow();
                    newMoveMasterRow["BILLNO"] = rAllot0["BILLNO"];
                    newMoveMasterRow["BILLDATE"] = DateTime.Now;
                    newMoveMasterRow["STATUS"] = "1";
                    newMoveMasterRow["BILLNO"] = rAllot0["BILLNO"];
                    newMoveMasterRow["BILLTYPE"] = "301";
                    newMoveMasterRow["WH_CODE"] = "001";
                    newMoveMasterRow["OPERATEPERSON"] = Session["EmployeeCode"].ToString();
                    newMoveMasterRow["VALIDATEPERSON"] = Session["EmployeeCode"].ToString();
                    newMoveMasterRow["VALIDATEDATE"] = DateTime.Now;
                    MoveMaster.Rows.Add(newMoveMasterRow);
                    master += 1;
                }
                newMoveDetailRow = MoveDetail.NewRow();
                newMoveDetailRow["BILLNO"] = rAllot0["BILLNO"];
                newMoveDetailRow["PRODUCTCODE"] = rAllot0["PRODUCTCODE"];
                newMoveDetailRow["OUT_CELLCODE"] = rAllot0["OUT_CELLCODE"];
                newMoveDetailRow["IN_CELLCODE"] = rAllot0["IN_CELLCODE"];
                newMoveDetailRow["QUANTITY"] = rAllot0["QUANTITY"];
                newMoveDetailRow["UNITCODE"] = rAllot0["UNITCODE"];
                newMoveDetailRow["STATUS"] = "0";
                MoveDetail.Rows.Add(newMoveDetailRow);
                continue;
            }
            foreach (DataRow rAllot in rows)
            {
                bool isMerged = false;
                decimal detailQuantity = Convert.ToDecimal(rAllot["QUANTITY"]);
                if (taskid == 1)//出库单开始合并
                {
                    newMasterRow = tableMaster.NewRow();
                    newMasterRow["TASKID"] = taskid.ToString();
                    newMasterRow["BILLNO"] = rAllot["BILLNO"];
                    newMasterRow["TOTALQUANTITY"] = rAllot["QUANTITY"];
                    newMasterRow["ISMERGED"] = "0";
                    newMasterRow["STATUS"] = "0";
                    tableMaster.Rows.Add(newMasterRow);


                    newDetailRow = tableDetail.NewRow();
                    newDetailRow["TASKID"] = taskid.ToString();
                    newDetailRow["BILLNO"] = rAllot["BILLNO"];
                    newDetailRow["DETAILID"] = rAllot["DETAILID"];
                    newDetailRow["PRODUCTCODE"] = rAllot["PRODUCTCODE"];
                    newDetailRow["CELLCODE"] = rAllot["CELLCODE"];
                    newDetailRow["QUANTITY"] = rAllot["QUANTITY"];
                    newDetailRow["OUTPUTQUANTITY"] = rAllot["QUANTITY"];
                    newDetailRow["PALLETID"] = rAllot["PALLETID"];
                    newDetailRow["NEWPALLETID"] = rAllot["PALLETID"];
                    newDetailRow["STATUS"] = "0";
                    tableDetail.Rows.Add(newDetailRow);
                    taskid++;
                    continue;
                }

                if (detailQuantity < 0.00M)//条烟，拆件烟，与上个托盘合并
                {
                    // tableMaster.Rows[tableMaster.Rows.Count - 1]["ISMERGED"] = "1";
                    newDetailRow = tableDetail.NewRow();
                    newDetailRow["TASKID"] = tableDetail.Rows[tableDetail.Rows.Count - 1]["TASKID"];
                    newDetailRow["BILLNO"] = rAllot["BILLNO"];
                    newDetailRow["DETAILID"] = rAllot["DETAILID"];
                    newDetailRow["PRODUCTCODE"] = rAllot["PRODUCTCODE"];
                    newDetailRow["CELLCODE"] = rAllot["CELLCODE"];
                    newDetailRow["QUANTITY"] = rAllot["QUANTITY"];
                    newDetailRow["OUTPUTQUANTITY"] = rAllot["QUANTITY"];
                    newDetailRow["PALLETID"] = rAllot["PALLETID"];
                    newDetailRow["NEWPALLETID"] = tableDetail.Rows[tableDetail.Rows.Count - 1]["NEWPALLETID"];
                    newDetailRow["STATUS"] = "0";
                    tableDetail.Rows.Add(newDetailRow);
                    continue;
                }
                else
                {
                    isMerged = false;

                    foreach (DataRow rMaster in tableMaster.Rows)
                    {
                        if (rMaster["BILLNO"].ToString() != rAllot["BILLNO"].ToString())//不同单，不合并
                        {
                            continue;
                        }
                        decimal masterQuantity = Convert.ToDecimal(rMaster["TOTALQUANTITY"]);
                        if (masterQuantity + detailQuantity >= 30.00M)// 超过一托盘的数量
                        {
                            continue;
                        }
                        else
                        {
                            rMaster["TOTALQUANTITY"] = Convert.ToDecimal(rMaster["TOTALQUANTITY"]) + detailQuantity;
                            rMaster["ISMERGED"] = "1";
                            newDetailRow = tableDetail.NewRow();
                            newDetailRow["TASKID"] = rMaster["TASKID"];
                            newDetailRow["BILLNO"] = rAllot["BILLNO"];
                            newDetailRow["DETAILID"] = rAllot["DETAILID"];
                            newDetailRow["PRODUCTCODE"] = rAllot["PRODUCTCODE"];
                            newDetailRow["CELLCODE"] = rAllot["CELLCODE"];
                            newDetailRow["QUANTITY"] = rAllot["QUANTITY"];
                            newDetailRow["OUTPUTQUANTITY"] = rAllot["QUANTITY"];
                            newDetailRow["PALLETID"] = rAllot["PALLETID"];
                            newDetailRow["NEWPALLETID"] = tableDetail.Rows[tableDetail.Rows.Count - 1]["NEWPALLETID"];
                            newDetailRow["STATUS"] = "0";
                            tableDetail.Rows.Add(newDetailRow);
                            isMerged = true;
                            break;
                        }
                    }
                    if (!isMerged)//与已拼盘的明细不能再拼盘，则新建托盘
                    {

                        newMasterRow = tableMaster.NewRow();
                        newMasterRow["TASKID"] = taskid.ToString();
                        newMasterRow["BILLNO"] = rAllot["BILLNO"];
                        newMasterRow["TOTALQUANTITY"] = rAllot["QUANTITY"];
                        newMasterRow["ISMERGED"] = "0";
                        newMasterRow["STATUS"] = "0";
                        tableMaster.Rows.Add(newMasterRow);


                        newDetailRow = tableDetail.NewRow();
                        newDetailRow["TASKID"] = taskid.ToString();
                        newDetailRow["BILLNO"] = rAllot["BILLNO"];
                        newDetailRow["DETAILID"] = rAllot["DETAILID"];
                        newDetailRow["PRODUCTCODE"] = rAllot["PRODUCTCODE"];
                        newDetailRow["CELLCODE"] = rAllot["CELLCODE"];
                        newDetailRow["QUANTITY"] = rAllot["QUANTITY"];
                        newDetailRow["OUTPUTQUANTITY"] = rAllot["QUANTITY"];
                        newDetailRow["PALLETID"] = rAllot["PALLETID"];
                        newDetailRow["NEWPALLETID"] = rAllot["PALLETID"];
                        newDetailRow["STATUS"] = "0";
                        tableDetail.Rows.Add(newDetailRow);
                        taskid++;
                        continue;
                    }
                }
            }
        }
        Session["tableMaster"] = tableMaster;
        Session["tableDetail"] = tableDetail;
        Session["MoveDetail"] = MoveDetail;
        Session["MoveMaster"] = MoveMaster;
    }
    #endregion

}
