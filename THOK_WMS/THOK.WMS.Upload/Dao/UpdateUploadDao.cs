using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Upload.Dao
{
    public class UpdateUploadDao:BaseDao
    {
        /// <summary>
        /// 根据单据号和产品名称查询上报出入库明细表信息
        /// </summary>
        /// <param name="billno"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public DataTable GetByOutInfo(string billno, string product, string tableName)
        {
            string sql = string.Format("SELECT * FROM {2} WHERE STORE_BILL_ID='{0}' AND BRAND_CODE='{1}'", billno, product, tableName);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询当前库存件的条数
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public decimal FindPieceQuantity(string product, string areaType)
        {
            string sql = @"SELECT ISNULL(SUM(QTY_STA),0)/(SELECT STANDARDRATE FROM WMS_UNIT U
                            LEFT JOIN WMS_PRODUCT P ON U.UNITCODE=P.TIAOCODE
                            WHERE P.PRODUCTCODE =C.CURRENTPRODUCT) AS QUANTITY FROM V_WMS_WH_CELL C
                            WHERE CURRENTPRODUCT='{0}' AND AREATYPE='{1}' GROUP BY CURRENTPRODUCT";
            sql = string.Format(sql, product, areaType);
            return Convert.ToDecimal(this.ExecuteScalar(sql));
        }

        /// <summary>
        /// 查询当前库存的条数
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public decimal FindBarQuantity(string product, string areaType)
        {
            string sql = @"SELECT ISNULL(SUM(QUANTITY),0) AS QUANTITY FROM V_WMS_WH_CELL WHERE CURRENTPRODUCT='{0}'AND AREATYPE='{1}'";
            sql = string.Format(sql, product, areaType);
            return Convert.ToDecimal(this.ExecuteScalar(sql));
        }

        /// <summary>
        /// 根据单据号查询上报出入库主表信息
        /// </summary>
        /// <param name="billno"></param>
        /// <returns></returns>
        public DataTable GetByOutInfo(string billno, string tableName)
        {
            string sql = string.Format("SELECT * FROM {1} WHERE STORE_BILL_ID='{0}'", billno, tableName);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据货位代码查询名称
        /// </summary>
        /// <param name="cellcode"></param>
        /// <returns></returns>
        public string GetCellCodeByName(string cellcode)
        {
            string sql = string.Format("SELECT CELLNAME FROM WMS_WH_CELL WHERE CELLCODE ='{0}'", cellcode);
            return this.ExecuteScalar(sql).ToString();
        }

        /// <summary>
        /// 根据传来的表名查询一个空表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryBusiBill(string outInfoTable)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE 1=0", outInfoTable);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 插入上传给中烟的出、入库业务表
        /// </summary>
        public void InsertBull(DataTable table, string outInfoTable)
        {
            BatchInsert(table, outInfoTable);
        }

        /// <summary>
        /// 根据日期和用户修改出、入库日结人员日期等。
        /// </summary>
        /// <param name="uase"></param>
        /// <param name="datetime"></param>
        public void UpdateDate(string uase, string datetime)
        {
            string sql = string.Format("UPDATE DWV_IWMS_OUT_BUSI_BILL SET RECKON_STATUS='1',RECKON_DATE='" + DateTime.Now.ToString("yyyyMMdd") + "',UPDATE_CODE='{0}',UPDATE_DATE='{1}'  WHERE IS_IMPORT='0'", uase, datetime);
            this.ExecuteNonQuery(sql);
            sql = string.Format("UPDATE DWV_IWMS_IN_BUSI_BILL SET RECKON_STATUS='1',RECKON_DATE='" + DateTime.Now.ToString("yyyyMMdd") + "',UPDATE_CODE='{0}',UPDATE_DATE='{1}'  WHERE IS_IMPORT='0'", uase, datetime);
            this.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 手动添加数据到中烟表
        /// </summary>
        /// <param name="masterTable"></param>
        /// <param name="table"></param>
        public void InsertMaster(string infoTable, DataTable table)
        {
            BatchInsert(table, infoTable);
        }

        /// <summary>
        /// 执行修改操作
        /// </summary>
        /// <param name="sql"></param>
        public void UpdateTable(string sql)
        {
            this.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 执行查询操作，返回一个值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetDate(string sql)
        {
            return Convert.ToString(this.ExecuteScalar(sql));
        }


        /// <summary>
        /// 获取产品比例
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public DataTable ProductRate(string productCode)
        {
            string sql = @"SELECT A.PRODUCTCODE,A.PRODUCTNAME,A.UNITCODE,A.JIANCODE,A.TIAOCODE,
                (SELECT B.STANDARDRATE FROM WMS_PRODUCT AS A,WMS_UNIT AS B WHERE A.JIANCODE=B.UNITCODE AND A.PRODUCTCODE='{0}') AS JIANRATE,
                (SELECT B.STANDARDRATE FROM WMS_PRODUCT AS A,WMS_UNIT AS B WHERE A.TIAOCODE=B.UNITCODE AND A.PRODUCTCODE='{0}') AS TIAORATE 
                 FROM WMS_PRODUCT AS A,WMS_UNIT AS B WHERE  A.PRODUCTCODE='{0}' GROUP BY A.PRODUCTCODE,A.PRODUCTNAME,A.UNITCODE,A.JIANCODE,A.TIAOCODE";
            sql = string.Format(sql, productCode);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 获取配送中心编码
        /// </summary>
        /// <returns></returns>
        public string GetCompany()
        {
            string sql = "SELECT DIST_CTR_CODE FROM DWV_OUT_DIST_CTR";
            return this.ExecuteScalar(sql).ToString();
        }

        public void SetData(string sql)
        {
            ExecuteNonQuery(sql);
        }
    }
}
