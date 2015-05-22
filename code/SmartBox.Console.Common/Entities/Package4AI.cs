using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// Android和iOS的发布包信息
    /// </summary>
    [Table("Package4AI")]
    [Serializable]
    public class Package4AI
    {
        /// <summary>
        /// Package的ID
        /// </summary>
        [Column("ID", ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }

        /// <summary>
        /// Package的包名（例如：com.beyondbit.smartbox.phone）
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }

        [Column("Type")]
        public string Type { get; set; }

        [Column("ClientType")]
        public string ClientType { get; set; }

        /// <summary>
        /// Package的显示名
        /// </summary>
        [Column("DisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Package的描述
        /// </summary>
        [Column("Description")]
        public string Description { get; set; }

        /// <summary>
        /// 对外发布的版本号
        /// </summary>
        [Column("Version")]
        public string Version { get; set; }

        /// <summary>
        /// 编译的版本号，解决Bug的快速更新时，发布版本不变，而实际产生变化的情况。
        /// </summary>
        [Column("BuildVer")]
        public int BuildVer { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        [Column("DownloadUri")]
        public string DownloadUri { get; set; }

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


        private List<App4AI> _app4AIList;
        public List<App4AI> App4AIList
        {
            get
            {
                if (_app4AIList == null)
                {
                    _app4AIList = new List<App4AI>();
                }
                return _app4AIList;
            }
            set
            {
                _app4AIList = value;
            }
        }
    }
}
