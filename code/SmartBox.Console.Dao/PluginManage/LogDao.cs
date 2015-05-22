using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao
{
    public class LogDao : BaseDao<LogInfo>
    {
        public LogDao(string key)
            : base(key)
        {

        }

        public JsonFlexiGridData GetLogInfoList(PageView view)
        {
            string Selectcolumns = " Time,Type,UserUid,UserName,Ip,LogId ";
            string sqlorder;
            string table = @" (select t1.*,t2.UserName from LogInfo as t1 left join V_User_Info t2 on t1.UserUid = t2.UserUid) as temp ";
            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  Time asc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(Selectcolumns, table, sqlorder, "LogId", "", view);
        }
    }
}
