//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_PackageExtSyncToOutside.cs
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
    public class SMC_PackageExtSyncToOutsideBO : BaseBO
    {
        private SMC_PackageExtSyncToOutsideDao _SMC_PackageExtSyncToOutsideDao;
        protected SMC_PackageExtSyncToOutsideDao SMC_PackageExtSyncToOutsideDao
        {
            get
            {
                if (_SMC_PackageExtSyncToOutsideDao == null)
                {
                    _SMC_PackageExtSyncToOutsideDao = new SMC_PackageExtSyncToOutsideDao(AppConfig.statisticDBKey);
                }
                return _SMC_PackageExtSyncToOutsideDao;
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPackageAsyncResultList(PageView pageview, string sync_bat_no, string sync_time_start, string sync_time_end, string sync_status, string packageName)
        {
            try
            {
                return SMC_PackageExtSyncToOutsideDao.QueryPackageAsyncResultList(pageview, sync_bat_no, sync_time_start, sync_time_end, sync_status, packageName);
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
        public virtual bool InsertOrUpdate(SMC_PackageExtSyncToOutside unit)
        {
            try
            {
                int i = 0;
                SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                //int max_batno = 1;
                //if (autoDao.HasMaxID("sync_bat_no"))
                //{
                //    max_batno = autoDao.GetMaxID("sync_bat_no");
                //}

                if (SMC_PackageExtSyncToOutsideDao.Get(unit.peso_id) == null)
                {                   
                    
                    if (autoDao.HasMaxID("SMC_PackageExtSyncToOutside"))
                    {
                        int max_id = autoDao.GetMaxID("SMC_PackageExtSyncToOutside");
                        unit.pe_id = max_id + 1;
                    }
                    else
                    {
                        unit.pe_id = 1;
                    }
                    //unit.sync_bat_no = max_batno;
                    i = SMC_PackageExtSyncToOutsideDao.Insert(unit);
                    if (true)
                    {
                        autoDao.UpdateMaxID("SMC_PackageExtSyncToOutside");
                    }
                }
                else
                {
                    i = SMC_PackageExtSyncToOutsideDao.Update(unit);
                }

                return true;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法InsertOrUpdate失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Insert(SMC_PackageExtSyncToOutside unit)
        {
            try
            {
                SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                //获取batchNo
               
                //int bat_no = autoDao.GetMaxID("SMC_PackageExtSyncToOutside", "sync_bat_no");
                //bat_no++;                
                int maxid = this.SMC_PackageExtSyncToOutsideDao.GetMaxId("SMC_PackageExtSyncToOutside");
                //if (autoDao.HasMaxID("SMC_PackageExtSyncToOutside"))
                //{
                //    int max_id = autoDao.GetMaxID("SMC_PackageExtSyncToOutside");
                //    unit.peso_id = max_id + 1;
                //    //unit.sync_bat_no = bat_no;
                //}
                //else
                //{
                //    unit.peso_id = 1;
                //    //unit.sync_bat_no = bat_no;
                //}
                maxid = maxid + 1;
                unit.peso_id = maxid;
                int i = SMC_PackageExtSyncToOutsideDao.Insert(unit);

                autoDao.UpdateMaxID("SMC_PackageExtSyncToOutside");

                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Insert失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Update(SMC_PackageExtSyncToOutside unit)
        {
            try
            {
                int i = SMC_PackageExtSyncToOutsideDao.Update(unit);
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual SMC_PackageExtSyncToOutside Get(int Unit_ID)
        {
            return SMC_PackageExtSyncToOutsideDao.Get(Unit_ID);
        }

        public virtual IList<SMC_PackageExtSyncToOutside> GetByBatNo(int bat_no)
        {
            return SMC_PackageExtSyncToOutsideDao.GetByBatNo(bat_no);
        }

        [Frame(false, false)]
        public virtual void Delete(SMC_PackageExtSyncToOutside entity)
        {

            try
            {
                SMC_PackageExtSyncToOutsideDao.Delete(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_PackageExtSyncToOutside> entities)
        {
            try
            {
                SMC_PackageExtSyncToOutsideDao.DeleteList(entities);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }
        
        [Frame(false, false)]
        public virtual void DeleteList(List<int> peso_ids)
        {
            try
            {
                SMC_PackageExtSyncToOutsideDao.DeleteList(peso_ids);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool DeleteByPEID(string pe_id)
        {
            try
            {
                return SMC_PackageExtSyncToOutsideDao.DeleteByPEID(pe_id);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }
    }
}