using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Dao
{
    public class ProductDao : BaseDao
    {
        public DataSet Query(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
        {
            return ExecuteQuery(TableViewName, PrimaryKey, QueryFields, pageIndex, pageSize, orderBy, filter, strTableName);
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
                        ExecuteNonQuery("delete WMS_PRODUCT WHERE PRODUCTCODE='" + dataRow["PRODUCTCODE", DataRowVersion.Original] + "'");
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
