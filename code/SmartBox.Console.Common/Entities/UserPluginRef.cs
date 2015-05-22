using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Biz.Entry;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("UserPluginRef", SorMappingType.ByAttributes)]
    public class UserPluginRef : BaseEntry
    {
        [Column("UserUId", ColumnType.PrimaryKey)]
        public string UserUId
        {
            set;
            get;
        }

        [Column("PluginCode", ColumnType.PrimaryKey)]
        public string PluginCode
        {
            set;
            get;
        }

        [Column("Sequence")]
        public int Sequence
        { get; set; }
    }
}
