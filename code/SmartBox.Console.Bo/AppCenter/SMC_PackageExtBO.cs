//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_UnitBo.cs
// 
// 
// 
// 2014-03-05 04:11:53
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
    public class SMC_PackageExtBO : BaseBO<SMC_PackageExt>
    {
        private SMC_PackageExtDao _SMC_PackageExtDao;
        protected SMC_PackageExtDao SMC_PackageExtDao
        {
            get
            {
                if (_SMC_PackageExtDao == null)
                {
                    _SMC_PackageExtDao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
                }
                return _SMC_PackageExtDao;
            }
        }

        public SMC_PackageExtBO()
        {
            base._dao = this.SMC_PackageExtDao;
        }

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool InsertOrUpdate(SMC_PackageExt unit)
        {
            try
            {
                int i = 0;
                if (SMC_PackageExtDao.Get(unit.pe_id) == null)
                {
                    //i = SMC_UnitDao.Insert(unit);
                    SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                    if (autoDao.HasMaxID("SMC_PackageExt"))
                    {
                        int max_id = autoDao.GetMaxID("SMC_PackageExt");
                        unit.pe_id = max_id + 1;
                    }
                    else
                    {
                        unit.pe_id = 1;
                    }
                    i = SMC_PackageExtDao.Insert(unit);
                    if (true)
                    {
                        autoDao.UpdateMaxID("SMC_PackageExt");
                    }
                }
                else
                {
                    i = SMC_PackageExtDao.Update(unit);
                }

                return true;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法InsertOrUpdate失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Insert(SMC_PackageExt unit)
        {
            try
            {
                SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                int maxid = SMC_PackageExtDao.GetMaxId();
                //if (autoDao.HasMaxID("SMC_PackageExt"))
                //{
                //    int max_id = autoDao.GetMaxID("SMC_PackageExt");
                //    unit.pe_id = max_id + 1;
                //}
                //else
                //{
                //    unit.pe_id = 1;
                //}
                unit.pe_id = maxid + 1;
                int i = SMC_PackageExtDao.Insert(unit);

                //autoDao.UpdateMaxID("SMC_PackageExt");
                SMC_PackageExtDao.UpdateMaxId();

                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Insert失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Update(SMC_PackageExt unit)
        {
            try
            {
                int i = SMC_PackageExtDao.Update(unit);
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }

        /// <summary>
        /// 通过发布审核
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool PassAppPackage(string id, string checkUser, string checkUserName)
        {
            //DeviceUserApplyDao dao = new DeviceUserApplyDao(AppConfig.mainDbKey);
            SMC_PackageExt ext = SMC_PackageExtDao.Get(id);
            if (ext != null)
            {
                ext.pe_AuthStatus = 1;
                ext.pe_AuthTime = DateTime.Now;
                ext.pe_AuthMan = checkUser;
                ext.pe_AuthManUID = checkUserName;
                SMC_PackageExtDao.Update(ext);
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// 不通过发布审核
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool NotPassAppPackage(string id, string checkUser)
        {
            //DeviceUserApplyDao dao = new DeviceUserApplyDao(AppConfig.mainDbKey);
            SMC_PackageExt ext = SMC_PackageExtDao.Get(id);
            if (ext != null)
            {
                ext.pe_AuthStatus = 2;
                SMC_PackageExtDao.Update(ext);
                return true;
            }
            else
            {
                return false;
            }
        }





        [Frame(false, false)]
        public virtual void Delete(SMC_PackageExt entity)
        {

            try
            {
                SMC_PackageExtDao.Delete(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_PackageExt> entities)
        {

            try
            {
                SMC_PackageExtDao.DeleteList(entities);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }

        [Frame(false, false)]
        public virtual IList<SMC_PackageExt> GetNotP4I()
        {
            return SMC_PackageExtDao.GetNotP4I();
        }
    }
}