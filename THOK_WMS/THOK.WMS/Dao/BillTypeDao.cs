using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Dao
{
    public class BillTypeDao:BaseDao
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

        public string GetNewTypeCode(string businessType)
        {
            string sql = string.Format("select top 1 TYPECODE from WMS_BILLTYPE WHERE TYPECODE LIKE '{0}%' order by TYPECODE desc", businessType);
            string sn = (string)ExecuteScalar(sql);
            string prefix = businessType; 
            if (sn == null)
            {
                return prefix + "01";
            }
            int num = Convert.ToInt32(sn.Substring(prefix.Length));
            num++;
            string newcode = num.ToString();
            for (int i = 0; i < 2 - num.ToString().Length; i++)
            {
                newcode = "0" + newcode;
            }
            return prefix + newcode;
        }

        public void DeleteEntity(DataSet dataSet)
        {
            try
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (dataRow.RowState == DataRowState.Deleted)
                    {
                        ExecuteNonQuery("delete wms_billtype WHERE ID='" + dataRow["ID", DataRowVersion.Original] + "'");
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
