using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;
using Beyondbit.Framework.Biz.Entry;

namespace SmartBox.Console.Common.Entities
{
    [Table("PluginInfo", SorMappingType.ByAttributes)]
    [Serializable]
    public class PluginInfo : BaseModels
    {
        [Column("PluginCode", ColumnType.PrimaryKey)]
        public string PluginCode
        {
            set;
            get;
        }
        [Column("PluginCateCode")]
        public string PluginCateCode
        {
            set;
            get;
        }
        [Column("HashCode")]
        public string HashCode
        {
            set;
            get;
        }
        [Column("Version")]
        public string Version
        {
            set;
            get;
        }
        [Column("DisplayName")]
        public string DisplayName
        {
            set;
            get;
        }
        [Column("DirectoryName")]
        public string DirectoryName
        {
            set;
            get;
        }
        [Column("TypeFullName")]
        public string TypeFullName
        {
            set;
            get;
        }
        [Column("FileName")]
        public string FileName
        {
            set;
            get;
        }
        [Column("IsNeed")]
        public bool IsNeed
        {
            set;
            get;
        }
        [Column("IsDefault")]
        public bool IsDefault
        {
            set;
            get;
        }
        [Column("Sequence")]
        public int Sequence
        {
            set;
            get;
        }
        [Column("Summary")]
        public string Summary
        {
            set;
            get;
        }
        [Column("PluginUrl")]
        public string PluginUrl
        {
            set;
            get;
        }


        [Column("CompanyName")]
        public string CompanyName
        {
            set;
            get;
        }
        [Column("CompanyLinkman")]
        public string CompanyLinkman
        {
            set;
            get;
        }
        [Column("CompanyTel")]
        public string CompanyTel
        {
            set;
            get;
        }
        [Column("CompanyHomePage")]
        public string CompanyHomePage
        {
            set;
            get;
        }
        [Column("IsNew")]
        public bool IsNew
        { get; set; }
        [Column("IsUse")]
        public bool IsUse
        { get; set; }

        [Column("isPublic")]
        public bool IsPublic
        {
            get;
            set;
        }

        [Column("AppCode")]
        public string AppCode
        {
            get;
            set;
        }

        [Column("PrivilegeCode")]
        public string PrivilegeCode
        {
            get;
            set;
        }

        public string ActionCode
        { get; set; }

        public string ActionSummary
        { get; set; }
        public string VersionSummary
        { get; set; }

        public bool IsIgnoreConfig
        { get; set; }

        public string PCname
        { get; set; }

        public IList<ConfigInfo> configList
        {
            get;
            set;
        }


    }
}
