//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_UserListBo.cs
// 
// 
// 
// 2014-03-05 04:11:59
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
using Beyondbit.BUA.Client;


namespace SmartBox.Console.Bo
{

    public class SMC_PackageExtHistoryBO : SmartBox.Console.Bo.BaseBO<SMC_PackageExtHistory>
    {
        public SMC_PackageExtHistoryBO(BaseDao<SMC_PackageExtHistory> dao)
            : base(dao)
        {
        }

        public SMC_PackageExtHistoryBO()
        {
            base._dao = this.SMC_PackageExtHistoryDao;
        }

        private SMC_PackageExtHistoryDao _SMC_PackageExtHistoryDao;
        protected SMC_PackageExtHistoryDao SMC_PackageExtHistoryDao
        {
            get
            {
                if (_SMC_PackageExtHistoryDao == null)
                {
                    _SMC_PackageExtHistoryDao = new SMC_PackageExtHistoryDao(AppConfig.statisticDBKey);
                }
                return _SMC_PackageExtHistoryDao;
            }
        }

        //[Frame(false, false)]
        //public virtual SelectPagnationExDictionary GetDeviceRetryLock(string uid, string model, string u_unitcode, string u_lock_time_start, string u_lock_time_end, int deviceStatus, string orderby, int pageSize, int pageIndex)
        //{
        //    DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
        //    return dao.GetDeviceRetryLock(uid, model, u_unitcode, u_lock_time_start, u_lock_time_end, deviceStatus, orderby, pageSize, pageIndex);
        //}

        [Frame(false, false)]
        public virtual IList<SMC_PackageExtHistory> LoadSMC_PackageExtHistory()
        {
            return SMC_PackageExtHistoryDao.LoadSMC_PackageExtHistory();
        }
        
        [Frame(false, false)]
        public virtual SMC_PackageExtHistory GetSMC_PackageExtHistory(string key)
        {
            SMC_PackageExtHistory parm = SMC_PackageExtHistoryDao.Get(key);
            return parm;
        }

        public virtual bool SaveSMC_PackageExtHistory(List<SMC_PackageExtHistory> parms)
        {
            foreach (SMC_PackageExtHistory parm in parms)
            {
                if (!SMC_PackageExtHistoryDao.Exists(parm))
                    SMC_PackageExtHistoryDao.Insert(parm);
                else
                    SMC_PackageExtHistoryDao.Update(parm);
            }

            return true;
        }
    }
}