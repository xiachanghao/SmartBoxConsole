using System;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using SmartBox.Console.Common.Entities;
using System.Data;
using SmartBox.Console.Common.Entities.Search;
using SmartBox.Console.Common;
using System.Collections;

namespace SmartBox.Console.Dao
{
    public partial class SMC_CollectDao : BaseDao<SMC_Collect>
    {
        public SMC_CollectDao(string key)
            : base(key)
        {

        }

        public SMC_Collect GetEntity(string uid, int pe_id)
        {
            string sql = string.Format("select * from SMC_Collect where pe_id=@pe_id and uid=@uid");
            Hashtable pars = new Hashtable();
            pars.Add("pe_id", pe_id);
            pars.Add("uid", uid);

            IList < SMC_Collect> r = this.Query(sql, pars);
            if (r != null && r.Count > 0)
                return r[0];
            else
                return null;
        }
        public void DeleteByPEID(string pe_id)
        {
            string sql = string.Format("Delete from SMC_Collect where pe_id='{0}'", pe_id);
            base.ExecuteNonQuery(sql);
        }

        public JsonFlexiGridData QueryPackageCollectList(PageView view, string id)
        {
            string columns = @"clt_id,uname,ClientType,clt_CollectDate";
            string sqlorder;

            string sWhere = "";
            sWhere = string.Format(" and pe_id={0}", id);


            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  clt_CollectDate asc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(columns, "SMC_Collect", sqlorder, "clt_id", sWhere, view);

        }

        public int GetMaxId()
        {
            string sql = "select isnull(max(clt_id), 0) clt_id from SMC_Collect";
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                try
                {
                    i = Convert.ToInt32(o);
                }
                catch (Exception e)
                {
                    i = 0;
                }
            }
            return i;
        }
    }
}
