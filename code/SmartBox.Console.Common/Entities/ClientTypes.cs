using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// 客户端类型
    /// </summary>
    [Table("ClientType")]
    public class ClientTypes
    {
        /// <summary>
        /// 客户端类型
        /// </summary>
        [Column("ClientType",ColumnType.PrimaryKey)]
        public string ClientType { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("DisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("Description")]
        public string Description { get; set; }
    }
}
