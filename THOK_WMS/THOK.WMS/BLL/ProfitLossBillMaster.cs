using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Dao;

namespace THOK.WMS.BLL
{
    public class ProfitLossBillMaster
    {
	    private string strTableView = "V_WMS_PL_BILLMASTER";
        private string strPrimaryKey = "ID";
        //private string strOrderByFields = "ExceptionalLogID ASC";
        private string strQueryFields = "*";
        public DataSet QueryProfitLossBillMaster(int pageIndex, int pageSize, string filter, string OrderByFields)
		{
		    using (PersistentManager persistentManager = new PersistentManager())
            {
                ProfitLossBillMasterDao dao = new ProfitLossBillMasterDao();
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

        public string GetNewBillNo()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillMasterDao dao = new MoveBillMasterDao();
                DataSet ds = dao.GetData(string.Format("select TOP 1 BILLNO FROM WMS_PL_BILLMASTER where BILLNO LIKE '{0}%' order by BILLNO DESC", System.DateTime.Now.ToString("yyyyMMdd")));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return System.DateTime.Now.ToString("yyyyMMdd") + "0001";
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
                    return System.DateTime.Now.ToString("yyyyMMdd") + newcode;
                }
            }
        }
		
	    public int GetRowCount(string filter)
        {
		    using (PersistentManager persistentManager = new PersistentManager())
            {
                ProfitLossBillMasterDao dao = new ProfitLossBillMasterDao();
		        return dao.GetRowCount(strTableView, filter);
			}
		}
		
		public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProfitLossBillMasterDao dao = new ProfitLossBillMasterDao();
                string sql = string.Format("Insert into WMS_PL_BILLMASTER (BILLNO,BILLTYPE,WH_CODE,CHECKBILLNO,BILLDATE,OPERATEPERSON,STATUS,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')"
				                             ,this.BILLNO,
							this.BILLTYPE,
							this.WH_CODE,
							this.CHECKBILLNO,
							this.BILLDATE,
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
                ProfitLossBillMasterDao dao = new ProfitLossBillMasterDao();
                string sql = string.Format("update WMS_PL_BILLMASTER set BILLTYPE='{1}',WH_CODE='{2}',CHECKBILLNO='{3}',BILLDATE='{4}',OPERATEPERSON='{5}',STATUS='{6}',MEMO='{7}'  where BILLNO='{0}'"
				                             ,this.BILLNO,
							this.BILLTYPE,
							this.WH_CODE,
							this.CHECKBILLNO,
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
                ProfitLossBillMasterDao dao = new ProfitLossBillMasterDao();
				dao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }


        /// <summary>
        /// 审核通过，更新库存
        /// </summary>
        /// <param name="BillNo"></param>
        /// <param name="EmployeeCode"></param>
        /// <returns></returns>
        public bool Validate(string BillNo, string EmployeeCode)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ProfitLossBillMasterDao dao = new ProfitLossBillMasterDao();
                DataSet dsTemp = dao.GetData("select * from v_wms_PL_billdetail where BILLNO='" + BillNo + "'");
                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in dsTemp.Tables[0].Rows)
                {
                    sb.Append(string.Format("update WMS_WH_CELL SET QUANTITY=QUANTITY+({1})  WHERE CELLCODE='{0}';", row["CELLCODE"].ToString(),row["QUANTITY"]));
                }
                sb.Append(string.Format("update WMS_PL_BILLMASTER SET STATUS='2', VALIDATEPERSON='{0}',VALIDATEDATE='{1}' WHERE BILLNO='{2}';", EmployeeCode, System.DateTime.Now.ToString("yyyy-MM-dd"), BillNo));
                dao.SetData(sb.ToString());
                flag = true;
            }
            return flag;
        }

	    #region property
			private int  _id;
			private string  _billno;
			private string  _billtype;
			private string  _wh_code;
			private string  _checkbillno;
			private DateTime  _billdate;
			private string  _operateperson;
			private string  _status;
            //private string  _validateperson;
            //private DateTime  _validatedate;
			private string  _memo;
		
			
			public int ID 
			{
				get
				{
				    return _id;
				} 
				set
				{
					 _id=value;
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
					 _billno=value;
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
					 _billtype=value;
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
					 _wh_code=value;
				}
			}
			
			public string CHECKBILLNO 
			{
				get
				{
				    return _checkbillno;
				} 
				set
				{
					 _checkbillno=value;
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
					 _billdate=value;
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
					 _operateperson=value;
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
					 _status=value;
				}
			}
			
            //public string VALIDATEPERSON 
            //{
            //    get
            //    {
            //        return _validateperson;
            //    } 
            //    set
            //    {
            //         _validateperson=value;
            //    }
            //}
			
            //public DateTime VALIDATEDATE 
            //{
            //    get
            //    {
            //        return _validatedate;
            //    } 
            //    set
            //    {
            //         _validatedate=value;
            //    }
            //}
			
			public string MEMO 
			{
				get
				{
				    return _memo;
				} 
				set
				{
					 _memo=value;
				}
			}
		#endregion
    }
}