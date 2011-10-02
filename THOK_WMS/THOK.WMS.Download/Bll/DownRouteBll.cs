using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.Download.Dao;

namespace THOK.WMS.Download.Bll
{
    public class DownRouteBll
    {
        #region 从营销系统下载线路信息

        /// <summary>
        /// 下载线路信息
        /// </summary>
        /// <returns></returns>
        public bool DownRouteInfo()
        {
            bool tag = true;           
            DataTable RouteCodeDt = this.GetRouteCode();
            string routeCodeList = UtinString.StringMake(RouteCodeDt, "DELIVER_LINE_CODE");
            routeCodeList = UtinString.StringMake(routeCodeList);
            routeCodeList = "DELIVER_LINE_CODE NOT IN (" + routeCodeList + ")";

            DataTable RouteDt = this.GetRouteInfo(routeCodeList);
            if (RouteDt.Rows.Count > 0)
            {
                DataSet routeCodeDs = this.InsertRouteCode(RouteDt);
                this.Insert(routeCodeDs);
            }else
                tag = false;
            return tag;
        }

        /// <summary>
        /// 自动下载线路信息，下载前清楚线路表
        /// </summary>
        /// <returns></returns>
       public bool GetDownRouteInfo()
       {
           bool tag = true;
           this.DeleteRoute();//下载清除线路表
           DataTable RouteDt = this.GetRouteInfo();
           if (RouteDt.Rows.Count > 0)
           {
               DataSet deptDs = this.InsertRouteCode(RouteDt);
               this.Insert(deptDs);
           }
           else
               tag = false;
           return tag;
       }

       /// <summary>
       /// 清除线路表
       /// </summary>
       public void DeleteRoute()
       {
           using (PersistentManager dbPm = new PersistentManager())
           {
               DownRouteDao dao = new DownRouteDao();
               dao.SetPersistentManager(dbPm);
               dao.Delete();
           }
       }

       /// <summary>
       /// 下载线路信息
       /// </summary>
       /// <returns></returns>
       public DataTable GetRouteInfo(string routeCodeList)
       {
           using (PersistentManager dbPm = new PersistentManager("YXConnection"))
           {
               DownRouteDao dao = new DownRouteDao();
               dao.SetPersistentManager(dbPm);
               return dao.GetRouteInfo(routeCodeList);
           }
       }
       /// <summary>
       /// 自动下载线路信息
       /// </summary>
       /// <returns></returns>
       public DataTable GetRouteInfo()
       {
           using (PersistentManager dbPm = new PersistentManager("YXConnection"))
           {
               DownRouteDao dao = new DownRouteDao();
               dao.SetPersistentManager(dbPm);
               return dao.GetRouteInfo();
           }
       }

       /// <summary>
       /// 查询仓库线路编号
       /// </summary>
       /// <returns></returns>
       public DataTable GetRouteCode()
       {
           using (PersistentManager dbPm = new PersistentManager())
           {
               DownRouteDao dao = new DownRouteDao();
               dao.SetPersistentManager(dbPm);
               return dao.GetRouteCode();
           }
       }

       /// <summary>
       /// 把虚拟表的数据添加到数据库
       /// </summary>
       /// <param name="ds"></param>
       public void Insert(DataSet ds)
       {
           using (PersistentManager dbPm = new PersistentManager())
           {
               DownRouteDao dao = new DownRouteDao();
               dao.SetPersistentManager(dbPm);
               dao.Insert(ds);
           }
       }

       /// <summary>
       /// 添加数据到虚拟表中
       /// </summary>
       /// <param name="dr"></param>
       /// <returns></returns>
       private DataSet InsertRouteCode(DataTable routeCodeTable)
       {
           DataSet ds = this.GenerateEmptyTables();
           foreach (DataRow row in routeCodeTable.Rows)
           {
               DataRow routeDr = ds.Tables["DWV_OUT_DELIVER_LINE"].NewRow();
               routeDr["DELIVER_LINE_CODE"] = row["DELIVER_LINE_CODE"];
               routeDr["LINE_TYPE"] = row["LINE_TYPE"];
               routeDr["DELIVER_LINE_NAME"] = row["DELIVER_LINE_NAME"];
               routeDr["DIST_STA_CODE"] = row["DIST_STA_CODE"];
               routeDr["DELIVER_LINE_ORDER"] = row["DELIVER_LINE_ORDER"];
               routeDr["UP_CODE"] = row["UP_CODE"];
               routeDr["ISACTIVE"] = row["ISACTIVE"];
               routeDr["ISALLOT"] = "0";
               ds.Tables["DWV_OUT_DELIVER_LINE"].Rows.Add(routeDr);
           }
           return ds;
       }

        /// <summary>
        /// 缓存中构建虚拟表
        /// </summary>
        /// <returns></returns>
        public DataSet GenerateEmptyTables()
        {
            DataSet ds = new DataSet();
            DataTable routeDt = ds.Tables.Add("DWV_OUT_DELIVER_LINE");
            routeDt.Columns.Add("DELIVER_LINE_CODE");
            routeDt.Columns.Add("LINE_TYPE");
            routeDt.Columns.Add("DELIVER_LINE_NAME");
            routeDt.Columns.Add("DIST_STA_CODE");
            routeDt.Columns.Add("DELIVER_LINE_ORDER");
            routeDt.Columns.Add("UP_CODE");
            routeDt.Columns.Add("ISACTIVE");
            routeDt.Columns.Add("ISALLOT");
            return ds;
        }
        #endregion
    }
}
