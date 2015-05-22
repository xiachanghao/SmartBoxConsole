using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common.Entities.Search
{
    public class SearchWebApplication:PageView
    {
        public SearchWebApplication()
            : base()
        { }

        public SearchWebApplication(System.Collections.Specialized.NameValueCollection nvc)
            : base(nvc)
        { 

        }

        public string ID { get; set; }
        public string AppID { get; set; }
        public string ShortName { get; set; }
    }
}
