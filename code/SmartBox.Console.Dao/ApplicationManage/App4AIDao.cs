using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities.Search;
using System.Collections;

namespace SmartBox.Console.Dao
{
    public class App4AIDao:BaseDao<App4AI>
    {
        #region 表查询SQL
        private const string APP4AITABLE = @"select 
	                                        ai.*,
	                                        app.Name AppName,
	                                        app.DisplayName AppDisplayName,
	                                        package.Name PackageName,
	                                        package.Version ver,
                                            (select DisplayName+',' from ClientType where ClientType in (select * from dbo.Split(ai.ClientType,'|',1)) for xml path('')) DisplayClientType
                                        from 
	                                        App4AI ai
	                                        left join Package4AI package on ai.Package4AIID=package.ID
	                                        left join Application app on ai.AppID=app.ID"; 
        #endregion

        public App4AIDao(string key)
            : base(key)
        { }

        public JsonFlexiGridData QueryApp4AIList(SearchApp4AI view)
        {
            string coloum = "ID,AppDisplayName,AppCode,DisplayClientType,PackageName,ver,IconUri,Seq";
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"{0} where 1=1 ",APP4AITABLE);
            if (!string.IsNullOrEmpty(view.ID))
            {
                sql.AppendFormat(" and ai.id='{0}' ", view.ID);
            }
            if (!string.IsNullOrEmpty(view.AppID))
            {
                sql.AppendFormat(" and ai.AppID='{0}' ", view.AppID);
            }
            if (!string.IsNullOrEmpty(view.AppCode))
            {
                sql.AppendFormat(" and ai.AppCode='{0}' ", view.AppCode);
            }
            if (!string.IsNullOrEmpty(view.ClientType))
            {
                sql.AppendFormat(" and ai.ClientType like '%{0}%' ", view.ClientType);
            }
            return base.QueryDataForFlexGridByPager(coloum, string.Format("({0}) as temp", sql), view.OrderBy.ToString(), "ID", string.Empty, view);
        }

        public IList<App4AI> QueryApp4AIListByPackageID(string packageID)
        {
            string sql = string.Format(@"select * from App4AI where Package4AIID='{0}'", packageID);
            return base.Query(sql);
        }

        public IList<App4AI> QueryApp4AIList(string clientType, string appId)
        {
            string sql = "select * from app4ai where appid=@appId and clienttype=@clientType";
            Hashtable pars = new Hashtable();
            pars.Add("appId", appId);
            pars.Add("clientType", clientType);
            return base.Query(sql, pars);
        }

        //public App4AI Get(string id)
        //{
        //    string sql = string.Format(@"{0} where ai.id='{1}'", APP4AITABLE,id);
        //    IList<App4AI> result = base.Query(sql);
        //    if (result.Count<=0)
        //    {
        //        throw new ArgumentOutOfRangeException("未找到指定的Application扩展");
        //    }
        //    return result[0];
        //}

        public bool HasApp4AI(string appID)
        {
            string sql = string.Format("select * from App4AI where AppID='{0}'", appID);
            return base.Query(sql).Count > 0;
        }

        public App4AI GetApp4AI(string package4aiid)
        {
            string sql = "select * from App4AI where package4aiid=@package4aiid";
            Hashtable pars = new Hashtable();
            pars.Add("package4aiid", package4aiid);
            IList<App4AI> r = base.QuerySql(sql, pars);
            if (r != null && r.Count > 0)
            {
                return r[0];
            }
            else
                return null;
        }
    }
}
