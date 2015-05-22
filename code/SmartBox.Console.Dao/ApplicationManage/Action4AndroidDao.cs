using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using System.Collections;

namespace SmartBox.Console.Dao
{
    public class Action4AndroidDao : BaseDao<Action4Android>
    {
        public Action4AndroidDao(string key)
            : base(key)
        { }

        public IList<Action4Android> QueryAction4AndroidListByApp4AIID(string app4AI_ID)
        {
            string sql = string.Format("select * from Action4Android where App4AIID='{0}'",app4AI_ID);
            return base.Query(sql);
        }

        public void DeleteAction4AndroidByPackage4AIID(int package4AIID)
        {
            string sql = "delete from [dbo].[Action4Android] where app4aiid in (select id from [dbo].[App4AI] where package4aiid=@package4aiid)";
            Hashtable pars = new Hashtable();
            pars.Add("package4aiid", package4AIID);
            this.ExecuteNonQuery(sql, pars);
        }
    }
}
