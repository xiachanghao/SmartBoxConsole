using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using System.Data;

namespace SmartBox.Console.Dao
{
    public class DeviceDao : BaseDao<Device>
    {
        public DeviceDao(string key)
            : base(key)
        { }


        public SelectPagnationExDictionary GetDeviceAuthorization(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            
            string where = "";

            if (deviceAuthStatus >= 0)
            {
                if (deviceAuthStatus == 2)
                {
                    where += " and (dua.status=2 or dua.status=3)";
                }
                else
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
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"deviceuserapply dua 
join deviceuser du on du.id=dua.deviceuserid 
join device d on d.id=du.deviceid 
left join " + maindbName+@".dbo.smc_user u on u.u_uid=du.uid 
", "dua.id, dua.deviceuserid,du.uid useruid,u.u_unitname unitname,u.u_unitcode unitcode,u.u_name username,du.deviceid,d.model,d.description,dua.applytime,dua.status,dua.checktime", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
        
        public SelectPagnationExDictionary GetDeviceUser(string uid, string model, string deviceid, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {

            string where = "";

            if (deviceAuthStatus >= 0)
            {
                where += " and du.status=" + deviceAuthStatus + "";
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

            if (!string.IsNullOrEmpty(deviceid))
            {
                where = where + " and du.deviceid like '%" + deviceid + "%'";
            }


            //if (!String.IsNullOrEmpty(u_auth_submit_time_end))
            //{
            //    where += " and dua.applytime <= '" + u_auth_submit_time_end + "'";
            //}

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"deviceuser du join device d on d.id=du.deviceid left join " + maindbName + @".dbo.smc_user u on u.u_uid=du.uid left join userinfo ui on ui.useruid=du.uid 
", "du.id,du.uid useruid,u.u_unitname unitname,u.u_unitcode unitcode,u.u_name username,du.deviceid,d.model,d.status devicestatus,d.description,du.status,ui.Lock lockstatus", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
        
        
        public List<IDictionary<string, object>> GetTaskCenter(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {

            string where = "";

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

            //bool isSystemManager = true;
            string sUnitCondition1 = "";
            if (!String.IsNullOrEmpty(u_unitcode))
            {
                sUnitCondition1 = " and u.u_unitcode = '" + u_unitcode + "'";
            }
            string s = String.Format(@"with t_deviceauth as(
select 'deviceauth' t,count(dua.id) cn from smartbox.dbo.deviceuserapply dua join smartbox.dbo.deviceuser
du on du.id=dua.DeviceUserID join smartboxapp.dbo.smc_user u on u.u_uid=du.uid 
where dua.status=0 {0})
,t_userauth as (
select 'userauth' t,count(u_id) cn from smartboxapp.dbo.smc_user u where u.u_enable_status = 2 {1}
),t_deviceunlock as (
select 'deviceunlock' t,count(d.id) cn from smartbox.dbo.device d 
join smartbox.dbo.deviceuser du on d.id=du.deviceid 
join smartboxapp.dbo.smc_user u on u.u_uid=du.uid
where d.status=1 {2}
),t_userunlock as (
select 'userunlock' t,count(u_uid) cn from smartboxapp.dbo.SMC_User u where u_lock_status=1 {3}
)
select * from t_deviceauth union all
select * from t_userauth union all
select * from t_deviceunlock union all
select * from t_userunlock", sUnitCondition1, sUnitCondition1, sUnitCondition1, sUnitCondition1);

            where = "";
            string statisticDBName = DbSqlHelper.GetStatisticDBName();
            string maindbName = DbSqlHelper.GetMainDBName();
            s = s.Replace("smartboxapp.", statisticDBName + ".");
            s = s.Replace("smartbox.", maindbName + ".");

            DataSet ds = base.ExecuteDataset(s, System.Data.CommandType.Text);
            List<IDictionary<string, object>> r = TranslateTable(ds.Tables[0]);
            //SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"", "*", pageIndex + 1, pageSize, orderby, where, s);


            return r;
        }
        
        public SelectPagnationExDictionary GetDeviceEnableAuthorizationSys(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            
            string where = "";

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
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"deviceuserapply dua 
join deviceuser du on du.id=dua.userdeviceid 
join "+maindbName+@".dbo.smc_user u on u.u_uid=du.uid 
join device d on d.id=du.deviceid", "dua.id, u.u_uid useruid,u.u_unitname unitname,u.u_unitcode unitcode,u.u_name username,du.deviceid,d.model,d.description,dua.applytime,dua.status", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }

        
        public SelectPagnationExDictionary GetAppPackageAuthorization(string appName, string application, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, string categoryID, string orderby, int pageSize, int pageIndex)
        {
            
            string where = "pe.pe_authstatus=0";

            if (!String.IsNullOrEmpty(categoryID))
            {
                where += " and pe.pe_CategoryID like '%" + categoryID + "%'";
            }

            if (!String.IsNullOrEmpty(appName))
            {
                where += " and pe.pe_Name like '%" + appName + "%'";
            }            

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and pe.pe_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(application))
            {
                where += " and pe.pe_ApplicationName like '%" + application + "%'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_start))
            {
                where += " and pe.pe_AuthSubmitTime >= '" + u_auth_submit_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_end))
            {
                where += " and pe.pe_AuthSubmitTime <= '" + u_auth_submit_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@""+maindbName+@".dbo.smc_packageext pe", "*", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }

        public SelectPagnationExDictionary GetAppPackageSyncList(string appName, string application, string unitcode, string auth_time_start, string auth_time_end, string syncstatus, string orderby, int pageIndex, int pageSize)
        {
            
            string where = "pe.pe_authstatus=1";

            if (!String.IsNullOrEmpty(appName))
            {
                where += " and pe.pe_Name like '%" + appName + "%'";
            }

            if (!String.IsNullOrEmpty(syncstatus))
            {
                where += " and pe.pe_SyncStatus = '" + syncstatus + "'";
            }
            else
            {
                where += " and (pe.pe_syncstatus=0 or pe.pe_syncstatus=2)";
            }

            if (!String.IsNullOrEmpty(unitcode))
            {
                where += " and pe.pe_unitcode like '%" + unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(application))
            {
                where += " and pe.pe_ApplicationName like '%" + application + "%'";
            }

            if (!String.IsNullOrEmpty(auth_time_start))
            {
                where += " and pe.pe_AuthTime >= '" + auth_time_start + "'";
            }

            if (!String.IsNullOrEmpty(auth_time_end))
            {
                where += " and pe.pe_AuthTime <= '" + auth_time_end + "'";
            }

           
            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }

            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@""+maindbName+@".dbo.smc_packageext pe", "*", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
        
        public SelectPagnationExDictionary GetDeviceRetryLock(string uid, string model, string u_unitcode, string u_lock_time_start, string u_lock_time_end, int deviceStatus, string orderby, int pageSize, int pageIndex)
        {
            
            string where = "d.status=1";

            if (deviceStatus >= 0)
            {
                //where += " and d.status=" + deviceStatus + "";
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

            if (!String.IsNullOrEmpty(u_lock_time_start))
            {
                where += " and d.locktime >= '" + u_lock_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_lock_time_end))
            {
                where += " and d.locktime <= '" + u_lock_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"deviceuser du 
join "+maindbName+@".dbo.smc_user u on u.u_uid=du.uid 
join device d on d.id=du.deviceid", "d.id, u.u_uid useruid,u.u_unitname unitname,u.u_unitcode unitcode,u.u_name username,du.deviceid,d.model,d.description,d.locktime,d.status", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
       
        public SelectPagnationExDictionary GetDeviceDisableAuthorizationSys(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            
            string where = "";

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
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"deviceuserapply dua 
join deviceuser du on du.id=dua.userdeviceid 
join "+maindbName+@".dbo.smc_user u on u.u_uid=du.uid 
join device d on d.id=du.deviceid", "dua.id, u.u_uid useruid,u.u_unitname unitname,u.u_unitcode unitcode,u.u_name username,du.deviceid,d.model,d.description,dua.applytime,dua.status", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
       
        public SelectPagnationExDictionary GetDeviceLost(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            
            string where = "";

            if (status >= 0)
            {
                where += " and d.status=" + status + "";
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

            if (!String.IsNullOrEmpty(lost_time_start))
            {
                where += " and d.losttime >= '" + lost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(lost_time_end))
            {
                where += " and d.losttime <= '" + lost_time_end + "'";
            }
            
            if (!String.IsNullOrEmpty(unlost_time_start))
            {
                where += " and d.unlosttime >= '" + unlost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(unlost_time_end))
            {
                where += " and d.unlosttime <= '" + unlost_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"device d 
join deviceuser du on d.id=du.deviceid 
join "+maindbName+".dbo.smc_user u on u.u_uid=du.uid ", "u.u_uid useruid,u.u_unitcode unitcode,u.u_unitname unitname,u.u_name username,du.deviceid,d.losttime,d.unlosttime,d.model,d.description,d.status", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
        
        public SelectPagnationExDictionary GetDeviceSync(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            
            string where = "";

            if (status >= 0)
            {
                where += " and d.status=" + status + "";
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

            if (!String.IsNullOrEmpty(lost_time_start))
            {
                where += " and d.losttime >= '" + lost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(lost_time_end))
            {
                where += " and d.losttime <= '" + lost_time_end + "'";
            }
            
            if (!String.IsNullOrEmpty(unlost_time_start))
            {
                where += " and d.unlosttime >= '" + unlost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(unlost_time_end))
            {
                where += " and d.unlosttime <= '" + unlost_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"device d 
join deviceuser du on d.id=du.deviceid 
join "+maindbName+".dbo.smc_user u on u.u_uid=du.uid ", "u.u_uid useruid,u.u_unitcode unitcode,u.u_unitname unitname,u.u_name username,du.deviceid,d.losttime,d.unlosttime,d.model,d.description,d.status", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
        

        
        
        
        public SelectPagnationExDictionary GetDeviceManage(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, int deviceUserStatus, string orderby, int pageSize, int pageIndex)
        {
            
            string where = "";

            if (status >= 0)
            {
                where += " and d.status=" + status + "";
            }

            if (deviceUserStatus >= 0)
            {
                where += " and du.status=" + deviceUserStatus + "";
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

            if (!String.IsNullOrEmpty(lost_time_start))
            {
                where += " and d.losttime >= '" + lost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(lost_time_end))
            {
                where += " and d.losttime <= '" + lost_time_end + "'";
            }
            
            if (!String.IsNullOrEmpty(unlost_time_start))
            {
                where += " and d.unlosttime >= '" + unlost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(unlost_time_end))
            {
                where += " and d.unlosttime <= '" + unlost_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"device d 
join deviceuser du on d.id=du.deviceid 
join "+maindbName+".dbo.smc_user u on u.u_uid=du.uid ", "du.id deviceuser_id,u.u_uid useruid,u.u_unitcode unitcode,u.u_unitname unitname,u.u_name username,du.deviceid,d.losttime,d.unlosttime,d.model,d.description,d.status,du.status deviceUserStatus", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }

        public SelectPagnationExDictionary GetTimeIndex(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {

            string where = "";

            if (status >= 0)
            {
                //where += " and d.status=" + status + "";
            }

            


            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and Org_Code like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(model))
            {
                //where += " and d.model like '%" + model + "%'";
            }

            if (!String.IsNullOrEmpty(lost_time_start))
            {
                //where += " and d.losttime >= '" + lost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(lost_time_end))
            {
                //where += " and d.losttime <= '" + lost_time_end + "'";
            }

            if (!String.IsNullOrEmpty(unlost_time_start))
            {
                //where += " and d.unlosttime >= '" + unlost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(unlost_time_end))
            {
                //where += " and d.unlosttime <= '" + unlost_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();

            string sql = @"with t as(
select uid,deviceid,begintime,lasttime,cast(datediff(mi,begintime,isnull(lasttime,getdate())) as numeric(12,3)) times from userusinglog where 1=1 ";
            if (!string.IsNullOrEmpty(lost_time_start))
            {
                sql += " and begintime >= '" + lost_time_start + " 0:00:00'";
            }
            if (!string.IsNullOrEmpty(lost_time_end))
            {
                sql += " and lasttime <= '" + lost_time_end + "'";//logouttime
            }
            sql += @"),u as
(select uid,deviceid,begintime,lasttime,cast(times /60 as numeric(12,2)) times from t),
v as(
select isnull(d.resource,'') clienttype,u.uid,u.deviceid,u.begintime,u.lasttime,u.times from u left join device d on u.deviceid=d.id
),w as(
select clienttype,uid,sum(times) times from v group by clienttype,uid
),x
as (
select usr.u_uid UserUid,usr.u_name User_Full_Name,usr.u_unitcode Org_Code,usr.u_unitname ORG_NAME, 
isnull((select times from w where w.uid=usr.u_uid and w.clienttype='phone/android'),0) as PhoneAndroid,
isnull((select times from w where w.uid=usr.u_uid and w.clienttype='pad/android'),0) as PadAndroid,
isnull((select times from w where w.uid=usr.u_uid and w.clienttype='phone/ios'),0) as PhoneiOS,
isnull((select times from w where w.uid=usr.u_uid and w.clienttype='pad/ios'),0) as PadiOS
 from " + maindbName + @".dbo.smc_user usr
)";


            if (!String.IsNullOrEmpty(uid))
            {
                where += " UserUid like '%" + uid + "%'";
            }
            string cte = sql;
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"x", "*", pageIndex + 1, pageSize, orderby, where, cte);


            return result;
        }
        public SelectPagnationExDictionary GetStatisticsByUnit(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {

            string where = "";

            if (status >= 0)
            {
                //where += " and d.status=" + status + "";
            }

            


            

            if (!String.IsNullOrEmpty(model))
            {
                //where += " and d.model like '%" + model + "%'";
            }

            if (!String.IsNullOrEmpty(lost_time_start))
            {
                //where += " and d.losttime >= '" + lost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(lost_time_end))
            {
                //where += " and d.losttime <= '" + lost_time_end + "'";
            }

            if (!String.IsNullOrEmpty(unlost_time_start))
            {
                //where += " and d.unlosttime >= '" + unlost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(unlost_time_end))
            {
                //where += " and d.unlosttime <= '" + unlost_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string statisticsDbName = DbSqlHelper.GetStatisticDBName();


            string column = @"UnitCode,isnull (UnitName, UnitCode) UnitName ,UserCount,PadAndroid,PadiOS,PCWindows,PhoneAndroid,PhoneiOS,UsageCount";

            StringBuilder sql = new StringBuilder();
            sql.Append(@"select UnitCode,(select unit_name from ");
            sql.Append(statisticsDbName);
            sql.Append(@"..[smc_unit] where unit_id =unitcode) UnitName,COUNT(distinct UserName) as UserCount,");
            sql.Append(@"sum(case Device when 'PAd/Android' then UsageCount else 0 end) PadAndroid,");
            sql.Append(@"sum(case Device when 'PAd/iOS' then UsageCount else 0 end) PadiOS,");
            sql.Append(@"sum(case Device when 'PC/Windows' then UsageCount else 0 end) PCWindows,");
            sql.Append(@"sum(case Device when 'Phone/Android' then UsageCount else 0 end) PhoneAndroid,");
            sql.Append(@"sum(case Device when 'Phone/iOS' then UsageCount else 0 end) PhoneiOS");
            sql.Append(",SUM(UsageCount) as UsageCount from (select * from ");
            sql.Append(statisticsDbName);

            sql.Append("..[UsageLogDaily] where 1=1");
            sql.Append(")a group by UnitCode");

            string with = "with t as (" + sql.ToString() + ") ";
            string tableName = "t";
            //string orderBy = "UnitName Asc";
            where += " UnitCode is not null";
            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and UnitCode = '" + u_unitcode + "'";
            }


//            string sql = @"with t as(
//select uid,deviceid,begintime,lasttime,cast(datediff(mi,begintime,isnull(lasttime,getdate())) as numeric(12,3)) times from userusinglog where 1=1 ";
//            if (!string.IsNullOrEmpty(lost_time_start))
//            {
//                sql += " and begintime >= '" + lost_time_start + " 0:00:00'";
//            }
//            if (!string.IsNullOrEmpty(lost_time_end))
//            {
//                sql += " and lasttime <= '" + lost_time_end + "'";//logouttime
//            }
//            sql += @"),u as
//(select uid,deviceid,begintime,lasttime,cast(times /60 as numeric(12,2)) times from t),
//v as(
//select isnull(d.resource,'') clienttype,u.uid,u.deviceid,u.begintime,u.lasttime,u.times from u left join device d on u.deviceid=d.id
//),w as(
//select clienttype,uid,sum(times) times from v group by clienttype,uid
//),x
//as (
//select usr.u_uid UserUid,usr.u_name User_Full_Name,usr.u_unitcode Org_Code,usr.u_unitname ORG_NAME, 
//isnull((select times from w where w.uid=usr.u_uid and w.clienttype='phone/android'),0) as PhoneAndroid,
//isnull((select times from w where w.uid=usr.u_uid and w.clienttype='pad/android'),0) as PadAndroid,
//isnull((select times from w where w.uid=usr.u_uid and w.clienttype='phone/ios'),0) as PhoneiOS,
//isnull((select times from w where w.uid=usr.u_uid and w.clienttype='pad/ios'),0) as PadiOS
// from " + maindbName + @".dbo.smc_user usr
//)";


            if (!String.IsNullOrEmpty(uid))
            {
                where += " UserUid like '%" + uid + "%'";
            }

            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(tableName, column, pageIndex + 1, pageSize, orderby, where, with);


            return result;
        }
        
        
        public SelectPagnationExDictionary GetUserDevice(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            
            string where = " and du.status=1 ";

            if (status >= 0)
            {
                where += " and d.status=" + status + "";
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

            if (!String.IsNullOrEmpty(lost_time_start))
            {
                where += " and d.losttime >= '" + lost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(lost_time_end))
            {
                where += " and d.losttime <= '" + lost_time_end + "'";
            }
            
            if (!String.IsNullOrEmpty(unlost_time_start))
            {
                where += " and d.unlosttime >= '" + unlost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(unlost_time_end))
            {
                where += " and d.unlosttime <= '" + unlost_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"device d 
join deviceuser du on d.id=du.deviceid 
join "+maindbName+".dbo.smc_user u on u.u_uid=du.uid ", "u.u_uid useruid,u.u_unitcode unitcode,u.u_unitname unitname,u.u_name username,du.deviceid,d.losttime,d.unlosttime,d.model,d.description,d.status", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
       
        
        
        
        
        public SelectPagnationExDictionary GetDeviceUnLock(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            
            string where = "";

            if (status >= 0)
            {
                where += " and d.status=" + status + "";
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

            if (!String.IsNullOrEmpty(lost_time_start))
            {
                where += " and d.locktime >= '" + lost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(lost_time_end))
            {
                where += " and d.locktime <= '" + lost_time_end + "'";
            }
            
            if (!String.IsNullOrEmpty(unlost_time_start))
            {
                where += " and d.unlocktime >= '" + unlost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(unlost_time_end))
            {
                where += " and d.unlocktime <= '" + unlost_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"device d 
join deviceuser du on d.id=du.deviceid 
join " + maindbName + ".dbo.smc_user u on u.u_uid=du.uid ", "u.u_uid useruid,u.u_unitcode unitcode,u.u_unitname unitname,u.u_name username,du.deviceid,d.locktime,d.unlocktime,d.model,d.description,d.status", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }

        /// <summary>
        /// 查询用户的设备
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<IDictionary<string, object>> GetUserDevices(string uid)
        {
            //string s = "select d.id,d.os,d.model,d.description,d.remark from device d join deviceuser du on du.deviceid=d.id
//where du.uid='liuyan'";

            SelectPagnationExDictionary result = this.SelectPaginationExDictionary("device d join deviceuser du on du.deviceid=d.id", "d.id,d.os+'-'+d.model model", 1, 200, "d.id", "du.uid='" + uid + "'", "");
            return result.Result;
        }

        public bool LostUserDevicePost(string deviceid)
        {
            try
            {
                Device d = this.Get(deviceid);
                if (d == null)
                    return false;
                d.Status = 2;
                Update(d);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
