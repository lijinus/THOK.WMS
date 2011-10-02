using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
    public class ProfitLossBillDetail
    {
        private string strTableView = "V_WMS_PL_BILLDETAIL";
        private string strPrimaryKey = "ID";
        //private string strOrderByFields = "ExceptionalLogID ASC";
        private string strQueryFields = "*";
        public DataSet QueryProfitLossBillDetail(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProfitLossBillDetailDao dao = new ProfitLossBillDetailDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProfitLossBillDetailDao dao = new ProfitLossBillDetailDao();
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
                ProfitLossBillDetailDao dao = new ProfitLossBillDetailDao();
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
                ProfitLossBillDetailDao dao = new ProfitLossBillDetailDao();
                return dao.Query(sql, strTableView, pageIndex, pageSize);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProfitLossBillDetailDao dao = new ProfitLossBillDetailDao();
                string sql = string.Format("Insert into WMS_PL_BILLDETAIL (BILLNO,CELLCODE,PRODUCTCODE,QUANTITY,UNITCODE,PRICE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                                             , this.BILLNO,
                            this.CELLCODE,
                            this.PRODUCTCODE,
                            this.QUANTITY,
                            this.UNITCODE,
                            this.PRICE,
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
                ProfitLossBillDetailDao dao = new ProfitLossBillDetailDao();
                string sql = string.Format("update WMS_PL_BILLDETAIL set BILLNO='{1}',CELLCODE='{2}',PRODUCTCODE='{3}',QUANTITY='{4}',UNITCODE='{5}',PRICE='{6}',MEMO='{7}'  where ID='{0}'"
                                             , this.ID,
                            this.BILLNO,
                            this.CELLCODE,
                            this.PRODUCTCODE,
                            this.QUANTITY,
                            this.UNITCODE,
                            this.PRICE,
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
                ProfitLossBillDetailDao dao = new ProfitLossBillDetailDao();
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
        private decimal _quantity;
        private string _unitcode;
        private decimal _price;
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

        public decimal PRICE
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
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