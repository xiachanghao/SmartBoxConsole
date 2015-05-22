//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_FunctionsBo.cs
// 
// 
// 
// 2014-03-05 04:11:30
//
// 
// 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.Biz;
using Beyondbit.Framework.Biz.BO;
using Beyondbit.Framework.Core.InterceptorHandler;
using Beyondbit.Framework.DataAccess;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using SmartBox.Console.Dao;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections;


namespace SmartBox.Console.Bo
{

    public class SMC_FunctionsBo
    {

        /// <summary>
        /// ?????????
        /// </summary>
        public SMC_FunctionsBo()
        {
        }

        private SMC_FunctionsDao _SMC_FunctionsDao;
        protected SMC_FunctionsDao SMC_FunctionsDao
        {
            get
            {
                if (_SMC_FunctionsDao == null)
                {
                    _SMC_FunctionsDao = new SMC_FunctionsDao(AppConfig.statisticDBKey);
                }
                return _SMC_FunctionsDao;
            }
        }


        [Frame(false, false)]
        public virtual SMC_Functions Get(int FN_ID)
        {
            return SMC_FunctionsDao.Get(FN_ID);
        }

        [Frame(false, false)]
        public virtual IList<SMC_Functions> GetFunctionsByUpperId(int FN_ID)
        {
            return SMC_FunctionsDao.GetFunctionsByUpperId(FN_ID);
        }


        [Frame(false, false)]
        public virtual IList<SMC_Functions> GetAllFunctions()
        {
            return SMC_FunctionsDao.GetAllFunctions();
        }

        [Frame(false, false)]
        public virtual int Save(SMC_Functions entity)
        {
            if (SMC_FunctionsDao.Get(entity.FN_ID) == null)
                return SMC_FunctionsDao.Insert(entity);
            else
                return SMC_FunctionsDao.Update(entity);
        }

        /// <summary>
        /// 按Unit_ID查询单位的权限列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryFunctionsByUnitID(PageView view, int Unit_ID)
        {
            try
            {
                return SMC_FunctionsDao.QueryFunctionsByUnitID(view, Unit_ID);
            }
            catch (DalException ex)
            {
                throw new BOException("QueryFunctionsByUnitID", ex);
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryFunctionByUpperFNID(PageView view, int Upper_FN_ID)
        {
            return SMC_FunctionsDao.QueryFunctionByUpperFNID(view, Upper_FN_ID);
        }

        [Frame(false, false)]
        public virtual IList<SMC_Functions> QueryFunctionsListByUID(string UL_UID)
        {
            try
            {
                //SMC_FunctionsDao frDao = new SMC_FunctionsDao(AppConfig.statisticDBKey);
                IList<SMC_Functions> funcs = null;

                UserInfoDao udao = new UserInfoDao(AppConfig.statisticDBKey);
                bool isSystemManager = udao.IsSystemManager(UL_UID);
                if (isSystemManager)
                    funcs = SMC_FunctionsDao.QueryFunctionsListSys();
                else
                    funcs = SMC_FunctionsDao.QueryFunctionsListByUID(UL_UID);
                return funcs;
            }
            catch (DalException ex)
            {
                throw new BOException("QueryFunctionsByUnitID", ex);
            }
        }
        

        [Frame(false, false)]
        public virtual Hashtable QueryFunctionsTreeDataByUpperFNID(int UpperFNID)
        {
            try
            {
                SMC_FunctionRoleDao frDao = new SMC_FunctionRoleDao(AppConfig.statisticDBKey);
                IList<SMC_Functions> funcs = SMC_FunctionsDao.QueryFunctionsByUpperFNIDForSystemManager(0);
                Hashtable phash = new Hashtable();
                phash["id"] = 0;
                phash["name"] = "权限";
                phash["url"] = "/AuthManage/FunctionList?upper_fn_id=0";
                phash["target"] = "ifrm";
                phash["open"] = true;
                phash["checked"] = false;
                List<Hashtable> objs = new List<Hashtable>();
                if (funcs != null && funcs.Count > 0)
                    foreach (SMC_Functions fn in funcs)
                    {
                        Hashtable hash = new Hashtable();

                        hash["id"] = fn.FN_ID;
                        hash["name"] = fn.FN_Name;
                        hash["url"] = "/AuthManage/FunctionList?upper_fn_id=" + fn.FN_ID;
                        hash["target"] = "ifrm";
                        hash["open"] = true;
                        parseChildTreeData(hash, fn, frDao);
                        objs.Add(hash);
                    }
                phash["children"] = objs;
                return phash;
            }
            catch (DalException ex)
            {
                throw new BOException("QueryFunctionsByUnitID", ex);
            }
        }

        [Frame(false, false)]
        public virtual Hashtable QueryFunctionsTreeDataByUnit(string Unit_ID, int Role_ID)
        {
            try
            {
                SMC_FunctionRoleDao frDao = new SMC_FunctionRoleDao(AppConfig.statisticDBKey);

                IList<SMC_Functions> funcs = SMC_FunctionsDao.QueryFunctionsByUpperFNIDForUnitManager(0);//SMC_FunctionsDao.QueryFunctionsByUnitID(Unit_ID, 0);
                Hashtable phash = new Hashtable();
                phash["id"] = 0;
                phash["name"] = "请选择权限";
                phash["open"] = true;
                phash["checked"] = false;
                List<Hashtable> objs = new List<Hashtable>();
                if (funcs != null && funcs.Count > 0)
                foreach (SMC_Functions fn in funcs)
                {
                    Hashtable hash = new Hashtable();
                    
                    bool isFnInRole = frDao.IsFunctionInRole(fn.FN_ID, Role_ID);
                    hash["id"] = fn.FN_ID;
                    hash["name"] = fn.FN_Name;
                    hash["open"] = true;
                    hash["checked"] = isFnInRole;
                    parseChildTreeData(hash, fn, frDao, Role_ID);
                    objs.Add(hash);
                }
                phash["children"] = objs;
                return phash;
            }
            catch (DalException ex)
            {
                throw new BOException("QueryFunctionsByUnitID", ex);
            }
        }

        private void parseChildTreeData(Hashtable pHash, SMC_Functions parent_fncs, int Unit_ID, SMC_FunctionRoleDao frDao, int RoleId)
        {
            IList<SMC_Functions> funcs = SMC_FunctionsDao.QueryFunctionsByUnitID(Unit_ID, parent_fncs.FN_ID);
            if (funcs != null && funcs.Count > 0)
            {
                
                List<Hashtable> objs = new List<Hashtable>();
                foreach (SMC_Functions fn in funcs)
                {
                    Hashtable hash = new Hashtable();
                    hash["id"] = fn.FN_ID;
                    hash["name"] = fn.FN_Name;
                    hash["checked"] = frDao.IsFunctionInRole(fn.FN_ID, RoleId);
                    parseChildTreeData(hash, fn, Unit_ID, frDao, RoleId);
                    objs.Add(hash);
                }
                if (objs.Count > 0)
                {
                    pHash["children"] = objs;
                }
            }            
        }

        private void parseChildTreeData(Hashtable pHash, SMC_Functions parent_fncs, SMC_FunctionRoleDao frDao)
        {
            IList<SMC_Functions> funcs = SMC_FunctionsDao.QueryFunctionsByUpperFNIDForSystemManager(parent_fncs.FN_ID);
            if (funcs != null && funcs.Count > 0)
            {

                List<Hashtable> objs = new List<Hashtable>();
                foreach (SMC_Functions fn in funcs)
                {
                    Hashtable hash = new Hashtable();
                    hash["id"] = fn.FN_ID;
                    hash["name"] = fn.FN_Name;
                    //hash["url"] = "/AuthManage/FunctionList?upper_fn_id=" + fn.FN_ID;

                    if (SMC_FunctionsDao.HasChild(fn.FN_ID))
                    {
                        hash["url"] = "/AuthManage/FunctionList?upper_fn_id=" + fn.FN_ID;
                        hash["target"] = "ifrm";
                    }
                    else
                    {   //叶节点
                        hash["url"] = "/AuthManage/AddModifyFunction?upper_fn_id=" + parent_fncs.FN_ID + "&FN_ID=" + fn.FN_ID;
                        hash["target"] = "_top";                       
                    }
                    parseChildTreeData(hash, fn, frDao);
                    objs.Add(hash);
                }
                if (objs.Count > 0)
                {
                    pHash["children"] = objs;
                }
            }
        }

        private void parseChildTreeData(Hashtable pHash, SMC_Functions parent_fncs, SMC_FunctionRoleDao frDao, int RoleId)
        {
            IList<SMC_Functions> funcs = SMC_FunctionsDao.QueryFunctionsByUpperFNIDForUnitManager(parent_fncs.FN_ID);
            if (funcs != null && funcs.Count > 0)
            {

                List<Hashtable> objs = new List<Hashtable>();
                foreach (SMC_Functions fn in funcs)
                {
                    Hashtable hash = new Hashtable();
                    hash["id"] = fn.FN_ID;
                    hash["name"] = fn.FN_Name;
                    hash["checked"] = frDao.IsFunctionInRole(fn.FN_ID, RoleId);
                    parseChildTreeData(hash, fn, frDao, RoleId);
                    objs.Add(hash);
                }
                if (objs.Count > 0)
                {
                    pHash["children"] = objs;
                }
            }
        }

        /// <summary>
        /// 按角色查询权限
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryFunctionsByRoleID(PageView view, int Role_ID)
        {
            try
            {
                return SMC_FunctionsDao.QueryFunctionsByRoleID(view, Role_ID);
            }
            catch (DalException ex)
            {
                throw new BOException("QueryFunctionsByRoleID", ex);
            }
        }

        /// <summary>
        /// 按单位、角色查询权限
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Role_ID"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryFunctions(PageView view, int Role_ID, int Unit_ID)
        {
            try
            {
                return SMC_FunctionsDao.QueryFunctions(view, Role_ID, Unit_ID);
            }
            catch (DalException ex)
            {
                throw new BOException("QueryFunctions", ex);
            }
        }

        /// <summary>
        /// 按角色、单位、上级权限ID查询权限,Upper_FN_ID为0则查询顶级权限
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Role_ID"></param>
        /// <param name="Unit_ID"></param>
        /// <param name="Upper_FN_ID"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryFunctions(PageView view, int Role_ID, int Unit_ID, int Upper_FN_ID)
        {
            try
            {
                return SMC_FunctionsDao.QueryFunctions(view, Role_ID, Unit_ID, Upper_FN_ID);
            }
            catch (DalException ex)
            {
                throw new BOException("QueryFunctions", ex);
            }
        }

        [Frame(false, false)]
        public virtual void Delete(SMC_Functions entity)
        {
            try
            {
                SMC_FunctionsDao.Delete(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("Delete", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_Functions> entities)
        {

            try
            {
                SMC_FunctionsDao.DeleteList(entities);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }

        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="userList"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool InsertOrUpdate(SMC_Functions functions)
        {
            try
            {
                int i = 0;
                if (SMC_FunctionsDao.Get(functions.FN_ID) == null)
                {
                    //i = SMC_FunctionsDao.Insert(functions);
                    SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                    if (autoDao.HasMaxID("SMC_Functions"))
                    {
                        int max_id = autoDao.GetMaxID("SMC_Functions");
                        functions.FN_ID = max_id + 1;
                    }
                    else
                    {
                        functions.FN_ID = 1;
                    }
                    i = SMC_FunctionsDao.Insert(functions);
                    if (true)
                    {
                        autoDao.UpdateMaxID("SMC_Functions");
                    }
                }
                else
                {
                    i = SMC_FunctionsDao.Update(functions);
                }

                return true;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法InsertOrUpdate失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Insert(SMC_Functions function)
        {
            try
            {
                SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                if (autoDao.HasMaxID("SMC_Functions"))
                {
                    int max_id = autoDao.GetMaxID("SMC_Functions");
                    function.FN_ID = max_id + 1;
                }
                else
                {
                    function.FN_ID = 1;
                }
                int i = SMC_FunctionsDao.Insert(function);
                if (i > 0)
                {
                    autoDao.UpdateMaxID("SMC_Functions");
                }
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Insert失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Update(SMC_Functions function)
        {
            try
            {
                int i = SMC_FunctionsDao.Update(function);
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool ExistsByCode(string FN_Code)
        {
            try
            {
                bool i = SMC_FunctionsDao.ExistsByCode(FN_Code);
                return i;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }

        public virtual bool DragNodeAsChild(int childNodeId, int parentNodeId)
        {
            try
            {
                bool r = SMC_FunctionsDao.DragNodeAsChild(childNodeId, parentNodeId);
                return r;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法DragNodeAsChild失败", ex);
            }
        }
    }
}
