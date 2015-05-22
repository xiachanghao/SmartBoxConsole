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

    public class SMC_PackageExtSyncToOutsideDao : BaseDao<SMC_PackageExtSyncToOutside>
    {

        public SMC_PackageExtSyncToOutsideDao(string key)
            : base(key)
        {

        }

        public JsonFlexiGridData QueryPackageAsyncResultList(PageView view, string sync_bat_no, string sync_time_start, string sync_time_end, string sync_status, string packageName)
        {
            string columns = @"peso_id,pe_id,pe_name,sync_bat_no,sync_time,sync_status,description";
            string sqlorder;

            string sWhere = " 1=1 ";
            if (!String.IsNullOrEmpty(sync_bat_no))
            {
                sWhere += " and sync_bat_no=" + sync_bat_no;
            }

            if (!String.IsNullOrEmpty(sync_status))
            {
                sWhere += " and sync_status=" + sync_status;
            }

            if (!String.IsNullOrEmpty(packageName))
            {
                sWhere += " and pe_name like '%" + packageName + "%'";
            }

            if (!String.IsNullOrEmpty(sync_time_start))
            {
                sWhere += " and sync_time >= '" + sync_time_start + "'";
            }

            if (!String.IsNullOrEmpty(sync_time_end))
            {
                sWhere += " and sync_time <= '" + sync_time_end + "'";
            }

            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " sync_time desc  ";
            else
                sqlorder = view.OrderBy.ToString();
            SelectPagnationEx r = base.SelectPaginationEx("SMC_PackageExtSyncToOutside", columns, view.PageIndex + 1, view.PageSize, sqlorder, sWhere, "");
            view.RecordCount = r.RecordCount;

            JsonFlexiGridData data = BaseDao<object>.ConvertJosnFlexGridData(r.Result.Tables[0], view, "peso_id");
            return data;
            //return base.QueryDataForFlexGridByPager(columns, "SMC_PackageExtSyncToOutside", sqlorder, "peso_id", sWhere, view);
        }

        /// <summary>
        /// 按Unit_ID查询单位的权限列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        //public JsonFlexiGridData QueryFunctionsByUnitID(PageView view, int Unit_ID)
        //{
        //    string column = @"*";

        //    string sWhere = "";
        //    if (Unit_ID > 0)
        //    {
        //        sWhere = string.Format(" and Unit_ID='{0}'", Unit_ID);
        //    }

        //    return base.QueryDataForFlexGridByPager(column, "SMC_Functions", view.OrderBy.ToString(), "FN_ID", sWhere, view);
        //}

        public void DeleteList(List<int> peso_ids)
        {
            string ids = "";
            foreach (int i in peso_ids) {
                ids += i.ToString() + ",";
            }
            string sql = "delete from SMC_PackageExtSyncToOutside where peso_id in (" + ids.TrimEnd(",".ToCharArray()) + ")";
            base.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询用户所拥有的权限
        /// </summary>
        /// <param name="UL_UID"></param>
        /// <returns></returns>
//        public IList<SMC_Functions> QueryFunctionsListByUID(string UL_UID)
//        {
//            string sql = @"select * from smc_functions where fn_id in (
//select distinct fn_id from smc_functionrole where role_id in (
//select role_id from smc_userlist where ul_uid=@uluid
//)
//) order by FN_Sequence";
//            Hashtable pars = new Hashtable();
//            pars.Add("uluid", UL_UID);

//            return base.Query(sql, pars);
//        }

        //public IList<SMC_Functions> GetFunctionsByUpperId(int upperId)
        //{
        //    string sql = @"select * from smc_functions where Upper_FN_ID = " + upperId.ToString() + " order by FN_Sequence";
           
        //    return base.Query(sql);
        //}

        //public IList<SMC_Functions> GetAllFunctions()
        //{
        //    string sql = @"select * from smc_functions order by FN_Sequence";

        //    return base.Query(sql);
        //}

        /// <summary>
        /// 获取系统管理员的权限
        /// </summary>
        /// <returns></returns>
        //public IList<SMC_Functions> QueryFunctionsListSys()
        //{
        //    string sql = @"select * from smc_functions order by FN_Sequence";

        //    return base.Query(sql);
        //}

        //public IList<SMC_Functions> QueryFunctionsByUnitID(int Unit_ID, int Upper_FN_ID)
        //{
        //    return base.QuerySql("SELECT FN_ID,FN_NAME,UPPER_FN_ID,FN_URL,FN_SEQUENCE FROM SMC_FUNCTIONS WHERE Upper_FN_ID=" + Upper_FN_ID + " AND UNIT_ID=" + Unit_ID + " ORDER BY FN_SEQUENCE ");
        //}

        //public JsonFlexiGridData QueryFunctionByUpperFNID(PageView view, int Upper_FN_ID)
        //{
        //    string column = " FN_ID,FN_NAME,FN_Code,FN_SEQUENCE,FN_URL,FN_Type,Unit_ID,FN_Img,FN_Path,FN_Demo,FN_IsDefault,FN_CreatedTime,FN_CreatedUser,FN_UpdateTime,FN_UpdateUser";
        //    //string column = @"*";
        //    string sWhere = "";
        //    if (Upper_FN_ID > -1)
        //    {
        //        sWhere = string.Format(" and Upper_FN_ID='{0}'", Upper_FN_ID);
        //    }

        //    return base.QueryDataForFlexGridByPager(column, "SMC_FUNCTIONS", view.OrderBy.ToString(), "FN_ID", sWhere, view);
        //}

        //public IList<SMC_Functions> QueryFunctionsByUpperFNID(int Upper_FN_ID)
        //{
        //    return base.QuerySql("SELECT FN_ID,FN_NAME,UPPER_FN_ID,FN_URL,FN_SEQUENCE FROM SMC_FUNCTIONS WHERE Upper_FN_ID=" + Upper_FN_ID + "  ORDER BY FN_SEQUENCE ");
        //}

        /// <summary>
        /// 按角色查询权限
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Role_ID"></param>
        /// <returns></returns>
//        public JsonFlexiGridData QueryFunctionsByRoleID(PageView view, int Role_ID)
//        {
//            //select f.* from [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
//            //on fr.FN_ID = f.FN_ID where fr.Role_ID = '' Orderby F.FN_ID DESC

//            string column = @"f.*";

//            string sWhere = "";
//            if (Role_ID > 0)
//            {
//                sWhere = string.Format(" and fr.Role_ID = '{0}'", Role_ID);
//            }

//            return base.QueryDataForFlexGridByPager(column, @" [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
//on fr.FN_ID = f.FN_ID ", view.OrderBy.ToString(), "F.FN_ID DESC", sWhere, view);
//        }

        /// <summary>
        /// 按单位、角色查询权限
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Role_ID"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
//        public JsonFlexiGridData QueryFunctions(PageView view, int Role_ID, int Unit_ID)
//        {
//            //select f.* from [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
//            //on fr.FN_ID = f.FN_ID where fr.Role_ID = '' and f.Unit_ID='' Orderby F.FN_ID DESC

//            string column = @"f.*";

//            string sWhere = "";
//            if (Role_ID > 0)
//            {
//                sWhere = string.Format(" and fr.Role_ID = '{0}'", Role_ID);
//            }

//            if (Unit_ID > 0)
//            {
//                sWhere += string.Format(" and f.Unit_ID = '{0}'", Unit_ID);
//            }

//            return base.QueryDataForFlexGridByPager(column, @" [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
//on fr.FN_ID = f.FN_ID ", view.OrderBy.ToString(), "F.FN_ID DESC", sWhere, view);
//        }

        /// <summary>
        /// 按角色、单位、上级权限ID查询权限,Upper_FN_ID为0则查询顶级权限
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Role_ID"></param>
        /// <param name="Unit_ID"></param>
        /// <param name="Upper_FN_ID"></param>
        /// <returns></returns>
//        public JsonFlexiGridData QueryFunctions(PageView view, int Role_ID, int Unit_ID, int Upper_FN_ID)
//        {
//            //select f.* from [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
//            //on fr.FN_ID = f.FN_ID where fr.Role_ID = '' and f.Unit_ID='' and f.Upper_FN_ID = '' Orderby F.FN_ID DESC

//            string column = @"f.*";

//            string sWhere = "";
//            if (Role_ID > 0)
//            {
//                sWhere = string.Format(" AND fr.Role_ID = '{0}'", Role_ID);
//            }

//            if (Unit_ID > 0)
//            {
//                sWhere += string.Format(" AND f.Unit_ID = '{0}'", Unit_ID);
//            }

//            if (Upper_FN_ID > 0)
//            {
//                sWhere += string.Format(" AND F.Upper_FN_ID = '{0}'", Upper_FN_ID);
//            }
//            else
//            {
//                sWhere += " AND F.Upper_FN_ID IS NULL ";
//            }

//            return base.QueryDataForFlexGridByPager(column, @" [dbo].[SMC_FunctionRole] fr join [dbo].[SMC_Functions] f
//on fr.FN_ID = f.FN_ID ", view.OrderBy.ToString(), "F.FN_ID DESC", sWhere, view);
//        }

        public int GetMaxId()
        {
            string sql = "select isnull(max(peso_id), 0) peso_id from SMC_PackageExtSyncToOutside";
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i;
        }

        public bool ExistsByCode(string peso_id)
        {
            string sql = "select count(peso_id) from SMC_PackageExtSyncToOutside where peso_id=@peso_id";
            Hashtable pars = new Hashtable();
            pars.Add("peso_id", peso_id);

            object o = base.ExecuteScalar(sql, pars);
            int cnt = Convert.ToInt32(o);
            return cnt > 0;
        }

        public JsonFlexiGridData QueryBUAUserAsyncToInsideResultList(PageView view, string sync_bat_no, string sync_time_start, string sync_time_end, string sync_status, string userName)
        {
            string columns = @"peso_id,pe_id,pe_name,sync_bat_no,sync_time,sync_status,description";
            string sqlorder;

            string sWhere = " 1=1 ";
            if (!String.IsNullOrEmpty(sync_bat_no))
            {
                sWhere += " and sync_bat_no=" + sync_bat_no;
            }

            if (!String.IsNullOrEmpty(sync_status))
            {
                sWhere += " and sync_status=" + sync_status;
            }

            if (!String.IsNullOrEmpty(userName))
            {
                sWhere += " and user_name like '%" + userName + "%'";
            }

            if (!String.IsNullOrEmpty(sync_time_start))
            {
                sWhere += " and sync_time >= '" + sync_time_start + "'";
            }

            if (!String.IsNullOrEmpty(sync_time_end))
            {
                sWhere += " and sync_time <= '" + sync_time_end + "'";
            }

            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " sync_time desc  ";
            else
                sqlorder = view.OrderBy.ToString();
            SelectPagnationEx r = base.SelectPaginationEx("SMC_PackageExtSyncToOutside", columns, view.PageIndex + 1, view.PageSize, sqlorder, sWhere, "");
            view.RecordCount = r.RecordCount;

            JsonFlexiGridData data = BaseDao<object>.ConvertJosnFlexGridData(r.Result.Tables[0], view, "peso_id");
            return data;
            //return base.QueryDataForFlexGridByPager(columns, "SMC_PackageExtSyncToOutside", sqlorder, "peso_id", sWhere, view);
        }

        //public int GetParentId(int childNodeId)
        //{
        //    string sql = "select upper_fn_id from  smc_functions where FN_id=@FN_id";
        //    Hashtable pars = new Hashtable();
        //    pars.Add("FN_id", childNodeId);

        //    object o = base.ExecuteScalar(sql, pars);
        //    int cnt = Convert.ToInt32(o);
        //    return cnt;
        //}

        public IList<SMC_PackageExtSyncToOutside> GetByBatNo(int bat_no)
        {
            string sql = "select * from SMC_PackageExtSyncToOutside where sync_bat_no = " + bat_no.ToString();

            return base.Query(sql);
        }

        public bool DeleteByPEID(string pe_id)
        {
            string sql = "delete SMC_PackageExtSyncToOutside where pe_id = " + pe_id.ToString();
            int i = this.ExecuteNonQuery(sql);
            return i > 0;
        }
    }
}
