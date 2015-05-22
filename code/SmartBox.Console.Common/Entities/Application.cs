using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// 应用
    /// </summary>
    [Table("Application")]
    public class Application
    {
        #region Property
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID", ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }

        /// <summary>
        /// 应用名称(应用标识)
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }
        
        [Column("Seq")]
        public int Seq { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("DisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 应用说明
        /// </summary>
        [Column("Description")]
        public string Description { get; set; }

        /// <summary>
        /// 应用是否启用
        /// </summary>
        [Column("Enable")]
        public bool Enable { get; set; }
        /// <summary>
        /// 启用类型
        /// </summary>
        [Column("EnableType")]
        public string EnableType { get; set; }

        /// <summary>
        /// Application的启用模式为权限绑定的方式，权限的ID
        /// </summary>
        [Column("PrivilegeID")]
        public int? PrivilegeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("CategoryIDs")]
        public string CategoryIDs { get; set; }

        /// <summary>
        /// 创建用户账号
        /// </summary>
        [Column("CreateUid")]
        public string CreateUid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新用户账号
        /// </summary>
        [Column("UpdateUid")]
        public string UpdateUid { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 所属单位
        /// </summary>
        [Column("Unit")]
        public string Unit { get; set; } 

        #endregion
    }
}
