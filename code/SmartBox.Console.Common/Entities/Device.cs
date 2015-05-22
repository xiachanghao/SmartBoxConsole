using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("Device")]
    public class Device
    {
        [Column("ID", ColumnType.PrimaryKey)]
        public string ID { get; set; }

        /// <summary>
        /// 0:默认，1：锁定，2：挂失
        /// </summary>
        [Column("Status")]
        public int Status { get; set; }
        
        [Column("Resource")]
        public string Resource { get; set; }

        [Column("OS")]
        public string OS { get; set; }

        [Column("Model")]
        public string Model { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Remark")]
        public string Remark { get; set; }
        
        [Column("LockTime")]
        public DateTime LockTime { get; set; }

        /// <summary>
        /// 设备锁定过期小时数，单位小时
        /// </summary>
        [Column("LockExpireHours")]
        public int LockExpireHours { get; set; }
        
        [Column("UnLockTime")]
        public DateTime UnLockTime { get; set; }
        
        [Column("LostTime")]
        public DateTime LostTime { get; set; }
        
        [Column("UnLostTime")]
        public DateTime UnLostTime { get; set; }
    }
}
