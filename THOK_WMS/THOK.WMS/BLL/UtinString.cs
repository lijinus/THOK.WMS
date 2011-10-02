using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace THOK.WMS
{
   public class UtinString
    {

        /// <summary>
        /// 处理字符串，截取字符，传来的DataTable和字段
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static string StringMake(DataTable dt, string field)
        {
            string list = "";
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list += row["" + field + ""].ToString() + ",";
                }
                list = list.Substring(0, list.Length - 1);
            }
            return list;
        }

        /// <summary>
        /// 处理字符串,截取字符，传来的DataRow和字段
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static string StringMake(DataRow[] dr, string field)
        {
            string list = "";
            if (dr.Length != 0)
            {
                foreach (DataRow row in dr)
                {
                    list += row["" + field + ""].ToString() + ",";
                }
                list = list.Substring(0, list.Length - 1);
            }
            return list;
        }

        /// <summary>
        /// 处理字符串，取得字符，传来的String
        /// </summary>
        /// <param name="stringList">字符串</param>
        /// <returns></returns>
        public static string StringMake(string stringList)
        {
            string list = "''";
            string[] arraryList = stringList.Split(',');
            //if(stringList.Equals(""))
            for (int i = 0; i < arraryList.Length; i++)
            {
                list += ",'" + arraryList[i] + "'";
            }
            return list;
        }

    }
}
