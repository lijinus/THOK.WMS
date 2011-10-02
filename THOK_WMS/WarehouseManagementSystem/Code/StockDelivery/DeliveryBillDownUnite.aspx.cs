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
using THOK.WMS;
using THOK.WMS.Download.Bll;


public partial class Code_StockDelivery_DeliveryBillDownUnite :BasePage
{
    DownOutBillBll outbill = new DownOutBillBll();
    DataTable ordergather = new DataTable();
    DeliveryBillMaster billMaster = new DeliveryBillMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Warehouse objHouse = new Warehouse();
            DataSet dsTemp = objHouse.QueryAllWarehouse();
            DataTable batchnodt = outbill.GetBatchNo();
            this.ddlWarehouse.DataSource = dsTemp.Tables[0].DefaultView;
            this.ddlWarehouse.DataTextField = "WH_NAME";
            this.ddlWarehouse.DataValueField = "WH_CODE";
            this.ddlWarehouse.DataBind();
            this.ddlBatch.DataSource = batchnodt;
            this.ddlBatch.DataValueField = "BATCHNO";
            this.ddlBatch.DataTextField = "BATCHNO";
            this.ddlBatch.DataBind();
            this.txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            
        }
        //this.GetBind();
    }


    public void GetBind()
    {

        //ordergather = outbill.GetOrderGather();
        ordergather = outbill.GetOrderGather(this.txtDate.Text.Trim(), this.ddlBatch.Text);
        if (ordergather.Rows.Count == 0)
        {
            ordergather.Rows.Add(ordergather.NewRow());
            gvUnite.DataSource = ordergather;
            gvUnite.DataBind();
            int columnCount = gvUnite.Rows[0].Cells.Count;
            gvUnite.Rows[0].Cells.Clear();
            gvUnite.Rows[0].Cells.Add(new TableCell());
            gvUnite.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvUnite.Rows[0].Cells[0].Text = "分拣线没有当前日期的单据！ ";
            gvUnite.Rows[0].Visible = true;
        }
        else
        {
            gvUnite.DataSource = ordergather;
            gvUnite.DataBind();
        }
        //gvUnite.Visible = true;
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        GetBind();
    }

    

    protected void btnUnite_Click(object sender, EventArgs e)
    {
        try
        {
            decimal quantity = 0.00M;
            string billno;
            string datelist = "";
            bool flag = false;
            if (this.txtDate.Text == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请选择日期");
            }

            for (int i = 0; i < gvUnite.Rows.Count; i++)
            {
                datelist += gvUnite.Rows[i].Cells[0].Text.ToString() + ",";
            }
            datelist = datelist.Substring(0, datelist.Length - 1);
            string[] arraydatelist = datelist.Split(',');
            for (int j = 0; j < arraydatelist.Length; j++)
            {
                if (string.Equals(this.txtDate.Text.ToString(), arraydatelist[j]) == false)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }

            if (flag == false)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "没有您输入日期的数据！");
            }

            if (this.ddlBatch.Text == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请选择批次号");
            }
            if (this.ddlWarehouse.Text == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请选择仓库");
            }
            if (this.txtBillTypeName.Text == "")
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "请选择出库单类型");
            }
            else
            {
                for (int i = 0; i < gvUnite.Rows.Count; i++)
                {
                    if (string.Equals(gvUnite.Rows[i].Cells[0].Text, this.txtDate.Text) == true && string.Equals(gvUnite.Rows[i].Cells[1].Text, this.ddlBatch.Text) == true)
                    {
                        quantity += Convert.ToDecimal(gvUnite.Rows[i].Cells[4].Text.ToString());
                    }
                }
                billno = billMaster.GetNewBillNo();
                outbill.GetOrderGather(billno, this.txtDate.Text, this.ddlBatch.Text, this.txtBillTypeCode.Text, this.ddlWarehouse.Text, quantity);
                outbill.GetOrderGather(billno, this.txtDate.Text, this.ddlBatch.Text);
                JScript.Instance.ShowMessage(this.UpdatePanel1, "下载合单完成！");
            }
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }
}
