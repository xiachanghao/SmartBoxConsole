using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common.Entities.Search;

namespace SmartBox.Console.Dao
{
    public class ConfigTempDao:BaseDao<ConfigTemp>
    {
                /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">数据库链接字符串</param>
        public ConfigTempDao(string key)
            : base(key)
        {

        }

        public void DeleteInfo(SearchConfig search)
        {
            string sql = "delete from ConfigTemp where 1=1 " + GetQueryCondition(search);

            base.ExecuteNonQuery(sql);
        }


        private string GetQueryCondition(SearchConfig search)
        {
            StringBuilder strsql = new StringBuilder();
            if (!string.IsNullOrEmpty(search.PluginCode))
            {
                strsql.Append(" and PluginCode='" + search.PluginCode + "'");
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

        public IList<ConfigTemp> GetConfigList(SearchConfig search)
        {
            string sql = "select * from ConfigTemp where 1=1 " + GetQueryCondition(search);
            return Query(sql);
        }
    }
}
