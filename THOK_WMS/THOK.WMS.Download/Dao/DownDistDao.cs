using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Download.Dao
{
    public class DownDistDao : BaseDao
    {
       #region 下载配送中心信息
       
       /// <summary>
       /// 下载配送中心信息
       /// </summary>
       /// <returns></returns>
       public DataTable GetDistInfo()
       {
           string sql = " SELECT * FROM IC.V_WMS_DIST_CTR";
           return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// 添加配送信息到数据库
       /// </summary>
       /// <param name="distTable"></param>
       public void Insert(DataTable distTable)
       {
           this.BatchInsert(distTable, "DWV_OUT_DIST_CTR");
       }

       #endregion

       #region 查询数字仓储配送中心信息

       /// <summary>
       /// 清除配送中心信息
       /// </summary>
       public void Delete()
       {
           string sql = "DELETE DWV_OUT_DIST_CTR";
           this.ExecuteNonQuery(sql);
       }

       /// <summary>
       /// 获取配送中心编码
       /// </summary>
       /// <returns></returns>
       public string GetCompany()
       {
           string sql = "SELECT COM_CODE FROM BI_COMPANY";
           return this.ExecuteScalar(sql).ToString();
       }

       #endregion
   }
}
