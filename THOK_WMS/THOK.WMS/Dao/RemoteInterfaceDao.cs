using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Dao
{
    public class RemoteInterfaceDao : BaseDao
    {
        #region DWV_IORG_PERSON±í
        public void InsertDWV_IORG_PERSON(Dictionary<string,string> prameters)
        {
            string sql = string.Format("INSERT INTO DWV_IORG_PERSON VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','1','0')"
                        , prameters["PERSON_CODE"], prameters["PERSON_N"], prameters["PERSON_NAME"], prameters["PWD"], prameters["SEX"], prameters["SUPER_ADMIN"], prameters["SYSTEM_ADMIN"]
                        , prameters["UPDATE_DATE"]);
            this.ExecuteNonQuery(sql);
        }


        public DataTable GetUserInfo(string userId)
        {
            string sql = string.Format("SELECT * FROM BI_EMPLOYEE A LEFT JOIN sys_UserList B ON A.EMPLOYEECODE = B.EmployeeCode WHERE A.EMPLOYEECODE = '{0}'"
                                        ,userId);
            return this.ExecuteQuery(sql).Tables[0];
        }


        public DataTable GetDWV_IORG_PERSON(string userId)
        {
            string sql = string.Format("SELECT * FROM DWV_IORG_PERSON WHERE  PERSON_N = '{0}' ", userId);
            return this.ExecuteQuery(sql).Tables[0];
        }


        public void UpdateDWV_IORG_PERSON(Dictionary<string, string> prameters)
        {
            string sql = string.Format("UPDATE DWV_IORG_PERSON SET PERSON_NAME='{0}',PWD='{1}',SEX='{2}',UPDATE_DATE='{3}',IS_IMPORT='0' WHERE PERSON_N='{4}'"
                                    , prameters["PERSON_NAME"], prameters["PWD"], prameters["SEX"], prameters["UPDATE_DATE"], prameters["PERSON_N"]);
            this.ExecuteNonQuery(sql);
        }

        public void DeleteDWV_IORG_PERSON(string userId)
        {
           // string sql = string.Format("DELETE  FROM DWV_IORG_PERSON WHERE PERSON_N IN ({0}) ", userId);
            string sql = string.Format("UPDATE DWV_IORG_PERSON SET ISACTIVE='0',IS_IMPORT='0' WHERE PERSON_N IN ({0}) ", userId);
            this.ExecuteNonQuery(sql);
        }
        #endregion




    }
}

