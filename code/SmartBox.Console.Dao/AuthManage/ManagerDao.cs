using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Dao
{
    public class ManagerDao : BaseDao<ManagerInfo>
    {
        public ManagerDao(string key)
            : base(key)
        {

        }

        #region 验证

        public bool CheckUserName(string uid, string pwd)
        {
            IDictionary dic = new Hashtable();
            dic.Add("uname", uid);
            dic.Add("pwd", pwd);
            string sql = "select count(*) from Manager where UserUid = @uname and UserPwd = @pwd";
            return Convert.ToBoolean(ExecuteScalar(sql,dic));
        }

        #endregion

        public bool IsSystemManager(string uid)
        {
            IDictionary dic = new Hashtable();
            dic.Add("uname", uid);
            string sql = "select count(*) from Manager where UserUid = @uname";
            return Convert.ToBoolean(ExecuteScalar(sql, dic));
        }

        public void ChangeManagePassowrd(string userUid, string password)
        {
            base.ExecuteNonQuery(string.Format("update [Manager] set [UserPwd]='{1}' where [UserUid]='{0}'", userUid, password));
        }
    }
}
