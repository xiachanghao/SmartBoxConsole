using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    public class ActionExtend
    {

        /// <summary>
        /// 
        /// </summary>
        [Column("PluginCode", ColumnType.PrimaryKey)]
        public string PluginCode
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("ActionCode")]
        public string ActionCode
        {
            set;
            get;
        }

        [Column("Summary")]
        public string Summary
        {
            set;
            get;
        }
    }
}
