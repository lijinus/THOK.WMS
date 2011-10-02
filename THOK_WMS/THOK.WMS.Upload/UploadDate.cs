using System;
using System.Collections.Generic;
using System.Text;
using THOK.WMS.BLL;
using THOK.WMS.Upload.Bll;

namespace THOK.WMS.Upload
{
   public class UploadDate
    {
       public event ScheduleEventHandler OnSchedule = null;
       UploadBll updateBll = new UploadBll();

       public void UploadInfoData()
       {
           string tag = "";
           try
           {
               //上报卷烟信息表
               //tag = updateBll.FindProduct();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报卷烟信息", 1, 18));

               //上报组织结构信息表
               tag = updateBll.FindOrganization();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报组织结构信息", 2, 18));

               //上报人员信息表
               tag = updateBll.FindPerson();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报人员信息", 3, 18));

               //上报客户信息表
               tag = updateBll.FindCustomer();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报客户信息", 4, 18));

               //上报仓储属性表
               tag = updateBll.FindIbasSorting();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报仓储属性信息", 5, 18));

               //上报仓库库存表
              tag = updateBll.FindStoreStock();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报仓库库存信息", 6, 18));

               //上报业务库存表
               tag = updateBll.FindBusiStock();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报业务库存信息", 7, 18));

               //上报仓库入库单据主表
               tag = updateBll.FindInMasterBill();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报入库单据主表信息", 8, 18));

               //上报仓库入库单据细表
               tag = updateBll.FindInDetailBill();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报入库单据细表信息", 9, 18));

               //上报入库业务单据表
               tag = updateBll.FindInBusiBill();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报入库业务单据信息", 10, 18));

               //上报仓库出库单据主表
               tag = updateBll.FindOutMasterBill();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报出库单据主表信息", 11, 18));

               //上报仓库出库单据细表
               tag = updateBll.FindOutDetailBill();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报出库单据细表信息", 12, 18));

               //上报出库业务单据表
               tag = updateBll.FindOutBusiBill();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报出库业务单据表信息", 13, 18));

               ////上报分拣订单主表
               //tag = updateBll.FindIordMasterOrder();
               //if (OnSchedule != null)
               //    OnSchedule(this, new ScheduleEventArgs(1, "正在上报分拣订单主表信息", 14, 18));

               ////上报分拣订单细表
               //tag = updateBll.FindIordDetailOrder();
               //if (OnSchedule != null)
               //    OnSchedule(this, new ScheduleEventArgs(1, "正在上报分拣订单细表信息", 15, 18));

               ////上报分拣情况表
               //tag = updateBll.FindSortStatus();
               //if (OnSchedule != null)
               //    OnSchedule(this, new ScheduleEventArgs(1, "正在上报分拣情况表信息", 16, 18));

               ////上报分拣线信息表
               //tag = updateBll.FindIdpsSorting();
               //if (OnSchedule != null)
               //    OnSchedule(this, new ScheduleEventArgs(1, "正在上报分拣线信息", 17, 18));

               //上报同步状态表
               tag = updateBll.FindSynchroInfo();
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(1, "正在上报同步状态信息", 18, 18));

           }
           catch (Exception exp)
           {
               if (OnSchedule != null)
                   OnSchedule(this, new ScheduleEventArgs(OptimizeStatus.ERROR, exp.Message));
               throw new Exception(exp.Message);
           }
       }

    }
}
