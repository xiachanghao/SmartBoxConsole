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

    public class SMC_UserExceptionBo : SmartBox.Console.Bo.BaseBO<SMC_UserException>
    {
        public SMC_UserExceptionBo(BaseDao<SMC_UserException> dao) : base(dao)
        {
        }

        public SMC_UserExceptionBo()
        {
            base._dao = this.SMC_UserExceptionDao;
        }

        private SMC_UserExceptionDao _SMC_UserExceptionDao;
        protected SMC_UserExceptionDao SMC_UserExceptionDao
        {
            get
            {
                if (_SMC_UserExceptionDao == null)
                {
                    _SMC_UserExceptionDao = new SMC_UserExceptionDao(AppConfig.statisticDBKey);
                }
                return _SMC_UserExceptionDao;
            }
        }

        
        //[Frame(false, false)]
        //public virtual bool ExistsByUID(string uid)
        //{
        //    return SMC_UserDao.ExistsByUID(uid);
        //}

        [Frame(false, false)]
        public virtual bool AddUserEnableException(string useruid)
        {

            if (useruid.IndexOf(",") != -1)
            {
                string[] uids = useruid.Split(",".ToCharArray());
                foreach (string uid in uids)
                {
                    IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                    KeyValuePair<string, object> p = new KeyValuePair<string, object>("uid", uid);
                    pars.Add(p);
                    p = new KeyValuePair<string, object>("type", 1);
                    pars.Add(p);

                    SMC_UserException de = _dao.Get(pars);
                    if (de == null)
                    {
                        de = new SMC_UserException();
                        de.Type = 1;
                        de.UID = uid;
                        try
                        {
                            _dao.Insert(de);
                        }
                        catch
                        {
                            //return false;
                        }
                        //return true;
                    }
                    //else
                        //return true;
                }
                return true;
            }
            else
            {
                IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                KeyValuePair<string, object> p = new KeyValuePair<string, object>("uid", useruid);
                pars.Add(p);
                p = new KeyValuePair<string, object>("type", 1);
                pars.Add(p);

                SMC_UserException de = _dao.Get(pars);
                if (de == null)
                {
                    de = new SMC_UserException();
                    de.Type = 1;
                    de.UID = useruid;
                    try
                    {
                        _dao.Insert(de);
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
                else
                    return true;
            }
        }

        [Frame(false, false)]
        public virtual bool AddUserDisableException(string useruid)
        {
            if (useruid.IndexOf(",") != -1)
            {
                string[] uids = useruid.Split(",".ToCharArray());
                foreach (string uid in uids)
                {
                    IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                    KeyValuePair<string, object> p = new KeyValuePair<string, object>("uid", uid);
                    pars.Add(p);
                    p = new KeyValuePair<string, object>("type", 2);
                    pars.Add(p);

                    SMC_UserException de = _dao.Get(pars);
                    if (de == null)
                    {
                        de = new SMC_UserException();
                        de.Type = 2;
                        de.UID = uid;
                        try
                        {
                            _dao.Insert(de);
                        }
                        catch
                        {
                            //return false;
                        }
                        //return true;
                    }
                }
                return true;
            }
            else
            {
                IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                KeyValuePair<string, object> p = new KeyValuePair<string, object>("uid", useruid);
                pars.Add(p);
                p = new KeyValuePair<string, object>("type", 2);
                pars.Add(p);

                SMC_UserException de = _dao.Get(pars);
                if (de == null)
                {
                    de = new SMC_UserException();
                    de.Type = 2;
                    de.UID = useruid;
                    try
                    {
                        _dao.Insert(de);
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
                else
                    return true;
            }
        }


        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetUserEnableAuthorizationException(string uid, string username, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            //DeviceExceptionDao dao = new DeviceExceptionDao(AppConfig.mainDbKey);
            return SMC_UserExceptionDao.GetUserEnableAuthorizationException(uid, username, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }
        
        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetUserDisableAuthorizationException(string uid, string username, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            //DeviceExceptionDao dao = new DeviceExceptionDao(AppConfig.mainDbKey);
            return SMC_UserExceptionDao.GetUserDisableAuthorizationException(uid, username, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }

        /// <summary>
        ///  删除用户启用例外
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool DeleteEnableUserException(string id, string checkUser)
        {
            //DeviceUserApplyDao dao = new DeviceUserApplyDao(AppConfig.mainDbKey);
            SMC_UserException de = _dao.Get(id);
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
        ///  删除用户禁用例外
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool DeleteDisableUserException(string id, string checkUser)
        {
            //DeviceUserApplyDao dao = new DeviceUserApplyDao(AppConfig.mainDbKey);
            SMC_UserException de = _dao.Get(id);
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
    }
}
