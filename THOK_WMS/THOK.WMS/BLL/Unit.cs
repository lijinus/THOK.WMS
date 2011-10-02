using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
    public class Unit
    {
        private string strTableView = "WMS_UNIT";
        private string strPrimaryKey = "UNITCODE";
        private string strQueryFields = "*";
        public DataSet QueryUnit(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UnitDao dao = new UnitDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public string GetNewUnitCode()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UnitDao dao = new UnitDao();
                return dao.GetNewUnitCode();
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UnitDao dao = new UnitDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UnitDao dao = new UnitDao();

                string sql = string.Format("Insert into WMS_UNIT (UNITCODE,UNITNAME,ISDEFAULT,ISACTIVE,STANDARDRATE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}')"
                                             , this.UNITCODE,
                            this.UNITNAME,
                            this.ISDEFAULT,
                            this.ISACTIVE,
                            this.STANDARDRATE,
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
                UnitDao dao = new UnitDao();

                string sql = string.Format("update WMS_UNIT set UNITCODE='{1}',UNITNAME='{2}',ISDEFAULT='{3}',ISACTIVE='{4}',STANDARDRATE='{5}',MEMO='{6}'  where ID='{0}'"
                                             , this.ID,
                            this.UNITCODE,
                            this.UNITNAME,
                            this.ISDEFAULT,
                            this.ISACTIVE,
                            this.STANDARDRATE,
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
                UnitDao dao = new UnitDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }


        #region property
        private int _id;
        private string _unitcode;
        private string _unitname;
        private string _isdefault;
        private string _isactive;
        private double _standardrate;
        private string _memo;


        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string UNITCODE
        {
            get
            {
                return _unitcode;
            }
            set
            {
                _unitcode = value;
            }
        }

        public string UNITNAME
        {
            get
            {
                return _unitname;
            }
            set
            {
                _unitname = value;
            }
        }

        public string ISDEFAULT
        {
            get
            {
                return _isdefault;
            }
            set
            {
                _isdefault = value;
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

        public double STANDARDRATE
        {
            get
            {
                return _standardrate;
            }
            set
            {
                _standardrate = value;
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
