using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Dao
{
    public class ComparisonDao:BaseDao
    {
        public DataSet GetData(string sql)
        {
            return ExecuteQuery(sql);
        }
    }
}
