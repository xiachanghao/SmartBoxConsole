using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao
{
    public class PrivilegeUserDao : BaseDao<PrivilegeUser>
    {
        public PrivilegeUserDao(string key)
            : base(key)
        { }

        public void RemoveAsyncPrivilege(string privilegeCode)
        {
            string sql = string.Format("Delete from PrivilegeUser where ID='{0}'", privilegeCode);
            base.ExecuteNonQuery(sql);
        }

        public bool ExitFk(string privilegeCode)
        {
            string sql = "select count(id) from PrivilegeUser where ID=" + privilegeCode ;
           
            object o = base.ExecuteScalar(sql);
            int cnt = Convert.ToInt32(o);
            return cnt > 0;
        }
    }
}
