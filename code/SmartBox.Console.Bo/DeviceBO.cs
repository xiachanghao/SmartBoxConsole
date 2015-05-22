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

    public class DeviceBO : SmartBox.Console.Bo.BaseBO<Device>
    {
        public DeviceBO(BaseDao<Device> dao)
            : base(dao)
        {
        }

        public DeviceBO()
        {
            base._dao = this.DeviceDao;
        }

        private DeviceDao _DeviceDao;
        protected DeviceDao DeviceDao
        {
            get
            {
                if (_DeviceDao == null)
                {
                    _DeviceDao = new DeviceDao(AppConfig.mainDbKey);
                }
                return _DeviceDao;
            }
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceRetryLock(string uid, string model, string u_unitcode, string u_lock_time_start, string u_lock_time_end, int deviceStatus, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetDeviceRetryLock(uid, model, u_unitcode, u_lock_time_start, u_lock_time_end, deviceStatus, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual void LockDevice(string id, string hour, Hashtable r)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            Device d = dao.Get(id);
            d.LockTime = DateTime.Now;
            d.Status = 1;
            d.LockExpireHours = String.IsNullOrEmpty(hour) ? 0 : int.Parse(hour);
            dao.Update(d);
            r["r"] = true;
            SmartBox.Console.Service.ServiceReference1.ManagerServiceClient client = new SmartBox.Console.Service.ServiceReference1.ManagerServiceClient();

            try
            {
                DeviceUserDao duDao = new DeviceUserDao(AppConfig.mainDbKey);
                List<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
                pars.Add(new Tuple<string, string, object>("deviceid", "=", id));
                List<DeviceUser> deviceUsers = duDao.QueryList(pars);
                var rr = from e in deviceUsers select e.UID;

                string[] sessionids = rr.ToArray();
                client.ForceQuitUsers(sessionids);
            }
            catch (Exception e)
            {
                r["d"] += "强制用户退出失败，详细信息：" + e.Message;
            }
        }
    }
}