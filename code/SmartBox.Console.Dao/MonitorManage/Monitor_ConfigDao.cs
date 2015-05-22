//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.Monitor_Config.cs
// ???????  
//
// ?????   2014-02-27 04:25:52
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

    public class Monitor_ConfigDao : BaseDao<Monitor_Config> {
		
		public Monitor_ConfigDao(string key)
            : base(key)
        {

        }

        public Common.JsonFlexiGridData QueryMonitorConfig(Common.PageView view)
        {
            JsonFlexiGridData result = base.QueryDataForFlexGridByPager("*", "Monitor_Config", " Order by  cfg_createdate asc ", "cfg_id", "", view);
            return result;
        }

        public SelectPagnationExDictionary GetMonitorConfigList(string hostname, string updatestatus, string isuse, string enalbe_time_start, string enalbe_time_end, string orderby, int pageSize, int pageIndex)
        {

            string where = "";

            int _updatestatus = -1;//
            if (!String.IsNullOrEmpty(updatestatus))
                _updatestatus = Convert.ToInt32(updatestatus);
            if (_updatestatus >= 0)
            {
                where += " and cfg_updatestatus=" + _updatestatus + "";
            }

            if (!String.IsNullOrEmpty(hostname))
            {
                where += " and cfg_hostname like '%" + hostname + "%'";
            }

            if (!String.IsNullOrEmpty(isuse))
            {
                where += " and cfg_isuse like '%" + isuse + "%'";
            }

            if (!String.IsNullOrEmpty(enalbe_time_start))
            {
                where += " and cfg_usedate >= '" + enalbe_time_start + "'";
            }

            if (!String.IsNullOrEmpty(enalbe_time_end))
            {
                where += " and cfg_usedate <= '" + enalbe_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"" + maindbName + @".dbo.Monitor_Config", "*", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }

    }
}
