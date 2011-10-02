using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Download.Dao;

namespace THOK.WMS.Download.Bll
{
    public class DownProductBll
    {
        #region 从营系统据下产品信息

        /// <summary>
        /// 下载产品信息
        /// </summary>
        /// <returns></returns>
        public bool DownProductInfo()
        {
            bool tag = true;
            DataTable codedt = this.GetProductCode();
            string codeList = UtinString.StringMake(codedt, "PRODUCTCODE");
            codeList = UtinString.StringMake(codeList);
            codeList = "BRAND_CODE NOT IN (" + codeList + ")";
            DataTable bradCodeTable = this.GetProductInfo(codeList);
            if (bradCodeTable.Rows.Count > 0)
            {
                DataSet brandCodeDs = this.Insert(bradCodeTable);
                this.Insert(brandCodeDs);
            }
            else
            {
                tag = false;
            }
            return tag;
        }

        /// <summary>
        /// 下载卷烟产品信息表
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductInfo(string codeList)
        {
            using (PersistentManager dbPm = new PersistentManager("YXConnection"))
            {
                DownProductDao dao = new DownProductDao();
                dao.SetPersistentManager(dbPm);
                return dao.GetProductInfo(codeList);
            }
        }

        /// <summary>
        /// 查询卷烟产品编号
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductCode()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownProductDao dao = new DownProductDao();
                dao.SetPersistentManager(pm);
                return dao.GetProductCode();
            }
        }

        /// <summary>
        /// 查询卷烟信息
        /// </summary>
        /// <returns></returns>
        public DataTable ProductInfo()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownProductDao dao = new DownProductDao();
                dao.SetPersistentManager(pm);
                return dao.ProductInfo();
            }
        }

        /// <summary>
        /// 把数据插入到数据库
        /// </summary>
        /// <param name="ds"></param>
        public void Insert(DataSet ds)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownProductDao dao = new DownProductDao();
                dao.SetPersistentManager(pm);
                dao.Insert(ds);
            }
        }

        /// <summary>
        /// 根据计量单位编号查询数据
        /// </summary>
        /// <param name="ulistCode"></param>
        /// <returns></returns>
        public DataTable FindUnitCode(string ulistCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownUnitDao dao = new DownUnitDao();
                dao.SetPersistentManager(pm);
                return dao.FindUnitCodeByUlistCode(ulistCode);
            }
        }

        public DataTable GetProductCode(string code)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownProductDao dao = new DownProductDao();
                dao.SetPersistentManager(pm);
                return dao.GetProductCode(code);
            }
        }

        /// <summary>
        /// 插入数据到虚拟表
        /// </summary>
        /// <param name="brandTable"></param>
        /// <returns></returns>
        public DataSet Insert(DataTable brandTable)
        {
            DownUnitBll bll = new DownUnitBll();
            DataSet ds = this.GenerateEmptyTables();
            foreach (DataRow row in brandTable.Rows)
            {
                DataTable ulistCodeTable = this.FindUnitCode(row["BRAND_ULIST_CODE"].ToString().Trim());
                DataRow inprdr = ds.Tables["WMS_PRODUCT"].NewRow();
                inprdr["PRODUCTCODE"] = row["BRAND_CODE"].ToString().Trim();
                inprdr["PRODUCTN"] = row["BRAND_N"].ToString().Trim();
                inprdr["PRODUCTCLASS"] = "";// row["BRAND_CLASS"];
                inprdr["PRODUCTNAME"] = row["BRAND_NAME"].ToString().Trim();
                inprdr["SHORTNAME"] = row["BRAND_NAME"].ToString().Trim();
                inprdr["SUPPLIERCODE"] = ""; //供应商
                inprdr["BARCODE"] = row["BARCODE_PIECE"]; //条形码（件）
                inprdr["ABCODE"] = row["SHORT_CODE"];
                inprdr["UNITCODE"] = "件";
                inprdr["JIANTIAORATE"] = "50";
                inprdr["TIAOBAORATE"] = "10";
                inprdr["BAOZHIRATE"] = "20";
                inprdr["JIANCODE"] = ulistCodeTable.Rows[0]["BRAND_UNIT_CODE_01"].ToString().Trim();
                inprdr["TIAOCODE"] = ulistCodeTable.Rows[0]["BRAND_UNIT_CODE_02"].ToString().Trim();
                inprdr["ZHICODE"] = ulistCodeTable.Rows[0]["BRAND_UNIT_CODE_04"].ToString().Trim();
                inprdr["IS_BARAND"] = row["IS_ABNORMITY_BRAND"];
                inprdr["MEMO"] = "";
                ds.Tables["WMS_PRODUCT"].Rows.Add(inprdr);

                DataRow inbrddr = ds.Tables["DWV_IINF_BRAND"].NewRow();
                inbrddr["BRAND_CODE"] = row["BRAND_CODE"];
                inbrddr["BRAND_TYPE"] = row["BRAND_TYPE"];
                inbrddr["BRAND_N"] = row["BRAND_N"];
                inbrddr["BRAND_NAME"] = row["BRAND_NAME"];
                inbrddr["SHORT_CODE"] = row["SHORT_CODE"];
                inbrddr["UP_CODE"] = row["UP_CODE"];
                inbrddr["FACTORY_CODE"] = row["FACTORY_CODE"];
                inbrddr["BRAND_TRADEMARK_CODE"] = row["BRAND_TRADEMARK_CODE"];
                inbrddr["BARCODE_PACKAGE"] = row["BARCODE_PACKAGE"];
                inbrddr["BARCODE_BAR"] = row["BARCODE_BAR"];
                inbrddr["BARCODE_PIECE"] = row["BARCODE_PIECE"];
                inbrddr["PRICE_LEVEL_CODE"] = row["PRICE_LEVEL_CODE"];
                inbrddr["IS_FILTERTIP"] = row["IS_FILTERTIP"];
                inbrddr["IS_NEW"] = row["IS_NEW"];
                inbrddr["IS_FAMOUS"] = row["IS_FAMOUS"];
                inbrddr["IS_MAINPRODUCT"] = row["IS_MAINPRODUCT"];
                inbrddr["IS_MAINPROVINCE"] = row["IS_MAINPROVINCE"];
                inbrddr["BELONG_REGION"] = row["BELONG_REGION"];
                inbrddr["IS_ABNORMITY_BRAND"] = row["IS_ABNORMITY_BRAND"];
                inbrddr["BUY_PRICE"] = row["BUY_PRICE"];
                inbrddr["TRADE_PRICE"] = row["TRADE_PRICE"];
                inbrddr["RETAIL_PRICE"] = row["RETAIL_PRICE"];
                inbrddr["COST_PRICE"] = row["COST_PRICE"];
                inbrddr["QTY_UNIT"] = row["QTY_UNIT"];
                inbrddr["BARCODE_ONE_PROJECT"] = row["BARCODE_ONE_PROJECT"];
                inbrddr["UPDATE_DATE"] = DateTime.Now.Date.ToString("yyyyMMdd");
                inbrddr["ISACTIVE"] = row["ISACTIVE"];
                inbrddr["N_UNIFY_CODE"] = "";//row["N_UNIFY_CODE"];
                inbrddr["IS_CONFISCATE"] = row["IS_CONFISCATE"];
                inbrddr["IS_IMPORT"] = "0";
                ds.Tables["DWV_IINF_BRAND"].Rows.Add(inbrddr);
            }
            return ds;
        }

        /// <summary>
        /// 构建四个虚拟数据表，2个上传给中烟的，2个
        /// </summary>
        /// <returns></returns>
        private DataSet GenerateEmptyTables()
        {
            DataSet ds = new DataSet();
            DataTable inbrtable = ds.Tables.Add("DWV_IINF_BRAND");
            inbrtable.Columns.Add("BRAND_CODE");
            inbrtable.Columns.Add("BRAND_TYPE");
            inbrtable.Columns.Add("BRAND_N");
            inbrtable.Columns.Add("BRAND_NAME");
            inbrtable.Columns.Add("SHORT_CODE");
            inbrtable.Columns.Add("UP_CODE");
            inbrtable.Columns.Add("FACTORY_CODE");
            inbrtable.Columns.Add("BRAND_TRADEMARK_CODE");
            inbrtable.Columns.Add("BARCODE_PACKAGE");
            inbrtable.Columns.Add("BARCODE_BAR");
            inbrtable.Columns.Add("BARCODE_PIECE");
            inbrtable.Columns.Add("PRICE_LEVEL_CODE");
            inbrtable.Columns.Add("IS_FILTERTIP");
            inbrtable.Columns.Add("IS_NEW");
            inbrtable.Columns.Add("IS_FAMOUS");
            inbrtable.Columns.Add("IS_MAINPRODUCT");
            inbrtable.Columns.Add("IS_MAINPROVINCE");
            inbrtable.Columns.Add("BELONG_REGION");
            inbrtable.Columns.Add("IS_ABNORMITY_BRAND");
            inbrtable.Columns.Add("BUY_PRICE");
            inbrtable.Columns.Add("TRADE_PRICE");
            inbrtable.Columns.Add("RETAIL_PRICE");
            inbrtable.Columns.Add("COST_PRICE");
            inbrtable.Columns.Add("QTY_UNIT");
            inbrtable.Columns.Add("BARCODE_ONE_PROJECT");//一号工程条形码
            inbrtable.Columns.Add("UPDATE_DATE");
            inbrtable.Columns.Add("ISACTIVE");
            inbrtable.Columns.Add("N_UNIFY_CODE");
            inbrtable.Columns.Add("IS_CONFISCATE");
            inbrtable.Columns.Add("IS_IMPORT");

            DataTable inpr = ds.Tables.Add("WMS_PRODUCT");
            inpr.Columns.Add("PRODUCTCODE");
            inpr.Columns.Add("PRODUCTN");
            inpr.Columns.Add("PRODUCTCLASS");
            inpr.Columns.Add("PRODUCTNAME");
            inpr.Columns.Add("SHORTNAME");
            inpr.Columns.Add("SUPPLIERCODE");
            inpr.Columns.Add("BARCODE");
            inpr.Columns.Add("ABCODE");
            inpr.Columns.Add("UNITCODE");
            inpr.Columns.Add("JIANTIAORATE");
            inpr.Columns.Add("TIAOBAORATE");
            inpr.Columns.Add("BAOZHIRATE");
            inpr.Columns.Add("JIANCODE");
            inpr.Columns.Add("TIAOCODE");
            inpr.Columns.Add("ZHICODE");
            inpr.Columns.Add("IS_BARAND");
            inpr.Columns.Add("MEMO");
            return ds;
        }
        #endregion

        #region 从营系统据下产品信息 - 广西浪潮

        /// <summary>
        /// 根据模糊计量单位和卷烟编码去中间表取得对应的计量单位
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public DataTable GetProductByUnitCode(string unitcode, string product)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                DownUnitDao dao = new DownUnitDao();
                dao.SetPersistentManager(pm);
                return dao.GetProductByUnitCode(unitcode, product);
            }
        }

        #endregion
    }
}
