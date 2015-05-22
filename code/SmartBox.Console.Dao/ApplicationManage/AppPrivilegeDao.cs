using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using System.Collections;
using Beyondbit.Framework.DataAccess.ObjectDAO;

namespace SmartBox.Console.Dao
{
    public class AppPrivilegeDao : BaseDao<AppPrivilege>
    {
        public AppPrivilegeDao(string key)
            : base(key)
        { }

        public IList<AppPrivilege> QueryAppPrivilegeList()
        {
            string sql = "select * from dbo.AppPrivilege";
            return base.Query(sql);
        }

        public JsonFlexiGridData QueryAppPrivilegeList(PageView view)
        {
            string column = "*";
            string sql = "select * from dbo.AppPrivilege";
            return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as temp", sql), view.OrderBy.ToString(), "ID", string.Empty,view);
        }

        public SplitPageResult<PrivilegeUser> QueryPrivilegeUser(string privilegeCode, int pageSize, int pageIndex)
        {
            string mainDB = Common.DbSqlHelper.GetMainDBName();
            string sql = String.Format("select * from {0}.[dbo].privilegeuser where id=@pcode", mainDB);

            Hashtable pars = new Hashtable();
            pars.Add("pcode", privilegeCode);
            //IList <PrivilegeUser>  r = this.QuerySql<PrivilegeUser>(sql, "*", "createtime desc,updatetime desc", pageSize, pageIndex, ref recordCount, pars);
            SplitPageResult<PrivilegeUser> re = this.QuerySplitPage<PrivilegeUser>(sql, "*", "createtime desc,updatetime desc", pageSize, pageIndex, pars);

            return re;
        }


        public bool Exists(string appid)
        {
            string sql = "select count(id) from [dbo].[AppPrivilege] where id=@id";
            Hashtable pars = new Hashtable();
            pars.Add("id", appid);

            object o = this.ExecuteScalar(sql, pars);
            int i = Convert.ToInt32(o);
            return i > 0;
        }
    }
}
