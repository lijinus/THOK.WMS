using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
    public class CheckBillDetail
    {
        private string strTableView = "V_wms_check_billdetail";
        private string strPrimaryKey = "ID";
        private string strQueryFields = "*";


        public DataSet QueryCheckBillDetail(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CheckBillDetailDao dao = new CheckBillDetailDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CheckBillDetailDao dao = new CheckBillDetailDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

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
                CheckBillDetailDao dao = new CheckBillDetailDao();
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
                CheckBillDetailDao dao = new CheckBillDetailDao();
                return dao.Query(sql, strTableView, pageIndex, pageSize);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {              
                CheckBillDetailDao dao = new CheckBillDetailDao();
                string sql = string.Format("Insert into wms_check_billdetail (BILLNO,CELLCODE,PRODUCTCODE,UNITCODE,RECORDQUANTITY,STATUS,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                                             , this.BILLNO,
                            this.CELLCODE,
                            this.PRODUCTCODE,
                            this.UNITCODE,
                            this.RECORDQUANTITY,
                            this.STATUS,
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
                CheckBillDetailDao dao = new CheckBillDetailDao();
                string sql = string.Format("update wms_check_billdetail set BILLNO='{1}',CELLCODE='{2}',PRODUCTCODE='{3}',UNITCODE='{4}',RECORDQUANTITY='{5}',COUNTQUANTITY='{6}',STATUS='{7}',MEMO='{8}'  where ID='{0}'"
                                             , this.ID,
                            this.BILLNO,
                            this.CELLCODE,
                            this.PRODUCTCODE,
                            this.UNITCODE,
                            this.RECORDQUANTITY,
                            0,//this.COUNTQUANTITY,
                            this.STATUS,
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
                CheckBillDetailDao dao = new CheckBillDetailDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }


        #region property
        private int _id;
        private string _billno;
        private string _cellcode;
        private string _productcode;
        private string _unitcode;
        private decimal _recordquantity;
        private decimal _countquantity;
        //private string _operateperson;
        //private DateTime _starttime;
        //private DateTime _finishtime;
        private string _status;
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

        public decimal RECORDQUANTITY
        {
            get
            {
                return _recordquantity;
            }
            set
            {
                _recordquantity = value;
            }
        }

        public decimal COUNTQUANTITY
        {
            get
            {
                return _countquantity;
            }
            set
            {
                _countquantity = value;
            }
        }

        //public string OPERATEPERSON
        //{
        //    get
        //    {
        //        return _operateperson;
        //    }
        //    set
        //    {
        //        _operateperson = value;
        //    }
        //}

        //public DateTime STARTTIME
        //{
        //    get
        //    {
        //        return _starttime;
        //    }
        //    set
        //    {
        //        _starttime = value;
        //    }
        //}

        //public DateTime FINISHTIME
        //{
        //    get
        //    {
        //        return _finishtime;
        //    }
        //    set
        //    {
        //        _finishtime = value;
        //    }
        //}

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
