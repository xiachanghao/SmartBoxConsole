using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;

namespace SmartBox.Console.Common.Configuration
{
    public class IOSPublishElement : ConfigurationElement
    {
        [ConfigurationProperty("clientType", IsRequired = true, IsKey = true)]
        public string ClientType
        {
            get { return (string)this["clientType"]; }
            set { this["clientType"] = value; }
        }

        [ConfigurationProperty("path", IsRequired = true, IsKey = false)]
        public string Path
        {
            get { return (string)this["path"]; }
            set { this["path"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true, IsKey = false)]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

    }
}
