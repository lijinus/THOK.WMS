using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Dao
{
    public class WarehouseShelfDao : BaseDao
    {
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
                        ExecuteNonQuery("delete WMS_WH_SHELF WHERE SHELF_ID='" + dataRow["SHELF_ID", DataRowVersion.Original] + "'");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNewShelfCode(string AreaCode)
        {
            string sql = string.Format("select top 1 SHELFCODE from WMS_WH_SHELF WHERE SHELFCODE LIKE '{0}%' order by SHELFCODE desc", AreaCode);
            string sn = (string)ExecuteScalar(sql);
            if (sn == null)
            {
                return AreaCode + "-001";
            }
            int num = Convert.ToInt32(sn.Replace(AreaCode,"").Replace("-",""));
            num++;
            string newcode = num.ToString();
            for (int i = 0; i < 3 - num.ToString().Length; i++)
            {
                newcode = "0" + newcode;
            }
            return AreaCode + "-" + newcode;
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
