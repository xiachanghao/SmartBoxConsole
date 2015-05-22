using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("DeviceUser")]
    public class DeviceUser
    {
        [Column("ID", ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }

        [Column("DeviceID")]
        public string DeviceID { get; set; }

        [Column("UID")]
        public string UID { get; set; }

        /// <summary>
        /// 0:默认，1：可以使用，2：不可使用
        /// </summary>
        [Column("Status")]
        public int Status { get; set; }

        [Column("NoUseReason")]
        public int NoUseReason { get; set; }

        [Column("LastUpdateUID")]
        public string LastUpdateUID { get; set; }

        [Column("LastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }
    }
}
