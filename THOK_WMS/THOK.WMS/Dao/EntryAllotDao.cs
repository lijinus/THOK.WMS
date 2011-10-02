using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Dao
{
    public class EntryAllotDao:BaseDao
    {
        //public void ExecProc(string procName, StoredProcParameter para)
        //{
        //    ExecuteNonQuery(procName, para);
        //}

        //public object ExecScaler(string sql)
        //{
        //    return ExecuteScalar(sql);
        //}
        public DataSet Query(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
        {
            return ExecuteQuery(TableViewName, PrimaryKey, QueryFields, pageIndex, pageSize, orderBy, filter, strTableName);
        }

        public void BatchInsertAllotment(DataTable tableAllotment)
        {
            BatchInsert(tableAllotment,"WMS_IN_ALLOT");
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

        public void SetData(string sql)
        {
            ExecuteNonQuery(sql);
        }
    }
}
