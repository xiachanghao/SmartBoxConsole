//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.SMC_Unit.cs
// ???????  
//
// ?????   2014-03-05 04:11:53
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
using System.Collections;


namespace SmartBox.Console.Dao
{

    public partial class UserOnlineDao : BaseDao<UserOnline>
    {

        public UserOnlineDao(string key)
            : base(key)
        {

        }

        public SelectPagnationEx<UserOnline> GetUserDeviceOnline(string uid, string name, string unitcode, string orderby, int pageSize, int pageIndex)
        {
            string mainDbName = SmartBox.Console.Common.DbSqlHelper.GetMainDBName();
            string statisticDbName = SmartBox.Console.Common.DbSqlHelper.GetStatisticDBName();

            string with = "";
            string tableName = @"" + mainDbName + @"..UserOnline";
            string where = "";

            if (!String.IsNullOrEmpty(uid))
            {
                where += " and uid = '" + uid + "'";
            }

            if (!String.IsNullOrEmpty(name))
            {
                where += " and u_name like '%" + name + "%'";
            }
            if (where.StartsWith(" and"))
            {
                where = where.Substring(4);
            }

            SelectPagnationEx<UserOnline> ex = this.SelectPaginationExT(tableName, "*", pageIndex + 1, pageSize, orderby, where, with);

            return ex;
        }
    }
}