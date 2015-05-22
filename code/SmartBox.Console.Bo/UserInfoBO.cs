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

    public class UserInfoBO : SmartBox.Console.Bo.BaseBO<UserInfo>
    {
        public UserInfoBO(BaseDao<UserInfo> dao)
            : base(dao)
        {
        }

        public UserInfoBO()
        {
            base._dao = this.UserInfoDao;
        }

        private UserInfoDao _UserInfoDao;
        protected UserInfoDao UserInfoDao
        {
            get
            {
                if (_UserInfoDao == null)
                {
                    _UserInfoDao = new UserInfoDao(AppConfig.mainDbKey);
                }
                return _UserInfoDao;
            }
        }

        public virtual void LockUser(string uid)
        {
            IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
            pars.Add(new KeyValuePair<string, object>("UserUid", uid));
            UserInfo u = UserInfoDao.Get(pars);
            if (u != null)
            {
                u.Lock = true;
                u.LastLockTime = DateTime.Now;
                UserInfoDao.Update(u);
            }
        }

        public virtual void UnLockUser(string uid)
        {
            IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
            pars.Add(new KeyValuePair<string, object>("UserUid", uid));
            UserInfo u = UserInfoDao.Get(pars);
            if (u != null)
            {
                u.Lock = false;
                u.LastUnLockTime = DateTime.Now;
                UserInfoDao.Update(u);
            }
        }
    }
}