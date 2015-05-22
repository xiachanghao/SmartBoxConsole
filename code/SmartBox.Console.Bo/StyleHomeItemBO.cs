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
using Beyondbit.Framework.DataAccess.ObjectDAO;

namespace SmartBox.Console.Bo
{
    public class StyleHomeItemBO
    {
        StyleHomeItemDao dao = null;
        public StyleHomeItemBO()
        {
            dao = new StyleHomeItemDao("mainDB");
        }

        
        [Frame(false, false)]
        public virtual SplitPageResult<StyleHomeItem> QueryStyleHomeItemList(string styleId, string orderby, int pageSize, int pageIndex)
        {
            return dao.QueryStyleHomeItemList(styleId, orderby, pageSize, pageIndex);
        }
        
        [Frame(false, false)]
        public virtual StyleHomeItem GetStyleHomeItemEntity(int styleid, string App4AIID)
        {
            StyleHomeItemDao _dao = new StyleHomeItemDao(AppConfig.mainDbKey);
            StyleHomeItem result = _dao.GetStyleHomeItem(styleid, App4AIID);
            return result;
        }

        [Frame(false, false)]
        public virtual void DeleteStyleHomeItem(int StyieID, string App4AIID)
        {
            StyleHomeItemDao itemDao = new StyleHomeItemDao(AppConfig.mainDbKey);
            itemDao.DeleteStyleHomeItem(StyieID, App4AIID);
        }

        [Frame(false, false)]
        public virtual void Save(string StyieID, string App4AIID, string DisplayName, string ImageAddress, string Seq)
        {
            int styleId = Convert.ToInt32(StyieID);

            bool exists = dao.ExistsStyleItem(styleId, App4AIID);
            StyleHomeItem item = null;
            if (exists)
            {
                //item = dao.GetStyleHomeItem(styleId, App4AIID);
                StyleHomeItemDao itemDao = new StyleHomeItemDao(AppConfig.mainDbKey);
                itemDao.DeleteStyleHomeItem(styleId, App4AIID);
            }
            //else
            //{
                item = new StyleHomeItem();
                item.App4AIID = int.Parse(App4AIID);
                item.DispalyName = DisplayName;
                item.Image = ImageAddress;
                item.Seq = int.Parse(Seq);
                item.StyleID = styleId;
                dao.Insert(item);
            //}
        }
    }
}
