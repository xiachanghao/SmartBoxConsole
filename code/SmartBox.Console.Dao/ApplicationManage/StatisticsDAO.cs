using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common.Entities.Search;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SmartBox.Console.Dao
{
    public class StatisticsDAO : BaseDao<Statist>
    {
        public StatisticsDAO(string key)
            : base(key)
        { }

        public JsonFlexiGridData QueryShow(PageView view)
        {
            /*string column = @"UnitName,UserCount,PadAndroid,PadiOS,PCWindows,PhoneAndroid,PhoneiOS,UsageCount";

            StringBuilder sql = new StringBuilder();
            sql.Append(@"select UnitName,COUNT(distinct UserName) as UserCount,");
            sql.AppendLine(@"sum(case Device when 'PAd/Android' then UsageCount else 0 end)'PadAndroid',");
            sql.AppendLine(@"sum(case Device when 'PAd/iOS' then UsageCount else 0 end)'PadiOS',");
            sql.AppendLine(@"sum(case Device when 'PC/Windows' then UsageCount else 0 end)'PCWindows',");
            sql.AppendLine(@"sum(case Device when 'Phone/Android' then UsageCount else 0 end)'PhoneAndroid',");
            sql.AppendLine(@"sum(case Device when 'Phone/iOS' then UsageCount else 0 end)'PhoneiOS'");
            sql.AppendLine(",SUM(UsageCount) as UsageCount from (select * from [UsageLogDaily] where 1=1");

            sql.AppendLine(")a group by UnitName");
            return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as temp", sql.ToString()), view.OrderBy.ToString(), "UnitName", string.Empty, view);*/
            string column = @"UnitName,UserCount,PadAndroid,PadiOS,PCWindows,PhoneAndroid,PhoneiOS,UsageCount";
            
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select UnitName,COUNT(distinct UserName) as UserCount,");
            sql.Append(@"sum(case Device when 'PAd/Android' then UsageCount else 0 end) PadAndroid,");
            sql.Append(@"sum(case Device when 'PAd/iOS' then UsageCount else 0 end) PadiOS,");
            sql.Append(@"sum(case Device when 'PC/Windows' then UsageCount else 0 end) PCWindows,");
            sql.Append(@"sum(case Device when 'Phone/Android' then UsageCount else 0 end) PhoneAndroid,");
            sql.Append(@"sum(case Device when 'Phone/iOS' then UsageCount else 0 end) PhoneiOS");
            sql.Append(",SUM(UsageCount) as UsageCount from (select * from ");
            string statisticsDbName = DbSqlHelper.GetStatisticDBName();
            sql.Append(statisticsDbName);

            sql.Append("..[UsageLogDaily] where 1=1");
            sql.Append(")a group by UnitName");

            string with = "with t as ("+sql.ToString()+") ";
            string tableName = "t";
            string orderBy = "UnitName Asc";
            string where = "UnitName is not null";

            Log4NetHelper.Info("tableName:" + tableName + " column:" + column + " orderBy:" + orderBy + " where:" + where + " with:" + with);
            SmartBox.Console.Common.SelectPagnationEx r = base.SelectPaginationEx(tableName, column, view.PageIndex + 1, view.PageSize, orderBy, where, with);
            JsonFlexiGridData result = BaseDao<object>.ConvertJosnFlexGridData(r.Result.Tables[0], view, "UnitName");
            result.page = r.PageCount;
            result.total = r.RecordCount;
            return result;
        }

        public JsonFlexiGridData QueryShow(PageView view, string unitName)
        {
            string column = @"UnitName,UserCount,PadAndroid,PadiOS,PCWindows,PhoneAndroid,PhoneiOS,UsageCount";

            StringBuilder sql = new StringBuilder();
            sql.Append(@"select UnitName,COUNT(distinct UserName) as UserCount,");
            sql.AppendLine(@"sum(case Device when 'PAd/Android' then UsageCount else 0 end)'PadAndroid',");
            sql.AppendLine(@"sum(case Device when 'PAd/iOS' then UsageCount else 0 end)'PadiOS',");
            sql.AppendLine(@"sum(case Device when 'PC/Windows' then UsageCount else 0 end)'PCWindows',");
            sql.AppendLine(@"sum(case Device when 'Phone/Android' then UsageCount else 0 end)'PhoneAndroid',");
            sql.AppendLine(@"sum(case Device when 'Phone/iOS' then UsageCount else 0 end)'PhoneiOS'");
            sql.AppendLine(",SUM(UsageCount) as UsageCount from (select * from [UsageLogDaily] where 1=1");

            if (!string.IsNullOrEmpty(unitName))
            {
                sql.Append(string.Format(" and unitName='{0}'", unitName));
            }

            sql.AppendLine(")a group by UnitName");
            return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as temp", sql.ToString()), view.OrderBy.ToString(), "UnitName", string.Empty, view);
        }


        /// <summary>
        /// 查询 
        /// </summary>
        /// <param name="UntitName">单位名称</param>
        /// <param name="OpTime">访问时间</param>
        /// <returns></returns>
        public List<Statist> GetList(string UntitName, Nullable<DateTime> start, Nullable<DateTime> end)
        {
            List<Statist> list = new List<Statist>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select UnitName,COUNT(distinct UserName) as UserCount,");
            sql.AppendLine("sum(case Device when 'PAd/Android' then UsageCount else 0 end)'Pad/Android',");
            sql.AppendLine("sum(case Device when 'PAd/iOS' then UsageCount else 0 end)'Pad/iOS',");
            sql.AppendLine("sum(case Device when 'PC/Windows' then UsageCount else 0 end)'PC/Windows',");
            sql.AppendLine("sum(case Device when 'Phone/Android' then UsageCount else 0 end)'Phone/Android',");
            sql.AppendLine("sum(case Device when 'Phone/iOS' then UsageCount else 0 end)'Phone/iOS'");
            sql.AppendLine(",SUM(UsageCount) as UsageCount from (select * from [UsageLogDaily] where 1=1");
            if (!string.IsNullOrEmpty(UntitName))
            {
                sql.Append(string.Format(" and unitName='{0}'", UntitName));
            }
            if (start != null && end == null)
            {
                sql.AppendLine("  and OpTime='" + start + "'");
            }
            if (end != null && start == null)
            {
                sql.AppendLine("  and OpTime='" + end + "'");
            }
            if (start != null && end != null)
            {
                if (start <= end)
                {
                    sql.Append(string.Format("  and  OpTime between'" + start + "' and '" + end + "'"));
                }
            }
            sql.AppendLine(")a group by UnitName");
            DbSqlHelper.connectionString = @"Data Source=192.168.200.141;Uid=sa;Pwd=App1234;Initial Catalog=SmartBoxApp";
            var ds = DbSqlHelper.Query(sql.ToString());
            foreach (DataRow row in ds.Tables[0].Rows)
            {

                Statist entity = new Statist();
                if (!row.IsNull("UsageCount"))
                {
                    entity.UsageCount = Convert.ToInt32(row["UsageCount"].ToString());
                }
                if (!row.IsNull("UserCount"))
                {
                    entity.UserCount = Convert.ToInt32(row["UserCount"].ToString());
                }
                if (!row.IsNull("UnitName"))
                {
                    entity.UnitName = row["UnitName"].ToString();
                }
                if (!row.IsNull("PAd/Android"))
                {
                    entity.Pad_Android = Convert.ToInt32(row["PAd/Android"].ToString());
                }
                if (!row.IsNull("Pad/iOS"))
                {
                    entity.Pad_iOS = Convert.ToInt32(row["Pad/iOS"].ToString());
                }
                if (!row.IsNull("PC/Windows"))
                {
                    entity.PC_Windows = Convert.ToInt32(row["PC/Windows"].ToString());
                }
                if (!row.IsNull("Phone/Android"))
                {
                    entity.Phone_Android = Convert.ToInt32(row["Phone/Android"].ToString());
                }
                if (!row.IsNull("Phone/iOS"))
                {
                    entity.Phone_iOS = Convert.ToInt32(row["Phone/iOS"].ToString());
                }
                list.Add(entity);
            }
            return list;
        }

        public JsonFlexiGridData QueryUser(PageView view)
        {
            string column = @"UserName,UnitName,PadAndroid,PadiOS,PCWindows,PhoneAndroid,PhoneiOS";
            string maindbName = DbSqlHelper.GetMainDBName();
            string statisticDBName = DbSqlHelper.GetStatisticDBName();

            StringBuilder sql = new StringBuilder();
            sql.Append("select UserName,UnitName,");
            sql.AppendLine("sum(case Device when 'PAD/ANDROID' then UsageCount else 0 end)'PadAndroid',");
            sql.AppendLine("sum(case Device when 'PAD/IOS' then UsageCount else 0 end)'PadiOS',");
            sql.AppendLine("sum(case Device when 'PC/Windows' then UsageCount else 0 end)'PCWindows',");
            sql.AppendLine("sum(case Device when 'PHONE/ANDROID' then UsageCount else 0 end)'PhoneAndroid',");
            sql.AppendLine("sum(case Device when 'PHONE/IOS' then UsageCount else 0 end)'PhoneiOS'");
            sql.AppendLine(" from (select * from ");
            sql.AppendLine(statisticDBName);
            sql.AppendLine("..[UsageLogDaily] where 1=1");

            sql.AppendLine(")a group by UserName,UnitName");


            string with = "with t as (" + sql.ToString() + ") ";
            
            with = with.Replace("[UserLoginInfo]", " " + maindbName + ".dbo.[UserLoginInfo]").Replace("V_BUA_USER", maindbName + ".dbo.V_BUA_USER");
            string tableName = "t";
            string orderBy = view.OrderBy.ToString().ToLower().Replace(" order by ", "");
            string where = "UserName is not null";

            with = with.Replace("\r\n", "");

            Log4NetHelper.Info("QueryUser-tableName:" + tableName + " column:" + column + " orderBy:" + orderBy + " where:" + where + " with:" + with);

            SmartBox.Console.Common.SelectPagnationEx rs = base.SelectPaginationEx(tableName, column, view.PageIndex + 1, view.PageSize, orderBy, where, with);
            JsonFlexiGridData result = BaseDao<object>.ConvertJosnFlexGridData(rs.Result.Tables[0], view, "UnitName");
            result.page = rs.PageCount;
            result.total = rs.RecordCount;
            return result;

            //return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as temp", sql.ToString()), view.OrderBy.ToString(), "UnitName", string.Empty, view);
        }
        /// <summary>
        /// 查询 
        /// </summary>
        /// <param name="UntitName">用户名称</param>
        /// <param name="OpTime">访问时间</param>
        /// <returns></returns>
        public List<Statist> GetUserList(string UserName, Nullable<DateTime> start, Nullable<DateTime> end)
        {
            List<Statist> list = new List<Statist>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select UserName,UnitName,");
            sql.AppendLine("sum(case Device when 'PAD/ANDROID' then UsageCount else 0 end)'PAD/ANDROID',");
            sql.AppendLine("sum(case Device when 'PAD/IOS' then UsageCount else 0 end)'PAD/IOS',");
            sql.AppendLine("sum(case Device when 'PHONE/ANDROID' then UsageCount else 0 end)'PHONE/ANDROID',");
            sql.AppendLine("sum(case Device when 'PHONE/IOS' then UsageCount else 0 end)'PHONE/IOS'");
            sql.AppendLine(" from (select * from [UsageLogDaily] where 1=1");
            if (!string.IsNullOrEmpty(UserName))
            {
                sql.Append(string.Format(" and UserName='{0}'", UserName));
            }
            if (start != null && end == null)
            {
                sql.AppendLine("  and OpTime='" + start + "'");
            }
            if (end != null && start == null)
            {
                sql.AppendLine("  and OpTime='" + end + "'");
            }
            if (start != null && end != null)
            {
                if (start <= end)
                {
                    sql.Append(string.Format("  and  OpTime between'" + start + "' and '" + end + "'"));
                }
            }
            sql.AppendLine(")a group by UserName,UnitName");
            DbSqlHelper.connectionString = @"Data Source=192.168.2.16\sql2008r2,2244;Initial Catalog=SmartBoxApp;User ID=SmartBox;Password=App1234";
            var ds = DbSqlHelper.Query(sql.ToString());
            foreach (DataRow row in ds.Tables[0].Rows)
            {

                Statist entity = new Statist();
                if (!row.IsNull("UserName"))
                {
                    entity.UserName = row["UserName"].ToString();
                }
                if (!row.IsNull("UnitName"))
                {
                    entity.UnitName = row["UnitName"].ToString();
                }
                if (!row.IsNull("PAD/ANDROID"))
                {
                    entity.Pad_Android = Convert.ToInt32(row["PAD/ANDROID"].ToString());
                }
                if (!row.IsNull("PAD/IOS"))
                {
                    entity.Pad_iOS = Convert.ToInt32(row["PAD/IOS"].ToString());
                }
                if (!row.IsNull("PHONE/ANDROID"))
                {
                    entity.Phone_Android = Convert.ToInt32(row["PHONE/ANDROID"].ToString());
                }
                if (!row.IsNull("PHONE/IOS"))
                {
                    entity.Phone_iOS = Convert.ToInt32(row["PHONE/IOS"].ToString());
                }
                list.Add(entity);
            }
            return list;
        }


        public List<Statist> GetAppNameList(string UserName, Nullable<DateTime> start, Nullable<DateTime> end)
        {
            List<Statist> list = new List<Statist>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select UserName,UnitName,");
            sql.AppendLine("sum(case AppName when 'mail' then UsageCount else 0 end)'mail',");
            sql.AppendLine("sum(case AppName when 'pt.calendar' then UsageCount else 0 end)'calendar',");
            sql.AppendLine("sum(case AppName when 'magazine' then UsageCount else 0 end)'magazine',");
            sql.AppendLine("sum(case AppName when 'contact' then UsageCount else 0 end)'contact',");
            sql.AppendLine("sum(case AppName when 'callboard' then UsageCount else 0 end)'callboard',");
            sql.AppendLine("sum(case AppName when 'sms' then UsageCount else 0 end)'sms',");
            sql.AppendLine("sum(case AppName when 'pt.keywork' then UsageCount else 0 end)'keywork',");
            sql.AppendLine("sum(case AppName when 'pt.dowork' then UsageCount else 0 end)'dowork',");
            sql.AppendLine("sum(case AppName when 'pt.yqzg' then UsageCount else 0 end)'yqzg',");
            sql.AppendLine("sum(case AppName when 'pt.instruction' then UsageCount else 0 end)'instruction',");
            sql.AppendLine("sum(case AppName when 'pt.dbd' then UsageCount else 0 end)'dbd',");
            sql.AppendLine("sum(case AppName when 'pt.fourdo' then UsageCount else 0 end)'fourdo'");
            sql.AppendLine("  from (select * from [UsageLogDaily] where 1=1");
            if (!string.IsNullOrEmpty(UserName))
            {
                sql.Append(string.Format(" and UserName='{0}'", UserName));
            }
            if (start != null && end == null)
            {
                sql.AppendLine("  and OpTime='" + start + "'");
            }
            if (end != null && start == null)
            {
                sql.AppendLine("  and OpTime='" + end + "'");
            }
            if (start != null && end != null)
            {
                if (start <= end)
                {
                    sql.Append(string.Format("  and  OpTime between'" + start + "' and '" + end + "'"));
                }
            }
            sql.AppendLine(")a group by UserName,UnitName");
            DbSqlHelper.connectionString = @"Data Source=192.168.2.16\sql2008r2,2244;Initial Catalog=SmartBoxApp;User ID=SmartBox;Password=App1234";
            var ds = DbSqlHelper.Query(sql.ToString());
            foreach (DataRow row in ds.Tables[0].Rows)
            {

                Statist entity = new Statist();
                if (!row.IsNull("UserName"))
                {
                    entity.UserName = row["UserName"].ToString();
                }
                if (!row.IsNull("UnitName"))
                {
                    entity.UnitName = row["UnitName"].ToString();
                }
                if (!row.IsNull("mail"))
                {
                    entity.mail = row["mail"].ToString();
                }
                if (!row.IsNull("calendar"))
                {
                    entity.calendar = row["calendar"].ToString();
                }
                if (!row.IsNull("magazine"))
                {
                    entity.magazine = row["magazine"].ToString();
                }
                if (!row.IsNull("contact"))
                {
                    entity.contact = row["contact"].ToString();
                }
                if (!row.IsNull("callboard"))
                {
                    entity.callboard = row["callboard"].ToString();
                }
                if (!row.IsNull("sms"))
                {
                    entity.sms = row["sms"].ToString();
                }
                if (!row.IsNull("keywork"))
                {
                    entity.keywork = row["keywork"].ToString();
                }
                if (!row.IsNull("dowork"))
                {
                    entity.dowork = row["dowork"].ToString();
                }
                if (!row.IsNull("yqzg"))
                {
                    entity.yqzg = row["yqzg"].ToString();
                }
                if (!row.IsNull("instruction"))
                {
                    entity.instruction = row["instruction"].ToString();
                }
                if (!row.IsNull("dbd"))
                {
                    entity.dbd = row["dbd"].ToString();
                }
                if (!row.IsNull("fourdo"))
                {
                    entity.fourdo = row["fourdo"].ToString();
                }
                list.Add(entity);
            }
            return list;
        }

        public List<string> GetAppList()
        {
            List<string> AppNameList = new List<string>();//所有应用
            List<string> AppDisplayNamelist = new List<string>();//所有应用名称
            string conStringMainDb = ConfigurationManager.ConnectionStrings[AppConfig.mainDbKey].ToString(); //AppConfig.mainDbKey;

            SqlConnection conn = new SqlConnection(conStringMainDb);
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select distinct name,displayName from application where name in (select distinct appname from [SmartBoxApp].[dbo].[UsageLogDaily])", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        AppNameList.Add(r.ItemArray[0].ToString().Replace('.', '@'));
                        AppDisplayNamelist.Add(r.ItemArray[1].ToString());
                    }
                }
            }
            catch (Exception e)
            { ; }
            finally
            {
                conn.Close();
            }

            foreach (string s in AppDisplayNamelist)
            { AppNameList.Add(s); }
            return AppNameList;
        }

        public JsonFlexiGridData QueryAppName(PageView view)
        {
            List<string> AppNameList = new List<string>();//所有应用
            List<string> AppDisplayNamelist = new List<string>();//所有应用名称
            string conStringMainDb = ConfigurationManager.ConnectionStrings[AppConfig.mainDbKey].ToString(); //AppConfig.mainDbKey;

            SqlConnection conn = new SqlConnection(conStringMainDb);
            string statisticsDbName = DbSqlHelper.GetStatisticDBName();
            try
            {
                conn.Open();


                string sql = "select distinct name,displayName from application where name in (select distinct appname from [" + statisticsDbName + "].[dbo].[UsageLogDaily])";
                Log4NetHelper.Info("QueryAppName-sql:" + sql);
                
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        AppNameList.Add(r.ItemArray[0].ToString());
                        AppDisplayNamelist.Add(r.ItemArray[1].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
            }
            finally
            {
                conn.Close();
            }

            try
            {
                StringBuilder columns = new StringBuilder("UserName,UnitName");

                StringBuilder sql = new StringBuilder();
                sql.Append("select UserName,UnitName");
                string fformat = ",sum(case AppName when '{0}' then UsageCount else 0 end)'{1}'";
                foreach (string s in AppNameList)
                {
                    string ns = s.Replace(".", "@");
                    string sqlCondition = "";
                    columns.Append("," + ns);
                    sqlCondition = string.Format(fformat, s, ns);
                    sql.Append(sqlCondition);
                }
                string column = columns.ToString();

                sql.Append(" from (select * from ");
                sql.Append(statisticsDbName);
                sql.Append("..[UsageLogDaily] where 1=1");

                sql.Append(")a group by UserName,UnitName");


                string with = "with t as (" + sql.ToString() + ") ";
                string tableName = "t";
                string orderBy = "UnitName Asc";
                string where = "UserName is not null";


                SmartBox.Console.Common.SelectPagnationEx rs = base.SelectPaginationEx(tableName, column, view.PageIndex + 1, view.PageSize, orderBy, where, with);
                JsonFlexiGridData result = BaseDao<object>.ConvertJosnFlexGridData(rs.Result.Tables[0], view, "UnitName");
                result.page = rs.PageCount;
                result.total = rs.RecordCount;
                return result;
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                return null;
            }

            //return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as temp", sql.ToString()), view.OrderBy.ToString(), "UnitName", string.Empty, view);
        }

        public JsonFlexiGridData QueryTime2(SearchStatisticOnlineTime view)
        {
            string maindbName = DbSqlHelper.GetMainDBName();
            string buadb = ConfigurationManager.AppSettings["bua_db_name"];
            string statisticsDbName = DbSqlHelper.GetStatisticDBName();
            string sql = @"with t as(
select useruid,device,logintime,isnull(logouttime,getdate()) logouttime,datediff(mi,logintime,isnull(logouttime,getdate())) times from " + maindbName + @".dbo.UserLoginInfo  where logouttime is not null
"; 
            if (!string.IsNullOrEmpty(view.StartTime))
            {
                sql += " where logintime >= '" +view.StartTime + " 0:00:00'";
            }
            if (!string.IsNullOrEmpty(view.EndTime))
            {
                sql += " and logintime <= '" + view.EndTime + " 23:59:59'";//logouttime
            }
            sql += 
@"),pc as(
select * from t where device='PC/WINDOWS' or device='pc/windows8'
),apad as (
select * from t where device='Pad/Android' or device='ANDROID_PAD'
),aphone as (
select * from t where device='Phone/Android' or device='ANDROID_PHONE'
),ipad as (
select * from t where device='PAD/IOS'
),iphone as (
select * from t where device='PHONE/IOS'
),u as (

select u_uid user_uid,u_name user_name,(select unit_name from " + statisticsDbName + @"..smc_unit where unit_id=bu.u_unitcode) org_name from " + statisticsDbName + @"..smc_user bu where u_unitcode is not null
),suser as(
select distinct useruid from " + maindbName + @".dbo.UserLoginInfo
)
, siphone as(
select useruid,cast(cast(sum(times) as decimal(18,2)) / 60 as decimal(18,2)) time_iphone from iphone group by useruid
), sipad as(
select useruid,cast(cast(sum(times) as decimal(18,2)) / 60 as decimal(18,2)) time_ipad from ipad group by useruid
), saphone as(
select useruid,cast(cast(sum(cast(times as decimal(15,2))) as decimal(18,2)) / 60 as decimal(18,2)) time_aphone from aphone group by useruid
), sapad as(
select useruid,cast(cast(sum(times) as decimal(18,2)) / 60 as decimal(18,2)) time_apad from apad group by useruid
), spc as(
select useruid,cast(cast(sum(times) as decimal(18,2)) / 60 as decimal(18,2)) time_pc from pc group by useruid
),st as(
select suser.useruid,u.user_name User_Full_Name,u.org_name,
isnull(siphone.time_iphone, 0.00) PhoneiOS,
isnull(sipad.time_ipad, 0.00) PadiOS,
isnull(saphone.time_aphone, 0.00) PhoneAndroid,
isnull(sapad.time_apad, 0.00) PadAndroid
from suser left join siphone on suser.useruid=siphone.useruid

 left join sipad on suser.useruid=sipad.useruid
  left join saphone on suser.useruid=saphone.useruid
   left join sapad on suser.useruid=sapad.useruid
   join u on u.user_uid=suser.useruid
   )
   select * from st order by org_name
   ";
            DataSet ds = base.ExecuteDataset(sql, CommandType.Text);

            JsonFlexiGridData result = BaseDao<object>.ConvertJosnFlexGridData(ds.Tables[0], view, "UserUid");
            result.page = 1;
            result.total = ds.Tables[0].Rows.Count;
            return result;
        }

        public JsonFlexiGridData QueryTime3(SearchStatisticOnlineTime view)
        {
            string maindbName = DbSqlHelper.GetMainDBName();
            string buadb = ConfigurationManager.AppSettings["bua_db_name"];
            string statisticsDbName = DbSqlHelper.GetStatisticDBName();
            string sql = @"with t as(
select uid,deviceid,begintime,lasttime,cast(datediff(mi,begintime,isnull(lasttime,getdate())) as numeric(12,3)) times from userusinglog where 1=1 ";
            if (!string.IsNullOrEmpty(view.StartTime))
            {
                sql += " and begintime >= '" + view.StartTime + " 0:00:00'";
            }
            if (!string.IsNullOrEmpty(view.EndTime))
            {
                sql += " and lasttime <= '" + view.EndTime + " 23:59:59'";//logouttime
            }
            sql +=@"),u as
(select uid,deviceid,begintime,lasttime,cast(times /60 as numeric(12,2)) times from t),
v as(
select isnull(d.resource,'') clienttype,u.uid,u.deviceid,u.begintime,u.lasttime,u.times from u left join device d on u.deviceid=d.id
),w as(
select clienttype,uid,sum(times) times from v group by clienttype,uid
),x
as (
select usr.u_uid UserUid,usr.u_name User_Full_Name,usr.u_unitname ORG_NAME, 
isnull((select times from w where w.uid=usr.u_uid and w.clienttype='phone/android'),0) as PhoneAndroid,
isnull((select times from w where w.uid=usr.u_uid and w.clienttype='pad/android'),0) as PadAndroid,
isnull((select times from w where w.uid=usr.u_uid and w.clienttype='phone/ios'),0) as PhoneiOS,
isnull((select times from w where w.uid=usr.u_uid and w.clienttype='pad/ios'),0) as PadiOS
 from " + statisticsDbName + @".dbo.smc_user usr
)
select * from x";
            if (!string.IsNullOrEmpty(view.UID))
            {
                sql += " where UserUid = '" + view.UID + "'";//logouttime
            }
            DataSet ds = base.ExecuteDataset(sql, CommandType.Text);

            JsonFlexiGridData result = BaseDao<object>.ConvertJosnFlexGridData(ds.Tables[0], view, "UserUid");
            result.page = 1;
            result.total = ds.Tables[0].Rows.Count;
            return result;
        }

        public JsonFlexiGridData QueryTime(SearchStatisticOnlineTime view)
        {
            string column = @"UserUid,User_Full_Name,ORG_NAME,PadAndroid,PadiOS,PhoneAndroid,PhoneiOS";

            StringBuilder sunSql = new StringBuilder();
            string startSunSql = string.Empty;
            string startTimeSql = string.Empty;
            string endSql = string.Empty;

            if (!string.IsNullOrEmpty(view.StartTime))
            {
                startSunSql = string.Format(" when datediff(day,logintime,'{0}')>0 then CONVERT(numeric(8,2),DateDiff(ss,'{0}',LogoutTime)/(60.00*60))", view.StartTime);
                startTimeSql = string.Format("(case when datediff(day,LoginTime,'{0}')>0 then '{0}' else LoginTime end)",view.StartTime);
            }
            else
            {
                startTimeSql = "LoginTime";
            }
            if (!string.IsNullOrEmpty(view.EndTime))
            {

                sunSql.AppendFormat(@"case when LogoutTime is null then CONVERT(numeric(8,2),DATEDIFF(ss,LoginTime,GETDATE())/(60.00*60))
		                                   when DateDiff(day,LogoutTime,'{0}')<0 then CONVERT(numeric(8,2),DATEDIFF(ss,LoginTime,'{0}'+' 23:59:59.999')/(60.00*60)) ",view.EndTime);
                sunSql.Append(startSunSql);
                sunSql.AppendFormat(@" else  CONVERT(numeric(8,2),DateDiff(ss,LoginTime,LogoutTime)/(60.00*60)) end ", view.EndTime);
            }
            else
            {
                sunSql.AppendFormat("case when LogoutTime is null then CONVERT(numeric(8,2),DateDiff(ss,LoginTime,getdate())/(60.00*60)) else CONVERT(numeric(8,2),DateDiff(ss,{0},LogoutTime)/(60.00*60)) end",startTimeSql);
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"select UserUid,User_Full_Name,ORG_NAME,
            sum(case Device when 'PAD/ANDROID' then {0} else 0 end)'PadAndroid',
            sum(case Device when 'PAD/IOS' then {0} else 0 end)'PadiOS',
            sum(case Device when 'PAD/ANDROID' then {0} else 0 end)'PhoneAndroid',
            sum(case Device when 'PHONE/IOS' then {0} else 0 end)'PhoneiOS'
            from (select info.*,u.USER_FULL_NAME,org.ORG_NAME from [UserLoginInfo]  info
                 inner join V_BUA_USER u on info.UserUid=u.USER_UID
                 left join bua.dbo.BUA_ORGANIZATION org on u.ORG_ID=org.ORG_ID where 1=1 ", sunSql);

            if (!string.IsNullOrEmpty(view.StartTime))
            {
                sql.AppendFormat(@" and (datediff(day,info.LoginTime,'{0}')<=0 or (info.LogoutTime is not null and datediff(day,info.LogoutTime,'{0}')<=0 and info.LoginTime<='{0}')) ", view.StartTime);
            }
            if (!string.IsNullOrEmpty(view.EndTime))
            {

                sql.AppendFormat(" and datediff(day,info.LoginTime,'{0}')>=0 ", view.EndTime);
            }

            sql.AppendLine(")a group by UserUid,User_Full_Name,ORG_NAME");

            string with = "with t as (" + sql.ToString() + ") ";
            string maindbName = DbSqlHelper.GetMainDBName();
            with = with.Replace("[UserLoginInfo]", " " + maindbName + ".dbo.[UserLoginInfo]").Replace("V_BUA_USER", maindbName + ".dbo.V_BUA_USER");
            string tableName = "t";
            string orderBy = view.OrderBy.ToString().ToLower().Replace(" order by ", "");
            string where = "";


            SmartBox.Console.Common.SelectPagnationEx rs = base.SelectPaginationEx(tableName, column, view.PageIndex + 1, view.PageSize, orderBy, where, with);
            JsonFlexiGridData result = BaseDao<object>.ConvertJosnFlexGridData(rs.Result.Tables[0], view, "UserUid");
            result.page = rs.PageCount;
            result.total = rs.RecordCount;
            return result;
            //return base.QueryDataForFlexGridByPager(column, string.Format("({0}) as temp", sql.ToString()), view.OrderBy.ToString(), "UserUid", string.Empty, view);
        }

        /// <summary>
        /// 查询 
        /// </summary>
        /// <param name="UntitName">用户名称</param>
        /// <param name="OpTime">访问时间</param>
        /// <returns></returns>
        public List<Statist> GetTimeList(string UserName)
        {
            List<Statist> list = new List<Statist>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select UserUid,");
            sql.AppendLine("sum(case Device when 'PAD/ANDROID' then DateDiff(HOUR,LoginTime,LogoutTime) else 0 end)'Pad/Android',");
            sql.AppendLine("sum(case Device when 'PAD/IOS' then DateDiff(HOUR,LoginTime,LogoutTime) else 0 end)'Pad/iOS',");
            sql.AppendLine("sum(case Device when 'PAD/ANDROID' then DateDiff(HOUR,LoginTime,LogoutTime) else 0 end)'Phone/Android',");
            sql.AppendLine("sum(case Device when 'PHONE/IOS' then DateDiff(HOUR,LoginTime,LogoutTime) else 0 end)'Phone/iOS'");
            sql.AppendLine(" from (select * from [UserLoginInfo] where 1=1");
            if (!string.IsNullOrEmpty(UserName))
            {
                sql.Append(string.Format(" and UserUid='{0}'", UserName));
            }
            sql.AppendLine(")a group by UserUid");
            DbSqlHelper.connectionString = @"Data Source=192.168.2.16\sql2008r2,2244;Initial Catalog=SmartBox;User ID=SmartBox;Password=App1234";
            var ds = DbSqlHelper.Query(sql.ToString());
            foreach (DataRow row in ds.Tables[0].Rows)
            {

                Statist entity = new Statist();
                if (!row.IsNull("UserUid"))
                {
                    entity.UserUid = row["UserUid"].ToString();
                }
                //if (!row.IsNull("UnitName"))
                //{
                //    entity.UnitName = row["UnitName"].ToString();
                //}
                if (!row.IsNull("PAD/ANDROID"))
                {
                    entity.Pad_Android = Convert.ToInt32(row["PAD/ANDROID"].ToString());
                }
                if (!row.IsNull("PAD/IOS"))
                {
                    entity.Pad_iOS = Convert.ToInt32(row["PAD/IOS"].ToString());
                }
                if (!row.IsNull("PHONE/ANDROID"))
                {
                    entity.Phone_Android = Convert.ToInt32(row["PHONE/ANDROID"].ToString());
                }
                if (!row.IsNull("PHONE/IOS"))
                {
                    entity.Phone_iOS = Convert.ToInt32(row["PHONE/IOS"].ToString());
                }
                list.Add(entity);
            }
            return list;
        }
    }
}
