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

    public class MonitorLinkmanBO : SmartBox.Console.Bo.BaseBO<Monitor_linkman>
    {
        public MonitorLinkmanBO(BaseDao<Monitor_linkman> dao)
            : base(dao)
        {
        }

        public MonitorLinkmanBO()
        {
            base._dao = this.Monitor_linkmanDao;
        }

        private Monitor_linkmanDao _Monitor_linkmanDao;
        protected Monitor_linkmanDao Monitor_linkmanDao
        {
            get
            {
                if (_Monitor_linkmanDao == null)
                {
                    _Monitor_linkmanDao = new Monitor_linkmanDao(AppConfig.statisticDBKey);
                }
                return _Monitor_linkmanDao;
            }
        }
    }
}