using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common.Entities.Search
{
    public class SearchApp4AI : PageView
    {
        public SearchApp4AI()
            : base()
        { }

        public SearchApp4AI(System.Collections.Specialized.NameValueCollection nvc)
            : base(nvc)
        {
            ID = nvc["ID"];
            AppID = nvc["AppID"];
            AppCode = nvc["AppCode"];
            ClientType = nvc["ClientType"];
        }

        public string ID { get; set; }
        public string AppID { get; set; }
        public string AppCode { get; set; }
        public string ClientType { get; set; }
    }
}
