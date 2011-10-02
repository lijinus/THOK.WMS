using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.WMS.Dao;
using System.Data;

namespace THOK.WMS.BLL
{
   public class SortingOrderBll
    {
       private string strTableView = "DWV_OUT_ORDER";
       private string strPrimaryKey = "ORDER_ID";
       private string strQueryFields = "*";

       /// <summary>
       /// 分页查询数据，指定数据集表名
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <param name="filter"></param>
       /// <param name="OrderByFields"></param>
       /// <returns></returns>
       public DataSet QuerySortingOrderMaster(int pageIndex, int pageSize, string filter, string OrderByFields)
       {
           using (PersistentManager persistentManager = new PersistentManager())
           {
               SortingOrderDao dao = new SortingOrderDao();
               return dao.FindExecuteQuery(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
           }
       }

       /// <summary>
       /// 清除当天主表数据和明细表数据
       /// </summary>
       public string deleteData()
       {
           string tag = "true";
           try
           {
               DateTime dateTime = DateTime.Now;
               string date = Convert.ToString(dateTime.ToString("yyyyMMdd"));
               using (PersistentManager persistentManager = new PersistentManager())
               {
                   SortingOrderDao dao = new SortingOrderDao();
                   SortingOrderDetailBll bll = new SortingOrderDetailBll();
                   DataTable orderdt = dao.QueryDate(date);
                   string orderidList = UtinString.StringMake(orderdt, "ORDER_ID");
                   string orderid = UtinString.StringMake(orderidList);
                   dao.deleteOrderDate(date);
                   bll.DeleteOrderId(orderid);
               }
           }
           catch (Exception e)
           {
               tag = e.Message;
           }
           return tag;
       }


       /// <summary>
       /// 一个表名一个条件查询分拣主表数据
       /// </summary>
       /// <param name="filter"></param>
       /// <returns></returns>
       public int GetRowCount(string filter)
       {
           using (PersistentManager persistentManager = new PersistentManager())
           {
               SortingOrderDao dao = new SortingOrderDao();
               return dao.GetRowCount(strTableView, filter);
           }
       }

       /// <summary>
       /// 确认线路分配
       /// </summary>
       /// <returns></returns>
       public string UpdateOrderSoringGroupInfo(string SortingCode, string RouteCode,string orderDate)
       {
           string tag="true";
           using (PersistentManager persistentManager = new PersistentManager())
           {
               RouteCode = RouteCode.Substring(3, RouteCode.Length - 3);
               DataTable orderTable = this.QueryOrderId(RouteCode,orderDate);//查询主表Id
               if (orderTable.Rows.Count > 0)
               {
                   string orderIdList = UtinString.StringMake(orderTable, "ORDER_ID");
                   orderIdList = UtinString.StringMake(orderIdList);//取得截取后的主表Id字符
                   SortingOrderDetailBll orderDetail = new SortingOrderDetailBll();
                   SortingRouteBll bll = new SortingRouteBll();
                   string str = orderDetail.UpdateOrderDeatil(SortingCode, orderIdList);//根据主表ID修改明细表分拣分配状态
                   if (str == "true")
                   {
                       this.UpdateOrderMaster(SortingCode, RouteCode, orderDate);//修改主表分拣线编号
                       //bll.UpdateRouteAllotState(RouteCode, "1");//修改线路表id
                   }
                   else
                       tag = str;
               }
               else
                   tag = "选择的线路没有主订单";
           }
           return tag;
       }

       /// <summary>
       /// 根据线路编号查询主表的主表Id
       /// </summary>
       /// <param name="RouteCode"></param>
       /// <returns></returns>
       public DataTable QueryOrderId(string RouteCode,string orderDate)
       {
           using (PersistentManager persistentManager = new PersistentManager())
           {
               SortingOrderDao dao = new SortingOrderDao();
               return dao.QueryOrderId(RouteCode,orderDate);
           }
       }

       /// <summary>
       /// 修改主表线路分配的分拣线编号
       /// </summary>
       /// <param name="SortingCode"></param>
       /// <param name="routeCode"></param>
       public void UpdateOrderMaster(string SortingCode, string routeCode,string orderDate)
       {
           using (PersistentManager persistentManager = new PersistentManager())
           {
               SortingOrderDao dao = new SortingOrderDao();
               dao.UpdateOrderMaster(SortingCode, routeCode,orderDate);
           }
       }


       /// <summary>
       /// 取消线路分配
       /// </summary>
       /// <param name="RouteCode"></param>
       /// <returns></returns>
       public string CancelAllotOrder(string RouteCode,string orderDate)
       {
           string tag = "true";
           using (PersistentManager persistentManager = new PersistentManager())
           {
               RouteCode = RouteCode.Substring(3, RouteCode.Length - 3);
               DataTable orderTable = this.QueryOrderId(RouteCode,orderDate);//查询主表Id
               if (orderTable.Rows.Count > 0)
               {
                   string orderIdList = UtinString.StringMake(orderTable, "ORDER_ID");
                   orderIdList = UtinString.StringMake(orderIdList);//取得截取后的主表Id字符
                   SortingOrderDetailBll orderDetail = new SortingOrderDetailBll();
                   SortingRouteBll bll = new SortingRouteBll();
                  // bll.UpdateRouteAllotState(RouteCode, "0");
                   this.UpdateOrderMaster("", RouteCode,orderDate);
                   tag = orderDetail.UpdateOrderDeatil("", orderIdList);
               }
               else
                   tag = "选择取消的线路没有订单！";
           }
           return tag;
       }

       #region 封装的字段
       private string ORDER_ID;

       public string ORDER_ID1
       {
           get { return ORDER_ID; }
           set { ORDER_ID = value; }
       }
       private string ORG_CODE;

       public string ORG_CODE1
       {
           get { return ORG_CODE; }
           set { ORG_CODE = value; }
       }
       private string SALE_REG_CODE;

       public string SALE_REG_CODE1
       {
           get { return SALE_REG_CODE; }
           set { SALE_REG_CODE = value; }
       }
       private string ORDER_DATE;

       public string ORDER_DATE1
       {
           get { return ORDER_DATE; }
           set { ORDER_DATE = value; }
       }
       private string ORDER_TYPE;

       public string ORDER_TYPE1
       {
           get { return ORDER_TYPE; }
           set { ORDER_TYPE = value; }
       }
       private string CUST_CODE;

       public string CUST_CODE1
       {
           get { return CUST_CODE; }
           set { CUST_CODE = value; }
       }
       private string CUST_NAME;

       public string CUST_NAME1
       {
           get { return CUST_NAME; }
           set { CUST_NAME = value; }
       }
       private decimal QUANTITY_SUM;

       public decimal QUANTITY_SUM1
       {
           get { return QUANTITY_SUM; }
           set { QUANTITY_SUM = value; }
       }


       private decimal AMOUNT_SUM;

       public decimal AMOUNT_SUM1
       {
           get { return AMOUNT_SUM; }
           set { AMOUNT_SUM = value; }
       }


       private decimal DETAIL_NUM;

       public decimal DETAIL_NUM1
       {
           get { return DETAIL_NUM; }
           set { DETAIL_NUM = value; }
       }

      
       private string DELIVER_LINE_NAME;

       public string DELIVER_LINE_NAME1
       {
           get { return DELIVER_LINE_NAME; }
           set { DELIVER_LINE_NAME = value; }
       }
       private string DELIVER_ORDER;

       public string DELIVER_ORDER1
       {
           get { return DELIVER_ORDER; }
           set { DELIVER_ORDER = value; }
       }
       private string ISVCTIVE;

       public string ISVCTIVE1
       {
           get { return ISVCTIVE; }
           set { ISVCTIVE = value; }
       }
       private string UPDATE_DATE;

       public string UPDATE_DATE1
       {
           get { return UPDATE_DATE; }
           set { UPDATE_DATE = value; }
       }
       #endregion


   }
}
