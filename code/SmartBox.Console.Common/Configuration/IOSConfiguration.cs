using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SmartBox.Console.Common.Configuration
{
    public class IOSConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("Publish", IsDefaultCollection = true)]
        public IOSPublishElementCollection IOSPublishs
        {
            get { return (IOSPublishElementCollection)base["Publish"]; }
            set { base["Publish"] = value; }
        }
    }
}
