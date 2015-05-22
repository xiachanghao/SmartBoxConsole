using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("SystemConfig")]
    public class SystemConfig
    {
        [Column("Key", ColumnType.PrimaryKey)]
        public string Key { get; set; }

        [Column("Value")]
        public string Value { get; set; }
    }
}
