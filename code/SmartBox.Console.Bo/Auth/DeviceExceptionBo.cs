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

    public class DeviceExceptionBo : SmartBox.Console.Bo.BaseBO<DeviceException>
    {
        public DeviceExceptionBo(BaseDao<DeviceException> dao) : base(dao)
        {
        }

        public DeviceExceptionBo()
        {
            base._dao = this.DeviceExceptionDao;
        }

        private DeviceExceptionDao _DeviceExceptionDao;
        protected DeviceExceptionDao DeviceExceptionDao
        {
            get
            {
                if (_DeviceExceptionDao == null)
                {
                    _DeviceExceptionDao = new DeviceExceptionDao(AppConfig.mainDbKey);
                }
                return _DeviceExceptionDao;
            }
        }


        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceEnableAuthorizationException(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            //DeviceExceptionDao dao = new DeviceExceptionDao(AppConfig.mainDbKey);
            return DeviceExceptionDao.GetDeviceEnableAuthorizationException(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceDisableAuthorizationException(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            //DeviceExceptionDao dao = new DeviceExceptionDao(AppConfig.mainDbKey);
            return DeviceExceptionDao.GetDeviceDisableAuthorizationException(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }

        //[Frame(false, false)]
        //public virtual bool ExistsByUID(string uid)
        //{
        //    return SMC_UserDao.ExistsByUID(uid);
        //}


        /// <summary>
        ///  删除设备启用例外
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool DeleteEnableDeviceException(string id, string checkUser)
        {
            //DeviceUserApplyDao dao = new DeviceUserApplyDao(AppConfig.mainDbKey);
            DeviceException de = _dao.Get(id);
            if (de != null)
            {
                _dao.Delete(de);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  删除设备启用例外
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool DeleteDisableDeviceException(string id, string checkUser)
        {
            //DeviceUserApplyDao dao = new DeviceUserApplyDao(AppConfig.mainDbKey);
            DeviceException de = _dao.Get(id);
            if (de != null)
            {
                _dao.Delete(de);
                return true;
            }
            else
            {
                return false;
            }
        }

        [Frame(false, false)]
        public virtual bool AddDeviceEnableException(string deviceid)
        {
            string[] dids = deviceid.Split(",".ToCharArray());
            foreach (string did in dids)
            {
                IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                KeyValuePair<string, object> p = new KeyValuePair<string, object>("useruid", did);
                pars.Add(p);
                KeyValuePair<string, object> p2 = new KeyValuePair<string, object>("type", 1);
                pars.Add(p2);

                DeviceException de = _dao.Get(pars);
                if (de == null)
                {
                    de = new DeviceException();
                    de.Type = 1;
                    de.UserUID = did;
                    try
                    {
                        _dao.Insert(de);
                    }
                    catch
                    {
                    }
                }
            }
            return true;
        }

        [Frame(false, false)]
        public virtual bool AddDeviceDisableException(string deviceid)
        {
            string[] dids = deviceid.Split(",".ToCharArray());
            foreach (string did in dids)
            {
                IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                KeyValuePair<string, object> p = new KeyValuePair<string, object>("useruid", did);
                pars.Add(p);
                KeyValuePair<string, object> p2 = new KeyValuePair<string, object>("type", 2);
                pars.Add(p2);

                DeviceException de = _dao.Get(pars);
                if (de == null)
                {
                    de = new DeviceException();
                    de.Type = 2;
                    de.UserUID = did;
                    try
                    {
                        _dao.Insert(de);
                    }
                    catch
                    {
                    }
                }
            }
            return true;
        }
    }
}
