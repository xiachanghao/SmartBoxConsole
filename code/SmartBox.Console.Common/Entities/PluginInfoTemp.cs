using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Biz.Entry;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("PluginInfoTemp", SorMappingType.ByAttributes)]
    public class PluginInfoTemp : BaseEntry
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
        [Column("PluginSummary")]
        public string PluginSummary
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


        [Column("ActionCode")]
        public string ActionCode
        {
            get;
            set;
        }

        [Column("ActionSummary")]
        public string ActionSummary
        {
            get;
            set;
        }
        [Column("VersionSummary")]
        public string VersionSummary
        {
            get;
            set;
        }

        [Column("IsIgnoreConfig")]
        public bool IsIgnoreConfig
        {
            get;
            set;
        }

        [Column("PreVersionPCs")]
        public string PreVersionPCs
        {
            get;
            set;
        }
        [Column("IsNew")]
        public bool IsNew
        {
            get;
            set;
        }

        [Column("IsUse")]
        public bool IsUse
        {
            get;
            set;
        }

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

        public IList<ConfigInfo> configList
        {
            get;
            set;
        }


        public string PCname
        { get; set; }

    }
}
