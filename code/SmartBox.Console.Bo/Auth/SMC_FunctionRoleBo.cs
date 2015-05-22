//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_FunctionRoleBo.cs
// 
// 
// 
// 2014-03-05 04:11:21
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


namespace SmartBox.Console.Bo
{

    public class SMC_FunctionRoleBo
    {

        /// <summary>
        /// SMC_FunctionRoleBo
        /// </summary>
        public SMC_FunctionRoleBo()
        {
        }

        private SMC_FunctionRoleDao _SMC_FunctionRoleDao;
        protected SMC_FunctionRoleDao SMC_FunctionRoleDao
        {
            get
            {
                if (_SMC_FunctionRoleDao == null)
                {
                    _SMC_FunctionRoleDao = new SMC_FunctionRoleDao(AppConfig.statisticDBKey);
                }
                return _SMC_FunctionRoleDao;
            }
        }


        [Frame(false, false)]
        public virtual SMC_FunctionRole Get(int FR_ID)
        {
            return SMC_FunctionRoleDao.Get(FR_ID);
        }

        [Frame(false, false)]
        public virtual void Delete(SMC_FunctionRole entity)
        {
            try
            {
                SMC_FunctionRoleDao.Delete(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Delete失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual void Delete(int functionId, int roleId)
        {
            try
            {
                SMC_FunctionRoleDao.Delete(functionId, roleId);
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Delete失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_FunctionRole> entities)
        {

            try
            {
                SMC_FunctionRoleDao.DeleteList(entities);
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法DeleteList失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteByFID(int functionId)
        {
            try
            {
                SMC_FunctionRoleDao.DeleteByFID(functionId);
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法DeleteByFID失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool IsFuncAssigned(int roleId)
        {
            var rel = true;
            try
            {
                rel=SMC_FunctionRoleDao.IsFuncAssigned(roleId);
            }
            catch (DalException ex)
            {
                rel = false;
                throw new BOException("调用方法IsFuncAssigned失败", ex);
            }

            return rel;
        }

        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="userList"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool InsertOrUpdate(SMC_FunctionRole functionRole)
        {
            try
            {
                int i = 0;
                if (!SMC_FunctionRoleDao.Exists(functionRole.FR_ID) && !SMC_FunctionRoleDao.Exists(functionRole.FN_ID, functionRole.Role_ID))
                {
                    //i = SMC_FunctionRoleDao.Insert(functionRole);
                    SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                    if (autoDao.HasMaxID("SMC_FunctionRole"))
                    {
                        int max_id = autoDao.GetMaxID("SMC_FunctionRole");
                        functionRole.FR_ID = max_id + 1;
                    }
                    else
                    {
                        functionRole.FR_ID = 1;
                    }
                    i = SMC_FunctionRoleDao.Insert(functionRole);
                    if (true)
                    {
                        autoDao.UpdateMaxID("SMC_FunctionRole");
                    }
                }
                else
                {
                    i = SMC_FunctionRoleDao.Update(functionRole);
                }

                return true;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法InsertOrUpdate失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Insert(SMC_FunctionRole functionRole)
        {
            try
            {
                SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                if (autoDao.HasMaxID("SMC_FunctionRole"))
                {
                    int max_id = autoDao.GetMaxID("SMC_FunctionRole");
                    functionRole.FR_ID = max_id + 1;
                }
                else
                {
                    functionRole.FR_ID = 1;
                }
                int i = SMC_FunctionRoleDao.Insert(functionRole);
                if (i > 0)
                {
                    autoDao.UpdateMaxID("SMC_FunctionRole");
                }
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Insert失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Update(SMC_FunctionRole functionRole)
        {
            try
            {
                int i = SMC_FunctionRoleDao.Update(functionRole);
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }
    }
}
