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

    public class MonitorSendWarnBO : SmartBox.Console.Bo.BaseBO<monitor_sendwarn>
    {
        public MonitorSendWarnBO(BaseDao<monitor_sendwarn> dao)
            : base(dao)
        {
        }

        public MonitorSendWarnBO()
        {
            base._dao = this.Monitor_SendWarnDao;
        }

        private monitor_sendwarnDao _Monitor_SendWarnDao;
        protected monitor_sendwarnDao Monitor_SendWarnDao
        {
            get
            {
                if (_Monitor_SendWarnDao == null)
                {
                    _Monitor_SendWarnDao = new monitor_sendwarnDao(AppConfig.statisticDBKey);
                }
                return _Monitor_SendWarnDao;
            }
        }
    }
}