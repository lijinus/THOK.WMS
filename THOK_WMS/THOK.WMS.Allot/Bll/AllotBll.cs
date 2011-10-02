using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.Allot.Dao;
using THOK.WMS.Allot.Util;

namespace THOK.WMS.Allot.Bll
{
    public class AllotBll
    {
        /// <summary>
        /// zys_2011-06-21
        /// 张青龙 2011-07-18 加入WMS_WH_CELL.LAYER_NO
        /// </summary>
        /// <returns></returns>
        internal DataTable FindAvailableCell()
        {
            string sql = @"SELECT WMS_WAREHOUSE.WH_CODE,WMS_WAREHOUSE.WH_NAME,
                            WMS_WH_AREA.AREACODE,WMS_WH_AREA.AREANAME,
                            WMS_WH_SHELF.SHELFCODE,WMS_WH_SHELF.SHELFNAME,
                            WMS_WH_CELL.CELL_ID,WMS_WH_CELL.CELLCODE,WMS_WH_CELL.CELLNAME, 
                            WMS_WH_CELL.AREATYPE,WMS_WH_CELL.LAYER_NO,-- WMS_WH_AREA.ENTRYALLOTORDER,
                            WMS_WH_CELL.ASSIGNEDPRODUCT,WMS_WH_CELL.CURRENTPRODUCT,
                            WMS_WH_CELL.UNITCODE CELLUNITCODE,WMS_UNIT.UNITNAME CELLUNITNAME,WMS_WH_CELL.MAX_QUANTITY,
                            WMS_WH_CELL.MAX_QUANTITY-WMS_WH_CELL.FROZEN_IN_QTY-WMS_WH_CELL.QUANTITY AS AVAILABLE,
                            '' AS BILLNO,0.00 AS DETAILID,'' AS PRODUCTCODE,'' AS PRODUCTNAME,
                            '' AS UNITCODE,'' AS UNITNAME,0.00 AS ALLOTQUANTITY
                            FROM WMS_WH_CELL 
                            LEFT JOIN WMS_WH_SHELF ON WMS_WH_SHELF.SHELFCODE = WMS_WH_CELL.SHELFCODE 
                            LEFT JOIN WMS_WH_AREA ON WMS_WH_SHELF.AREACODE = WMS_WH_AREA.AREACODE
                            LEFT JOIN WMS_WAREHOUSE ON WMS_WAREHOUSE.WH_CODE = WMS_WH_AREA.WH_CODE
                            LEFT JOIN WMS_UNIT ON WMS_UNIT.UNITCODE = WMS_WH_CELL.UNITCODE
                            WHERE WMS_WH_CELL.ISACTIVE = '1'                                                            --储位是否可用
                                  AND WMS_WH_CELL.ISLOCKED='0'                                                          --储位是否冻结
                                  AND WMS_WH_CELL.MAX_QUANTITY - WMS_WH_CELL.QUANTITY - WMS_WH_CELL.FROZEN_IN_QTY > 0   --储位可入库量
                                  AND ((WMS_WH_CELL.CURRENTPRODUCT !='' AND WMS_WH_CELL.CURRENTPRODUCT IS NOT NULL) OR WMS_WH_CELL.FROZEN_IN_QTY = 0)                                                   --入库未入数量
                            --      AND WMS_WH_AREA.ENTRYALLOTORDER != 0 
                            ORDER BY -- WMS_WH_AREA.ENTRYALLOTORDER,
                            WMS_WH_CELL.AREATYPE,WMS_WH_CELL.SHELFCODE,WMS_WH_CELL.CELLCODE";

            using (PersistentManager persistentManager = new PersistentManager())
            {
                AllotDao dao = new AllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }

        /// <summary>
        /// zys_2011-06-21
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        internal string FindProductPieceUnitCode(string productCode)
        {
            string sql = @"SELECT U.UNITCODE FROM WMS_UNIT U 
                               LEFT JOIN WMS_PRODUCT P ON U.UNITCODE= P.JIANCODE WHERE P.PRODUCTCODE='{0}'";
            sql = string.Format(sql, productCode);

            using (PersistentManager persistentManager = new PersistentManager())
            {
                AllotDao dao = new AllotDao();
                return dao.ExecScaler(sql).ToString();
            }
        }

        /// <summary>
        /// zys_2011-06-21
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        internal string FindProductBarUnitCode(string productCode)
        {
            string sql = @"SELECT U.UNITCODE FROM WMS_UNIT U 
                               LEFT JOIN WMS_PRODUCT P ON U.UNITCODE= P.TIAOCODE WHERE P.PRODUCTCODE='{0}'";
            sql = string.Format(sql, productCode);

            using (PersistentManager persistentManager = new PersistentManager())
            {
                AllotDao dao = new AllotDao();
                return dao.ExecScaler(sql).ToString();
            }
        }

        /// <summary>
        /// zys_2011-06-21
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        internal decimal FindProductMaxQuantity(string productCode)
        {
            string sql = @"SELECT MAXCELLPIECE FROM WMS_PRODUCT WHERE PRODUCTCODE = '{0}'";
            sql = string.Format(sql, productCode);

            using (PersistentManager persistentManager = new PersistentManager())
            {
                AllotDao dao = new AllotDao();
                decimal returnVal = 0;
                decimal.TryParse(dao.ExecScaler(sql).ToString(), out returnVal);                
                return returnVal;
            }
        }

        /// <summary>
        /// zys_2011-06-22
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        internal string FindProductAssignedCell(string productCode)
        {
            string sql = @"SELECT SHELFCODE FROM WMS_WH_CELL 
                           WHERE ASSIGNEDPRODUCT='{0}' GROUP BY SHELFCODE";
            sql = string.Format(sql,productCode);

            using (PersistentManager persistentManager = new PersistentManager())
            {
                AllotDao dao = new AllotDao();
                DataTable shelfTable = dao.GetData(sql).Tables[0];
                string shelfList = StringUtil.StringMake(shelfTable, "SHELFCODE");
                return StringUtil.StringMake(shelfList);
            }
        }

        /// <summary>
        /// zys_2011-06-21
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        internal string FindUnitName(string unitCode)
        {
            string sql = @"SELECT UNITNAME FROM WMS_UNIT WHERE UNITCODE='{0}'";
            sql = string.Format(sql, unitCode);

            using (PersistentManager persistentManager = new PersistentManager())
            {
                AllotDao dao = new AllotDao();
                string returnVal = dao.ExecScaler(sql).ToString();
                return returnVal;
            }
        }

        /// <summary>
        /// zys_2011-06-21
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        internal decimal FindUnitStandardRate(string unitCode)
        {
            string sql = @"SELECT STANDARDRATE FROM WMS_UNIT WHERE UNITCODE='{0}'";
            sql = string.Format(sql,unitCode);

            using (PersistentManager persistentManager = new PersistentManager())
            {
                AllotDao dao = new AllotDao();
                decimal returnVal = 0;
                decimal.TryParse(dao.ExecScaler(sql).ToString(), out returnVal);
                return returnVal;
            }
        }
        
        /// <summary>
        /// zys_2011-06-22
        /// </summary>
        /// <returns></returns>
        internal bool FindIsUseArea(string areaType)
        {
            string sql = @"SELECT * FROM WMS_WH_AREA WHERE AREATYPE = '{0}'";
            sql = string.Format(sql,areaType);

            using (PersistentManager persistentManager = new PersistentManager())
            {
                AllotDao dao = new AllotDao();
                if (dao.GetData(sql).Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        
        //=========================================

        internal DataTable FindDeliveryAvailableCell()
        {
            string sql = @"SELECT WMS_WAREHOUSE.WH_CODE,WMS_WAREHOUSE.WH_NAME,
                            WMS_WH_AREA.AREACODE,WMS_WH_AREA.AREANAME,
                            WMS_WH_SHELF.SHELFCODE,WMS_WH_SHELF.SHELFNAME,
                            WMS_WH_CELL.CELL_ID,WMS_WH_CELL.CELLCODE,WMS_WH_CELL.CELLNAME,WMS_WH_CELL.LAYER_NO,
                            WMS_WH_CELL.AREATYPE,-- WMS_WH_AREA.ENTRYALLOTORDER,
                            WMS_WH_CELL.ASSIGNEDPRODUCT,WMS_WH_CELL.CURRENTPRODUCT,WMS_PRODUCT.PRODUCTNAME,CONVERT(NVARCHAR(10),WMS_WH_CELL.INPUTDATE,120) INPUTDATE,
                            WMS_WH_CELL.UNITCODE,WMS_UNIT.UNITNAME,WMS_WH_CELL.MAX_QUANTITY,WMS_WH_CELL.QUANTITY,
                            WMS_WH_CELL.QUANTITY-WMS_WH_CELL.FROZEN_OUT_QTY AS AVAILABLE,0.00 AS ALLOTQUANTITY,
                            '' AS TASKID,'' AS BILLNO, 0.00 AS DETAILID,
                            '' AS PALLETID,'' AS NEWPALLETID
                            FROM WMS_WH_CELL 
                            LEFT JOIN WMS_WH_SHELF ON WMS_WH_SHELF.SHELFCODE = WMS_WH_CELL.SHELFCODE 
                            LEFT JOIN WMS_WH_AREA ON WMS_WH_SHELF.AREACODE = WMS_WH_AREA.AREACODE
                            LEFT JOIN WMS_WAREHOUSE ON WMS_WAREHOUSE.WH_CODE = WMS_WH_AREA.WH_CODE
                            LEFT JOIN WMS_UNIT ON WMS_UNIT.UNITCODE = WMS_WH_CELL.UNITCODE
                            LEFT JOIN WMS_PRODUCT ON WMS_PRODUCT.PRODUCTCODE = WMS_WH_CELL.CURRENTPRODUCT
                            WHERE WMS_WH_CELL.ISACTIVE = '1'                                                            --储位是否可用
                              AND WMS_WH_CELL.ISLOCKED='0'																--储位是否冻结
                              AND ((WMS_WH_CELL.CURRENTPRODUCT !='' AND WMS_WH_CELL.CURRENTPRODUCT IS NOT NULL) OR WMS_WH_CELL.FROZEN_IN_QTY = 0) 
                            --AND QUANTITY>0																			--储位库存量
                            --AND QUANTITY>FROZEN_OUT_QTY																--储位可出库量
                            --AND WMS_WH_AREA.ENTRYALLOTORDER != 0 
                            ORDER BY -- WMS_WH_AREA.ENTRYALLOTORDER,
                            CONVERT(NVARCHAR(10),WMS_WH_CELL.INPUTDATE,120),WMS_WH_CELL.QUANTITY";

            using (PersistentManager persistentManager = new PersistentManager())
            {
                AllotDao dao = new AllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }


        internal DataTable GetEmptyAllotmentTable(string InOrOut)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = @"SELECT ID,TASKID,BILLNO,DETAILID,
                                PRODUCTCODE,'' AS PRODUCTNAME,
                                '' AS UNITCODE,'' AS UNITNAME,
                                CELLCODE,'' AS CELLNAME ,QUANTITY,OUTPUTQUANTITY,
                                PALLETID ,NEWPALLETID ,OPERATEPERSON,
                                STARTTIME,FINISHTIME ,STATUS,INPUTDATE,OUTPUTDATE
                                FROM WMS_OUT_ALLOTDETAIL WHERE 1=0";
                AllotDao dao = new AllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }

        internal DataTable GetEmptyMoveDetailTable()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = @"SELECT ID,BILLNO,
                                PRODUCTCODE,'' AS PRODUCTNAME,
                                '' AS UNITCODE,'' AS UNITNAME,
                                OUT_CELLCODE ,'' AS OUT_CELLNAME ,
                                IN_CELLCODE,'' AS IN_CELLNAME ,QUANTITY, 
                                OPERATEPERSON , STARTTIME , FINISHTIME , STATUS  
                                FROM  WMS_MOVE_BILLDETAIL WHERE 1=0";
                AllotDao dao = new AllotDao();
                return dao.GetData(sql).Tables[0];
            }
        }
    }
}
