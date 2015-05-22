using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("HomePlan")]
    public class HomePlan
    {
        [Column("ID",ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }

        /// <summary>
        /// 布局标识
        /// </summary>
        [Column("Code")]
        public string Code { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("DisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 所有者
        /// </summary>
        [Column("Owner")]
        public string Owner { get; set; }

        /// <summary>
        /// 默认布局
        /// </summary>
        [Column("IsDefault")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 布局格式(宽,高)
        /// </summary>
        [Column("Format")]
        public string Format { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [Column("CreateUid")]
        public string CreateUid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [Column("UpdateUid")]
        public string UpdateUid { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }


    }
}
