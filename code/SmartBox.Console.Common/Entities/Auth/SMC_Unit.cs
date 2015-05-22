//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.SMC_Unit.cs
// ??????? 
//
// ????? 2014-03-05 04:11:44
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
    [Table("SMC_Unit", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_Unit : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("Unit_ID", ColumnType.PrimaryKey)]
        public string Unit_ID
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("Unit_Name", ColumnType.Normal)]
        public string Unit_Name
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("Upper_Unit_ID", ColumnType.Normal)]
        public string Upper_Unit_ID
        {
            get;
            set;
        }


        ///
        ///????????text
        ///


        [Column("Unit_Demo", ColumnType.Normal)]
        public string Unit_Demo
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("Unit_Path", ColumnType.Normal)]
        public string Unit_Path
        {
            get;
            set;
        }


        ///
        ///????????datetime
        ///


        [Column("Unit_CreatedTime", ColumnType.Normal)]
        public DateTime Unit_CreatedTime
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("Unit_CreatedUser", ColumnType.Normal)]
        public string Unit_CreatedUser
        {
            get;
            set;
        }


        ///
        ///????????datetime
        ///


        [Column("Unit_UpdateTime", ColumnType.Normal)]
        public DateTime Unit_UpdateTime
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("Unit_UpdateUser", ColumnType.Normal)]
        public string Unit_UpdateUser
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("Unit_Sequence", ColumnType.Normal)]
        public int Unit_Sequence
        {
            get;
            set;
        }


    }
}


