//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.Monitor_Defind.cs
// ??????? 
//
// ????? 2014-02-27 04:25:52
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
    [Table("Monitor_Defind", SorMappingType.ByAttributes)]
    [Serializable]
    public class Monitor_Defind {
        ///
        ///????????int
        ///
        

        [Column("df_id", ColumnType.IdentityAndPrimaryKey)]        
        public int df_id {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("df_kind", ColumnType.Normal)]
        public string df_kind {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("df_item", ColumnType.Normal)]
        public string df_item {
            get;
			set;
        }
        
        
        ///
        ///????????int
        ///
        

        [Column("df_maxvalue", ColumnType.Normal)]
        public int df_maxvalue {
            get;
			set;
        }
        
        
        ///
        ///????????int
        ///
        

        [Column("df_minvalue", ColumnType.Normal)]
        public int df_minvalue {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("df_lever", ColumnType.Normal)]
        public string df_lever {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("df_code", ColumnType.Normal)]
        public string df_code {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("df_sendway", ColumnType.Normal)]
        public string df_sendway {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("df_receptman", ColumnType.Normal)]
        public string df_receptman {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("df_startsenddate", ColumnType.Normal)]
        public string df_startsenddate {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("df_endsenddate", ColumnType.Normal)]
        public string df_endsenddate {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("df_issend", ColumnType.Normal)]
        public string df_issend {
            get;
			set;
        }
        
        
    }
}


