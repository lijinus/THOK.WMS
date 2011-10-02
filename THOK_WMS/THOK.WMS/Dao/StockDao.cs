using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS
{
    public class StockDao : BaseDao
    {
        public DataTable RealTimeStock()
        {
            //string sql = "select * from V_REAL_TIME_STOCK ";
            string sql = "SELECT R.CURRENTPRODUCT, R.PRODUCTNAME,SUM(R.QUANTITY*R.STANDARDRATE)/"+
                "(SELECT U.STANDARDRATE FROM WMS_PRODUCT AS P LEFT JOIN WMS_UNIT AS U ON "+
                "U.UNITCODE=P.JIANCODE WHERE P.PRODUCTCODE=R.CURRENTPRODUCT) AS QUANTITY," +
                "P.UNITCODE,U.UNITNAME,'全部库区' AS AREANAME FROM V_REAL_TIME_STOCK AS R " +
                "LEFT JOIN WMS_PRODUCT AS P ON P.PRODUCTCODE= R.CURRENTPRODUCT " +
                "LEFT JOIN WMS_UNIT AS U ON U.UNITCODE= P.UNITCODE " +
                "GROUP BY R.CURRENTPRODUCT,R.PRODUCTNAME,P.UNITCODE,U.UNITNAME";
            return this.ExecuteQuery(sql).Tables[0];
        }

        public DataTable Query(string filter)
        {
            string sql = string.Format("select * from V_REAL_TIME_STOCK where {0}", filter);
            return this.ExecuteQuery(sql).Tables[0];
        }

        public DataSet QueryMaster(int pageIndex, int pageSize)
        {
            int preRec = (pageIndex - 1) * pageSize;
            //string sql = "SELECT * FROM V_WMS_IN_OUT_BILLMASTER";
            string sql = string.Format("SELECT TOP {0} * FROM V_WMS_IN_OUT_BILLMASTER WHERE BILLNO NOT IN (SELECT TOP {1} BILLNO FROM V_WMS_IN_OUT_BILLMASTER)", 
                pageSize,
                preRec.ToString());
            return this.ExecuteQuery(sql);
        }

        public DataSet QueryDetail(int pageIndex,int pageSize,string billNo)
        {
            int preRec = (pageIndex - 1) * pageSize;
            //string sql = "SELECT * FROM V_WMS_IN_OUT_BILLDETAIL WHERE BILLNO='" + billNo + "'";
            string sql = string.Format("SELECT TOP {0} * FROM V_WMS_IN_OUT_BILLDETAIL WHERE ID NOT IN (SELECT TOP {1} ID FROM V_WMS_IN_OUT_BILLDETAIL) AND BILLNO='{2}'",
                pageSize,
                preRec.ToString(),
                billNo);
            return this.ExecuteQuery(sql);
        }


        /// <summary>
        /// 查询库存
        /// </summary>
        /// <returns></returns>
        public DataTable QuerySockProduct(string file)
        {
            string sql = string.Format(@"SELECT * FROM V_CELLMOUNT WHERE {0}", file);
           return this.ExecuteQuery(sql).Tables[0];
        }
    }
}
