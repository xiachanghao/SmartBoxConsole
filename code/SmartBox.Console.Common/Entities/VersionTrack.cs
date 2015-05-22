using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;
using Beyondbit.Framework.Biz.Entry;
namespace SmartBox.Console.Common.Entities
{
    [Table("VersionTrack", SorMappingType.ByAttributes)]
    [Serializable]
    public class VersionTrack: BaseModels
    {
        [Column("VersionId", ColumnType.IdentityAndPrimaryKey)]
        public int VersionId
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("PluginCode")]
        public string PluginCode
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("VersionName")]
        public string VersionName
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("PreVersionId")]
        public int PreVersionId
        {
            set;
            get;
        }

        [Column("FilePath")]
        public string FilePath
        { get; set; }

        /// <summary>
        /// 版本状态 
        /// 0:未发布（升级）
        /// 1:正在使用
        /// 2:已过期
        /// 3:未发布（新增）
        /// 4:修改（新增未上传）
        /// 5:修改（升级未上传）
        /// </summary>
        [Column("VersionStatus")]
        public int VersionStatus
        { get; set; }


        [Column("VersionSummary")]
        public string VersionSummary
        { get; set; }

        public IList<ConfigInfo> configList
        {
            get;
            set;
        }
    }
}
