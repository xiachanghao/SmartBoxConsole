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

    public class DeviceExceptionDao : BaseDao<DeviceException>
    {

        public DeviceExceptionDao(string key)
            : base(key)
        {

        }

        public SelectPagnationExDictionary GetDeviceEnableAuthorizationException(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {

            string where = "de.type=1";

            if (deviceAuthStatus >= 0)
            {
                where += " and dua.status=" + deviceAuthStatus + "";
            }

            if (!String.IsNullOrEmpty(uid))
            {
                where += " and u.u_uid like '%" + uid + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u.u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u.u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(model))
            {
                where += " and d.model like '%" + model + "%'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_start))
            {
                where += " and dua.applytime >= '" + u_auth_submit_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_end))
            {
                where += " and dua.applytime <= '" + u_auth_submit_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"deviceexception de 
join " + maindbName + @".dbo.smc_user u on u.u_uid=de.useruid ", @"de.id, u.u_uid useruid,u.u_unitname unitname,
u.u_unitcode unitcode,u.u_name username", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
        public SelectPagnationExDictionary GetDeviceDisableAuthorizationException(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {

            string where = "de.type=2";

            if (deviceAuthStatus >= 0)
            {
                where += " and dua.status=" + deviceAuthStatus + "";
            }

            if (!String.IsNullOrEmpty(uid))
            {
                where += " and u.u_uid like '%" + uid + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u.u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u.u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(model))
            {
                where += " and d.model like '%" + model + "%'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_start))
            {
                where += " and dua.applytime >= '" + u_auth_submit_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_end))
            {
                where += " and dua.applytime <= '" + u_auth_submit_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
//            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"deviceexception de 
//join device d on d.id=de.deviceid
//join deviceuser du on d.id= du.deviceid
//join " + maindbName + @".dbo.smc_user u on u.u_uid=du.uid ", @"de.id, u.u_uid useruid,u.u_unitname unitname,
//u.u_unitcode unitcode,u.u_name username,du.deviceid,
//d.model,d.description", pageIndex + 1, pageSize, orderby, where, "");

            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"deviceexception de 
join " + maindbName + @".dbo.smc_user u on u.u_uid=de.useruid ", @"de.id, u.u_uid useruid,u.u_unitname unitname,
u.u_unitcode unitcode,u.u_name username", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
        
    }
}
