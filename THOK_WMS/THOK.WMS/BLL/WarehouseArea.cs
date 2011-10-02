using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.WMS.Dao;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class WarehouseArea
    {
        private string strTableView = "WMS_WH_AREA";
        //private string strPrimaryKey = "AREA_ID";
        //private string strOrderByFields = "ExceptionalLogID ASC";
        //private string strQueryFields = "*";
        public DataSet QueryAllArea()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseAreaDao dao = new WarehouseAreaDao();
                string sql = string.Format("SELECT * FROM WMS_WH_AREA ORDER BY WH_CODE,AREACODE");
                return dao.GetData(sql);
            }
        }
        //获取新的库区类型
        public DataSet QueryNewAreaType(string wh_code)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseAreaDao dao = new WarehouseAreaDao();
                string sql = string.Format("SELECT MAX(AREATYPE+1) FROM WMS_WH_AREA where WH_CODE={0}", wh_code);
                return dao.GetData(sql);
            }
        }
        public DataSet QueryAreaByWHCODE(string WhCode)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseAreaDao dao = new WarehouseAreaDao();
                string sql = string.Format("SELECT * FROM WMS_WH_AREA where WH_CODE='{0}' ORDER BY AREACODE",WhCode);
                return dao.GetData(sql);
            }
        }

        public DataSet QueryAreaByCode(string AreaCode)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseAreaDao dao = new WarehouseAreaDao();
                string sql = string.Format("SELECT * FROM WMS_WH_AREA where AREACODE='{0}'", AreaCode);
                return dao.GetData(sql);
            }
        }

        public DataSet QueryAreaByID(int AreaID)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseAreaDao dao = new WarehouseAreaDao();
                string sql = string.Format("SELECT * FROM WMS_WH_AREA where AREA_ID='{0}'", AreaID);
                return dao.GetData(sql);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseAreaDao dao = new WarehouseAreaDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public string GetNewAreaCode(string whcode)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseAreaDao dao = new WarehouseAreaDao();
                return dao.GetNewAreaCode(whcode);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseAreaDao dao = new WarehouseAreaDao();

                string sql = string.Format("Insert into WMS_WH_AREA (WH_CODE,AREACODE,AREANAME,SHORTNAME,ISACTIVE,AREATYPE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                                             , this.WH_CODE,
                            this.AREACODE,
                            this.AREANAME,
                            this.SHORTNAME,
                            this.ISACTIVE,
                            this.AREATYPE,
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
                WarehouseAreaDao dao = new WarehouseAreaDao();

                string sql = string.Format("update WMS_WH_AREA set WH_CODE='{0}',AREACODE='{1}',AREANAME='{2}',SHORTNAME='{3}',ISACTIVE='{4}',MEMO='{5}',AREATYPE='{6}'  where AREA_ID='{7}'"
                                             , this.WH_CODE,
                            this.AREACODE,
                            this.AREANAME,
                            this.SHORTNAME,
                            this.ISACTIVE,
                            this.MEMO,
                            this.AREATYPE,
                            this.AREA_ID);

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
                WarehouseAreaDao dao = new WarehouseAreaDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        public bool Delete(int AreaID)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseAreaDao dao = new WarehouseAreaDao();
                string sql = string.Format("delete from WMS_WH_AREA WHERE AREA_ID={0}",AreaID);
                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        #region 中烟接口数据

        //public bool InsertStorage()
        //{
        //    bool flag = false;
        //    using (PersistentManager dbpm = new PersistentManager())
        //    {
        //        WarehouseDao dao = new WarehouseDao();
        //        string sql = string.Format("Insert into DWV_IBAS_STORAGE (STORAGE_CODE,STORAGE_TYPE,CONTAINER,STORAGE_NAME,UP_CODE,DIST_CTR_CODE,AREA_TYPE,UPDATE_DATE,ISACTIVE,IS_IMPORT) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')"
        //                                     , this.AREACODE,
        //                    2,
        //                    5001,
        //                    this.AREANAME,
        //                    this.WH_CODE,
        //                    001,                            
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
        //    using (PersistentManager pm = new PersistentManager())
        //    {
        //        WarehouseDao dao = new WarehouseDao();
        //        string sql = string.Format("update DWV_IBAS_STORAGE set STORAGE_TYPE='{1}',CONTAINER='{2}',STORAGE_NAME='{3}',UP_CODE='{4}',DIST_CTR_CODE='{5}',AREA_TYPE='{6}',UPDATE_DATE='{7}',ISACTIVE='{8}',IS_IMPORT='0' where STORAGE_CODE='{0}'"
        //            ,)
        //    }
        //}
        #endregion


        #region property
        private int _areaid;
        private string _wh_code;
        private string _areacode;
        private string _areaname;
        private string _shortname;
        private string _isactive;
        private string _memo;
        private string _areatype;

        public int AREA_ID
        {
            get { return _areaid; }
            set { _areaid = value; }
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

        public string AREACODE
        {
            get
            {
                return _areacode;
            }
            set
            {
                _areacode = value;
            }
        }

        public string AREANAME
        {
            get
            {
                return _areaname;
            }
            set
            {
                _areaname = value;
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

        public string AREATYPE
        {
            get
            {
                return _areatype;
            }
            set
            {
                _areatype = value;
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
