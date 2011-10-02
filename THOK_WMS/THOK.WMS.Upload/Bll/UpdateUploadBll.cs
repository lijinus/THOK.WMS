using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Upload.Dao;
using System.Threading;

namespace THOK.WMS.Upload.Bll
{
    public class UpdateUploadBll
    {
        #region 上报给中烟的出入库业务表

        /// <summary>
        /// 给上报中烟入库业务表插入数据
        /// </summary>
        /// <param name="tableBull"></param>
        public void InsertBull(DataTable tableAllotDetail, string masterTableName, string detailTableName, string outInfoTabel, string employeeCode)
        {
            DataTable table = null;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao(); 
                dao.SetPersistentManager(persistentManager);
                table = dao.QueryBusiBill(outInfoTabel);
                int s = 0;
                decimal quantity = 0.00M;
                string product = "1";//判断是否是同一个品牌
                DataRow[] tableAllotDr = tableAllotDetail.Select("1=1", "PRODUCTCODE");
                foreach (DataRow row in tableAllotDr)
                {
                    s++;
                    if (row["PRODUCTCODE"].ToString() != product)
                        quantity = 0.00M;//计算这个品牌分配的总数
                    DataTable detailTable = dao.GetByOutInfo(row["BILLNO"].ToString(), row["PRODUCTCODE"].ToString(), detailTableName);
                    DataTable masterTable = dao.GetByOutInfo(row["BILLNO"].ToString(), masterTableName);
                    DataTable productStandArdrate = dao.ProductRate(row["PRODUCTCODE"].ToString());//获取产品的比例STANDARDRATE
                    decimal pieceBarQuantity = dao.FindPieceQuantity(row["PRODUCTCODE"].ToString(), "0");//查询原库存件的条数
                    decimal barQuantity = dao.FindBarQuantity(row["PRODUCTCODE"].ToString(), "1");//查询原库存条的数量
                    decimal beginQuantity = pieceBarQuantity + barQuantity;//原库存
                    decimal endBarQuantity = 0.00M;//当前分配的数量
                    decimal endQuntity = 0.00M;//当前库存=原库存+当前分配数量
                    if (row["CELLCODE"].ToString() == productStandArdrate.Rows[0]["JIANCODE"].ToString())//判断是件还是条，件就要转换，条不需要
                    {
                        int endPieceQuantity = Convert.ToInt32(row["QUANTITY"]) * Convert.ToInt32(productStandArdrate.Rows[0]["JIANRATE"].ToString());//把当前分配的数量转换为支
                        endBarQuantity = (Convert.ToDecimal(endPieceQuantity) / Convert.ToDecimal(productStandArdrate.Rows[0]["TIAORATE"].ToString()));//把当前分配的支转换条                       
                        quantity = quantity + endBarQuantity;
                    }
                    else
                    {
                        endBarQuantity = Convert.ToDecimal(row["QUANTITY"]);
                        quantity = quantity + endBarQuantity;
                    }

                    if (outInfoTabel == "DWV_IWMS_OUT_BUSI_BILL")
                        endQuntity = beginQuantity - quantity;//出库减去
                    else
                        endQuntity = beginQuantity + quantity;//入库加上   

                    DataRow newRow = table.NewRow();
                    newRow["BUSI_ACT_ID"] = DateTime.Now.ToString("yyMMddssff") + s;
                    newRow["BUSI_BILL_DETAIL_ID"] = detailTable.Rows[0]["STORE_BILL_DETAIL_ID"].ToString();
                    newRow["BUSI_BILL_ID"] = row["BILLNO"];
                    newRow["RELATE_BUSI_BILL_ID"] = "";
                    newRow["STORE_BILL_ID"] = "";
                    newRow["BRAND_CODE"] = row["PRODUCTCODE"];
                    newRow["BRAND_NAME"] = detailTable.Rows[0]["BRAND_NAME"].ToString();
                    newRow["QUANTITY"] = endBarQuantity;
                    newRow["DIST_CTR_CODE"] = masterTable.Rows[0]["DIST_CTR_CODE"].ToString();
                    newRow["ORG_CODE"] = this.QueryOrgCode().ToString();
                    newRow["STORE_ROOM_CODE"] = "001";
                    newRow["STORE_PLACE_CODE"] = row["CELLCODE"];
                    newRow["TARGET_NAME"] = Convert.ToString(dao.GetCellCodeByName(row["CELLCODE"].ToString()));
                    newRow["IN_OUT_TYPE"] = masterTable.Rows[0]["IN_OUT_TYPE"].ToString();
                    newRow["BILL_TYPE"] = masterTable.Rows[0]["BILL_TYPE"].ToString();
                    newRow["BEGIN_STOCK_QUANTITY"] = Convert.ToInt32(beginQuantity);
                    newRow["END_STOCK_QUANTITY"] = Convert.ToInt32(endQuntity);
                    newRow["DISUSE_STATUS"] = "0";
                    newRow["RECKON_STATUS"] = "";
                    newRow["RECKON_DATE"] = "";
                    newRow["UPDATE_CODE"] = employeeCode;
                    newRow["UPDATE_DATE"] = DateTime.Now.ToString("yyyyMMddHHmmss");
                    newRow["IS_IMPORT"] = "0";
                    table.Rows.Add(newRow);
                    product = row["PRODUCTCODE"].ToString();
                }
                dao.InsertBull(table, outInfoTabel);
            }
        }

        /// <summary>
        /// 插入上传给中烟的出、入库业务表
        /// </summary>
        public void InsertBull(DataTable table, string outInfoTabel)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao(); 
                dao.SetPersistentManager(persistentManager);
                dao.InsertBull(table, outInfoTabel);
            }
        }

        /// <summary>
        /// 手动添加单据主表
        /// </summary>
        /// <param name="infoTableName"></param>
        /// <param name="bill"></param>
        /// <param name="type"></param>
        /// <param name="state"></param>
        /// <param name="operate"></param>
        /// <param name="isUpdate"></param>
        public void InsertBillMaster(string infoTableName, string bill, string type, string state, string operate, bool isUpdate)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                dao.SetPersistentManager(persistentManager);
                if (isUpdate)
                {
                    DataTable table = dao.QueryBusiBill(infoTableName);
                    DataRow newRow = table.NewRow();
                    newRow["STORE_BILL_ID"] = bill.ToString().Trim();
                    newRow["DIST_CTR_CODE"] = dao.GetCompany().ToString();
                    newRow["AREA_TYPE"] = "0901";
                    newRow["CREATOR_CODE"] = operate;
                    newRow["CREATE_DATE"] = DateTime.Now.ToString("yyyyMMddHHmmss");
                    newRow["IN_OUT_TYPE"] = "1202";
                    newRow["BILL_TYPE"] = type;
                    newRow["BILL_STATUS"] = state;
                    newRow["DISUSE_STATUS"] = "0";
                    newRow["IS_IMPORT"] = "0";
                    table.Rows.Add(newRow);
                    dao.InsertMaster(infoTableName, table);
                }
                else
                {
                    string sql = string.Format("UPDATE {0} SET CREATE_DATE='{2}',BILL_TYPE='{3}',BILL_STATUS='{4}' WHERE STORE_BILL_ID='{5}'",
                        infoTableName, operate, DateTime.Now.ToString("yyyyMMddHHmmss"), type, state, bill);
                    dao.UpdateTable(sql);
                }
            }
        }

        /// <summary>
        /// 手动添加单据细表
        /// </summary>
        /// <param name="infoTableName"></param>
        /// <param name="id"></param>
        /// <param name="bill"></param>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <param name="isUpdate"></param>
        /// <param name="queryTable"></param>
        public void InsertDetail(string infoTableName, string id, string bill, string product, decimal quantity, bool isUpdate, string billDetailTable)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao(); 
                dao.SetPersistentManager(persistentManager);
                DataTable productTable = dao.ProductRate(product);
                decimal quan = Convert.ToDecimal(quantity * Convert.ToInt32(productTable.Rows[0]["JIANRATE"].ToString()) / Convert.ToInt32(productTable.Rows[0]["TIAORATE"].ToString()));
                if (isUpdate)
                {
                    Thread.Sleep(500);//确保数据已经添加
                    //string sql = string.Format("SELECT ID FROM {2} WHERE PRODUCTCODE='{0}' AND BILLNO='{1}'", product, bill, billDetailTable);
                    //string stokeId = dao.GetDate(sql).ToString();
                    DataTable table = dao.QueryBusiBill(infoTableName);
                    DataRow newRow = table.NewRow();
                    newRow["STORE_BILL_DETAIL_ID"] = id;
                    newRow["STORE_BILL_ID"] = bill.ToString().Trim();
                    newRow["BRAND_CODE"] = product;
                    newRow["BRAND_NAME"] = this.QueryProduceName(product).ToString();
                    newRow["QUANTITY"] = quan;
                    newRow["IS_IMPORT"] = "0";
                    newRow["BILL_TYPE"] = "101";
                    table.Rows.Add(newRow);
                    dao.InsertMaster(infoTableName, table);
                }
                else
                {
                    string productname = this.QueryProduceName(product).ToString();
                    string sql = string.Format("UPDATE {0} SET STORE_BILL_ID='{1}',BRAND_CODE='{2}',BRAND_NAME='{3}',QUANTITY='{4}' WHERE STORE_BILL_DETAIL_ID='{5}'",
                        infoTableName, bill, product, productname, Convert.ToDecimal(quan), id.ToString());
                    dao.UpdateTable(sql);
                }
            }
        }

        /// <summary>
        /// 根据卷烟编码查询名称
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public string QueryProduceName(string productCode)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao(); 
                string sql = string.Format("SELECT PRODUCTNAME FROM WMS_PRODUCT WHERE PRODUCTCODE='{0}'", productCode);
                return Convert.ToString(dao.GetDate(sql));
            }
        }

        /// <summary>
        /// 获取所属单位编码
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public string QueryOrgCode()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao(); 
                string sql = "SELECT SUBSTRING(ORG_CODE,0,5)+SUBSTRING(ORG_CODE,11,5) FROM DWV_OUT_ORG";
                return Convert.ToString(dao.GetDate(sql));
            }
        }
        #endregion        

        #region 修改入库主表部分数据

        /// <summary>
        /// 审核，修改中烟入库主表审核人、状态和时间
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void inBillAudot(string EmployeeCode, string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("UPDATE DWV_IWMS_IN_STORE_BILL SET BILL_STATUS='2', AUDITOR_CODE='{0}',AUDIT_DATE='{1}' WHERE STORE_BILL_ID='{2}'", EmployeeCode, System.DateTime.Now.ToString("yyyyMMddHHmmss"), BillNo);
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 反核，修改中烟入库主表审核人、状态和时间
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void inRevBillAudot(string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("UPDATE DWV_IWMS_IN_STORE_BILL SET BILL_STATUS='1', AUDITOR_CODE='',AUDIT_DATE=NULL WHERE STORE_BILL_ID='{0}'", BillNo);
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 分配，修改中烟入库主表分配人、状态和时间
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void inUpdateAllot(string EmployeeCode, string BillNo)
        {
            string[] aryBillNo = BillNo.Split(',');
            string BillNoList = "''";
            for (int i = 0; i < aryBillNo.Length; i++)
            {
                BillNoList += ",'" + aryBillNo[i] + "'";
            }
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("UPDATE DWV_IWMS_IN_STORE_BILL SET BILL_STATUS='3', ASSIGNER_CODE='{0}',ASSIGN_DATE='{1}' WHERE STORE_BILL_ID  IN({2})", EmployeeCode, System.DateTime.Now.ToString("yyyyMMddHHmmss"), BillNoList);
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 取消分配，修改中烟入库主表分配人、状态和时间并删除分配结果
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void inDeleteAllot(string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("DELETE FROM DWV_IWMS_IN_BUSI_BILL WHERE BUSI_BILL_ID='{0}';UPDATE DWV_IWMS_IN_STORE_BILL SET BILL_STATUS='2', ASSIGNER_CODE='',ASSIGN_DATE=NULL WHERE STORE_BILL_ID='{0}'", BillNo);              
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 分配确认，修改中烟入库主表确认人、状态和时间
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void inConfirmAllot(string EmployeeCode, string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("UPDATE DWV_IWMS_IN_STORE_BILL SET BILL_STATUS='4', AFFIRM_CODE='{0}',AFFIRM_DATE='{1}' WHERE STORE_BILL_ID='{2}'", EmployeeCode, System.DateTime.Now.ToString("yyyyMMddHHmmss"), BillNo);
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 取消确认，修改中烟入库主表确认人、状态和时间
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void inCancelAllot(string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("UPDATE DWV_IWMS_IN_STORE_BILL SET BILL_STATUS='3', AFFIRM_CODE='',AFFIRM_DATE=NULL WHERE STORE_BILL_ID='{0}'", BillNo);
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 删除入库单据主表和细表
        /// </summary>
        /// <param name="BillNo"></param>
        public void deleteInBill(string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("DELETE DWV_IWMS_IN_STORE_BILL_DETAIL WHERE STORE_BILL_ID IN({0})", BillNo);
                dao.SetData(sql);
                sql = string.Format("DELETE DWV_IWMS_IN_STORE_BILL WHERE STORE_BILL_ID IN({0})", BillNo);
                dao.SetData(sql);
            }
        }
        #endregion

        #region 修改出库主表部分数据

        /// <summary>
        /// 审核，修改中烟出库主表审核人、状态和时间
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void outBillAudot(string EmployeeCode, string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("UPDATE DWV_IWMS_OUT_STORE_BILL SET BILL_STATUS='2', AUDITOR_CODE='{0}',AUDIT_DATE='{1}' WHERE STORE_BILL_ID='{2}'", EmployeeCode, System.DateTime.Now.ToString("yyyyMMddHHmmss"), BillNo);
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 反核，修改中烟出库主表审核人、状态和时间
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void outReBbillAudot(string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("UPDATE DWV_IWMS_OUT_STORE_BILL SET BILL_STATUS='1', AUDITOR_CODE='',AUDIT_DATE=NULL WHERE STORE_BILL_ID='{0}'", BillNo);
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 分配，修改中烟出库主表分配人、状态和时间
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void outUpdateAudot(string EmployeeCode,string BillNo)
        {
            string[] aryBillNo = BillNo.Split(',');
            string BillNoList = "''";
            for (int i = 0; i < aryBillNo.Length; i++)
            {
                BillNoList += ",'" + aryBillNo[i] + "'";
            }
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("UPDATE DWV_IWMS_OUT_STORE_BILL SET BILL_STATUS='3', ASSIGNER_CODE='{0}',ASSIGN_DATE='{1}' WHERE STORE_BILL_ID IN({2})", EmployeeCode, System.DateTime.Now.ToString("yyyyMMddHHmmss"), BillNoList);
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 取消分配，修改中烟出库主表分配人、状态和时间,并删除分配表
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void outDeleteAudot(string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("DELETE FROM DWV_IWMS_OUT_BUSI_BILL WHERE BUSI_BILL_ID='{0}';UPDATE DWV_IWMS_OUT_STORE_BILL SET BILL_STATUS='2', ASSIGNER_CODE='',ASSIGN_DATE=NULL WHERE STORE_BILL_ID='{0}'", BillNo);
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 确认分配，修改中烟出库主表确认人、状态和时间
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void outConfirmAudot(string EmployeeCode, string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("UPDATE DWV_IWMS_OUT_STORE_BILL SET BILL_STATUS='4', AFFIRM_CODE='{0}',AFFIRM_DATE='{1}' WHERE STORE_BILL_ID='{2}'", EmployeeCode, System.DateTime.Now.ToString("yyyyMMddHHmmss"), BillNo);
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 取消确认，修改中烟出库主表确认人、状态和时间
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <param name="BillNo"></param>
        public void outCancelAudot(string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("UPDATE DWV_IWMS_OUT_STORE_BILL SET BILL_STATUS='3', AFFIRM_CODE='',AFFIRM_DATE=NULL WHERE STORE_BILL_ID='{0}'", BillNo);
                dao.SetData(sql);
            }
        }

        /// <summary>
        /// 删除出库单据主表和细表
        /// </summary>
        /// <param name="BillNo"></param>
        public void deleteOutBill(string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                string sql = string.Format("DELETE DWV_IWMS_OUT_STORE_BILL_DETAIL WHERE STORE_BILL_ID IN({0})",BillNo);
                dao.SetData(sql);
                sql = string.Format("DELETE DWV_IWMS_OUT_STORE_BILL WHERE STORE_BILL_ID IN({0})", BillNo);
                dao.SetData(sql);
            }
        }

        #endregion
    }
}
