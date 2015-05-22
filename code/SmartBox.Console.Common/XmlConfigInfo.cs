using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Common
{
    public class XmlConfigInfo
    {

        public string savefile
        { get; set; }
        public string oldfile { get; set; }

        public string oldCode { get; set; }


        public IList<PluginInfoTemp> PluginInfo
        { get; set; }
    }


    public class XmlMainConfigInfo
    {

        public string savefile
        { get; set; }
        public string oldfile { get; set; }

        public string oldCode { get; set; }

        public IList<ConfigInfoPC> configList
        {
            get;
            set;
        }
    }
}
