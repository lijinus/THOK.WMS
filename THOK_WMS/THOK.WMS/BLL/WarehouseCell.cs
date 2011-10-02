using System;
using System.Collections.Generic;
using System.Text;
using THOK.WMS.Dao;
using System.Data;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class WarehouseCell
    {
        private string strTableView = "V_WMS_WH_CELL";
        //private string strPrimaryKey = "";
        //private string strOrderByFields = "ExceptionalLogID ASC";
        //private string strQueryFields = "*";
        public DataSet QueryAllCell()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                string sql = "select * from V_WMS_WH_CELL ORDER BY AREACODE,SHELFCODE,CELLCODE";
                return dao.GetData(sql);
            }
        }

        public DataSet QueryWarehouseCell(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                string sql = string.Format("select * from V_WMS_WH_CELL WHERE {0} ORDER BY AREACODE,SHELFCODE,CELLCODE", filter);
                return dao.GetData(sql);
            }
        }
        public DataSet QueryWarehouseCell(string filter,int pageIndex,int pageSize)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                string sql = string.Format("select * from V_WMS_WH_CELL WHERE {0} ORDER BY AREACODE,SHELFCODE,CELLCODE", filter);
                return dao.Query(sql, pageIndex, pageSize);
            }
        }

        public string GetNewCellCode(string shelfcode)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                return dao.GetNewCellCode(shelfcode);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();

                string sql = string.Format("Insert into WMS_WH_CELL (SHELFCODE,CELLCODE,CELLNAME,ISACTIVE,MAX_QUANTITY,LAYER_NO,ASSIGNEDPRODUCT,PALLETID,ELECTRICGROUP,ELECTRICCOM,ELECTRICADDRESS,ISVIRTUAL,UNITCODE,AREATYPE) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')"
                                             ,this.SHELFCODE,
                            this.CELLCODE,
                            this.CELLNAME,
                            this.ISACTIVE,
                            this.MAX_QUANTITY,
                            this.LAYER_NO,
                            this.ASSIGNEDPRODUCT,
                           this.PALLETID, this.ELECTRICGROUP, this.ELECTRICCOM, this.ELECTRICADDRESS, this.ISVIRTUAL, this.UNITCODE, AREATYPE);

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
                WarehouseCellDao dao = new WarehouseCellDao();

                string sql = string.Format("update WMS_WH_CELL set SHELFCODE='{1}',CELLCODE='{2}',CELLNAME='{3}',ISACTIVE='{4}',MAX_QUANTITY='{5}',LAYER_NO='{6}',ASSIGNEDPRODUCT='{7}',PALLETID='{8}',ELECTRICGROUP='{9}',ELECTRICCOM='{10}',ELECTRICADDRESS='{11}',ISVIRTUAL='{12}',UNITCODE='{13}',AREATYPE='{14}' where CELL_ID='{0}'"
                                             , this.CELL_ID,
                            this.SHELFCODE,
                            this.CELLCODE,
                            this.CELLNAME,
                            this.ISACTIVE,
                            this.MAX_QUANTITY,
                            this.LAYER_NO,
                            this.ASSIGNEDPRODUCT,
                            this.PALLETID,this.ELECTRICGROUP,this.ELECTRICCOM,this.ELECTRICADDRESS,this.ISVIRTUAL,this.UNITCODE,this.AREATYPE);

                dao.SetData(sql);
                dao.SetData("update wms_wh_cell set  assignedproduct=null where assignedproduct=''");
                flag = true;
            }
            return flag;
        }
        #region 修改入库冻结量
        public bool UpdateFrozenQty(string cellCode)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                string sql = string.Format("update WMS_WH_CELL set FROZEN_IN_QTY='{0}'where CELLCODE='{1}'"
                                             , this.FROZEN_IN_QTY, cellCode);


                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }
        #endregion
        #region 批量分配指定烟
        public bool UpdateBatch()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();

                string sql = string.Format("update WMS_WH_CELL set ISACTIVE='{1}',ASSIGNEDPRODUCT='{2}',ISVIRTUAL='{3}',UNITCODE='{4}' where CELL_ID='{0}'"
                                             , this.CELL_ID,
                            this.ISACTIVE,
                            this.ASSIGNEDPRODUCT,
                            this.ISVIRTUAL,
                            this.UNITCODE);

                dao.SetData(sql);
                dao.SetData("update wms_wh_cell set  assignedproduct=null where assignedproduct=''");
                flag = true;
            }
            return flag;
        }
        #endregion
        public bool Delete(DataSet dataSet)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        public bool Delete(int CellID)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                string sql = string.Format("delete from WMS_WH_CELL WHERE CELL_ID={0}",CellID);
                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 查询产品分布
        /// </summary>
        public DataSet QueryProductDistribution(int pageIndex,int pageSize,string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                string sql = string.Format("SELECT * FROM {0} WHERE  {1} ORDER BY CURRENTPRODUCT,CELLCODE", strTableView, filter);
                return dao.Query(sql, pageIndex, pageSize);
            }
        }

        //报表
        public DataTable QueryProductDistribution(string filter)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                dao.SetPersistentManager(pm);
                string sql = string.Format("SELECT * FROM {0} WHERE  {1} ORDER BY CURRENTPRODUCT,CELLCODE", strTableView, filter);
                return dao.Query(sql).Tables[0];
            }
        }
        //更新货位信息，将没有重要信息的货位初始化
        public bool UpdateCell()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();

                string sql = string.Format("UPDATE WMS_WH_CELL set CURRENTPRODUCT=NULL,INPUTDATE=NULL WHERE ISACTIVE=1 AND QUANTITY=0 AND FROZEN_IN_QTY=0 AND FROZEN_OUT_QTY=0 AND  ISLOCKED=0 AND AREATYPE<> 1");

                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        public string QueryShelfCode(string productCode)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                string sql = string.Format("SELECT SHELFCODE FROM WMS_WH_CELL WHERE ASSIGNEDPRODUCT='{0}' AND AREATYPE='0' GROUP BY SHELFCODE", productCode);
                DataTable shelfTable = dao.GetData(sql).Tables[0];
                string shelfList= UtinString.StringMake(shelfTable, "SHELFCODE");
                return UtinString.StringMake(shelfList);                
            }
        }

        #region
        public bool UpdateCellEx()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("update wms_wh_cell set quantity=quantity-frozen_out_qty,frozen_out_qty=0 where FROZEN_OUT_QTY<0"));
                sb.Append(string.Format("update wms_wh_cell set quantity=quantity+frozen_in_qty,frozen_in_qty=0 where frozen_in_qty<0"));
                dao.SetData(sb.ToString());
                flag = true;
            }
            return flag;
        }
        #endregion

        #region 获取截止上次下载出库单发生变化的货位
        //获取入库单产生的货位变化
        public DataSet InChangeCell(DateTime timeNow)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                dao.SetPersistentManager(pm);
                string sql = string.Format("SELECT CELLCODE FROM WMS_IN_ALLOT WHERE FINISHTIME<='{0}' AND FINISHTIME>=(select max(billdate) from WMS_OUT_BILLMASTER) AND STATUS='2'", timeNow);
                return dao.Query(sql);
            }
        }
        //获取出库单产生的货位变化
        public DataSet OutChangeCell(DateTime timeNow)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                dao.SetPersistentManager(pm);
                string sql = string.Format("SELECT CELLCODE FROM WMS_OUT_ALLOTDETAIL WHERE FINISHTIME<='{0}' AND FINISHTIME>=(select max(billdate) from WMS_OUT_BILLMASTER) AND STATUS='2'", timeNow);
                return dao.Query(sql);
            }
        }
        //获取移位单产生的货位变化
        public DataSet MoveChangeCell(DateTime timeNow)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                dao.SetPersistentManager(pm);
                string sql = string.Format("SELECT OUT_CELLCODE,IN_CELLCODE FROM WMS_MOVE_BILLDETAIL WHERE FINISHTIME<='{0}' AND FINISHTIME>=(select max(billdate) from WMS_OUT_BILLMASTER) AND STATUS='2'", timeNow);
                return dao.Query(sql);
            }
        }
        #endregion


        #region 中烟接口数据操作

        public bool InsertStorage()
        {
            bool flag = false;
            string cellcode = this.CELLCODE.Substring(7,15);
            string upcode = this.CELLCODE.Substring(0,7);
            string ctrcode = this.CELLCODE.Substring(0,3);
            using (PersistentManager pm = new PersistentManager())
            {
                WarehouseCellDao dao = new WarehouseCellDao();
                string sql = string.Format("insert into DWV_IBAS_STORAGE (STORAGE_CODE,STORAGE_TYPE,CONTAINER,STORAGE_NAME,UP_CODE,DIST_CTR_CODE,AREA_TYPE,UPDATE_DATE,ISACTIVE,IS_IMPORT) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                    cellcode,
                    4,
                    5002,
                    this.CELLNAME,
                    upcode,
                    ctrcode,
                    0901,
                    DateTime.Now.ToString(),
                    this.ISACTIVE,
                    0);
                dao.GetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool UpdateStorage()
        {
            bool flag = false;
            using (PersistentManager om = new PersistentManager())
            {
                //string cellcode = this.CELLCODE.Substring(7, 15);
                string cellcode = this.CELLCODE.Substring(4, 2) + this.CELLCODE.Substring(7, 3) + this.CELLCODE.Substring(11, 2) + this.CELLCODE.Substring(14, 1);
                WarehouseDao dao = new WarehouseDao();
                string sql = string.Format("update DWV_IBAS_STORAGE set STORAGE_NAME='{0}',UPDATE_DATE='{1}',ISACTIVE='{2}' where STORAGE_CODE='{3}'",
                    this.CELLNAME,
                    DateTime.Now.ToString(),
                    this.ISACTIVE,
                    cellcode);
                dao.GetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool DeleteStorage()
        {
            bool flag = false;
            using (PersistentManager pm = new PersistentManager())
            {
                string cellcode = this.CELLCODE.Substring(7, 15);
                WarehouseDao dao = new WarehouseDao();
                string sql = string.Format("delete from DWV_IBAS_STORAGE where STORAGE_CODE='{0}'",
                    cellcode);
                dao.GetData(sql);
                flag = true;
            }
            return flag;
        }
        #endregion

        #region property
        private int _cell_id;
        private string _shelfcode;
        private string _cellcode;
        private string _cellname;
        private string _isactive;
        private decimal _max_quantity;
        private string _layer_no;
        private string _assignedproduct;
        //private string _currentproduct;
        private string _unitcode;
        private decimal _quantity;
        //private DateTime _inputdate;
        private string _palletid;
        private double _frozen_in_qty;
        //private double _frozen_out_qty;
        //private string _islocked;
        private string _isvirtual;
        private int _electricgroup;
        private int _electriccom;
        private int _electricaddress;
        private string _areatype;


        public int CELL_ID
        {
            get
            {
                return _cell_id;
            }
            set
            {
                _cell_id = value;
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

        public string CELLCODE
        {
            get
            {
                return _cellcode;
            }
            set
            {
                _cellcode = value;
            }
        }

        public string CELLNAME
        {
            get
            {
                return _cellname;
            }
            set
            {
                _cellname = value;
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

        public decimal MAX_QUANTITY
        {
            get
            {
                return _max_quantity;
            }
            set
            {
                _max_quantity = value;
            }
        }

        public string LAYER_NO
        {
            get
            {
                return _layer_no;
            }
            set
            {
                _layer_no = value;
            }
        }

        public string ASSIGNEDPRODUCT
        {
            get
            {
                return _assignedproduct;
            }
            set
            {
                _assignedproduct = value;
            }
        }

        //public string CURRENTPRODUCT
        //{
        //    get
        //    {
        //        return _currentproduct;
        //    }
        //    set
        //    {
        //        _currentproduct = value;
        //    }
        //}

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

        public decimal QUANTITY
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }

        //public DateTime INPUTDATE
        //{
        //    get
        //    {
        //        return _inputdate;
        //    }
        //    set
        //    {
        //        _inputdate = value;
        //    }
        //}

        public string PALLETID
        {
            get
            {
                return _palletid;
            }
            set
            {
                _palletid = value;
            }
        }

        public double FROZEN_IN_QTY
        {
            get
            {
                return _frozen_in_qty;
            }
            set
            {
                _frozen_in_qty = value;
            }
        }

        //public double FROZEN_OUT_QTY
        //{
        //    get
        //    {
        //        return _frozen_out_qty;
        //    }
        //    set
        //    {
        //        _frozen_out_qty = value;
        //    }
        //}

        //public string ISLOCKED
        //{
        //    get
        //    {
        //        return _islocked;
        //    }
        //    set
        //    {
        //        _islocked = value;
        //    }
        //}

        public string ISVIRTUAL
        {
            get
            {
                return _isvirtual;
            }
            set
            {
                _isvirtual = value;
            }
        }

        public int ELECTRICGROUP
        {
            get
            {
                return _electricgroup;
            }
            set
            {
                _electricgroup = value;
            }
        }

        public int ELECTRICCOM
        {
            get
            {
                return _electriccom;
            }
            set
            {
                _electriccom = value;
            }
        }

        public int ELECTRICADDRESS
        {
            get
            {
                return _electricaddress;
            }
            set
            {
                _electricaddress = value;
            }
        }
        #endregion
    }
}
