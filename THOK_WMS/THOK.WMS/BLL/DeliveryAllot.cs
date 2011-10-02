using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
    public class DeliveryAllot
    {
        /// <summary>
        /// zxl 2011-06-22 可出库货位
        /// </summary>
        /// <returns></returns>
        public DataTable FindAvailableCell()
        {
            string sql = @"SELECT [CELL_ID],[SHELFCODE] ,[CELLCODE],[CELLNAME],[MAX_QUANTITY], C.UNITCODE ,U.UNITNAME ,INPUTDATE 
                            ,ASSIGNEDPRODUCT ,CURRENTPRODUCT ,P.PRODUCTNAME,QUANTITY ,0.00 AS ALLOTEDQUANTITY,QUANTITY-FROZEN_OUT_QTY AS AVAILQTY
                            ,'' AS TASKID,'' AS BILLNO, 0.00 AS DETAILID,'' AS PALLETID,'' AS NEWPALLETID
                            FROM [WMS_WH_CELL] C
                            LEFT JOIN WMS_UNIT U ON U.UNITCODE=C.UNITCODE
                            LEFT JOIN WMS_PRODUCT P ON P.PRODUCTCODE=C.CURRENTPRODUCT
                            WHERE C.ISACTIVE='1'                                     --储位是否可用
                            AND ISLOCKED='0'                                         --储位是否冻结
                            AND QUANTITY-FROZEN_OUT_QTY >0                           --储位可出库量
                            ORDER BY INPUTDATE,CURRENTPRODUCT";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }



        public DataTable GetJianCell()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = " SELECT [CELL_ID],[SHELFCODE] ,[CELLCODE],[CELLNAME], C.UNITCODE ,U.UNITNAME ,INPUTDATE " +
                             "  ,CURRENTPRODUCT ,P.PRODUCTNAME,QUANTITY ,0.00 AS ALLOTEDQUANTITY,QUANTITY-FROZEN_OUT_QTY AS AVAILQTY" +
                             " ,'' AS TASKID,'' AS BILLNO, 0.00 AS DETAILID,'' AS PALLETID,'' AS NEWPALLETID"+      
                             " FROM [WMS_WH_CELL] C"+
                             " LEFT JOIN WMS_UNIT U ON U.UNITCODE=C.UNITCODE"+
                             " LEFT JOIN WMS_PRODUCT P ON P.PRODUCTCODE=C.CURRENTPRODUCT"+
                             " WHERE C.ISACTIVE='1' AND ISLOCKED='0' AND QUANTITY>0 AND  QUANTITY>FROZEN_OUT_QTY" +
                             " AND (U.UNITNAME LIKE '%件%'or U.UNITNAME LIKE '%箱%') " +
                             " ORDER BY INPUTDATE,QUANTITY";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }

       #region
        
        
        //*********2010-12-13*********件烟从主库区出，条烟从零烟柜出******************************
        public DataTable GetJianCell1()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = " SELECT [CELL_ID],[SHELFCODE] ,[CELLCODE],[CELLNAME],AREATYPE,LAYER_NO, C.UNITCODE ,U.UNITNAME ,INPUTDATE,convert(varchar(10),INPUTDATE,120) AS DATE " +
                             "  ,CURRENTPRODUCT ,P.PRODUCTNAME,QUANTITY ,0.00 AS ALLOTEDQUANTITY ,QUANTITY-FROZEN_OUT_QTY AS AVAILQTY" +
                             " ,'' AS TASKID,'' AS BILLNO, 0.00 AS DETAILID,'' AS PALLETID,'' AS NEWPALLETID" +
                             " FROM [WMS_WH_CELL] C" +
                             " LEFT JOIN WMS_UNIT U ON U.UNITCODE=C.UNITCODE" +
                             " LEFT JOIN WMS_PRODUCT P ON P.PRODUCTCODE=C.CURRENTPRODUCT" +
                             " WHERE C.ISACTIVE='1' AND ISLOCKED='0' AND QUANTITY>0 AND  QUANTITY>FROZEN_OUT_QTY" +
                             " AND (AREATYPE='0' OR AREATYPE='3')" +
                             " ORDER BY AREATYPE desc,DATE,QUANTITY desc";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }
        /// <summary>
        /// 备货烟区出烟
        /// </summary>
        /// <returns></returns>
        public DataTable GetJianCell2()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = " SELECT [CELL_ID],[SHELFCODE] ,[CELLCODE],[CELLNAME], C.UNITCODE ,U.UNITNAME ,INPUTDATE " +
                             "  ,CURRENTPRODUCT ,P.PRODUCTNAME,QUANTITY ,0.00 AS ALLOTEDQUANTITY,QUANTITY-FROZEN_OUT_QTY AS AVAILQTY" +
                             " ,'' AS TASKID,'' AS BILLNO, 0.00 AS DETAILID,'' AS PALLETID,'' AS NEWPALLETID" +
                             " FROM [WMS_WH_CELL] C" +
                             " LEFT JOIN WMS_UNIT U ON U.UNITCODE=C.UNITCODE" +
                             " LEFT JOIN WMS_PRODUCT P ON P.PRODUCTCODE=C.CURRENTPRODUCT" +
                             " WHERE C.ISACTIVE='1' AND ISLOCKED='0' AND QUANTITY>0 AND QUANTITY>FROZEN_OUT_QTY" +
                             " AND (U.UNITNAME LIKE '%件%'or U.UNITNAME LIKE '%箱%') AND AREATYPE='2'" +
                             " ORDER BY CURRENTPRODUCT,INPUTDATE,QUANTITY";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }
        public DataTable GetTiaoCell1()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = @" SELECT [CELL_ID],[SHELFCODE] ,[CELLCODE],[CELLNAME],[MAX_QUANTITY], C.UNITCODE ,U.UNITNAME ,INPUTDATE 
                              ,ASSIGNEDPRODUCT ,CURRENTPRODUCT ,P.PRODUCTNAME,QUANTITY ,0.00 AS ALLOTEDQUANTITY,QUANTITY-FROZEN_OUT_QTY AS AVAILQTY
                              ,'' AS TASKID,'' AS BILLNO, 0.00 AS DETAILID,'' AS PALLETID,'' AS NEWPALLETID
                              FROM [WMS_WH_CELL] C
                              LEFT JOIN WMS_UNIT U ON U.UNITCODE=C.UNITCODE
                              LEFT JOIN WMS_PRODUCT P ON P.PRODUCTCODE=C.CURRENTPRODUCT
                              WHERE C.ISACTIVE='1' AND ISLOCKED='0' AND QUANTITY>0 AND QUANTITY> FROZEN_OUT_QTY
                              AND U.UNITNAME not LIKE '%件%' AND U.UNITNAME not like '%箱%' AND AREATYPE='1'
                              ORDER BY INPUTDATE,CURRENTPRODUCT";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }
        /// <summary>
        /// ****修改，将产生的移库单保存起来 ****
        /// </summary>
        /// <param name="tableAllotMaster"></param>
        /// <param name="tableAllotDetail"></param>
        /// <returns></returns>
        public bool SaveAllotment(DataTable tableAllotMaster, DataTable tableAllotDetail, DataTable tableMoveDetail, DataTable tableMoveMaster)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DeliveryAllotDao dao = new DeliveryAllotDao();
                dao.BatchInsertAllotment(tableAllotMaster, tableAllotDetail, tableMoveDetail, tableMoveMaster);
                StringBuilder sb = new StringBuilder();
                foreach (DataRow r in tableAllotDetail.Rows)
                {
                    if (Convert.ToDecimal(r["QUANTITY"]) < 0.00M)
                    {
                        sb.Append(string.Format("update WMS_WH_CELL SET FROZEN_IN_QTY=FROZEN_IN_QTY-({0}) WHERE CELLCODE='{1}' ;"
                                                   , r["QUANTITY"].ToString(), r["CELLCODE"].ToString()));
                    }
                    else
                    {
                        sb.Append(string.Format("update WMS_WH_CELL SET FROZEN_OUT_QTY=FROZEN_OUT_QTY+{0} WHERE CELLCODE='{1}' ;"
                                                   , r["QUANTITY"].ToString(), r["CELLCODE"].ToString()));
                    }
                }
                try
                {
                    dao.SetData(sb.ToString()); return true;
                }
                catch
                {
                    foreach (DataRow r2 in tableAllotMaster.Rows)
                    {
                        sb = new StringBuilder();
                        sb.Append(string.Format("Delete from WMS_OUT_ALLOTDETAIL WHERE BILLNO='{0}';Delete from WMS_OUT_ALLOTMASTER WHERE BILLNO='{0}';Delete from WMS_MOVE_BILLMASTER WHERE BILLNO='{1}';Delete from WMS_MOVE_BILLDETAIL WHERE BILLNO='{1}';", r2["BILLNO"].ToString(), r2["BILLNO"].ToString() + "M"));
                    }
                    dao.SetData(sb.ToString()); return true;
                }

            }
        }
        //获取将出库货物的库存数量 2010-12-8
        public DataTable GetQuantity(string currentproduct)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                //string sql = "SELECT ISNULL(SUM(QUANTITY),0) AS QUANTITY FROM WMS_WH_CELL WHERE CURRENTPRODUCT='" + currentproduct + "'";
                string sql = "SELECT ISNULL(SUM(C.QUANTITY*U.STANDARDRATE),0) AS QUANTITY FROM WMS_WH_CELL AS C LEFT JOIN WMS_UNIT AS U ON U.UNITCODE=C.UNITCODE WHERE CURRENTPRODUCT='" + currentproduct + "'";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }

        //
        public DataTable Rate(string unitCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                string sql = "SELECT STANDARDRATE FROM WMS_UNIT WHERE UNITCODE='" + unitCode + "'";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.Rate(sql).Tables[0];
            }
        }

        /// <summary>
        /// 移库单；主表空表
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmptyMoveMasterTable()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = "SELECT * FROM WMS_MOVE_BILLMASTER where 1=0";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }
        /// <summary>
        /// 移库单；明细表空表
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmptyMoveDetailTable()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = "SELECT * FROM WMS_MOVE_BILLDETAIL where 1=0";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }
       #endregion
        public DataTable GetTiaoCell()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = @" SELECT [CELL_ID],[SHELFCODE] ,[CELLCODE],[CELLNAME],[MAX_QUANTITY], C.UNITCODE ,U.UNITNAME ,INPUTDATE 
                              ,ASSIGNEDPRODUCT ,CURRENTPRODUCT ,P.PRODUCTNAME,QUANTITY ,0.00 AS ALLOTEDQUANTITY,QUANTITY-FROZEN_OUT_QTY AS AVAILQTY,MAX_QUANTITY-QUANTITY- FROZEN_IN_QTY AS AVAILINQTY
                              ,'' AS TASKID,'' AS BILLNO, 0.00 AS DETAILID,'' AS PALLETID,'' AS NEWPALLETID
                              FROM [WMS_WH_CELL] C
                              LEFT JOIN WMS_UNIT U ON U.UNITCODE=C.UNITCODE
                              LEFT JOIN WMS_PRODUCT P ON P.PRODUCTCODE=C.CURRENTPRODUCT
                              WHERE C.ISACTIVE='1' AND ISLOCKED='0' 
                              AND U.UNITNAME not LIKE '%件%' AND U.UNITNAME not like '%箱%' AND AREATYPE='1'
                              ORDER BY CURRENTPRODUCT,INPUTDATE";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }

        /// <summary>
        /// 获取分配主表空表
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmptyAllotMasterTable()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = "SELECT * FROM WMS_OUT_ALLOTMASTER where 1=0";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }

        /// <summary>
        /// 获取分配明细空表
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmptyAllotDetailTable()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = "SELECT * FROM WMS_OUT_ALLOTDETAIL where 1=0";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }

        /// <summary>
        /// 获取分配明细空表
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmptyAllotmentTable()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = "SELECT [ID] ,[TASKID],[BILLNO],[DETAILID]"+
                             " ,[PRODUCTCODE],'' AS PRODUCTNAME,'' AS UNITCODE,'' AS UNITNAME" +
                             "  ,[CELLCODE],'' AS CELLNAME ,[QUANTITY],[OUTPUTQUANTITY]"+
                             "  ,[PALLETID] ,[NEWPALLETID] ,[OPERATEPERSON]"+
                             "  ,[STARTTIME],[FINISHTIME] ,[STATUS],[INPUTDATE],[OUTPUTDATE]"+
                             " FROM [WMS_OUT_ALLOTDETAIL] where 1=0";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }
        /// <summary>
        /// 获取产生的移库单明细空表
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmptyMoveTable()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = "SELECT [ID],[BILLNO]" +
                             " ,[PRODUCTCODE],'' AS PRODUCTNAME,'' AS UNITCODE,'' AS UNITNAME" +
                             "  ,[OUT_CELLCODE],'' AS OUT_CELLNAME ,[IN_CELLCODE],'' AS IN_CELLNAME ,[QUANTITY]" +
                             "  ,[OPERATEPERSON]" +
                             "  ,[STARTTIME],[FINISHTIME] ,[STATUS]" +
                             " FROM [WMS_MOVE_BILLDETAIL] where 1=0";
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }
        /// <summary>
        /// 保存分配结果,更新货位出库冻结量
        /// </summary>
        /// <param name="tableAllotment"></param>
        /// <returns></returns>
        public bool SaveAllotment(DataTable tableAllotMaster,DataTable tableAllotDetail)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DeliveryAllotDao dao = new DeliveryAllotDao();
              
                dao.BatchInsertAllotment(tableAllotMaster, tableAllotDetail);
                StringBuilder sb = new StringBuilder();
                foreach (DataRow r in tableAllotDetail.Rows)
                {
                    if (Convert.ToDecimal(r["QUANTITY"]) < 0.00M)
                    {
                        sb.Append(string.Format("update WMS_WH_CELL SET FROZEN_IN_QTY=FROZEN_IN_QTY-({0}) WHERE CELLCODE='{1}' ;"
                                                   , r["QUANTITY"].ToString(), r["CELLCODE"].ToString()));
                    }
                    else
                    {
                        sb.Append(string.Format("update WMS_WH_CELL SET FROZEN_OUT_QTY=FROZEN_OUT_QTY+{0} WHERE CELLCODE='{1}' ;"
                                                   , r["QUANTITY"].ToString(), r["CELLCODE"].ToString()));
                    }
                }
                try
                {
                    dao.SetData(sb.ToString()); return true;
                }
                catch
                {
                    foreach (DataRow r2 in tableAllotMaster.Rows)
                    {
                        sb = new StringBuilder();
                        sb.Append(string.Format("Delete from WMS_OUT_ALLOTDETAIL WHERE BILLNO='{0}';Delete from WMS_OUT_ALLOTMASTER WHERE BILLNO='{0}';", r2["BILLNO"].ToString()));
                    }
                    dao.SetData(sb.ToString()); return true;
                }

            }
        }
       

        /// <summary>
        /// 删除分配结果,更新入库单状态,  更新货位入库冻结量
        /// </summary>
        /// <param name="BillNO"></param>
        /// <returns></returns>
        public bool DeleteAllotment(string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryAllotDao dao = new EntryAllotDao();
                DataSet ds = dao.GetData("SELECT * FROM WMS_OUT_ALLOTDETAIL WHERE BILLNO='" + BillNo + "'");

                StringBuilder sb = new StringBuilder();
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (Convert.ToDecimal(r["QUANTITY"]) < 0.00M)
                    {
                        sb.Append(string.Format("update WMS_WH_CELL SET FROZEN_IN_QTY=FROZEN_IN_QTY+{0} WHERE CELLCODE='{1}' ;"
                                                   , r["QUANTITY"].ToString(), r["CELLCODE"].ToString()));
                    }
                    else
                    {
                        sb.Append(string.Format("update WMS_WH_CELL SET FROZEN_OUT_QTY=FROZEN_OUT_QTY-{0} WHERE CELLCODE='{1}' ;"
                                                   , r["QUANTITY"].ToString(), r["CELLCODE"].ToString()));
                    }
                }
                sb.Append(string.Format("DELETE FROM WMS_OUT_ALLOTDETAIL WHERE BILLNO='{0}';DELETE FROM WMS_OUT_ALLOTMASTER WHERE BILLNO='{0}'; update WMS_OUT_BILLMASTER SET STATUS='2' where BILLNO='{0}'", BillNo));
                dao.SetData(sb.ToString());
                return true;
            }
        }


        private string strTableView = "V_WMS_OUT_ALLOTDETAIL";
        private string strPrimaryKey = "ID";
        //private string strOrderByFields = "BILLNO,TASKID,ID";
        private string strQueryFields = "*";

        public DataSet QueryAllotment(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, "BILLNO,TASKID,ID", filter, strTableView);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DeliveryAllotDao dao = new DeliveryAllotDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        /// <summary>
        /// 在出库自动分配货位的时候修改分拣区的货位时间
        /// </summary>
        public void UpdateBhDate()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                DeliveryAllotDao dao = new DeliveryAllotDao();
                string sql = "UPDATE WMS_WH_CELL SET INPUTDATE=GETDATE() WHERE CELLCODE NOT IN(SELECT CELLCODE FROM WMS_WH_CELL WHERE CURRENTPRODUCT IS NULL AND AREATYPE=2) AND AREATYPE=2";
                dao.SetData(sql);
            }
        }

        
    }
}
