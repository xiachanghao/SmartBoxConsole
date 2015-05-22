using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao
{
    public class ApplyDeviceBindDao : BaseDao<ApplyDeviceBind>
    {
        public ApplyDeviceBindDao(string key)
            : base(key)
        { }

        public JsonFlexiGridData QueryApplyDeviceBindList(PageView view)
        {
            string maindbName = DbSqlHelper.GetMainDBName();
            string column = "ID,UserUid,DeviceId,Ip,Status,ApplyTime,Description";
            string sql = "select * from " + maindbName + ".dbo.ApplyDeviceBind where status = 0 ";
            return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as temp", sql), view.OrderBy.ToString(), "ID", string.Empty, view);
        }

        public SelectPagnationExDictionary GetDeviceAuthorization(string uid, string deviceid, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            string where = "";

            if (deviceAuthStatus >= 0)
            {
                where += " and adb.status=" + deviceAuthStatus + "";
            }

            if (!String.IsNullOrEmpty(uid))
            {
                where += " and u_uid like '%" + uid + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u.u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and u_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(deviceid))
            {
                where += " and adb.deviceid like '%" + deviceid + "%'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_start))
            {
                where += " and adb.applytime >= '" + u_auth_submit_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_end))
            {
                where += " and adb.applytime <= '" + u_auth_submit_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);

            string maindbName = DbSqlHelper.GetMainDBName();

            SelectPagnationExDictionary result = this.SelectPaginationExDictionary("applydevicebind adb join "+maindbName+".dbo.smc_user u on adb.useruid=u.u_uid", "id,useruid,u.u_name username,u.u_unitname unitname,adb.status,u_unitcode unitcode,deviceid,description,applytime", pageIndex + 1, pageSize, orderby, where, "");
        

            return result;
        }
    }
}
