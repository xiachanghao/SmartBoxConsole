//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.SMC_AutoTableID.cs
// ??????? 
//
// ????? 2014-03-05 04:10:59
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
    [Table("SMC_AutoTableID", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_AutoTableID : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("at_id", ColumnType.IdentityAndPrimaryKey)]
        public int at_id
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("AT_TableName", ColumnType.Normal)]
        public string AT_TableName
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("AT_MaxID", ColumnType.Normal)]
        public int AT_MaxID
        {
            get;
            set;
        }


    }
}


