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

public partial class Code_StorageManagement_StorageRemindPage :BasePage
{
    Alarm objAlarm = new Alarm();
    DataSet dsRemind;
    int rowcount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //if (!IsPostBack)
            //{
                
            //}
            //else
            //{

            //}
            GridDataBind();

        }
        catch (Exception exp)
        {
            //JScript.Instance.ShowMessage(this, exp.Message);
        }
    }

    #region 数据源绑定
    void GridDataBind()
    {
        dsRemind = objAlarm.GetRemindList();
        rowcount = dsRemind.Tables[0].Rows.Count;
        if (dsRemind.Tables[0].Rows.Count == 0)
        {   
            
            dsRemind.Tables[0].Rows.Add(dsRemind.Tables[0].NewRow());
            gvRemind.DataSource = dsRemind;
            gvRemind.DataBind();
            
            int columnCount = gvRemind.Rows[0].Cells.Count;
            gvRemind.Rows[0].Cells.Clear();
            gvRemind.Rows[0].Cells.Add(new TableCell());
            gvRemind.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvRemind.Rows[0].Cells[0].Text = "没有预警信息 ";
            gvRemind.Rows[0].Visible = true;


        }
        else
        {
            this.gvRemind.DataSource = dsRemind.Tables[0];
            this.gvRemind.DataBind();
        }
    }
    #endregion


    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (rowcount > 0)
            {
                decimal max = Convert.ToDecimal(e.Row.Cells[2].Text);
                decimal min = Convert.ToDecimal(e.Row.Cells[3].Text);
                decimal qty = Convert.ToDecimal(e.Row.Cells[4].Text);
                if (qty > max)
                {
                    decimal a = qty - max;
                    e.Row.Cells[5].Text = "超出上限：" + a.ToString();
                }
                else if (qty < min)
                {
                    decimal b = min - qty;
                    e.Row.Cells[5].Text = "超出下限：" + b.ToString();
                }
            }
            

            if (e.Row.RowIndex % 2 == 0)
            {
                e.Row.BackColor = Color.FromName(Session["grid_OddRowColor"].ToString());
            }
            else
            {
                e.Row.BackColor = Color.FromName(Session["grid_EvenRowColor"].ToString());
            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f5f5f5',this.style.fontWeight=''; this.style.cursor='hand';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
        }
    }
}
