using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using System.Collections;
using SmartBox.Console.Common.Entities.Search;

namespace SmartBox.Console.Dao
{
    public class PluginDao : BaseDao<PluginInfo>
    {
               /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">数据库链接字符串</param>
        public PluginDao(string key)
            : base(key)
        {

        }

        #region 插件管理


        /// <summary>
        /// 获取插件列表
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public JsonFlexiGridData QueryPluginInfos(PageView view)
        {
            string Selectcolumns = "DisplayName,PluginCode,Version,VersionSummary,PDisplayName,CompanyName,CreateUid,CreateTime,CAST(VersionStatus as varchar) + ',' + CAST(IsUse as varchar) + ',' + PluginCode+','+ CAST(PluginCateCode as varchar) as VersionStatuss,versionId";
            string sqlorder;
            string table = @" (
                           select temp11.*,vts.versionsummary from (select ver.VersionStatus,ver.versionid, p.*,pc.DisplayName as PDisplayName 
 from  dbo.PluginInfo p inner join (   
   select v3.versionId, v3. PluginCode ,v3.VersionName ,0 as VersionStatus from                           
    (
      select isuse.PluginCode,v1.FilePath  from 
		 (
         select p.PluginCode , p.Version  from PluginInfo p inner join PluginInfoTemp pt on pt.PluginCode = p.PluginCode and p.IsUse =1
		 )isuse  inner join VersionTrack v1  on isuse.PluginCode = v1.PluginCode and v1.VersionStatus<>2
         and  exists ( select  PluginCode , Version from VersionTrack v2 where v2.PreVersionId = v1.VersionId )  
            )  v4 inner join VersionTrack v3   on v3.FilePath = v4.FilePath                              
      union 
      select vt.versionId, vt.PluginCode,vt.VersionName,vt.VersionStatus from 
          VersionTrack vt where not  exists (
				select v3.versionId, v3. PluginCode ,v3.VersionName ,0 as VersionStatus from                           
					(
					  select isuse.PluginCode,v1.FilePath  from 
						 (
						 select p.PluginCode , p.Version  from PluginInfo p inner join PluginInfoTemp pt on pt.PluginCode = p.PluginCode and p.IsUse =1
						 )isuse  inner join VersionTrack v1  on isuse.PluginCode = v1.PluginCode and v1.VersionStatus<>2
						 and  exists ( select  PluginCode , Version from VersionTrack v2 where v2.PreVersionId = v1.VersionId )  
							)  v4 inner join VersionTrack v3   on v3.FilePath = v4.FilePath
							and 	vt.PluginCode=v3.PluginCode
                            ) 
                            ) ver on ver.PluginCode = p.PluginCode and ver.VersionName = p.Version 
        inner join PluginCategory pc  on p.PluginCateCode = pc.PluginCateCode) as temp11,versiontrack as vts
          where temp11.versionid=vts.versionid and temp11.VersionStatus<>2
                             ) as temp ";

            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  CreateTime asc ";
            else
                sqlorder = view.OrderBy.ToString();
        
            return base.QueryDataForFlexGridByPager(Selectcolumns, table, sqlorder, "versionId", "", view);
        }

        public void DeleteInfo(SearchConfig search)
        {
            string sql = "delete from PluginInfo where 1=1 " + GetQueryCondition(search);

            base.ExecuteNonQuery(sql);
        }

        private string GetQueryCondition(SearchConfig search)
        {
            StringBuilder strsql = new StringBuilder();
            if (!string.IsNullOrEmpty(search.PluginCode))
            {
                strsql.Append(" and PluginCode='" + search.PluginCode+"'");
            }
            if (!string.IsNullOrEmpty(search.version))
            {
                strsql.Append(" and Version='" + search.version + "'");
            }
            return strsql.ToString();
        }



        #endregion














        public JsonFlexiGridData QueryPluginInfo(PageView view, SearchPlugin search)
        {
            string Selectcolumns = "DisplayName,CName,Version,PluginCode,TypeFullName,FileName,IsNeed,IsDefault,PluginUrl,CreateUid,CreateTime,PluginCode";
            string sqlorder;
            string table = "(select a.*,b.DisplayName as  CName from PluginInfo as a,PluginCategory as b where a.PluginCateCode = b.PluginCateCode ) as temp1";
            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  CreateTime asc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(Selectcolumns, table, sqlorder, "PluginCode", "", view);
        }

      

        private string GetQueryCondition(SearchPlugin search, IDictionary dic)
        {
            StringBuilder strsql = new StringBuilder();
            
            if (!string.IsNullOrEmpty(search.Pcode))
            {
                strsql.Append(" and PluginCode=@PluginCode");
                dic.Add("PluginCode", search.Pcode);
            }
            if (!string.IsNullOrEmpty(search.PluginCateCode))
            {
                strsql.Append(" and PluginCateCode=@PluginCateCode");
                dic.Add("PluginCateCode", search.PluginCateCode);
            }
            if (!string.IsNullOrEmpty(search.NotPluginCateCode))
            {
                string[] arr = search.NotPluginCateCode.Split(',');
                int i = 0;
                foreach (string a in arr)
                {
                    strsql.Append(" and PluginCateCode!=@PluginCateCode" + i.ToString());
                    dic.Add("PluginCateCode" + i.ToString(), a);
                    i++;
                }
            }
            return strsql.ToString();
        }

        public IList<PluginInfo> GetPluginInfoList(SearchPlugin search)
        {
            var dic = new Dictionary<string, string>();
            string Select = "select * from PluginInfo where 1=1 " + GetQueryCondition(search, dic);
            return Query(Select,dic);
        }

        public IList<PluginCategory> GetPluginCategoryInfo(SearchPlugin search)
        {
            IDictionary dic = new Hashtable();
            string sql = "select * from PluginCategory where 1=1 " + GetQueryCondition(search, dic);
            return QuerySql<PluginCategory>(sql, dic);
        }

  

    }
}
