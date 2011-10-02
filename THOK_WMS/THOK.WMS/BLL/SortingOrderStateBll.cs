using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.WMS.Dao;
using System.Data;

namespace THOK.WMS.BLL
{
    public class SortingOrderStateBll
    {
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SortingOrderStateDao dao = new SortingOrderStateDao();
                return dao.GetRowCount(filter);
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet QuerySortingState(string datetime,string file)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SortingOrderStateDao dao = new SortingOrderStateDao();
                if (file == "0")
                    return dao.QuerySortStatus(datetime);
                else
                    return dao.QuerySortingState(file);
            }
        }

        /// <summary>
        /// 根据线路查询客户编码
        /// </summary>
        /// <param name="routeCode"></param>
        /// <returns></returns>
        public DataTable GetCustByLine(string routeCode)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SortingOrderStateDao dao = new SortingOrderStateDao();
                return dao.GetCustByList(routeCode);
            }
        }

        /// <summary>
        /// 根据时间查询数据
        /// </summary>
        /// <param name="datatiem"></param>
        /// <returns></returns>
        public DataSet QuerySortStatus(string datatiem)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SortingOrderStateDao dao = new SortingOrderStateDao();
                return dao.QuerySortStatus(datatiem);
            }
        }

       /// <summary>
       /// 确认送货员或取消送货员
       /// </summary>
       /// <param name="custCode"></param>
       /// <param name="employee"></param>
        public void UpdateSortStatus(string custCode,string employee)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SortingOrderStateDao dao = new SortingOrderStateDao();
                dao.UpdateSortStatus(custCode, employee);
            }
        }


        /// <summary>
        /// 根据条件查询分拣状态数据
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public DataTable QuerySort(string file)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SortingOrderStateDao dao = new SortingOrderStateDao();
                return dao.QuerySort(file);
            }
        }
       
    }
}
