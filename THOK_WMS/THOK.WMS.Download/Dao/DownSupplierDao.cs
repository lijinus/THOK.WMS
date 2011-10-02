using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Download.Dao
{
    public class DownSupplierDao : BaseDao
    {
        #region 从浪潮营系统据下载供应商数据

        /// <summary>
        /// 下载供应商数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetSupplierInfo(string spplierCode)
        {
            string sql = string.Format("SELECT * FROM IC.V_WMS_FACTORY WHERE {0}",spplierCode);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 把数据插入数据库
        /// </summary>
        /// <param name="ds"></param>
        public void Insert(DataSet ds)
        {
            BatchInsert(ds.Tables["BI_SUPPLIER_INSERT"], "BI_SUPPLIER");
        }

        /// <summary>
        /// 查询供应商编号
        /// </summary>
        /// <returns></returns>
        public DataTable GetSupplierCode()
        {
            string sql = "SELECT SUPPLIERCODE FROM BI_SUPPLIER";
            return this.ExecuteQuery(sql).Tables[0];
        }
        #endregion
    }
}
