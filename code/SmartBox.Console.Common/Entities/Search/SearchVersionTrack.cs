using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common.Entities.Search
{
    public class SearchVersionTrack
    {
           /// <summary>
        /// 是否精确查询
        /// </summary>
        public bool IsPrecise
        {
            get;
            set;
        }
        public string VID
        { get; set; }

        public string PluginCode
        { get; set; }
        public string VersionName
        { get; set; }
        public string PreVersionId
        { get; set; }

        public string NotPluginId
        { get; set; }

        public string VersionStatus
        {
            get;
            set;
        }

        public string filepath
        { get; set; }


        public string UserUid
        { get; set; }

        public string UserName
        { get; set; }

        public string NotPluginIdForCategory
        { get; set; }

        public string InPluginIdForCategory
        { get; set; }
     
    }
}
