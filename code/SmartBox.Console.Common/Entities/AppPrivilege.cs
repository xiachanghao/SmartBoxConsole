using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// 应用的权限
    /// </summary>
    [Table("AppPrivilege")]
    public class AppPrivilege
    {
        #region Property
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID", ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("DisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 对应的Bua的应用系统标识
        /// </summary>
        [Column("BuaAppCode")]
        public string BuaAppCode { get; set; }

        /// <summary>
        /// 对应的Bua的权限标识
        /// </summary>
        [Column("BuaPrivilegeCode")]
        public string BuaPrivilegeCode { get; set; }

        /// <summary>
        /// 启用同步
        /// </summary>
        [Column("EnableSync")]
        public bool EnableSync { get; set; }

        /// <summary>
        /// 同步的间隔时间
        /// </summary>
        [Column("SyncIntervalTime")]
        public int SyncIntervalTime { get; set; }

        /// <summary>
        /// 上次同步时间
        /// </summary>
        [Column("SyncLastTime")]
        public DateTime SyncLastTime { get; set; }

        /// <summary>
        /// 同步开始时间
        /// </summary>
        [Column("SyncTime")]
        public DateTime SyncTime { get; set; }

        /// <summary>
        /// 创建者UID
        /// </summary>
        [Column("CreateUid")]
        public string CreateUid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者UID
        /// </summary>
        [Column("UpdateUid")]
        public string UpdateUid { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }
        #endregion
    }
}
