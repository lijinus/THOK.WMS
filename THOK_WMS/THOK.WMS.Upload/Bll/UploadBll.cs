using System;
using System.Collections.Generic;
using System.Text;
using THOK.WMS.Upload.Dao;
using System.Data;
using THOK.Util;

namespace THOK.WMS.Upload.Bll
{
    public class UploadBll
    {

        #region 查询卷烟信息数据

        /// <summary>
        /// 查询卷烟信息表上报
        /// </summary>
        public string FindProduct()
        {
            string tag = "上报卷烟信息表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();                
                dao.SetPersistentManager(dbpm);
                DataTable brandTable = this.QueryProductInfo();
                if (brandTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertProduct(brandTable);
                        this.UpdateProduct();
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的卷烟信息要上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询卷烟产品信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryProductInfo()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryProductInfo();
            }
        }

        /// <summary>
        /// 修改卷烟信息表上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateProduct()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateProduct();
                dao.InsertSynchroInfo("DWV_IINF_BRAND", "卷烟信息表");
            }
        }

        #endregion


        #region 查询组织结构表数据

        /// <summary>
        /// 查询组织结构表上报
        /// </summary>
        public string FindOrganization()
        {
            string tag = "上报组织结构表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable organizationTable = this.QueryOrganization();
                if (organizationTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertOrganization(organizationTable);
                        this.UpdateOrganization();
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的组织结构信息要上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询组织结构表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryOrganization()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryOrganization();
            }
        }

        /// <summary>
        /// 修改组织结构表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateOrganization()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateOrganization();
                dao.InsertSynchroInfo("DWV_IORG_ORGANIZATION", "组织结构表");
            }
        }


        #endregion


        #region 查询人员信息表数据

        /// <summary>
        /// 查询人员信息表上报
        /// </summary>
        public string FindPerson()
        {
            string tag = "上报人员信息表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable personTable = this.QueryPerson();
                if (personTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertPreson(personTable);
                        this.UpdatePerson();
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的人员信息要上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询人员信息表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryPerson()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryPerson();
            }
        }

        /// <summary>
        /// 修改人员信息表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdatePerson()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdatePerson();
                dao.InsertSynchroInfo("DWV_IORG_PERSON", "人员信息表");
            }
        }
        #endregion


        #region 查询客户信息表数据

        /// <summary>
        /// 查询客户信息表上报
        /// </summary>
        public string FindCustomer()
        {
            string tag = "上报客户信息表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable customerTable = this.QueryCustomer();
                if (customerTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertCustomer(customerTable);
                        this.UpdateCustomer("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的客户信息要上报！";
            } return tag;
        }

        /// <summary>
        /// 查询客户信息表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomer()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryCustomer();
            }
        }

        /// <summary>
        /// 修改客户信息表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateCustomer(string customerCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateCustomer(customerCode);
                dao.InsertSynchroInfo("DWV_IORG_CUSTOMER", "客户信息表");
            }
        }

        #endregion


        #region 查询仓库库存表数据

        /// <summary>
        /// 查询仓库库存表上报
        /// </summary>
        public string FindStoreStock()
        {
            string tag = "上报仓库库存表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable stockTable = this.QueryStoreStock();
                DataTable stockQuantityTable = this.InsertStoreQuantity(stockTable);
                if (stockQuantityTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertStoreStock(stockQuantityTable);
                        this.UpdateStoreStock("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的仓库库存表要上报！";
            }
            return tag;
        }


        /// <summary>
        /// 查询仓库库存表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryStoreStock()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryStoreStock();
            }
        }

        /// <summary>
        /// 修改仓库库存表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateStoreStock(string storeStockCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateStoreStock(storeStockCode);
                dao.InsertSynchroInfo("DWV_IWMS_STORE_STOCK", "仓库库存表");
            }
        }

        /// <summary>
        /// 把仓库库存表数据插入虚拟表
        /// </summary>
        /// <param name="busiTable"></param>
        public DataTable InsertStoreQuantity(DataTable stortStockQuantityTable)
        {
            DataSet ds = this.GenerateStoreTables();
            foreach (DataRow row in stortStockQuantityTable.Rows)
            {
                DataRow storerow = ds.Tables["DWV_IWMS_STORE_STOCK"].NewRow();
                storerow["STORE_PLACE_CODE"] = row["STORE_PLACE_CODE"].ToString().Trim();
                storerow["BRAND_CODE"] = row["CURRENTPRODUCT"].ToString().Trim();
                storerow["AREA_TYPE"] = row["AREA_TYPE"];
                storerow["BRAND_BATCH"] = row["BRAND_BATCH"];
                storerow["DIST_CTR_CODE"] = this.GetCompany().ToString();
                storerow["QUANTITY"] = row["QUANTITY"];
                storerow["IS_IMPORT"] = "0";
                ds.Tables["DWV_IWMS_STORE_STOCK"].Rows.Add(storerow);
            }
            return ds.Tables[0];
        }



        /// <summary>
        /// 创建虚拟表
        /// </summary>
        /// <returns></returns>
        private DataSet GenerateStoreTables()
        {
            DataSet ds = new DataSet();
            DataTable storedetail = ds.Tables.Add("DWV_IWMS_STORE_STOCK");
            storedetail.Columns.Add("STORE_PLACE_CODE");
            storedetail.Columns.Add("BRAND_CODE");
            storedetail.Columns.Add("AREA_TYPE");
            storedetail.Columns.Add("BRAND_BATCH");
            storedetail.Columns.Add("DIST_CTR_CODE");
            storedetail.Columns.Add("QUANTITY");
            storedetail.Columns.Add("IS_IMPORT");
            return ds;
        }

        #endregion


        #region 查询业务库存表数据

        /// <summary>
        /// 查询业务库存表表上报
        /// </summary>
        public string FindBusiStock()
        {
            string tag = "上报业务库存表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable busiStockTable = this.QueryBusiStock();
                if (busiStockTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertBustStock(busiStockTable);
                        this.UpdateBusiStock("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的业务库存信息要上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询业务库存表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryBusiStock()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryBusiStock();
            }
        }

        /// <summary>
        /// 修改业务库存表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateBusiStock(string busiStockCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateBusiStock(busiStockCode);
                dao.InsertSynchroInfo("DWV_IWMS_BUSI_STOCK", "业务库存表");
            }
        }

        #endregion


        #region 查询仓库入库单据主表数据

        /// <summary>
        /// 查询仓库入库单据主表上报
        /// </summary>
        public string FindInMasterBill()
        {
            string tag = "上报仓库入库单据主表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable inMasterTable = this.QueryInMasterBill();
                if (inMasterTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertInMasterBill(inMasterTable);
                        this.UpdateInMaster("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的仓库入库信息要上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询仓库入库单据主表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryInMasterBill()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryInMasterBill();
            }
        }

        /// <summary>
        /// 修改仓库入库单据主表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateInMaster(string inMasterCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateInMaster(inMasterCode);
                dao.InsertSynchroInfo("DWV_IWMS_IN_STORE_BILL", "仓库入库单据主表");
            }
        }
        #endregion


        #region 查询仓库入库单据细表数据

        /// <summary>
        /// 查询仓库入库单据细表上报
        /// </summary>
        public string FindInDetailBill()
        {
            string tag = "上报仓库入库单据细表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable inDetailTable = this.QueryInDetailBill();
                if (inDetailTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertInDetailBill(inDetailTable);
                        this.UpdateInDetail("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的仓库入库细表信息要上报！";
            }
            return tag;
        }


        /// <summary>
        /// 查询仓库入库单据细表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryInDetailBill()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryInDetailBill();
            }
        }

        /// <summary>
        /// 修改仓库入库单据细表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateInDetail(string inDetailCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateInDetail(inDetailCode);
                dao.InsertSynchroInfo("DWV_IWMS_IN_STORE_BILL_DETAIL", "仓库入库单据细表");
            }
        }
        #endregion


        #region 查询入库业务单据表数据

        /// <summary>
        /// 查询入库业务单据表上报
        /// </summary>
        public string FindInBusiBill()
        {
            string tag = "上报仓库入库业务单据表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable inBusiTable = this.QueryInBusiBill();
                if (inBusiTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertInBusiBill(inBusiTable);
                        this.UpdateInBusi("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的入库业务单据信息要上报！";
            }
            return tag;
        }


        /// <summary>
        /// 查询入库业务单据表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryInBusiBill()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryInBusiBill();
            }
        }

        /// <summary>
        /// 修改入库业务单据表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateInBusi(string inBusiCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateInBusi(inBusiCode);
                dao.InsertSynchroInfo("DWV_IWMS_IN_BUSI_BILL", "入库业务单据表");
            }
        }

        #endregion


        #region 查询仓库出库单据主表数据

        /// <summary>
        /// 查询仓库出库单据主表上报
        /// </summary>
        public string FindOutMasterBill()
        {
            string tag = "上报仓库出库单据主表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable outMasterTable = this.QueryOutMasterBill();
                if (outMasterTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertOutMasertBill(outMasterTable);
                        this.UpdateOutMaster("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的仓库出库单据主表要上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询仓库出库单据主表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryOutMasterBill()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryOutMasterBill();
            }
        }

        /// <summary>
        /// 修改仓库出库单据主表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateOutMaster(string outMasterCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateOutMaster(outMasterCode);
                dao.InsertSynchroInfo("DWV_IWMS_OUT_STORE_BILL", "仓库出库单据主表");
            }
        }
        #endregion


        #region 查询仓库出库单据细表数据

        /// <summary>
        /// 查询仓库出库单据细表上报
        /// </summary>
        public string FindOutDetailBill()
        {
            string tag = "上报仓库出库单据细表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable outDetailTable = this.QueryOutDetailBill();
                if (outDetailTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertOutDetailBill(outDetailTable);
                        this.UpdateOutDetail("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的仓库出库单据细表要上报！";
            }
            return tag;
        }


        /// <summary>
        /// 查询仓库出库单据细表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryOutDetailBill()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryOutDetailBill();
            }
        }

        /// <summary>
        /// 修改仓库出库单据细表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateOutDetail(string outDetailCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateOutDetail(outDetailCode);
                dao.InsertSynchroInfo("DWV_IWMS_OUT_STORE_BILL_DETAIL", "仓库出库单据细表");
            }
        }
        #endregion


        #region 查询出库业务单据表数据

        /// <summary>
        /// 查询出库业务单据表上报
        /// </summary>
        public string FindOutBusiBill()
        {
            string tag = "上报仓库出库业务单据表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable outBusiTable = this.QueryOutBusiBill();
                if (outBusiTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertOutBusiBill(outBusiTable);
                        this.UpdateOutBusi("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的仓库出库业务单据表要上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询出库业务单据表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryOutBusiBill()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryOutBusiBill();
            }
        }

        /// <summary>
        /// 修改出库业务单据表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateOutBusi(string outBusiCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateOutBusi(outBusiCode);
                dao.InsertSynchroInfo("DWV_IWMS_OUT_BUSI_BILL", "出库业务单据表");
            }
        }

        #endregion


        #region 查询同步状态表数据

        /// <summary>
        /// 查询同步状态表上报
        /// </summary>
        public string FindSynchroInfo()
        {
            string tag = "上报同步状态表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable synchroTable = this.QuerySynchroInfo();
                if (synchroTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertSynchro(synchroTable);
                        this.UpdateSyachro("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有同步状态表数据可上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询同步状态表
        /// </summary>
        /// <returns></returns>
        public DataTable QuerySynchroInfo()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QuerySynchroInfo();
            }
        }

        /// <summary>
        /// 修改同步状态表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateSyachro(string syachroCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateSyachro(syachroCode);
            }
        }

        #endregion


        #region 查询分拣订单主表数据
        /// <summary>
        /// 查询分拣订单主表上报
        /// </summary>
        public string FindIordMasterOrder()
        {
            string tag = "上报分拣订单主表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable orderMasterTable = this.QueryIordMasterOrder();
                if (orderMasterTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertIordOrder(orderMasterTable);
                        this.UpdateOrderMaster("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的分拣订单主表要上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询分拣订单主表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryIordMasterOrder()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryIordMasterOrder();
            }
        }

        /// <summary>
        /// 修改分拣订单主表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateOrderMaster(string orderMasterCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateOrderMaster(orderMasterCode);
                dao.InsertSynchroInfo("DWV_OUT_ORDER", "分拣订单主表");
            }
        }
        #endregion


        #region 查询分拣订单细表数据

        /// <summary>
        /// 查询分拣订单细表上报
        /// </summary>
        public string FindIordDetailOrder()
        {
            string tag = "上报分拣订单细表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable orderDetailTable = this.QueryIordDetailOrder();
                if (orderDetailTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertIordOrderDetail(orderDetailTable);
                        this.UpdateOrderDetail("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的分拣订单细表要上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询分拣订单细表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryIordDetailOrder()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryIordDetailOrder();
            }
        }

        /// <summary>
        /// 修改分拣订单细表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateOrderDetail(string orderDetailCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateOrderDetail(orderDetailCode);
                dao.InsertSynchroInfo("DWV_OUT_ORDER_DETAIL", "分拣订单细表");
            }
        }
        #endregion


        #region 查询分拣情况表数据

        /// <summary>
        /// 查询分拣情况表上报
        /// </summary>
        public string FindSortStatus()
        {
            string tag = "上报分拣情况表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable sortStockTable = this.QuerySortStatus();
                if (sortStockTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertSortStatus(sortStockTable);
                        this.UpdateSortStatus("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的分拣情况表要上报！";
            } return tag;
        }

        /// <summary>
        /// 查询分拣情况表
        /// </summary>
        /// <returns></returns>
        public DataTable QuerySortStatus()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QuerySortStatus();
            }
        }

        /// <summary>
        /// 修改分拣情况表信息上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateSortStatus(string sortStatusCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateSortStatus(sortStatusCode);
                dao.InsertSynchroInfo("DWV_IORD_SORT_STATUS", "分拣情况表");
            }
        }

        #endregion


        #region 查询分拣线信息表数据

        /// <summary>
        /// 查询分拣线信息表上报
        /// </summary>
        public string FindIdpsSorting()
        {
            string tag = "上报分拣线信息表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable sortingTable = this.QueryIdpsSorting();
                if (sortingTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertIdpsSorting(sortingTable);
                        this.UpdateSorting("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的分拣线信息表要上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询分拣线信息表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryIdpsSorting()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryIdpsSorting();
            }
        }

        /// <summary>
        /// 修改分拣线信息表上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateSorting(string sortingCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateSorting(sortingCode);
                dao.InsertSynchroInfo("DWV_DPS_SORTING", "分拣线信息表");
            }
        }

        #endregion


        #region 查询仓储属性表
        /// <summary>
        /// 查询仓储属性表上报
        /// </summary>
        public string FindIbasSorting()
        {
            string tag = "上报仓储属性表成功！";
            using (PersistentManager dbpm = new PersistentManager("ZYDB2Connection"))
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                DataTable ibasSortingTable = this.QueryIbasSorting();
                if (ibasSortingTable.Rows.Count > 0)
                {
                    try
                    {
                        dbpm.BeginTransaction();
                        dao.InsertIbasSorting(ibasSortingTable);
                        this.UpdateIbasSorting("");
                        dbpm.Commit();
                    }
                    catch (Exception exp)
                    {
                        dbpm.Rollback();
                        throw new Exception(exp.Message);
                    }
                }
                else
                    tag = "没有新的仓储属性表要上报！";
            }
            return tag;
        }

        /// <summary>
        /// 查询仓储属性表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryIbasSorting()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryIbasSorting();
            }
        }

        /// <summary>
        /// 修改仓储属性表上报状态
        /// </summary>
        /// <param name="productCode"></param>
        public void UpdateIbasSorting(string sortingCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                dao.UpdateIbsaSorting(sortingCode);
                dao.InsertSynchroInfo("DWV_IBAS_STORAGE", "仓储属性表");
            }
        }
        #endregion


        #region 给业务数据表插入数据

        /// <summary>
        /// 插入数据到业务库存表
        /// </summary>
        public void InsertBusiStock()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                DataTable cellQuantityTable = dao.QueryCellQuantity();//查询件的数量
                DataTable busiTable = this.InsertBusiQuantity(cellQuantityTable);
                dao.InsertBusiStockQuntity(busiTable);
            }
        }

        /// <summary>
        /// 根据产品编码查询业务库存零烟柜数量
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public DataTable QueryCellTiao(string product)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(pm);
                return dao.QueryCellTiao(product);
            }
        }

        /// <summary>
        /// 把数据插入虚拟表
        /// </summary>
        /// <param name="busiTable"></param>
        public DataTable InsertBusiQuantity(DataTable cellQuantityTable)
        {
            DataSet ds = this.GenerateEmptyTables();
            foreach (DataRow row in cellQuantityTable.Rows)
            {
                DataTable prodt = this.ProductRate(row["CURRENTPRODUCT"].ToString());
                DataTable tiaoTable = this.QueryCellTiao(row["CURRENTPRODUCT"].ToString());
                
                int quantityTiao = Convert.ToInt32(Convert.ToInt32(row["QUANTITY"]) / Convert.ToInt32(prodt.Rows[0]["TIAORATE"].ToString()));

                DataRow storerow = ds.Tables["DWV_IWMS_BUSI_STOCK"].NewRow();
                storerow["ORG_CODE"] = row["ORG_CODE"].ToString().Trim();
                storerow["BRAND_CODE"] = row["CURRENTPRODUCT"].ToString().Trim();
                storerow["DIST_CTR_CODE"] = this.GetCompany().ToString();
                storerow["QUANTITY"] = quantityTiao;
                storerow["IS_IMPORT"] = "0";
                ds.Tables["DWV_IWMS_BUSI_STOCK"].Rows.Add(storerow);
            }
            return ds.Tables[0];
        }
        #endregion

        #region 其他

        //获取产品的单位比例
        public DataTable ProductRate(string productCode)
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbPm);
                return dao.ProductRate(productCode);
            }
        }

        /// <summary>
        /// 获取配送中心编码
        /// </summary>
        /// <returns></returns>
        public string GetCompany()
        {
            using (PersistentManager dbpm = new PersistentManager())
            {
                UploadDao dao = new UploadDao();
                dao.SetPersistentManager(dbpm);
                return dao.GetCompany().ToString();
            }
        }

        /// <summary>
        /// 创建虚拟表
        /// </summary>
        /// <returns></returns>
        private DataSet GenerateEmptyTables()
        {
            DataSet ds = new DataSet();
            DataTable storedetail = ds.Tables.Add("DWV_IWMS_BUSI_STOCK");
            storedetail.Columns.Add("ORG_CODE");
            storedetail.Columns.Add("BRAND_CODE");
            storedetail.Columns.Add("DIST_CTR_CODE");
            storedetail.Columns.Add("QUANTITY");
            storedetail.Columns.Add("IS_IMPORT");
            return ds;
        }

        /// <summary>
        /// 修改日结信息
        /// </summary>
        /// <param name="uase"></param>
        public void UpdateDayReckno(string uase)
        {
            string datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UpdateUploadDao dao = new UpdateUploadDao();
                dao.SetPersistentManager(persistentManager);
                dao.UpdateDate(uase, datetime);
            }
        }
        #endregion

    }
}
