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
using THOK.WMS.BLL;

public partial class Code_StockDelivery_DeliveryAllotPage : BasePage
{
    public string div01display = "block";
    public string div02display = "none";
    DeliveryBillMaster billMaster = new DeliveryBillMaster();
    DeliveryBillDetail billDetail = new DeliveryBillDetail();
    DataSet dsMaster;
    DataSet dsDetail;
    int pageIndex = 1;
    int pageSize = 150;
    string filter = "STATUS='2'";
    string PrimaryKey = "ID";
    string OrderByFields = "BillNo desc";
    string BillNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Session["G_user"].ToString();
        if (Application["MNU_M00E_00D"] == null || Application["MNU_M00E_00D"].ToString() == userName || Application["MNU_M00E_00D"].ToString() == "")
        {
            if (!IsPostBack)
            {
                BillType objType = new BillType();
                DataSet dsTemp = objType.QueryBillType(1, 100, "BUSINESS='2'", "TYPECODE");
                DataRow newRow = dsTemp.Tables[0].NewRow();
                newRow["TYPECODE"] = "2";
                newRow["TYPENAME"] = "所有出库单";
                dsTemp.Tables[0].Rows.InsertAt(newRow, 0);
                this.ddlBillType.DataSource = dsTemp.Tables[0].DefaultView;
                this.ddlBillType.DataTextField = "TYPENAME";
                this.ddlBillType.DataValueField = "TYPECODE";
                this.ddlBillType.DataBind();
                BindMaster();
                BindDetail();
                ViewState["div01display"] = div01display;
                ViewState["div02display"] = div02display;
            }
            else
            {
                if (ViewState["BILLNO"] != null)
                {
                    BindMaster();
                    BillNo = ViewState["BILLNO"].ToString();
                    BindDetail();
                    div01display = ViewState["div01display"].ToString();
                    div02display = ViewState["div02display"].ToString();
                }
            }
            Application["MNU_M00E_00D"] = Session["G_user"];
        }
        else
        {
            Exception exp = new Exception();
            Session["ModuleName"] = "出库分配";
            Session["FunctionName"] = "Page_Load";
            Session["ExceptionalType"] = exp.GetType().FullName;
            Session["ExceptionalDescription"] = "该页面已为" + Application["MNU_M00E_00D"].ToString() + "用户占用，请稍候...";
            Response.Redirect("../../Common/MistakesPage.aspx");
            //Response.Redirect("../../MainPage.aspx");
        }
    }

    protected void BindMaster()
    {
        dsMaster = billMaster.QueryDeliveryBillMaster(pageIndex, pageSize, filter, OrderByFields);
        this.dgMaster.DataSource = dsMaster.Tables[0];
        this.dgMaster.DataBind();
        if (dsMaster.Tables[0].Rows.Count > 0)
        {
            BillNo = dsMaster.Tables[0].Rows[0]["BILLNO"].ToString();
            ViewState["BILLNO"] = BillNo;
            this.btnNext.Enabled = true;
            this.lblMsg.Visible = false;
        }
        else
        {
            this.btnNext.Enabled = false;
            this.lblMsg.Visible = true;
        }
    }

    protected void BindDetail()
    {
        dsDetail = billDetail.QueryByBillNo(BillNo);
        this.dgDetail.DataSource = dsDetail.Tables[0];
        this.dgDetail.DataBind();
    }

    protected void dgMaster_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                CheckBox chk = new CheckBox();
                chk.Attributes.Add("style", "text-align:center;word-break:keep-all; white-space:nowrap");
                chk.ID = "checkAll";
                chk.Attributes.Add("onclick", "checkboxChange(this,'dgMaster',0);");
                chk.Text = "";
                e.Item.Cells[0].Controls.Add(chk);
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chk = new CheckBox();
                e.Item.Cells[0].Controls.Add(chk);
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

                e.Item.Attributes.Add("style", "cursor:pointer;");
            }
        }
        catch { }
    }
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


    protected void btnLoadDetail_Click(object sender, EventArgs e)
    {
        BillNo = dsMaster.Tables[0].Rows[Convert.ToInt32(hdnRowIndex.Value)]["BILLNO"].ToString();
        BindDetail();
        ViewState["BILLNO"] = BillNo;
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (dgMaster.Items.Count == 0)
        {
            return;
        }
        string BillNoList = "";
        bool hasSelected = false;
        for (int i = 0; i < dgMaster.Items.Count; i++)
        {
            CheckBox chk = ((CheckBox)dgMaster.Items[i].Cells[0].Controls[0]);
            if (chk.Checked)
            {
                BillNoList += dgMaster.Items[i].Cells[3].Text + ",";
                hasSelected = true;
            }
        }
        if (hasSelected)
        {
            BillNoList = BillNoList.Substring(0, BillNoList.Length - 1);
        }
        else
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, "请选择出库单");
            return;
        }

        Session["BillNoList"] = BillNoList;

        dsDetail = billDetail.QueryByBillNo(BillNoList);
        this.dgDetail.DataSource = dsDetail.Tables[0];
        this.dgDetail.DataBind();
        GridRowSpan();
        //pnlOne.Visible = false;
        //pnlTwo.Visible = true;
        div01display = "none";
        div02display = "block";

    }
    protected void btnPrevious2_Click(object sender, EventArgs e)
    {
        div02display = "none";
        div01display = "block";
    }
    protected void btnNext2_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeliveryAutoAllotPage.aspx?time=" + System.DateTime.Now.ToLongTimeString());
    }

    protected void GridRowSpan()
    {
        if (this.dgDetail.Items.Count >= 2)
        {
            int start = 0;
            int end = 0;
            for (int i = 1; i < dgDetail.Items.Count; i++)
            {
                if (dgDetail.Items[i].Cells[1].Text == dgDetail.Items[i - 1].Cells[1].Text)
                {
                    end = i;
                    if (i < dgDetail.Items.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        int span = end - start + 1;
                        dgDetail.Items[start].Cells[1].RowSpan = span;
                        for (int k = start + 1; k <= end; k++)
                        {
                            dgDetail.Items[k].Cells[1].Visible = false;
                        }
                    }
                }
                else
                {
                    int span = end - start + 1;
                    dgDetail.Items[start].Cells[1].RowSpan = span;
                    for (int k = start + 1; k <= end; k++)
                    {
                        dgDetail.Items[k].Cells[1].Visible = false;
                    }

                    start = end + 1;
                    end++;
                }
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            string start = "1900-01-01";
            string end = System.DateTime.Now.ToString("yyyy-MM-dd");
            if (this.txtStartDate.Text.Trim().Length > 0)
            {
                start = this.txtStartDate.Text.Trim();
            }
            if (this.txtEndDate.Text.Trim().Length > 0)
            {
                end = this.txtEndDate.Text.Trim();
            }
            filter = string.Format("(STATUS='2') AND BILLDATE BETWEEN '{0}' AND '{1}' AND BILLNO LIKE '{2}%' AND MEMO LIKE '%{3}%' AND TYPECODE LIKE '{4}%'"
                                      , start, end, this.txtBillNo.Text.Trim(), this.txtMemo.Text.Trim(), ddlBillType.SelectedValue);

            BindMaster();
            BindDetail();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../MainPage.aspx");
    }
}
