using System;
using System.Collections.Generic;
using System.Text;
using THOK.WMS.Dao;
using System.Data;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class Supplier
    {
        private string strTableView = "BI_SUPPLIER";
        private string strPrimaryKey = "SUPPLIERCODE";
        private string strQueryFields = "*";

        public DataSet QuerySupplier(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SupplierDao dao = new SupplierDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public string GetNewSupplierCode()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SupplierDao dao = new SupplierDao();
                return dao.GetNewSupplierCode();
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SupplierDao dao = new SupplierDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SupplierDao dao = new SupplierDao();

                string sql = string.Format("Insert into BI_SUPPLIER (SUPPLIERCODE,SUPPLIERNAME,TEL,FAX,CONTECTPERSON,ADDRESS,ZIP,BANKACCOUNT,BANKNAME,TAXNO,CREDITGRADE,ISACTIVE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')"
                                             , this.SUPPLIERCODE,
                            this.SUPPLIERNAME,
                            this.TEL,
                            this.FAX,
                            this.CONTECTPERSON,
                            this.ADDRESS,
                            this.ZIP,
                            this.BANKACCOUNT,
                            this.BANKNAME,
                            this.TAXNO,
                            this.CREDITGRADE,
                            this.ISACTIVE,
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
                SupplierDao dao = new SupplierDao();

                string sql = string.Format("update BI_SUPPLIER set SUPPLIERNAME='{1}',TEL='{2}',FAX='{3}',CONTECTPERSON='{4}',ADDRESS='{5}',ZIP='{6}',BANKACCOUNT='{7}',BANKNAME='{8}',TAXNO='{9}',CREDITGRADE='{10}',ISACTIVE='{11}',MEMO='{12}'  where SUPPLIERCODE='{0}'"
                                             , this.SUPPLIERCODE,
                            this.SUPPLIERNAME,
                            this.TEL,
                            this.FAX,
                            this.CONTECTPERSON,
                            this.ADDRESS,
                            this.ZIP,
                            this.BANKACCOUNT,
                            this.BANKNAME,
                            this.TAXNO,
                            this.CREDITGRADE,
                            this.ISACTIVE,
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
                SupplierDao dao = new SupplierDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        #region property
        private string _suppliercode;
        private string _suppliername;
        private string _tel;
        private string _fax;
        private string _contectperson;
        private string _address;
        private string _zip;
        private string _bankaccount;
        private string _bankname;
        private string _taxno;
        private string _creditgrade;
        private string _isactive;
        private string _memo;


        public string SUPPLIERCODE
        {
            get
            {
                return _suppliercode;
            }
            set
            {
                _suppliercode = value;
            }
        }

        public string SUPPLIERNAME
        {
            get
            {
                return _suppliername;
            }
            set
            {
                _suppliername = value;
            }
        }

        public string TEL
        {
            get
            {
                return _tel;
            }
            set
            {
                _tel = value;
            }
        }

        public string FAX
        {
            get
            {
                return _fax;
            }
            set
            {
                _fax = value;
            }
        }

        public string CONTECTPERSON
        {
            get
            {
                return _contectperson;
            }
            set
            {
                _contectperson = value;
            }
        }

        public string ADDRESS
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        public string ZIP
        {
            get
            {
                return _zip;
            }
            set
            {
                _zip = value;
            }
        }

        public string BANKACCOUNT
        {
            get
            {
                return _bankaccount;
            }
            set
            {
                _bankaccount = value;
            }
        }

        public string BANKNAME
        {
            get
            {
                return _bankname;
            }
            set
            {
                _bankname = value;
            }
        }

        public string TAXNO
        {
            get
            {
                return _taxno;
            }
            set
            {
                _taxno = value;
            }
        }

        public string CREDITGRADE
        {
            get
            {
                return _creditgrade;
            }
            set
            {
                _creditgrade = value;
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
