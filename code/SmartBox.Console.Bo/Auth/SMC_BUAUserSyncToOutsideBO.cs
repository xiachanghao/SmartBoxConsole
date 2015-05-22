//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_BUAUserSyncToOutsideBO.cs
// 
// 
// 
// 2014-06-06 14:02:53
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
using System.Collections;

namespace SmartBox.Console.Bo
{
    public class SMC_BUAUserSyncToOutsideBO : BaseBO
    {
        private SMC_BUAUserSyncToOutsideDao _SMC_BUAUserSyncToOutsideDao;
        protected SMC_BUAUserSyncToOutsideDao SMC_BUAUserSyncToOutsideDao
        {
            get
            {
                if (_SMC_BUAUserSyncToOutsideDao == null)
                {
                    _SMC_BUAUserSyncToOutsideDao = new SMC_BUAUserSyncToOutsideDao(AppConfig.statisticDBKey);
                }
                return _SMC_BUAUserSyncToOutsideDao;
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryBUAUserAsyncToOutsideResultList(PageView pageview, string sync_bat_no, string sync_time_start, string sync_time_end, string sync_status, string userName)
        {
            try
            {
                return SMC_BUAUserSyncToOutsideDao.QueryBUAUserAsyncToOutsideResultList(pageview, sync_bat_no, sync_time_start, sync_time_end, sync_status, userName);
            }
            catch (DalException ex)
            {
                throw new BOException("查询包的同步结果出错", ex);
            }
        }

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool InsertOrUpdate(SMC_BUAUserSyncToOutside unit)
        {
            try
            {
                int i = 0;
                if (SMC_BUAUserSyncToOutsideDao.Get(unit.buso_id) == null)
                {
                    //i = SMC_UnitDao.Insert(unit);
                    SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                    if (autoDao.HasMaxID("SMC_BUAUserSyncToOutside"))
                    {
                        int max_id = autoDao.GetMaxID("SMC_BUAUserSyncToOutside");
                        unit.buso_id = max_id + 1;
                    }
                    else
                    {
                        unit.buso_id = 1;
                    }
                    i = SMC_BUAUserSyncToOutsideDao.Insert(unit);
                    if (true)
                    {
                        autoDao.UpdateMaxID("SMC_BUAUserSyncToOutside");
                    }
                }
                else
                {
                    i = SMC_BUAUserSyncToOutsideDao.Update(unit);
                }

                return true;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法InsertOrUpdate失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Insert(SMC_BUAUserSyncToOutside unit)
        {
            try
            {
                SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                if (autoDao.HasMaxID("SMC_BUAUserSyncToOutside"))
                {
                    int max_id = autoDao.GetMaxID("SMC_BUAUserSyncToOutside");
                    unit.buso_id = max_id + 1;
                }
                else
                {
                    unit.buso_id = 1;
                }
                int i = SMC_BUAUserSyncToOutsideDao.Insert(unit);

                autoDao.UpdateMaxID("SMC_BUAUserSyncToOutside");

                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Insert失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Update(SMC_BUAUserSyncToOutside unit)
        {
            try
            {
                int i = SMC_BUAUserSyncToOutsideDao.Update(unit);
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual SMC_BUAUserSyncToOutside Get(int Unit_ID)
        {
            return SMC_BUAUserSyncToOutsideDao.Get(Unit_ID);
        }



        [Frame(false, false)]
        public virtual void Delete(SMC_BUAUserSyncToOutside entity)
        {

            try
            {
                SMC_BUAUserSyncToOutsideDao.Delete(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_BUAUserSyncToOutside> entities)
        {
            try
            {
                SMC_BUAUserSyncToOutsideDao.DeleteList(entities);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }
        
        [Frame(false, false)]
        public virtual void DeleteList(List<int> busi_ids)
        {
            try
            {
                SMC_BUAUserSyncToOutsideDao.DeleteList(busi_ids);
            }
            catch (DalException ex)
            {
                throw new BOException("DeleteList", ex);
            }
        }
    }
}