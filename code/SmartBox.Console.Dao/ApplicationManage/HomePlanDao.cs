using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao
{
    public class HomePlanDao:BaseDao<HomePlan>
    {
        public HomePlanDao(string key)
            : base(key)
        { }

        public JsonFlexiGridData QueryHomePlanList(PageView view)
        {
            string column = "*";
            string sql = "select * from HomePlan where Owner is null or Owner=''";
            return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as Temp", sql), view.OrderBy.ToString(), "ID", string.Empty, view);
        }

        public List<HomePlan> QueryDefaultHomePlan(string id, string owner, string format)
        {
            string sql = string.Format("select * from dbo.HomePlan where IsDefault=1 and ID<>'{0}' and (Owner='{1}' or Owner is null ) and Format='{2}' ", id, owner, format);
            return base.Query(sql) as List<HomePlan>;
        }
    }
}
