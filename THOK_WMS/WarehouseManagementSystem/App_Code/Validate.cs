using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Configuration;
using THOK.Util;
using System.Data;

/// <summary>
/// Validate 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class Validate : System.Web.Services.WebService
{

    public Validate()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string UniqueValidate(string tableName, string fieldName, string value,string filter)
    {
        using (PersistentManager persistentManager = new PersistentManager())
        {
            ValidateDao dao = new ValidateDao();
            string commandText = string.Format("select count(*) from {1} where {0}='{2}' and {3}", fieldName, tableName, value,filter);
            string s = dao.GetScalar(commandText).ToString();
            return s;
            
        }
    }

    [WebMethod]
    public string IsExist(string tableName, string fieldName, string value)
    {
        using (PersistentManager persistentManager = new PersistentManager())
        {
            ValidateDao dao = new ValidateDao();
            string commandText = string.Format("select count(*) from {1} where {0}='{2}'", fieldName, tableName, value);
            string s = dao.GetScalar(commandText).ToString();
            return s;
        }
    }
}

public class ValidateDao : BaseDao
{
    public object GetScalar(string sql)
    {
        return ExecuteScalar(sql);
    }

    public DataSet GetData(string sql)
    {
        return ExecuteQuery(sql);
    }
}

