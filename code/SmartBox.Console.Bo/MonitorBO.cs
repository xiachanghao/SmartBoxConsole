using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Core.InterceptorHandler;
using SmartBox.Console.Common;
using SmartBox.Console.Dao;
using Beyondbit.Framework.DataAccess;
using Beyondbit.Framework.Biz;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Bo
{
    public class MonitorBO
    {
        private Monitor_ConfigDao _Monitor_ConfigDao;
        protected Monitor_ConfigDao MonitorConfigDao
        {
            get
            {
                if (_Monitor_ConfigDao == null)
                {
                    _Monitor_ConfigDao = new Monitor_ConfigDao(AppConfig.statisticDBKey);
                }
                return _Monitor_ConfigDao;
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryMonitorConfig(PageView view)
        {
            try
            {
                return MonitorConfigDao.QueryMonitorConfig(view);
            }
            catch (DalException ex)
            {
                throw new BOException("获取插件相关所有信息出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual Monitor_Config Get(int cfg_id)
        {
            return MonitorConfigDao.Get(cfg_id);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetMonitorConfigList(string hostname, string updatestatus, string isuse, string enalbe_time_start, string enalbe_time_end, string orderby, int pageSize, int pageIndex)
        {
            Monitor_ConfigDao dao = new Monitor_ConfigDao(AppConfig.mainDbKey);
            return dao.GetMonitorConfigList(hostname, updatestatus, isuse, enalbe_time_start, enalbe_time_end, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual int Save(Monitor_Config cfg)
        {   
            if (MonitorConfigDao.Get(cfg.cfg_id) == null)
                return MonitorConfigDao.Insert(cfg);
            else
                return MonitorConfigDao.Update(cfg);
        }

        [Frame(false, false)]
        public virtual void Delete(Monitor_Config cfg)
        {
            
            try
            {
                MonitorConfigDao.Delete(cfg);
            }
            catch (DalException ex)
            {
                throw new BOException("删除实体出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<Monitor_Config> entities)
        {

            try
            {
                MonitorConfigDao.DeleteList(entities);
            }
            catch (DalException ex)
            {
                throw new BOException("删除实体集合出错", ex);
            }
        }
    }
}
