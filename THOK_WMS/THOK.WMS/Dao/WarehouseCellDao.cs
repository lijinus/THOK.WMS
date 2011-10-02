using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Dao
{
    public class WarehouseCellDao : BaseDao
    {
        public DataSet Query(string sql,int pageIndex ,int pageSize)
        {
            return ExecuteQuery(sql, "WMS_WH_CELL", (pageIndex-1) * pageSize, pageSize);
        }

        //2011.4.6
        public DataSet Query(string sql)
        {
            return ExecuteQuery(sql);
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
                        ExecuteNonQuery("delete WMS_WH_CELL WHERE CELL_ID='" + dataRow["CELL_ID", DataRowVersion.Original] + "'");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNewCellCode(string ShelfCode)
        {
            string sql = string.Format("select top 1 CELLCODE from WMS_WH_CELL WHERE CELLCODE LIKE '{0}%' order by CELLCODE desc", ShelfCode);
            string sn = (string)ExecuteScalar(sql);
            if (sn == null)
            {
                return ShelfCode + "-01-1";
            }
            string[] sep = new string[] { "-"};
            string[] arycode = sn.Replace(ShelfCode, "").Split(sep,StringSplitOptions.RemoveEmptyEntries);
            string newcolcode = arycode[0];
            string layer = arycode[1];
            if (layer == "3")
            {
                int num = Convert.ToInt32(newcolcode);
                num++;
                newcolcode = num.ToString();
                for (int i = 0; i < 2 - num.ToString().Length; i++)
                {
                    newcolcode = "0" + newcolcode;
                }
                return ShelfCode + "-" + newcolcode + "-1";
            }
            else
            {
                int num = Convert.ToInt32(layer);
                num++;
                return ShelfCode + "-" + newcolcode + "-" + num.ToString();
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
