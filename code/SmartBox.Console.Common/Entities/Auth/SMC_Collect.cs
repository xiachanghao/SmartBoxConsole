using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.Biz.Entry;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("SMC_Collect", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_Collect : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("clt_id", ColumnType.PrimaryKey)]
        public int clt_id
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("pe_id", ColumnType.Normal)]
        public int pe_id
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("ClientType", ColumnType.Normal)]
        public string ClientType
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("uid", ColumnType.Normal)]
        public string uid
        {
            get;
            set;
        }
        

        [Column("uname", ColumnType.Normal)]
        public string uname
        {
            get;
            set;
        }

        [Column("clt_CollectDate", ColumnType.Normal)]
        public DateTime clt_CollectDate
        {
            get;
            set;
        }
        

    }
}


