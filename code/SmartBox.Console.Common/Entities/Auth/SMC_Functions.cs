//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.SMC_Functions.cs
// ??????? 
//
// ????? 2014-03-05 04:11:21
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
    [Table("SMC_Functions", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_Functions : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("FN_ID", ColumnType.PrimaryKey)]
        public int FN_ID
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("FN_Name", ColumnType.Normal)]
        public string FN_Name
        {
            get;
            set;
        }

        [Column("FN_Code", ColumnType.Normal)]
        public string FN_Code
        {
            get;
            set;
        }

        [Column("FN_Type", ColumnType.Normal)]
        public string FN_Type
        {
            get;
            set;
        }

        [Column("FN_VisibleType", ColumnType.Normal)]
        public string FN_VisibleType
        {
            get;
            set;
        }
        ///
        ///????????int
        ///


        [Column("Upper_FN_ID", ColumnType.Normal)]
        public int Upper_FN_ID
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("FN_Url", ColumnType.Normal)]
        public string FN_Url
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
        ///????????varchar
        ///


        [Column("FN_Img", ColumnType.Normal)]
        public string FN_Img
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("FN_Path", ColumnType.Normal)]
        public string FN_Path
        {
            get;
            set;
        }


        ///
        ///????????text
        ///


        [Column("FN_Demo", ColumnType.Normal)]
        public string FN_Demo
        {
            get;
            set;
        }


        ///
        ///????????bit
        ///


        [Column("FN_IsDefault", ColumnType.Normal)]
        public bool FN_IsDefault
        {
            get;
            set;
        }
        
        [Column("FN_Disabled", ColumnType.Normal)]
        public bool FN_Disabled
        {
            get;
            set;
        }


        ///
        ///????????datetime
        ///


        [Column("FN_CreatedTime", ColumnType.Normal)]
        public DateTime FN_CreatedTime
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("FN_CreatedUser", ColumnType.Normal)]
        public string FN_CreatedUser
        {
            get;
            set;
        }


        ///
        ///????????datetime
        ///


        [Column("FN_UpdateTime", ColumnType.Normal)]
        public DateTime FN_UpdateTime
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("FN_UpdateUser", ColumnType.Normal)]
        public string FN_UpdateUser
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("FN_Sequence", ColumnType.Normal)]
        public int FN_Sequence
        {
            get;
            set;
        }


    }
}


