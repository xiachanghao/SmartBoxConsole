using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("UserUsingLog")]
    public class UserUsingLog
    {
        [Column("ID", ColumnType.Identity)]
        public int ID { get; set; }

        [Column("SessionID")]
        public string SessionID { get; set; }

        [Column("UID")]
        public string UID { get; set; }

        [Column("DeviceID")]
        public string DeviceID { get; set; }

        [Column("Status")]
        public int Status { get; set; }

        [Column("BeginTime")]
        public DateTime BeginTime { get; set; }

        [Column("LastTime")]
        public DateTime LastTime { get; set; }
    }
}
