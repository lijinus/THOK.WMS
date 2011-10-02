using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Dao
{
   public class SortingRouteDao:BaseDao
   {
       #region 查询线路信息

       /// <summary>
       /// 根据表名和条件查询记录
       /// </summary>
       /// <param name="TableViewName"></param>
       /// <param name="filter"></param>
       /// <returns></returns>
       public int GetRowCount(string filter,string date,string isZhi)
       {
           string sql = string.Format("SELECT COUNT(*) FROM (SELECT DELIVER_LINE_NAME,DIST_STA_NAME,ORDER_DATE FROM DWV_OUT_ORDER WHERE IS_IMPORT=0 AND SORTING_CODE {1} 0 " +
                                       " GROUP BY DIST_STA_NAME,DELIVER_LINE_NAME,ORDER_DATE ) A WHERE {0}"
                                        , filter,isZhi,date);
           return (int)ExecuteScalar(sql);
       }

       /// <summary>
       /// 查询总数量和金额
       /// </summary>
       /// <returns></returns>
       public DataTable QuerySortingQuantity()
       {
           string sql = "SELECT SUM(QUANTITY_SUM) AS QUANTITY,SUM(AMOUNT_SUM) AS AMOUNT FROM DWV_OUT_ORDER WHERE IS_IMPORT=0";
           return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// 分页查询数据
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
       public DataSet QuerySortingRoute(int pageIndex, int pageSize,string file,string date,string isZhi)
       {
           int preRec = (pageIndex - 1) * pageSize;
           string sql = string.Format("SELECT COUNT(*) AS ORDERQUANTITY,SUM(A.QUANTITY_SUM) AS QUANTITY,SUM(A.AMOUNT_SUM) AS AMOUNT,A.ORDER_DATE,"+
                                       " D.DIST_STA_CODE,D.DIST_STA_NAME,A.DELIVER_LINE_CODE,A.DELIVER_LINE_NAME,A.SORTING_CODE,B.SORTING_NAME,"+
                                       " CASE WHEN A.SORTING_CODE ='' THEN '未分配' ELSE '已分配' END AS ISALLOTS FROM DWV_OUT_ORDER A "+
                                       " LEFT JOIN DWV_DPS_SORTING B ON A.SORTING_CODE=B.SORTING_CODE "+
                                       " LEFT JOIN DWV_OUT_DELIVER_LINE C ON A.DELIVER_LINE_CODE=C.DELIVER_LINE_CODE "+
                                       " LEFT JOIN DWV_OUT_DIST_STATION D ON A.DIST_STA_CODE =D.DIST_STA_CODE "+
                                       " WHERE {0} AND A.IS_IMPORT=0 AND A.SORTING_CODE {1}0" +// AND A.DELIVER_LINE_CODE NOT IN("+
                                       //" SELECT TOP {2} DELIVER_LINE_CODE FROM DWV_OUT_ORDER A WHERE {0} AND IS_IMPORT=0  GROUP BY DELIVER_LINE_CODE "+
                                      // " ,ORDER_DATE  ORDER BY ORDER_DATE DESC,DELIVER_LINE_CODE)" +
                                       " GROUP BY D.DIST_STA_CODE,D.DIST_STA_NAME,A.DELIVER_LINE_CODE,"+
                                       " A.DELIVER_LINE_NAME,A.SORTING_CODE,B.SORTING_NAME,A.ORDER_DATE "+
                                       " ORDER BY A.SORTING_CODE ASC,A.ORDER_DATE DESC,A.DELIVER_LINE_CODE", file, isZhi, date, preRec);
          return this.ExecuteQuery(sql);
       }

       public DataSet GetData(string sql)
       {
           return ExecuteQuery(sql);
       }


       /// <summary>
       /// 根据线路编号，更改分配状态
       /// </summary>
       /// <param name="RouteCode"></param>
       public void UpdateRouteAllotState(string RouteCode,string isAllot)
       {
           string sql = string.Format("UPDATE DWV_OUT_DELIVER_LINE SET ISALLOT={0} WHERE DELIVER_LINE_CODE IN({1})", isAllot, RouteCode);
           this.ExecuteNonQuery(sql);
       }

       /// <summary>
       /// 根据线路和时间查询每条线路上的卷烟数量进行汇总
       /// </summary>
       /// <param name="file"></param>
       /// <returns></returns>
       public DataSet QuerySortingRouteDetail(string file)
       { 
          string sql=  string.Format(@"SELECT A.PRODUCTCODE,A.PRODUCTNAME,
                            CAST(QTY_JIAN *(SELECT U.STANDARDRATE FROM WMS_UNIT U LEFT JOIN WMS_PRODUCT P ON U.UNITCODE = P.TIAOCODE
                            WHERE  P.PRODUCTCODE = A.PRODUCTCODE)/(SELECT U.STANDARDRATE FROM WMS_UNIT U LEFT JOIN WMS_PRODUCT P ON U.UNITCODE = P.JIANCODE
                            WHERE  P.PRODUCTCODE = A.PRODUCTCODE) AS INT) AS QTY_JIAN,
                            ((CONVERT(DECIMAL(18,2),QTY_TIAO *(SELECT U.STANDARDRATE FROM WMS_UNIT U LEFT JOIN WMS_PRODUCT P ON U.UNITCODE = P.TIAOCODE
                            WHERE  P.PRODUCTCODE = A.PRODUCTCODE)/(SELECT U.STANDARDRATE FROM WMS_UNIT U LEFT JOIN WMS_PRODUCT P ON U.UNITCODE = P.JIANCODE
                            WHERE  P.PRODUCTCODE = A.PRODUCTCODE)))-(CAST( QTY_JIAN *(SELECT U.STANDARDRATE FROM WMS_UNIT U LEFT JOIN WMS_PRODUCT P ON U.UNITCODE = P.TIAOCODE
                            WHERE  P.PRODUCTCODE = A.PRODUCTCODE)/(SELECT U.STANDARDRATE FROM WMS_UNIT U LEFT JOIN WMS_PRODUCT P ON U.UNITCODE = P.JIANCODE
                            WHERE  P.PRODUCTCODE = A.PRODUCTCODE) AS INT)))*(SELECT U.STANDARDRATE FROM WMS_UNIT U LEFT JOIN WMS_PRODUCT P ON U.UNITCODE = P.JIANCODE
                            WHERE  P.PRODUCTCODE = A.PRODUCTCODE)/(SELECT U.STANDARDRATE FROM WMS_UNIT U LEFT JOIN WMS_PRODUCT P ON U.UNITCODE = P.TIAOCODE
                            WHERE  P.PRODUCTCODE = A.PRODUCTCODE) AS QTY_TIAO,SORTING_NAME,SORTING_CODE
                            FROM (SELECT P.PRODUCTCODE,P.PRODUCTNAME,SUM(QUANTITY) AS QTY_JIAN,SUM(QUANTITY) AS QTY_TIAO,S.SORTING_NAME,S.SORTING_CODE
                            FROM DWV_OUT_ORDER_DETAIL A 
                            LEFT JOIN WMS_PRODUCT AS P ON P.PRODUCTN = A.BRAND_CODE 
                            LEFT JOIN DWV_DPS_SORTING S ON A.SORTING_CODE=S.SORTING_CODE
                            WHERE ORDER_ID IN(SELECT ORDER_ID FROM DWV_OUT_ORDER WHERE {0}
                            ) AND QUANTITY>0 GROUP BY P.PRODUCTCODE,P.PRODUCTNAME,S.SORTING_NAME,S.SORTING_CODE) A
                            LEFT JOIN WMS_PRODUCT AS P ON P.PRODUCTCODE = A.PRODUCTCODE
                            LEFT JOIN WMS_UNIT AS U ON P.UNITCODE=U.UNITCODE", file);
          return this.ExecuteQuery(sql);
       }


       #endregion
   }
}
