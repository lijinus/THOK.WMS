using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.System.BLL;

namespace THOK.System.Dao
{
    public class SysExceptionalLogDao:BaseDao
    {
        public DataSet QueryExceptionLog(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
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
                        ExecuteNonQuery("DELETE sys_ExceptionalLog WHERE ExceptionalLogID='" + dataRow["ExceptionalLogID", DataRowVersion.Original] + "'");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertEntity(ExceptionLog setExpLog)
        {
            try
            {
                string sqlInsert = string.Format("INSERT INTO sys_ExceptionalLog([CatchTime],[ModuleName],[FunctionName]" +
                                                 " ,[ExceptionalType] ,[ExceptionalDescription])" +
                                                " VALUES('{0}','{1}','{2}','{3}','{4}')"
                                                ,setExpLog.CatchTime.ToString()
                                                ,setExpLog.ModuleName,setExpLog.FunctionName,setExpLog.ExceptionalType,setExpLog.ExceptionalDescription);
                ExecuteNonQuery(sqlInsert);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetData(string sql)
        {
            ExecuteNonQuery(sql);
        }
    }
}
