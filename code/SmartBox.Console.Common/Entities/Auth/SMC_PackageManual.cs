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
    [Table("SMC_PackageManual", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_PackageManual : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("pm_id", ColumnType.PrimaryKey)]
        public int pm_id
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("pm_name", ColumnType.Normal)]
        public string pm_name
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("pm_url", ColumnType.Normal)]
        public string pm_url
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("pm_createdtime", ColumnType.Normal)]
        public DateTime pm_createdtime
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("pm_updatetime", ColumnType.Normal)]
        public DateTime pm_updatetime
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

        
    }
}


