using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
    public class EntryBillMaster
    {
        #region property
        private int _id;
        private string _billno;
        private DateTime _billdate;
        private string _billtype;
        private string _wh_code;
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

        
        private string strTableView = "V_WMS_IN_BILLMASTER";
        private string strPrimaryKey = "BILLNO";
        //private string strOrderByFields = "ExceptionalLogID ASC";
        private string strQueryFields = "*";
        public DataSet QueryEntryBillMaster(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao = new EntryBillMasterDao();
                return dao.Query(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public DataSet QueryByBillNo(string BillNo)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao =  new EntryBillMasterDao();
                string sql = string.Format("select {0} from {1} WHERE BILLNO='{2}'",strQueryFields,strTableView,BillNo);
                return dao.GetData(sql);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao =  new EntryBillMasterDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public string GetNewBillNo()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao =  new EntryBillMasterDao();
                DataSet ds=dao.GetData(string.Format("select TOP 1 BILLNO FROM WMS_IN_BILLMASTER where BILLNO LIKE '{0}%' order by BILLNO DESC",System.DateTime.Now.ToString("yyMMdd")));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return System.DateTime.Now.ToString("yyMMdd") + "0001" + "E";
                }
                else
                {
                    int i = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString().Substring(6, 4));
                    i++;
                    string newcode = i.ToString();
                    for (int j = 0; j < 4 - i.ToString().Length; j++)
                    {
                        newcode = "0" + newcode;
                    }
                    return System.DateTime.Now.ToString("yyMMdd") + newcode + "E";
                }
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao =  new EntryBillMasterDao();
                string sql = string.Format("Insert into WMS_IN_BILLMASTER (BILLNO,BILLDATE,BILLTYPE,WH_CODE,OPERATEPERSON,STATUS,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                                              ,this.BILLNO,
                             this.BILLDATE.ToString("yyyy-MM-dd HH:mm"),
                             this.BILLTYPE,
                             this.WH_CODE,
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
                EntryBillMasterDao dao =  new EntryBillMasterDao();
                string sql = string.Format("update WMS_IN_BILLMASTER set BILLDATE='{1}',BILLTYPE='{2}',WH_CODE='{3}',OPERATEPERSON='{4}',STATUS='{5}',MEMO='{6}'  where BILLNO='{0}'"
                                             , this.BILLNO,
                            this.BILLDATE.ToString("yyyy-MM-dd HH:mm"),
                            this.BILLTYPE,
                            this.WH_CODE,
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
                EntryBillMasterDao dao =  new EntryBillMasterDao();
                dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="BillNo"></param>
        /// <param name="EmployeeCode"></param>
        /// <returns></returns>
        public bool Validate(string BillNo, string EmployeeCode)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao = new EntryBillMasterDao();
                string sql = string.Format("update WMS_IN_BILLMASTER SET STATUS='2', VALIDATEPERSON='{0}',VALIDATEDATE='{1}' WHERE BILLNO='{2}'",EmployeeCode,System.DateTime.Now.ToString("yyyy-MM-dd"),BillNo);
                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 反向审核
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public bool Rev_Validate(string BillNo)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao = new EntryBillMasterDao();
                string sql = string.Format("update WMS_IN_BILLMASTER SET STATUS='1', VALIDATEPERSON='',VALIDATEDATE=null where BILLNO='{0}'",BillNo);
                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }


        /// <summary>
        /// 更新已经执行分配的入库单状态
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public bool UpdateAlloted(string BillNo,string EmployeeCode)
        {
            string[] aryBillNo = BillNo.Split(',');
            string BillNoList = "''";
            for (int i = 0; i < aryBillNo.Length; i++)
            {
                BillNoList += ",'" + aryBillNo[i] + "'";
            }
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao = new EntryBillMasterDao();
                string sql = string.Format("update WMS_IN_BILLMASTER SET STATUS='3' where BILLNO in ({0}) ", BillNoList);
                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 分配确认
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public bool ConfirmAllotment(string BillNo, string EmployeeCode)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao = new EntryBillMasterDao();
                string sql = string.Format("update WMS_IN_BILLMASTER SET STATUS='4' where BILLNO='{0}'", BillNo);
                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 取消分配确认
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public bool Rev_ConAllotment(string BillNo)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao = new EntryBillMasterDao();
                string sql = string.Format("update WMS_IN_BILLMASTER SET STATUS='3' where BILLNO='{0}'", BillNo);
                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }
        #region 获取备货烟道不满的货位信息
        public DataTable QueryNotNull2()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                EntryBillMasterDao dao = new EntryBillMasterDao();
                string sql = string.Format(@"select CELLCODE , CELLNAME ,FROZEN_IN_QTY,MAX_QUANTITY-QUANTITY-FROZEN_IN_QTY as ALLOWINQUANTITY
                                            ,ASSIGNEDPRODUCT,CURRENTPRODUCT,UNITCODE,ISACTIVE from WMS_WH_CELL where AREATYPE='2'
                                             AND MAX_QUANTITY-QUANTITY-FROZEN_IN_QTY>0 AND ISACTIVE='1'order by CELLCODE");
                return dao.GetData(sql).Tables[0];
            }
        }
        #endregion
    }
}
