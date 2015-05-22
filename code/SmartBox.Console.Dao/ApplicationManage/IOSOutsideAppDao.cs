using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Dao
{
    public class IOSOutsideAppDao : BaseDao<IOSOutsideApp>
    {
        public IOSOutsideAppDao(string key)
            : base(key)
        {

        }
               

        public JsonFlexiGridData QueryIOSOutsideAppList(PageView view)
        {
            string Selectcolumns = @"ID,AppID,Uri,Scheme,ClientType,Seq,IconUri,CreateUid,CreateTime,UpdateUid,UpdateTime";
            string SelectTable = "IOSOutsideApplication";
            string sqlorder;
            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by ID asc ";
            else
                sqlorder = view.OrderBy.ToString();
            return base.QueryDataForFlexGridByPager(Selectcolumns, SelectTable.ToString(), sqlorder, "ID", "", view);
        }



    }
}
