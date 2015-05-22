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
using System.Configuration;


namespace SmartBox.Console.Bo
{

    public class DeviceUserApplyBO : SmartBox.Console.Bo.BaseBO<DeviceUserApply>
    {
        public DeviceUserApplyBO(BaseDao<DeviceUserApply> dao)
            : base(dao)
        {
        }

        public DeviceUserApplyBO()
        {
            base._dao = this.DeviceUserApplyDao;
        }

        private DeviceUserApplyDao _DeviceUserApplyDao;
        protected DeviceUserApplyDao DeviceUserApplyDao
        {
            get
            {
                if (_DeviceUserApplyDao == null)
                {
                    _DeviceUserApplyDao = new DeviceUserApplyDao(AppConfig.mainDbKey);
                }
                return _DeviceUserApplyDao;
            }
        }

        /// <summary>
        ///  拒绝通过审核设备
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool NotPassDevice(string deviceuserapplyid, string checkUser, GlobalParam parm, string refuse_msg)
        {
            DeviceUserApply dua = DeviceUserApplyDao.Get(deviceuserapplyid);
            SmartBox.Console.Reminder.Reminder reminder = new Reminder.Reminder();
            if (dua != null)
            {                
                bool disable_device_after_notpass_device = (parm.ConfigValue == "1");

                dua.CheckTime = DateTime.Now;
                dua.CheckUid = checkUser;
                dua.Status = disable_device_after_notpass_device ? 3 : 2;
                DeviceUserApplyDao.Update(dua);


                DeviceUserDao duDao = new DeviceUserDao(AppConfig.mainDbKey);
                List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                pars.Add(new KeyValuePair<string, object>("ID", dua.DeviceUserID));

                DeviceUser du = duDao.Get(pars);
                du.Status = 2;
                if (disable_device_after_notpass_device)
                {
                    du.NoUseReason = 1;
                }
                duDao.Update(du);

                try
                {
                    string content = "";
                    string send_refuse_msg_after_device_user_auth = ConfigurationManager.AppSettings["send_refuse_msg_after_device_user_auth"];
                    if (send_refuse_msg_after_device_user_auth != null && send_refuse_msg_after_device_user_auth.ToLower() == "true") {
                        content = ConfigurationManager.AppSettings["send_refuse_msg_after_device_user_auth_content"] + refuse_msg;
                    }

                    if (!String.IsNullOrEmpty(content))
                        reminder.RemindByMobile(du.UID, "", "设备审核未通过", content);
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Error(ex);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}