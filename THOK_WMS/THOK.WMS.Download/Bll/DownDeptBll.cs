using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.Download.Dao;


namespace THOK.WMS.Download.Bll
{
    public class DownDeptBll
    {
        #region 从营销系统下载部门信息

        /// <summary>
        /// 下载部门信息
        /// </summary>
        /// <returns></returns>
        public bool DownDeptInfo()
        {
            bool tag = true;
            DataTable deptCodeDt = this.GetDeptCode();
            string deptCodeList = UtinString.StringMake(deptCodeDt, "DEPTCODE");
            deptCodeList = UtinString.StringMake(deptCodeList);
            deptCodeList = "DEPT_CODE NOT IN (" + deptCodeList + ")";
            DataTable deptDt = this.GetDeptInfo(deptCodeList);
            if (deptDt.Rows.Count > 0)
            {
                DataSet deptDs = this.Insert(deptDt);
                this.Insert(deptDs);
            }
            else
                tag = false;

            return tag;
        }

        /// <summary>
        /// 把虚拟表的数据添加到数据库
        /// </summary>
        /// <param name="ds"></param>
        public void Insert(DataSet ds)
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownDeptDao dao = new DownDeptDao();
                dao.SetPersistentManager(dbPm);
                dao.Insert(ds);
            }
        }

        /// <summary>
        /// 下载营销系统部门信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeptInfo(string deptCode)
        {
            using (PersistentManager dbPm = new PersistentManager("YXConnection"))
            {
                DownDeptDao dao = new DownDeptDao();
                dao.SetPersistentManager(dbPm);
                return dao.GetDeptInfo(deptCode);
            }
        }

        /// <summary>
        /// 查询数字仓储部门编号
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeptCode()
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownDeptDao dao = new DownDeptDao();
                dao.SetPersistentManager(dbPm);
                return dao.GetDeptCode();
            }
        }

        /// <summary>
        /// 添加数据到虚拟表中
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private DataSet Insert(DataTable deptTable)
        {
            DataSet ds = this.GenerateEmptyTables();
            foreach (DataRow row in deptTable.Rows)
            {
                DataRow deptDr = ds.Tables["BI_DEPARTMENT"].NewRow();
                deptDr["DEPTCODE"] = row["DEPT_CODE"].ToString().Trim();
                deptDr["DEPTNAME"] = row["DEPT_NAME"].ToString().Trim();
                deptDr["DEPTLEADER"] = "";
                deptDr["ISACTIVE"] = row["ISACTIVE"];
                deptDr["WARECODE"] = "001";
                deptDr["MEMO"] = "";
                ds.Tables["BI_DEPARTMENT"].Rows.Add(deptDr);
            }
            return ds;
        }

        /// <summary>
        /// 缓存中构建虚拟表
        /// </summary>
        /// <returns></returns>
        public DataSet GenerateEmptyTables()
        {
            DataSet ds = new DataSet();
            DataTable deptDt = ds.Tables.Add("BI_DEPARTMENT");
            deptDt.Columns.Add("DEPTCODE");
            deptDt.Columns.Add("DEPTNAME");
            deptDt.Columns.Add("DEPTLEADER");
            deptDt.Columns.Add("ISACTIVE");
            deptDt.Columns.Add("WARECODE");
            deptDt.Columns.Add("MEMO");
            return ds;
        }
        #endregion
    }
}
