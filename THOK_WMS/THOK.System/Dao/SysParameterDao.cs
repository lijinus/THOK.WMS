using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;


namespace THOK.System.Dao
{
    public class SysParameterDao : BaseDao
    {
        public void InsertEntity(DataSet dataSet)
        {
        }
        public void UpdateEntity(DataSet dataSet)
        {
        }
        //public void Update(string sql)
        //{
        //    ExecuteNonQuery(sql);
        //}


        public void DeleteEntity(DataSet dataSet)
        {
        }

        /// <summary>
        /// 取得想要字段的参数值
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns></returns>
        //public string FindSingle(string paraName)
        //{
        //    ParameterCreate parameterCreate = new ParameterCreate();
        //    parameterCreate.AddList("@columnName", paraName);
        //    return ExecuteScalar("PROD_PARA", false, parameterCreate.GetName(), parameterCreate.GetValue(), parameterCreate.GetTypes());
        //}

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns></returns>
        public string GetParameterValue(string paraName)
        {
            return ExecuteScalar(string.Format("select PARAMETERVALUE from AS_SYS_PARAMETER WHERE PARAMETERNAME='{0}'", paraName)).ToString();
        }

        public DataSet AllGetParameterValue(string paraName)
        {
            return ExecuteQuery(string.Format("select PARAMETERVALUE from AS_SYS_PARAMETER WHERE PARAMETERNAME='{0}'", paraName));
        }
        public void UpdataData(string RemoteServerDB, string RemoteServerIP, string RemoteServerUserID, string RemoteServerPassword, string DatabaseType, string OuterBatch, string ChannelBlankCount, string TowerBlankCount)
        {
            ExecuteNonQuery("Update AS_SYS_PARAMETER set PARAMETERVALUE='" + RemoteServerDB + "' WHERE PARAMETERNAME='RemoteServerDB'");
            ExecuteNonQuery("Update AS_SYS_PARAMETER set PARAMETERVALUE='" + RemoteServerIP + "' WHERE PARAMETERNAME='RemoteServerIP'");
            ExecuteNonQuery("Update AS_SYS_PARAMETER set PARAMETERVALUE='" + RemoteServerUserID + "' WHERE PARAMETERNAME='RemoteServerUserID'");
            ExecuteNonQuery("Update AS_SYS_PARAMETER set PARAMETERVALUE='" + RemoteServerPassword + "' WHERE PARAMETERNAME='RemoteServerPassword'");
            ExecuteNonQuery("Update AS_SYS_PARAMETER set PARAMETERVALUE='" + DatabaseType + "' WHERE PARAMETERNAME='DatabaseType'");
            ExecuteNonQuery("Update AS_SYS_PARAMETER set PARAMETERVALUE='" + OuterBatch + "' WHERE PARAMETERNAME='OuterBatch'");
            ExecuteNonQuery("Update AS_SYS_PARAMETER set PARAMETERVALUE='" + ChannelBlankCount + "' WHERE PARAMETERNAME='ChannelBlankCount'");
            ExecuteNonQuery("Update AS_SYS_PARAMETER set PARAMETERVALUE='" + TowerBlankCount + "' WHERE PARAMETERNAME='TowerBlankCount'");

        }

    }
}
