using System;
using System.Collections.Generic;
using System.Text;
using THOK.System.Dao;
using System.Data;
using THOK.Util;

namespace THOK.System.BLL
{
    public class ExceptionLog
    {
        private string strTableView = "sys_ExceptionalLog";
        private string strPrimaryKey = "ExceptionalLogID";
        //private string strOrderByFields = "ExceptionalLogID ASC";
        private string strQueryFields = "ExceptionalLogID,CatchTime,ModuleName,FunctionName,ExceptionalType,ExceptionalDescription";

        public DataSet GetOperatorLogList(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysExceptionalLogDao LogDao = new SysExceptionalLogDao();
                return LogDao.QueryExceptionLog(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysExceptionalLogDao LogDao = new SysExceptionalLogDao();
                return LogDao.GetRowCount(strTableView, filter);
            }
        }




        public bool Insert(ExceptionLog setExpLog)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysExceptionalLogDao LogDao = new SysExceptionalLogDao();
                LogDao.InsertEntity(setExpLog);
                flag = true;
            }
            return flag;
        }

        public bool Delete(DataSet dataSet)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysExceptionalLogDao LogDao = new SysExceptionalLogDao();
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
                SysExceptionalLogDao LogDao = new SysExceptionalLogDao();
                LogDao.SetData("truncate table sys_ExceptionalLog");
                flag = true;
            }
            return flag;
        }

        #region property
        DateTime _CatchTime;
        string _ModuleName;
        string _FunctionName;
        string _ExceptionalType;
        string _ExceptionalDesc;
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime CatchTime
        {
            get
            {
                return _CatchTime;
            }
            set
            {
                _CatchTime = value;
            }
        }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName
        {
            get
            {
                return _ModuleName;
            }
            set
            {
                if (value is string)
                {
                    _ModuleName = value;
                }
            }
        }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string FunctionName
        {
            get
            {
                return _FunctionName;
            }
            set
            {
                if (value is string)
                {
                    _FunctionName = value;
                }
            }
        }
        /// <summary>
        /// 错误类型
        /// </summary>
        public string ExceptionalType
        {
            get
            {
                return _ExceptionalType;
            }
            set
            {
                if (value is string)
                {
                    _ExceptionalType = value;
                }
            }
        }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string ExceptionalDescription
        {
            get
            {
                return _ExceptionalDesc;
            }
            set
            {
                if (value is string)
                {
                    _ExceptionalDesc = value;
                }
            }
        }

        #endregion
    }
}
