using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Download.Dao;
using System.Threading;

namespace THOK.WMS.Download.Bll
{
    public class DownInBillBll
    {
        private string Employee = "";

        #region 从营销系统下载入库数据

        #region 手动从营销系统下载入库数据

        /// <summary>
        /// 手动下载入库数据
        /// </summary>
        /// <param name="billno"></param>
        /// <returns></returns>
        public bool GetInBillManual(string billno, string EmployeeCode)
        {
            bool tag = true;
            Employee = EmployeeCode;
            using (PersistentManager pm = new PersistentManager())
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);

                DataTable inBillTable = this.GetInBillNo();
                string inBillNoList = UtinString.StringMake(inBillTable, "BILLNO");
                inBillNoList = UtinString.StringMake(inBillNoList);
                inBillNoList = "ORDER_ID NOT IN(" + inBillNoList + ")";

                DataTable masterdt = this.InBillMaster(inBillNoList);
                DataTable detaildt = this.InBillDetail(inBillNoList);

                DataRow[] masterdr = masterdt.Select("ORDER_ID  IN (" + billno + ")");
                DataRow[] detaildr = detaildt.Select("ORDER_ID  IN (" + billno + ")");

                if (masterdr.Length > 0 && detaildr.Length > 0)
                {
                    DataSet detailds = this.InBillDetail(detaildr);
                    DataSet masterds = this.InBillMaster(masterdr);
                    this.Insert(masterds, detailds);
                }
                else
                    tag = false;
            }
            return tag;
        }

        #endregion

        #region 自动从营销系统下载入库数据

        public bool DownInBillInfoAuto(string EmployeeCode)
        {
            bool tag = true;
            Employee = EmployeeCode;
            using (PersistentManager dbpm = new PersistentManager("YXConnection"))
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(dbpm);

                DataTable WmsInBillTable = this.GetInBillNo();
                string inBillNoList = UtinString.StringMake(WmsInBillTable, "BILLNO");
                inBillNoList = UtinString.StringMake(inBillNoList);
                inBillNoList = "ORDER_ID NOT IN(" + inBillNoList + ")";
                DataTable masterdt = this.InBillMaster(inBillNoList);
                DataTable detaildt = this.InBillDetail(inBillNoList);

                DataRow[] masterdr = masterdt.Select("1=1");
                DataRow[] detaildr = detaildt.Select("1=1");

                if (masterdr.Length > 0 && detaildr.Length > 0)
                {
                    DataSet detailds = this.InBillDetail(detaildr);
                    DataSet masterds = this.InBillMaster(masterdr);
                    this.Insert(masterds, detailds);
                }
                else
                {
                    tag = false;
                }
            }
            return tag;
        }

        #endregion

        #region 选择日期从营销系统下载入库数据

        /// <summary>
        /// 根据日期下载入库数据
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool GetInBill(string startDate, string endDate, string EmployeeCode)
        {
            bool tag = true;
            Employee = EmployeeCode;
            using (PersistentManager pm = new PersistentManager())
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                DataTable inMasterBillNo = this.GetInBillNo();
                string billnolist = UtinString.StringMake(inMasterBillNo, "BILLNO");
                billnolist = UtinString.StringMake(billnolist);
                billnolist = string.Format("ORDER_DATE >='{0}' AND ORDER_DATE <='{1}' AND ORDER_ID NOT IN({2})", startDate, endDate, billnolist);
                DataTable masterdt = this.InBillMaster(billnolist);

                string inDetailList = UtinString.StringMake(masterdt, "ORDER_ID");
                inDetailList = UtinString.StringMake(inDetailList);
                inDetailList = "ORDER_ID IN(" + inDetailList + ")";
                DataTable detaildt = this.InBillDetail(inDetailList);

                DataRow[] masterdr = masterdt.Select("1=1");
                DataRow[] detaildr = detaildt.Select("1=1");
                if (masterdr.Length > 0 && detaildr.Length > 0)
                {
                    DataSet detailds = this.InBillDetail(detaildr);
                    DataSet masterds = this.InBillMaster(masterdr);
                    this.Insert(masterds, detailds);
                }
                else
                    tag = false;
            }
            return tag;
        }

        #endregion

        #region 其他下载查询方法
        /// <summary>
        /// 分页查询营销系统数据入库单据主表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataTable GetInBillMaster(int pageIndex, int pageSize)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                using (PersistentManager dbpm = new PersistentManager("YXConnection"))
                {
                    DownInBillDao masterdao = new DownInBillDao();
                    masterdao.SetPersistentManager(pm);
                    DownInBillDao dao = new DownInBillDao();
                    dao.SetPersistentManager(dbpm);
                    DataTable billnodt = masterdao.GetBillNo();
                    string billnolist = UtinString.StringMake(billnodt, "BILLNO");
                    billnolist = UtinString.StringMake(billnolist);
                    return dao.GetInBillMasterByBillNo(billnolist);
                }
            }
        }

        /// <summary>
        /// 分页查询营销系统数据入库单据明细表
        /// </summary>
        /// <param name="PrimaryKey"></param>
        /// <param name="papeIndex"></param>
        /// <param name="papeSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="inBillNo"></param>
        /// <returns></returns>
        public DataTable GetInBillDetail(string PrimaryKey, int papeIndex, int papeSize, string orderBy, string inBillNo)
        {
            using (PersistentManager dbpm = new PersistentManager("YXConnection"))
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(dbpm);
                return dao.GetInBillDetailByBillNo(inBillNo);
            }
        }

        /// <summary>
        /// 把入库主表数据保存在虚拟表中2011-08-02 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public DataSet InBillMaster(DataRow[] inBillMasterdr)
        {
            DataSet ds = this.GenerateEmptyTables();
            foreach (DataRow row in inBillMasterdr)
            {
                string createdate = row["ORDER_DATE"].ToString();
                createdate = createdate.Substring(0, 4) + "-" + createdate.Substring(4, 2) + "-" + createdate.Substring(6, 2);
                DataRow masterrow = ds.Tables["WMS_IN_BILLMASTER"].NewRow();
                masterrow["BILLNO"] = row["ORDER_ID"].ToString().Trim();
                masterrow["BILLDATE"] = createdate;//DateTime.Now.ToString();//row["BILLDATE"];
                masterrow["BILLTYPE"] = row["ORDER_TYPE"].ToString().Trim();
                masterrow["WH_CODE"] = row["DIST_CTR_CODE"].ToString().Trim();
                masterrow["OPERATEPERSON"] = Employee;
                masterrow["STATUS"] = "1";
                masterrow["VALIDATEPERSON"] = "";
                masterrow["VALIDATEDATE"] = null;
                masterrow["MEMO"] = "";
                ds.Tables["WMS_IN_BILLMASTER"].Rows.Add(masterrow);

                DataRow storerow = ds.Tables["DWV_IWMS_IN_STORE_BILL"].NewRow();
                storerow["STORE_BILL_ID"] = row["ORDER_ID"].ToString().Trim();
                storerow["DIST_CTR_CODE"] = row["DIST_CTR_CODE"].ToString().Trim();
                storerow["QUANTITY_SUM"] = Convert.ToDecimal(row["QUANTITY_SUM"]);
                storerow["AMOUNT_SUM"] = row["AMOUNT_SUM"];//
                storerow["DETAIL_NUM"] = row["DETAIL_NUM"];//明细数
                storerow["CREATE_DATE"] = row["ORDER_DATE"].ToString();
                storerow["CREATOR_CODE"] = Employee;
                storerow["IN_OUT_TYPE"] = "1202";
                storerow["BILL_TYPE"] = row["ORDER_TYPE"].ToString().Trim();
                storerow["AREA_TYPE"] = "0901";
                storerow["DISUSE_STATUS"] = "0";
                storerow["BILL_STATUS"] = "1";
                storerow["IS_IMPORT"] = "0";
                ds.Tables["DWV_IWMS_IN_STORE_BILL"].Rows.Add(storerow);
            }
            return ds;
        }

        /// <summary>
        /// 把入库明细单数据保存在虚拟表,2011-08-02
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public DataSet InBillDetail(DataRow[] inBillDetaildr)
        {
            DataSet ds = this.GenerateEmptyTables();            
            foreach (DataRow row in inBillDetaildr)
            {
                string id = DateTime.Now.ToString("yyMMddHHmmssfff");
                id = id.Substring(1, 14);
                DataTable prodt = DownProductRate(row["BRAND_CODE"].ToString());//
                decimal rate = Convert.ToDecimal(Convert.ToDecimal(row["QUANTITY"].ToString()) / Convert.ToDecimal(prodt.Rows[0]["JIANRATE"].ToString()));
                DataRow detailrow = ds.Tables["WMS_IN_BILLDETAIL"].NewRow();
                detailrow["ID"] = id;
                detailrow["BILLNO"] = row["ORDER_ID"].ToString().Trim();
                detailrow["PRODUCTCODE"] = row["BRAND_CODE"].ToString();
                detailrow["PRICE"] = row["PRICE"];
                detailrow["QUANTITY"] = rate;
                detailrow["INPUTQUANTITY"] = rate;
                detailrow["UNITCODE"] = prodt.Rows[0]["JIANCODE"];
                detailrow["MEMO"] = "";
                detailrow["BILLTYPE"] = "";
                ds.Tables["WMS_IN_BILLDETAIL"].Rows.Add(detailrow);
                decimal quantity = Convert.ToDecimal(Convert.ToDecimal(row["QUANTITY"].ToString()) / Convert.ToDecimal(prodt.Rows[0]["TIAORATE"].ToString()));
                DataRow storerow = ds.Tables["DWV_IWMS_IN_STORE_BILL_DETAIL"].NewRow();
                storerow["STORE_BILL_DETAIL_ID"] = id;
                storerow["STORE_BILL_ID"] = row["ORDER_ID"].ToString().Trim();
                storerow["BRAND_CODE"] = row["BRAND_CODE"].ToString();
                storerow["BRAND_NAME"] = row["BRAND_NAME"];
                storerow["QUANTITY"] = quantity;
                storerow["IS_IMPORT"] = "0";
                ds.Tables["DWV_IWMS_IN_STORE_BILL_DETAIL"].Rows.Add(storerow);
                Thread.Sleep(1);
            }
            return ds;
        }

        /// <summary>
        /// 把查询的数据添加到仓储数据库
        /// </summary>
        /// <param name="masterds"></param>
        /// <param name="detailds"></param>
        public void Insert(DataSet masterds, DataSet detailds)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                if (masterds.Tables["WMS_IN_BILLMASTER"].Rows.Count > 0)
                {
                    dao.InsertInBillMaster(masterds);
                }
                if (masterds.Tables["DWV_IWMS_IN_STORE_BILL"].Rows.Count > 0)
                {
                    dao.InsertInStoreBill(masterds);
                }
                if (detailds.Tables["WMS_IN_BILLDETAIL"].Rows.Count > 0)
                {
                    dao.InsertInBillDetail(detailds);
                }
                if (detailds.Tables["DWV_IWMS_IN_STORE_BILL_DETAIL"].Rows.Count > 0)
                {
                    dao.InsertInStoreBillDetail(detailds);
                }
            }
        }

        /// <summary>
        /// 下载入库单主表数据
        /// </summary>
        /// <returns></returns>
        public DataTable InBillMaster(string inBillNoList)
        {
            using (PersistentManager dbpm = new PersistentManager("YXConnection"))
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(dbpm);
                return dao.GetInBillMaster(inBillNoList);
            }
        }

        /// <summary>
        /// 获取单位比例转换
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public DataTable DownProductRate(string productCode)
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownProductDao dao = new DownProductDao();
                dao.SetPersistentManager(dbPm);
                return dao.DownProductRate(productCode);
            }
        }

        /// <summary>
        /// 下载入库单明细表数据
        /// </summary>
        /// <returns></returns>
        public DataTable InBillDetail(string inBillNoList)
        {
            using (PersistentManager dbpm = new PersistentManager("YXConnection"))
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(dbpm);
                return dao.GetInBillDetail(inBillNoList);
            }
        }
        #endregion

        #endregion

        #region 分拣线退货入库单


        public bool DownReturnInBill(string billNO)
        {
            bool tag = true;
            int count = this.ReturnInBillCount();
            if (count == 0)
            {
                return false;
            }
            else
            {
                string idList = "";
                string memo = "此单据于" + DateTime.Now.ToString("yyMMdd") + "从分拣线退烟入库！";
                this.InsertReturnInBillMaster(billNO, memo);
                idList = this.InsertReturnInBillDetail(billNO);
                this.InsertReturnInBill(billNO, idList);
                idList = UtinString.StringMake(idList);
                this.UpdateReturnInBillState(idList, "1");
                //DataTable returntable = this.ReturnInBill();

            }
            return tag;
        }

        public int ReturnInBillCount()
        {
            using (PersistentManager pm = new PersistentManager("ServerConnection"))
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                return dao.ReturnInBillCount();
            }
        }

        public DataTable ReturnInBill()
        {
            using (PersistentManager pm = new PersistentManager("ServerConnection"))
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                return dao.ReturnInBillInfo();
            }
        }

        public void InsertReturnInBillMaster(string billNo, string memo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                dao.InsertReturnInBillMaster(billNo, memo);
            }
        }

        public void InsertReturnInBill(string idList, string billNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                dao.Insert(billNo, idList);
            }
        }

        public DataTable ReturnInBillInfo()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                return dao.ReturnInBill();
            }
        }

        public string InsertReturnInBillDetail(string billNo)
        {
            DataSet ds = this.GenerateEmptyTables();
            DataTable dt = this.ReturnInBill();
            DownOutBillBll bll = new DownOutBillBll();
            string idList = "";
            foreach (DataRow d in dt.Rows)
            {
                idList += d["ID"].ToString() + ",";
            }

            DataRow[] row = dt.Select("1=1");
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i].RowState != DataRowState.Deleted)
                {
                    decimal quantity = Convert.ToDecimal(row[i]["QUANTITY"].ToString());
                    DataTable prodt = bll.DownProductRate(row[i]["CIGARETTECODE"].ToString());//
                    decimal rate = Convert.ToDecimal(prodt.Rows[0]["JIANRATE"].ToString());//bll.Quantity(row[i]["CIGARETTECODE"].ToString());
                    DataRow[] dr = dt.Select("CIGARETTECODE ='" + row[i]["CIGARETTECODE"] + "' and ID <>'" + row[i]["ID"] + "'");

                    if (dr.Length < 1)
                    {
                        DataRow detailrow = ds.Tables["WMS_IN_BILLDETAIL"].NewRow();
                        detailrow["BILLNO"] = billNo;
                        detailrow["PRODUCTCODE"] = row[i]["CIGARETTECODE"];
                        detailrow["PRICE"] = 0;
                        detailrow["QUANTITY"] = Convert.ToDecimal(row[i]["QUANTITY"].ToString()) / rate;
                        detailrow["INPUTQUANTITY"] = Convert.ToDecimal(row[i]["QUANTITY"].ToString()) / rate;
                        detailrow["UNITCODE"] = prodt.Rows[0]["JIANCODE"];//
                        ds.Tables["WMS_IN_BILLDETAIL"].Rows.Add(detailrow);
                    }
                    else
                    {
                        DataRow drow = ds.Tables["WMS_IN_BILLDETAIL"].NewRow();
                        foreach (DataRow r in dr)
                        {
                            quantity += Convert.ToDecimal(r["QUANTITY"].ToString());
                            row[i]["QUANTITY"] = quantity;
                            r.Delete();
                        }
                        drow["BILLNO"] = billNo;
                        drow["PRODUCTCODE"] = row[i]["CIGARETTECODE"];
                        drow["PRICE"] = 0;
                        drow["QUANTITY"] = Convert.ToDecimal(row[i]["QUANTITY"].ToString()) / rate;
                        drow["INPUTQUANTITY"] = Convert.ToDecimal(row[i]["QUANTITY"].ToString()) / rate;
                        drow["UNITCODE"] = prodt.Rows[0]["JIANCODE"]; // "002";
                        ds.Tables["WMS_IN_BILLDETAIL"].Rows.Add(drow);
                    }
                }
            }

            this.InsertReturnInBillDetail(ds);
            idList = idList.Substring(0, idList.Length - 1);
            return idList;
        }

        public void InsertReturnInBillDetail(DataSet ds)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                dao.InsertInBillDetail(ds);
            }
        }

        public void UpdateReturnInBillState(string idList, string state)
        {
            using (PersistentManager pm = new PersistentManager("ServerConnection"))
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                dao.UpdateReturnInBilLState(idList, state);
            }
        }

        public void DeleteDownReturnInBillInfo(string billNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                dao.Delete(billNo);
            }
        }

        public void DeleteInBill(string billNo)
        {
            string idList = this.SelectIdList(billNo);
            if (idList != null)
            {
                idList = UtinString.StringMake(idList);
                this.DeleteDownReturnInBillInfo(billNo);
                this.UpdateReturnInBillState(idList, "0");
            }
        }

        public string SelectIdList(string billNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                return dao.SelectIdList(billNo);
            }
        }
        #endregion

        #region 操作数字仓储数据

        /// <summary>
        /// 查询数字仓储4天内入库单
        /// </summary>
        /// <returns></returns>
        public DataTable GetInBillNo()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownInBillDao dao = new DownInBillDao();
                dao.SetPersistentManager(pm);
                return dao.GetBillNo();
            }
        }

        /// <summary>
        /// 构建入库虚拟表
        /// </summary>
        /// <returns></returns>
        private DataSet GenerateEmptyTables()
        {
            DataSet ds = new DataSet();
            DataTable mastertable = ds.Tables.Add("WMS_IN_BILLMASTER");
            mastertable.Columns.Add("ID");
            mastertable.Columns.Add("BILLNO");
            mastertable.Columns.Add("BILLDATE");
            mastertable.Columns.Add("BILLTYPE");
            mastertable.Columns.Add("WH_CODE");
            mastertable.Columns.Add("OPERATEPERSON");
            mastertable.Columns.Add("STATUS");
            mastertable.Columns.Add("VALIDATEPERSON");
            mastertable.Columns.Add("VALIDATEDATE");
            mastertable.Columns.Add("MEMO");

            DataTable detailtable = ds.Tables.Add("WMS_IN_BILLDETAIL");
            detailtable.Columns.Add("ID");
            detailtable.Columns.Add("BILLNO");
            detailtable.Columns.Add("PRODUCTCODE");
            detailtable.Columns.Add("PRICE");
            detailtable.Columns.Add("QUANTITY");
            detailtable.Columns.Add("INPUTQUANTITY");
            detailtable.Columns.Add("UNITCODE");
            detailtable.Columns.Add("MEMO");
            detailtable.Columns.Add("BILLTYPE");


            DataTable inmastertable = ds.Tables.Add("DWV_IWMS_IN_STORE_BILL");
            inmastertable.Columns.Add("STORE_BILL_ID");
            inmastertable.Columns.Add("DIST_CTR_CODE");
            inmastertable.Columns.Add("QUANTITY_SUM");
            inmastertable.Columns.Add("AMOUNT_SUM");
            inmastertable.Columns.Add("DETAIL_NUM");
            inmastertable.Columns.Add("CREATOR_CODE");
            inmastertable.Columns.Add("AREA_TYPE");
            inmastertable.Columns.Add("CREATE_DATE");
            inmastertable.Columns.Add("BILL_TYPE");
            inmastertable.Columns.Add("BILL_STATUS");
            inmastertable.Columns.Add("IS_IMPORT");
            inmastertable.Columns.Add("IN_OUT_TYPE");
            inmastertable.Columns.Add("DISUSE_STATUS");


            DataTable indetailtable = ds.Tables.Add("DWV_IWMS_IN_STORE_BILL_DETAIL");
            indetailtable.Columns.Add("STORE_BILL_DETAIL_ID");
            indetailtable.Columns.Add("STORE_BILL_ID");
            indetailtable.Columns.Add("BRAND_CODE");
            indetailtable.Columns.Add("BRAND_NAME");
            indetailtable.Columns.Add("QUANTITY");
            indetailtable.Columns.Add("IS_IMPORT");
            indetailtable.Columns.Add("BILL_TYPE");
            return ds;
        }
        #endregion       
    }
}
