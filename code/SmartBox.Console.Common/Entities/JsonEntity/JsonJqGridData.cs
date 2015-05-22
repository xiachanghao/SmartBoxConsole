using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common
{
    public class JsonJqGridData
    {
        public JsonJqGridData()
        {
        }

        public JsonJqGridData(int pageIndex,int totalPages,int totalrecords,IList<JqGridRow> data)
        {
            page = pageIndex;
            total = totalPages;
            records = totalrecords;
            rows = data;
        }

        public int page { get; set; }
        public int total { get; set; }
        public int records { get; set; }
        public IList<JqGridRow> rows { get; set; }
                
    }

    public class JqGridRow
    {
        public string id { get; set; }
        public List<object> cell { get; set; }
    }
}
