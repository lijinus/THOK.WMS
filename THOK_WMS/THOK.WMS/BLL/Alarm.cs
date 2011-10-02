using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.WMS.Dao;
using System.Data;

namespace THOK.WMS.BLL
{
    public class Alarm
    {
        private string strTableView = "V_WMS_ALARM";
        private string strPrimaryKey = "ID";
        //private string strOrderByFields = "ExceptionalLogID ASC";
        private string strQueryFields = "*";
        public DataSet QueryAlarm(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                AlarmDao dao = new AlarmDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                AlarmDao dao = new AlarmDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                AlarmDao dao = new AlarmDao();

                string sql = string.Format("Insert into WMS_ALARM (PRODUCTCODE,MAX_LIMITED,MIN_LIMITED,MEMO) values('{0}','{1}','{2}','{3}')"
                                             , this.PRODUCTCODE,
                            this.MAX_LIMITED,
                            this.MIN_LIMITED,
                            this.MEMO);

                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool Update()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                AlarmDao dao = new AlarmDao();

                string sql = string.Format("update WMS_ALARM set PRODUCTCODE='{1}',MAX_LIMITED='{2}',MIN_LIMITED='{3}',MEMO='{4}'  where ID='{0}'"
                                             , this.ID,
                            this.PRODUCTCODE,
                            this.MAX_LIMITED,
                            this.MIN_LIMITED,
                            this.MEMO);

                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool Delete(DataSet dataSet)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                AlarmDao dao = new AlarmDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }


        #region property
        private int _id;
        private string _productcode;
        private decimal _max_limited;
        private decimal _min_limited;
        private string _memo;


        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string PRODUCTCODE
        {
            get
            {
                return _productcode;
            }
            set
            {
                _productcode = value;
            }
        }

        public decimal MAX_LIMITED
        {
            get
            {
                return _max_limited;
            }
            set
            {
                _max_limited = value;
            }
        }

        public decimal MIN_LIMITED
        {
            get
            {
                return _min_limited;
            }
            set
            {
                _min_limited = value;
            }
        }

        public string MEMO
        {
            get
            {
                return _memo;
            }
            set
            {
                _memo = value;
            }
        }
        #endregion

        public DataSet GetRemindList()
        {
            string sql = "SELECT * FROM V_WMS_REMIND WHERE ISNULL(QTY_JIAN,0)>MAX_LIMITED OR  ISNULL(QTY_JIAN,0)<MIN_LIMITED ";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                AlarmDao dao = new AlarmDao();
                return dao.GetData(sql);
            }
        }
    }
}
