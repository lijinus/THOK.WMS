using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.WMS.BLL
{
    public delegate void ScheduleEventHandler(object sender, ScheduleEventArgs e);

    public enum OptimizeStatus { WAITING, PROCESSING, COMPLETE, ERROR };

    public class ScheduleEventArgs
    {
        private int scheduleStep = 0;

        private string stepName = "";

        private int completeCount;

        private int totalCount;

        private string message;

        private OptimizeStatus optimizeStatus = OptimizeStatus.WAITING;

        private bool isContinure = true;

        public bool IsContinure
        {
            get { return isContinure; }
            set { isContinure = value; }
        }

        public int ScheduleStep
        {
            get
            {
                return scheduleStep;
            }
        }

        public string StepName
        {
            get
            {
                return stepName;
            }
        }

        public int CompleteCount
        {
            get
            {
                return completeCount;
            }
        }

        public int TotalCount
        {
            get
            {
                return totalCount;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = null;
            }
        }

        public OptimizeStatus OptimizeStatus
        {
            get
            {
                return optimizeStatus;
            }
            set
            {
                optimizeStatus = value;
            }
        }
        public ScheduleEventArgs(OptimizeStatus optimizeStatus)
        {
            this.optimizeStatus = optimizeStatus;
        }

        public ScheduleEventArgs(OptimizeStatus optimizeStatus, string message)
        {
            this.optimizeStatus = optimizeStatus;
            this.message = message;
        }
        public ScheduleEventArgs(int scheduleStep, string stepName, int completeCount, int totalCount)
        {
            this.scheduleStep = scheduleStep;
            this.stepName = stepName;
            this.completeCount = completeCount;
            this.totalCount = totalCount;
            this.optimizeStatus = OptimizeStatus.PROCESSING;
        }

        public new string ToString()
        {
            string msg = "";
            if (optimizeStatus != OptimizeStatus.PROCESSING)
            {
                msg = string.Format("<root><status>{0}</status><message>{1}</message></root>", optimizeStatus.ToString(), message);
            }
            else
            {
                msg = string.Format("<root><status>{0}</status><message>{1}</message><step>{2}</step><completecount>{3}</completecount><totalcount>{4}</totalcount></root>",
                    optimizeStatus.ToString(), stepName, scheduleStep, completeCount, totalCount);
            }
            return msg;
        }
    }
}
