using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common
{
    public class JsonFlexiGridData
    {
        public JsonFlexiGridData()
        {
        }

        public JsonFlexiGridData(
            int pageIndex, int totalCount, IList<FlexiGridRow> data)
        {
            page = pageIndex;
            total = totalCount;
            rows = data;
        }
        public int page { get; set; }
        public int total { get; set; }
        public IList<FlexiGridRow> rows { get; set; }
    }
    public class FlexiGridRow
    {
        public string id { get; set; }
        public List<string> cell { get; set; }
    }
}
