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

    public class ApplicationBo : BaseBO<Application>
    {

        /// <summary>
        /// ?????????
        /// </summary>
        public ApplicationBo()
        {
            base._dao = this.ApplicationDao;
        }



        private ApplicationDao _ApplicationDao;
        protected ApplicationDao ApplicationDao
        {
            get
            {
                if (_ApplicationDao == null)
                {
                    _ApplicationDao = new ApplicationDao(AppConfig.mainDbKey);
                }
                return _ApplicationDao;
            }
        }
    }
}