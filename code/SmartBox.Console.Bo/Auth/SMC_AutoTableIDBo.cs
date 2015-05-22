//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_AutoTableIDBo.cs
// 
// 
// 
// 2014-03-05 04:11:17
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

    public class SMC_AutoTableIDBo
    {

        /// <summary>
        /// ?????????
        /// </summary>
        public SMC_AutoTableIDBo()
        {
        }

        private SMC_AutoTableIDDao _SMC_AutoTableIDDao;
        protected SMC_AutoTableIDDao SMC_AutoTableIDDao
        {
            get
            {
                if (_SMC_AutoTableIDDao == null)
                {
                    _SMC_AutoTableIDDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                }
                return _SMC_AutoTableIDDao;
            }
        }

        [Frame(false, false)]
        public virtual int GetMaxId(string tableName, string columnName)
        {
            return SMC_AutoTableIDDao.GetMaxID(tableName, columnName);
        }

        [Frame(false, false)]
        public virtual SMC_AutoTableID Get(int at_id)
        {
            return SMC_AutoTableIDDao.Get(at_id);
        }

        [Frame(false, false)]
        public virtual int Save(SMC_AutoTableID entity)
        {
            if (SMC_AutoTableIDDao.Get(entity.at_id) == null)
                return SMC_AutoTableIDDao.Insert(entity);
            else
                return SMC_AutoTableIDDao.Update(entity);
        }

        [Frame(false, false)]
        public virtual void Delete(SMC_AutoTableID entity)
        {

            try
            {
                SMC_AutoTableIDDao.Delete(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_AutoTableID> entities)
        {

            try
            {
                SMC_AutoTableIDDao.DeleteList(entities);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }
    }
}
