using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.WMS.Dao;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class MoveBillDetail
    {
        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillDetailDao dao = new MoveBillDetailDao();
                string sql = string.Format("Insert into wms_move_billdetail (BILLNO,OUT_CELLCODE,IN_CELLCODE,PRODUCTCODE,UNITCODE,QUANTITY,STATUS) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                                             ,this.BILLNO,
                            this.OUT_CELLCODE,
                            this.IN_CELLCODE,
                            this.PRODUCTCODE,
                            this.UNITCODE,
                            this.QUANTITY,
                            this.STATUS);

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
                MoveBillDetailDao dao = new MoveBillDetailDao();

                string sql = string.Format("update wms_move_billdetail set BILLNO='{1}',OUT_CELLCODE='{2}',IN_CELLCODE='{3}',PRODUCTCODE='{4}',UNITCODE='{5}',QUANTITY='{6}',STATUS='{7}'  where ID='{0}'"
                                             , this.ID,
                            this.BILLNO,
                            this.OUT_CELLCODE,
                            this.IN_CELLCODE,
                            this.PRODUCTCODE,
                            this.UNITCODE,
                            this.QUANTITY,
                            this.STATUS);

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
                MoveBillDetailDao dao = new MoveBillDetailDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }


        private string strTableView = "V_WMS_MOVE_BILLDETAIL";
        //private string strPrimaryKey = "ID";
        //private string strOrderByFields = "BILLNO,ID";
        private string strQueryFields = "*";

        /// <summary>
        /// 根据主表单号查询明细
        /// </summary>
        /// <param name="BillNo">单据编号，可以多个用逗号隔开</param>
        /// <returns></returns>
        public DataSet QueryByBillNo(string BillNo)
        {
            string[] aryBillNo = BillNo.Split(',');
            string BillNoList = "''";
            for (int i = 0; i < aryBillNo.Length; i++)
            {
                BillNoList += ",'" + aryBillNo[i] + "'";
            }
            string sql = string.Format("select {0} from {1} where BILLNO in ({2}) ORDER BY BILLNO DESC", strQueryFields, strTableView, BillNoList);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillDetailDao dao = new MoveBillDetailDao();
                return dao.GetData(sql);
            }
        }

        public DataSet QueryByBillNo(string BillNo, int pageIndex, int pageSize)
        {
            string[] aryBillNo = BillNo.Split(',');
            string BillNoList = "''";
            for (int i = 0; i < aryBillNo.Length; i++)
            {
                BillNoList += ",'" + aryBillNo[i] + "'";
            }
            string sql = string.Format("SELECT  {0} from {1} where BILLNO in ({2}) ORDER BY BILLNO DESC", strQueryFields, strTableView, BillNoList);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillDetailDao dao = new MoveBillDetailDao();
                return dao.Query(sql, strTableView, pageIndex, pageSize);
            }
        }


        public DataSet QueryByID(int id)
        {
            string sql = string.Format("SELECT {1} FROM {2} where ID='{0}' ", id.ToString(), strQueryFields, strTableView);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillDetailDao dao = new MoveBillDetailDao();
                return dao.GetData(sql);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillDetailDao dao = new MoveBillDetailDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }
        #region  获取所有移位单货位
        public DataSet QueryAllCell()
        {
            string sql = string.Format("SELECT OUT_CELLCODE,IN_CELLCODE from WMS_MOVE_BILLDETAIL WHERE STATUS=0");
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillDetailDao dao = new MoveBillDetailDao();
                return dao.GetData(sql);
            }
        }
        #endregion

        #region property
        private int _id;
        private string _billno;
        private string _out_cellcode;
        private string _in_cellcode;
        private string _productcode;
        private string _unitcode;
        private decimal _quantity;
        private string _operateperson;
        private DateTime _starttime;
        private DateTime _finishtime;
        private string _status;


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

        public string BILLNO
        {
            get
            {
                return _billno;
            }
            set
            {
                _billno = value;
            }
        }

        public string OUT_CELLCODE
        {
            get
            {
                return _out_cellcode;
            }
            set
            {
                _out_cellcode = value;
            }
        }

        public string IN_CELLCODE
        {
            get
            {
                return _in_cellcode;
            }
            set
            {
                _in_cellcode = value;
            }
        }

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

        public string OPERATEPERSON
        {
            get
            {
                return _operateperson;
            }
            set
            {
                _operateperson = value;
            }
        }

        public DateTime STARTTIME
        {
            get
            {
                return _starttime;
            }
            set
            {
                _starttime = value;
            }
        }

        public DateTime FINISHTIME
        {
            get
            {
                return _finishtime;
            }
            set
            {
                _finishtime = value;
            }
        }

        public string STATUS
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }
        #endregion
    }
}
