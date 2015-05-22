using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Dao
{
    public class HomePlanDesignDao : BaseDao<HomePlanDesign>
    {
        public HomePlanDesignDao(string key)
            : base(key)
        { }

        public IList<HomePlanDesign> QueryHomePlanDesign(string planID)
        {
            string sql = string.Format("select * from HomePlanDesign where PlanID='{0}'", planID);
            return base.Query(sql);
        }

        public IList<HomePlanDesign> QueryHomePlanDesignByPage(string planID, int pagePoint, int pageWidth)
        {
            string sql = string.Format(@"select 
                * from HomePlanDesign 
                where PlanID='{0}'
                and cast(substring(Location,0,(len(Location)-CHARINDEX(Location,',',0)-1)) as int) between {1} and {2}", planID, pagePoint, pagePoint + pageWidth);
            return base.Query(sql);
        }

        public HomePlanDesign Get(string planID, string appID)
        {
            if (string.IsNullOrEmpty(planID))
            {
                throw new Exception("必须指定布局ID");
            }
            if (string.IsNullOrEmpty(appID))
            {
                throw new Exception("必须指定应用ID");
            }
            string sql = string.Format("select * from HomePlanDesign where PlanID='{0}' and AppID='{1}'", planID, appID);
            IList<HomePlanDesign> designList = base.Query(sql);
            if (designList.Count > 0)
            {
                return designList[0];
            }
            return null;
        }
    }
}
