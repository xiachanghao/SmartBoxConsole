using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.Biz.Entry;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("SMC_PackagePicture", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_PackagePicture:BaseEntry
    {
        [Column("pp_id", ColumnType.PrimaryKey)]
        public int pp_id
        {
            get;
            set;
        }

        [Column("pe_id", ColumnType.Normal)]
        public int pe_id
        {
            get;
            set;
        }

        [Column("pp_path", ColumnType.Normal)]
        public string pp_path
        {
            get;
            set;
        }

        [Column("pp_CreatedDate", ColumnType.Normal)]
        public DateTime pp_CreatedDate
        {
            get;
            set;
        }

        [Column("pp_desc", ColumnType.Normal)]
        public string pp_desc
        {
            get;
            set;
        }

        [Column("pp_title", ColumnType.Normal)]
        public string pp_title
        {
            get;
            set;
        }
    }
}
