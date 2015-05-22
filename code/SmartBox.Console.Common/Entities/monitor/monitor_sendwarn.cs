//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.monitor_sendwarn.cs
// ??????? 
//
// ????? 2014-02-27 04:26:13
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
    [Table("monitor_sendwarn", SorMappingType.ByAttributes)]
    [Serializable]
    public class monitor_sendwarn : BaseModels {
        ///
        ///????????int
        ///
        

        [Column("sw_id", ColumnType.IdentityAndPrimaryKey)]        
        public int sw_id {
            get;
			set;
        }
        
        
        ///
        ///????????int
        ///
        

        [Column("sw_log_id", ColumnType.Normal)]
        public int sw_log_id {
            get;
			set;
        }
        
        
        ///
        ///????????datetime
        ///
        

        [Column("sw_senddate", ColumnType.Normal)]
        public DateTime sw_senddate {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("sw_sendresult", ColumnType.Normal)]
        public string sw_sendresult {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("sw_receptman", ColumnType.Normal)]
        public string sw_receptman {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("sw_sendway", ColumnType.Normal)]
        public string sw_sendway {
            get;
			set;
        }
        
        
        ///
        ///????????datetime
        ///
        

        [Column("sw_createdate", ColumnType.Normal)]
        public DateTime sw_createdate {
            get;
			set;
        }
        
        
        ///
        ///????????datetime
        ///
        

        [Column("sw_lastsenddate", ColumnType.Normal)]
        public DateTime sw_lastsenddate {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("sw_mobile", ColumnType.Normal)]
        public string sw_mobile {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("sw_email", ColumnType.Normal)]
        public string sw_email {
            get;
			set;
        }
        
        
    }
}


