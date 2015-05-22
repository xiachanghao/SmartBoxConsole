using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao
{
    public class SystemConfigDao : BaseDao<SystemConfig>
    {
        public SystemConfigDao(string key)
            : base(key)
        { }

        public SelectPagnationEx<SystemConfig> GetServiceConfigList(string key, string orderby, int pageSize, int pageIndex)
        {
            string where = "";


            if (!String.IsNullOrEmpty(key))
            {
                where += " and [key] like '%" + key + "%'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);


            SelectPagnationEx<SystemConfig> ex = this.SelectPaginationExT("SystemConfig", "*", pageIndex + 1, pageSize, orderby, where, "");

            //Hashtable pars = new Hashtable();
            //SplitPageResult<SMC_User> re = this.QuerySplitPage<SMC_User>(sql, "*", orderby, pageSize, pageIndex, pars);

            return ex;
        }
    }
}
