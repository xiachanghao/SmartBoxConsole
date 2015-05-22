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
    public class SMC_PackageFAQBO : BaseBO<SMC_PackageFAQ>
    {
        private SMC_PackageFAQDao _SMC_PackageFAQDao;
        protected SMC_PackageFAQDao SMC_PackageFAQDao
        {
            get
            {
                if (_SMC_PackageFAQDao == null)
                {
                    _SMC_PackageFAQDao = new SMC_PackageFAQDao(AppConfig.statisticDBKey);
                }
                return _SMC_PackageFAQDao;
            }
        }

        public SMC_PackageFAQBO()
        {
            base._dao = this.SMC_PackageFAQDao;
        }

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        //[Frame(false, false)]
        //public virtual bool InsertOrUpdate(SMC_PackageExt unit)
        //{
        //    try
        //    {
        //        int i = 0;
        //        if (SMC_PackageExtDao.Get(unit.pe_id) == null)
        //        {
        //            //i = SMC_UnitDao.Insert(unit);
        //            SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
        //            if (autoDao.HasMaxID("SMC_PackageExt"))
        //            {
        //                int max_id = autoDao.GetMaxID("SMC_PackageExt");
        //                unit.pe_id = max_id + 1;
        //            }
        //            else
        //            {
        //                unit.pe_id = 1;
        //            }
        //            i = SMC_PackageExtDao.Insert(unit);
        //            if (true)
        //            {
        //                autoDao.UpdateMaxID("SMC_PackageExt");
        //            }
        //        }
        //        else
        //        {
        //            i = SMC_PackageExtDao.Update(unit);
        //        }

        //        return true;
        //    }
        //    catch (DalException ex)
        //    {
        //        throw new BOException("调用方法InsertOrUpdate失败", ex);
        //    }
        //}
    }
}