//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    SMC_BUAUserSyncToInsideDao.cs
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

    public class SMC_BUAUserSyncToOutsideDao : BaseDao<SMC_BUAUserSyncToOutside>
    {

        public SMC_BUAUserSyncToOutsideDao(string key)
            : base(key)
        {

        }

        public JsonFlexiGridData QueryBUAUserAsyncToOutsideResultList(PageView view, string sync_bat_no, string sync_time_start, string sync_time_end, string sync_status, string userName)
        {
            string columns = @"buso_id,user_uid,user_name,sync_bat_no,sync_time,sync_status,description";
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
            SelectPagnationEx r = base.SelectPaginationEx("SMC_BUAUserSyncToOutside", columns, view.PageIndex + 1, view.PageSize, sqlorder, sWhere, "");
            view.RecordCount = r.RecordCount;

            JsonFlexiGridData data = BaseDao<object>.ConvertJosnFlexGridData(r.Result.Tables[0], view, "buso_id");
            return data;
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

        public void DeleteList(List<int> buso_ids)
        {
            string ids = "";
            foreach (int i in buso_ids) {
                ids += i.ToString() + ",";
            }
            string sql = "delete from SMC_BUAUserSyncToOutside where buso_id in (" + ids.TrimEnd(",".ToCharArray()) + ")";
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

        //public int GetMaxId()
        //{
        //    string sql = "select max(buso_id) from SMC_BUAUserSyncToOutside";
        //    object o = this.ExecuteScalar(sql);
        //    int i = 0;
        //    if (o != null)
        //    {
        //        i = Convert.ToInt32(o);
        //    }
        //    return i;
        //}

        public int GetMaxBatNo()
        {
            string sql = "select isnull(max(sync_bat_no),0) bat_no from dbo.SMC_BUAUserSyncToOutside";

            object o = base.ExecuteScalar(sql);
            int cnt = Convert.ToInt32(o);
            return cnt;
        }

        public bool ExistsByCode(string buso_id)
        {
            string sql = "select count(busi_id) from SMC_BUAUserSyncToInside where buso_id=@buso_id";
            Hashtable pars = new Hashtable();
            pars.Add("buso_id", buso_id);

            object o = base.ExecuteScalar(sql, pars);
            int cnt = Convert.ToInt32(o);
            return cnt > 0;
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
    }
}
