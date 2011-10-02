using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.System.Dao
{
    public class SysCopyRightDao : BaseDao
    {
        public void InsertEntity(DataSet dataSet)
        {
            try
            {
                foreach (DataRow dataRow in dataSet.Tables["AS_SYS_BARCODE"].Rows)
                {
                    if (dataRow.RowState == DataRowState.Added)
                    {
                        SqlCreate sqlCreate = new SqlCreate("AS_SYS_BARCODE", SqlType.UPDATE);
                        sqlCreate.AppendQuote("CIGARETTECODE", dataRow["CIGARETTECODE"]);
                        ExecuteNonQuery(sqlCreate.GetSQL());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateEntity(DataSet dataSet)
        {
            try
            {
                foreach (DataRow dataRow in dataSet.Tables["AS_SYS_BARCODE"].Rows)
                {
                    if (dataRow.RowState == DataRowState.Modified)
                    {
                        SqlCreate sqlCreate = new SqlCreate("AS_BI_CHANNEL", SqlType.UPDATE);
                        sqlCreate.AppendQuote("CIGARETTECODE", dataRow["CIGARETTECODE"]);
                        sqlCreate.AppendWhereQuote("CHANNELID", dataRow["CHANNELID", DataRowVersion.Original]);
                        ExecuteNonQuery(sqlCreate.GetSQL());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEntity(DataSet dataSet)
        {
            try
            {
                foreach (DataRow dataRow in dataSet.Tables["AS_SYS_BARCODE"].Rows)
                {
                    if (dataRow.RowState == DataRowState.Deleted)
                    {
                        ExecuteNonQuery("DELETE FROM AS_SYS_BARCODE WHERE FIELDCODE='" + dataRow["FIELDCODE"] + "'");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
