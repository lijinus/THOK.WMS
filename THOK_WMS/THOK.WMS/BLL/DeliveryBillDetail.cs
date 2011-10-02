using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.WMS.Dao;
using System.Data;

namespace THOK.WMS.BLL
{
    public class DeliveryBillDetail
    {
        #region property
        private string _id;
        private string _billno;
        private string _productcode;
        private decimal _price;
        private decimal _quantity;
        private decimal _outputquantity;
        private string _unitcode;
        private string _memo;


        public string ID
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

        public decimal OUTPUTQUANTITY
        {
            get
            {
                return _outputquantity;
            }
            set
            {
                _outputquantity = value;
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

        private string strTableView = "V_WMS_OUT_BILLDETAIL";
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
                DeliveryBillDetailDao dao = new DeliveryBillDetailDao();
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
            string sql = string.Format("SELECT  {0} from {1} where BILLNO in ({2}) ORDER BY BILLNO DESC,PRODUCTCODE ASC", strQueryFields, strTableView, BillNoList);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DeliveryBillDetailDao dao = new DeliveryBillDetailDao();
                return dao.Query(sql, strTableView, pageIndex, pageSize);
            }
        }


        public DataSet QueryByID(string id)
        {
            string sql = string.Format("SELECT {1} FROM {2} where ID='{0}' ", id.ToString(), strQueryFields, strTableView);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DeliveryBillDetailDao dao = new DeliveryBillDetailDao();
                return dao.GetData(sql);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DeliveryBillDetailDao dao = new DeliveryBillDetailDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }



        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DeliveryBillDetailDao dao = new DeliveryBillDetailDao();

                string sql = string.Format("Insert into WMS_OUT_BILLDETAIL (ID,BILLNO,PRODUCTCODE,PRICE,QUANTITY,OUTPUTQUANTITY,UNITCODE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')"
                                             , this.ID,this.BILLNO,
                            this.PRODUCTCODE,
                            this.PRICE,
                            this.QUANTITY,
                            this.OUTPUTQUANTITY,
                            this.UNITCODE,
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
                DeliveryBillDetailDao dao = new DeliveryBillDetailDao();
                string sql = string.Format("update WMS_OUT_BILLDETAIL set BILLNO='{1}',PRODUCTCODE='{2}',PRICE='{3}',QUANTITY='{4}',OUTPUTQUANTITY='{5}',UNITCODE='{6}',MEMO='{7}'  where ID='{0}'"
                                             , this.ID,
                            this.BILLNO,
                            this.PRODUCTCODE,
                            this.PRICE,
                            this.QUANTITY,
                            this.OUTPUTQUANTITY,
                            this.UNITCODE,
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
                DeliveryBillDetailDao dao = new DeliveryBillDetailDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }
    }
}
