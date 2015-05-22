using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common.Entities.Search;

namespace SmartBox.Console.Dao
{
    public class WebApplicationDao : BaseDao<WebApplication>
    {
        public WebApplicationDao(string key)
            : base(key)
        { }

        public JsonFlexiGridData QueryWebApplicationList(SearchWebApplication view)
        {
            string column = "ID,AppDisplayName,ShortName,DisplayClientType,Uri,IconUri,CreateUid,CreateTime,UpdateUid,UpdateTime,Seq,Unit";
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select
	                        web.*,
	                        app.Name AppName,
	                        app.DisplayName AppDisplayName,
                            (select DisplayName+',' from ClientType where ClientType in (select * from dbo.Split(web.ClientType,'|',1)) for xml path('')) DisplayClientType
                        from 
	                        WebApplication web
	                        left join Application app on app.ID=web.AppID 
                        where 1=1 ");
            if (!string.IsNullOrEmpty(view.ID))
            {
                sql.AppendFormat(" and web.id='{0}' ", view.ID);
            }
            if (!string.IsNullOrEmpty(view.AppID))
            {
                sql.AppendFormat(" and web.AppID='{0}' ", view.AppID);
            }
            if (!string.IsNullOrEmpty(view.ShortName))
            {
                sql.AppendFormat(" and web.ShortName like '%{0}%' ", view.ShortName);
            }
            return base.QueryDataForFlexGridByPager(column,string.Format("({0}) as temp",sql.ToString()),view.OrderBy.ToString(),"ID",string.Empty,view);
        }

        public IList<WebApplication> QueryWebApplicationListByAppID(string appID)
        {
            string sql = string.Format("select * from WebApplication where AppID='{0}'",appID);
            return base.Query(sql);
        }


        public bool HasWebApplication(string appID)
        {
            return QueryWebApplicationListByAppID(appID).Count > 0;
        }
    }
}
