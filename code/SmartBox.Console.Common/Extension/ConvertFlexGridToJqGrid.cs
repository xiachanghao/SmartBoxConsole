using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SmartBox.Console.Common
{
    public static class ConvertJsonGrid
    {
        public static JsonJqGridData ConvertFlexGridToJqGrid(JsonFlexiGridData data,PageView view,string columns)
        {
            JsonJqGridData returndata = new JsonJqGridData {page=view.PageIndex+1,records=view.RecordCount };
            returndata.total = view.RecordCount / view.PageSize + 1;
            returndata.rows = new List<JqGridRow>();
            if (data.total>0 && data.rows != null && data.rows.Count > 0)
            {
                string[] Column = columns.Split(',');
                int length = Column.Length;
                foreach (FlexiGridRow row in data.rows)
                {
                    JqGridRow r = new JqGridRow();
                    r.id = row.id;
                    r.cell = new List<object>();
                    var o = new { id="",value="" };
                    for (int i = 0; i < length; i++)
                    {
                        //r.cell.Add(Column[i]);
                        //r.cell.Add(":");
                        r.cell.Add(row.cell[i]);
                    }
                    returndata.rows.Add(r);
                }
            }

            return returndata;
        }
    }
}
