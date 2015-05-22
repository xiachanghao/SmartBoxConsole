using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("DeviceConfig")]
    public class DeviceConfig
    {
        [Column("Key", ColumnType.PrimaryKey)]
        public string Key { get; set; }

        [Column("ValueType")]
        public int ValueType { get; set; }

        [Column("Value")]
        public string Value { get; set; }

        [Column("XmlValue")]
        public string XmlValue { get; set; }
    }
}
