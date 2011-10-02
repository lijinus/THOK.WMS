using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.System.Dao;

namespace THOK.System.BLL
{
    public class OperatorLog
    {
        private string strTableView = "sys_OperatorLog";
        private string strPrimaryKey = "OperatorLogID";
        private string strQueryFields = "OperatorLogID,LoginUser,LoginTime,LoginModule,ExecuteOperator";

        public DataSet GetOperatorLogList(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysOperatorLogDao LogDao = new SysOperatorLogDao();
                return LogDao.QueryOperatorLog(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysOperatorLogDao LogDao = new SysOperatorLogDao();
                return LogDao.GetRowCount(strTableView, filter);
            }
        }

        public bool Delete(DataSet dataSet)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysOperatorLogDao LogDao = new SysOperatorLogDao();
                LogDao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        public bool Clear()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysOperatorLogDao LogDao = new SysOperatorLogDao();
                LogDao.SetData("truncate table sys_OperatorLog");
                flag = true;
            }
            return flag;
        }

        public bool InsertOperationLog(DateTime operateTime, string OperateUser, string moduleName, string executeOperation)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysOperatorLogDao LogDao = new SysOperatorLogDao();
                LogDao.Insert(operateTime, OperateUser, moduleName.Replace("\'", "\''"), executeOperation.Replace("\'", "\''"));
                flag = true;
            }
            return flag;
        }
    }
}
