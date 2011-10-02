using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Dao
{
    public class DeliveryBillMasterDao : BaseDao
    {
        public DataSet Query(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
        {
            return ExecuteQuery(TableViewName, PrimaryKey, QueryFields, pageIndex, pageSize, orderBy, filter, strTableName);
        }

        //public void ExecProc(string procName, StoredProcParameter para)
        //{
        //    ExecuteNonQuery(procName, para);
        //}

        public object ExecScaler(string sql)
        {
            return ExecuteScalar(sql);
        }

        public int GetRowCount(string TableViewName, string filter)
        {
            string sql = string.Format("select count(*) from {0}" +
                                         " where {1} "
                                         , TableViewName, filter);
            return (int)ExecuteScalar(sql);
        }


        public void DeleteEntity(DataSet dataSet)
        {
            try
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (dataRow.RowState == DataRowState.Deleted)
                    {
                        ExecuteNonQuery("delete WMS_OUT_BILLDETAIL WHERE BILLNO='" + dataRow["BILLNO", DataRowVersion.Original] + "'");
                        ExecuteNonQuery("delete WMS_OUT_BILLMASTER WHERE ID='" + dataRow["ID", DataRowVersion.Original] + "'");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
