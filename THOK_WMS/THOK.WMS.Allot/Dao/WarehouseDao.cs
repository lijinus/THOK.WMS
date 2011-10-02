using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Allot.Dao
{
    public class WarehouseDao : BaseDao
    {
        public int GetRowCount(string TableViewName, string filter)
        {
            string sql = string.Format("select count(*) from {0}" +
                                         " where {1} "
                                         , TableViewName, filter);
            return (int)ExecuteScalar(sql);
        }

        public string  GetNewCode(string type)
        {
            string sql = string.Format("select top 1 WH_CODE from WMS_WAREHOUSE WHERE WH_CODE LIKE '{0}%' order by WH_CODE desc",type);
            string sn = (string)ExecuteScalar(sql);
            if (sn == null)
            {
                return type + "01";
            }
            int num = Convert.ToInt32(sn.Substring(1));
            num++;
            string newcode = num.ToString();
            for (int i = 0; i < 2 - num.ToString().Length; i++)
            {
                newcode = "0" + newcode;
            }
            return type + newcode;
        }

        public void DeleteEntity(DataSet dataSet)
        {
            try
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (dataRow.RowState == DataRowState.Deleted)
                    {
                        ExecuteNonQuery("delete WMS_WAREHOUSE WHERE WH_ID='" + dataRow["WH_ID", DataRowVersion.Original] + "'");
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
