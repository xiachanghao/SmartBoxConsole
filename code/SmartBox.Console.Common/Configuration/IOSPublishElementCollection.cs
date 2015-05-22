using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SmartBox.Console.Common.Configuration
{
    public class IOSPublishElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new IOSPublishElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IOSPublishElement)element).ClientType;
        }

        public IOSPublishElement GetElementByKey(string key)
        {
            return (IOSPublishElement)base.BaseGet(key);
        }
    }
}
