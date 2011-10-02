using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Download.Dao
{
    public class DownOutBillDao : BaseDao
    {
        #region 从营销系统下载数据

        /// <summary>
        /// 从营销系统下载出库单主表单据数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetOutBillMaster(string outBillNo)
        {
            string sql = string.Format("SELECT * FROM IC.V_WMS_OUT_ORDER WHERE {0}", outBillNo);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 从营销系统下载出库单明细表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetOutBillDetail(string outBillNo)
        {
            string sql = string.Format("SELECT * FROM IC.V_WMS_OUT_ORDER_DETAIL WHERE {0}", outBillNo);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 从营销系统查询主单
        /// </summary>
        /// <param name="billNo"></param>
        /// <param name="unite"></param>
        /// <returns></returns>
        public DataTable GetOutBillMaster(string billNo, string unite)
        {
            string sql = @"SELECT ORDER_ID AS BILLNO,ORDER_DATE AS BILLDATE,ORDER_TYPE AS BILLTYPE,QUANTITY_SUM AS QUANTITY_SUM,DETAIL_NUM AS DETAIL_NUM 
                           FROM IC.V_WMS_OUT_ORDER WHERE {0} AND ORDER_ID NOT IN ({1})";
            sql = string.Format(sql, billNo, unite);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 从营销系统查询明细单
        /// </summary>
        /// <param name="inBillNo"></param>
        /// <returns></returns>
        public DataTable GetOutBillDetailInfo(string inBillNo)
        {
            string sql = @"SELECT ORDER_DETAIL_ID AS ID,ORDER_ID AS BILLNO,BRAND_CODE AS PRODUCTCODE,BRAND_NAME AS PRODUCT_NAME,QUANTITY AS QUANTITY
                           FROM IC.V_WMS_OUT_ORDER_DETAIL WHERE ORDER_ID ='{0}'";
            sql = string.Format(sql, inBillNo);
            return this.ExecuteQuery(sql).Tables[0];
        }


        #endregion

        #region 操作数字化仓储数据

        /// <summary>
        /// 添加主表数据到表 WMS_OUT_BILLMASTER
        /// </summary>
        /// <param name="ds"></param>
        public void InsertOutBillMaster(DataSet ds)
        {
            BatchInsert(ds.Tables["WMS_OUT_BILLMASTER"], "WMS_OUT_BILLMASTER");
        }


        /// <summary>
        /// 添加明细表数据到表 WMS_OUT_BILLDETAIL
        /// </summary>
        /// <param name="ds"></param>
        public void InsertOutBillDetail(DataSet ds)
        {
            if (ds.Tables["WMS_OUT_BILLDETAILA"].Rows.Count > 0)
            {
                BatchInsert(ds.Tables["WMS_OUT_BILLDETAILA"], "WMS_OUT_BILLDETAIL");
            }
            //foreach (DataRow row in ds.Tables["WMS_OUT_BILLDETAILA"].Rows)
            //{
            //    string sql = string.Format("INSERT INTO WMS_OUT_BILLDETAIL([BILLNO],[BILLTYPE],[PRODUCTCODE],[PRICE],[QUANTITY],[OUTPUTQUANTITY],[UNITCODE],[MEMO])" +
            //                                 "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", row["BILLNO"], row["BILLTYPE"], row["PRODUCTCODE"], row["PRICE"], row["QUANTITY"], row["OUTPUTQUANTITY"], row["UNITCODE"], row["MEMO"]);
            //    this.ExecuteNonQuery(sql);
            //}
        }

        /// <summary>
        /// 查询数字仓储出库7天之内的单据
        /// </summary>
        /// <returns></returns>
        public DataTable GetOutBillNo()
        {
            string sql = "SELECT BILLNO FROM WMS_OUT_BILLMASTER WHERE BILLDATE>=DATEADD(DAY, -7, CONVERT(VARCHAR(14), GETDATE(), 112)) ORDER BY BILLDATE";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询合单后的单据号
        /// </summary>
        /// <returns></returns>
        public DataTable UniteBillNo()
        {
            string sql = "SELECT DOWNBILLINO,BILLINFO FROM WMS_DOWNBILL_INFO WHERE BILLTYPE='2'";
            return this.ExecuteQuery(sql).Tables[0];
        }

        #endregion

        #region 分拣系统接口数据


        //获取分拣线上单据
        public DataTable GetOrderGather(string orderDate, string batchNo)
        {
            string sql = "SELECT * FROM V_AS_SC_ORDER_GATHER where ORDERDATE='" + orderDate + "' and BATCHNO='" + batchNo + "'";
            return this.ExecuteQuery(sql).Tables[0];
        }

        //获取批次号
        public DataTable GetBatchNo()
        {
            string sql = "SELECT BATCHNO FROM V_AS_SC_ORDER_GATHER GROUP BY BATCHNO";
            return this.ExecuteQuery(sql).Tables[0];
        }

        public void InsertOutBillMaster(string billno, string billdate, string batchno, string billtype, string warehouse, string memo, decimal quantity)
        {
            string sql = "INSERT INTO WMS_OUT_BILLMASTER(BILLNO,BILLDATE,BILLTYPE,WH_CODE,STATUS,MEMO) VALUES('" + billno + "','" + DateTime.Now.ToString("yy-MM-dd") + "','" + billtype + "','" + warehouse + "','1','" + memo + "')";
            //"INSERT INTO DWV_IWMS_OUT_STORE_BILL(STORE_BILL_ID,BILL_TYPE,QUANTITY_SUM,CREATOR_CODE,CREATE_DATE,BILL_STATUS,IS_IMPORT) " +
            //"VALUES('" + billno.Substring(0, 12) + "','" + billtype + "','" + quantity + "','" + Environment.MachineName + "','" + DateTime.Now.ToString("yy-MM-dd") + "','10','0')";
            this.ExecuteNonQuery(sql);
        }

        public DataTable GetOutStoreDetailId()
        {
            string sql = String.Format("SELECT  TOP 1 STORE_BILL_DETAIL_ID FROM DWV_IWMS_OUT_STORE_BILL_DETAIL WHERE STORE_BILL_DETAIL_ID LIKE '{0}%' ORDER BY STORE_BILL_DETAIL_ID DESC", DateTime.Now.ToString("yyyyMMdd"));
            return this.ExecuteQuery(sql).Tables[0];
        }
        #endregion

        #region 中烟接口数据
        //插入主单
        public void InsertOutStoreBill(DataSet ds)
        {
            foreach (DataRow row in ds.Tables["DWV_IWMS_OUT_STORE_BILL"].Rows)
            {
                string sql = "insert into DWV_IWMS_OUT_STORE_BILL(STORE_BILL_ID,DIST_CTR_CODE,AREA_TYPE,QUANTITY_SUM,AMOUNT_SUM,DETAIL_NUM,CREATOR_CODE,CREATE_DATE," +
                    "IN_OUT_TYPE,BILL_TYPE,BILL_STATUS,IS_IMPORT) values('" + row["STORE_BILL_ID"] + "','" + row["DIST_CTR_CODE"] + "','" + row["AREA_TYPE"] + "'," +
                    "'" + row["QUANTITY_SUM"] + "','" + row["AMOUNT_SUM"] + "','" + row["DETAIL_NUM"] + "','" + row["CREATOR_CODE"] + "','" + row["CREATE_DATE"] + "','" + row["IN_OUT_TYPE"] + "','" + row["BILL_TYPE"] + "','" + row["BILL_STATUS"] + "'," +
                    "'" + row["IS_IMPORT"] + "')";
                this.ExecuteNonQuery(sql);
            }
        }

        //插入细单
        public void InsertOutStoreBillDetail(DataSet ds)
        {
            BatchInsert(ds.Tables["DWV_IWMS_OUT_STORE_BILL_DETAIL"], "DWV_IWMS_OUT_STORE_BILL_DETAIL");
        }

        //删除
        public void DeleteOutStoreBill(DataSet ds)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string sql = "delete DWV_IWMS_OUT_STORE_BILL_DETAIL where STORE_BILL_ID='" + row["BILLNO", DataRowVersion.Original] + "';" +
                    "delete DWV_IWMS_OUT_STORE_BILL where STORE_BILL_ID ='" + row["BILLNO", DataRowVersion.Original] + "'";
                this.ExecuteNonQuery(sql);
            }
        }

        #endregion

        #region 合单
        /// <summary>
        /// 查询一个空表
        /// </summary>
        /// <param name="outTable"></param>
        /// <returns></returns>
        public DataTable QueryOutMaterTable(string outTableName)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE 1=0", outTableName);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据单据号查询合单数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataTable GetOutDetailByBillNo(string billNoList)
        {
            string sql = @"SELECT PRODUCTCODE,SUM(QUANTITY) AS QUANTITY,SUM(OUTPUTQUANTITY) AS OUTPUTQUANTITY,UNITCODE,PRICE 
                           FROM WMS_OUT_BILLDETAIL WHERE BILLNO IN({0}) GROUP BY PRODUCTCODE,UNITCODE,PRICE";
            sql = string.Format(sql, billNoList);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 把数据插入到数据表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="table"></param>
        public void InsertTable(string tableName, DataTable table)
        {
            this.BatchInsert(table, tableName);
        }

        /// <summary>
        /// 根据单据号查询合单后的明细数据
        /// </summary>
        /// <param name="billno"></param>
        /// <returns></returns>
        public DataTable QueryOutDetailTable(string billno)
        {
            string sql = string.Format("SELECT * FROM WMS_OUT_BILLDETAIL WHERE BILLNO ='{0}'", billno);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询合单总金额和数量
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataTable GetCountQuantity(string billNoList)
        {
            string sql = string.Format("SELECT DIST_CTR_CODE,SUM(QUANTITY_SUM) AS QUANTITY,SUM(AMOUNT_SUM) AS AMOUNT_SUM FROM DWV_IWMS_OUT_STORE_BILL WHERE STORE_BILL_ID IN({0}) GROUP BY DIST_CTR_CODE", billNoList);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 清除一个星期（7天）以前没有作业的单据
        /// </summary>
        public void DeleteOutBillInfo()
        {
            string sql = @"DELETE WMS_OUT_BILLDETAIL WHERE BILLNO IN(
                            SELECT BILLNO FROM WMS_OUT_BILLMASTER WHERE BILLDATE<= DATEADD(DAY, -7, CONVERT(VARCHAR(14), GETDATE(), 112))AND STATUS=1) ";
            this.ExecuteNonQuery(sql);
            sql = @"DELETE FROM WMS_OUT_BILLMASTER WHERE BILLDATE<= DATEADD(DAY, -7, CONVERT(VARCHAR(14), GETDATE(), 112)) AND STATUS=1 ";
            this.ExecuteNonQuery(sql);            
        }
        #endregion
    }
}
