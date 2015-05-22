using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("DeviceBind")]
    public class DeviceBind
    {
        [Column("Id",ColumnType.PrimaryKey)]
        public Guid Id { get; set; }

        /// <summary>
        /// 用户登录ID
        /// </summary>
        [Column("UserUid")]
        public string UserUid { get; set; }

        /// <summary>
        /// 绑定设备
        /// </summary>
        [Column("Device")]
        public string Device { get; set; }

        /// <summary>
        /// 设备标识
        /// </summary>
        [Column("DeviceId")]
        public string DeviceId { get; set; }

        /// <summary>
        /// 状态 
        /// ENABLE 启用
        /// DISABLE 禁用
        /// LOST 遗失
        /// </summary>
        [Column("Status")]
        public string Status { get; set; }

        /// <summary>
        /// 设备描述
        /// </summary>
        [Column("Description")]
        public string Description { get; set; }
    }
}
