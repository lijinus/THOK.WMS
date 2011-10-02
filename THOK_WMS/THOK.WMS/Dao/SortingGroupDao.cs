using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
namespace THOK.WMS.Dao
{
    public class SortingGroupDao : BaseDao
    {
        
        /// <summary>
        /// 根据分页查询数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        public void FindSortingGroupInfo(string code,string desc)
        {
            string sql = string.Format("SELECT * FROM DWV_DPS_SORTING_GROUP {0}{1}", code, desc);
        }



        /// <summary>
        /// 查询分拣厂家记录
        /// </summary>
        /// <param name="filte"></param>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public int FindSortingGroupCount(string filte, string strTableName)
        {
            string sql = string.Format("SELECT COUNT(*) FROM {0} WHERE {1}", strTableName, filte);
            return (int)ExecuteScalar(sql);
        }

        /// <summary>
        /// 分页查询数据，指定数据集表名TableName
        /// </summary>
        /// <param name="TableViewName">表名或视图名</param>
        /// <param name="PrimaryKey">表主键字段名称</param>
        /// <param name="QueryFields">查询字段字符串，字段名称逗号隔开</param>
        /// <param name="pageIndex">查询页码</param>
        /// <param name="pageSize">页码大小</param>
        /// <param name="orderBy">排序字段和方式</param>
        /// <param name="filter">查询条件</param>
        /// <param name="strTableName">返回数据集填充的表名</param>
        /// <returns>返回DataSet</returns>
        public DataSet FindExecuteQuery(string TableViewName, string PrimaryKey, string QueryFields, int pageIndex, int pageSize, string orderBy, string filter, string strTableName)
        {
            int preRec = (pageIndex - 1) * pageSize;
            string sql = string.Format("select top {4} {2} ,case when ISACTIVE=1 then '使用' else '停用' end as SORTINGSTATE from {0} " +
                                        " where {1} not in ( select top {3} {1} from {0} where {6} order by {5}) " +
                                        " and {6} order by {5}"
                                        , TableViewName, PrimaryKey, QueryFields, preRec.ToString(), pageSize.ToString(), orderBy, filter);

            return ExecuteQuery(sql).Tables[0].DataSet;
        }


        /// <summary>
        /// 查询分拣线编号
        /// </summary>
        /// <returns></returns>
        public string FindSortingGroupId()
        {
            string sql = "SELECT TOP 1 SORTING_CODE FROM DWV_DPS_SORTING ORDER BY SORTINGID DESC";
            int sn = Convert.ToInt32( ExecuteScalar(sql));
            
            sn++;
            string id=""+sn;
            //if (sn < 10 )
            //    id = "0" + sn;
            return id;
        }

        public void delete(string dataSet)
        {
            string sql = string.Format("delete DWV_DPS_SORTING where SORTING_CODE ={0}", dataSet);
            this.ExecuteNonQuery(sql);
         }

        public void SetData(string sql)
        {
            ExecuteNonQuery(sql);
        }
    }
}
    