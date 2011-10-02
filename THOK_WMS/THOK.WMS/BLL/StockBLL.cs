using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.Dao;

namespace THOK.WMS
{
    public class StockBLL
    {
        public DataTable RealTimeStock()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockDao dao = new StockDao();
                dao.SetPersistentManager(pm);
                return dao.RealTimeStock();
            }
        }

        public DataTable Query(string filter)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockDao dao = new StockDao();
                dao.SetPersistentManager(pm);
                return dao.Query(filter);
            }
        }

        public DataSet QueryMaster(int pageIndex, int pageSize)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockDao dao = new StockDao();
                dao.SetPersistentManager(pm);
                return dao.QueryMaster(pageIndex, pageSize);
            }
        }

        public DataSet QueryDetail(int pageIndex, int pageSize,string billNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockDao dao = new StockDao();
                dao.SetPersistentManager(pm);
                return dao.QueryDetail(pageIndex, pageSize, billNo);
            }
        }

        public DataTable QueryStockProduct(string file)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockDao dao = new StockDao();
                dao.SetPersistentManager(pm);
                return dao.QuerySockProduct(file);
            }
        }
    }
}
