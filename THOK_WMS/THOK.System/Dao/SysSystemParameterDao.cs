using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using THOK.Util;

namespace THOK.System.Dao
{
    public class SysSystemParameterDao : BaseDao
    {
        public void ResetUserParameter(int UserID)
        {
            StoredProcParameter param = new StoredProcParameter();
            param.Names.Add("UserID");
            param.Values.Add(UserID.ToString());
            param.Types.Add(DbType.String);
            ExecuteNonQuery("sys_UserSysInfoReset", param);

        }

        public void ExecuteProcedure(string procName, StoredProcParameter param)
        {
            ExecuteNonQuery(procName, param);
        }

        public void SetData(string sql)
        {
            ExecuteNonQuery(sql);
        }

        public DataSet GetData(string sql)
        {
            return ExecuteQuery(sql);
        }


        public void InsertEntity(DataSet dataSet)
        {

        }

        public void UpdateEntity(DataSet dataSet)
        {

        }

        public void DeleteEntity(DataSet dataSet)
        {
            try
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (dataRow.RowState == DataRowState.Deleted)
                    {
                        ExecuteNonQuery("DELETE sys_SystemParameter WHERE SystemParameterID='" + dataRow["SystemParameterID", DataRowVersion.Original] + "'");
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
