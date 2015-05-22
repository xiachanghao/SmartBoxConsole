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

    public class GlobalParamBO : SmartBox.Console.Bo.BaseBO<GlobalParam>
    {
        public GlobalParamBO(BaseDao<GlobalParam> dao)
            : base(dao)
        {
        }

        public GlobalParamBO()
        {
            base._dao = this.GlobalParamDao;
        }

        private GlobalParamDao _GlobalParamDao;
        protected GlobalParamDao GlobalParamDao
        {
            get
            {
                if (_GlobalParamDao == null)
                {
                    _GlobalParamDao = new GlobalParamDao(AppConfig.statisticDBKey);
                }
                return _GlobalParamDao;
            }
        }

        //[Frame(false, false)]
        //public virtual SelectPagnationExDictionary GetDeviceRetryLock(string uid, string model, string u_unitcode, string u_lock_time_start, string u_lock_time_end, int deviceStatus, string orderby, int pageSize, int pageIndex)
        //{
        //    DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
        //    return dao.GetDeviceRetryLock(uid, model, u_unitcode, u_lock_time_start, u_lock_time_end, deviceStatus, orderby, pageSize, pageIndex);
        //}

        [Frame(false, false)]
        public virtual IList<GlobalParam> LoadGlobalParam()
        {
            return GlobalParamDao.LoadGlobalParam();
        }
        
        [Frame(false, false)]
        public virtual GlobalParam GetGlobalParam(string key)
        {
            GlobalParam parm =  GlobalParamDao.Get(key);
            return parm;
        }

        public virtual bool SaveGlobalParam(List<GlobalParam> parms) {
            foreach (GlobalParam parm in parms)
            {
                if (!GlobalParamDao.Exists(parm))
                    GlobalParamDao.Insert(parm);
                else
                    GlobalParamDao.Update(parm);
            }

            return true;
        }
    }
}