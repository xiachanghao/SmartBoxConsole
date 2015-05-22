using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common.Entities.Search
{
    public class SearchStatisticOnlineTime:PageView
    {
        public SearchStatisticOnlineTime()
            : base()
        { }

        public SearchStatisticOnlineTime(System.Collections.Specialized.NameValueCollection nvc)
            : base(nvc)
        {
            if (!string.IsNullOrEmpty(nvc["StartTime"]) && nvc["StartTime"] != "undefined")
            {
                this.StartTime = nvc["StartTime"];
            }
            if (!string.IsNullOrEmpty(nvc["EndTime"]) && nvc["EndTime"] != "undefined")
            {
                this.EndTime = nvc["EndTime"];
            } if (!string.IsNullOrEmpty(nvc["UID"]) && nvc["UID"] != "undefined")
            {
                this.UID = nvc["UID"];
            }
        }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string UID { get; set; }
    }
}
