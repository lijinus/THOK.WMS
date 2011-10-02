using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Download.Dao
{
    public class DownUnitDao : BaseDao
    {
        #region 从营系统据下载单位数据

        /// <summary>
        /// 下载单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnitInfo(string unitCode)
        {
            string sql = string.Format("SELECT * FROM IC.V_WMS_BRAND_UNIT WHERE {0}", unitCode);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询计量单位系列表
        /// </summary>
        /// <param name="ulistCode"></param>
        /// <returns></returns>
        public DataTable GetBrandUlistInfo(string ulistCode)
        {
            string sql = string.Format("SELECT * FROM IC.V_WMS_BRAND_ULIST WHERE {0}", ulistCode);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 把下载的数据插入数据库
        /// </summary>
        /// <param name="ds"></param>
        public void InsertUnit(DataSet ds)
        {
            BatchInsert(ds.Tables["WMS_UNIT_INSERT"], "WMS_UNIT");
        }

        public void InsertUlist(DataTable ulistCodeTable)
        {
            BatchInsert(ulistCodeTable, "WMS_BRAND_ULIST");
        }

        /// <summary>
        /// 把下载的中间表数据插入数据库
        /// </summary>
        /// <param name="ds"></param>
        public void InsertUnitProduct(DataSet ds)
        {
            BatchInsert(ds.Tables["WMS_UNIT_PRODUCT"], "WMS_UNIT_PRODUCT");
        }

        /// <summary>
        /// 查询仓储单位系列编号
        /// </summary>
        /// <returns></returns>
        public DataTable GetUlistCode()
        {
            string sql = "SELECT BRAND_ULIST_CODE FROM WMS_BRAND_ULIST";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询仓储单位编号
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnitCode()
        {
            string sql = "SELECT UNITCODE FROM WMS_UNIT";
            return this.ExecuteQuery(sql).Tables[0];
        }


        public DataTable GetUnitProduct()
        {
            string sql = "SELECT PRODUCTCODE FROM WMS_UNIT_PRODUCT WHERE UNITCODE LIKE '04%'";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查计量单位件单位
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductByUnitCodeTiao()
        {
            string sql = "SELECT * FROM DBO.WMS_UNIT WHERE UNITNAME LIKE '条%'";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据计量单位系列编号查询数据
        /// </summary>
        /// <param name="ulistCode"></param>
        /// <returns></returns>
        public DataTable FindUnitCodeByUlistCode(string ulistCode)
        {
            string sql = string.Format("SELECT * FROM WMS_BRAND_ULIST WHERE BRAND_ULIST_CODE='{0}'", ulistCode);
            return this.ExecuteQuery(sql).Tables[0];
        }
     
        #endregion

        #region 从营系统据下载单位数据-广西浪潮

        /// <summary>
        /// 下载单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnitInfo()
        {
            string sql = "SELECT BRAND_CODE,BRAND_UNIT_CODE,BRAND_UNIT_NAME,COUNT FROM V_WMS_BRAND_UNIT";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查计量单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductByUnitCode(string unitcode, string product)
        {
            string sql = string.Format("SELECT * FROM WMS_UNIT_PRODUCT WHERE UNITCODE LIKE '{0}%' AND PRODUCTCODE='{1}'", unitcode, product);
            return this.ExecuteQuery(sql).Tables[0];
        }
        #endregion
    }
}
