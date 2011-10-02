using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.WMS.Dao;
using System.Data;

namespace THOK.WMS.BLL
{
   public class SortingRouteBll
    {
       private string strTableView = "DWV_OUT_DELIVER_LINE";
       private string strPrimaryKey = "DELIVER_LINE_CODE";
       private string strQueryFields = "*";


       /// <summary>
       /// 分页查询
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
       public DataSet QuerySortingRoute(int pageIndex, int pageSize,string file,string isZhi)
       {
           DateTime dateTime = DateTime.Now;
           string date = Convert.ToString(dateTime.ToString("yyyyMMdd"));
           using (PersistentManager persistentManager = new PersistentManager())
           {
               SortingRouteDao dao = new SortingRouteDao();
               return dao.QuerySortingRoute(pageIndex, pageSize,file,date,isZhi);
           }
       }

       /// <summary>
       /// 查询未上报的分拣总数量和金额
       /// </summary>
       /// <returns></returns>
       public DataTable QuerySortingQuantity()
       {
           using (PersistentManager persistentManager = new PersistentManager())
           {
               SortingRouteDao dao = new SortingRouteDao();
               return dao.QuerySortingQuantity();
           }
       }

       /// <summary>
       /// 查询记录
       /// </summary>
       /// <param name="filter"></param>
       /// <returns></returns>
       public int GetRowCount(string filter,string isZhi)
       {
           DateTime dateTime = DateTime.Now;
           string date = Convert.ToString(dateTime.ToString("yyyyMMdd"));
           using (PersistentManager persistentManager = new PersistentManager())
           {
               SortingRouteDao dao = new SortingRouteDao();
               return dao.GetRowCount(filter,date,isZhi);
           }
       }


       /// <summary>
       /// 根据线路编号，更改分配状态
       /// </summary>
       /// <param name="RouteCode"></param>
       public void UpdateRouteAllotState(string RouteCode,string isAllot)
       {
           using (PersistentManager persistentManager = new PersistentManager())
           {
               SortingRouteDao dao = new SortingRouteDao();
               dao.UpdateRouteAllotState(RouteCode,isAllot);
           }
       }

       /// <summary>
       /// 查询每条线路中每个卷烟的总数
       /// </summary>
       /// <param name="file"></param>
       /// <returns></returns>
       public DataSet QuerySortingRouteDetail(string file)
       {
           using (PersistentManager persistentManager = new PersistentManager())
           {
               SortingRouteDao dao = new SortingRouteDao();
               return dao.QuerySortingRouteDetail(file);
           }
       }
    }
}
