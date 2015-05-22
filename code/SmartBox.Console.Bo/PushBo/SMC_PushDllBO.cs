//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_PushDllBO.cs
// 
// 
// 
// 2014-03-05 04:11:44
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
using SmartBox.Console.Dao.PushManage;
using System.Web;

namespace SmartBox.Console.Bo
{

    public class SMC_PushDllBO
    {

        /// <summary>
        /// ?????????
        /// </summary>
        public SMC_PushDllBO()
        {
        }

        private SMC_PushDllDAO dao;
        protected SMC_PushDllDAO SMC_PushDllDAO
        {
            get
            {
                if (dao == null)
                {
                    dao = new SMC_PushDllDAO(AppConfig.statisticDBKey);
                }
                return dao;
            }
        }


        [Frame(false, false)]
        public virtual SMC_PushDll Get(int Role_ID)
        {
            return SMC_PushDllDAO.Get(Role_ID);
        }

        [Frame(false, false)]
        public virtual void Save(SMC_PushDll entity)
        {
            if (SMC_PushDllDAO.Get(entity.pd_id) == null)
                SMC_PushDllDAO.Insert(entity);
            else
                SMC_PushDllDAO.Update(entity);
        }

        //查询权限用户
        [Frame(false, false)]
        public virtual SplitPageResult<SMC_PushDll> QueryPushDllList(int pageSize, int pageIndex)
        {
            return SMC_PushDllDAO.QueryPushDLLList(pageSize, pageIndex);
        }

        /// <summary>
        /// 按Unit_ID查询单位的角色列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        //[Frame(false, false)]
        //public virtual JsonFlexiGridData QueryRolesByUnitID(PageView view, int Unit_ID)
        //{
        //    return SMC_RoleDao.QueryRolesByUnitID(view, Unit_ID);
        //}

        //[Frame(false, false)]
        //public virtual Hashtable QueryRolesTreeDataByUnit(int Unit_ID, int User_ID)
        //{
        //    try
        //    {
        //        SMC_FunctionRoleDao frDao = new SMC_FunctionRoleDao(AppConfig.statisticDBKey);

        //        IList<SMC_Functions> funcs = SMC_FunctionsDao.QueryFunctionsByUpperFNID(0);//SMC_FunctionsDao.QueryFunctionsByUnitID(Unit_ID, 0);
        //        Hashtable phash = new Hashtable();
        //        phash["id"] = 0;
        //        phash["name"] = "请选择权限";
        //        phash["open"] = true;
        //        phash["checked"] = false;
        //        List<Hashtable> objs = new List<Hashtable>();
        //        if (funcs != null && funcs.Count > 0)
        //            foreach (SMC_Functions fn in funcs)
        //            {
        //                Hashtable hash = new Hashtable();

        //                bool isFnInRole = frDao.IsFunctionInRole(fn.FN_ID, Role_ID);
        //                hash["id"] = fn.FN_ID;
        //                hash["name"] = fn.FN_Name;
        //                hash["open"] = true;
        //                hash["checked"] = isFnInRole;
        //                parseChildTreeData(hash, fn, frDao, Role_ID);
        //                objs.Add(hash);
        //            }
        //        phash["children"] = objs;
        //        return phash;
        //    }
        //    catch (DalException ex)
        //    {
        //        throw new BOException("QueryFunctionsByUnitID", ex);
        //    }
        //}

        /// <summary>
        /// 将角色赋给用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        //[Frame(false, false)]
        //public virtual bool SetRoleToUser(int role_id, int user_id)
        //{
        //    try
        //    {
        //        bool result = false;
        //        SMC_UserList user = SMC_UserListDao.Get(user_id);
        //        user.Role_ID = role_id;
        //        int i = SMC_UserListDao.Update(user);
        //        return result;
        //    }
        //    catch (DalException ex)
        //    {
        //        throw new BOException("调用方法SetRoleToUser失败", ex);
        //    }
        //}

        [Frame(false, false)]
        public virtual void Delete(SMC_PushDll entity)
        {

            try
            {
                SMC_PushDllDAO.Delete(entity);
                string path = HttpContext.Current.Server.MapPath("~/PushZipPacks/") + entity.pd_id + "\\";
                if (System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.Delete(path, true);
                }
            }
            catch (DalException ex)
            {
                throw new BOException("????????????", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_PushDll> entities)
        {

            try
            {
                SMC_PushDllDAO.DeleteList(entities);
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
        public virtual bool InsertOrUpdate(SMC_PushDll role)
        {
            try
            {
                int i = 0;
                if (SMC_PushDllDAO.Get(role.pd_id) == null)
                {
                    SMC_PushDllDAO.Insert(role);
                }
                else
                {
                    SMC_PushDllDAO.Update(role);
                }

                return true;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法InsertOrUpdate失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual void Insert(SMC_PushDll role)
        {
            try
            {
                int maxid = SMC_PushDllDAO.GetMaxID();
                role.pd_id = maxid + 1;
                SMC_PushDllDAO.Insert(role);
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Insert失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual void Update(SMC_PushDll role)
        {
            try
            {
                SMC_PushDllDAO.Update(role);
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }

        //[Frame(false, false)]
        //public virtual void InsertBatch(IList<SMC_Role> roles, int batchSize)
        //{
        //    try
        //    {
        //        SMC_RoleDao.InsertBatch(roles, batchSize);
        //    }
        //    catch (DalException ex)
        //    {
        //        throw new BOException("调用方法InsertBatch失败", ex);
        //    }
        //}

        public virtual bool ExistsXml(SMC_PushDll entity)
        {
            try
            {
                return SMC_PushDllDAO.ExistsXml(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法ExistsXml失败", ex);
            }
        }

        public virtual void CleanTrashPushDll()
        {
            try
            {
                IList <SMC_PushDll> dlls = SMC_PushDllDAO.GetTrashPushDll();
                if (dlls != null && dlls.Count > 0)
                {
                    foreach (SMC_PushDll dll in dlls)
                    {
                        string path = HttpContext.Current.Server.MapPath("~/PushZipPacks/") + dll.pd_id + "\\";

                        try
                        {
                            System.IO.Directory.Delete(path, true);
                        }
                        catch
                        {
                        }

                        try
                        {
                            SMC_PushDllDAO.Delete(dll);
                        }
                        catch
                        {
                        }
                    }
                }
                //SMC_PushDllDAO.CleanTrashPushDll();
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法ExistsXml失败", ex);
            }
        }
    }
}
