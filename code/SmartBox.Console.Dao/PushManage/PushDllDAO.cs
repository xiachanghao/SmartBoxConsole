using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using System.Collections;

namespace SmartBox.Console.Dao.PushManage
{
    public class SMC_PushDllDAO : BaseDao<SMC_PushDll>
    {
        public SMC_PushDllDAO(string key)
            : base(key)
        { }

        //public JsonFlexiGridData QueryPushNotificationList(PageView view)
        //{
        //    string tableName = "NotificationReport";
        //    string columns = "id,NotificationID,ReportCode,ReportMessage,AppCode,DeviceToken,Payload,NotificationIdentifier,ExpirationData,Priority";
        //    string orderby = "id desc";
        //    string where = "";
        //    string with = "";

        //    SmartBox.Console.Common.SelectPagnationEx r = base.SelectPaginationEx(tableName, columns, view.PageIndex + 1, view.PageSize, orderby, where, with);
        //    JsonFlexiGridData result = BaseDao<object>.ConvertJosnFlexGridData(r.Result.Tables[0], view, "id");
        //    result.page = r.PageCount;
        //    result.total = r.RecordCount;
        //    return result;
        //}

        public SplitPageResult<SMC_PushDll> QueryPushDLLList(int pageSize, int pageIndex)
        {
            
            string mainDB = Common.DbSqlHelper.GetMainDBName();
            string sql = "select * from [dbo].SMC_PushDll where pd_status=1";
            Hashtable pars = new Hashtable();
            //pars.Add("name", nam);
            //IList <PrivilegeUser>  r = this.QuerySql<PrivilegeUser>(sql, "*", "createtime desc,updatetime desc", pageSize, pageIndex, ref recordCount, pars);
            SplitPageResult<SMC_PushDll> re = this.QuerySplitPage<SMC_PushDll>(sql, "*", "pd_createdtime desc,pd_updatetime desc", pageSize, pageIndex, null);

            return re;
        }

        public bool ExistsXml(SMC_PushDll entity)
        {
            string sql = String.Format("select count(pd_id) from [dbo].SMC_PushDll where pd_xml_filename='{0}' and pd_status=1 and pd_id <> {1}", entity.pd_xml_filename, entity.pd_id);
            object o = this.ExecuteScalar(sql);
            if (o != null)
            {
                return Convert.ToInt32(o) > 0;
            }
            else
                return false;
        }

        public int GetMaxID()
        {
            string sql = "select isnull(max(pd_id),0) pd_id from [dbo].[SMC_PushDll]";
            object o = this.ExecuteScalar(sql);
            if (o != null)
            {
                return Convert.ToInt32(o);
            }
            else
                return 0;
        }

        public IList<SMC_PushDll> GetTrashPushDll()
        {
            string sql = "select * from [dbo].[SMC_PushDll] where pd_status=0";
            return this.QuerySql(sql);
        }

        public void CleanTrashPushDll()
        {
            string sql = "delete from [dbo].[SMC_PushDll] where pd_status=0";
            this.ExecuteNonQuery(sql);
        }
    }
}
