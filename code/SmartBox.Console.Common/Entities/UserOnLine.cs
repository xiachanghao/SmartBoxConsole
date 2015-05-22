using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("UserOnline")]
    public class UserOnline
    {
        #region Property
        [Column("ID", ColumnType.PrimaryKey)]
        public int ID { get; set; }

        [Column("UID", ColumnType.PrimaryKey)]
        public string UID { get; set; }

        [Column("DeviceID")]
        public string DeviceID { get; set; }

        [Column("ClientType")]
        public string ClientType { get; set; }

        [Column("Status")]
        public int Status { get; set; }

        [Column("LastLoginTime")]
        public DateTime LastLoginTime { get; set; }

        [Column("LastLogoutTime")]
        public DateTime LastLogoutTime { get; set; }

        #endregion
    }
}
