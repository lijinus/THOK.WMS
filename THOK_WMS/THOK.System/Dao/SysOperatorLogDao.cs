using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.System.Dao
{
    public class SysOperatorLogDao:BaseDao
    {
        public DataSet QueryOperatorLog(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
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
                        ExecuteNonQuery("DELETE sys_OperatorLog WHERE OperatorLogID='" + dataRow["OperatorLogID", DataRowVersion.Original] + "'");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(DateTime operateTime, string OperateUser, string moduleName, string executeOperation)
        {
            try
            {
                string sql = string.Format("insert into sys_OperatorLog ([LoginUser],[LoginTime],[LoginModule],[ExecuteOperator]) values ('{0}','{1}','{2}','{3}')",OperateUser,operateTime.ToString(),moduleName,executeOperation);
                ExecuteNonQuery(sql);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void SetData(string sql)
        {
            ExecuteNonQuery(sql);
        }
    }
}
