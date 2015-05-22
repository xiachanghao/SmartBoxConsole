using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Core.Proxy;
using SmartBox.Console.Dao;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using Beyondbit.Framework.Core.InterceptorHandler;
using SmartBox.Console.Common.Entities.Search;
using Beyondbit.Framework.Biz;
using Beyondbit.Framework.DataAccess;
using System.IO;
using System.Collections;

namespace SmartBox.Console.Bo
{
    public class StatisticsBO
    {
        StatisticsDAO dll = null;
        public StatisticsBO()
        {
            dll = new StatisticsDAO("statisticDB");
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryShow(PageView view)
        {
            return dll.QueryShow(view);
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryShow(PageView view,string unitName)
        {
            return dll.QueryShow(view,unitName);
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryAppName(PageView view)
        {           
            return dll.QueryAppName(view);
        }

        [Frame(false, false)]
        public virtual List<string> GetAppList()
        {
            return dll.GetAppList();
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryUser(PageView view)
        {
            return dll.QueryUser(view);
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryTime(SearchStatisticOnlineTime view)
        {
            dll = new StatisticsDAO("mainDB");
            return dll.QueryTime3(view);
        }



       [Frame(false,false)]
        public List<Statist> GetList(string UnitName, DateTime? start, DateTime? end)
        {
            return dll.GetList(UnitName, start, end);
        }

        public List<Statist> GetUserList(string UserName, DateTime? start, DateTime? end)
        {
            return dll.GetUserList(UserName, start, end);
        }


        public List<Statist> GetAppNameList(string UserName, DateTime? start, DateTime? end)
        {
            return dll.GetAppNameList(UserName, start, end);
        }

        public List<Statist> GetTimeList(string UserName)
        {
            return dll.GetTimeList(UserName);
        }
    }
}
