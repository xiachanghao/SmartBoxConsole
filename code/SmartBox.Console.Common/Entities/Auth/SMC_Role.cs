//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.SMC_Role.cs
// ??????? 
//
// ????? 2014-03-05 04:11:30
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
    [Table("SMC_Role", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_Role : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("Role_ID", ColumnType.PrimaryKey)]
        public int Role_ID
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("Role_Name", ColumnType.Normal)]
        public string Role_Name
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("Unit_ID", ColumnType.Normal)]
        public string Unit_ID
        {
            get;
            set;
        }


        ///
        ///????????text
        ///


        [Column("Role_Demo", ColumnType.Normal)]
        public string Role_Demo
        {
            get;
            set;
        }


        ///
        ///????????datetime
        ///


        [Column("Role_CreatedTime", ColumnType.Normal)]
        public DateTime Role_CreatedTime
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("Role_CreatedUser", ColumnType.Normal)]
        public string Role_CreatedUser
        {
            get;
            set;
        }


        ///
        ///????????datetime
        ///


        [Column("Role_UpdateTime", ColumnType.Normal)]
        public DateTime Role_UpdateTime
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("Role_UpdateUser", ColumnType.Normal)]
        public string Role_UpdateUser
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("Role_Sequence", ColumnType.Normal)]
        public int Role_Sequence
        {
            get;
            set;
        }


    }
}


