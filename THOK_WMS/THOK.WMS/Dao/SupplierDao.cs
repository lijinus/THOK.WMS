using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Dao
{
    public class SupplierDao : BaseDao
    {
        public DataSet Query(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
        {
            return ExecuteQuery(TableViewName, PrimaryKey, QueryFields, pageIndex, pageSize, orderBy, filter, strTableName);
        }

        public string GetNewSupplierCode()
        {
            string sql = "select top 1 SUPPLIERCODE from BI_SUPPLIER order by SUPPLIERCODE desc";
            string sn = (string)ExecuteScalar(sql);
            DataSet ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "code\\TableXML\\BI_SUPPLIER.xml");
            string prefix = ds.Tables["TABLE"].Rows[0]["PrefixText"].ToString();
            if (sn == null)
            {
                return prefix + "000001";
            }
            int num = Convert.ToInt32(sn.Substring(prefix.Length));
            num++;
            string newcode = num.ToString();
            for (int i = 0; i < 6 - num.ToString().Length; i++)
            {
                newcode = "0" + newcode;
            }
            return prefix + newcode;
        }

        //public object ExecScaler(string sql)
        //{
        //    return ExecuteScalar(sql);
        //}

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
                        ExecuteNonQuery("delete BI_SUPPLIER WHERE SUPPLIERCODE='" + dataRow["SUPPLIERCODE", DataRowVersion.Original] + "'");
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
