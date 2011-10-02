using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Download.Dao
{
    public class DownOrdDistDao : BaseDao
    {
        /// <summary>
        /// 下载营销的配车单主表数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetDistBillMaster(string bistBillId)
        {
            string sql = string.Format(" SELECT * FROM IC.V_WMS_DIST_BILL WHERE {0} ", bistBillId);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 下载营销的配车单细表数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetDistBillDetail(string bistBillId)
        {
            string sql = string.Format(" SELECT * FROM IC.V_WMS_DIST_BILL_DETAIL WHERE {0} ", bistBillId);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询已下载过的配车单编码
        /// </summary>
        /// <returns></returns>
        public DataTable QueryOrgDistCode()
        {
            string sql = "SELECT DIST_BILL_ID FROM DWV_ORD_DIST_BILL";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 保存配车单信息到数字仓储
        /// </summary>
        /// <param name="bistBillTable"></param>
        public void Insert(DataTable bistBillMasterTable,DataTable bistBillDetailTable)
        {
            this.BatchInsert(bistBillMasterTable, "WMS_DIST_BILL");
            this.BatchInsert(bistBillDetailTable, "WMS_DIST_BILL_DETAIL");
        }
    }
}
