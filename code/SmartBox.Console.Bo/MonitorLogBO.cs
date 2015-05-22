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

    public class MonitorLogBO : SmartBox.Console.Bo.BaseBO<monitor_log>
    {
        public MonitorLogBO(BaseDao<monitor_log> dao)
            : base(dao)
        {
        }

        public MonitorLogBO()
        {
            base._dao = this.MonitorLogDao;
        }

        private monitor_logDao _MonitorLogDao;
        protected monitor_logDao MonitorLogDao
        {
            get
            {
                if (_MonitorLogDao == null)
                {
                    _MonitorLogDao = new monitor_logDao(AppConfig.statisticDBKey);
                }
                return _MonitorLogDao;
            }
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetMonitorLogList(string timeStart, string timeEnd, string log_status, string log_df_lever, string log_df_item, string orderby, int pageSize, int pageIndex)
        {
            return MonitorLogDao.GetMonitorLogList(timeStart, timeEnd, log_status, log_df_lever, log_df_item, orderby, pageSize, pageIndex);
        }
    }
}