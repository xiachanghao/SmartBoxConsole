using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("Image")]
    public class Image
    {
        [Column("ID",ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }

        [Column("HashCode")]
        public string HashCode { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Column("Data")]
        public byte[] Data { get; set; }
    }
}
