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

    public class SystemConfigBO : SmartBox.Console.Bo.BaseBO<SystemConfig>
    {
        public SystemConfigBO(BaseDao<SystemConfig> dao)
            : base(dao)
        {
        }

        public SystemConfigBO()
        {
            base._dao = this.SystemConfigDao;
        }

        private SystemConfigDao _SystemConfigDao;
        protected SystemConfigDao SystemConfigDao
        {
            get
            {
                if (_SystemConfigDao == null)
                {
                    _SystemConfigDao = new SystemConfigDao(AppConfig.mainDbKey);
                }
                return _SystemConfigDao;
            }
        }

        //[Frame(false, false)]
        //public virtual SelectPagnationExDictionary GetDeviceRetryLock(string uid, string model, string u_unitcode, string u_lock_time_start, string u_lock_time_end, int deviceStatus, string orderby, int pageSize, int pageIndex)
        //{
        //    DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
        //    return dao.GetDeviceRetryLock(uid, model, u_unitcode, u_lock_time_start, u_lock_time_end, deviceStatus, orderby, pageSize, pageIndex);
        //}

        //[Frame(false, false)]
        //public virtual IList<SystemConfig> LoadSMC_PackageExtHistory()
        //{
        //    return SystemConfigDao.LoadSMC_PackageExtHistory();
        //}


        [Frame(false, false)]
        public virtual bool SaveSystemConfig(string key, string val, Hashtable result)
        {
            if (String.IsNullOrEmpty(key) || String.IsNullOrEmpty(val))
            {
                result["r"] = false;
                result["d"] = "key或者val为空,保存失败!";
            }
            else
            {
                List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                pars.Add(new KeyValuePair<string, object>("[key]", key));
                SystemConfig config = SystemConfigDao.Get(pars);
                if (config == null)
                {
                    config = new SystemConfig();
                    config.Key = key;
                    config.Value = val;
                    int i = SystemConfigDao.Insert(config);
                    result["r"] = true;
                    result["d"] = "服务配置保存成功!";
                }
                else
                {
                    config.Value = val;
                    SystemConfigDao.Update(config);
                    result["r"] = true;
                    result["d"] = "服务配置保存成功!";
                }
                SmartBox.Console.Service.ServiceReference1.ManagerServiceClient ms = new Service.ServiceReference1.ManagerServiceClient();
                ms.ResetRuntimeConfigs();
            }
            return (bool)result["r"];
        }

        [Frame(false, false)]
        public virtual void ResetConfigs()
        {
            try
            {
                SmartBox.Console.Service.ServiceReference1.ManagerServiceClient ms = new Service.ServiceReference1.ManagerServiceClient();
                ms.ResetRuntimeConfigs();
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }
        }
        
        [Frame(false, false)]
        public virtual void ResetClientVer()
        {
            try
            {
                SmartBox.Console.Service.ServiceReference1.ManagerServiceClient ms = new Service.ServiceReference1.ManagerServiceClient();
                ms.ResetClientVer();
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }
        }
    }
}