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

    public partial class SMC_UserDao : BaseDao<SMC_User>
    {

        public SMC_UserDao(string key)
            : base(key)
        {

        }

        



        //public IList<SMC_User> QueryChildUnitsByUnitID(int Unit_ID)
        //{
        //    string sql = string.Format("SELECT * FROM SMC_User WHERE Upper_Unit_ID=@unitid order by Unit_Sequence", Unit_ID);
        //    Hashtable pars = new Hashtable();
        //    pars.Add("unitid", Unit_ID);

        //    return base.Query(sql, pars);
        //}    

        public SMC_User GetUser(string uid)
        {
            string sql = "SELECT * FROM SMC_User WHERE U_UID=@uid";
            Hashtable pars = new Hashtable();
            pars.Add("uid", uid);

            IList<SMC_User> r = base.Query(sql, pars);
            if (r.Count <= 0)
                return null;
            else
                return r[0];
        }

        public int GetMaxId()
        {
            string sql = "select isnull(max(u_id), 0) u_id from smc_user";
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i;
        }

        /*
        public SelectPagnationExDictionary GetUserEnableAuthorizationSys(string uid, string username, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {

            string where = "";

            if (deviceAuthStatus >= 0)
            {
                where += " and u.u_enable_status=" + deviceAuthStatus + "";
            }

            if (!String.IsNullOrEmpty(uid))
            {
                where += " and u.u_uid like '%" + uid + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u.u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(username))
            {
                where += " and u.u_name like '%" + username + "%'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_start))
            {
                where += " and u_auth_submit_time >= '" + u_auth_submit_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_end))
            {
                where += " and u_auth_submit_time <= '" + u_auth_submit_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"dbo.smc_user u", "u.u_id id, u.u_uid useruid,u.u_unitname unitname,u.u_unitcode unitcode,u.u_name username,u.u_enable_status,u_auth_submit_time", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
        */
        public SelectPagnationExDictionary GetSelectUser(string uid, string username, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {

            string where = "";

            if (deviceAuthStatus >= 0)
            {
                where += " and u.u_enable_status=" + deviceAuthStatus + "";
            }

            if (!String.IsNullOrEmpty(uid))
            {
                where += " and u.u_uid like '%" + uid + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u.u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(username))
            {
                where += " and u.u_name like '%" + username + "%'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_start))
            {
                where += " and u_auth_submit_time >= '" + u_auth_submit_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_end))
            {
                where += " and u_auth_submit_time <= '" + u_auth_submit_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"dbo.smc_user u", "u.u_id id, u.u_uid useruid,u.u_unitname unitname,u.u_unitcode unitcode,u.u_name username,u.u_enable_status,u_auth_submit_time", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }

        public SelectPagnationExDictionary GetUserRetryLock(string uid, string username, string u_unitcode, string u_lock_time_start, string u_lock_time_end, int lockStatus, string orderby, int pageSize, int pageIndex)
        {

            string where = "u_lock_status=1";

            if (lockStatus >= 0)
            {
                where += " and u.u_lock_status=" + lockStatus + "";
            }

            if (!String.IsNullOrEmpty(uid))
            {
                where += " and u.u_uid like '%" + uid + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u.u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(username))
            {
                where += " and u.u_name like '%" + username + "%'";
            }

            if (!String.IsNullOrEmpty(u_lock_time_start))
            {
                where += " and u_lock_time >= '" + u_lock_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_lock_time_end))
            {
                where += " and u_lock_time <= '" + u_lock_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"dbo.smc_user u", "u.u_id id, u.u_uid useruid,u.u_unitname unitname,u.u_unitcode unitcode,u.u_name username,u.u_enable_status,u_lock_status,u_lock_time,u_lock_expire_time", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
        

        public void UpdateMaxId()
        {
            int maxId = GetMaxId();
            string sql = "update smc_autotableid set at_maxid=@maxid where at_tablename='SMC_User'";
            Hashtable pars = new Hashtable();
            pars.Add("maxid", maxId);
            this.ExecuteNonQuery(sql, pars);
        }

        public IList<SMC_User> GetNeedSyncSMC_Users()
        {
            string sql = "select * from smc_user";
            return this.Query(sql);
        }

        public SelectPagnationEx<SMC_User> GetUserManageList(string uid, string uname, string u_unitcode, string u_disable_time_start, string u_disable_time_end, string u_enable_time_start, string u_enable_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            string where = "";
            if (lockStatus >= 0)
            {
                where += " and u_lock_status=" + lockStatus + "";
            }
            if (enabledStatus >= 0)
            {
                where += " and u_enable_status=" + enabledStatus + "";
            }
            if (authStatus >= 0)
            {
                where += " and u_auth_status=" + authStatus + "";
            }

            if (!String.IsNullOrEmpty(uid))
            {
                where += " and u_uid like '%" + uid + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(uname))
            {
                where += " and u_name like '%" + uname + "%'";
            }

            if (!String.IsNullOrEmpty(u_disable_time_start))
            {
                where += " and u_disable_time >= '" + u_disable_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_disable_time_end))
            {
                where += " and u_disable_time <= '" + u_disable_time_end + "'";
            }

            if (!String.IsNullOrEmpty(u_enable_time_start))
            {
                where += " and u_enable_time >= '" + u_enable_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_enable_time_end))
            {
                where += " and u_enable_time <= '" + u_enable_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}",  where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);


            SelectPagnationEx<SMC_User> ex = this.SelectPaginationExT("SMC_User", "*", pageIndex + 1, pageSize, orderby, where, "");

            //Hashtable pars = new Hashtable();
            //SplitPageResult<SMC_User> re = this.QuerySplitPage<SMC_User>(sql, "*", orderby, pageSize, pageIndex, pars);

            return ex;
        }

        public SelectPagnationEx<_OnLineUser> GetUserInfoManage(string uid, string name, string unitcode, string orderby, int pageSize, int pageIndex)
        {
            string mainDbName = SmartBox.Console.Common.DbSqlHelper.GetMainDBName();
            string statisticDbName = SmartBox.Console.Common.DbSqlHelper.GetStatisticDBName();
//            string with = @"with x as(
//select createtime lastusetime ,SessionValue.value('(/Session/UserInfo/Uid)[1]', 'varchar(50)') uid
//,SessionValue.value('(/Session/UserInfo/Status)[1]', 'varchar(50)') Status
//,SessionValue.value('(/Session/ClientInfo/ID)[1]', 'varchar(50)') DeviceID
//,SessionValue.value('(/Session/ClientInfo/Resource)[1]', 'varchar(50)') ClientType 
//from " + mainDbName + @"..SessionStore
//),y as (
//select lastusetime,uid,Status,DeviceID,ClientType,rank() over (partition by uid order by lastusetime desc) r from x
//),
//
//t as (
//select id,uid,DeviceID,ClientType,status,lastlogintime,lastlogouttime,RANK() over (partition by uid order by lastlogintime desc) r from " + mainDbName + @"..useronline where status=1
//)
//,p as(
//select * from t where r>=1 and r=1
//)
//,q as(
//select p.UID,p.DeviceID,p.ClientType,p.Status,p.LastLoginTime,p.LastLogoutTime,y.lastusetime from p join y on p.UID=y.uid where y.r=1
// ),r as(
// select q.*,u.u_id,u.u_name,u.u_unitcode,u.u_unitname,u.u_enable_time,u.u_disable_time from q join " + statisticDbName+@"..smc_user u on q.UID=u.u_uid
// )";

            string with = @"with t as (
select id,uid,deviceid,clienttype,status,lastlogintime,lastlogouttime,RANK() over (partition by uid order by lastlogintime desc) r from " + mainDbName + @"..useronline where status=1
),u as(
select createtime lastusetime ,SessionValue.value('(/Session/UserInfo/Uid)[1]', 'varchar(50)') uid
,SessionValue.value('(/Session/UserInfo/Status)[1]', 'varchar(50)') Status
,SessionValue.value('(/Session/ClientInfo/ID)[1]', 'varchar(50)') DeviceID
,SessionValue.value('(/Session/ClientInfo/Resource)[1]', 'varchar(50)') ClientType
from " + mainDbName + @"..SessionStore
),r as(
select t.*,u.u_id,u.u_name,u.u_unitcode,u.u_unitname,u.u_enable_time,u.u_disable_time from t join " + statisticDbName + @"..smc_user u on t.UID=u.u_uid
),s as(
select r.*,(select top 1 logintime from " + mainDbName + @"..useronlinelog where uid=r.uid and deviceid=r.DeviceID order by logintime desc) lastusetime from r where r=1)";
            string where = "";
            if (!String.IsNullOrEmpty(unitcode))
            {
                where += " and u_unitcode='" + unitcode + "'";
            }

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

            SelectPagnationExDictionary ex = this.SelectPaginationExDictionary("s", "*", pageIndex + 1, pageSize, orderby, where, with);
            SelectPagnationEx<_OnLineUser> ex2 = new SelectPagnationEx<_OnLineUser>();
            List<_OnLineUser> lst = new List<_OnLineUser>();

            foreach (IDictionary<string, object> u in ex.Result)
            {
                _OnLineUser _u = new _OnLineUser();
                _u.U_ID = u["u_id"].ToString();
                _u.U_NAME = u["u_name"].ToString();
                _u.U_UID = u["uid"].ToString();
                _u.ClientType = u["clienttype"].ToString();
                _u.DeviceID = u["deviceid"].ToString();
                _u.U_UnitCode = u["u_unitcode"].ToString();
                _u.u_enable_time = u["u_enable_time"] == null ? DateTime.MinValue: (DateTime)u["u_enable_time"];
                _u.u_disable_time = u["u_disable_time"] == null ? DateTime.MinValue : (DateTime)u["u_disable_time"];
                _u.last_ip = "";
                _u.lastusetime = u["lastusetime"] == null ? DateTime.MinValue : (DateTime)u["lastusetime"];
                _u.U_UnitName = u["u_unitname"].ToString();
                _u.Status = u["status"].ToString() == "1" ? "在线" : "不在线";//_get_useronline_status(mainDbName, u.U_UID);
                _u.LastLoginTime = u["lastlogintime"] == null ? DateTime.MinValue : (DateTime)u["lastlogintime"];//_get_useronline_LastLoginTime(mainDbName, u.U_UID);
                _u.LastLogoutTime = u["lastlogouttime"] == null ? DateTime.MinValue : (DateTime)u["lastlogouttime"];// _get_useronline_LastLogoutTime(mainDbName, u.U_UID);
                lst.Add(_u);
            }
            ex2.Result = lst;
            ex2.RecordCount = ex.RecordCount;
            ex2.PageCount = ex.PageCount;
            ex2.ReturnValue = ex.ReturnValue;

            return ex2;
        }
        
        public SelectPagnationEx<_OnLineUser> GetUserInfoManageList(string uid, string uname, string u_unitcode, string u_disable_time_start, string u_disable_time_end, string u_enable_time_start, string u_enable_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            string where = "";
            if (lockStatus >= 0)
            {
                where += " and u_lock_status=" + lockStatus + "";
            }
            if (enabledStatus >= 0)
            {
                where += " and u_enable_status=" + enabledStatus + "";
            }
            if (authStatus >= 0)
            {
                where += " and u_auth_status=" + authStatus + "";
            }

            if (!String.IsNullOrEmpty(uid))
            {
                where += " and u_uid like '%" + uid + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(uname))
            {
                where += " and u_name like '%" + uname + "%'";
            }

            if (!String.IsNullOrEmpty(u_disable_time_start))
            {
                where += " and u_disable_time >= '" + u_disable_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_disable_time_end))
            {
                where += " and u_disable_time <= '" + u_disable_time_end + "'";
            }

            if (!String.IsNullOrEmpty(u_enable_time_start))
            {
                where += " and u_enable_time >= '" + u_enable_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_enable_time_end))
            {
                where += " and u_enable_time <= '" + u_enable_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}",  where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);


            SelectPagnationEx<SMC_User> ex = this.SelectPaginationExT("SMC_User", "*", pageIndex + 1, pageSize, orderby, where, "");
            SelectPagnationEx<_OnLineUser> ex2 = new SelectPagnationEx<_OnLineUser>();
            List<_OnLineUser> lst = new List<_OnLineUser>();

            string mainDbName = SmartBox.Console.Common.DbSqlHelper.GetMainDBName();
            foreach (SMC_User u in ex.Result)
            {
                _OnLineUser _u = new _OnLineUser();
                _u.U_ID = u.U_ID.ToString();
                _u.U_NAME = u.U_NAME;
                _u.U_UID = u.U_UID;
                _u.U_UnitCode = u.U_UNITCODE;
                _u.u_enable_time = u.u_enable_time;
                _u.u_disable_time = u.u_disable_time;
                _u.last_ip = "";
                _u.U_UnitName = u.u_unitname;
                _u.Status = _get_useronline_status(mainDbName, u.U_UID);
                _u.LastLoginTime = _get_useronline_LastLoginTime(mainDbName, u.U_UID);
                _u.LastLogoutTime = _get_useronline_LastLogoutTime(mainDbName, u.U_UID);
                lst.Add(_u);
            }
            ex2.Result = lst;
            ex2.RecordCount = ex.RecordCount;
            ex2.PageCount = ex.PageCount;
            ex2.ReturnValue = ex.ReturnValue;
            //Hashtable pars = new Hashtable();
            //SplitPageResult<SMC_User> re = this.QuerySplitPage<SMC_User>(sql, "*", orderby, pageSize, pageIndex, pars);

            return ex2;
        }

        private string _get_useronline_status(string mainDbName, string uid)
        {
            string sql = "select top 1 status from " + mainDbName + "..UserOnline where UID=@uid order by status";
            Hashtable pars = new Hashtable();
            pars.Add("uid", uid);
            object o = this.ExecuteScalar(sql, pars);
            if (o == null)
            {
                return "";
            }
            else
            {
                return Convert.ToString(o);
            }
        }
        
        private DateTime _get_useronline_LastLoginTime(string mainDbName, string uid)
        {
            string sql = "select top 1 lastlogintime from " + mainDbName + "..UserOnline where UID=@uid order by lastlogintime desc";
            Hashtable pars = new Hashtable();
            pars.Add("uid", uid);
            object o = this.ExecuteScalar(sql, pars);
            if (o == null)
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(o);
        }

        private DateTime _get_useronline_LastLogoutTime(string mainDbName, string uid)
        {
            string sql = "select top 1 lastlogouttime from " + mainDbName + "..UserOnline where UID=@uid order by lastlogouttime desc";
            Hashtable pars = new Hashtable();
            pars.Add("uid", uid);
            object o = this.ExecuteScalar(sql, pars);
            if (o == null)
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(o);
        }
        
        
        public SelectPagnationEx<SMC_User> GetUserLoginUnLockList(string uid, string uname, string u_unitcode, string u_lock_time_start, string u_lock_time_end, string u_lock_expire_time_start, string u_lock_expire_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            string where = "";
            if (lockStatus >= 0)
            {
                where += " and u_lock_status=" + lockStatus + "";
            }
            if (enabledStatus >= 0)
            {
                where += " and u_enable_status=" + enabledStatus + "";
            }
            if (authStatus >= 0)
            {
                where += " and u_auth_status=" + authStatus + "";
            }

            if (!String.IsNullOrEmpty(uid))
            {
                where += " and u_uid like '%" + uid + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(uname))
            {
                where += " and u_name like '%" + uname + "%'";
            }

            if (!String.IsNullOrEmpty(u_lock_time_start))
            {
                where += " and u_lock_time >= '" + u_lock_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_lock_time_end))
            {
                where += " and u_lock_time <= '" + u_lock_time_end + "'";
            }

            if (!String.IsNullOrEmpty(u_lock_expire_time_start))
            {
                where += " and u_lock_expire_time >= '" + u_lock_expire_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_lock_expire_time_end))
            {
                where += " and u_lock_expire_time <= '" + u_lock_expire_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}",  where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);


            SelectPagnationEx<SMC_User> ex = this.SelectPaginationExT("SMC_User", "*", pageIndex + 1, pageSize, orderby, where, "");

            //Hashtable pars = new Hashtable();
            //SplitPageResult<SMC_User> re = this.QuerySplitPage<SMC_User>(sql, "*", orderby, pageSize, pageIndex, pars);

            return ex;
        }
        

        public SelectPagnationEx<SMC_User> GetUserAuthorizationList(string uid, string uname, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            string where = "";
            if (lockStatus >= 0)
            {
                where += " and u_lock_status=" + lockStatus + "";
            }
            if (enabledStatus >= 0)
            {
                where += " and u_enable_status=" + enabledStatus + "";
            }
            if (authStatus >= 0)
            {
                where += " and u_auth_status=" + authStatus + "";
            }

            if (!String.IsNullOrEmpty(uid))
            {
                where += " and u_uid like '%" + uid + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(uname))
            {
                where += " and u_name like '%" + uname + "%'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_start))
            {
                where += " and u_auth_submit_time >= '" + u_auth_submit_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_end))
            {
                where += " and u_auth_submit_time <= '" + u_auth_submit_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}",  where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);


            SelectPagnationEx<SMC_User> ex = this.SelectPaginationExT("SMC_User", "*", pageIndex + 1, pageSize, orderby, where, "");

            //Hashtable pars = new Hashtable();
            //SplitPageResult<SMC_User> re = this.QuerySplitPage<SMC_User>(sql, "*", orderby, pageSize, pageIndex, pars);

            return ex;
        }
        public SelectPagnationEx<SMC_User> GetUserComparerationList(string uid, string uname, string u_unitcode, string u_compare_time_start, string u_compare_time_end, string orderby, int pageSize, int pageIndex)
        {
            string where = " and u_need_sync=1 and u_lock_status=0 ";
            if (!String.IsNullOrEmpty(uid))
            {
                where += " and u_uid like '%" + uid + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(uname))
            {
                where += " and u_name like '%" + uname + "%'";
            }

            if (!String.IsNullOrEmpty(u_compare_time_start))
            {
                where += " and u_need_sync_compare_time >= '" + u_compare_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_compare_time_end))
            {
                where += " and u_need_sync_compare_time <= '" + u_compare_time_end + "'";
            }

            

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            string sql = String.Format("select * from [dbo].SMC_User where u_need_sync=1 and u_lock_status=0 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);


            SelectPagnationEx<SMC_User> ex = this.SelectPaginationExT("SMC_User", "*", pageIndex + 1, pageSize, orderby, where, "");
            return ex;
        }
    }
}
