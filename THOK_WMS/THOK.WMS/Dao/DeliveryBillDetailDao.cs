using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Dao
{
    public class DeliveryBillDetailDao : BaseDao
    {
        public DataSet Query(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
        {
            return ExecuteQuery(TableViewName, PrimaryKey, QueryFields, pageIndex, pageSize, orderBy, filter, strTableName);
        }

        public DataSet Query(string sql, string tableName, int pageIndex, int pageSize)
        {
            //int start = (pageIndex - 1) * pageSize;
            //return ExecuteQuery(sql, tableName, start, pageSize);
            return ExecuteQuery(sql);//��ʵ�ַ�ҳ
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
                        ExecuteNonQuery("delete WMS_OUT_BILLDetail WHERE ID='" + dataRow["ID", DataRowVersion.Original] + "'");
                        ExecuteNonQuery("delete DWV_IWMS_OUT_STORE_BILL_DETAIL WHERE STORE_BILL_DETAIL_ID='" + dataRow["ID", DataRowVersion.Original] + "'");
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