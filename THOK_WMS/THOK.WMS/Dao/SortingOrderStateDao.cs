using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Dao
{
    public class SortingOrderStateDao : BaseDao
    {
        /// <summary>
        /// 根据表名和条件查询记录
        /// </summary>
        /// <param name="TableViewName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int GetRowCount(string filter)
        {
            string sql = string.Format("SELECT COUNT(*) FROM DWV_IORD_SORT_STATUS WHERE {0}", filter);
            return (int)ExecuteScalar(sql);
        }

        /// <summary>
        /// 根据时间查询数据
        /// </summary>
        /// <returns></returns>
        public DataSet QuerySortStatus(string datetime)
        {
            string sql = string.Format("SELECT B.SORTING_CODE,C.SORTING_NAME,B.SORT_DATE, A.DELIVER_LINE_CODE, A.DELIVER_LINE_NAME, " +
                        "SUM(A.QUANTITY_SUM) AS SORT_QUANTITY, COUNT(*) AS SORT_ORDER_NUM, MIN(B.SORT_BEGIN_TIME) AS SORT_BEGIN_DATE, MAX(B.SORT_END_TIME) " +
                        "AS SORT_END_DATE, DATEDIFF(MS, CAST(STUFF(STUFF(STUFF(MIN(B.SORT_BEGIN_TIME), 9, 0, ' '), 12, 0, ':'), 15, 0, ':') AS DATETIME), " +
                        "CAST(STUFF(STUFF(STUFF(MAX(B.SORT_END_TIME), 9, 0, ' '), 12, 0, ':'), 15, 0, ':') AS DATETIME)) AS SORT_COST_TIME,b.SORT_BILL_ID,D.EMPLOYEENAME " +
                        "FROM DWV_OUT_ORDER AS A RIGHT OUTER JOIN  " +
                        "DWV_DPS_SORT_STATUS AS B ON A.CUST_CODE = B.CUST_CODE LEFT OUTER JOIN " +
                        "DWV_DPS_SORTING AS C ON B.SORTING_CODE = C.SORTING_CODE LEFT OUTER JOIN " +
                        "BI_EMPLOYEE AS D ON B.EmployeeCode = D.EMPLOYEECODE " +
                        "WHERE B.SORT_DATE='{0}' AND A.ORDER_DATE='{0}' GROUP BY C.SORTING_NAME,A.DELIVER_LINE_CODE, A.DELIVER_LINE_NAME,b.SORT_BILL_ID,D.EMPLOYEENAME,B.SORTING_CODE,B.SORT_DATE", datetime);
            return this.ExecuteQuery(sql);
        }


        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet QuerySortingState(string file)
        {
            string sql = string.Format("SELECT * FROM V_DWV_DPS_SORT_STATUS WHERE {0}",file);
            return this.ExecuteQuery(sql);
        }

        /// <summary>
        /// 根据线路编码查询客户编码
        /// </summary>
        /// <param name="routeCode"></param>
        /// <returns></returns>
        public DataTable GetCustByList(string routeCode)
        {
            string sql = string.Format("SELECT * FROM DWV_OUT_ORDER WHERE DELIVER_LINE_CODE IN({0}) AND IS_IMPORT=0",routeCode);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 更改分拣状态表送货人
        /// </summary>
        /// <param name="custCode"></param>
        /// <param name="employee"></param>
        public void UpdateSortStatus(string custCode,string employee)
        {
            string sql = string.Format("UPDATE DWV_DPS_SORT_STATUS SET EMPLOYEECODE='{0}' WHERE CUST_CODE IN({1}) AND UP_STATUS=0",employee,custCode);
            this.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 根据条件查询分拣状态数据
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public DataTable QuerySort(string file)
        {
            string sql = string.Format("SELECT A.CUST_CODE,B.CUST_NAME,B.QUANTITY_SUM,A.SORT_BEGIN_TIME,SORT_END_TIME FROM DWV_DPS_SORT_STATUS A " +
                                      "  LEFT JOIN DWV_OUT_ORDER B ON A.CUST_CODE=B.CUST_CODE WHERE {0}", file);
            return this.ExecuteQuery(sql).Tables[0];
        }
    }
}
