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
    public class UserInfoDao : BaseDao<UserInfo>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">数据库链接字符串</param>
        public UserInfoDao(string key)
            : base(key)
        {

        }

        public JsonFlexiGridData QueryUserInfoByPId(SearchConfig search, PageView view)
        {
            string Selectcolumns = "Status,UserUId,UserName,LastLoginIP,LastLoginTime,LastLogoutTime";
            string sqlorder;
            string table = " (select *  from  V_USER_INFO where 1=1  " + GetQueryCondition(search) + ") as temp ";
            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  LastLoginTime asc ";
            else
                sqlorder = view.OrderBy.ToString();
            return base.QueryDataForFlexGridByPager(Selectcolumns, table, sqlorder, "UserUId", "", view);
        }


        public JsonFlexiGridData QueryUserInfoNotByPId(SearchConfig search,SearchVersionTrack search1, PageView view)
        {
            string Selectcolumns = "UserUId,Gender,UserName";
            string sqlorder;
            string table = @" (select userinfo.* from userinfo left join (select b.*  from userpluginref as c ,userinfo as b 
                            where  c.useruid=b.useruid " + GetQueryCondition(search) + @") as d
                            on d.useruid=userinfo.useruid  where d.useruid is null " + GetQueryConditions(search1)+") as temp ";
            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  CreateTime asc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(Selectcolumns, table, sqlorder, "UserUId", "", view);
        }


        public IList<UserInfo> GetStuNameList(SearchVersionTrack search)
        {
            string sql = @"select *  from UserInfo  where 1=1 " + GetQueryConditions(search);

            return base.Query(sql);
        }


        #region 私有方法
        private string GetQueryCondition(SearchConfig search)
        {
            StringBuilder strsql = new StringBuilder();
            if (!string.IsNullOrEmpty(search.PluginCode))
            {
                strsql.Append(" and PluginCode='" + search.PluginCode+"'");
            }
            if (!string.IsNullOrEmpty(search.status))
            {
                strsql.Append(" and Status ='" + search.status + "'");
            }
            return strsql.ToString();
        }

        private string GetQueryConditions(SearchVersionTrack search)
        {
            StringBuilder strsql = new StringBuilder();
            if (!string.IsNullOrEmpty(search.UserUid))
            {
                strsql.Append(" and userinfo.UserUid='" + search.UserUid+"'");
            }
            if (!string.IsNullOrEmpty(search.UserName))
            {
                strsql.AppendFormat(" and ( UPPER(userinfo.UserName) like '{0}%' or userinfo.UserUid like '{0}%' ) ", search.UserName);
            }
            return strsql.ToString();
        }
        #endregion


        #region 验证

        public bool CheckUserName(string uid, string pwd)
        {
            IDictionary dic = new Hashtable();
            dic.Add("uname", uid);
            dic.Add("pwd", pwd);
            string maindbName = DbSqlHelper.GetMainDBName();
            string sql = "select count(*) from "+maindbName+".dbo.Manager where UserUid = @uname and UserPwd = @pwd";
            return Convert.ToBoolean(ExecuteScalar(sql,dic));
        }

        #endregion

        public bool IsSystemManager(string uid)
        {
            IDictionary dic = new Hashtable();
            dic.Add("uname", uid);
            string maindbName = DbSqlHelper.GetMainDBName();
            string sql = "select count(*) from "+maindbName+".dbo.Manager where UserUid = @uname";
            return Convert.ToBoolean(ExecuteScalar(sql, dic));
        }

    }
}
