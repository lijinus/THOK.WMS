using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.System.Dao
{
    public class SysGroupDao : BaseDao
    {
        public DataSet QueryGroup(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
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

        public int GetGroupMemberCount(int GroupID)
        {
            string sql=string.Format("select count(*) from sys_UserList where GroupID={0}",GroupID);
            return (int)ExecuteScalar(sql);
        }


        public void InsertEntity(DataSet dataSet)
        {
            try
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (dataRow.RowState == DataRowState.Added)
                    {
                        SqlCreate sqlCreate = new SqlCreate("sys_GroupList", SqlType.INSERT);
                        sqlCreate.AppendQuote("GroupName", dataRow["GroupName"]);
                        sqlCreate.AppendQuote("Memo", dataRow["Memo"].ToString().Replace("\'", "\''"));
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
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (dataRow.RowState == DataRowState.Modified)
                    {
                        string sqlUpdate = string.Format("update sys_GroupList set GroupName='{0}',Memo='{1}' where GroupID={2}"
                            , dataRow["GroupName"].ToString(), dataRow["Memo"].ToString().Replace("\'", "\''"), dataRow["GroupID"].ToString());
                        ExecuteNonQuery(sqlUpdate);
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
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (dataRow.RowState == DataRowState.Deleted)
                    {
                        ExecuteNonQuery("delete sys_GroupList WHERE GroupID='" + dataRow["GroupID", DataRowVersion.Original] + "'");
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
