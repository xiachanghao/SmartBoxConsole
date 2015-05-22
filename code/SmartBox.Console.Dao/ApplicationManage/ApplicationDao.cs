using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Dao
{
    public class ApplicationDao : BaseDao<Application>
    {
        public ApplicationDao(string key)
            : base(key)
        {

        }

        public JsonFlexiGridData QueryApplicationList(PageView view, string unitcode)
        {
            string coloum = @"ID,Name,DisplayName,Description,Enable,EnableType,PrivilegeDisplayName,Categorys,CreateUid,CreateTime,UpdateUid,UpdateTime,Unit";
            string sql = @"select 
	                            app.*,
	                            privilege.Name PrivilegeName,
	                            privilege.DisplayName PrivilegeDisplayName,
	                            (select Name+',' from dbo.ApplicationCategory where ID in (select * from Split(app.CategoryIDs,',',1))  for xml path('')) as Categorys
                            from 
	                            dbo.Application app
	                            left join dbo.AppPrivilege privilege on app.PrivilegeID = privilege.ID";
            string parm = "";
            if (!String.IsNullOrEmpty(unitcode))
                parm = " and unit='" + unitcode + "'";

            return base.QueryDataForFlexGridByPager(coloum, string.Format("({0}) as temp", sql), view.OrderBy.ToString(), "ID", parm, view);
        }

        public IList<Application> QueryApplicationList()
        {
            string sql = "select * from dbo.Application";
            return base.Query(sql);
        }

        public bool HasApplication(string privilegeID)
        {
            string sql = string.Format("select * from Application where PrivilegeID='{0}'", privilegeID);
            return base.Query(sql).Count>0;
        }

        public SelectPagnationExDictionary GetPrivilegeManageList(string app, string unitcode, string orderby, int pageSize, int pageIndex)
        {
            string where = " 1=1 ";


            if (!String.IsNullOrEmpty(app))
            {
                where += " and app.DisplayName like '%" + app + "%'";
            }

            if (!String.IsNullOrEmpty(unitcode))
            {
                where += " and app.unit= '" + unitcode + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }

            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);


            SelectPagnationExDictionary ex = this.SelectPaginationExDictionary("[application] app left join [AppPrivilege] ap on app.PrivilegeID=ap.ID", "app.id,app.createtime created_time,app.Name,app.DisplayName,ap.BuaAppCode,ap.EnableSync,ap.SyncIntervalTime,ap.SyncLastTime,ap.id apid,ap.DisplayName PrivilegeName,ap.BuaPrivilegeCode", pageIndex + 1, pageSize, orderby, where, "");

            return ex;
        }
        public SelectPagnationExDictionary GetApplicationManageList(string app, string unitcode, string orderby, int pageSize, int pageIndex)
            {
                string where = " 1=1 ";


                if (!String.IsNullOrEmpty(app))
                {
                    where += " and Name like '%" + app + "%'";
                }

                if (!String.IsNullOrEmpty(unitcode))
                {
                    where += " and unit= '" + unitcode + "'";
                }

                if (!String.IsNullOrEmpty(orderby))
                {
                    //orderby = " order by " + orderby;
                }

                if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                    where = where.Substring(5);


                SelectPagnationExDictionary ex = this.SelectPaginationExDictionary("[application]", "*", pageIndex + 1, pageSize, orderby, where, "");

                return ex;
            }
    }
}
