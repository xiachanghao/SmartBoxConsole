//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.monitor_log.cs
// ???????  
//
// ?????   2014-02-27 04:26:13
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

    public class monitor_logDao : BaseDao<monitor_log> {
		
		public monitor_logDao(string key)
            : base(key)
        {

        }

        public SelectPagnationExDictionary GetMonitorLogList(string timeStart, string timeEnd, string log_status, string log_df_lever, string log_df_item, string orderby, int pageSize, int pageIndex)
        {

            string where = "";

            int _log_status = -1;//
            if (!String.IsNullOrEmpty(log_status))
                _log_status = Convert.ToInt32(log_status);
            if (_log_status >= 0)
            {
                where += " and log_status=" + _log_status + "";
            }

            int _log_df_lever = -1;//
            if (!String.IsNullOrEmpty(log_df_lever))
                _log_df_lever = Convert.ToInt32(log_df_lever);
            if (_log_df_lever >= 0)
            {
                where += " and log_df_lever=" + _log_df_lever + "";
            }

            if (!String.IsNullOrEmpty(log_df_item))
            {
                where += " and log_df_item like '%" + log_df_item + "%'";
            }

            if (!String.IsNullOrEmpty(timeStart))
            {
                where += " and log_datetime >= '" + timeStart + "'";
            }

            if (!String.IsNullOrEmpty(timeEnd))
            {
                where += " and log_datetime <= '" + timeEnd + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"" + maindbName + @".dbo.Monitor_Log", "*", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
    }
}
