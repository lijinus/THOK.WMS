using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Dao
{
   public class SortingOrderDetailBllDao:BaseDao
    {


        /// <summary>
        /// 分页查询明细
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tableName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet Query(string sql, string tableName, int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize;
            return ExecuteQuery(sql, tableName, start, pageSize);
        }


       /// <summary>
       /// 通过主表id修改分配字段
       /// </summary>
       /// <param name="sortingCode"></param>
       /// <param name="orderId"></param>
       /// <returns></returns>
       public string UpdateOrderDeatil(string sortingCode, string orderId)
       {
           string tag = "true";
           try
           {
               string sql = string.Format("UPDATE DWV_OUT_ORDER_DETAIL SET SORTING_CODE ='{0}' WHERE ORDER_ID IN({1})", sortingCode, orderId);
               this.ExecuteNonQuery(sql);
           }
           catch (Exception e)
           {
               return tag = e.Message;
           }
           return tag;
       }

       /// <summary>
       /// 根据主表id删除明细表数据
       /// </summary>
       /// <param name="orderid"></param>
       public void DeleteOrderId(string orderid)
       {
           string sql = string.Format("DELETE DWV_OUT_ORDER_DETAIL WHERE ORDER_ID IN({0})",orderid);
           this.ExecuteNonQuery(sql);
       }
    }
}
