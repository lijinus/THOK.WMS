using System;
using System.Collections.Generic;
using System.Text;
using THOK.WMS.Dao;
using System.Data;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class MoveBillMaster
    {
        public string GetNewBillNo()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillMasterDao dao = new MoveBillMasterDao();
                DataSet ds = dao.GetData(string.Format("select TOP 1 BILLNO FROM WMS_MOVE_BILLMASTER where BILLNO LIKE '{0}%' order by BILLNO DESC", System.DateTime.Now.ToString("yyyyMMdd")));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return System.DateTime.Now.ToString("yyyyMMdd") + "0001" + "M";
                }
                else
                {
                    int i = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString().Substring(8, 4));
                    i++;
                    string newcode = i.ToString();
                    for (int j = 0; j < 4 - i.ToString().Length; j++)
                    {
                        newcode = "0" + newcode;
                    }
                    return System.DateTime.Now.ToString("yyyyMMdd") + newcode + "M";
                }
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillMasterDao dao = new MoveBillMasterDao();

                string sql = string.Format("Insert into WMS_MOVE_BILLMASTER (BILLNO,WH_CODE,BILLTYPE,BILLDATE,OPERATEPERSON,STATUS,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                                             ,this.BILLNO,
                            this.WH_CODE,
                            this.BILLTYPE,
                            this.BILLDATE.ToString("yyyy-MM-dd"),
                            this.OPERATEPERSON,
                            this.STATUS,
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
                MoveBillMasterDao dao = new MoveBillMasterDao();

                string sql = string.Format("update WMS_MOVE_BILLMASTER set BILLNO='{1}',WH_CODE='{2}',BILLTYPE='{3}',BILLDATE='{4}',OPERATEPERSON='{5}',STATUS='{6}',MEMO='{7}'  where BILLNO='{1}'"
                                             , this.ID,
                            this.BILLNO,
                            this.WH_CODE,
                            this.BILLTYPE,
                            this.BILLDATE,
                            this.OPERATEPERSON,
                            this.STATUS,
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
                MoveBillMasterDao dao = new MoveBillMasterDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 审核通过，锁定货位
        /// </summary>
        /// <param name="BillNo"></param>
        /// <param name="EmployeeCode"></param>
        /// <returns></returns>
        public bool Validate(string BillNo, string EmployeeCode)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillMasterDao dao = new MoveBillMasterDao();
                DataSet dsTemp=dao.GetData("select * from v_wms_move_billdetail where BILLNO='"+BillNo+"'");
                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in dsTemp.Tables[0].Rows)
                {
                    sb.Append(string.Format("update WMS_WH_CELL SET ISLOCKED='1' WHERE CELLCODE IN ('{0}','{1}');",row["OUT_CELLCODE"].ToString(),row["IN_CELLCODE"].ToString()));
                }
                sb.Append( string.Format("update WMS_MOVE_BILLMASTER SET STATUS='2', VALIDATEPERSON='{0}',VALIDATEDATE='{1}' WHERE BILLNO='{2}';", EmployeeCode, System.DateTime.Now.ToString("yyyy-MM-dd"), BillNo));
                dao.SetData(sb.ToString());
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 反向审核，取消货位锁定
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public bool Rev_Validate(string BillNo)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillMasterDao dao = new MoveBillMasterDao();
                DataSet dsTemp = dao.GetData("select * from v_wms_move_billdetail where BILLNO='" + BillNo + "'");
                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in dsTemp.Tables[0].Rows)
                {
                    sb.Append(string.Format("update WMS_WH_CELL SET ISLOCKED='0' WHERE CELLCODE IN ('{0}','{1}');", row["OUT_CELLCODE"].ToString(), row["IN_CELLCODE"].ToString()));
                }
               sb.Append(string.Format("update WMS_MOVE_BILLMASTER SET STATUS='1', VALIDATEPERSON='',VALIDATEDATE=null where BILLNO='{0}'", BillNo));
                dao.SetData(sb.ToString());
                flag = true;
            }
            return flag;
        }


        private string strTableView = "V_WMS_MOVE_BILLMASTER";
        private string strPrimaryKey = "BILLNO";
        //private string strOrderByFields = "ExceptionalLogID ASC";
        private string strQueryFields = "*";
        public DataSet QueryMoveBillMaster(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillMasterDao dao = new MoveBillMasterDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public DataSet QueryByBillNo(string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao = new EntryBillMasterDao();
                string sql = string.Format("select {0} from {1} WHERE BILLNO='{2}'", strQueryFields, strTableView, BillNo);
                return dao.GetData(sql);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillMasterDao dao = new MoveBillMasterDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        #region property
        private int _id;
        private string _billno;
        private string _wh_code;
        private string _billtype;
        private DateTime _billdate;
        private string _operateperson;
        private string _status;
        private string _validateperson;
        private DateTime _validatedate;
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

        public string BILLNO
        {
            get
            {
                return _billno;
            }
            set
            {
                _billno = value;
            }
        }

        public string WH_CODE
        {
            get
            {
                return _wh_code;
            }
            set
            {
                _wh_code = value;
            }
        }

        public string BILLTYPE
        {
            get
            {
                return _billtype;
            }
            set
            {
                _billtype = value;
            }
        }

        public DateTime BILLDATE
        {
            get
            {
                return _billdate;
            }
            set
            {
                _billdate = value;
            }
        }

        public string OPERATEPERSON
        {
            get
            {
                return _operateperson;
            }
            set
            {
                _operateperson = value;
            }
        }

        public string STATUS
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        public string VALIDATEPERSON
        {
            get
            {
                return _validateperson;
            }
            set
            {
                _validateperson = value;
            }
        }

        public DateTime VALIDATEDATE
        {
            get
            {
                return _validatedate;
            }
            set
            {
                _validatedate = value;
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
    }
}
