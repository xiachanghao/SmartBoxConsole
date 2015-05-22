using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Style")]
    public class Style
    {
        [Column("ID", ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }
        /// <summary>
        /// Android和iOS的发布包信息ID
        /// </summary>
        [Column("Code")]
        public string Code { get; set; }
        /// <summary>
        /// 应用系统ID
        /// </summary>
        [Column("DipalsyName")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 接入端类型（用于区分硬件设备）
        /// </summary>
        [Column("ClientType")]
        public string ClientType { get; set; }
    }
}
