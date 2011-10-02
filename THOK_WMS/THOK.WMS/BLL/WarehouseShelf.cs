using System;
using System.Collections.Generic;
using System.Text;
using THOK.WMS.Dao;
using System.Data;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class WarehouseShelf
    {
        private string strTableView = "WMS_WH_SHELF";
        //private string strPrimaryKey = "";
        //private string strOrderByFields = "ExceptionalLogID ASC";
        //private string strQueryFields = "";
        public DataSet QueryAllShelf()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseShelfDao dao = new WarehouseShelfDao();
                string sql = string.Format("SELECT * FROM WMS_WH_SHELF  ORDER BY AREACODE,SHELFCODE");
                return dao.GetData(sql);
            }
        }

        public DataSet QueryShelfByWHCODE(string whcode)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseShelfDao dao = new WarehouseShelfDao();
                string sql = string.Format("SELECT * FROM WMS_WH_SHELF where WH_CODE='{0}'  ORDER BY AREACODE,SHELFCODE",whcode);
                return dao.GetData(sql);
            }
        }

        public DataSet QueryShelfByAreaCode(string AreaCode)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseShelfDao dao = new WarehouseShelfDao();
                string sql = string.Format("SELECT * FROM WMS_WH_SHELF WHERE AREACODE='{0}'  ORDER BY AREACODE,SHELFCODE",AreaCode);
                return dao.GetData(sql);
            }
        }

        public DataSet QueryShelfByID(int ShelfID)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseShelfDao dao = new WarehouseShelfDao();
                string sql = string.Format("SELECT * FROM WMS_WH_SHELF WHERE SHELF_ID='{0}'  ORDER BY AREACODE,SHELFCODE", ShelfID);
                return dao.GetData(sql);
            }
        }

        //public DataSet QueryWarehouseShelf(string filter)
        //{
        //    using (PersistentManager persistentManager = new PersistentManager())
        //    {
        //        WarehouseShelfDao dao = new WarehouseShelfDao();
        //        string sql = string.Format("SELECT * FROM WMS_WH_SHELF where {0}  ORDER BY AREACODE,SHELFCODE",filter);
        //        return dao.GetData(sql);
        //    }
        //}

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseShelfDao dao = new WarehouseShelfDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public string GetNewShelfCode(string AreaCode)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseShelfDao dao = new WarehouseShelfDao();
                return dao.GetNewShelfCode(AreaCode);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseShelfDao dao = new WarehouseShelfDao();

                string sql = string.Format("Insert into WMS_WH_SHELF (WH_CODE,AREACODE,SHELFCODE,SHELFNAME,CELLROWS,CELLCOLS,IMG_X,IMG_Y,ISACTIVE,MEMO,AREATYPE) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')"
                                             ,this.WH_CODE,
                            this.AREACODE,
                            this.SHELFCODE,
                            this.SHELFNAME,
                            this.CELLROWS,
                            this.CELLCOLS,
                            this.IMG_X,
                            this.IMG_Y,
                            this.ISACTIVE,
                            this.MEMO,
                            this.AREATYPE);

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
                WarehouseShelfDao dao = new WarehouseShelfDao();

                string sql = string.Format("update WMS_WH_SHELF set WH_CODE='{1}',AREACODE='{2}',SHELFCODE='{3}',SHELFNAME='{4}',CELLROWS='{5}',CELLCOLS='{6}',IMG_X='{7}',IMG_Y='{8}',ISACTIVE='{9}',MEMO='{10}',AREATYPE='{11}'  where SHELF_ID='{0}'"
                                             , this.SHELF_ID,
                            this.WH_CODE,
                            this.AREACODE,
                            this.SHELFCODE,
                            this.SHELFNAME,
                            this.CELLROWS,
                            this.CELLCOLS,
                            this.IMG_X,
                            this.IMG_Y,
                            this.ISACTIVE,
                            this.MEMO,
                            this.AREATYPE);

                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool Delete(int ShelfID)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseShelfDao dao = new WarehouseShelfDao();
                string sql = string.Format("delete from WMS_WH_SHELF WHERE SHELF_ID={0}", ShelfID);
                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }


        #region property
        private int _shelf_id;
        private string _wh_code;
        private string _area_code;
        private string _shelfcode;
        private string _shelfname;
        private int _cellrows;
        private int _cellcols;
        private double _img_x;
        private double _img_y;
        private string _isactive;
        private string _memo;
        private string _areatype;

        public int SHELF_ID
        {
            get
            {
                return _shelf_id;
            }
            set
            {
                _shelf_id = value;
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

        public string AREACODE
        {
            get
            {
                return _area_code;
            }
            set
            {
                _area_code = value;
            }
        }


        public string SHELFCODE
        {
            get
            {
                return _shelfcode;
            }
            set
            {
                _shelfcode = value;
            }
        }

        public string SHELFNAME
        {
            get
            {
                return _shelfname;
            }
            set
            {
                _shelfname = value;
            }
        }

        public int CELLROWS
        {
            get
            {
                return _cellrows;
            }
            set
            {
                _cellrows = value;
            }
        }

        public int CELLCOLS
        {
            get
            {
                return _cellcols;
            }
            set
            {
                _cellcols = value;
            }
        }

        public double IMG_X
        {
            get
            {
                return _img_x;
            }
            set
            {
                _img_x = value;
            }
        }

        public double IMG_Y
        {
            get
            {
                return _img_y;
            }
            set
            {
                _img_y = value;
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
