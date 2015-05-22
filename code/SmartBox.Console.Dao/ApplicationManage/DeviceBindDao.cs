using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using System.Collections;

namespace SmartBox.Console.Dao
{
    public class DeviceBindDao:BaseDao<DeviceBind>
    {
        public DeviceBindDao(string key)
            : base(key)
        { }

        public JsonFlexiGridData QueryDeviceBindList(PageView view)
        {
            string column = "Status as Operate,UserUid,DeviceId,Status,Description,ID";
            string sql = "select * from dbo.DeviceBind ";
            return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as temp", sql), view.OrderBy.ToString(), "ID", string.Empty, view);
        }

        public DeviceBind Get(string uid, string deviceId)
        {
            string sql = "select * from dbo.DeviceBind where useruid=@uid and deviceid=@did";
            Hashtable pars = new Hashtable();
            pars.Add("uid", uid);
            pars.Add("did", deviceId);
            IList<DeviceBind> re = this.QuerySql(sql, pars);
            if (re != null && re.Count > 0)
                return re[0];
            else
                return null;
        }

        public JsonFlexiGridData SearchDeviceBindList(PageView view, string UserId, string Description, string Status)
        {
            string column = "Status as Operate,UserUid,DeviceId,Status,Description,ID";
            //string sql = "select * from dbo.DeviceBind ";
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select * from dbo.DeviceBind where 1=1");

            if (!string.IsNullOrEmpty(UserId))
            {
                sql.Append(string.Format(" and UserUid='{0}'", UserId));
            }

            if (!string.IsNullOrEmpty(Status))
            {
                sql.Append(string.Format(" and Status='{0}'", Status));
            }

            if (!string.IsNullOrEmpty(Description))
            {
                sql.Append(string.Format(" and Description like'%{0}%'", Description));
            }

            return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as temp", sql.ToString()), view.OrderBy.ToString(), "ID", string.Empty, view);
        }
    }
}
