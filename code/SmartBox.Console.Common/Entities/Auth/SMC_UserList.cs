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
    [Table("SMC_UserList", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_UserList : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("UL_ID", ColumnType.PrimaryKey)]
        public int UL_ID
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
        ///????????varchar
        ///


        [Column("UL_UID", ColumnType.Normal)]
        public string UL_UID
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


        public string Unit_Name
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("UL_PWD", ColumnType.Normal)]
        public string UL_PWD
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("UL_Name", ColumnType.Normal)]
        public string UL_Name
        {
            get;
            set;
        }


        ///
        ///????????text
        ///


        [Column("UL_Demo", ColumnType.Normal)]
        public string UL_Demo
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("UL_MobilePhone", ColumnType.Normal)]
        public string UL_MobilePhone
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("UL_MailAddress", ColumnType.Normal)]
        public string UL_MailAddress
        {
            get;
            set;
        }


        ///
        ///????????datetime
        ///


        [Column("UL_CreatedTime", ColumnType.Normal)]
        public DateTime UL_CreatedTime
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("UL_CreatedUser", ColumnType.Normal)]
        public string UL_CreatedUser
        {
            get;
            set;
        }


        ///
        ///????????datetime
        ///


        [Column("UL_UpdateTime", ColumnType.Normal)]
        public DateTime UL_UpdateTime
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("UL_UpdateUser", ColumnType.Normal)]
        public string UL_UpdateUser
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("UL_Sequence", ColumnType.Normal)]
        public int UL_Sequence
        {
            get;
            set;
        }


        [Column("UL_Gender", ColumnType.Normal)]
        public string UL_Gender
        {
            get;
            set;
        }

        /// <summary>
        /// 是否主管理员
        /// </summary>
        [Column("UL_IsMain", ColumnType.Normal)]
        public bool UL_IsMain
        {
            get;
            set;
        }
    }
}


