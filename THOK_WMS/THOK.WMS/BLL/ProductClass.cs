using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
    public class ProductClass
    {
        private string strTableView = "WMS_PRODUCTCLASS";
        private string strPrimaryKey = "CLASSCODE";
        private string strQueryFields = "*";
        public DataSet QueryProductClass(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProductClassDao dao = new ProductClassDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProductClassDao dao = new ProductClassDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public string GetNewClassCode()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProductClassDao dao = new ProductClassDao();
                return dao.GetNewClassCode();
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProductClassDao dao = new ProductClassDao();

                string sql = string.Format("Insert into WMS_PRODUCTCLASS (CLASSCODE,CLASSNAME,MEMO) values('{0}','{1}','{2}')"
                                             , this.CLASSCODE,
                            this.CLASSNAME,
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
                ProductClassDao dao = new ProductClassDao();

                string sql = string.Format("update WMS_PRODUCTCLASS set CLASSCODE='{1}',CLASSNAME='{2}',MEMO='{3}'  where ID='{0}'"
                                             , this.ID,
                            this.CLASSCODE,
                            this.CLASSNAME,
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
                ProductClassDao dao = new ProductClassDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }


        #region property
        private int _id;
        private string _classcode;
        private string _classname;
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

        public string CLASSCODE
        {
            get
            {
                return _classcode;
            }
            set
            {
                _classcode = value;
            }
        }

        public string CLASSNAME
        {
            get
            {
                return _classname;
            }
            set
            {
                _classname = value;
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
