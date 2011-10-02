using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
    public class RemoteInterfaceDal
    {
        #region DWV_IORG_PERSON±í
        public void SaveDWV_IORG_PERSON(string userId)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                RemoteInterfaceDao dao = new RemoteInterfaceDao();
                dao.SetPersistentManager(persistentManager);
                DataTable tempTable = dao.GetUserInfo(userId);
                if (tempTable.Rows.Count == 0)
                {
                    return;
                }
                Dictionary<string, string> values = new Dictionary<string, string>();
                values["PERSON_CODE"] = tempTable.Rows[0]["EMPLOYEECODE"].ToString();
                values["PERSON_N"] = tempTable.Rows[0]["EMPLOYEECODE"].ToString();
                values["PERSON_NAME"] = tempTable.Rows[0]["EMPLOYEENAME"].ToString();
                values["PWD"] = tempTable.Rows[0]["UserPassword"].ToString();
                values["SEX"] = tempTable.Rows[0]["SEX"].ToString();
                values["SUPER_ADMIN"] = "0";
                values["SYSTEM_ADMIN"] = "0";
                values["UPDATE_DATE"] = DateTime.Now.ToString("yy/MM/dd HH:mm");


                if (dao.GetDWV_IORG_PERSON(userId).Rows.Count == 0)
                {

                    dao.InsertDWV_IORG_PERSON(values);
                    
                }
                else
                {
                    dao.UpdateDWV_IORG_PERSON(values);
                }

            }
        
        }


        public void DeleteDWV_IORG_PERSON(string userId)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                RemoteInterfaceDao dao = new RemoteInterfaceDao();
                dao.SetPersistentManager(persistentManager);
                dao.DeleteDWV_IORG_PERSON(userId);
            }
        }


        #endregion


    }
}
