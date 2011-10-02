using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.WMS.Dao;
using System.Data;
namespace THOK.WMS.Dao
{
    public class CompanyDao:BaseDao
    {
        public DataSet GetData(string sql)
        {
            return ExecuteQuery(sql);
        }

        public void SetData(string sql)
        {
            ExecuteNonQuery(sql);
        }
    }
}
