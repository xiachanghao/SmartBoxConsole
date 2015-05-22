//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.Monitor_Config.cs
// ??????? 
//
// ????? 2014-02-27 04:25:34
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
    [Table("Monitor_Config", SorMappingType.ByAttributes)]
    [Serializable]
    public class Monitor_Config  {
        ///
        ///????????int
        ///
        

        [Column("cfg_id", ColumnType.IdentityAndPrimaryKey)]        
        public int cfg_id {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cfg_hostname", ColumnType.Normal)]
        public string cfg_hostname {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cfg_hostip", ColumnType.Normal)]
        public string cfg_hostip {
            get;
			set;
        }
        
        
        ///
        ///????????text
        ///
        

        [Column("cfg_file", ColumnType.Normal)]
        public string cfg_file {
            get;
			set;
        }
        
        
        ///
        ///????????datetime
        ///
        

        [Column("cfg_createdate", ColumnType.Normal)]
        public DateTime cfg_createdate {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cfg_createman", ColumnType.Normal)]
        public string cfg_createman {
            get;
			set;
        }
        
        
        ///
        ///????????datetime
        ///
        

        [Column("cfg_updatedate", ColumnType.Normal)]
        public DateTime cfg_updatedate {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cfg_updateman", ColumnType.Normal)]
        public string cfg_updateman {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cfg_updatestatus", ColumnType.Normal)]
        public string cfg_updatestatus {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("cfg_isuse", ColumnType.Normal)]
        public string cfg_isuse {
            get;
			set;
        }
        
        
        ///
        ///????????datetime
        ///
        

        [Column("cfg_usedate", ColumnType.Normal)]
        public DateTime cfg_usedate {
            get;
			set;
        }
        
        
    }
}


