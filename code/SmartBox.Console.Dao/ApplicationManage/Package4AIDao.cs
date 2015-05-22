using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using System.Collections;

namespace SmartBox.Console.Dao
{
    public class Package4AIDao:BaseDao<Package4AI>
    {
        public Package4AIDao(string key)
            : base(key)
        { }

        public JsonFlexiGridData QueryPackage4AIList(PageView view)
        {
            string coloum = "ID,DisplayName,Type,DisplayClientType,Version,BuildVer,Description";
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select 
	                        package.*,
                            (select DisplayName+',' from ClientType where ClientType in (select * from dbo.Split(package.ClientType,'|',1)) for xml path('')) DisplayClientType
                        from 
	                        Package4AI package");
            return base.QueryDataForFlexGridByPager(coloum, string.Format("({0}) as temp",sql.ToString()), view.OrderBy.ToString(), "ID", string.Empty, view);
        }

        public JsonFlexiGridData QueryNeedImportedPackageList(PageView view)
        {
            string maindbName = DbSqlHelper.GetMainDBName();
            string tableName = maindbName + ".dbo.package4ai";
            string columns = "id,name,displayname,type,clienttype,version,buildver,downloaduri";
            string orderby = "id desc";
            string where = "id not in (select tableid from smc_packageext)";
            string with = "";

            SmartBox.Console.Common.SelectPagnationEx r = base.SelectPaginationEx(tableName, columns, view.PageIndex + 1, view.PageSize, orderby, where, with);
            JsonFlexiGridData result = BaseDao<object>.ConvertJosnFlexGridData(r.Result.Tables[0], view, "id");
            result.page = r.PageCount;
            result.total = r.RecordCount;
            return result;
        }

        public bool Exists(Package4AI package4AI)
        {
            string sql = "select count(id) from Package4AI where name=@name and type=@type and clienttype=@clienttype";
            Hashtable pars = new Hashtable();
            pars.Add("name", package4AI.Name);
            pars.Add("type", package4AI.Type);
            pars.Add("clienttype", package4AI.ClientType);

            object o = base.ExecuteScalar(sql, pars);
            int cnt = Convert.ToInt32(o);
            return cnt > 0;
        }

        public SelectPagnationExDictionary GetUnImportedAppPackages(string name, string displayName, string clientType, string lost_time_start, string lost_time_end, string type, string orderby, int pageSize, int pageIndex)
        {
            string where = " id not in (select tableid from {0}..smc_packageext) ";

            if (!String.IsNullOrEmpty(name))
            {
                where += " and [name] like '%" + name + "%'";
            }

            if (!String.IsNullOrEmpty(displayName))
            {
                where += " and [displayName] like '%" + displayName + "%'";
            }

            if (!String.IsNullOrEmpty(clientType))
            {
                where += " and [clientType] like '%" + clientType + "%'";
            }

            if (!String.IsNullOrEmpty(type))
            {
                where += " and [type] like '%" + type + "%'";
            }

            if (!String.IsNullOrEmpty(lost_time_start))
            {
                //where += " and d.locktime >= '" + lost_time_start + "'";
            }

            if (!String.IsNullOrEmpty(lost_time_end))
            {
                //where += " and d.locktime <= '" + lost_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }            

            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetMainDBName();
            string appDBName = DbSqlHelper.GetStatisticDBName();
            string tableName = maindbName + ".dbo.package4ai";
            where = String.Format(where, appDBName);
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(tableName, "id,name,displayname,type,clienttype,version,buildver,downloaduri", pageIndex + 1, pageSize, "id desc", where, "");


            return result;
        }
    }
}
