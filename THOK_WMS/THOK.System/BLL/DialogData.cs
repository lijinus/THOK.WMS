using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.System.Dao;
using THOK.Util;

namespace THOK.System.BLL
{
    public class DialogData
    {
        public DataSet GetData(string procName, StoredProcParameter param)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSelectDialogDao dao = new SysSelectDialogDao();
                return dao.ExecuteProcedure(procName, param);
            }
        }

        public int GetRowCount(string TableView, string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSelectDialogDao dao = new SysSelectDialogDao();
                return dao.GetRowCount(TableView, filter);
            }
        }
    }
}
