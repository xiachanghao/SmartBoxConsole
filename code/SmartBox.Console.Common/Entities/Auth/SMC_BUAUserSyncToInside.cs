//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 SMC_BUAUserSyncToInside.cs
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
    [Table("SMC_BUAUserSyncToInside", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_BUAUserSyncToInside : BaseEntry
    {
        [Column("busi_id", ColumnType.PrimaryKey)]
        public int busi_id
        {
            get;
            set;
        }

        [Column("user_uid", ColumnType.Normal)]
        public string user_uid
        {
            get;
            set;
        }

        [Column("sync_bat_no", ColumnType.Normal)]
        public int sync_bat_no
        {
            get;
            set;
        }

        [Column("sync_time", ColumnType.Normal)]
        public DateTime sync_time
        {
            get;
            set;
        }

        [Column("sync_status", ColumnType.Normal)]
        public bool sync_status
        {
            get;
            set;
        }

        [Column("sync_user_uid", ColumnType.Normal)]
        public string sync_user_uid
        {
            get;
            set;
        }

        [Column("sync_user_name", ColumnType.Normal)]
        public string sync_user_name
        {
            get;
            set;
        }

        [Column("description", ColumnType.Normal)]
        public string description
        {
            get;
            set;
        }

        [Column("user_name", ColumnType.Normal)]
        public string user_name
        {
            get;
            set;
        }
    }
}
