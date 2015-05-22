using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities.Search;
using System.Collections;
using Beyondbit.Framework.DataAccess.ObjectDAO;

namespace SmartBox.Console.Dao
{
    public class StyleHomeItemDao:BaseDao<StyleHomeItem>
    {
        public StyleHomeItemDao(string key)
            : base(key)
        { }

        public StyleHomeItem GetStyleHomeItem(int styleId, string appId)
        {
            string sql = "select * from StyleHomeItem where styleid=@styleid and app4aiid=@appid";
            Hashtable pars = new Hashtable();
            pars.Add("styleid", styleId);
            pars.Add("appid", appId);

            IList<StyleHomeItem> r = this.Query(sql, pars);
            if (r != null && r.Count > 0)
            {
                return r[0];
            }
            else
                return null;
        }
        
        public void DeleteStyleHomeItem(int styleId, string appId)
        {
            string sql = "delete from StyleHomeItem where styleid=@styleid and app4aiid=@appid";
            Hashtable pars = new Hashtable();
            pars.Add("styleid", styleId);
            pars.Add("appid", appId);

            base.ExecuteNonQuery(sql, pars);
        }

        public SplitPageResult<StyleHomeItem> QueryStyleHomeItemList(string styleId, string orderby, int pageSize, int pageIndex)
        {
            string mainDB = Common.DbSqlHelper.GetMainDBName();
            string where = "";
            if (!String.IsNullOrEmpty(styleId))
            {
                where += " and styleId='" + styleId + "'";
            }
            string sql = String.Format("select * from {0}.[dbo].StyleHomeItem where 1=1 {1}", mainDB, where);

            Hashtable pars = new Hashtable();
            //IList <PrivilegeUser>  r = this.QuerySql<PrivilegeUser>(sql, "*", "createtime desc,updatetime desc", pageSize, pageIndex, ref recordCount, pars);
            SplitPageResult<StyleHomeItem> re = this.QuerySplitPage<StyleHomeItem>(sql, "*", orderby, pageSize, pageIndex, pars);

            return re;
        }

        public bool ExistsStyleItem(int styleId, string appId)
        {
            string sql = "select count(styleid) from StyleHomeItem where styleid=@styleid and app4aiid=@appid";
            Hashtable pars = new Hashtable();
            pars.Add("styleid", styleId);
            pars.Add("appid", appId);
            object o = this.ExecuteScalar(sql, pars);
            if (o == null)
                return false;
            else
            {
                return Convert.ToInt32(o) > 0;
            }
        }
    }
}
