using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao
{
    public class IMGroupDao : BaseDao<IMGroupInfo>
    {
        public IMGroupDao(string key)
            : base(key)
        {

        }

        public JsonFlexiGridData QueryGroupList(PageView view)
        {
            string Selectcolumns = "PublishId, GroupDisplayName, UserName, Description, CreateTime";
            string sqlorder;
            string table = "IMGroup as i inner join V_USER_INFO as u on i.Owner = u.UserUId";
            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  CreateTime asc ";
            else
                sqlorder = view.OrderBy.ToString();
            JsonFlexiGridData data = base.QueryDataForFlexGridByPager(Selectcolumns, table, sqlorder, "PublishId", "", view);

            //foreach (var row in data.rows)
            //{
            //    row.cell[4] = DateTime.Parse(row.cell[4]).ToLocalTime().ToString();
            //}

            return data;
        }
    }
}
