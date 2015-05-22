//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.monitor_cmd.cs
// ??????? 
//
// ????? 2014-02-27 04:25:30
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
    [Table("monitor_cmd", SorMappingType.ByAttributes)]
    [Serializable]
    public class monitor_cmd  {
        ///
        ///????????int
        ///
        

        [Column("cmd_id", ColumnType.IdentityAndPrimaryKey)]        
        public int cmd_id {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cmd_title", ColumnType.Normal)]
        public string cmd_title {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cmd_code", ColumnType.Normal)]
        public string cmd_code {
            get;
			set;
        }
        
        
        ///
        ///????????datetime
        ///
        

        [Column("cmd_senddate", ColumnType.Normal)]
        public DateTime cmd_senddate {
            get;
			set;
        }
        
        
        ///
        ///????????datetime
        ///
        

        [Column("cmd_excudedate", ColumnType.Normal)]
        public DateTime cmd_excudedate {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cmd_excuderesult", ColumnType.Normal)]
        public string cmd_excuderesult {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cmd_hostname", ColumnType.Normal)]
        public string cmd_hostname {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cmd_hostip", ColumnType.Normal)]
        public string cmd_hostip {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cmd_discription", ColumnType.Normal)]
        public string cmd_discription {
            get;
			set;
        }
        
        
    }
}


