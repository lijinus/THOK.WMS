using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.System.Dao
{
    public class SysUserDao : BaseDao
    {
        public DataSet QueryUser(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
        {
            return ExecuteQuery(TableViewName, PrimaryKey, QueryFields, pageIndex, pageSize, orderBy, filter, strTableName);
        }

        public DataSet GetUser(string sql)
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


        public void InsertEntity(DataSet dataSet)
        {
            try
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (dataRow.RowState == DataRowState.Added)
                    {
                        SqlCreate sqlCreate = new SqlCreate("sys_UserList", SqlType.INSERT);
                        sqlCreate.AppendQuote("UserName", dataRow["UserName"]);
                        sqlCreate.AppendQuote("UserPassword", dataRow["UserPassword"]);
                        sqlCreate.AppendQuote("EmployeeCode", dataRow["EmployeeCode"]);
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
                        string sqlUpdate = string.Format("update sys_UserList set UserName='{0}',EmployeeCode='{1}',Memo='{2}' where UserID={3}"
                            , dataRow["UserName"].ToString(), dataRow["EmployeeCode"].ToString(), dataRow["Memo"].ToString().Replace("\'", "\''"),dataRow["UserID"].ToString());
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
                        ExecuteNonQuery("DELETE sys_UserList WHERE UserID='" + dataRow["UserID", DataRowVersion.Original] + "'");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEntity(string sql)
        {
            try
            {
                ExecuteNonQuery(sql);
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

        public DataSet GetData(string sql)
        {
            return ExecuteQuery(sql);
        }
    }
}
