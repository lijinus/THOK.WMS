using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.WMS.Dao;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class CheckBillMaster
    {
        private string strTableView = "v_wms_check_billmaster";
        private string strPrimaryKey = "ID";
        private string strQueryFields = "*";

        public DataSet QueryCheckBillMaster(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CheckBillMasterDao dao = new CheckBillMasterDao();
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
                CheckBillMasterDao dao = new CheckBillMasterDao();
                return dao.GetRowCount(strTableView, filter);
            }
        }

        public string GetNewBillNo()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                MoveBillMasterDao dao = new MoveBillMasterDao();
                DataSet ds = dao.GetData(string.Format("select TOP 1 BILLNO FROM WMS_CHECK_BILLMASTER where BILLNO LIKE '{0}%' order by BILLNO DESC", System.DateTime.Now.ToString("yyyyMMdd")));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return System.DateTime.Now.ToString("yyyyMMdd") + "0001" + "C";
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
                    return System.DateTime.Now.ToString("yyyyMMdd") + newcode + "C";
                }
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CheckBillMasterDao dao = new CheckBillMasterDao();

                string sql = string.Format("Insert into wms_check_billmaster (BILLNO,WH_CODE,BILLTYPE,BILLDATE,OPERATEPERSON,STATUS,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                                             , this.BILLNO,
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

        /// <summary>
        /// 批量插入盘点明细
        /// </summary>
        /// <param name="tableCell"></param>
        /// <param name="EmployeeCode"></param>
        /// <returns></returns>
        public bool BatchInsertBill(DataTable tableCell,string EmployeeCode)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                //if(tableCell)
                CheckBillMasterDao dao = new CheckBillMasterDao();
                CheckBillDetailDao daoDetail = new CheckBillDetailDao();
                string billNo = GetNewBillNo();
                string wh_code=tableCell.Rows[0]["WH_CODE"].ToString();
                string sql = string.Format("Insert into wms_check_billmaster (BILLNO,WH_CODE,BILLTYPE,BILLDATE,OPERATEPERSON,STATUS,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                                             , billNo, wh_code, "401", System.DateTime.Now.ToString("yyyy-MM-dd"), EmployeeCode, "1", "");

                dao.SetData(sql);
                DataTable tableTmep = daoDetail.GetData("select * from wms_check_billdetail where 1=0").Tables[0];
                foreach (DataRow row in tableCell.Rows)
                {
                    DataRow newRow = tableTmep.NewRow();
                    newRow["BILLNO"] = billNo;
                    newRow["CELLCODE"] = row["CELLCODE"];
                    string p = row["CURRENTPRODUCT"].ToString();
                    newRow["PRODUCTCODE"] = p;
                    newRow["UNITCODE"] = row["UNITCODE"];
                    newRow["RECORDQUANTITY"] = row["QUANTITY"];
                    newRow["COUNTQUANTITY"] = 0;//row["QUANTITY"];//盘点数量默认为账面数量
                    newRow["STATUS"] ="0";
                    tableTmep.Rows.Add(newRow);
                }
                daoDetail.BatchInsertDetail(tableTmep);
                flag = true;
            }
            return flag;
        }

        public bool Update()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CheckBillMasterDao dao = new CheckBillMasterDao();
                string sql = string.Format("update wms_check_billmaster set WH_CODE='{1}',BILLTYPE='{2}',BILLDATE='{3}',OPERATEPERSON='{4}',STATUS='{5}',MEMO='{6}'  where BILLNO='{0}'"
                                             ,this.BILLNO,
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
                CheckBillMasterDao dao = new CheckBillMasterDao(); 
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
                CheckBillMasterDao dao = new CheckBillMasterDao();
                DataSet dsTemp = dao.GetData("select * from v_wms_CHECK_billdetail where BILLNO='" + BillNo + "'");
                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in dsTemp.Tables[0].Rows)
                {
                    sb.Append(string.Format("update WMS_WH_CELL SET ISLOCKED='1' WHERE CELLCODE='{0}';", row["CELLCODE"].ToString()));
                }
                sb.Append(string.Format("update WMS_CHECK_BILLMASTER SET STATUS='2', VALIDATEPERSON='{0}',VALIDATEDATE='{1}' WHERE BILLNO='{2}';", EmployeeCode, System.DateTime.Now.ToString("yyyy-MM-dd"), BillNo));
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
                CheckBillMasterDao dao = new CheckBillMasterDao();
                DataSet dsTemp = dao.GetData("select * from v_wms_Check_billdetail where BILLNO='" + BillNo + "'");
                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in dsTemp.Tables[0].Rows)
                {
                    sb.Append(string.Format("update WMS_WH_CELL SET ISLOCKED='0' WHERE CELLCODE='{0}';", row["CELLCODE"].ToString()));
                }
                sb.Append(string.Format("update WMS_Check_BILLMASTER SET STATUS='1', VALIDATEPERSON='',VALIDATEDATE=null where BILLNO='{0}'", BillNo));
                dao.SetData(sb.ToString());
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 确认盘点损益，生成损益单
        /// </summary>
        /// <param name="BillNo"></param>
        /// <param name="EmployeeCode"></param>
        /// <returns></returns>
        public bool ConfirmProfitOrLoss(string BillNo,string EmployeeCode)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                CheckBillMasterDao dao = new CheckBillMasterDao();
                DataSet dsTemp = dao.GetData("select * from v_wms_CHECK_billdetail where BILLNO='" + BillNo + "'");
                StringBuilder sb = new StringBuilder();
                ProfitLossBillMaster plMaster=new ProfitLossBillMaster();
                string NewProfitLossBillNo =plMaster.GetNewBillNo();
                string wh_code = dao.GetData("select * from v_wms_CHECK_billmaster where BILLNO='" + BillNo + "'").Tables[0].Rows[0]["WH_CODE"].ToString();
                sb.Append(string.Format("Insert into WMS_PL_BILLMASTER (BILLNO,BILLTYPE,WH_CODE,CHECKBILLNO,BILLDATE,OPERATEPERSON,STATUS,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');"
				                             ,NewProfitLossBillNo,"",wh_code,BillNo,System.DateTime.Now.ToString("yyyy-MM-dd"),EmployeeCode,"1",""));
                foreach (DataRow row in dsTemp.Tables[0].Rows)
                {
                    string countProduct = row["COUNTPRODUCT"].ToString().Trim();
                    decimal diffQty = Convert.ToDecimal(row["DIFF_QTY"]);
                    if (diffQty !=0.00M)//数量差异
                    {
                        if (countProduct == row["PRODUCTCODE"].ToString().Trim())
                        {
                            sb.Append(string.Format("Insert into WMS_PL_BILLDETAIL (BILLNO,CELLCODE,PRODUCTCODE,QUANTITY,UNITCODE,PRICE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}');"
                                                    , NewProfitLossBillNo, row["CELLCODE"], row["PRODUCTCODE"], row["DIFF_QTY"], row["UNITCODE"], 0.00M, ""));
                        }
                        else//货位产品异常
                        {
                            if (row["PRODUCTCODE"].ToString().Trim().Length > 0)
                            {
                                //原帐面产品报损
                                sb.Append(string.Format("Insert into WMS_PL_BILLDETAIL (BILLNO,CELLCODE,PRODUCTCODE,QUANTITY,UNITCODE,PRICE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}');"
                                                        , NewProfitLossBillNo, row["CELLCODE"], row["PRODUCTCODE"], "-" + row["RECORDQUANTITY"].ToString(), row["UNITCODE"], 0.00M, ""));

                            }
                            //实际存放产品报益
                            sb.Append(string.Format("Insert into WMS_PL_BILLDETAIL (BILLNO,CELLCODE,PRODUCTCODE,QUANTITY,UNITCODE,PRICE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}');"
                                                    , NewProfitLossBillNo, row["CELLCODE"], row["PRODUCTCODE"], row["COUNTQUANTITY"], row["UNITCODE"], 0.00M, ""));

                        }
                    }
                    else
                    {
                        decimal recordQty = Convert.ToDecimal(row["RECORDQUANTITY"]);
                        if (recordQty > 0.00M && countProduct != row["PRODUCTCODE"].ToString().Trim())
                        {
                            if (row["PRODUCTCODE"].ToString().Trim().Length > 0)
                            {
                                //原帐面产品报损
                                sb.Append(string.Format("Insert into WMS_PL_BILLDETAIL (BILLNO,CELLCODE,PRODUCTCODE,QUANTITY,UNITCODE,PRICE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}');"
                                                        , NewProfitLossBillNo, row["CELLCODE"], row["PRODUCTCODE"], "-" + row["RECORDQUANTITY"].ToString(), row["UNITCODE"], 0.00M, ""));

                            }
                            //实际存放产品报益
                            sb.Append(string.Format("Insert into WMS_PL_BILLDETAIL (BILLNO,CELLCODE,PRODUCTCODE,QUANTITY,UNITCODE,PRICE,MEMO) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}');"
                                                    , NewProfitLossBillNo, row["CELLCODE"], row["PRODUCTCODE"], row["COUNTQUANTITY"], row["UNITCODE"], 0.00M, ""));
                        }
                    }
                    dao.SetData("update wms_wh_cell set  ISLOCKED='0' where CELLCODE='"+row["CELLCODE"].ToString()+"'");
                }
                sb.Append(string.Format("UPDATE WMS_CHECK_BILLMASTER SET STATUS='5'  WHERE BILLNO='{0}';",BillNo));
                dao.SetData(sb.ToString());
                flag = true;
            }
            return flag;
        }

        #region property
        private int _id;
        private string _billno;
        private string _wh_code;
        private string _billtype;
        private DateTime _billdate;
        private string _operateperson;
        private string _status;
        //private string _validateperson;
        //private DateTime _validatedate;
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

        //public string VALIDATEPERSON
        //{
        //    get
        //    {
        //        return _validateperson;
        //    }
        //    set
        //    {
        //        _validateperson = value;
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
        //        _validatedate = value;
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
                _memo = value;
            }
        }
        #endregion
    }
}
