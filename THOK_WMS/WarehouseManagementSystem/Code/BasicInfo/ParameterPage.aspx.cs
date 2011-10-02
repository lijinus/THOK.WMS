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
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;

public partial class Code_BasicInfo_ParameterPage :BasePage
{
    THOK.WMS.BLL.Parameter objPara = new THOK.WMS.BLL.Parameter();
    DataSet dsPara;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            dsPara = objPara.GetParameterInfo();
            if (dsPara.Tables[0].Rows.Count == 1)
            {
                this.txtID.Text = dsPara.Tables[0].Rows[0]["ID"].ToString();
                this.ddlDBTYPE_1.SelectedValue = dsPara.Tables[0].Rows[0]["DBTYPE_1"].ToString();
                this.txtSERVERNAME_1.Text = dsPara.Tables[0].Rows[0]["SERVERNAME_1"].ToString();
                this.txtDBNAME_1.Text = dsPara.Tables[0].Rows[0]["DBNAME_1"].ToString();
                this.txtUSERID_1.Text = dsPara.Tables[0].Rows[0]["USERID_1"].ToString();
                this.txtPWD_1.Text = dsPara.Tables[0].Rows[0]["PWD_1"].ToString();
                this.ddlDBTYPE_2.SelectedValue = dsPara.Tables[0].Rows[0]["DBTYPE_2"].ToString();
                this.txtSERVERNAME_2.Text = dsPara.Tables[0].Rows[0]["SERVERNAME_2"].ToString();
                this.txtDBNAME_2.Text = dsPara.Tables[0].Rows[0]["DBNAME_2"].ToString();
                this.txtUSERID_2.Text = dsPara.Tables[0].Rows[0]["USERID_2"].ToString();
                this.txtPWD_2.Text = dsPara.Tables[0].Rows[0]["PWD_2"].ToString();
                this.ddlDBTYPE_3.SelectedValue = dsPara.Tables[0].Rows[0]["DBTYPE_3"].ToString();
                this.txtSERVERNAME_3.Text = dsPara.Tables[0].Rows[0]["SERVERNAME_3"].ToString();
                this.txtDBNAME_3.Text = dsPara.Tables[0].Rows[0]["DBNAME_3"].ToString();
                this.txtUSERID_3.Text = dsPara.Tables[0].Rows[0]["USERID_3"].ToString();
                this.txtPWD_3.Text = dsPara.Tables[0].Rows[0]["PWD_3"].ToString();
                this.txtCELL_IMG_X.Text = dsPara.Tables[0].Rows[0]["CELL_IMG_X"].ToString();
                this.txtCELL_IMG_Y.Text = dsPara.Tables[0].Rows[0]["CELL_IMG_Y"].ToString();
                this.txtSPACE_Z.Text = dsPara.Tables[0].Rows[0]["SPACE_Z"].ToString();


                this.txtPWD_1.Attributes.Add("value", dsPara.Tables[0].Rows[0]["PWD_1"].ToString());
                this.txtPWD_2.Attributes.Add("value", dsPara.Tables[0].Rows[0]["PWD_2"].ToString());
                this.txtPWD_3.Attributes.Add("value", dsPara.Tables[0].Rows[0]["PWD_3"].ToString());

            }
            else
            {
                this.txtCELL_IMG_X.Text = "0";
                this.txtCELL_IMG_Y.Text = "0";
                this.txtSPACE_Z.Text = "0";
            }
            if (this.hdnXGQX.Value == "1")
            {
                this.btnSave.Enabled = true;
            }
        }
        else
        {
            dsPara = objPara.GetParameterInfo();
            this.txtPWD_1.Attributes.Add("value", Request["txtPWD_1"]);
            this.txtPWD_2.Attributes.Add("value", Request["txtPWD_2"]);
            this.txtPWD_3.Attributes.Add("value", Request["txtPWD_3"]);

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (this.dsPara.Tables[0].Rows.Count == 0) //添加
            {
                objPara.DBTYPE_1 = this.ddlDBTYPE_1.SelectedValue;
                objPara.SERVERNAME_1 = this.txtSERVERNAME_1.Text;
                objPara.DBNAME_1 = this.txtDBNAME_1.Text;
                objPara.USERID_1 = this.txtUSERID_1.Text;
                objPara.PWD_1 = this.txtPWD_1.Text;
                objPara.DBTYPE_2 = this.ddlDBTYPE_2.SelectedValue;
                objPara.SERVERNAME_2 = this.txtSERVERNAME_2.Text;
                objPara.DBNAME_2 = this.txtDBNAME_2.Text;
                objPara.USERID_2 = this.txtUSERID_2.Text;
                objPara.PWD_2 = this.txtPWD_2.Text;
                objPara.DBTYPE_3 = this.ddlDBTYPE_3.SelectedValue;
                objPara.SERVERNAME_3 = this.txtSERVERNAME_3.Text;
                objPara.DBNAME_3 = this.txtDBNAME_3.Text;
                objPara.USERID_3 = this.txtUSERID_3.Text;
                objPara.PWD_3 = this.txtPWD_3.Text;
                objPara.CELL_IMG_X = Convert.ToDouble(this.txtCELL_IMG_X.Text);
                objPara.CELL_IMG_Y = Convert.ToDouble(this.txtCELL_IMG_Y.Text);
                objPara.SPACE_Z = Convert.ToDouble(this.txtSPACE_Z.Text);
                objPara.Insert();
                dsPara = objPara.GetParameterInfo();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "参数保存成功！");
            }
            else //修改
            {
                objPara.ID = Convert.ToInt32(this.txtID.Text);
                objPara.DBTYPE_1 = this.ddlDBTYPE_1.SelectedValue;
                objPara.SERVERNAME_1 = this.txtSERVERNAME_1.Text;
                objPara.DBNAME_1 = this.txtDBNAME_1.Text;
                objPara.USERID_1 = this.txtUSERID_1.Text;
                objPara.PWD_1 = this.txtPWD_1.Text;
                objPara.DBTYPE_2 = this.ddlDBTYPE_2.SelectedValue;
                objPara.SERVERNAME_2 = this.txtSERVERNAME_2.Text;
                objPara.DBNAME_2 = this.txtDBNAME_2.Text;
                objPara.USERID_2 = this.txtUSERID_2.Text;
                objPara.PWD_2 = this.txtPWD_2.Text;
                objPara.DBTYPE_3 = this.ddlDBTYPE_3.SelectedValue;
                objPara.SERVERNAME_3 = this.txtSERVERNAME_3.Text;
                objPara.DBNAME_3 = this.txtDBNAME_3.Text;
                objPara.USERID_3 = this.txtUSERID_3.Text;
                objPara.PWD_3 = this.txtPWD_3.Text;
                objPara.CELL_IMG_X = Convert.ToDouble(this.txtCELL_IMG_X.Text);
                objPara.CELL_IMG_Y = Convert.ToDouble(this.txtCELL_IMG_Y.Text);
                objPara.SPACE_Z = Convert.ToDouble(this.txtSPACE_Z.Text);
                objPara.Update();
                dsPara = objPara.GetParameterInfo();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "参数保存成功！");
            }

            //ViewState["pwd1"] = dsPara.Tables[0].Rows[0]["PWD_1"].ToString();
            //ViewState["pwd2"] = dsPara.Tables[0].Rows[0]["PWD_2"].ToString();
            //ViewState["pwd3"] = dsPara.Tables[0].Rows[0]["PWD_3"].ToString();
        }
        catch (Exception exp)
        {
            JScript.Instance.ShowMessage(this.UpdatePanel1, exp.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../mainpage.aspx");
    }

    #region 测试连接

    protected void btnTestConnection1_Click(object sender, EventArgs e)
    {
        TestConnect(this.ddlDBTYPE_1.SelectedValue, this.txtSERVERNAME_1.Text, this.txtDBNAME_1.Text, this.txtUSERID_1.Text, this.txtPWD_1.Text);
    }
    protected void btnTestConnection2_Click(object sender, EventArgs e)
    {
        TestConnect(this.ddlDBTYPE_2.SelectedValue, this.txtSERVERNAME_2.Text, this.txtDBNAME_2.Text, this.txtUSERID_2.Text, this.txtPWD_2.Text);
    }
    protected void btnTestConnection3_Click(object sender, EventArgs e)
    {
        TestConnect(this.ddlDBTYPE_3.SelectedValue, this.txtSERVERNAME_3.Text, this.txtDBNAME_3.Text, this.txtUSERID_3.Text, this.txtPWD_3.Text);
    }

    void TestConnect(string dbType,string serverName,string dbName,string userid,string pwd)
    {
        #region SQL
        if (dbType.ToUpper() == "SQLSERVER")
        {

            //创建连接对象
            string ConnectionString = "server=" +serverName.Trim().Replace("\'", "\''") + ";" + "database =" + dbName.Trim().Replace("\'", "\''") + ";" + "UID =" + userid.Trim().Replace("\'", "\''") + ";" + " PWD =" + pwd.Trim().Replace("\'", "\''") + ";";
            SqlConnection mySqlConnection = new SqlConnection(ConnectionString);
            try
            {

                mySqlConnection.Open();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "数据库连接成功！");

            }
            catch
            {

                JScript.Instance.ShowMessage(this.UpdatePanel1, "数据库连接失败！");
            }
            finally
            {
                mySqlConnection.Close();
            }
        }
        #endregion

        #region ORACLE
        else if (dbType.ToUpper() == "ORACLE")
        {
            string ConnectionString = "Data Source=" + serverName.Trim().Replace("\'", "\''") + ";" + "User Id=" + userid.Trim().Replace("\'", "\''") + ";" + "Password=" + pwd.Trim().Replace("\'", "\''") + ";";
            OracleConnection myOracleConnection = new OracleConnection(ConnectionString);
            try
            {
                myOracleConnection.Open();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "数据库连接成功！");
            }
            catch
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "数据库连接失败！");
            }
            finally
            {
                myOracleConnection.Close();

            }
        }
        #endregion

        #region OLEDB
        else if (dbType.ToUpper()== "OLEDB")
        {
            string ConnectionString = string.Format("Provider=sqloledb;Data Source={0};Initial Catalog={1};User Id={2};Password={3};"
                                                      , serverName.Trim().Replace("\'", "\''")
                                                      , dbName.Trim().Replace("\'", "\''")
                                                      , userid.Trim().Replace("\'", "\''")
                                                      , pwd.Trim().Replace("\'", "\''"));

            //"Provider=sqloledb;Data Source=Aron1;Initial Catalog=pubs;User Id=sa;Password=asdasd;" 

            OleDbConnection myOleDbConnection = new OleDbConnection(ConnectionString);
            try
            {
                myOleDbConnection.Open();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "数据库连接成功！");
            }
            catch
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "数据库连接失败！");
            }
            finally
            {
                myOleDbConnection.Close();

            }
        }
        #endregion

        #region ODBC
        else if ( dbType.ToUpper() == "ODBC")
        {
            string ConnectionString = string.Format("Driver=SQL Server;Server={0};Database={1};Uid={2};Pwd={3};",serverName,dbName,userid,pwd);
            OdbcConnection myOdbcConnection = new OdbcConnection(ConnectionString);
            try
            {
                myOdbcConnection.Open();
                JScript.Instance.ShowMessage(this.UpdatePanel1, "数据库连接成功！");
            }
            catch(Exception exp)
            {
                JScript.Instance.ShowMessage(this.UpdatePanel1, "数据库连接失败！"+exp.Message);
            }
            finally
            {
                myOdbcConnection.Close();

            }
        }
        #endregion
    }

    #endregion
}
