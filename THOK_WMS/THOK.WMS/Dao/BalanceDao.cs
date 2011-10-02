using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Dao
{
    public class BalanceDao:BaseDao
    {
        public void ExecProc(string procName, StoredProcParameter para)
        {
            ExecuteNonQuery(procName, para);
        }

        public DataSet Query(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
        {
            return ExecuteQuery(TableViewName, PrimaryKey, QueryFields, pageIndex, pageSize, orderBy, filter, strTableName);
        }

        public DataSet Query(string sql, string tableName, int pageIndex, int pageSize)
        {
            return ExecuteQuery(sql, tableName, (pageIndex - 1) * pageSize, pageSize);
        }

        public int GetRowCount(string TableViewName, string filter)
        {
            string sql = string.Format("select count(*) from {0}" +
                                         " where {1} "
                                         , TableViewName, filter);
            return (int)ExecuteScalar(sql);
        }

        public DataSet GetData(string sql)
        {
            return ExecuteQuery(sql);
        }
    }
}
