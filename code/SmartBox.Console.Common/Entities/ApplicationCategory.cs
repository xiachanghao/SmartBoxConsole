using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// 应用的分类，一个应用可以有多个分类
    /// </summary>
    [Table("ApplicationCategory")]
    public class ApplicationCategory
    {
        [Column("ID", ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }

        /// <summary>
        /// 分类标识
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 分类显示名称
        /// </summary>
        [Column("DisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Column("Seq")]
        public int Seq { get; set; }

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
