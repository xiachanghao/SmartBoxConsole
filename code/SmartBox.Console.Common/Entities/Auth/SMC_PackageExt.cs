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
    [Table("SMC_PackageExt", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_PackageExt : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("pe_id", ColumnType.PrimaryKey)]
        public int pe_id
        {
            get;
            set;
        }
        [Column("pe_UpdateTime", ColumnType.Normal)]
        public DateTime pe_UpdateTime
        {
            get;
            set;
        }
        [Column("pe_UpdateUid", ColumnType.Normal)]
        public string pe_UpdateUid
        {
            get;
            set;
        }

        [Column("pe_CreateUid", ColumnType.Normal)]
        public string pe_CreateUid
        {
            get;
            set;
        }
        [Column("pe_DownloadUri", ColumnType.Normal)]
        public string pe_DownloadUri
        {
            get;
            set;
        }
        [Column("pe_BuildVer", ColumnType.Normal)]
        public string pe_BuildVer
        {
            get;
            set;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        [Column("pe_Version", ColumnType.Normal)]
        public string pe_Version
        {
            get;
            set;
        }
        /// <summary>
        /// 上一版本号
        /// </summary>
        [Column("pe_LastVersion", ColumnType.Normal)]
        public string pe_LastVersion
        {
            get;
            set;
        }
        [Column("pe_Description", ColumnType.Normal)]
        public string pe_Description
        {
            get;
            set;
        }

        [Column("pe_DisplayName", ColumnType.Normal)]
        public string pe_DisplayName
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("pe_ClientType", ColumnType.Normal)]
        public string pe_ClientType
        {
            get;
            set;
        }
        [Column("pe_Type", ColumnType.Normal)]
        public string pe_Type
        {
            get;
            set;
        }

        ///
        ///????????varchar
        ///


        [Column("pe_IsTJ", ColumnType.Normal)]
        public string pe_IsTJ
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("pe_IsBB", ColumnType.Normal)]
        public string pe_IsBB
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///
        [Column("pe_PictureUrl", ColumnType.Normal)]
        public string pe_PictureUrl
        {
            get;
            set;
        }

        [Column("pe_2dPictureUrl", ColumnType.Normal)]
        public string pe_2dPictureUrl
        {
            get;
            set;
        }

        [Column("pe_Firmware", ColumnType.Normal)]
        public string pe_Firmware
        {
            get;
            set;
        }

        [Column("TableName", ColumnType.Normal)]
        public string TableName
        {
            get;
            set;
        }

        [Column("pe_UnitCode", ColumnType.Normal)]
        public string pe_UnitCode
        {
            get;
            set;
        }

        [Column("pe_UnitName", ColumnType.Normal)]
        public string pe_UnitName
        {
            get;
            set;
        }

        [Column("pe_Name", ColumnType.Normal)]
        public string pe_Name
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///

        [Column("TableID", ColumnType.Normal)]
        public int TableID
        {
            get;
            set;
        }

        [Column("pe_DownCount", ColumnType.Normal)]
        public int pe_DownCount
        {
            get;
            set;
        }

        [Column("pe_Size", ColumnType.Normal)]
        public int pe_Size
        {
            get;
            set;
        }

        [Column("pe_CreatedTime", ColumnType.Normal)]
        public DateTime pe_CreatedTime
        {
            get;
            set;
        }

        [Column("pe_Category", ColumnType.Normal)]
        public string pe_Category
        {
            get;
            set;
        }

        [Column("pe_CategoryID", ColumnType.Normal)]
        public string pe_CategoryID
        {
            get;
            set;
        }

        //[Column("pe_AsyncStatus", ColumnType.Normal)]
        //public string pe_AsyncStatus
        //{
        //    get;
        //    set;
        //}

        [Column("pe_UsefulStstus", ColumnType.Normal)]
        public string pe_UsefulStstus
        {
            get;
            set;
        }

        /// <summary>
        /// 0待审核 1审核通过 2审核不通过
        /// </summary>
        [Column("pe_AuthStatus", ColumnType.Normal)]
        public int pe_AuthStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 审核提交时间
        /// </summary>
        [Column("pe_AuthSubmitTime", ColumnType.Normal)]
        public DateTime pe_AuthSubmitTime
        {
            get;
            set;
        }

        /// <summary>
        /// 审核提交人uid
        /// </summary>
        [Column("pe_AuthSubmitUID", ColumnType.Normal)]
        public string pe_AuthSubmitUID
        {
            get;
            set;
        }

        /// <summary>
        /// 审核提交人
        /// </summary>
        [Column("pe_AuthSubmitName", ColumnType.Normal)]
        public string pe_AuthSubmitName
        {
            get;
            set;
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        [Column("pe_AuthTime", ColumnType.Normal)]
        public DateTime pe_AuthTime
        {
            get;
            set;
        }
        
        [Column("pe_ApplicationCode", ColumnType.Normal)]
        public String pe_ApplicationCode
        {
            get;
            set;
        }

        /// <summary>
        /// 审核人
        /// </summary>
        [Column("pe_AuthMan", ColumnType.Normal)]
        public String pe_AuthMan
        {
            get;
            set;
        }

        /// <summary>
        /// 审核人UID
        /// </summary>
        [Column("pe_AuthManUID", ColumnType.Normal)]
        public String pe_AuthManUID
        {
            get;
            set;
        }

        [Column("pe_ApplicationName", ColumnType.Normal)]
        public String pe_ApplicationName
        {
            get;
            set;
        }
        
        /// <summary>
        /// 0待同步 1同步成功 2同步失败
        /// </summary>
        [Column("pe_SyncStatus", ColumnType.Normal)]
        public int pe_SyncStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 上下架操作人UID
        /// </summary>
        [Column("pe_UsefulOperatorUID", ColumnType.Normal)]
        public string pe_UsefulOperatorUID
        {
            get;
            set;
        }

        /// <summary>
        /// 上下架操作人姓名
        /// </summary>
        [Column("pe_UsefulOperatorName", ColumnType.Normal)]
        public string pe_UsefulOperatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 上下架操作时间
        /// </summary>
        [Column("pe_UsefulTime", ColumnType.Normal)]
        public DateTime pe_UsefulTime
        {
            get;
            set;
        }

        /// <summary>
        /// 操作方向，比如上架、下架
        /// </summary>
        [Column("pe_Direction", ColumnType.Normal)]
        public string pe_Direction
        {
            get;
            set;
        }

        /// <summary>
        /// 文件地址：如~/PackageExt/32/ia.ipa
        /// </summary>
        [Column("pe_FileUrl", ColumnType.Normal)]
        public string pe_FileUrl
        {
            get;
            set;
        }

        [Column("pe_ExtentInfo", ColumnType.Normal)]
        public string pe_ExtentInfo
        {
            get;
            set;
        }
    }
}


