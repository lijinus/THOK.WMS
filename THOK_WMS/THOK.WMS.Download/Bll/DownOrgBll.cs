using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Download.Dao;

namespace THOK.WMS.Download.Bll
{
    public class DownOrgBll
    {
        #region 从营销系统下载所属单位信息

        /// <summary>
        /// 下载所属单位信息
        /// </summary>
        /// <returns></returns>
        public bool DownOrgInfo()
        {
            bool tag = true;
            this.Delete();
            DataTable orgdt = this.GetOrgInfo();
            if (orgdt.Rows.Count > 0)
                this.Insert(orgdt);
            else
                tag = false;
            return tag;
        }

        /// <summary>
        /// 下载所属单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrgInfo()
        {
            using (PersistentManager dbPm = new PersistentManager("YXConnection"))
            {
                DownOrgDao dao = new DownOrgDao();
                dao.SetPersistentManager(dbPm);
                return dao.GetOrgInfo();
            }
        }

        /// <summary>
        /// 清除单位信息
        /// </summary>
        public void Delete()
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownOrgDao dao = new DownOrgDao();
                dao.SetPersistentManager(dbPm);
                dao.Delete();
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="orgDt"></param>
        public void Insert(DataTable orgDt)
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownOrgDao dao = new DownOrgDao();
                dao.SetPersistentManager(dbPm);
                dao.Insert(orgDt);
            }
        }
        #endregion
    }
}
