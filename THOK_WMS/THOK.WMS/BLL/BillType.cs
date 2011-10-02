using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.WMS.Dao;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class BillType
    {
        private string strTableView = "WMS_BILLTYPE";
        private string strPrimaryKey = "TYPECODE";
        private string strQueryFields = "*";
        public DataSet QueryBillType(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                BillTypeDao dao =new BillTypeDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                BillTypeDao dao = new BillTypeDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public string GetNewTypeCode(string businessType)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                BillTypeDao dao = new BillTypeDao();
                return dao.GetNewTypeCode(businessType);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                BillTypeDao dao = new BillTypeDao();

                string sql = string.Format("Insert into WMS_BILLTYPE (TYPECODE,TYPENAME,BUSINESS,ISNEEDCELL,MEMO) values('{0}','{1}','{2}','{3}','{4}')"
                                             , this.TYPECODE,
                            this.TYPENAME,
                            this.BUSINESS,
                            this.ISNEEDCELL,
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
                BillTypeDao dao = new BillTypeDao();

                string sql = string.Format("update WMS_BILLTYPE set TYPECODE='{1}',TYPENAME='{2}',BUSINESS='{3}',ISNEEDCELL='{4}',MEMO='{5}'  where ID='{0}'"
                                             , this.ID,
                            this.TYPECODE,
                            this.TYPENAME,
                            this.BUSINESS,
                            this.ISNEEDCELL,
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
                BillTypeDao dao = new BillTypeDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        #region property
        private int _id;
        private string _typecode;
        private string _typename;
        private string _business;
        private string _isneedcell;
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

        public string TYPECODE
        {
            get
            {
                return _typecode;
            }
            set
            {
                _typecode = value;
            }
        }

        public string TYPENAME
        {
            get
            {
                return _typename;
            }
            set
            {
                _typename = value;
            }
        }

        public string BUSINESS
        {
            get
            {
                return _business;
            }
            set
            {
                _business = value;
            }
        }

        public string ISNEEDCELL
        {
            get
            {
                return _isneedcell;
            }
            set
            {
                _isneedcell = value;
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
