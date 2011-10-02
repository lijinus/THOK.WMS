using System;
using System.Collections.Generic;
using System.Text;
using THOK.WMS.Dao;
using System.Data;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class Employee
    {
        private string strTableView = "V_BI_EMPLOYEE";
        private string strPrimaryKey = "EMPLOYEECODE";
        private string strQueryFields = "*";
        public DataSet QueryEmployee(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EmployeeDao dao = new EmployeeDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public string GetNewEmployeeCode()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EmployeeDao dao = new EmployeeDao();
                return dao.GetNewEmployeeCode();
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EmployeeDao dao = new EmployeeDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EmployeeDao dao = new EmployeeDao();

                string sql = string.Format("Insert into BI_EMPLOYEE (EMPLOYEECODE,EMPLOYEENAME,SEX,DEPTCODE,POSITION,STATUS,MEMO,TEL) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')"
                                             , this.EMPLOYEECODE,
                            this.EMPLOYEENAME,
                            this.SEX,
                            this.DEPTCODE,
                            this.POSITION,
                            this.STATUS,
                            this.MEMO,
                            this.Tel);

                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool Update()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EmployeeDao dao = new EmployeeDao();

                string sql = string.Format("update BI_EMPLOYEE set EMPLOYEENAME='{1}',SEX='{2}',DEPTCODE='{3}',POSITION='{4}',STATUS='{5}',MEMO='{6}',TEL='{7}'  where EMPLOYEECODE='{0}'"
                                             , this.EMPLOYEECODE,
                            this.EMPLOYEENAME,
                            this.SEX,
                            this.DEPTCODE,
                            this.POSITION,
                            this.STATUS,
                            this.MEMO,this.Tel);

                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool Delete(DataSet dataSet)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EmployeeDao dao = new EmployeeDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }


        #region property
        private string _employeecode;
        private string _employeename;
        private string _sex;
        private string _deptcode;
        private string _position;
        private string _status;
        private string _memo;
        private string _tel;

        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }

        public string EMPLOYEECODE
        {
            get
            {
                return _employeecode;
            }
            set
            {
                _employeecode = value;
            }
        }

        public string EMPLOYEENAME
        {
            get
            {
                return _employeename;
            }
            set
            {
                _employeename = value;
            }
        }

        public string SEX
        {
            get
            {
                return _sex;
            }
            set
            {
                _sex = value;
            }
        }

        public string DEPTCODE
        {
            get
            {
                return _deptcode;
            }
            set
            {
                _deptcode = value;
            }
        }

        public string POSITION
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public string STATUS
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        public string MEMO
        {
            get
            {
                return _memo;
            }
            set
            {
                _memo = value;
            }
        }
        #endregion
    }
}
