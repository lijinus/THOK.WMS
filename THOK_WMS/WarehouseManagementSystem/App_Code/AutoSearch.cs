using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// AutoSearch 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class AutoSearch : System.Web.Services.WebService
{

    public AutoSearch()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod]
    public string[] GetValueList(string prefixText, int count, string contextKey)
    {
        string[] separator = new string[] { ","};
        string[] aryPara = contextKey.Split(separator, StringSplitOptions.None);
        string tableName = aryPara[0];
        string fieldName = aryPara[1];
        List<string> items = new List<string>();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["managedbConnectionString"].ToString());
        con.Open();
        string commandText = string.Format("select distinct {0} from {1} where {0} like @prefixname order by {0}",fieldName,tableName);
        SqlCommand com = new SqlCommand(commandText, con);
        com.Parameters.Add("@prefixname", SqlDbType.NVarChar).Value = prefixText + "%";
        SqlDataReader sdr = com.ExecuteReader();
        while (sdr.Read())
        {
            items.Add(sdr.GetString(0));
        }
        sdr.Close();
        con.Close();
        return items.ToArray();
    }
}

