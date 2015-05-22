using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common.Entities.Search;

namespace SmartBox.Console.Dao
{
    public class PluginInfoTempDao:BaseDao<PluginInfoTemp>
    {
                /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">数据库链接字符串</param>
        public PluginInfoTempDao(string key)
            : base(key)
        {

        }


        public void DeleteInfo(SearchConfig search)
        {
            string sql = "delete from PluginInfoTemp where 1=1 " + GetQueryCondition(search);

            base.ExecuteNonQuery(sql);
        }

        public IList<PluginInfoTemp> GetTempList(SearchConfig search)
        {
            string sql = "select * from PluginInfoTemp where 1=1 " + GetQueryCondition(search);

            return Query(sql);
        }

        private string GetQueryCondition(SearchConfig search)
        {
            StringBuilder strsql = new StringBuilder();
            if (!string.IsNullOrEmpty(search.PluginCode))
            {
                strsql.Append(" and PluginCode='" + search.PluginCode+"'");
            }
            if (!string.IsNullOrEmpty(search.code))
            {
                strsql.Append(" and ActionCode='" + search.code + "'");
            }
            return strsql.ToString();
        }
    }
}
