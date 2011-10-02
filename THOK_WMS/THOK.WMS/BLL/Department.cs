using System;
using System.Collections.Generic;
using System.Text;
using THOK.WMS.Dao;
using System.Data;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class Department
    {
        private string strTableView = "V_BI_DEPARTMENT";
        private string strPrimaryKey = "DEPTCODE";
        private string strQueryFields = "*";
        public DataSet QueryDepartment(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DepartmentDao dao = new DepartmentDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public string GetNewDeptCode()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DepartmentDao dao = new DepartmentDao();
                return dao.GetNewDeptCode();
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DepartmentDao dao = new DepartmentDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DepartmentDao dao = new DepartmentDao();

                string sql = string.Format("Insert into BI_DEPARTMENT (DEPTCODE,DEPTNAME,DEPTLEADER,ISACTIVE,WARECODE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}')"
                                             , this.DEPTCODE,
                            this.DEPTNAME,
                            this.DEPTLEADER,
                            this.ISACTIVE,
                            this.WARECODE,
                            this.MEMO);

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
                DepartmentDao dao = new DepartmentDao();

                string sql = string.Format("update BI_DEPARTMENT set DEPTNAME='{1}',DEPTLEADER='{2}',ISACTIVE='{3}',WARECODE='{4}',MEMO='{5}'  where DEPTCODE='{0}'"
                                             , this.DEPTCODE,
                            this.DEPTNAME,
                            this.DEPTLEADER,
                            this.ISACTIVE,
                            this.WARECODE,
                            this.MEMO);

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
                DepartmentDao dao = new DepartmentDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        public bool Delete(string DeptCode)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DepartmentDao dao = new DepartmentDao();
                dao.SetData("delete BI_DEPARTMENT WHERE DEPTCODE='" + DeptCode + "'");
                flag = true;
            }
            return flag;
        }

        #region property
        private string _deptcode;
        private string _deptname;
        private string _deptleader;
        private string _isactive;
        private string _warecode;
        private string _memo;


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

        public string DEPTNAME
        {
            get
            {
                return _deptname;
            }
            set
            {
                _deptname = value;
            }
        }

        public string DEPTLEADER
        {
            get
            {
                return _deptleader;
            }
            set
            {
                _deptleader = value;
            }
        }

        public string ISACTIVE
        {
            get
            {
                return _isactive;
            }
            set
            {
                _isactive = value;
            }
        }

        public string WARECODE
        {
            get
            {
                return _warecode;
            }
            set
            {
                _warecode = value;
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
