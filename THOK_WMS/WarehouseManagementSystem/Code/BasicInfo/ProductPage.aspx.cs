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
using org.in2bits.MyXls;
using System.IO;
using System.Threading;
using THOK.WMS;
using THOK.WMS.Download.Bll;

public partial class Code_BasicInfo_ProductPage : BasePage
{
    int pageIndex = 1;
    int pageSize = 15;
    int totalCount = 0;
    int pageCount = 0;
    string filter = "1=1";
    DataSet dsProduct;
    string PrimaryKey = "PRODUCTCODE";
    string OrderByFields = "PRODUCTCODE";
    Product objProduct = new Product();

    #region 窗体加载
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["sys_PageCount"] != null)
            {
                pageSize = Convert.ToInt32(Session["sys_PageCount"].ToString());
                pager.PageSize = pageSize;
            }
            if (Session["pager_ShowPageIndex"] != null)
            {
                pager.ShowPageIndex = Convert.ToBoolean(Session["pager_ShowPageIndex"].ToString());
            }

            if (!IsPostBack)
            {
                totalCount = objProduct.GetRowCount(filter);
                pager.RecordCount = totalCount;
                GridDataBind();
            }
            else
            {
                pageCount = Convert.ToInt32(ViewState["pageCount"]);
                pageIndex = Convert.ToInt32(ViewState["pageIndex"]);
                totalCount = Convert.ToInt32(ViewState["totalCount"]);
                filter = ViewState["filter"].ToString();
                OrderByFields = ViewState["OrderByFields"].ToString();

                totalCount = objProduct.GetRowCount(filter);
                GridDataBind();
            }

        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    #endregion

    #region 数据源绑定
    void GridDataBind()
    {
        dsProduct = objProduct.QueryProduct(pageIndex, pageSize, filter, OrderByFields);
        if (dsProduct.Tables[0].Rows.Count == 0)
        {
            dsProduct.Tables[0].Rows.Add(dsProduct.Tables[0].NewRow());
            gvMain.DataSource = dsProduct;
            gvMain.DataBind();
            int columnCount = gvMain.Rows[0].Cells.Count;
            gvMain.Rows[0].Cells.Clear();
            gvMain.Rows[0].Cells.Add(new TableCell());
            gvMain.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvMain.Rows[0].Cells[0].Text = "没有符合以上条件的数据,请重新查询 ";
            gvMain.Rows[0].Visible = true;

        }
        else
        {
            this.gvMain.DataSource = dsProduct.Tables[0];
            this.gvMain.DataBind();
        }

        ViewState["pageIndex"] = pageIndex;
        ViewState["totalCount"] = totalCount;
        ViewState["pageCount"] = pageCount;
        ViewState["filter"] = filter;
        ViewState["OrderByFields"] = OrderByFields;
    }
    #endregion

    #region 显示切换
    private void SwitchView(int index)
    {
        if (index == 0)
        {
            this.pnlList.Visible = true;
            this.pnlEdit.Visible = false;
        }
        else
        {
            this.pnlList.Visible = false;
            this.pnlEdit.Visible = true;
        }
    }
    #endregion

    #region GridView绑定
    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = new CheckBox();
            chk.Attributes.Add("style", " font-weight:bold; text-align:center;word-break:keep-all; white-space:nowrap");
            chk.ID = "checkAll";
            chk.Attributes.Add("onclick", "checkboxChange(this,'gvMain',0);");
            chk.Text = "操作";
            e.Row.Cells[0].Controls.Add(chk);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex % 2 == 0)
            {
                e.Row.BackColor = System.Drawing.Color.FromName(Session["grid_OddRowColor"].ToString());
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.FromName(Session["grid_EvenRowColor"].ToString());
            }
            e.Row.Cells[0].Attributes.Add("style", "word-break:keep-all; white-space:nowrap");

            CheckBox chk = new CheckBox();
            LinkButton lkbtn = new LinkButton();
            lkbtn.CommandName = "Edit";
            lkbtn.ID = e.Row.ID;
            lkbtn.Text = " 编辑 ";
            if (this.hdnXGQX.Value == "0")
            {
                lkbtn.Enabled = false;
            }
            e.Row.Cells[0].Controls.Add(chk);
            e.Row.Cells[0].Controls.Add(lkbtn);

            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f5f5f5',this.style.fontWeight=''; this.style.cursor='hand';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
        }
    }
    #endregion

    
    #region 数据编辑
    protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hdnOpFlag.Value = "1";
        ViewState["OpFlag"] = "1";
        SwitchView(1);
        this.txtPRODUCTCODE.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["PRODUCTCODE"].ToString();
        this.txtPRODUCTCLASS.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["PRODUCTCLASS"].ToString();
        this.txtCLASSNAME.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["CLASSNAME"].ToString();
        this.txtPRODUCTNAME.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["PRODUCTNAME"].ToString();
        this.txtSHORTNAME.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["SHORTNAME"].ToString();
        this.txtSUPPLIERCODE.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["SUPPLIERCODE"].ToString();
        this.txtBARCODE.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["BARCODE"].ToString();
        this.txtABCODE.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["ABCODE"].ToString();
        this.txtUNITCODE.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["UNITCODE"].ToString();
        this.txtUNITNAME.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["UNITNAME"].ToString();
        //this.txtJIANTIAORATE.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["JIANTIAORATE"].ToString();//
        //this.txtTIAOBAORATE.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["TIAOBAORATE"].ToString();//
        //this.txtBAOZHIRATE.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["BAOZHIRATE"].ToString();//
        this.txtJIANCODE.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["JIANCODE"].ToString();
        this.txtTIAOCODE.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["TIAOCODE"].ToString();
        this.txtMAXCELLPIECE.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["MAXCELLPIECE"].ToString();
        this.txtMEMO.Text = dsProduct.Tables[0].Rows[e.NewEditIndex]["MEMO"].ToString();
        this.txtPRODUCTCODE.Attributes.Add("onblur", "");
        this.txtPRODUCTCODE.ReadOnly = true;
    }
    #endregion

    #region 按字段查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            filter = string.Format("{0} like '{1}%'", this.ddl_Field.SelectedValue, this.txtKeyWords.Text.Trim().Replace("'", ""));
            ViewState["filter"] = filter;
            if (rbASC.Checked)
            {
                OrderByFields = this.ddl_Field.SelectedValue + " asc ";
            }
            else
            {
                OrderByFields = this.ddl_Field.SelectedValue + " desc ";
            }

            totalCount = objProduct.GetRowCount(filter);
            pageIndex = 1;
            pager.CurrentPageIndex = 1;
            pager.RecordCount = totalCount;
            GridDataBind();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    #endregion

    #region 新增
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        ClearData();
        this.hdnOpFlag.Value = "0";
        ViewState["OpFlag"] = "0";
        SwitchView(1);
        this.txtPRODUCTCODE.Attributes.Add("onblur", string.Format("return UniqueValidate('WMS_PRODUCT','PRODUCTCODE',this.value,'1=1')"));
        this.txtPRODUCTCODE.ReadOnly = false;
    }
    #endregion

    #region 删除
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvMain.Rows.Count; i++)
            {
                if (gvMain.Rows[0].Cells[0].Text == "没有符合以上条件的数据,请重新查询 ")
                {
                    break;
                }
                else
                {
                    CheckBox chk = (CheckBox)gvMain.Rows[i].Cells[0].Controls[0];
                    if (chk.Enabled && chk.Checked)
                    {
                        dsProduct.Tables[0].Rows[i].Delete();
                    }
                }
            }
            objProduct.Delete(dsProduct);
            totalCount = objProduct.GetRowCount(filter);
            pager.RecordCount = totalCount;
            if (pageIndex > pager.PageCount)
            {
                pageIndex = pager.PageCount;
            }
            GridDataBind();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    #endregion

    # region 分页控件 页码changing事件
    protected void pager_PageChanging(object src, PageChangingEventArgs e)
    {
        pager.CurrentPageIndex = e.NewPageIndex;
        pager.RecordCount = totalCount;
        pageIndex = pager.CurrentPageIndex;
        ViewState["pageIndex"] = pageIndex;
        GridDataBind();
    }
    #endregion

    #region 数据保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["OpFlag"].ToString() == "0")//新增
            {
                objProduct.PRODUCTCODE = this.txtPRODUCTCODE.Text.Trim().Replace("\'", "\''");
                objProduct.PRODUCTCLASS = this.txtPRODUCTCLASS.Text.Trim().Replace("\'", "\''");
                objProduct.PRODUCTNAME = this.txtPRODUCTNAME.Text.Trim().Replace("\'", "\''");
                objProduct.SHORTNAME = this.txtSHORTNAME.Text.Trim().Replace("\'", "\''");
                objProduct.SUPPLIERCODE = this.txtSUPPLIERCODE.Text.Trim().Replace("\'", "\''");
                objProduct.BARCODE = this.txtBARCODE.Text.Trim().Replace("\'", "\''");
                objProduct.ABCODE = this.txtABCODE.Text.Trim().Replace("\'", "\''");
                objProduct.UNITCODE = this.txtUNITCODE.Text.Trim().Replace("\'", "\''");
                //objProduct.JIANTIAORATE = Convert.ToDouble(this.txtJIANTIAORATE.Text);//
                //objProduct.TIAOBAORATE = Convert.ToDouble(this.txtTIAOBAORATE.Text);//
                //objProduct.BAOZHIRATE = Convert.ToDouble(this.txtBAOZHIRATE.Text);//
                objProduct.MEMO = this.txtMEMO.Text.Trim().Replace("\'", "\''");
                objProduct.JIANCODE = this.txtJIANCODE.Text.Trim().Replace("\'","\''");
                objProduct.TIAOCODE = this.txtTIAOCODE.Text.Trim().Replace("\'", "\''");
                objProduct.MAXCELLPIECE = Convert.ToDouble(this.txtMAXCELLPIECE.Text);

                

                objProduct.Insert();
                //objProduct.InsertBrand();//插入中烟接口卷烟信息表（DWV_IINF_BRAND）

                totalCount = objProduct.GetRowCount(filter);
                pager.RecordCount = totalCount;
                GridDataBind();
                SwitchView(0);
                JScript.Instance.ShowMessage(this.UpdatePanel1, "产品添加成功！");
            }
            else//修改
            {
                objProduct.PRODUCTCODE = this.txtPRODUCTCODE.Text.Trim().Replace("\'", "\''");
                objProduct.PRODUCTCLASS = this.txtPRODUCTCLASS.Text.Trim().Replace("\'", "\''");
                objProduct.PRODUCTNAME = this.txtPRODUCTNAME.Text.Trim().Replace("\'", "\''");
                objProduct.SHORTNAME = this.txtSHORTNAME.Text.Trim().Replace("\'", "\''");
                objProduct.SUPPLIERCODE = this.txtSUPPLIERCODE.Text.Trim().Replace("\'", "\''");
                objProduct.BARCODE = this.txtBARCODE.Text.Trim().Replace("\'", "\''");
                objProduct.ABCODE = this.txtABCODE.Text.Trim().Replace("\'", "\''");
                objProduct.UNITCODE = this.txtUNITCODE.Text.Trim().Replace("\'", "\''");
                //objProduct.JIANTIAORATE = Convert.ToDouble(this.txtJIANTIAORATE.Text);//
                //objProduct.TIAOBAORATE = Convert.ToDouble(this.txtTIAOBAORATE.Text);//
                //objProduct.BAOZHIRATE = Convert.ToDouble(this.txtBAOZHIRATE.Text);//
                objProduct.MEMO = this.txtMEMO.Text.Trim().Replace("\'", "\''");
                objProduct.JIANCODE = this.txtJIANCODE.Text.Trim().Replace("\'", "\''");
                objProduct.TIAOCODE = this.txtTIAOCODE.Text.Trim().Replace("\'", "\''");
                objProduct.MAXCELLPIECE = Convert.ToDouble(this.txtMAXCELLPIECE.Text);
                objProduct.Update();

                
                GridDataBind();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "产品修改成功！");
                SwitchView(0);
            }
        }
        catch (Exception exp)
        {
            if(exp.Message.Contains("违反了 PRIMARY KEY 约束"))
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "产品代码重复，请重新编码！");
            }
            else
            {
               JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
            }
        }
    }
    #endregion

    #region 取消
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearData();
        SwitchView(0);
    }

    protected void ClearData()
    {
        this.txtPRODUCTCODE.Text = "";
        this.txtPRODUCTCLASS.Text = "";
        this.txtCLASSNAME.Text = "";
        this.txtPRODUCTNAME.Text = "";
        this.txtSHORTNAME.Text = "";
        this.txtSUPPLIERCODE.Text = "";
        this.txtBARCODE.Text = "";
        this.txtABCODE.Text = "";
        this.txtUNITCODE.Text = "";
        this.txtUNITNAME.Text = "";
        this.txtJIANCODE.Text = "0";
        this.txtTIAOCODE.Text = "0";
        this.txtMAXCELLPIECE.Text = "0";
        this.txtMEMO.Text = "";
    }
    #endregion

    #region 退出
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../MainPage.aspx");
    }
    #endregion

    #region 下载产品信息
    
    
    protected void btnDown_Click(object sender, EventArgs e)
    {
        DownProductBll prdll = new DownProductBll();
        try
        {
            bool tag = prdll.DownProductInfo();
            if (!tag)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "没有更多产品信息数据下载！");
            }
            else
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "下载完成！");
            }
        }
        catch (Exception exp )
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }

    #endregion

    #region 导出Excel

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DownProductBll bll = new DownProductBll();
            DataTable dt = bll.ProductInfo();
            if (dt.Rows.Count == 0)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "没有数据导出");
            }
            else
            {
                string fileName = this.Export(dt);
                string path = Request.PhysicalApplicationPath + "Excel\\" + fileName + ".xls";
                bool flag = Excel.ResponseFile(Page.Request, Page.Response, "\\Excel\\" + fileName + ".xls", path, 1024000);//ResponseFile(Page.Request, Page.Response, "\\Excel\\" + fileName + ".xls", path, 1024000);
                FileInfo file = new FileInfo(path);
                file.Delete();
                if (flag)
                {
                    JScript.Instance.ShowMessage(this.UpdatePanel1, "导出成功！");
                }
            }
        }
        
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }

    public string Export(DataTable dt)
    {
        XlsDocument xls = new XlsDocument();
        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss");
        xls.FileName = fileName;
        int rowIndex = 1;
        Worksheet sheet = xls.Workbook.Worksheets.Add("测试表");
        Cells cells = sheet.Cells;
        sheet.Cells.Merge(1, 1, 1, 2);
        Cell cell = cells.Add(1, 1, "产品信息");
        cell.Font.Bold = true;
        cell = cells.Add(2, 1, "产品代码");
        cell.Font.Bold = true;
        cell = cells.Add(2, 2, "产品名称");
        cell.Font.Bold = true;
        foreach (DataRow row in dt.Rows)
        {
            cells.Add(rowIndex + 2, 1, "" + row["PRODUCTCODE"] + "");
            cells.Add(rowIndex + 2, 2, "" + row["PRODUCTNAME"] + "");

            rowIndex++;
        }

        cell.HorizontalAlignment = HorizontalAlignments.Centered;
        string file = System.Web.HttpContext.Current.Server.MapPath("~/Excel/");
        xls.Save(file);
        //xls.Send();
        return fileName;
       
    }


    #endregion
}
