using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// 针对Android和iOS的Application扩展
    /// </summary>
    [Table("App4AI")]
    [Serializable]
    public class App4AI
    {
        [Column("ID", ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }
        /// <summary>
        /// Android和iOS的发布包信息ID
        /// </summary>
        [Column("Package4AIID")]
        public int Package4AIID { get; set; }
        /// <summary>
        /// 应用系统ID
        /// </summary>
        [Column("AppID")]
        public int? AppID { get; set; }
        /// <summary>
        /// 应用系统Code
        /// </summary>
        [Column("AppCode")]
        public string AppCode { get; set; }
        /// <summary>
        /// 接入端类型（用于区分硬件设备）
        /// </summary>
        [Column("ClientType")]
        public string ClientType { get; set; }
        /// <summary>
        /// 图标地址
        /// </summary>
        [Column("IconUri")]
        public string IconUri { get; set; }
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
        /// 更新者
        /// </summary>
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }


        #region Readonly
        /// <summary>
        /// 关联应用的名称
        /// </summary>
        [Column("AppName",ColumnType.ReadOnly)]
        public string AppName { get; set; }

        /// <summary>
        /// 关联应用的显示名称
        /// </summary>
        [Column("AppDisplayName",ColumnType.ReadOnly)]
        public string AppDisplayName { get; set; }

        /// <summary>
        /// 应用安装包的名称
        /// </summary>
        [Column("PackageName",ColumnType.ReadOnly)]
        public string PackageName { get; set; }

        [Column("ver",ColumnType.ReadOnly)]
        public string Version { get; set; }
        #endregion

        private List<Action4Android> _actionList;
        public List<Action4Android> ActionList
        {
            get
            {
                if (_actionList == null)
                {
                    _actionList = new List<Action4Android>();
                }
                return _actionList;
            }
            set 
            {
                _actionList = value;
            }
        }
    }
}
