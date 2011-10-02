using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.System.Dao;

namespace THOK.System.BLL
{
    public  class SysParameter
    {
        public string GetParameterValue(string paraName)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysParameterDao paraDao = new SysParameterDao();
                return paraDao.GetParameterValue(paraName);
            }
        }

        public DataSet AllGetParameterValue(string paraName)
        {
            DataSet DS = new DataSet();
            using (PersistentManager persistenManager = new PersistentManager())
            {
                SysParameterDao SysParameterDao = new SysParameterDao();
                DS = SysParameterDao.AllGetParameterValue(paraName);
            }
            return DS;
        }

        public void SetUpdataData(string RemoteServerDB, string RemoteServerIP, string RemoteServerUserID, string RemoteServerPassword, string DatabaseType, string OuterBatch, string ChannelBlankCount, string TowerBlankCount)
        {

            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysParameterDao SysParameterDao = new SysParameterDao();
                SysParameterDao.UpdataData(RemoteServerDB, RemoteServerIP, RemoteServerUserID, RemoteServerPassword, DatabaseType, OuterBatch, ChannelBlankCount, TowerBlankCount);
            }
        }
    }
}
