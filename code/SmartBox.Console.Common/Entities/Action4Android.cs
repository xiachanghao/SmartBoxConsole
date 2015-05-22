using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// Android打Action设置
    /// </summary>
    [Table("Action4Android")]
    [Serializable]
    public class Action4Android
    {
        /// <summary>
        /// Action名，用于Action的调用
        /// </summary>
        [Column("Name",ColumnType.PrimaryKey)]
        public string Name { get; set; }

        /// <summary>
        /// 应用的ID
        /// </summary>
        [Column("App4AIID")]
        public int App4AIID { get; set; }

        [Column("ShortName")]
        public string ShortName { get; set; }

        /// <summary>
        /// 是否为启动Action
        /// </summary>
        [Column("IsLaunch")]
        public bool IsLaunch { get; set; }

        /// <summary>
        /// action的图标地址
        /// </summary>
        [Column("IconUri")]
        public string IconUri { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Column("Seq")]
        public int Seq { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [Column("CreateUid")]
        public string CreateUid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新用户
        /// </summary>
        [Column("UpdateUid")]
        public string UpdateUid { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("DisplayName")]
        public string DisplayName { get; set; }
    }
}
