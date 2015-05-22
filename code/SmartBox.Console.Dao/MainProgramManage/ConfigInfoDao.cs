using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities.Search;

namespace SmartBox.Console.Dao
{
    public class ConfigInfoDao:BaseDao<ConfigInfo>
    {
           /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">数据库链接字符串</param>
        public ConfigInfoDao(string key)
            : base(key)
        {

        }

        public void DeleteConfigInfo(SearchConfig search)
        {
            string sql = "delete from configInfo where 1=1 " + GetQueryCondition(search);
            //sql += " delete from configInfopc where 1=1" + GetQueryCondition(search);
            base.ExecuteNonQuery(sql);
        }

        private string GetQueryCondition(SearchConfig search)
        {
            StringBuilder strsql = new StringBuilder();
            if (!string.IsNullOrEmpty(search.PluginCode))
            {
                strsql.Append(" and PluginCode='" + search.PluginCode+"'");
            }
            if (!string.IsNullOrEmpty(search.key))
            {
                strsql.Append(" and [key] = '" + search.key + "'");
            }
            if (!string.IsNullOrEmpty(search.ConfigCategoryCode))
            {
                string[] arr = search.ConfigCategoryCode.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i == 0)
                        strsql.Append(" and ( ConfigCategoryCode='" + arr[i] + "'");
                    else
                        strsql.Append(" or ConfigCategoryCode='" + arr[i] + "'");
                }
                strsql.Append(" ) ");
            }
            return strsql.ToString();
        }

        public IList<ConfigInfo> GetConfigList(SearchConfig search)
        {
            string sql = "select * from configInfo where 1=1 " + GetQueryCondition(search);
            string union = " union ";
            string sqlp = "select * from configInfoPC where 1=1" + GetQueryCondition(search);

            return Query(sql + union + sqlp);
        }
        
        public IList<ConfigInfo> GetMobileConfigList(SearchConfig search)
        {
            string sql = "select * from configInfo where 1=1 " + GetQueryCondition(search);
            //string union = " union ";
            //string sqlp = "select * from configInfoPC where 1=1" + GetQueryCondition(search);

            return Query(sql);
        }
        
        public IList<ConfigInfo> GetPCConfigList(SearchConfig search)
        {
            string sql = "select * from configInfoPC where 1=1 " + GetQueryCondition(search);
            //string union = " union ";
            //string sqlp = "select * from configInfoPC where 1=1" + GetQueryCondition(search);

            return Query(sql);
        }

        public JsonFlexiGridData QueryConfigInfo(PageView view, SearchConfig search)
        {
            string Selectcolumns = "[Key],Value,Summary";
            string sqlorder;
            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  ConfigId asc ";
            else
                sqlorder = view.OrderBy.ToString();
            string parm = GetQueryCondition(search);

            return base.QueryDataForFlexGridByPager(Selectcolumns, "ConfigInfo", sqlorder, "ConfigId", parm, view);
        }
    }
}
