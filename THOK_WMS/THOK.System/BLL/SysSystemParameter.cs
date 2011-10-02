using System;
using System.Collections.Generic;
using System.Text;
using THOK.System.Dao;
using THOK.Util;
using System.Data;
using System.Collections;

namespace THOK.System.BLL
{
    public class SysSystemParameter
    {
        #region property
        private int _systemParameterID;
        private string _parameterType;
        private string _parameterName;
        private string _parameterValue;
        private string _parameterText;
        private string _description;
        private int _state;

        public int SystemParameterID
        {
            get
            {
                return _systemParameterID;
            }
            set
            {
                _systemParameterID = value;
            }
        }

        public string ParameterType
        {
            get
            {
                return _parameterType;
            }
            set
            {
                _parameterType = value;
            }
        }

        public string ParameterName
        {
            get
            {
                return _parameterName;
            }
            set
            {
                _parameterName = value;
            }
        }

        public string ParameterValue
        {
            get
            {
                return _parameterValue;
            }
            set
            {
                _parameterValue = value;
            }
        }

        public string ParameterText
        {
            get
            {
                return _parameterText;
            }
            set
            {
                _parameterText = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public int State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        #endregion

        public DataSet GetSystemParameter()
        {
            string sql = "select ParameterName,ParameterValue,ParameterText from sys_SystemParameter where ParameterType='0' and State=1";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                return paraDao.GetData(sql);
            }
        }

        public DataSet GetFormatParameter()
        {
            string sql = "select ParameterName,ParameterValue,ParameterText from sys_SystemParameter where ParameterType='1' and ParameterName in('ddl_FormatDateTimeMode','ddl_FormatMoneyMode','ddl_FormatNumberMode','sys_SessionTimeOut')";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                return paraDao.GetData(sql);
            }
        }

        public DataSet GetOptionParameter(string parameterName)
        {
            string sql = "select ParameterName,ParameterValue,ParameterText from sys_SystemParameter where ParameterName='" + parameterName + "'";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                return paraDao.GetData(sql);
            }
        }

        public DataSet GetAllOptionParameter()
        {
            string strSql = " select SystemParameterID , ParameterName,ParameterValue , ParameterText," +
                " Description , State" +
                " from sys_SystemParameter  where ParameterType='1' order by ParameterName";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                return paraDao.GetData(strSql);
            }
        }

        public DataSet SearchParameter(SysSystemParameter obj)
        {
            string strSql = string.Format(" select SystemParameterID , ParameterName,ParameterValue , ParameterText," +
                " Description , State" +
                " from sys_SystemParameter  where ParameterType='1'  and ParameterName like '%{0}%'" +
                " and ParameterValue like '%{1}%'" +
                " and ParameterText like'%{2}%'" +
                " and Description like '%{3}%'" +
                " and State like '%{4}%' order by ParameterName ", obj.ParameterName, obj.ParameterValue, obj.ParameterText, obj.Description, obj.State);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                return paraDao.GetData(strSql);
            }
        }

        public DataSet GetSystemParameterName()
        {
            string sql = "select distinct(ParameterName),Description from sys_SystemParameter where ParameterType=1";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                return paraDao.GetData(sql);
            }
        }

        public DataSet GetVersionParameter()
        {
            string sql = "select SoftwareName,Version,Company,Copyrigth,CompanyTelephone,CompanyFax,CompanyAddress,CompanyEmail,CompanyWeb,Memo from sys_SoftwareDescription where state=1";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                return paraDao.GetData(sql);
            }
        }


        /// <summary>
        /// 恢复用户系统参数默认值
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool ResetUserParameter(int UserID)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                paraDao.ResetUserParameter(UserID);
                flag = true;
            }
            return flag;
        }

        public bool UpdateSystemParameter(string procName, StoredProcParameter param)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                paraDao.ExecuteProcedure(procName, param);
                flag = true;
            }
            return flag;
        }

        public bool UpdateOptionParameter(SysSystemParameter obj)
        {
            bool flag = false;
            string sql = string.Format("update sys_SystemParameter set ParameterName='{0}',ParameterValue='{1}',ParameterText='{2}',Description='{3}',State='{4}' where SystemParameterID={5}"
                                        , obj.ParameterName, obj.ParameterValue, obj.ParameterText, obj.Description, obj.State, obj.SystemParameterID);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                paraDao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool InsertOptionParameter(SysSystemParameter obj)
        {
            bool flag = false;
            string sql = string.Format("insert into sys_SystemParameter (ParameterName,ParameterValue,ParameterText,Description,State,ParameterType) values('{0}','{1}','{2}','{3}','{4}','{5}')"
                                        , obj.ParameterName, obj.ParameterValue, obj.ParameterText, obj.Description, obj.State, "1");
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                paraDao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool DeleteOptionParameter(DataSet dsParam)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                paraDao.DeleteEntity(dsParam);
                flag = true;
            }
            return flag;
        }

        public bool IsExist(string parameterName, string parameterValue)
        {
            string strSql = string.Format("select count(*) count  from sys_systemparameter where ParameterName='{0}' and ParameterValue='{1}'", parameterName, parameterValue);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                int count = Convert.ToInt32(paraDao.GetData(strSql).Tables[0].Rows[0][0].ToString());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsExist(string ParameterName, string ParameterValue, int recid)
        {
            string strSql = string.Format("select count(*) count  from sys_systemparameter where ParameterName='{0}' and ParameterValue='{1}' and SystemParameterID<>{2}", ParameterName, ParameterValue, recid);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSystemParameterDao paraDao = new SysSystemParameterDao();
                int count = Convert.ToInt32(paraDao.GetData(strSql).Tables[0].Rows[0][0].ToString());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UpdateSysPar(Hashtable htValue)
        {
            try
            {
                string strUpdate = "";
                foreach (DictionaryEntry de in htValue)
                {
                    string strKey = de.Key.ToString();
                    strUpdate += string.Format("update sys_SystemParameter set ParameterValue='{0}'  where ParameterName='{1}';", htValue[strKey].ToString(), strKey);
                }
                bool flag = false;
                using (PersistentManager persistentManager = new PersistentManager())
                {
                    SysSystemParameterDao paraDao = new SysSystemParameterDao();
                    paraDao.SetData(strUpdate);
                    flag = true;
                }
                return flag;
            }
            catch (Exception exp)
            {
                return false;
                //throw exp;
            }
        }
    }
}
