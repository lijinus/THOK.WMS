using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Download.Dao
{
    public class DownReachDao : BaseDao
   {
       #region 下载送货区域

       /// <summary>
       /// 下载送货区域
       /// </summary>
       /// <returns></returns>
       public DataTable GetReachInfo()
       {
           string sql = " SELECT * FROM IC.V_WMS_DIST_STATION";
           return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// 根据送货编码下载送货区域
       /// </summary>
       /// <returns></returns>
       public DataTable GetReachInfo(string reachCode)
       {
           string sql = string.Format(" SELECT * FROM IC.V_WMS_DIST_STATION WHERE {0}", reachCode);
           return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// 查询送货编码
       /// </summary>
       /// <returns></returns>
       public DataTable QueryReachCode()
       {
           string sql = " SELECT DIST_STA_CODE FROM DWV_OUT_DIST_STATION";
           return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// 添加数据到数据库
       /// </summary>
       /// <param name="reachDt"></param>
       public void Insert(DataTable reachDt)
       {
           this.BatchInsert(reachDt, "DWV_OUT_DIST_STATION");
       }

       /// <summary>
       /// 清除送货区域表
       /// </summary>
       public void Delete()
       {
          string sql = "DELETE DWV_OUT_DIST_STATION";
          this.ExecuteNonQuery(sql);
       }


       #endregion
   }
}
