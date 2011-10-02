using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Dao
{
   public class SortingOrderDao:BaseDao
    {
       /// <summary>
       /// 一个表名一个条件查询分拣订单数据
       /// </summary>
       /// <param name="TableViewName"></param>
       /// <param name="filter"></param>
       /// <returns></returns>
       public int GetRowCount(string TableViewName, string filter)
       {
           string sql = string.Format("SELECT COUNT(*) FROM {0}" +
                                        " WHERE {1} "
                                        , TableViewName, filter);
           return (int)ExecuteScalar(sql);
       }


       /// <summary>
       /// 分页查询数据，指定数据集表名TableName
       /// </summary>
       /// <param name="TableViewName">表名或视图名</param>
       /// <param name="PrimaryKey">表主键字段名称</param>
       /// <param name="QueryFields">查询字段字符串，字段名称逗号隔开</param>
       /// <param name="pageIndex">查询页码</param>
       /// <param name="pageSize">页码大小</param>
       /// <param name="orderBy">排序字段和方式</param>
       /// <param name="filter">查询条件</param>
       /// <param name="strTableName">返回数据集填充的表名</param>
       /// <returns>返回DataSet</returns>
       public DataSet FindExecuteQuery(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
       {
           //string bei = ",case when ISACTIVE=1 then '使用' else '停用' end as SORTINGSTATE ";
           int preRec = (pageIndex - 1) * pageSize;
           string sql = string.Format("select top {4} {2} from {0} " +
                                       " where {1} not in ( select top {3} {1} from {0} where {6} order by {5}) " +
                                       " and {6} order by {5}"
                                       , TableViewName, PrimaryKey, QueryFields, preRec.ToString(), pageSize.ToString(), orderBy, filter);

           return ExecuteQuery(sql).Tables[0].DataSet;
       }

       /// <summary>
       /// 清除当天主表的信息
       /// </summary>
       public void deleteOrderDate(string date)
       {
           string sql = string.Format("DELETE DWV_OUT_ORDER WHERE ORDER_DATE ='{0}'", date);
           this.ExecuteNonQuery(sql);
       }

       public DataTable QueryDate(string date)
       {
           string sql = string.Format("SELECT * FROM DWV_OUT_ORDER WHERE ORDER_DATE='{0}'",date);
           return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// 根据线路编号和时间查询主表的Id
       /// </summary>
       /// <param name="RouteCode"></param>
       /// <returns></returns>
       public DataTable QueryOrderId(string RouteCode,string orderDate)
       {
           string sql = string.Format("SELECT ORDER_ID FROM DWV_OUT_ORDER WHERE DELIVER_LINE_CODE IN ({0}) AND ORDER_DATE IN({1})", RouteCode,orderDate);
           return this.ExecuteQuery(sql).Tables[0];
       
       }


       /// <summary>
       ///根据线路编号修改订单分配情况
       /// </summary>
       /// <param name="SortingCode"></param>
       /// <param name="routeCode"></param>
       public void UpdateOrderMaster(string SortingCode, string routeCode,string orderDate)
       {
           string sql = string.Format("UPDATE DWV_OUT_ORDER SET SORTING_CODE ='{0}' WHERE DELIVER_LINE_CODE IN({1}) AND ORDER_DATE IN({2})", SortingCode, routeCode, orderDate);
           this.ExecuteNonQuery(sql);
       }

    }
}
