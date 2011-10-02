using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Dao
{
    public class WarehouseAreaDao : BaseDao
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
                        ExecuteNonQuery(string.Format("delete WMS_WH_AREA WHERE AREA_ID='{0}'", dataRow["AREA_ID", DataRowVersion.Original]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNewAreaCode(string whcode)
        {
            string sql = string.Format("select top 1 AREACODE from WMS_WH_AREA WHERE AREACODE LIKE '{0}%' order by AREACODE desc", whcode);
            string sn = (string)ExecuteScalar(sql);
            if (sn == null)
            {
                return whcode + "-01";
            }
            int num = Convert.ToInt32(sn.Replace(whcode,"").Replace("-",""));
            num++;
            string newcode = num.ToString();
            for (int i = 0; i < 2 - num.ToString().Length; i++)
            {
                newcode = "0" + newcode;
            }
            return whcode+"-" + newcode;
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
