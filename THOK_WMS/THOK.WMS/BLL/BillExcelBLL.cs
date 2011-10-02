using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using THOK.Util;
using THOK.WMS.Dao;

namespace THOK.WMS
{
    public class BillExcelBLL
    {
        /// <summary>
        /// 查询相应单据明细
        /// </summary>
        /// <param name="filter">查询范围</param>
        /// <param name="tableName">表名</param>
        /// <param name="billNo">单据号</param>
        /// <returns></returns>
        public DataTable QueryBillDetail(string filter,string tableName,string billNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BillExcelDao dao = new BillExcelDao();
                dao.SetPersistentManager(pm);
                return dao.QueryBillDetail(filter, tableName, billNo);
            }
        }


        public DataTable QueryBillAllot(string tableName, string billNo, string filter)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BillExcelDao dao = new BillExcelDao();
                dao.SetPersistentManager(pm);
                return dao.QueryBillAllot(tableName, billNo, filter);
            }
        }
    }
}
