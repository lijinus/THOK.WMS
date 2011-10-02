using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.Download.Dao
{
    public class DownDeptDao : BaseDao
    {
        #region 营销系统下载部门信息

        /// <summary>
        /// 下载部门信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeptInfo(string deptCode)
        {
            string sql = string.Format("SELECT DEPT_CODE,DEPT_NAME,UP_CODE,UP_DOWN_CODE,DEPT_TYPE,ISACTIVE  FROM IC.V_WMS_DEPT WHERE {0}", deptCode);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 把部门信息插入数据库
        /// </summary>
        /// <param name="ds"></param>
        public void Insert(DataSet ds)
        {
            BatchInsert(ds.Tables["BI_DEPARTMENT"], "BI_DEPARTMENT");
        }
        #endregion

        #region 查询仓储部门信息
        /// <summary>
        /// 查询仓储部门编号
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeptCode()
        {
            string sql = "SELECT DEPTCODE FROM BI_DEPARTMENT";
            return this.ExecuteQuery(sql).Tables[0];
        }

        #endregion
    }
}
