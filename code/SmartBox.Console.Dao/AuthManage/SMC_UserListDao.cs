//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.SMC_UserList.cs
// ???????  
//
// ?????   2014-03-05 04:11:59
//
// ?????
// ?????
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using SmartBox.Console.Common.Entities;
using System.Data;
using SmartBox.Console.Common.Entities.Search;
using SmartBox.Console.Common;
using System.Collections;


namespace SmartBox.Console.Dao
{

    public class SMC_UserListDao : BaseDao<SMC_UserList>
    {

        public SMC_UserListDao(string key)
            : base(key)
        {

        }

        /// <summary>
        /// 按Unit_ID查询单位的用户列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        public JsonFlexiGridData QueryUserListByUnitID(PageView view, string Unit_ID)
        {
            string column = @"UL_ID,UL_UID,UL_NAME,ROLE_ID,UL_MOBILEPHONE,UL_MAILADDRESS,UL_CREATEDTIME,UL_SEQUENCE,UL_Demo";

            string sWhere = "";
            if (!String.IsNullOrEmpty(Unit_ID))
            {
                sWhere = string.Format(" and Unit_ID='{0}'", Unit_ID);
            }

            return base.QueryDataForFlexGridByPager(column, "SMC_UserList", view.OrderBy.ToString(), "UL_ID", sWhere, view);
        }

        /// <summary>
        /// 判断管理员UID是否有权限FN_ID
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="FN_ID"></param>
        /// <returns></returns>
        public bool HasFunction(string UID, int FN_ID)
        {
            string sql = @"select count(fr_id) cnt from smc_functionrole where role_id in (
select role_id from smc_userlist where rtrim(ltrim(ul_uid))=@uid
) and fn_id=@fn_id";
            Hashtable pars = new Hashtable();
            pars.Add("uid", UID);
            pars.Add("fn_id", FN_ID);
            object o = base.ExecuteScalar(sql, pars);
            int cnt = Convert.ToInt32(o);
            return cnt > 0;
        }

        public virtual bool HasFunction(string UID, string functionCode)
        {
                string sql = @"select count(fr_id) cnt from smc_functionrole where role_id in (
select role_id from smc_userlist where rtrim(ltrim(ul_uid))=@uid
) and fn_id=(select fn_id from smc_functions where fn_code=@fncode)";
                Hashtable pars = new Hashtable();
                pars.Add("uid", UID);
                pars.Add("fncode", functionCode);
                object o = base.ExecuteScalar(sql, pars);
                int cnt = Convert.ToInt32(o);
                return cnt > 0;
        }

        public IList<SMC_UserList> QueryUserListByUnitID(string Unit_ID)
        {
            string sql = string.Format("SELECT * FROM SMC_UserList where Unit_ID=@unitid order by UL_SEQUENCE", Unit_ID);
            Hashtable pars = new Hashtable();
            pars.Add("unitid", Unit_ID);

            return base.Query(sql, pars);
        }

        public bool CheckUserName(string uid, string pwd)
        {
            IDictionary dic = new Hashtable();
            dic.Add("uname", uid);
            dic.Add("pwd", pwd);
            string sql = "select count(ul_id) from SMC_UserList where rtrim(ltrim(ul_uid)) = @uname and ul_pwd = @pwd";
            return Convert.ToBoolean(ExecuteScalar(sql, dic));
        }

        public bool IsUserListInRole(int UL_ID, int RoleId) {
            string sql = "SELECT COUNT(UL_ID) CNT FROM SMC_UserList WHERE UL_ID=@ULID AND Role_ID=@ROLEID";
            Hashtable pars = new Hashtable();
            pars.Add("ULID", UL_ID);
            pars.Add("ROLEID", RoleId);
            object o = base.ExecuteScalar(sql, pars);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i > 0;
        }

        public bool IsUserListInRole(string UL_UID, int RoleId)
        {
            string sql = "SELECT COUNT(UL_ID) CNT FROM SMC_UserList WHERE rtrim(ltrim(ul_uid))=@ULID AND Role_ID=@ROLEID";
            Hashtable pars = new Hashtable();
            pars.Add("ULID", UL_UID);
            pars.Add("ROLEID", RoleId);
            object o = base.ExecuteScalar(sql, pars);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i > 0;
        }

        /// <summary>
        /// 获取角色里的用户
        /// </summary>
        /// <param name="role_id"></param>
        /// <returns></returns>
        public JsonFlexiGridData QueryUserListHasRole(PageView view, int role_id)
        {
            string column = @"*";

            string sWhere = "";
            if (role_id > 0)
            {
                sWhere = string.Format(" and role_id='{0}'", role_id);
            }

            return base.QueryDataForFlexGridByPager(column, "SMC_UserList", view.OrderBy.ToString(), "UL_ID", sWhere, view);
        }

        /// <summary>
        /// 获取某单位属于某角色的用户
        /// </summary>
        /// <param name="role_id"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        public JsonFlexiGridData QueryUserListHasRole(PageView view, int role_id, string Unit_ID)
        {
            string column = @"*";

            string sWhere = "";
            if (!String.IsNullOrEmpty(Unit_ID))
            {
                sWhere = string.Format(" and Unit_ID='{0}'", Unit_ID);
            }

            if (role_id > 0)
            {
                sWhere = string.Format(" and role_id='{0}'", role_id);
            }

            return base.QueryDataForFlexGridByPager(column, "SMC_UserList", view.OrderBy.ToString(), "UL_ID", sWhere, view);
        }

        public int GetMaxId()
        {
            string sql = "select isnull(max(ul_id), 0) from smc_userlist";
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i;
        }

        public string GetUnitIdByUID(string ul_uid)
        {
            string sql = "select top 1 Unit_ID from smc_userlist where rtrim(ltrim(ul_uid))='" + ul_uid + "'";
            object o = this.ExecuteScalar(sql);
            string i = "";
            if (o != DBNull.Value)
            {
                i = Convert.ToString(o);
            }
            return i;
        }

        public bool DeleteByRoleID(int role_id)
        {
            string sql = String.Format("delete from smc_userlist where Role_ID={0}", role_id);
            object o = this.ExecuteNonQuery(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }

            return i > 0;
        }


        public bool ExistsByUID(string uid)
        {
            string sql = "select count(ul_id) from smc_userlist where ul_uid=@uid";
            Hashtable pars = new Hashtable();
            pars.Add("uid", uid);
            object o = this.ExecuteScalar(sql, pars);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i > 0;
        }
    }
}
