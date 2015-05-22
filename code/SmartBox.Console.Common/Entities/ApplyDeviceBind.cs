using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("ApplyDeviceBind")]
    public class ApplyDeviceBind
    {
        /// <summary>
        /// 标识
        /// </summary>
        [Column("Id",ColumnType.IdentityAndPrimaryKey)]
        public int Id { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        [Column("UserUid")]
        public string UserUid { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        [Column("DeviceId")]
        public string DeviceId { get; set; }

        /// <summary>
        /// 申请IP地址
        /// </summary>
        [Column("Ip")]
        public string Ip { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [Column("Status")]
        public ApplyDeviceBindStatus Status { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        [Column("ApplyTime")]
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [Column("CheckTime")]
        public DateTime CheckTime { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [Column("CheckUser")]
        public string CheckUser { get; set; }

        /// <summary>
        /// 设备描述
        /// </summary>
        [Column("Description")]
        public string Description { get; set; }
    }
}
