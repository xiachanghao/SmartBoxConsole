//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.SMC_UserList.cs
// ??????? 
//
// ????? 2014-03-05 04:11:53
//
// ?????
// ?????
//----------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.Biz.Entry;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("SMC_PackageExtHistory", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_PackageExtHistory : BaseEntry
    {
        ///
        ///????????int
        ///
        [Column("id", ColumnType.IdentityAndPrimaryKey)]
        public int id
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
        [Column("pe_Version", ColumnType.Normal)]
        public string pe_Version
        {
            get;
            set;
        }
        [Column("pe_CreateTime", ColumnType.Normal)]
        public DateTime pe_CreateTime
        {
            get;
            set;
        }

        [Column("pe_PackageUrl", ColumnType.Normal)]
        public string pe_PackageUrl
        {
            get;
            set;
        }
    }
}


