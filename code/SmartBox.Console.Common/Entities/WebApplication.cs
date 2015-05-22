using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// Web形式的应用
    /// </summary>
    [Table("WebApplication")]
    public class WebApplication
    {
        [Column("ID",ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        [Column("AppID")]
        public int AppID { get; set; }
        /// <summary>
        /// 应用ID的地址
        /// </summary>
        [Column("Uri")]
        public string Uri { get; set; }
        /// <summary>
        /// 应用短名称
        /// </summary>
        [Column("ShortName")]
        public string ShortName { get; set; }
        /// <summary>
        /// 接入端类型（用于区分硬件设备）
        /// </summary>
        [Column("ClientType")]
        public string ClientType { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        [Column("Seq")]
        public int Seq { get; set; }
        /// <summary>
        /// 图标地址
        /// </summary>
        [Column("IconUri")]
        public string IconUri { get; set; }
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
        /// 单位id
        /// </summary>
        [Column("Unit")]
        public string Unit { get; set; }

        ///// <summary>
        ///// 是否推荐应用
        ///// </summary>
        //public string pe_IsTJ
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 是否必备应用
        ///// </summary>
        //public string pe_IsBB
        //{
        //    get;
        //    set;
        //}
    }
}
