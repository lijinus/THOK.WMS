using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
    public class Company
    {

        public DataSet GetCompanyInfo()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CompanyDao dao = new CompanyDao();
                string sql = string.Format("SELECT * FROM BI_COMPANY");
                return dao.GetData(sql);
            }
        }

        public DataTable GetDWV_IORG_ORGANIZATION()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CompanyDao dao = new CompanyDao();
                string sql = string.Format("SELECT * FROM DWV_IORG_ORGANIZATION");
                return dao.GetData(sql).Tables[0];
            }
        }



        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CompanyDao dao = new CompanyDao();

                string sql = string.Format("Insert into BI_COMPANY (COM_CODE,COM_NAME,COM_TYPE,UNIFIEDCODE,CAPACITY,SORTLINE,UPDATEDTIME) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                                             , this.COM_CODE,
                            this.COM_NAME,
                            this.COM_TYPE,
                            this.UNIFIEDCODE,
                            this.CAPACITY,
                            this.SORTLINE,
                            this.UPDATEDTIME);

                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }


        public void InsertDWV_IORG_ORGANIZATION(Dictionary<string, string> parameters)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CompanyDao dao = new CompanyDao();


                string sql = string.Format("INSERT INTO DWV_IORG_ORGANIZATION VALUES('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},'{9}','1','0')"
                        , parameters["ORGANIZATION_CODE"], parameters["ORGANIZATION_NAME"], parameters["ORGANIZATION_TYPE"], parameters["UP_CODE"], parameters["N_ORGANIZATION_CODE"]
                        , parameters["STORE_ROOM_AREA"], parameters["STORE_ROOM_NUM"], parameters["STORE_ROOM_CAPACITY"], parameters["SORTING_NUM"], DateTime.Now.ToString("yy/MM/dd HH:mm"));

                dao.SetData(sql);
            }
            
        }


        public void UpdateDWV_IORG_ORGANIZATION(Dictionary<string, string> parameters)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CompanyDao dao = new CompanyDao();


                string sql = string.Format("UPDATE DWV_IORG_ORGANIZATION SET ORGANIZATION_CODE='{0}',ORGANIZATION_NAME='{1}',ORGANIZATION_TYPE='{2}',UP_CODE='{3}',N_ORGANIZATION_CODE='{4}',STORE_ROOM_AREA={5},STORE_ROOM_NUM={6} "
                                    + " ,STORE_ROOM_CAPACITY={7},SORTING_NUM={8},UPDATE_DATE='{9}',ISACTIVE='1',IS_IMPORT='0'"
                                    , parameters["ORGANIZATION_CODE"], parameters["ORGANIZATION_NAME"], parameters["ORGANIZATION_TYPE"], parameters["UP_CODE"], parameters["N_ORGANIZATION_CODE"]
                                    , parameters["STORE_ROOM_AREA"], parameters["STORE_ROOM_NUM"], parameters["STORE_ROOM_CAPACITY"], parameters["SORTING_NUM"], DateTime.Now.ToString("yy/MM/dd HH:mm"));


                dao.SetData(sql);
            }
        }


        public bool Update()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CompanyDao dao = new CompanyDao();

                string sql = string.Format("update BI_COMPANY set COM_CODE='{0}',COM_NAME='{1}',COM_TYPE='{2}',UNIFIEDCODE='{3}',CAPACITY='{4}',SORTLINE='{5}',UPDATEDTIME='{6}'"
                                             , this.COM_CODE,
                            this.COM_NAME,
                            this.COM_TYPE,
                            this.UNIFIEDCODE,
                            this.CAPACITY,
                            this.SORTLINE,
                            this.UPDATEDTIME);

                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }


        #region property
        private string _com_code;
        private string _com_name;
        private string _com_type;
        private string _unifiedcode;
        private decimal _capacity;
        private int _sortline;
        private DateTime _updatedtime;


        public string COM_CODE
        {
            get
            {
                return _com_code;
            }
            set
            {
                _com_code = value;
            }
        }

        public string COM_NAME
        {
            get
            {
                return _com_name;
            }
            set
            {
                _com_name = value;
            }
        }

        public string COM_TYPE
        {
            get
            {
                return _com_type;
            }
            set
            {
                _com_type = value;
            }
        }

        public string UNIFIEDCODE
        {
            get
            {
                return _unifiedcode;
            }
            set
            {
                _unifiedcode = value;
            }
        }

        public decimal CAPACITY
        {
            get
            {
                return _capacity;
            }
            set
            {
                _capacity = value;
            }
        }

        public int SORTLINE
        {
            get
            {
                return _sortline;
            }
            set
            {
                _sortline = value;
            }
        }

        public DateTime UPDATEDTIME
        {
            get
            {
                return _updatedtime;
            }
            set
            {
                _updatedtime = value;
            }
        }
        #endregion
    }
}
