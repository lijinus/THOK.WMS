using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Download.Dao
{
    public class DownProductDao : BaseDao
    {
        #region 从营系统据下载产品信息

        /// <summary>
        /// 下载卷烟产品信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductInfo(string codeList)
        {
            string sql = string.Format(" SELECT * FROM IC.V_WMS_BRAND WHERE {0}", codeList);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="ds"></param>
        public void Insert(DataSet ds)
        {
            BatchInsert(ds.Tables["DWV_IINF_BRAND"], "DWV_IINF_BRAND");
            BatchInsert(ds.Tables["WMS_PRODUCT"], "WMS_PRODUCT");
        }

        #endregion


        /// <summary>
        /// 查询数字仓储产品编号
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductCode()
        {
            string sql = "SELECT PRODUCTCODE FROM WMS_PRODUCT";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据编码筛选查询
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetProductCode(string code)
        {
            string sql = string.Format("SELECT TOP 10 PRODUCTCODE FROM WMS_PRODUCT WHERE PRODUCTCODE LIKE '{0}%'", code);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询卷烟产品信息
        /// </summary>
        /// <returns></returns>
        public DataTable ProductInfo()
        {
            string sql = "SELECT * FROM WMS_PRODUCT";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 支转换为件的换算
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public DataTable DownProductRate(string productCode)
        {
            string sql = @"SELECT A.PRODUCTCODE,A.PRODUCTNAME,A.UNITCODE,A.JIANCODE,A.TIAOCODE,
                (SELECT B.STANDARDRATE FROM WMS_PRODUCT AS A,WMS_UNIT AS B WHERE A.JIANCODE=B.UNITCODE AND A.PRODUCTCODE='{0}') AS JIANRATE,
                (SELECT B.STANDARDRATE FROM WMS_PRODUCT AS A,WMS_UNIT AS B WHERE A.TIAOCODE=B.UNITCODE AND A.PRODUCTCODE='{0}') AS TIAORATE 
                 FROM WMS_PRODUCT AS A,WMS_UNIT AS B WHERE  A.PRODUCTCODE='{0}' GROUP BY A.PRODUCTCODE,A.PRODUCTNAME,A.UNITCODE,A.JIANCODE,A.TIAOCODE";
            sql = string.Format(sql, productCode);
            return this.ExecuteQuery(sql).Tables[0];
        }
    }
}
