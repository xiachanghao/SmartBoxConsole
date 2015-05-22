//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.Monitor_linkman.cs
// ???????  
//
// ?????   2014-02-27 04:26:00
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
using SmartBox.Console.Common;


namespace SmartBox.Console.Dao {

    public class Monitor_linkmanDao : BaseDao<Monitor_linkman> {
		
		public Monitor_linkmanDao(string key)
            : base(key)
        {

        }

        public SelectPagnationExDictionary GetMonitorLinkmanList(string lm_uname, string lm_udept, string orderby, int pageSize, int pageIndex)
        {

            string where = "";

            if (!String.IsNullOrEmpty(lm_uname))
            {
                where += " and lm_uname like '%" + lm_uname + "%'";
            }

            if (!String.IsNullOrEmpty(lm_udept))
            {
                where += " and lm_udept like '%" + lm_udept + "%'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"" + maindbName + @".dbo.monitor_linkman", "*", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
    }
}
