//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.Monitor_linkman.cs
// ??????? 
//
// ????? 2014-02-27 04:25:56
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
    [Table("Monitor_linkman", SorMappingType.ByAttributes)]
    [Serializable]
    public class Monitor_linkman  {
        ///
        ///????????int
        ///
        

        [Column("lm_id", ColumnType.IdentityAndPrimaryKey)]        
        public int lm_id {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("lm_uid", ColumnType.Normal)]
        public string lm_uid {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("lm_uname", ColumnType.Normal)]
        public string lm_uname {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("lm_udept", ColumnType.Normal)]
        public string lm_udept {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("lm_mobile", ColumnType.Normal)]
        public string lm_mobile {
            get;
			set;
        }
        
        
        ///
        ///????????varchar
        ///
        

        [Column("lm_email", ColumnType.Normal)]
        public string lm_email {
            get;
			set;
        }
        
        
    }
}


