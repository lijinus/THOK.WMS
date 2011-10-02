using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Download.Dao
{
    public class DownInBillDao : BaseDao
    {
        #region 从营系统下载入库数据

        /// <summary>
        /// 查询营销系统入库单据主表
        /// </summary>
        /// <returns></returns>
        public DataTable GetInBillMaster(string inBillNoList)
        {
            string sql = string.Format("SELECT * FROM IC.V_WMS_IN_ORDER WHERE {0}", inBillNoList);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询营销系统入库明细表
        /// </summary>
        /// <returns></returns>
        public DataTable GetInBillDetail(string inBillNoList)
        {
            string sql = string.Format("SELECT * FROM IC.V_WMS_IN_ORDER_DETAIL WHERE {0}",inBillNoList);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 分页查询主单
        /// </summary>
        /// <param name="billNo"></param>
        /// <returns></returns>
        public DataTable GetInBillMasterByBillNo(string billNo)
        {
            string sql = string.Format("SELECT ORDER_ID AS BILLNO,ORDER_DATE AS BILLDATE,ORDER_TYPE AS BILLTYPE,QUANTITY_SUM AS QUANTITY_SUM,DETAIL_NUM AS DETAIL_NUM FROM IC.V_WMS_IN_ORDER WHERE ORDER_ID NOT IN ({0})", billNo);           
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 分页查询细单
        /// </summary>
        /// <param name="inBillNo">主单编号</param>
        /// <returns>DataTable</returns>
        public DataTable GetInBillDetailByBillNo(string inBillNo)
        {
            string sql = @"SELECT ORDER_DETAIL_ID AS ID,ORDER_ID AS BILLNO,BRAND_CODE AS PRODUCTCODE,BRAND_NAME AS PRODUCT_NAME,QUANTITY AS QUANTITY
                           FROM IC.V_WMS_IN_ORDER_DETAIL WHERE ORDER_ID='{0}'";
            sql = string.Format(sql, inBillNo);
            return this.ExecuteQuery(sql).Tables[0];
        }



        #endregion

        #region 操作数字化仓储数据

        /// <summary>
        /// 查询主表7天内单据
        /// </summary>
        /// <returns></returns>
        public DataTable GetBillNo()
        {
            string sql = "SELECT BILLNO FROM WMS_IN_BILLMASTER WHERE BILLDATE>=DATEADD(DAY, -7, CONVERT(VARCHAR(14), GETDATE(), 112)) ORDER BY BILLDATE";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 插入主表数据
        /// </summary>
        /// <param name="ds"></param>
        public void InsertInBillMaster(DataSet ds)
        {
            BatchInsert(ds.Tables["WMS_IN_BILLMASTER"], "WMS_IN_BILLMASTER");
        }

        /// <summary>
        /// 插入明细表数据
        /// </summary>
        /// <param name="ds"></param>
        public void InsertInBillDetail(DataSet ds)
        {
            BatchInsert(ds.Tables["WMS_IN_BILLDETAIL"], "WMS_IN_BILLDETAIL");
        }       
        #endregion

        #region 从中烟获取入库数据

        /// <summary>
        /// 插入主表数据到上报给中烟的数据表中
        /// </summary>
        /// <param name="ds"></param>
        public void InsertInStoreBill(DataSet ds)
        {
            foreach (DataRow row in ds.Tables["DWV_IWMS_IN_STORE_BILL"].Rows)
            {
                string sql = "iNSERT INTO DWV_IWMS_IN_STORE_BILL(STORE_BILL_ID,DIST_CTR_CODE,AREA_TYPE,QUANTITY_SUM,AMOUNT_SUM,DETAIL_NUM,CREATOR_CODE,CREATE_DATE," +
                   "IN_OUT_TYPE,BILL_TYPE,BILL_STATUS,IS_IMPORT) VALUES('" + row["STORE_BILL_ID"] + "','" + row["DIST_CTR_CODE"] + "','" + row["AREA_TYPE"] + "'," +
                   "'" + row["QUANTITY_SUM"] + "','" + row["AMOUNT_SUM"] + "','" + row["DETAIL_NUM"] + "','" + row["CREATOR_CODE"] + "','" + row["CREATE_DATE"] + "','" + row["IN_OUT_TYPE"] + "','" + row["BILL_TYPE"] + "','" + row["BILL_STATUS"] + "'," +
                   "'" + row["IS_IMPORT"] + "')";
                this.ExecuteNonQuery(sql);
            }
        }

        /// <summary>
        /// 插入明细表数据到上报给中烟的数据表中
        /// </summary>
        /// <param name="ds"></param>
        public void InsertInStoreBillDetail(DataSet ds)
        {
            BatchInsert(ds.Tables["DWV_IWMS_IN_STORE_BILL_DETAIL"], "DWV_IWMS_IN_STORE_BILL_DETAIL");
        }

        #endregion

        #region 分拣管理系统数据库

        public DataTable ReturnInBillInfo()
        {
            string sql = "select ID,CIGARETTECODE,CIGARETTENAME,QUANTITY from AS_SC_BALANCE_OUT where ISSTOCKIN='0'";
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdateReturnInBilLState(string idList, string state)
        {
            string sql = "UPDATE AS_SC_BALANCE_OUT SET ISSTOCKIN='" + state + "' where ID in (" + idList + ")";
            ExecuteNonQuery(sql);
        }

        public int ReturnInBillCount()
        {
            string sql = "select count(*) from AS_SC_BALANCE_OUT where ISSTOCKIN='0'";
            return (int)ExecuteScalar(sql);
        }

        public DataTable ReturnInBill()
        {
            string sql = "select DOWNBILLINO,BILLINFO from WMS_DOWNBILL_INFO where BILLTYPE='1'";
            return this.ExecuteQuery(sql).Tables[0];
        }

        //记录所有分拣线退货入库的信息
        public void Insert(string billNo, string id)
        {
            string sql = "insert into WMS_DOWNBILL_INFO(DOWNBILLINO,BILLINFO,BILLTYPE) values('" + id + "','" + billNo + "','1')";
            this.ExecuteNonQuery(sql);
        }

        public void Delete(string billNo)
        {
            string sql = "DELETE FROM WMS_DOWNBILL_INFO WHERE DOWNBILLINO='" + billNo + "'";
            this.ExecuteNonQuery(sql);
        }

        public string SelectIdList(string billNo)
        {
            string sql = "select BILLINFO from WMS_DOWNBILL_INFO where DOWNBILLINO='" + billNo + "'";
            return (string)ExecuteScalar(sql);
        }


        public void InsertReturnInBillMaster(string billNo, string memo)
        {
            string sql = "insert into WMS_IN_BILLMASTER(BILLNO,BILLDATE,WH_CODE,STATUS,MEMO)" +
                "values('" + billNo + "','" + DateTime.Now.ToString("yyMMdd") + "','001','1','" + memo + "')";
            this.ExecuteNonQuery(sql);
        }

        #endregion
    }
}
