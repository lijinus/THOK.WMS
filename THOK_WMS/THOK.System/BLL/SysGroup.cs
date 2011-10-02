using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.System.Dao;

namespace THOK.System.BLL
{
    public class SysGroup
    {
        private string strTableView = "v_sys_GroupList";
        private string strPrimaryKey = "GroupID";
        //private string strOrderByFields = "GroupName ASC";
        private string strQueryFields = "GroupID,GroupName,Memo,State";

        public DataSet GetGroupList(int pageIndex, int pageSize, string filter, string OrderByFields)
        {
            using (THOK.Util.PersistentManager pm = new THOK.Util.PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.QueryGroup(strTableView, strPrimaryKey, strQueryFields, pageIndex, pageSize, OrderByFields, filter, strTableView);
            }
        }

        public DataSet GetGroupList()
        {
            string strsql = @"SELECT * FROM V_SYS_GROUPLIST";
            SysGroupDao GroupDao = new SysGroupDao();
            return GroupDao.GetData(strsql);
        }

        public int GetGroupMemberCount(int GroupID)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetGroupMemberCount(GroupID);
            }
        }

        public int GetRowCount(string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetRowCount(strTableView, filter);
            }
        }

        public int GetRemindRowCount(string TableName, string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetRowCount(TableName, filter);
            }
        }

        /// <summary>
        /// 组操作菜单
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public DataSet GetGroupRole(int GroupID, string SystemName)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                string sql = "select distinct(SubModuleCode),n.SystemName,m.ModuleID,OperatorCode,n.MenuCode,n.MenuTitle,n.MenuParent,n.MenuUrl,n.MenuImage,n2.MenuImage as ParentImage,n.OrderIndex" +
                               " from sys_GroupOperationList o  " +
                               " left join sys_ModuleList  m on m.ModuleID=o.ModuleID " +
                               " left join sys_GroupList g on g.GroupID=o.GroupID " +
                               " left join sys_UserLIst  u on u.GroupID=g.GroupID " +
                               " left join sys_Menu      n on n.MenuCode=m.SubModuleCode" +
                               " left join sys_Menu      n2 on n2.MenuCode=substring(n.MenuCode,1,8)" +
                               " where g.GroupID=" + GroupID + "and n.SystemName='" + SystemName + "'" +
                               " order by n.OrderIndex, SubModuleCode ";
                return GroupDao.GetData(sql);
            }
        }

        /// <summary>
        /// 获取组操作模块的ID
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public DataSet GetGroupOperation(int GroupID)
        {
            string sql = "select ModuleID from sys_GroupOperationList where GroupID=" + GroupID;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetData(sql);
            }
        }

        public DataSet GetGroupOperationWithWES(int GroupID)
        {
            string sql = @"select * from sys_GroupOperationList 
                            where GroupID="+GroupID+@" and ModuleID in 
                            (select ModuleID from sys_ModuleList 
                            where ModuleCode in 
                            (select MenuCode as  ModuleCode 
                            from sys_Menu where SystemName='WES'))" ;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetData(sql);
            }
        }

        /// <summary>
        /// 获取系统一级模块
        /// </summary>
        /// <returns></returns>
        public DataSet GetSystemModules()
        {
            string sql = "select distinct(ModuleCode) ,MenuTitle from sys_ModuleList  m left join sys_Menu n  on m.ModuleCode=n.MenuCode order by ModuleCode";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetData(sql);
            }
        }

        public DataSet GetSystemModules(string SystemName)
        {
            string sql = string.Format("select distinct(ModuleCode) ,MenuTitle from sys_ModuleList  m left join sys_Menu n  on m.ModuleCode=n.MenuCode where n.SystemName='{0}' order by ModuleCode",SystemName);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetData(sql);
            }
        }



        /// <summary>
        /// 获取系统二级模块
        /// </summary>
        /// <returns></returns>
        public DataSet GetSystemSubModules()
        {
            string sql = "select distinct(N.ID),MenuTitle,m.SubModuleCode,MenuParent,ModuleCode from sys_ModuleList m left join sys_Menu n on m.SubModuleCode=n.MenuCode order by m.SubModuleCode";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetData(sql);
            }
        }

        public DataSet GetSystemSubModules(string SystemName)
        {
            string sql = string.Format("select distinct(N.ID),MenuTitle,m.SubModuleCode,MenuParent,ModuleCode,n.OrderIndex from sys_ModuleList m left join sys_Menu n on m.SubModuleCode=n.MenuCode where SystemName='{0}' order by n.OrderIndex, m.SubModuleCode",SystemName);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetData(sql);
            }
        }

        /// <summary>
        /// 获取系统操作项
        /// </summary>
        /// <returns></returns>
        public DataSet GetSystemOperations()
        {
            string sql = "select SubModuleName,SubModuleCode,OperatorDescription,ModuleID from sys_ModuleList order by SubModuleCode";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetData(sql);
            }
        }

        public DataSet GetSystemOperations(string SystemName)
        {
            string sql =string.Format( @"select SubModuleName,SubModuleCode,OperatorDescription,ModuleID 
                            from sys_ModuleList 
                            where ModuleCode in 
                            (select MenuCode from sys_Menu where SystemName='{0}')
                            order by SubModuleCode",SystemName);
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetData(sql);
            }
        }

        /////////////////

        public DataSet GetWESModules()
        {
            string sql = "select distinct(ModuleCode) ,MenuTitle from sys_ModuleList  m left join sys_Menu n  on m.ModuleCode=n.MenuCode where n.SystemName='WES' order by ModuleCode";
            SysGroupDao GroupDao = new SysGroupDao();
            return GroupDao.GetData(sql);
        }

        public DataSet GetWESSubModules()
        {
            string sql = "select distinct(N.ID),MenuTitle,m.SubModuleCode,MenuParent,ModuleCode from sys_ModuleList m left join sys_Menu n on m.SubModuleCode=n.MenuCode where n.SystemName='WES' order by m.SubModuleCode";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetData(sql);
            }
        }

        public DataSet GetWESOperations()
        {
            string sql = @"select SubModuleName,SubModuleCode,OperatorDescription,ModuleID 
                            from sys_ModuleList 
                            where ModuleCode in 
                            (select MenuCode from sys_Menu where SystemName='WES')
                            order by SubModuleCode";
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                return GroupDao.GetData(sql);
            }
        }


        public bool Insert(DataSet dataSet)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                GroupDao.InsertEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        public bool Update(DataSet dataSet)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                GroupDao.UpdateEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        public bool Delete(DataSet dataSet)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                GroupDao.DeleteEntity(dataSet);
                flag = true;
            }
            return flag;
        }

        public bool SetGroupOperation(string sql)
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysGroupDao GroupDao = new SysGroupDao();
                GroupDao.SetData(sql);
                flag = true;
            }
            return flag;
        }

        public void DeleteGroupByGroupID(string groupid)
        {
            SysGroupDao GroupDao = new SysGroupDao();
            string strsql = "DELETE FROM sys_GroupList where groupid =" + groupid;
            GroupDao.SetData(strsql);
            strsql = "DELETE FROM sys_UserList where groupid =" + groupid;
            GroupDao.SetData(strsql);
        }

        public void UpdateGroupByGroupName(string groupname, string memo)
        {
            SysGroupDao GroupDao = new SysGroupDao();
            string strsql = "UPDATE sys_GroupList set memo='" + memo + "' where groupname='" + groupname+"'";
            GroupDao.SetData(strsql);
        }

        public DataSet FindGroupByGroupName(string groupname)
        {
            SysGroupDao GroupDao = new SysGroupDao();
            string strsql = "select * from sys_GroupList where groupname='" + groupname+"'";
            return GroupDao.GetData(strsql);
        }

        public void AddNewGroup(string groupname,string memo)
        {
            SysGroupDao GroupDao = new SysGroupDao();
            string strsql = "insert sys_GroupList (groupname,memo,state) values ('"+groupname+"','"+memo+"',1)";
            GroupDao.SetData(strsql);
        }
    }
}
