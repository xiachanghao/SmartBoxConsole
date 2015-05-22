using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using System.Collections;
using SmartBox.Console.Common.Entities.Search;

namespace SmartBox.Console.Dao
{
    public class ActionExtendDao : BaseDao<ActionExtend>
    {
        public ActionExtendDao(string key)
            : base(key)
        {

        }

        public IList<ActionExtend> QueryActionExtendInfo(SearchConfig search)
        {
            var dic = new Dictionary<string, string>();
            string sql = "select * from ActionExtend where 1=1 " + GetQueryCondition(search, dic);

            return Query(sql, dic);
        }

        public void DelActionExtendInfo(SearchConfig search)
        {
            var dic = new Dictionary<string, string>();
            string sql = "delete from ActionExtend where 1=1 " + GetQueryCondition(search, dic);

            base.ExecuteNonQuery(sql, dic);
        }

        private string GetQueryCondition(SearchConfig search, IDictionary dic)
        {
            StringBuilder strsql = new StringBuilder();
            if (!string.IsNullOrEmpty(search.PluginCode))
            {
                strsql.Append(" and PluginCode=@PluginCode");
                dic.Add("PluginCode", search.PluginCode);
            }
            if (!string.IsNullOrEmpty(search.code))
            {
                strsql.Append(" and ActionCode=@ActionCode");
                dic.Add("ActionCode", search.code);
            }
            return strsql.ToString();
        }

    }
}
