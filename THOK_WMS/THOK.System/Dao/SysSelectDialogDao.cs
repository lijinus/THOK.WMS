using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.System.Dao
{
    public class SysSelectDialogDao:BaseDao
    {
        public int GetRowCount(string TableViewName, string filter)
        {
            string sql = string.Format("select count(*) from {0}" +
                                         " where {1} "
                                         , TableViewName, filter);
            return (int)ExecuteScalar(sql);
        }

        public DataSet ExecuteProcedure(string procName, StoredProcParameter param)
        {
            return ExecuteQuery(procName, param);
        }

        public DataSet GetData(string sql)
        {
            return ExecuteQuery(sql);
        }
    }
}
