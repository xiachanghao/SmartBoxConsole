//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.monitor_sendwarn.cs
// ???????  
//
// ?????   2014-02-27 04:26:23
//
// ?????
// ?????
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using SmartBox.Console.Common.Entities;
using System.Data;
using SmartBox.Console.Common.Entities.Search;


namespace SmartBox.Console.Dao {

    public class monitor_sendwarnDao : BaseDao<monitor_sendwarn> {
		
		public monitor_sendwarnDao(string key)
            : base(key)
        {

        }
    }
}
