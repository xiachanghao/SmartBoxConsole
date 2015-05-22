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
    public class StyleBO
    {
        StyleDao dll = null;
        public StyleBO()
        {
            dll = new StyleDao("mainDB");
        }

        [Frame(false, false)]
        public virtual SelectPagnationEx QueryList(string orderby, string where, int pageIndex, int pageSize)
        {
            return dll.QueryList(orderby, where, pageIndex, pageSize);
        }

        [Frame(false, false)]
        public virtual SplitPageResult<Style> QueryList(string clientType, string code, string displayName, string orderby, int pageSize, int pageIndex)
        {
            return dll.QueryList(clientType, code, displayName, orderby, pageSize, pageIndex);
        }
        
        //[Frame(false, false)]
        //public virtual SplitPageResult<StyleHomeItem> QueryStyleHomeItemList(string styleId, string orderby, int pageSize, int pageIndex)
        //{
        //    StyleHomeItemDao dao = new StyleHomeItemDao(AppConfig.mainDbKey);
        //    return dao.QueryStyleHomeItemList(styleId, orderby, pageSize, pageIndex);
        //}

        [Frame(false, false)]
        public virtual bool DeleteStyleItem(int id)
        {
            return dll.DeleteStyleItem(id);
        }

        [Frame(false, false)]
        public virtual int StyleHomeCount(int styleid)
        {
            return dll.StyleHomeCount(styleid);
        }

        [Frame(false, false)]
        public virtual Style GetEntity(int styleid)
        {
            return dll.GetEntity(styleid);
        }
        
        //[Frame(false, false)]
        //public virtual StyleHomeItem GetStyleHomeItemEntity(int styleid, string App4AIID)
        //{
        //    StyleHomeItemDao dao = new StyleHomeItemDao(AppConfig.mainDbKey);
        //    StyleHomeItem result = dao.GetStyleHomeItem(styleid, App4AIID);
        //    return result;
        //}

        
    }
}
