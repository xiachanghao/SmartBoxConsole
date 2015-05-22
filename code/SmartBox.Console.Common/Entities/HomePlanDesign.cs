using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("HomePlanDesign")]
    public class HomePlanDesign
    {
        /// <summary>
        /// 布局ID
        /// </summary>
        [Column("PlanID",ColumnType.PrimaryKey)]
        public int PlanID { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        [Column("AppID",ColumnType.PrimaryKey)]
        public int AppID { get; set; }

        /// <summary>
        /// 位置
        /// <para>页面为3x3的方形,共九个单位,每个格子为一个单位</para>
        /// <para>存储格式为:x,y</para>
        /// </summary>
        [Column("Location")]
        public string Location { get; set; }

        /// <summary>
        /// 宽,高
        /// <para>存储格式为:宽,高</para>
        /// </summary>
        [Column("Size")]
        public string Size { get; set; }

        [Column("Type")]
        public string Type { get; set; }

        /// <summary>
        /// 图片地址
        /// 格式为:(Server://beyondbit.smartbox.server.image/图片ID)
        /// </summary>
        [Column("ValueUri")]
        public string ValueUri { get; set; }

        public string CreateUid { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateUid { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
