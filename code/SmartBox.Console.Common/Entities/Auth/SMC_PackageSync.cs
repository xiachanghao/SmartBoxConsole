using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.Biz.Entry;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("SMC_PackageExtSyncToOutside", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_PackageSync : BaseEntry
    {
        //[Column("peso_id", ColumnType.IdentityAndPrimaryKey)]
        //public int peso_id
        //{
        //    get;
        //    set;
        //}

        [Column("pe_id", ColumnType.Normal)]
        public int pe_id
        {
            get;
            set;
        }

        [Column("sync_bat_no", ColumnType.Normal)]
        public int sync_bat_no
        {
            get;
            set;
        }

        [Column("pe_name", ColumnType.Normal)]
        public string pe_name
        {
            get;
            set;
        }

        [Column("sync_time", ColumnType.Normal)]
        public DateTime last_sync_time
        {
            get;
            set;
        }
        [Column("sync_status", ColumnType.Normal)]
        public bool last_sync_status
        {
            get;
            set;
        }

        [Column("description", ColumnType.Normal)]
        public string description
        {
            get;
            set;
        }
       
    }
}


