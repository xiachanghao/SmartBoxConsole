//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.monitor_log.cs
// ??????? 
//
// ????? 2014-02-27 04:26:00
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
    [Table("monitor_log", SorMappingType.ByAttributes)]
    [Serializable]
    public class monitor_log {
        ///
        ///????????int
        ///
        

        [Column("log_id", ColumnType.IdentityAndPrimaryKey)]        
        public int log_id {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("log_df_item", ColumnType.Normal)]
        public string log_df_item {
            get;
			set;
        }
        
        
        ///
        ///????????int
        ///
        

        [Column("log_monitorvalue", ColumnType.Normal)]
        public int log_monitorvalue {
            get;
			set;
        }
        
        
        ///
        ///????????datetime
        ///
        

        [Column("log_datetime", ColumnType.Normal)]
        public DateTime log_datetime {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("log_df_kind", ColumnType.Normal)]
        public string log_df_kind {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("log_df_code", ColumnType.Normal)]
        public string log_df_code {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("log_df_lever", ColumnType.Normal)]
        public string log_df_lever {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("log_status", ColumnType.Normal)]
        public string log_status {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("log_hostip", ColumnType.Normal)]
        public string log_hostip {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("log_hostname", ColumnType.Normal)]
        public string log_hostname {
            get;
			set;
        }
        
        
    }
}


