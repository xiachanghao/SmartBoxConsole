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
    [Table("SMC_UserException", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_UserException : BaseEntry
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("ID", ColumnType.IdentityAndPrimaryKey)]
        public int ID
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        [Column("UID", ColumnType.Normal)]
        public string UID
        {
            get;
            set;
        }


        /// <summary>
        /// 1启用例外 2禁用例外
        /// </summary>
        [Column("Type", ColumnType.Normal)]
        public int Type
        {
            get;
            set;
        }
    }
}


