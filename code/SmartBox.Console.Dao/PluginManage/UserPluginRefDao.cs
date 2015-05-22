using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Dao
{
    public class UserPluginRefDao:BaseDao<UserPluginRef>
    {
         /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">数据库链接字符串</param>
        public UserPluginRefDao(string key)
            : base(key)
        {

        }

        public void DeleteUPInfo(string plugincode)
        {
            string sql = "delete from UserPluginRef where plugincode='" + plugincode + "'";
            base.ExecuteNonQuery(sql);
        }
    }
}
