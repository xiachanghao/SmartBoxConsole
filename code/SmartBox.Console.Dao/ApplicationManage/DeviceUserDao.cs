using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao
{
    public class DeviceUserDao : BaseDao<DeviceUser>
    {
        public DeviceUserDao(string key)
            : base(key)
        { }

        public string GetDeviceOnlineUser(string deviceid)
        {
            string maindbName = DbSqlHelper.GetMainDBName();
            string sql = String.Format(@"select top 1 uid  from {0}..UserAuthLog where deviceid='{1}' and result=1
 order by logintime desc", maindbName, deviceid);

            object obj = this.ExecuteScalar(sql);
            if (obj == null || obj.ToString() == "")
                return "";
            else
                return obj.ToString();
        }
    }
}
