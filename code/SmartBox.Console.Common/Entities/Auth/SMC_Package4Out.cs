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
    [Table("SMC_Package4Out", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_Package4Out : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("po_ID", ColumnType.PrimaryKey)]
        public int po_ID
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("Name", ColumnType.Normal)]
        public string Name
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("Type", ColumnType.Normal)]
        public string Type
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("ClientType", ColumnType.Normal)]
        public string ClientType
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("DisplayName", ColumnType.Normal)]
        public string DisplayName
        {
            get;
            set;
        }

        [Column("Description", ColumnType.Normal)]
        public string Description
        {
            get;
            set;
        }

        [Column("Version", ColumnType.Normal)]
        public string Version
        {
            get;
            set;
        }


        [Column("DownloadUri", ColumnType.Normal)]
        public string DownloadUri
        {
            get;
            set;
        }

        [Column("CreateUid", ColumnType.Normal)]
        public string CreateUid
        {
            get;
            set;
        }

        [Column("UpdateUid", ColumnType.Normal)]
        public string UpdateUid
        {
            get;
            set;
        }

[Column("BuildVer", ColumnType.Normal)]
        public int BuildVer
        {
            get;
            set;
        }
        ///
        ///????????varchar
        ///


        [Column("CreateTime", ColumnType.Normal)]
public DateTime CreateTime
        {
            get;
            set;
        }

        [Column("UpdateTime", ColumnType.Normal)]
        public DateTime UpdateTime
        {
            get;
            set;
        }
    }
}


