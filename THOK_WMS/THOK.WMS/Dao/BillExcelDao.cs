using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS
{
    public class BillExcelDao : BaseDao
    {
        /// <summary>
        /// 查询相应单据明细
        /// </summary>
        /// <param name="filter">查询范围</param>
        /// <param name="tableName">表（视图）名</param>
        /// <param name="billNo">单据号</param>
        /// <returns></returns>
        public DataTable QueryBillDetail(string filter,string tableName, string billNo)
        {
            string sql = string.Format("select {0} from {1} where BILLNO='{2}'", filter, tableName, billNo);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询相应单据明细分配
        /// </summary>
        /// <param name="tableName">表（视图）名</param>
        /// <param name="billNo">单据号</param>
        /// <param name="filter">单据类型【WMS_COMPARISON】</param>
        /// <returns></returns>
        public DataTable QueryBillAllot(string tableName,string billNo,string filter)
        {
            string sql = string.Format("select A.*,C.TEXT from {0} as A left join WMS_COMPARISON  as C on A.STATUS = C.VALUE where A.BILLNO='{1}' and C.FIELD='{2}'",
                tableName, billNo, filter);
            return this.ExecuteQuery(sql).Tables[0];

        }
    }
}
