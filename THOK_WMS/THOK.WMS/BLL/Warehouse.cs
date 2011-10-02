using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.WMS.Dao;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class Warehouse
    {
        private string strTableView = "V_WMS_WAREHOUSE";
        //private string strPrimaryKey = "WH_ID";
        //private string strOrderByFields = "ExceptionalLogID ASC";
        //private string strQueryFields = "*";

        public DataSet QueryAllWarehouse()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseDao dao = new WarehouseDao();
                string sql = string.Format("SELECT * FROM V_WMS_WAREHOUSE order by wh_code");
                return dao.GetData(sql);
            }
        }

        public DataSet QueryWarehouseByCode(string wh_code)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseDao dao = new WarehouseDao();
                string sql = string.Format("SELECT * FROM V_WMS_WAREHOUSE where  wh_code='{0}'",wh_code);
                return dao.GetData(sql);
            }
        }

        public string GetNewCode(string type)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseDao dao = new WarehouseDao();
                return dao.GetNewCode(type);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseDao dao = new WarehouseDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseDao dao = new WarehouseDao();

                string sql = string.Format("Insert into WMS_WAREHOUSE (WH_CODE,WH_NAME,SHORTNAME,DEFAULTUNIT,WH_TYPE,WH_AREA,CITYCODE,CAPACITY,WH_AMOUNT,SORTLINE,ISACTIVE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')"
                                             , this.WH_CODE,
                            this.WH_NAME,
                            this.SHORTNAME,
                            this.DEFAULTUNIT,
                            this.WH_TYPE,
                            this.WH_AREA,
                            this.CITYCODE,
                            this.CAPACITY,
                            this.WH_AMOUNT,
                            this.SORTLINE,
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
                WarehouseDao dao = new WarehouseDao();

                string sql = string.Format("update WMS_WAREHOUSE set WH_CODE='{1}',WH_NAME='{2}',SHORTNAME='{3}',DEFAULTUNIT='{4}',WH_TYPE='{5}',WH_AREA='{6}',CITYCODE='{7}',CAPACITY='{8}',WH_AMOUNT='{9}',SORTLINE='{10}',ISACTIVE='{11}',MEMO='{12}'  where WH_ID='{0}'"
                                             , this.WH_ID,
                            this.WH_CODE,
                            this.WH_NAME,
                            this.SHORTNAME,
                            this.DEFAULTUNIT,
                            this.WH_TYPE,
                            this.WH_AREA,
                            this.CITYCODE,
                            this.CAPACITY,
                            this.WH_AMOUNT,
                            this.SORTLINE,
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
                WarehouseDao dao = new WarehouseDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        public bool Delete(string wh_code)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseDao dao = new WarehouseDao();
                string sql=string.Format("DELETE FROM WMS_WAREHOUSE WHERE WH_CODE='{0}'",wh_code);
                dao.SetData(sql); 
                flag = true;
            }
            return flag;
        }

        #region 中烟接口表操作

        //public bool InsertStorage()
        //{
        //    bool flag = false;
        //    using (PersistentManager dbpm = new PersistentManager())
        //    {
        //        WarehouseDao dao = new WarehouseDao();
        //        string sql = string.Format("Insert into DWV_IBAS_STORAGE (STORAGE_CODE,STORAGE_TYPE,CONTAINER,STORAGE_NAME,UP_CODE,DIST_CTR_CODE,CAPACITY,AREA_TYPE,UPDATE_DATE,ISACTIVE,IS_IMPORT) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')"
        //                                     , this.WH_CODE,
        //                    1,
        //                    5001,
        //                    this.SHORTNAME,
        //                    001,
        //                    001,
        //                    this.CAPACITY,
        //                    0901,
        //                    DateTime.Now.ToString(),
        //                    this.ISACTIVE,
        //                    0);

        //        dao.SetData(sql);
        //        flag = true;
        //    }
        //    return flag;
        //}

        //public bool UpdateStorage()
        //{
        //    bool flag = false;
        //    using (PersistentManager dbpm = new PersistentManager())
        //    {
        //        WarehouseDao dao = new WarehouseDao();
        //        string sql = string.Format("update DWV_IBAS_STORAGE set STORAGE_TYPE='{1}',CONTAINER='{2}',STORAGE_NAME='{3}',UP_CODE='{4}',DIST_CTR_CODE='{5}',CAPACITY='{6}',AREA_TYPE='{7}',UPDATE_DATE='{8}',ISACTIVE='{9}',IS_IMPORT='{10}' where STORAGE_CODE='{0}'"
        //                                     , this.WH_CODE,
        //                    1,
        //                    5001,
        //                    this.SHORTNAME,
        //                    001,
        //                    001,
        //                    this.CAPACITY,
        //                    0901,
        //                    DateTime.Now.ToString(),
        //                    this.ISACTIVE,
        //                    0);

        //        dao.SetData(sql);
        //        flag = true;
        //    }
        //    return flag;
        //}

        #endregion

        #region property
        private int _wh_id;
        private string _wh_code;
        private string _wh_name;
        private string _shortname;
        private string _defaultunit;
        private string _wh_type;
        private decimal _wh_area;
        private string _citycode;
        private int _capacity;
        private int _wh_amount;
        private int _sortline;
        private string _isactive;
        private string _memo;


        public int WH_ID
        {
            get
            {
                return _wh_id;
            }
            set
            {
                _wh_id = value;
            }
        }

        public string WH_CODE
        {
            get
            {
                return _wh_code;
            }
            set
            {
                _wh_code = value;
            }
        }

        public string WH_NAME
        {
            get
            {
                return _wh_name;
            }
            set
            {
                _wh_name = value;
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

        public string DEFAULTUNIT
        {
            get
            {
                return _defaultunit;
            }
            set
            {
                _defaultunit = value;
            }
        }

        public string WH_TYPE
        {
            get
            {
                return _wh_type;
            }
            set
            {
                _wh_type = value;
            }
        }

        public decimal WH_AREA
        {
            get
            {
                return _wh_area;
            }
            set
            {
                _wh_area = value;
            }
        }

        public string CITYCODE
        {
            get
            {
                return _citycode;
            }
            set
            {
                _citycode = value;
            }
        }

        public int CAPACITY
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

        public int WH_AMOUNT
        {
            get
            {
                return _wh_amount;
            }
            set
            {
                _wh_amount = value;
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
