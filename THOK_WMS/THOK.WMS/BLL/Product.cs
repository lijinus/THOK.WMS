using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
    public class Product
    {
        private string strTableView = "V_WMS_PRODUCT";
        private string strPrimaryKey = "PRODUCTCODE";
        private string strQueryFields = "*";
        public DataSet QueryProduct(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProductDao dao = new ProductDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProductDao dao = new ProductDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProductDao dao = new ProductDao();

                string sql = string.Format("Insert into WMS_PRODUCT (PRODUCTCODE,PRODUCTCLASS,PRODUCTNAME,SHORTNAME,SUPPLIERCODE,BARCODE,ABCODE,UNITCODE,MEMO,JIANCODE,TIAOCODE,MAXCELLPIECE) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')"
			    , this.PRODUCTCODE,
                            this.PRODUCTCLASS,
                            this.PRODUCTNAME,
                            this.SHORTNAME,
                            this.SUPPLIERCODE,
                            this.BARCODE,
                            this.ABCODE,
                            this.UNITCODE,
                            this.MEMO,
                            this.JIANCODE,
                            this.TIAOCODE,
                            this.MAXCELLPIECE);

                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        //插入中烟接口卷烟信息表（DWV_IINF_BRAND）
        public bool InsertBrand()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProductDao dao = new ProductDao();
                string sql = string.Format("insert into DWV_IINF_BRAND(BRAND_CODE,BRAND_NAME,SHORT_CODE,IS_IMPORT) values('{0}','{1}','{2}','{3}')",
                    this.PRODUCTCODE,
                    this.PRODUCTNAME,
                    this.ABCODE,
                    0);
                dao.GetData(sql);
                flag = true;
            }
            return flag;
        }

        //更新中烟接口卷烟信息表（DWV_IINF_BRAND）
        public bool UpdateBrand()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProductDao dao = new ProductDao();
                string sql = string.Format("update DWV_IINF_BRAND set BRAND_CODE='{0}',BRAND_NAME='{1}',SHORT_CODE='{2}',IS_IMPORT='{3}')",
                    this.PRODUCTCODE,
                    this.PRODUCTNAME,
                    this.ABCODE,
                    0);
                dao.GetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool Update()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProductDao dao = new ProductDao();

                string sql = string.Format("update WMS_PRODUCT set PRODUCTCLASS='{1}',PRODUCTNAME='{2}',SHORTNAME='{3}',SUPPLIERCODE='{4}',BARCODE='{5}',ABCODE='{6}',UNITCODE='{7}',MEMO='{8}',JIANCODE='{9}',TIAOCODE='{10}',MAXCELLPIECE='{11}'  where PRODUCTCODE='{0}'"
                            , this.PRODUCTCODE,
                            this.PRODUCTCLASS,
                            this.PRODUCTNAME,
                            this.SHORTNAME,
                            this.SUPPLIERCODE,
                            this.BARCODE,
                            this.ABCODE,
                            this.UNITCODE,
                            this.MEMO,
                             this.JIANCODE,
                            this.TIAOCODE,
                            this.MAXCELLPIECE);

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
                ProductDao dao = new ProductDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        #region property
        private string _productcode;
        private string _productclass;
        private string _productname;
        private string _shortname;
        private string _suppliercode;
        private string _barcode;
        private string _abcode;
        private string _unitcode;
        private double _jiantiaorate;
        private double _tiaobaorate;
        private double _baozhirate;
        private string _memo;
        //
        private string _jiancode;
        private string _tiaocode;
        private double _maxcellpiece;

        public string PRODUCTCODE
        {
            get
            {
                return _productcode;
            }
            set
            {
                _productcode = value;
            }
        }

        public string PRODUCTCLASS
        {
            get
            {
                return _productclass;
            }
            set
            {
                _productclass = value;
            }
        }

        public string PRODUCTNAME
        {
            get
            {
                return _productname;
            }
            set
            {
                _productname = value;
            }
        }

        public string SHORTNAME
        {
            get
            {
                return _shortname;
            }
            set
            {
                _shortname = value;
            }
        }

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

        public string BARCODE
        {
            get
            {
                return _barcode;
            }
            set
            {
                _barcode = value;
            }
        }

        public string ABCODE
        {
            get
            {
                return _abcode;
            }
            set
            {
                _abcode = value;
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

        public double JIANTIAORATE
        {
            get
            {
                return _jiantiaorate;
            }
            set
            {
                _jiantiaorate = value;
            }
        }

        public double TIAOBAORATE
        {
            get
            {
                return _tiaobaorate;
            }
            set
            {
                _tiaobaorate = value;
            }
        }

        public double BAOZHIRATE
        {
            get
            {
                return _baozhirate;
            }
            set
            {
                _baozhirate = value;
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

        public string JIANCODE
        {
            get { return _jiancode; }
            set { _jiancode = value; }
        }

        public string TIAOCODE
        {
            get { return _tiaocode; }
            set { _tiaocode = value; }
        }

        public double MAXCELLPIECE
        {
            get { return _maxcellpiece; }
            set { _maxcellpiece = value; }
        }
        #endregion
    }
}
