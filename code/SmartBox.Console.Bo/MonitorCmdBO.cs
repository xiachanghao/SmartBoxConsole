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

    public class MonitorCmdBO : SmartBox.Console.Bo.BaseBO<monitor_cmd>
    {
        public MonitorCmdBO(BaseDao<monitor_cmd> dao)
            : base(dao)
        {
        }

        public MonitorCmdBO()
        {
            base._dao = this.Monitor_cmdDao;
        }

        private monitor_cmdDao _monitor_cmdDao;
        protected monitor_cmdDao Monitor_cmdDao
        {
            get
            {
                if (_monitor_cmdDao == null)
                {
                    _monitor_cmdDao = new monitor_cmdDao(AppConfig.statisticDBKey);
                }
                return _monitor_cmdDao;
            }
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetMonitorConfigList(string cmd_title, string cmd_senddate_start, string cmd_senddate_end, string cmd_executeresult, string cmd_code, string orderby, int pageSize, int pageIndex)
        {
            return Monitor_cmdDao.GetMonitorCmdList(cmd_title, cmd_senddate_start, cmd_senddate_end, cmd_executeresult, cmd_code, orderby, pageSize, pageIndex);
        }
    }
}