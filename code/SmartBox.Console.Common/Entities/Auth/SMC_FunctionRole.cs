//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.SMC_FunctionRole.cs
// ??????? 
//
// ????? 2014-03-05 04:11:17
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
    [Table("SMC_FunctionRole", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_FunctionRole : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("FR_ID", ColumnType.PrimaryKey)]
        public int FR_ID
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("Role_ID", ColumnType.Normal)]
        public int Role_ID
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("FN_ID", ColumnType.Normal)]
        public int FN_ID
        {
            get;
            set;
        }


        ///
        ///????????datetime
        ///


        [Column("FR_CreatedTime", ColumnType.Normal)]
        public DateTime FR_CreatedTime
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("FR_CreatedUser", ColumnType.Normal)]
        public string FR_CreatedUser
        {
            get;
            set;
        }


        ///
        ///????????datetime
        ///


        [Column("FR_UpdateTime", ColumnType.Normal)]
        public DateTime FR_UpdateTime
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("FR_UpdateUser", ColumnType.Normal)]
        public string FR_UpdateUser
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("FR_Sequence", ColumnType.Normal)]
        public int FR_Sequence
        {
            get;
            set;
        }


    }
}


