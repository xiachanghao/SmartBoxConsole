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
    public class StyleDao:BaseDao<Style>
    {
        public StyleDao(string key)
            : base(key)
        { }

        public SelectPagnationEx QueryList(string orderby, string where, int pageIndex, int pageSize)
        {
            Type tp = this.GetType();
            SelectPagnationEx r = base.SelectPaginationEx(tp.Name.Replace("Dao", ""), "*", pageIndex, pageSize, orderby, where, "");
            return r;
            //Hashtable pars = new Hashtable();
            //return this.QuerySql("select * from [style]", "*", "id desc", pageSize, pageIndex, ref recordCount, pars);            
        }

        public SplitPageResult<Style> QueryList(string clientType, string code, string displayName, string orderby, int pageSize, int pageIndex)
        {
            string mainDB = Common.DbSqlHelper.GetMainDBName();
            string where = "";
            if (!String.IsNullOrEmpty(clientType))
            {
                where += " and clienttype='" + clientType + "'";
            }
            if (!String.IsNullOrEmpty(code))
            {
                where += " and code like '%" + code + "%'";
            }
            if (!String.IsNullOrEmpty(displayName))
            {
                where += " and DipalsyName like '%" + displayName + "%'";
            }
            string sql = String.Format("select * from {0}.[dbo].Style where 1=1 {1}", mainDB, where);

            Hashtable pars = new Hashtable();
            //IList <PrivilegeUser>  r = this.QuerySql<PrivilegeUser>(sql, "*", "createtime desc,updatetime desc", pageSize, pageIndex, ref recordCount, pars);
            SplitPageResult<Style> re = this.QuerySplitPage<Style>(sql, "*", orderby, pageSize, pageIndex, pars);

            return re;
        }

        public bool DeleteStyleItem(int id)
        {
            string mainDB = Common.DbSqlHelper.GetMainDBName();
            string sql = String.Format("delete from {0}.[dbo].style where id=@id", mainDB);
            Hashtable pars = new Hashtable();
            pars.Add("id", id);
            try
            {
                this.ExecuteNonQuery(sql, pars);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int StyleHomeCount(int styleid)
        {
            string mainDB = Common.DbSqlHelper.GetMainDBName();
            string sql = String.Format("select count(styleid) cnt from {0}.[dbo].stylehomeitem where styleid=@styleid", mainDB);
            Hashtable pars = new Hashtable();
            pars.Add("styleid", styleid);
            object o = this.ExecuteScalar(sql, pars);
            if (o == null)
                return 0;
            else
                return Convert.ToInt32(o);
        }

        public Style GetEntity(int styleid)
        {
            return this.Get(styleid);
        }

        public void Save(string StyieID, string App4AIID, string DisplayName, string ImageAddress, string Seq)
        {
            StyleHomeItem item = new StyleHomeItem();
            item.StyleID = int.Parse(StyieID);
            item.App4AIID = int.Parse(App4AIID);
            item.DispalyName = DisplayName;
            item.Image = ImageAddress;
            item.Seq = int.Parse(Seq);            
        }

        

        
    }
}
