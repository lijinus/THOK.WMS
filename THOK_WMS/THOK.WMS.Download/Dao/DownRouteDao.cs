using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Download.Dao
{
    public class DownRouteDao : BaseDao
    {
        #region 下载送货线路信息

        /// <summary>
        /// 下载送货线路表信息
        /// </summary>
        public DataTable GetRouteInfo(string routeCodeList)
        {
            string sql = string.Format("SELECT * FROM IC.V_WMS_DELIVER_LINE WHERE {0}",routeCodeList);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 下载送货线路表信息
        /// </summary>
        public DataTable GetRouteInfo()
        {
            string sql = "SELECT * FROM IC.V_WMS_DELIVER_LINE ";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询线路表中的线路代码
        /// </summary>
        /// <returns></returns>
        public DataTable GetRouteCode()
        {
            string sql = " SELECT DELIVER_LINE_CODE FROM DWV_OUT_DELIVER_LINE";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 把线路信息插入数据库
        /// </summary>
        /// <param name="ds"></param>
        public void Insert(DataSet ds)
        {
            BatchInsert(ds.Tables["DWV_OUT_DELIVER_LINE"], "DWV_OUT_DELIVER_LINE");
        }

        /// <summary>
        /// 清除送货线路表
        /// </summary>
        public void Delete()
        {
            string sql = "DELETE DWV_OUT_DELIVER_LINE";
            this.ExecuteNonQuery(sql);
        }

        #endregion
    }
}
