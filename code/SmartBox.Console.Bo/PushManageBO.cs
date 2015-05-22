using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Biz.BO;
using SmartBox.Console.Common;
using Beyondbit.Framework.DataAccess;
using SmartBox.Console.Dao.PushManage;
using Beyondbit.Framework.Biz;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Bo
{
    public class PushManageBO : BaseBO
    {
        PushManageDAO dao = null;
        public PushManageBO()
        {
            dao = new PushManageDAO(AppConfig.statisticDBKey);
        }
        public virtual void DeletePushNotifications(string id)
        {
            NotificationReport entity = dao.Get(id);
            dao.Delete(entity);
            
        }
        public virtual JsonFlexiGridData QueryPushNotificationList(PageView view)
        {
            try
            {
                
                return dao.QueryPushNotificationList(view);
            }
            catch (DalException ex)
            {
                throw new BOException("查询推送日志出错", ex);
            }
        }
    }
}
