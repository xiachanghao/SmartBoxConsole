using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao
{
    public class ClientTypeDao : BaseDao<ClientTypes>
    {
        public ClientTypeDao(string key)
            : base(key)
        { }

        public IList<ClientTypes> QueryClientTypeList()
        {
            string sql = "select * from ClientType";
            return base.Query(sql);
        }

        public JsonFlexiGridData QueryClientTypeList(PageView view)
        {
            string column = "*";
            string sql = "select * from ClientType";
            return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as temp", sql), view.OrderBy.ToString(), "ClientType",string.Empty,view);
        }
    }
}
