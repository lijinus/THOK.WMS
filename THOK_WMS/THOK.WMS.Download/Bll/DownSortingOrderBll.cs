using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.Download.Dao;
using System.Threading;

namespace THOK.WMS.Download.Bll
{
    public class DownSortingOrderBll
   {

       #region 手动从营销系统下载分拣信息

       /// <summary>
       /// 手动下载
       /// </summary>
       /// <param name="billno"></param>
       /// <returns></returns>
       public string GetSortingOrderById(string orderid)
       {
           string tag = "true";
           using (PersistentManager dbPm = new PersistentManager())
           {
               DownSortingOrderDao dao = new DownSortingOrderDao();
               dao.SetPersistentManager(dbPm);
               try
               {
                   orderid = "ORDER_ID IN (" + orderid + ")";
                   DataTable masterdt = this.GetSortingOrder(orderid);
                   DataTable detaildt = this.GetSortingOrderDetail(orderid);
                   if (masterdt.Rows.Count > 0 && detaildt.Rows.Count>0)
                   {
                       DataSet masterds = this.SaveSortingOrder(masterdt);
                       DataSet detailds = this.SaveSortingOrderDetail(detaildt);
                       this.Insert(masterds, detailds);
                   }
                   else
                       return "没有数据可下载！";
               }
               catch (Exception e)
               {
                   tag = e.Message;
               }
           }
           return tag;
       }
       #endregion

       #region 自动从营销系统下载分拣信息

       /// <summary>
       /// 自动下载订单
       /// </summary>
       /// <returns></returns>
       public string DownSortingOrder()
       {
           string tag = "true";
           using (PersistentManager dbpm = new PersistentManager())
           {
               DownSortingOrderDao dao = new DownSortingOrderDao();
               dao.SetPersistentManager(dbpm);
               try
               {
                   DataTable orderdt = this.GetOrderId();
                   string orderlist = UtinString.StringMake(orderdt, "ORDER_ID");
                   orderlist = UtinString.StringMake(orderlist);
                   orderlist = "ORDER_ID NOT IN(" + orderlist + ")";

                   DataTable masterdt = this.GetSortingOrder(orderlist);
                   DataTable detaildt = this.GetSortingOrderDetail(orderlist);
                   if (masterdt.Rows.Count > 0 && detaildt.Rows.Count > 0)
                   {
                       DataSet masterds = this.SaveSortingOrder(masterdt);
                       DataSet detailds = this.SaveSortingOrderDetail(detaildt);
                       this.Insert(masterds, detailds);
                   }
                   else
                       return "没有可用的数据下载！";
               }
               catch (Exception e)
               {
                   tag = e.Message;
               }
           }
           return tag;
       }

       #endregion

       #region 选择日期从营销系统下载分拣信息

       /// <summary>
       /// 选择日期从营销系统下载分拣信息
       /// </summary>
       /// <param name="startDate"></param>
       /// <param name="endDate"></param>
       /// <returns></returns>
       public string GetSortingOrderDate(string startDate, string endDate)
       {
           string tag = "true";
           using (PersistentManager dbpm = new PersistentManager())
           {
               DownSortingOrderDao dao = new DownSortingOrderDao();
               dao.SetPersistentManager(dbpm);
               try
               {
                   //查询仓库7天内的订单号
                   DataTable orderdt = this.GetOrderId();
                   string orderlist = UtinString.StringMake(orderdt, "ORDER_ID");
                   orderlist = UtinString.StringMake(orderlist);
                   string orderlistDate = "ORDER_DATE>='" + startDate + "' AND ORDER_DATE<='" + endDate + "' AND ORDER_ID NOT IN(" + orderlist + ")";
                   DataTable masterdt = this.GetSortingOrder(orderlistDate);//根据时间查询订单信息

                   string ordermasterlist = UtinString.StringMake(masterdt, "ORDER_ID");//取得根据时间查询的订单号
                   string ordermasterid = UtinString.StringMake(ordermasterlist);
                   ordermasterid = "ORDER_ID IN (" + ordermasterid + ")";
                   DataTable detaildt = this.GetSortingOrderDetail(ordermasterid);//根据订单号查询明细
                   if (masterdt.Rows.Count > 0 && detaildt.Rows.Count > 0)
                   {
                       DataSet masterds = this.SaveSortingOrder(masterdt);
                       DataSet detailds = this.SaveSortingOrderDetail(detaildt);
                       this.Insert(masterds, detailds);
                   }
                   else
                       return "没有可用的数据下载！";
               }
               catch (Exception e)
               {
                   tag = e.Message;
               }
           }
           return tag;
       }

       #endregion

       #region 分页从营销系统据查询分拣订单数据

       /// <summary>
       /// 查询营销系统分拣订单主表数据进行下载
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
       public DataTable GetSortingOrder(int pageIndex, int pageSize)
       {
           using (PersistentManager dbpm = new PersistentManager("YXConnection"))
           {
               DownSortingOrderDao dao = new DownSortingOrderDao();
               dao.SetPersistentManager(dbpm);
               DataTable orderdt = this.GetOrderId();
               string orderlist = UtinString.StringMake(orderdt, "ORDER_ID");
               orderlist = UtinString.StringMake(orderlist);
               orderlist = "ORDER_ID NOT IN(" + orderlist + ")";
               return dao.GetSortingOrder(orderlist);
           }
       }

       /// <summary>
       /// 根据时间查询营销系统分拣订单主表数据进行下载
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
       public DataTable GetSortingOrder(int pageIndex, int pageSize,string startDate,string endDate)
       {
           using (PersistentManager dbpm = new PersistentManager("YXConnection"))
           {
               DownSortingOrderDao dao = new DownSortingOrderDao();
               dao.SetPersistentManager(dbpm);
               DataTable orderdt = this.GetOrderId();
               string orderlist = UtinString.StringMake(orderdt, "ORDER_ID");
               orderlist = UtinString.StringMake(orderlist);
               //orderlist = "ORDER_DATE>='" + startDate + "' AND ORDER_DATE<='" + endDate + "' AND ORDER_ID NOT IN(" + orderlist + ")";
               orderlist = "ORDER_ID NOT IN(" + orderlist + ")";
               DataTable masert = dao.GetSortingOrder("ORDER_ID NOT IN('')");
               DataRow[] orderdr = masert.Select(orderlist);
               return this.SaveSortingOrder(orderdr).Tables[0];
           }
       }

       /// <summary>
       /// 查询营销系统分拣明细表数据
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <param name="inBillNo"></param>
       /// <returns></returns>
       public DataTable GetSortingOrderDetail(int pageIndex, int pageSize, string inBillNo)
       {
           using (PersistentManager dbpm = new PersistentManager("YXConnection"))
           {
               DownSortingOrderDao dao = new DownSortingOrderDao();
               dao.SetPersistentManager(dbpm);
               inBillNo = "ORDER_ID = '" + inBillNo + "'";
               return dao.GetSortingOrderDetail(inBillNo);
           }
       }

       #endregion

       #region 查询仓储分拣信息

       /// <summary>
       /// 查询4天之内的分拣订单
       /// </summary>
       /// <returns></returns>
       public DataTable GetOrderId()
       {
           using (PersistentManager dbpm = new PersistentManager())
           {
               DownSortingOrderDao dao = new DownSortingOrderDao();
               dao.SetPersistentManager(dbpm);
               return dao.GetOrderId();
           }
       }

       /// <summary>
       /// 获取配送中心编码
       /// </summary>
       /// <returns></returns>
       public string GetCompany()
       {
           using (PersistentManager dbpm = new PersistentManager())
           {
               DownDistDao dao = new DownDistDao();
               dao.SetPersistentManager(dbpm);
               return dao.GetCompany().ToString();
           }
       }
       #endregion

       #region 构建表等信息

       /// <summary>
       /// 把下载的数据添加到数据库。
       /// </summary>
       /// <param name="masterds"></param>
       /// <param name="detailds"></param>
       public void Insert(DataSet masterds, DataSet detailds)
       {
           using (PersistentManager pm = new PersistentManager())
           {
               DownSortingOrderDao dao = new DownSortingOrderDao();
               dao.SetPersistentManager(pm);
               if (masterds.Tables["DWV_OUT_ORDER"].Rows.Count > 0)
               {
                   dao.InsertSortingOrder(masterds);
               }
               if (detailds.Tables["DWV_OUT_ORDER_DETAIL"].Rows.Count > 0)
               {
                   dao.InsertSortingOrderDetail(detailds);
               }
           }
       }

       /// <summary>
       /// 保存订单主表信息到虚拟表，传来的是DATATABLE
       /// </summary>
       /// <param name="dr"></param>
       /// <returns></returns>
       public DataSet SaveSortingOrder(DataTable masterdt)
       {
           DataSet ds = this.GenerateEmptyTables();
           foreach (DataRow row in masterdt.Rows)
           {
               DataRow masterrow = ds.Tables["DWV_OUT_ORDER"].NewRow();
               masterrow["ORDER_ID"] = row["ORDER_ID"].ToString().Trim();//订单编号
               masterrow["ORG_CODE"] = row["ORG_CODE"].ToString().Trim();//所属单位编号
               masterrow["SALE_REG_CODE"] = row["SALE_REG_CODE"].ToString().Trim();//营销部编号
               masterrow["ORDER_DATE"] = row["ORDER_DATE"].ToString().Trim();//订单日期
               masterrow["ORDER_TYPE"] = row["ORDER_TYPE"].ToString().Trim();//订单类型
               masterrow["CUST_CODE"] = row["CUST_CODE"].ToString().Trim();//客户编号
               masterrow["CUST_NAME"] = row["CUST_NAME"].ToString().Trim();//客户名称
               masterrow["QUANTITY_SUM"] = Convert.ToDecimal(row["QUANTITY_SUM"].ToString());//总数量
               masterrow["AMOUNT_SUM"] = Convert.ToDecimal(row["AMOUNT_SUM"].ToString());//总金额
               masterrow["DETAIL_NUM"] = Convert.ToInt32(row["DETAIL_NUM"].ToString());//明细数
               masterrow["DIST_BILL_ID"] = row["DIST_BILL_ID"].ToString().Trim();//配车单号
               masterrow["DIST_STA_CODE"] =  row["DIST_STA_CODE"].ToString().Trim();//送货区域编码
               masterrow["DIST_STA_NAME"] =  row["DIST_STA_NAME"].ToString().Trim();//送货区域名称
               masterrow["DELIVER_LINE_CODE"] = row["DELIVER_LINE_CODE"].ToString().Trim();//送货线路编码
               masterrow["DELIVER_LINE_NAME"] = row["DELIVER_LINE_NAME"].ToString().Trim();//送货线路名称
               masterrow["DELIVER_ORDER"] = row["DELIVER_ORDER"];//送货顺序编码
               masterrow["ISACTIVE"] = row["ISACTIVE"];//是否可用
               masterrow["UPDATE_DATE"] = row["UPDATE_DATE"];//更新时间
               masterrow["SORTING_CODE"] = "";//分拣组号
               masterrow["IS_IMPORT"] = "0";
               ds.Tables["DWV_OUT_ORDER"].Rows.Add(masterrow);
           }
           return ds;
       }


       /// <summary>
       /// 保存订单主表信息到虚拟表，传来的是DATATABLE
       /// </summary>
       /// <param name="dr"></param>
       /// <returns></returns>
       public DataSet SaveSortingOrder(DataRow[] masterdr)
       {
           DataSet ds = this.GenerateEmptyTables();
           foreach (DataRow row in masterdr)
           {
               DataRow masterrow = ds.Tables["DWV_OUT_ORDER"].NewRow();
               masterrow["ORDER_ID"] = row["ORDER_ID"].ToString().Trim();//订单编号
               masterrow["ORG_CODE"] = row["ORG_CODE"].ToString().Trim();//所属单位编号
               masterrow["SALE_REG_CODE"] = row["SALE_REG_CODE"].ToString().Trim();//营销部编号
               masterrow["ORDER_DATE"] = row["ORDER_DATE"].ToString().Trim();//订单日期
               masterrow["ORDER_TYPE"] = row["ORDER_TYPE"].ToString().Trim();//订单类型
               masterrow["CUST_CODE"] = row["CUST_CODE"].ToString().Trim();//客户编号
               masterrow["CUST_NAME"] = row["CUST_NAME"].ToString().Trim();//客户名称
               masterrow["QUANTITY_SUM"] = Convert.ToDecimal(row["QUANTITY_SUM"].ToString());//总数量
               masterrow["AMOUNT_SUM"] = Convert.ToDecimal(row["AMOUNT_SUM"].ToString());//总金额
               masterrow["DETAIL_NUM"] = Convert.ToInt32(row["DETAIL_NUM"].ToString());//明细数
               masterrow["DIST_BILL_ID"] = row["DIST_BILL_ID"].ToString().Trim();//配车单号
               masterrow["DIST_STA_CODE"] =  row["DIST_STA_CODE"].ToString().Trim();//送货区域编码
               masterrow["DIST_STA_NAME"] =  row["DIST_STA_NAME"].ToString().Trim();//送货区域名称
               masterrow["DELIVER_LINE_CODE"] = row["DELIVER_LINE_CODE"].ToString().Trim();//送货线路编码
               masterrow["DELIVER_LINE_NAME"] =  row["DELIVER_LINE_NAME"].ToString().Trim();//送货线路名称
               masterrow["DELIVER_ORDER"] = row["DELIVER_ORDER"];//送货顺序编码
               masterrow["ISACTIVE"] = row["ISACTIVE"];//是否可用
               masterrow["UPDATE_DATE"] = row["UPDATE_DATE"];//更新时间
               masterrow["SORTING_CODE"] = "";//分拣组号
               masterrow["IS_IMPORT"] = "0";
               ds.Tables["DWV_OUT_ORDER"].Rows.Add(masterrow);
           }
           return ds;
       }

       /// <summary>
       /// 保存订单明细到虚拟表，传来DataTable
       /// </summary>
       /// <param name="dr"></param>
       /// <returns></returns>
       public DataSet SaveSortingOrderDetail(DataTable detaildt)
       {
           DataSet ds = this.GenerateEmptyTables();
           foreach (DataRow row in detaildt.Rows)
           {
               string id = DateTime.Now.ToString("yyMMddHHmmssfff");
               id = id.Substring(1, 14);
               DataRow detailrow = ds.Tables["DWV_OUT_ORDER_DETAIL"].NewRow();
               detailrow["ORDER_DETAIL_ID"] = id;// row["ORDER_DETAIL_ID"].ToString().Trim();
               detailrow["ORDER_ID"] = row["ORDER_ID"].ToString().Trim();
               detailrow["BRAND_CODE"] = row["BRAND_CODE"].ToString().Trim();
               detailrow["BRAND_NAME"] = row["BRAND_NAME"].ToString().Trim();
               detailrow["BRAND_UNIT_NAME"] = row["BRAND_UNIT_NAME"].ToString().Trim();
               detailrow["QUANTITY"] = Convert.ToDecimal(row["QUANTITY"]);
               detailrow["PRICE"] = Convert.ToDecimal(row["PRICE"]);
               detailrow["AMOUNT"] = Convert.ToDecimal(row["AMOUNT"]);
               detailrow["SORTING_CODE"] = "";
               detailrow["IS_IMPORT"] = "0";
               detailrow["ORDER_DATE"] = "";// row["ORDER_DATE"].ToString().Trim();
               ds.Tables["DWV_OUT_ORDER_DETAIL"].Rows.Add(detailrow);
               Thread.Sleep(1);
           }
           return ds;
       }

       /// <summary>
       /// 构建订单主表和细表虚拟表
       /// </summary>
       /// <returns></returns>
       private DataSet GenerateEmptyTables()
       {
           DataSet ds = new DataSet();
           DataTable mastertable = ds.Tables.Add("DWV_OUT_ORDER");
           mastertable.Columns.Add("ORDER_ID");
           mastertable.Columns.Add("ORG_CODE");
           mastertable.Columns.Add("SALE_REG_CODE");
           mastertable.Columns.Add("ORDER_DATE");
           mastertable.Columns.Add("ORDER_TYPE");
           mastertable.Columns.Add("CUST_CODE");
           mastertable.Columns.Add("CUST_NAME");
           mastertable.Columns.Add("QUANTITY_SUM");
           mastertable.Columns.Add("AMOUNT_SUM");
           mastertable.Columns.Add("DETAIL_NUM");
           mastertable.Columns.Add("DIST_BILL_ID");
           mastertable.Columns.Add("DIST_STA_CODE");
           mastertable.Columns.Add("DIST_STA_NAME");
           mastertable.Columns.Add("DELIVER_LINE_CODE");
           mastertable.Columns.Add("DELIVER_LINE_NAME");
           mastertable.Columns.Add("DELIVER_ORDER");
           mastertable.Columns.Add("ISACTIVE");
           mastertable.Columns.Add("UPDATE_DATE");
           mastertable.Columns.Add("SORTING_CODE");
           mastertable.Columns.Add("IS_IMPORT");

           DataTable detailtable = ds.Tables.Add("DWV_OUT_ORDER_DETAIL");
           detailtable.Columns.Add("ORDER_DETAIL_ID");
           detailtable.Columns.Add("ORDER_ID");
           detailtable.Columns.Add("BRAND_CODE");
           detailtable.Columns.Add("BRAND_NAME");
           detailtable.Columns.Add("BRAND_UNIT_NAME");
           detailtable.Columns.Add("QUANTITY");
           detailtable.Columns.Add("PRICE");
           detailtable.Columns.Add("AMOUNT");
           detailtable.Columns.Add("SORTING_CODE");
           detailtable.Columns.Add("IS_IMPORT");
           detailtable.Columns.Add("ORDER_DATE");
           return ds;
       }
       #endregion

       #region 其他查询下载分拣数据的方法


       /// <summary>
       /// 根据用户选择的订单下载分拣线订单主表
       /// </summary>
       /// <returns></returns>
       public DataTable GetSortingOrder(string orderid)
       {
           using (PersistentManager dbpm = new PersistentManager("YXConnection"))
           {
               DownSortingOrderDao dao = new DownSortingOrderDao();
               dao.SetPersistentManager(dbpm);
               return dao.GetSortingOrder(orderid);
           }
       }

       /// <summary>
       /// 根据用户选择的订单下载分拣线订单明细表
       /// </summary>
       /// <returns></returns>
       public DataTable GetSortingOrderDetail(string orderid)
       {
           using (PersistentManager dbpm = new PersistentManager("YXConnection"))
           {
               DownSortingOrderDao dao = new DownSortingOrderDao();
               dao.SetPersistentManager(dbpm);
               return dao.GetSortingOrderDetail(orderid);
           }
       }

       #endregion

   }
}
