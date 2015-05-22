using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities.Search;

namespace SmartBox.Console.Dao
{
    public class VersionTrackDao : BaseDao<VersionTrack>
    {
          /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">数据库链接字符串</param>
        public VersionTrackDao(string key)
            : base(key)
        {

        }



        #region

        public void DeleteInfo(SearchVersionTrack search)
        {
            string sql = "delete from VersionTrack where 1=1 " + GetQueryCondition(search);

            base.ExecuteNonQuery(sql);
        }


        public JsonFlexiGridData QueryMiansInfo(PageView view)
        {
            string Selectcolumns = " versionname as Version,VersionSummary,VersionStatus as VersionStatus1,CreateUid,CreateTime,cast(VersionStatus as varchar) + ',' + cast(C1 as varchar) as VersionStatus ,versionId";
            
            string sqlorder;
            string table = @" (
                            select a.*, case when VersionStatus=1 and b.c0 is  null then 1 else 0 end as C1  from versiontrack a
                                left  join (
                                    select count(*) as c0,plugincode  from versiontrack where (PluginCode='smartbox' and versionstatus=0) or (PluginCode='smartbox' and versionstatus=5) 
                                    group by plugincode
                                 ) b  on a.plugincode=b.plugincode  where a.PluginCode='smartbox') as temp1";

            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  CreateTime asc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(Selectcolumns, table, sqlorder, "versionId", "", view);
        }


        public JsonFlexiGridData QueryUpdaterInfo(PageView view)
        {
            string Selectcolumns = " versionname as Version,VersionSummary,VersionStatus as VersionStatus1,CreateUid,CreateTime,cast(VersionStatus as varchar) + ',' + cast(C1 as varchar) as VersionStatus ,versionId";
            string sqlorder;
            string table = @" (
                            select a.*, case when VersionStatus=1 and b.c0 is  null then 1 else 0 end as C1  from versiontrack a
                                left  join (
                                    select count(*) as c0,plugincode  from versiontrack where (PluginCode='Updater' and versionstatus=0) or (PluginCode='Updater' and versionstatus=5) 
                                    group by plugincode
                                 ) b  on a.plugincode=b.plugincode  where a.PluginCode='Updater') as temp1";
            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  CreateTime asc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(Selectcolumns, table, sqlorder, "versionId", "", view);
        }



        #endregion


        public IList<VersionTrack> GetVersionTrackList(SearchVersionTrack search)
        {
            var dic = new Dictionary<string, string>();
            string Select = "select * from VersionTrack where 1=1 " + GetQueryCondition(search, dic) +" order by PluginCode asc ";
            return Query(Select, dic);
        }

        public VersionTrack GetCurrentSmartBoxInfo()
        {
            VersionTrack ret = null;
            string sql = "select * from VersionTrack where PluginCode = 'SmartBox' and VersionStatus=1";
            var result = base.QuerySql(sql);
            if (result != null && result.Count > 0)
                ret = result.First();
            return ret;
        }


        private string GetQueryCondition(SearchVersionTrack search, IDictionary dic)
        {
            StringBuilder strsql = new StringBuilder();
            if (!string.IsNullOrEmpty(search.VID))
            {
                strsql.Append(" and VersionId=@VersionId");
                dic.Add("VersionId", search.VID);
            }
            if (!string.IsNullOrEmpty(search.PluginCode))
            {
                strsql.Append(" and PluginCode=@PluginCode");
                dic.Add("PluginCode", search.PluginCode);
            }
            if (!string.IsNullOrEmpty(search.VersionStatus))
            {
                strsql.Append(" and VersionStatus=@VersionStatus");
                dic.Add("VersionStatus", search.VersionStatus);
            }
            if (!string.IsNullOrEmpty(search.VersionName))
            {
                strsql.Append(" and VersionName=@VersionName");
                dic.Add("VersionName", search.VersionName);
            }
            if (!string.IsNullOrEmpty(search.filepath))
            {
                strsql.Append(" and FilePath=@FilePath");
                dic.Add("FilePath", search.filepath);
            }
            if (!string.IsNullOrEmpty(search.PreVersionId))
            {
                strsql.Append(" and PreVersionId=@PreVersionId");
                dic.Add("PreVersionId", search.PreVersionId);
            }

            return strsql.ToString();
        }

        private string GetQueryCondition(SearchVersionTrack search)
        {
            StringBuilder strsql = new StringBuilder();
            if (!string.IsNullOrEmpty(search.VID))
            {
                strsql.Append(" and VersionId=" + search.VID);
            }

            //排除多个不满足条件的pluginId
            if (!string.IsNullOrEmpty(search.NotPluginId))//排除主程序
            {
                strsql.Append(" and PluginCode !=" + search.NotPluginId);
            }
            if (!string.IsNullOrEmpty(search.NotPluginIdForCategory))//排除web插件
            {
                string[] arr = search.NotPluginIdForCategory.Split(',');
                foreach (string a in arr)
                {
                    strsql.Append(" and PluginCode !=" + a);
                }
            }
            
            //满足多个pluginId
            if (!string.IsNullOrEmpty(search.InPluginIdForCategory))//排除web插件
            {
                string[] arr = search.InPluginIdForCategory.Split(',');
                int i = 0;
                foreach (string a in arr)
                {
                    if (i == 0)
                        strsql.Append(" and PluginCode =" + a);
                    else
                        strsql.Append(" or PluginCode =" + a);
                    i++;
                }
            }

            if (!string.IsNullOrEmpty(search.PluginCode))
            {
                strsql.Append(" and PluginCode='" + search.PluginCode + "'");
            }
            if (!string.IsNullOrEmpty(search.VersionStatus))
            {
                strsql.Append(" and VersionStatus=" + search.VersionStatus);
            }
            if (!string.IsNullOrEmpty(search.VersionName))
            {
                strsql.Append(" and VersionName=" + search.VersionStatus);
            }

            return strsql.ToString();
        }

        public JsonFlexiGridData QueryVersionTrackInfo(PageView view, SearchVersionTrack search)
        {
            string Selectcolumns = "VersionName,VersionSummary,VersionStatus,CreateUid,CreateTime,cast(VersionStatus as varchar(2))  as VersionStatus1,VersionId";
            string sqlorder;
            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  CreateTime asc ";
            else
                sqlorder = view.OrderBy.ToString();
            var dic = new Dictionary<string, string>();
            string parm = GetQueryCondition(search);

            return base.QueryDataForFlexGridByPager(Selectcolumns, "VersionTrack", sqlorder, "VersionId", parm, view);
        }


        public JsonFlexiGridData QueryPluginVersionTrackInfo(PageView view, SearchVersionTrack search)
        {
            string Selectcolumns = "VersionName,displayName,FilePath,PreVersionId,VersionStatus,CreateUid,CreateTime,cast(c1 as varchar(2))+','+cast(VersionStatus as varchar(2)) as VersionStatus1,VersionId";
            string sqlorder;
            string table = @"(select temptb.*,p.displayName  from (select a.*, case when VersionStatus=1 and b.c0 is null then 1 else 0 end as C1  from versiontrack a
                            left  join (
                            select count(1) as c0, PluginCode from versiontrack
                            where VersionStatus=0
                            group by PluginCode) b on a.PluginCode=b.PluginCode  ) as temptb  left join pluginInfo as  p on temptb.PluginCode=p.PluginCode) as temp";
            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  CreateTime asc ";
            else
                sqlorder = view.OrderBy.ToString();
            var dic = new Dictionary<string, string>();
            string parm = GetQueryCondition(search);

            return base.QueryDataForFlexGridByPager(Selectcolumns, table, sqlorder, "VersionId", parm, view);
        }


        //获取正在使用的版本
        public IList<VersionTrack> GetPreVersionId(int VersionStatus, string PluginCode)
        {
            string sql = "select *  from VersionTrack where  VersionStatus =" + VersionStatus;
            return base.Query(sql);
        }
        //检查指定版本是否重名
        public IList<VersionTrack> CheckVersionName(string PluginCode, string VersionName)
        {
            string sql = "select * from VersionTrack where VersionName = '" + VersionName + "' and PluginCode ='" + PluginCode + "'";
            return Query(sql);
        }

        public IList<Application> GetApplications()
        {
            string sql = "select * from Application";
            IList<Application> re = this.QuerySql<Application>(sql, null);
            return re;
        }
    }
}
