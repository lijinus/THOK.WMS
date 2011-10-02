using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
   public class SortingOrderDetailBll
    {

       private string strTableView = "DWV_OUT_ORDER_DETAIL";
        //private string strPrimaryKey = "ORDER_ID";
        private string strQueryFields = "*";

        /// <summary>
        /// 根据订单号、页数、总条数查询数据
        /// </summary>
        /// <param name="BillNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet QueryByOrderId(string orderid, int pageIndex, int pageSize)
        {
            string[] aryBillNo = orderid.Split(',');
            string BillNoList = "''";
            for (int i = 0; i < aryBillNo.Length; i++)
            {
                BillNoList += ",'" + aryBillNo[i] + "'";
            }
            string sql = string.Format("SELECT  {0} FROM {1} WHERE ORDER_ID IN ({2}) ORDER BY ORDER_ID DESC", strQueryFields, strTableView, BillNoList);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SortingOrderDetailBllDao dao = new SortingOrderDetailBllDao();
                return dao.Query(sql, strTableView, pageIndex, pageSize);
            }
        }

       /// <summary>
       /// 根据主表的id修改分配的分拣线编号
       /// </summary>
       /// <param name="sortingCode"></param>
       /// <param name="orderId"></param>
       public string UpdateOrderDeatil(string sortingCode,string orderId)
       {
           using (PersistentManager persistentManager = new PersistentManager())
           {
               SortingOrderDetailBllDao dao = new SortingOrderDetailBllDao();
              return dao.UpdateOrderDeatil(sortingCode, orderId);
           }
       }

       /// <summary>
       /// 根据主表id删除明细表数据
       /// </summary>
       /// <param name="orderid"></param>
       public void DeleteOrderId(string orderid)
       {
           using (PersistentManager persistentManager = new PersistentManager())
           {
               SortingOrderDetailBllDao dao = new SortingOrderDetailBllDao();
               dao.DeleteOrderId(orderid);
           }
       }

        #region 封装的字段

        private string ORDER_DETAIL_ID;

        public string ORDER_DETAIL_ID1
        {
            get { return ORDER_DETAIL_ID; }
            set { ORDER_DETAIL_ID = value; }
        }
        private string ORDER_ID;

        public string ORDER_ID1
        {
            get { return ORDER_ID; }
            set { ORDER_ID = value; }
        }
        private string BRAND_CODE;

        public string BRAND_CODE1
        {
            get { return BRAND_CODE; }
            set { BRAND_CODE = value; }
        }
        private string BRAND_NAME;

        public string BRAND_NAME1
        {
            get { return BRAND_NAME; }
            set { BRAND_NAME = value; }
        }
        private string BRAND_UNIT_NAME;

        public string BRAND_UNIT_NAME1
        {
            get { return BRAND_UNIT_NAME; }
            set { BRAND_UNIT_NAME = value; }
        }
        private decimal QUANTITY;

        public decimal QUANTITY1
        {
            get { return QUANTITY; }
            set { QUANTITY = value; }
        }
        private decimal PRICE;

        public decimal PRICE1
        {
            get { return PRICE; }
            set { PRICE = value; }
        }
        private decimal AMOUNT;

        public decimal AMOUNT1
        {
            get { return AMOUNT; }
            set { AMOUNT = value; }
        }
        #endregion
    }
}
