using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao
{
    public class ApplicationCategoryDao : BaseDao<ApplicationCategory>
    {
        public ApplicationCategoryDao(string key)
            : base(key)
        { }

        public JsonFlexiGridData QueryApplicationCategoryList(PageView view)
        {
            string column = "*";
            string sql = "select * from ApplicationCategory ";
            return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as temp", sql), view.OrderBy.ToString(), "seq", string.Empty, view);
        }

        public IList<ApplicationCategory> QueryApplicationCategoryList()
        {
            string sql = "select * from ApplicationCategory ";
            return base.Query(sql);
        }
    }
}
