using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao.PushManage
{
    public class PushManageDAO : BaseDao<NotificationReport>
    {
        public PushManageDAO(string key)
            : base(key)
        { }

        public JsonFlexiGridData QueryPushNotificationList(PageView view)
        {
            string tableName = "NotificationReport";
            string columns = "id,NotificationID,ReportCode,ReportMessage,AppCode,DeviceToken,Payload,NotificationIdentifier,ExpirationData,Priority";
            string orderby = "id desc";
            string where = "";
            string with = "";

            SmartBox.Console.Common.SelectPagnationEx r = base.SelectPaginationEx(tableName, columns, view.PageIndex + 1, view.PageSize, orderby, where, with);
            JsonFlexiGridData result = BaseDao<object>.ConvertJosnFlexGridData(r.Result.Tables[0], view, "id");
            result.page = r.PageCount;
            result.total = r.RecordCount;
            return result;
        }
    }
}
