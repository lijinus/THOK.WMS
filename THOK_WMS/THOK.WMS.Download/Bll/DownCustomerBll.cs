using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.Download.Dao;

namespace THOK.WMS.Download.Bll
{
    public class DownCustomerBll
    {
        #region 从营销系统下载客户信息

        /// <summary>
        /// 下载客户信息
        /// </summary>
        /// <returns></returns>
        public bool DownCustomerInfo()
        {
            bool tag = true;
            DataTable customerCodeDt = this.GetCustomerCode();
            string CusromerList = UtinString.StringMake(customerCodeDt, "CUST_CODE");
            CusromerList = UtinString.StringMake(CusromerList);
            CusromerList = " CUST_CODE NOT IN (" + CusromerList + ")";
            DataTable customerDt = this.GetCustomerInfo(CusromerList);
            if (customerDt.Rows.Count > 0)
            {
                DataSet custDs = this.Insert(customerDt);
                this.Insert(custDs);
            }
            else
                tag = false;
            return tag;
        }

        /// <summary>
        /// 下载客户信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCustomerInfo(string customerCode)
        {
            using (PersistentManager dbPm = new PersistentManager("YXConnection"))
            {
                DownCustomerDao dao = new DownCustomerDao();
                dao.SetPersistentManager(dbPm);
                return dao.GetCustomerInfo(customerCode);
            }
        }

        /// <summary>
        /// 查询已下载过的客户编码
        /// </summary>
        /// <returns></returns>
        public DataTable GetCustomerCode()
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownCustomerDao dao = new DownCustomerDao();
                dao.SetPersistentManager(dbPm);
                return dao.GetCustomerCode();
            }
        }

        /// <summary>
        /// 保存数据到数据表
        /// </summary>
        /// <param name="customerDt"></param>
        public void Insert(DataSet customerDs)
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownCustomerDao dao = new DownCustomerDao();
                dao.SetPersistentManager(dbPm);
                dao.Insert(customerDs);
            }
        }

        /// <summary>
        ///保存虚拟表
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public DataSet Insert(DataTable custTable)
        {
            DataSet ds = this.GenerateEmptyTables();
            foreach (DataRow row in custTable.Rows)
            {
                DataRow inbrddr = ds.Tables["DWV_IINF_BRAND"].NewRow();
                inbrddr["CUST_CODE"] = row["CUST_CODE"].ToString().Trim();
                inbrddr["CUST_N"] = row["CUST_N"].ToString().Trim();
                inbrddr["CUST_NAME"] = row["CUST_NAME"].ToString().Trim();
                inbrddr["ORG_CODE"] = row["ORG_CODE"].ToString().Trim();
                inbrddr["SALE_REG_CODE"] = row["SALE_REG_CODE"].ToString().Trim();
                inbrddr["N_CUST_CODE"] = row["N_CUST_CODE"].ToString().Trim();
                inbrddr["CUST_TYPE"] = row["CUST_TYPE"];
                inbrddr["DELIVER_LINE_CODE"] = row["DELIVER_LINE_CODE"].ToString().Trim();
                inbrddr["LINE_SECTION_CODE"] = row["LINE_SECTION_CODE"].ToString().Trim();
                inbrddr["DELIVER_ORDER"] = row["DELIVER_ORDER"];
                inbrddr["DIST_ADDRESS"] = row["DIST_ADDRESS"].ToString().Trim();
                inbrddr["DIST_PHONE"] = row["DIST_PHONE"].ToString().Trim();
                inbrddr["VISIT_LINE_NAME"] = row["VISIT_LINE_NAME"].ToString().Trim();
                inbrddr["VISIT_FREQUENCY"] = row["VISIT_FREQUENCY"].ToString().Trim();
                inbrddr["SALE_SCOPE"] = row["CUST_TYPE"].ToString().Trim();
                inbrddr["RTL_CUST_TYPE_CODE"] = row["RTL_CUST_TYPE_CODE"].ToString().Trim();
                inbrddr["CUST_GEO_TYPE_CODE"] = row["CUST_GEO_TYPE_CODE"].ToString().Trim();
                inbrddr["LICENSE_TYPE"] = row["LICENSE_TYPE"].ToString().Trim();
                inbrddr["LICENSE_CODE"] = row["LICENSE_CODE"].ToString().Trim();
                inbrddr["PRINCIPAL_NAME"] = row["PRINCIPAL_NAME"].ToString().Trim();
                inbrddr["PRINCIPAL_TEL"] = row["PRINCIPAL_TEL"].ToString().Trim();
                inbrddr["PRINCIPAL_ADDRESS"] = row["PRINCIPAL_ADDRESS"].ToString().Trim();
                inbrddr["MANAGEMENT_NAME"] = row["MANAGEMENT_NAME"].ToString().Trim();
                inbrddr["MANAGEMENT_TEL"] = row["MANAGEMENT_TEL"].ToString().Trim();
                inbrddr["BANK"] = row["BANK"].ToString().Trim();
                inbrddr["BANK_ACCOUNTS"] = row["BANK_ACCOUNTS"].ToString().Trim();
                inbrddr["UPDATE_DATE"] = row["UPDATE_DATE"].ToString().Trim();
                inbrddr["ISACTIVE"] = row["ISACTIVE"].ToString().Trim();
                inbrddr["IS_IMPORT"] = "0";
                ds.Tables["DWV_IINF_BRAND"].Rows.Add(inbrddr);
            }
            return ds;
        }


        /// <summary>
        /// 构建一个客户虚拟表
        /// </summary>
        /// <returns></returns>
        private DataSet GenerateEmptyTables()
        {
            DataSet ds = new DataSet();
            DataTable inbrtable = ds.Tables.Add("DWV_IINF_BRAND");
            inbrtable.Columns.Add("CUST_CODE");
            inbrtable.Columns.Add("CUST_N");
            inbrtable.Columns.Add("CUST_NAME");
            inbrtable.Columns.Add("ORG_CODE");
            inbrtable.Columns.Add("SALE_REG_CODE");
            inbrtable.Columns.Add("N_CUST_CODE");
            inbrtable.Columns.Add("CUST_TYPE");
            inbrtable.Columns.Add("DELIVER_LINE_CODE");
            inbrtable.Columns.Add("LINE_SECTION_CODE");
            inbrtable.Columns.Add("DELIVER_ORDER");
            inbrtable.Columns.Add("DIST_ADDRESS");
            inbrtable.Columns.Add("DIST_PHONE");
            inbrtable.Columns.Add("VISIT_LINE_NAME");
            inbrtable.Columns.Add("VISIT_FREQUENCY");
            inbrtable.Columns.Add("SALE_SCOPE");
            inbrtable.Columns.Add("RTL_CUST_TYPE_CODE");
            inbrtable.Columns.Add("CUST_GEO_TYPE_CODE");
            inbrtable.Columns.Add("LICENSE_TYPE");
            inbrtable.Columns.Add("LICENSE_CODE");
            inbrtable.Columns.Add("PRINCIPAL_NAME");
            inbrtable.Columns.Add("PRINCIPAL_TEL");
            inbrtable.Columns.Add("PRINCIPAL_ADDRESS");
            inbrtable.Columns.Add("MANAGEMENT_NAME");
            inbrtable.Columns.Add("MANAGEMENT_TEL");
            inbrtable.Columns.Add("BANK");//一号工程条形码
            inbrtable.Columns.Add("BANK_ACCOUNTS");
            inbrtable.Columns.Add("UPDATE_DATE");
            inbrtable.Columns.Add("ISACTIVE");
            inbrtable.Columns.Add("IS_IMPORT");
            return ds;
        }
        #endregion
    }
}
