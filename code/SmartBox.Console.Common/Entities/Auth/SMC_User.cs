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
    [Table("SMC_User", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_User : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("U_ID", ColumnType.PrimaryKey)]
        public int U_ID
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("U_UID", ColumnType.Normal)]
        public string U_UID
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("U_NAME", ColumnType.Normal)]
        public string U_NAME
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("U_UNITCODE", ColumnType.Normal)]
        public string U_UNITCODE
        {
            get;
            set;
        }

        [Column("u_unitname", ColumnType.Normal)]
        public string u_unitname
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("U_PASSWORD", ColumnType.Normal)]
        public string U_PASSWORD
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("U_CREATEDDATE", ColumnType.Normal)]
        public DateTime U_CREATEDDATE
        {
            get;
            set;
        }

        //[Column("u_auth_status", ColumnType.Normal)]
        //public UserAuthStatus u_auth_status
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 待审核2 启用1 禁用0
        /// </summary>
        [Column("u_enable_status", ColumnType.Normal)]
        public UserEnabledStatus u_enable_status
        {
            get;
            set;
        }

        /// <summary>
        /// 未锁定0 已锁定1
        /// </summary>
        [Column("u_lock_status", ColumnType.Normal)]
        public UserLockedStatus u_lock_status
        {
            get;
            set;
        }

        [Column("u_auth_time", ColumnType.Normal)]
        public DateTime u_auth_time
        {
            get;
            set;
        }

        [Column("u_enable_time", ColumnType.Normal)]
        public DateTime u_enable_time
        {
            get;
            set;
        }

        [Column("u_lock_time", ColumnType.Normal)]
        public DateTime u_lock_time
        {
            get;
            set;
        }

        [Column("u_unlock_time", ColumnType.Normal)]
        public DateTime u_unlock_time
        {
            get;
            set;
        }

        [Column("u_lock_expire_time", ColumnType.Normal)]
        public DateTime u_lock_expire_time
        {
            get;
            set;
        }

        [Column("u_auth_submit_time", ColumnType.Normal)]
        public DateTime u_auth_submit_time
        {
            get;
            set;
        }

        [Column("u_disable_time", ColumnType.Normal)]
        public DateTime u_disable_time
        {
            get;
            set;
        }

        /// <summary>
        /// 用户比对的时间
        /// </summary>
        [Column("u_need_sync_compare_time", ColumnType.Normal)]
        public DateTime u_need_sync_compare_time
        {
            get;
            set;
        }
        
        /// <summary>
        /// 更新用户时间
        /// </summary>
        [Column("u_update_time", ColumnType.Normal)]
        public DateTime u_update_time
        {
            get;
            set;
        }

        /// <summary>
        /// 是否需要从统一授权同步
        /// </summary>
        [Column("u_need_sync", ColumnType.Normal)]
        public bool u_need_sync
        {
            get;
            set;
        }
    }
}


