using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities.Search;
using System.Collections;

namespace SmartBox.Console.Dao
{
    public class ManagerDao : BaseDao<Manager>
    {
        public ManagerDao(string key)
            : base(key)
        {

        }

        public void ChangeManagePassowrd(string userUid, string password)
        {
            base.ExecuteNonQuery(string.Format("update [Manager] set [UserPwd]='{1}' where [UserUid]='{0}'",userUid,password));
        }
    }
}
