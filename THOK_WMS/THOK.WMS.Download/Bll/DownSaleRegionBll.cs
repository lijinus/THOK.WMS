using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.Download.Dao;

namespace THOK.WMS.Download.Bll
{
   public class DownSaleRegionBll
   {
       #region 从营销系统下载营销区域信息

       /// <summary>
        /// 下载营销区域信息
        /// </summary>
        /// <returns></returns>
        public bool DownOrgInfo()
        {
            bool tag = true;
            this.Delete();
            DataTable saleTable = this.GetSaleInfo();
            if (saleTable.Rows.Count > 0)
                this.Insert(saleTable);
            else
                tag = false;
            return tag;
        }

        /// <summary>
        /// 下载营销区域信息
        /// </summary>
        /// <returns></returns>
       public DataTable GetSaleInfo()
        {
            using (PersistentManager dbPm = new PersistentManager("YXConnection"))
            {
                DownSaleRegionDao dao = new DownSaleRegionDao();
                dao.SetPersistentManager(dbPm);
                return dao.GetSaleInfo();
            }
        }

        /// <summary>
        /// 清除营销区域信息
        /// </summary>
        public void Delete()
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownSaleRegionDao dao = new DownSaleRegionDao();
                dao.SetPersistentManager(dbPm);
                dao.Delete();
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="orgDt"></param>
       public void Insert(DataTable saleTable)
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownSaleRegionDao dao = new DownSaleRegionDao();
                dao.SetPersistentManager(dbPm);
                dao.Insert(saleTable);
            }
        }
        #endregion
    }
}
