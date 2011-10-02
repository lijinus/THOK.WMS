using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
    public class Comparison
    {
        private string strTableView = "WMS_COMPARISON";
        //private string strPrimaryKey = "ID";
        private string strQueryFields = "[ID],[FIELD],[VALUE],[TEXT],[DESCRIPTION],[MEMO]";

        public DataSet GetItems(string field)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ComparisonDao dao = new ComparisonDao();
                string sql = string.Format("SELECT {0} FROM {1} WHERE FIELD='{2}'",strQueryFields,strTableView,field);
                return dao.GetData(sql);
            }
        }
    }
}
