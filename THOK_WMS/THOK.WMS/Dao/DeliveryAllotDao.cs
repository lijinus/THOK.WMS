using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Dao
{
    public class DeliveryAllotDao : BaseDao
    {
        public DataSet Query(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
        {
            return ExecuteQuery(TableViewName, PrimaryKey, QueryFields, pageIndex, pageSize, orderBy, filter, strTableName);
        }

        public void BatchInsertAllotment(DataTable tableMaster,DataTable tableDetail)
        {
            BatchInsert(tableMaster, "WMS_OUT_ALLOTMASTER");
            BatchInsert(tableDetail, "WMS_OUT_ALLOTDETAIL");
        }
        public void BatchInsertAllotment(DataTable tableMaster, DataTable tableDetail, DataTable tableMoveDetail, DataTable tableMoveMaster)
        {
            BatchInsert(tableMaster, "WMS_OUT_ALLOTMASTER");
            BatchInsert(tableDetail, "WMS_OUT_ALLOTDETAIL");
            BatchInsert(tableMoveDetail, "WMS_MOVE_BILLDETAIL");
            BatchInsert(tableMoveMaster, "WMS_MOVE_BILLMASTER");

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

        public DataSet Rate(string sql)
        {
            return ExecuteQuery(sql);
        }
    }
}
