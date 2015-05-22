using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common
{
    public class StoredProcedureHelper
    {
        public static string GetTableName(string sql) {
            int fromIndex = sql.IndexOf("from");
            int whereIndex = sql.IndexOf("where");
            string tableName = sql.Substring(fromIndex + 4, whereIndex - fromIndex - 4);
            return tableName;
        }

        public static string GetColumns(string sql)
        {
            int fromIndex = sql.IndexOf("from");
            int selectIndex = sql.IndexOf("select");
            string columns = sql.Substring(selectIndex + 6, fromIndex - selectIndex - 6);
            return columns;
        }

        public static string GetWhere(string sql)
        {
            int whereIndex = sql.IndexOf("where");
            string where = sql.Substring(whereIndex + 5);
            return where;
        }
    }
}
