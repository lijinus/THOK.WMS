using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Download.Dao
{
   public class DownOrgDao:BaseDao
    {
        /// <summary>
        /// 清除单位信息
        /// </summary>
        public void Delete()
        {
            string sql = "DELETE DWV_OUT_ORG";
            this.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 下载所属单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrgInfo()
        {
            string sql = " SELECT * FROM IC.V_WMS_ORG";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="dt"></param>
        public void Insert(DataTable dt)
        {
            this.BatchInsert(dt, "DWV_OUT_ORG");
        }
    }
}
