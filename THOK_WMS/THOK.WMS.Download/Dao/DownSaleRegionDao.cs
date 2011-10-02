using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Download.Dao
{
    public class DownSaleRegionDao : BaseDao
    {
        /// <summary>
        /// 清除营销区域信息
        /// </summary>
        public void Delete()
        {
            string sql = "DELETE WMS_SALE_REGION";
            this.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 下载营销区域信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetSaleInfo()
        {
            string sql = "SELECT * FROM IC.V_WMS_SALE_REGION";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="dt"></param>
        public void Insert(DataTable saleTable)
        {
            this.BatchInsert(saleTable, "WMS_SALE_REGION");
        }
    }
}
