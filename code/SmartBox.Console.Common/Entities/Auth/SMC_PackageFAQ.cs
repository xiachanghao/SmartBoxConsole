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
    [Table("SMC_PackageFAQ", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_PackageFAQ : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("pf_id", ColumnType.PrimaryKey)]
        public int pf_id
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("pf_uid", ColumnType.Normal)]
        public string pf_uid
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("pf_uname", ColumnType.Normal)]
        public string pf_uname
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("pf_question", ColumnType.Normal)]
        public string pf_question
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("pf_answer", ColumnType.Normal)]
        public string pf_answer
        {
            get;
            set;
        }

        [Column("pf_askdate", ColumnType.Normal)]
        public DateTime pf_askdate
        {
            get;
            set;
        }

        [Column("pf_askemail", ColumnType.Normal)]
        public string pf_askemail
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///

        [Column("pe_id", ColumnType.Normal)]
        public int pe_id
        {
            get;
            set;
        }

        [Column("pf_askmobile", ColumnType.Normal)]
        public string pf_askmobile
        {
            get;
            set;
        }

        [Column("pf_peplyman", ColumnType.Normal)]
        public string pf_peplyman
        {
            get;
            set;
        }

        [Column("pf_need_syncto_inside", ColumnType.Normal)]
        public bool pf_need_syncto_inside
        {
            get;
            set;
        }
        
        [Column("pf_need_syncto_outside", ColumnType.Normal)]
        public bool pf_need_syncto_outside
        {
            get;
            set;
        }
    }
}


