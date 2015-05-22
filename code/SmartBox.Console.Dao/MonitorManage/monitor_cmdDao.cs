//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.monitor_cmd.cs
// ???????  
//
// ?????   2014-02-27 04:25:34
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

    public class monitor_cmdDao : BaseDao<monitor_cmd> {
		
		public monitor_cmdDao(string key)
            : base(key)
        {

        }

        public SelectPagnationExDictionary GetMonitorCmdList(string cmd_title, string cmd_senddate_start, string cmd_senddate_end, string cmd_executeresult, string cmd_code, string orderby, int pageSize, int pageIndex)
        {

            string where = "";

            int _cmd_executeresult = -1;//
            if (!String.IsNullOrEmpty(cmd_executeresult))
                _cmd_executeresult = Convert.ToInt32(cmd_executeresult);
            if (_cmd_executeresult >= 0)
            {
                where += " and cmd_executeresult=" + _cmd_executeresult + "";
            }

            if (!String.IsNullOrEmpty(cmd_title))
            {
                where += " and cmd_title like '%" + cmd_title + "%'";
            }

            if (!String.IsNullOrEmpty(cmd_code))
            {
                where += " and cmd_code like '%" + cmd_code + "%'";
            }

            if (!String.IsNullOrEmpty(cmd_senddate_start))
            {
                where += " and cmd_senddate >= '" + cmd_senddate_start + "'";
            }

            if (!String.IsNullOrEmpty(cmd_senddate_end))
            {
                where += " and cmd_senddate <= '" + cmd_senddate_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"" + maindbName + @".dbo.Monitor_Cmd", "*", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
    }
}
