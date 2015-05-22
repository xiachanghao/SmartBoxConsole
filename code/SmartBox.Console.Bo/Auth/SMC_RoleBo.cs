//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_RoleBo.cs
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

namespace SmartBox.Console.Bo
{

    public class SMC_RoleBo
    {

        /// <summary>
        /// ?????????
        /// </summary>
        public SMC_RoleBo()
        {
        }

        private SMC_RoleDao _SMC_RoleDao;
        protected SMC_RoleDao SMC_RoleDao
        {
            get
            {
                if (_SMC_RoleDao == null)
                {
                    _SMC_RoleDao = new SMC_RoleDao(AppConfig.statisticDBKey);
                }
                return _SMC_RoleDao;
            }
        }

        private SMC_UserListDao _SMC_UserListDao;
        protected SMC_UserListDao SMC_UserListDao
        {
            get
            {
                if (_SMC_UserListDao == null)
                {
                    _SMC_UserListDao = new SMC_UserListDao(AppConfig.statisticDBKey);
                }
                return _SMC_UserListDao;
            }
        }


        [Frame(false, false)]
        public virtual SMC_Role Get(int Role_ID)
        {
            return SMC_RoleDao.Get(Role_ID);
        }

        [Frame(false, false)]
        public virtual int Save(SMC_Role entity)
        {
            if (SMC_RoleDao.Get(entity.Role_ID) == null)
                return SMC_RoleDao.Insert(entity);
            else
                return SMC_RoleDao.Update(entity);
        }

        /// <summary>
        /// 按Unit_ID查询单位的角色列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryRolesByUnitID(PageView view, string Unit_ID)
        {
            return SMC_RoleDao.QueryRolesByUnitID(view, Unit_ID);
        }

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
        [Frame(false, false)]
        public virtual bool SetRoleToUser(int role_id, int user_id)
        {
            try
            {
                bool result = false;
                SMC_UserList user = SMC_UserListDao.Get(user_id);
                user.Role_ID = role_id;
                int i = SMC_UserListDao.Update(user);
                return result;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法SetRoleToUser失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual void Delete(SMC_Role entity)
        {

            try
            {
                SMC_RoleDao.Delete(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_Role> entities)
        {

            try
            {
                SMC_RoleDao.DeleteList(entities);
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
        public virtual bool InsertOrUpdate(SMC_Role role)
        {
            try
            {
                int i = 0;
                if (SMC_RoleDao.Get(role.Role_ID) == null)
                {
                    //i = SMC_RoleDao.Insert(role);
                    SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                    if (autoDao.HasMaxID("SMC_Role"))
                    {
                        int max_id = autoDao.GetMaxID("SMC_Role");
                        role.Role_ID = max_id + 1;
                    }
                    else
                    {
                        role.Role_ID = 1;
                    }
                    i = SMC_RoleDao.Insert(role);
                    if (true)
                    {
                        autoDao.UpdateMaxID("SMC_Role");
                    }
                }
                else
                {
                    i = SMC_RoleDao.Update(role);
                }

                return true;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法InsertOrUpdate失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Insert(SMC_Role role)
        {
            try
            {
                SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                if (autoDao.HasMaxID("SMC_Role"))
                {
                    int max_id = autoDao.GetMaxID("SMC_Role");
                    role.Role_ID = max_id + 1;
                }
                else
                {
                    role.Role_ID = 1;
                }
                int i = SMC_RoleDao.Insert(role);
                if (i > 0)
                {
                    autoDao.UpdateMaxID("SMC_Role");
                }
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Insert失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Update(SMC_Role role)
        {
            try
            {
                int i = SMC_RoleDao.Update(role);
                return i > 0;
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
    }
}
