using System;
using System.Collections.Generic;
using System.Text;
using THOK.System.Dao;
using System.Data;
using THOK.Util;

namespace THOK.System.BLL
{
    public class SysUser
    {
        private string strTableView = "v_sys_UserList";
        private string strPrimaryKey = "UserID";
        //private string strOrderByFields = "UserName ASC";
        private string strQueryFields = "UserID,UserName,UserPassword,Memo,EmployeeCode,EmployeeName";

        public DataSet GetUserList(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                return UserDao.QueryUser(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public DataSet GetGroupUser(int GroupID)
        {
            string sql = "select UserID, UserName ,GroupName  from sys_UserList u left join sys_GroupList g on u.GroupID=g.GroupID where u.GroupID=" + GroupID;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                return UserDao.GetUser(sql);
            }
        }

        public DataSet GetAllUser()
        {
            string sql = "select UserName ,GroupName ,UserID from sys_UserList u left join sys_GroupList g on u.GroupID=g.GroupID ";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                return UserDao.GetUser(sql);
            }
        }

        public DataSet GetUserInfo(string UserName)
        {
            string sql = string.Format("select a.*," +
                           " d.sys_PageCount,d.grid_ColumnTitleFont,d.grid_ContentFont," +
                           " d.grid_ColumnTextAlign,d.grid_ContentTextAlign,d.grid_NumberColumnAlign," +
                           " d.grid_MoneyColumnAlign,d.grid_SelectMode,d.grid_OddRowColor," +
                           " d.grid_EvenRowColor,d.grid_IsRefreshBeforeAdd,d.grid_IsRefreshBeforeUpdate," +
                           " d.grid_IsRefreshBeforeDelete,d.sys_PrintForm,d.pager_ShowPageIndex," +
                           " e.ParameterValue as SessionTimeOut" +
                           "  from sys_UserList a" +
                           " left join sys_UserConfigPlan b on a.UserID=b.UserID " +
                           " left join sys_ConfigPlan c on b.ConfigPlanID=c.ConfigPlanID " +
                           " left join sys_UserSysInfo d on a.UserID=d.UserID " +
                           " left join sys_SystemParameter e on e.ParameterName='sys_SessionTimeOut' " +
                           "where UserName='{0}'", UserName);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                return UserDao.GetUser(sql);
            }
        }

        /// <summary>
        /// 获取用户有权限操作的一级模块
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataSet GetUserOperateModule(string UserName)
        {
            string sql = "select distinct(n2.MenuTitle),n2.ID, n2.OrderIndex" +
                           " from sys_GroupOperationList o   " +
                           " left join sys_ModuleList  m on m.ModuleID=o.ModuleID " +
                           " left join sys_GroupList g on g.GroupID=o.GroupID " +
                           " left join sys_UserLIst  u on u.GroupID=g.GroupID " +
                           " left join sys_Menu      n on n.MenuCode=m.SubModuleCode " +
                           " left join sys_Menu      n2 on n2.MenuCode=substring(n.MenuCode,1,8) " +
                            " where n2.SystemName='WMS' and UserName='" + UserName + "'order by n2.OrderIndex";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                return UserDao.GetData(sql);
            }
        }

        /// <summary>
        /// 获取用户有权限操作的二级模块
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataSet GetUserOperateSubModule(string UserName)
        {
            string sql = "select distinct(m.SubModuleCode),n.MenuParent,n.MenuTitle,n.ID,n.OrderIndex " +
                           " from sys_GroupOperationList o   " +
                           " left join sys_ModuleList  m on m.ModuleID=o.ModuleID " +
                           " left join sys_GroupList g on g.GroupID=o.GroupID " +
                           " left join sys_UserLIst  u on u.GroupID=g.GroupID " +
                           " left join sys_Menu      n on n.MenuCode=m.SubModuleCode " +
                           " where n.SystemName='WMS' AND UserName='" + UserName + "'order by n.OrderIndex ";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                return UserDao.GetData(sql);
            }
        }

        public DataSet GetUserQuickDesktop(int UserID)
        {
            string sql = "SELECT m.MenuParent, m.MenuTitle, q.ModuleID, m.MenuImage, m.DestopImage, m.MenuUrl " +
                         "FROM dbo.sys_QuickDestop AS q LEFT OUTER JOIN  dbo.sys_Menu AS m ON q.ModuleID = m.ID " +
                         "WHERE     (q.UserID = " + UserID + ") AND (q.ModuleID IN " +
                         "(SELECT DISTINCT n.ID " +
                         " FROM dbo.sys_GroupOperationList AS o LEFT OUTER JOIN " +
                         "dbo.sys_ModuleList AS m ON m.ModuleID = o.ModuleID LEFT OUTER JOIN " +
                         "dbo.sys_GroupList AS g ON g.GroupID = o.GroupID LEFT OUTER JOIN " +
                         "dbo.sys_UserList AS u ON u.GroupID = g.GroupID LEFT OUTER JOIN " +
                         " dbo.sys_Menu AS n ON n.MenuCode = m.SubModuleCode " +
                         "WHERE (u.UserID = " + UserID + "))) order by m.MenuCode";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                return UserDao.GetData(sql);
            }
        }


        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                return UserDao.GetRowCount(strTableView, filter);
            }
        }


        public bool Insert(DataSet dataSet)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                UserDao.InsertEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        public bool Update(DataSet dataSet)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                UserDao.UpdateEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        public bool Delete(DataSet dataSet)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                UserDao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        public bool DeleteUserFromGroup(int UserID)
        {
            string sql = "Update  sys_UserList set GroupID='' where UserID=" + UserID;
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                UserDao.DeleteEntity(sql);
                flag = true;
            }
            return flag;
        }

        public bool AddUserToGroup(string sql)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                UserDao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool SetUserQuickDesktop(string sql)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysUserDao UserDao = new SysUserDao();
                UserDao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        public bool ChangePassword(string UserName, string NewPassword)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                string sql = string.Format("update sys_UserList set UserPassword='{0}' where UserName='{1}'", NewPassword, UserName);
                SysUserDao UserDao = new SysUserDao();
                UserDao.SetData(sql);
                flag = true;
            }
            return flag;
        }
    }
}
