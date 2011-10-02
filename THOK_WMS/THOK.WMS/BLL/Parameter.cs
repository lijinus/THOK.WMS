using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.WMS.Dao;
using THOK.Util;

namespace THOK.WMS.BLL
{
    public class Parameter
    {
        public DataSet GetParameterInfo()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ParameterDao dao = new ParameterDao();
                return dao.GetData("select * from WMS_PARAMETER");
            }
        }

        public bool Insert()
        {
            bool flag = false;
            using (PersistentManager persistentManager = new PersistentManager())
            {
                ParameterDao dao = new ParameterDao();

                string sql = string.Format("Insert into WMS_PARAMETER (DBTYPE_1,SERVERNAME_1,DBNAME_1,USERID_1,PWD_1,DBTYPE_2,SERVERNAME_2,DBNAME_2,USERID_2,PWD_2,DBTYPE_3,SERVERNAME_3,DBNAME_3,USERID_3,PWD_3,CELL_IMG_X,CELL_IMG_Y,SPACE_Z) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')"
                                             ,  this.DBTYPE_1,
                            this.SERVERNAME_1,
                            this.DBNAME_1,
                            this.USERID_1,
                            this.PWD_1,
                            this.DBTYPE_2,
                            this.SERVERNAME_2,
                            this.DBNAME_2,
                            this.USERID_2,
                            this.PWD_2,
                            this.DBTYPE_3,
                            this.SERVERNAME_3,
                            this.DBNAME_3,
                            this.USERID_3,
                            this.PWD_3,
                            this.CELL_IMG_X,
                            this.CELL_IMG_Y,
                            this.SPACE_Z);

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
                ParameterDao dao = new ParameterDao();

                string sql = string.Format("update WMS_PARAMETER set DBTYPE_1='{1}',SERVERNAME_1='{2}',DBNAME_1='{3}',USERID_1='{4}',PWD_1='{5}',DBTYPE_2='{6}',SERVERNAME_2='{7}',DBNAME_2='{8}',USERID_2='{9}',PWD_2='{10}',DBTYPE_3='{11}',SERVERNAME_3='{12}',DBNAME_3='{13}',USERID_3='{14}',PWD_3='{15}',CELL_IMG_X='{16}',CELL_IMG_Y='{17}',SPACE_Z='{18}'  where ID='{0}'"
                                             , this.ID,
                            this.DBTYPE_1,
                            this.SERVERNAME_1,
                            this.DBNAME_1,
                            this.USERID_1,
                            this.PWD_1,
                            this.DBTYPE_2,
                            this.SERVERNAME_2,
                            this.DBNAME_2,
                            this.USERID_2,
                            this.PWD_2,
                            this.DBTYPE_3,
                            this.SERVERNAME_3,
                            this.DBNAME_3,
                            this.USERID_3,
                            this.PWD_3,
                            this.CELL_IMG_X,
                            this.CELL_IMG_Y,
                            this.SPACE_Z);

                dao.SetData(sql);
                flag = true;
            }
            return flag;
        }


        #region property
        private int _id;
        private string _dbtype_1;
        private string _servername_1;
        private string _dbname_1;
        private string _userid_1;
        private string _pwd_1;
        private string _dbtype_2;
        private string _servername_2;
        private string _dbname_2;
        private string _userid_2;
        private string _pwd_2;
        private string _dbtype_3;
        private string _servername_3;
        private string _dbname_3;
        private string _userid_3;
        private string _pwd_3;
        private double _cell_img_x;
        private double _cell_img_y;
        private double _space_z;


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

        public string DBTYPE_1
        {
            get
            {
                return _dbtype_1;
            }
            set
            {
                _dbtype_1 = value;
            }
        }

        public string SERVERNAME_1
        {
            get
            {
                return _servername_1;
            }
            set
            {
                _servername_1 = value;
            }
        }

        public string DBNAME_1
        {
            get
            {
                return _dbname_1;
            }
            set
            {
                _dbname_1 = value;
            }
        }

        public string USERID_1
        {
            get
            {
                return _userid_1;
            }
            set
            {
                _userid_1 = value;
            }
        }

        public string PWD_1
        {
            get
            {
                return _pwd_1;
            }
            set
            {
                _pwd_1 = value;
            }
        }

        public string DBTYPE_2
        {
            get
            {
                return _dbtype_2;
            }
            set
            {
                _dbtype_2 = value;
            }
        }

        public string SERVERNAME_2
        {
            get
            {
                return _servername_2;
            }
            set
            {
                _servername_2 = value;
            }
        }

        public string DBNAME_2
        {
            get
            {
                return _dbname_2;
            }
            set
            {
                _dbname_2 = value;
            }
        }

        public string USERID_2
        {
            get
            {
                return _userid_2;
            }
            set
            {
                _userid_2 = value;
            }
        }

        public string PWD_2
        {
            get
            {
                return _pwd_2;
            }
            set
            {
                _pwd_2 = value;
            }
        }

        public string DBTYPE_3
        {
            get
            {
                return _dbtype_3;
            }
            set
            {
                _dbtype_3 = value;
            }
        }

        public string SERVERNAME_3
        {
            get
            {
                return _servername_3;
            }
            set
            {
                _servername_3 = value;
            }
        }

        public string DBNAME_3
        {
            get
            {
                return _dbname_3;
            }
            set
            {
                _dbname_3 = value;
            }
        }

        public string USERID_3
        {
            get
            {
                return _userid_3;
            }
            set
            {
                _userid_3 = value;
            }
        }

        public string PWD_3
        {
            get
            {
                return _pwd_3;
            }
            set
            {
                _pwd_3 = value;
            }
        }

        public double CELL_IMG_X
        {
            get
            {
                return _cell_img_x;
            }
            set
            {
                _cell_img_x = value;
            }
        }

        public double CELL_IMG_Y
        {
            get
            {
                return _cell_img_y;
            }
            set
            {
                _cell_img_y = value;
            }
        }

        public double SPACE_Z
        {
            get
            {
                return _space_z;
            }
            set
            {
                _space_z = value;
            }
        }
        #endregion
    }
}
