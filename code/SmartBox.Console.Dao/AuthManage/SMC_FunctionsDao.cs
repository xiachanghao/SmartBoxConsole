//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.SMC_Functions.cs
// ???????  
//
// ?????   2014-03-05 04:11:30
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

    public class SMC_FunctionsDao : BaseDao<SMC_Functions>
    {

        public SMC_FunctionsDao(string key)
            : base(key)
        {

        }

        /// <summary>
        /// 按Unit_ID查询单位的权限列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        public JsonFlexiGridData QueryFunctionsByUnitID(PageView view, int Unit_ID)
        {
            string column = @"*";

            string sWhere = "";
            if (Unit_ID > 0)
            {
                sWhere = string.Format(" and Unit_ID='{0}'", Unit_ID);
            }

            return base.QueryDataForFlexGridByPager(column, "SMC_Functions", view.OrderBy.ToString(), "FN_ID", sWhere, view);
        }

        /// <summary>
        /// 查询用户所拥有的权限
        /// </summary>
        /// <param name="UL_UID"></param>
        /// <returns></returns>
        public IList<SMC_Functions> QueryFunctionsListByUID(string UL_UID)
        {
            string sql = @"select * from smc_functions where FN_Disabled=0 and (fn_visibletype=0 or fn_visibletype=1) and fn_id in (
select distinct fn_id from smc_functionrole where role_id in (
select role_id from smc_userlist where rtrim(ltrim(ul_uid))=@uluid
)
) order by FN_Sequence";
            Hashtable pars = new Hashtable();
            pars.Add("uluid", UL_UID);

            return base.Query(sql, pars);
        }

        public IList<SMC_Functions> GetFunctionsByUpperId(int upperId)
        {
            string sql = @"select * from smc_functions where Upper_FN_ID = " + upperId.ToString() + " order by FN_Sequence";
           
            return base.Query(sql);
        }

        public IList<SMC_Functions> GetAllFunctions()
        {
            string sql = @"select * from smc_functions order by FN_Sequence";

            return base.Query(sql);
        }

        /// <summary>
        /// 获取系统管理员的权限
        /// </summary>
        /// <returns></returns>
        public IList<SMC_Functions> QueryFunctionsListSys()
        {
            string sql = @"select * from smc_functions where FN_Disabled=0 and (fn_visibletype=0 or fn_visibletype=2) order by FN_Sequence";

            return base.Query(sql);
        }

        public IList<SMC_Functions> QueryFunctionsByUnitID(int Unit_ID, int Upper_FN_ID)
        {
            return base.QuerySql("SELECT FN_ID,FN_NAME,UPPER_FN_ID,FN_URL,FN_SEQUENCE FROM SMC_FUNCTIONS WHERE FN_Disabled=0 and (fn_visibletype=0 or fn_visibletype=1) and Upper_FN_ID=" + Upper_FN_ID + " AND UNIT_ID=" + Unit_ID + " ORDER BY FN_SEQUENCE ");
        }

        public JsonFlexiGridData QueryFunctionByUpperFNID(PageView view, int Upper_FN_ID)
        {
            string column = " FN_ID,FN_NAME,FN_Code,FN_SEQUENCE,FN_URL,FN_Type,Unit_ID,FN_Img,FN_Path,FN_Demo,FN_IsDefault,FN_CreatedTime,FN_CreatedUser,FN_UpdateTime,FN_UpdateUser";
            //string column = @"*";
            string sWhere = "";
            if (Upper_FN_ID > -1)
            {
                sWhere = string.Format(" and Upper_FN_ID='{0}'", Upper_FN_ID);
            }

            return base.QueryDataForFlexGridByPager(column, "SMC_FUNCTIONS", view.OrderBy.ToString(), "FN_ID", sWhere, view);
        }

        public IList<SMC_Functions> QueryFunctionsByUpperFNIDForUnitManager(int Upper_FN_ID)
        {
            return base.QuerySql("SELECT FN_ID,FN_NAME,UPPER_FN_ID,FN_URL,FN_SEQUENCE FROM SMC_FUNCTIONS WHERE FN_Disabled=0 and (fn_visibletype=0 or fn_visibletype=1) and Upper_FN_ID=" + Upper_FN_ID + "  ORDER BY FN_SEQUENCE ");
        }
        
        public IList<SMC_Functions> QueryFunctionsByUpperFNIDForSystemManager(int Upper_FN_ID)
        {
            return base.QuerySql("SELECT FN_ID,FN_NAME,UPPER_FN_ID,FN_URL,FN_SEQUENCE FROM SMC_FUNCTIONS WHERE (fn_visibletype=0 or fn_visibletype=2) and Upper_FN_ID=" + Upper_FN_ID + "  ORDER BY FN_SEQUENCE ");
        }

        /// <summary>
        /// 按角色查询权限
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Role_ID"></param>
        /// <returns></returns>
        public JsonFlexiGridData QueryFunctionsByRoleID(PageView view, int Role_ID)
        {
            //select f.* from [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
            //on fr.FN_ID = f.FN_ID where fr.Role_ID = '' Orderby F.FN_ID DESC

            string column = @"f.*";

            string sWhere = "";
            if (Role_ID > 0)
            {
                sWhere = string.Format(" and fr.Role_ID = '{0}'", Role_ID);
            }

            return base.QueryDataForFlexGridByPager(column, @" [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
on fr.FN_ID = f.FN_ID ", view.OrderBy.ToString(), "F.FN_ID DESC", sWhere, view);
        }

        /// <summary>
        /// 按单位、角色查询权限
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Role_ID"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        public JsonFlexiGridData QueryFunctions(PageView view, int Role_ID, int Unit_ID)
        {
            //select f.* from [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
            //on fr.FN_ID = f.FN_ID where fr.Role_ID = '' and f.Unit_ID='' Orderby F.FN_ID DESC

            string column = @"f.*";

            string sWhere = "";
            if (Role_ID > 0)
            {
                sWhere = string.Format(" and fr.Role_ID = '{0}'", Role_ID);
            }

            if (Unit_ID > 0)
            {
                sWhere += string.Format(" and f.Unit_ID = '{0}'", Unit_ID);
            }

            return base.QueryDataForFlexGridByPager(column, @" [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
on fr.FN_ID = f.FN_ID ", view.OrderBy.ToString(), "F.FN_ID DESC", sWhere, view);
        }

        /// <summary>
        /// 按角色、单位、上级权限ID查询权限,Upper_FN_ID为0则查询顶级权限
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Role_ID"></param>
        /// <param name="Unit_ID"></param>
        /// <param name="Upper_FN_ID"></param>
        /// <returns></returns>
        public JsonFlexiGridData QueryFunctions(PageView view, int Role_ID, int Unit_ID, int Upper_FN_ID)
        {
            //select f.* from [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
            //on fr.FN_ID = f.FN_ID where fr.Role_ID = '' and f.Unit_ID='' and f.Upper_FN_ID = '' Orderby F.FN_ID DESC

            string column = @"f.*";

            string sWhere = "";
            if (Role_ID > 0)
            {
                sWhere = string.Format(" AND fr.Role_ID = '{0}'", Role_ID);
            }

            if (Unit_ID > 0)
            {
                sWhere += string.Format(" AND f.Unit_ID = '{0}'", Unit_ID);
            }

            if (Upper_FN_ID > 0)
            {
                sWhere += string.Format(" AND F.Upper_FN_ID = '{0}'", Upper_FN_ID);
            }
            else
            {
                sWhere += " AND F.Upper_FN_ID IS NULL ";
            }

            return base.QueryDataForFlexGridByPager(column, @" [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
on fr.FN_ID = f.FN_ID ", view.OrderBy.ToString(), "F.FN_ID DESC", sWhere, view);
        }

        public int GetMaxId()
        {
            string sql = "select isnull(max(fn_id), 0) from smc_functions";
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i;
        }

        public bool ExistsByCode(string FN_Code)
        {
            string sql = "select count(fn_id) from smc_functions where FN_Code=@code";
            Hashtable pars = new Hashtable();
            pars.Add("code", FN_Code);

            object o = base.ExecuteScalar(sql, pars);
            int cnt = Convert.ToInt32(o);
            return cnt > 0;
        }

        public bool DragNodeAsChild(int childNodeId, int parentNodeId)
        {
            string sql = "update smc_functions set upper_fn_id=@upper_fn_id where FN_id=@FN_id";
            Hashtable pars = new Hashtable();
            pars.Add("upper_fn_id", parentNodeId);
            pars.Add("FN_id", childNodeId);

            int o = base.ExecuteNonQuery(sql, pars);
            //int cnt = Convert.ToInt32(o);
            return o > 0;
        }

        public int GetParentId(int childNodeId)
        {
            string sql = "select upper_fn_id from  smc_functions where FN_id=@FN_id";
            Hashtable pars = new Hashtable();
            pars.Add("FN_id", childNodeId);

            object o = base.ExecuteScalar(sql, pars);
            int cnt = Convert.ToInt32(o);
            return cnt;
        }

        public bool HasChild(int FN_Id)
        {
            string sql = "select count(fn_id) from smc_functions where Upper_FN_ID=@code";
            Hashtable pars = new Hashtable();
            pars.Add("code", FN_Id);

            object o = base.ExecuteScalar(sql, pars);
            int cnt = Convert.ToInt32(o);
            return cnt > 0;
        }
    }
}
