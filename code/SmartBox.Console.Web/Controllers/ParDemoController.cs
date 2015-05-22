using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Bo;
using Beyondbit.Framework.Core.Proxy;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SmartBox.Console.Web.Controllers
{
    public partial class DemoController : MyControllerBase
    {
        //
        // GET: /ParDemo/

        public ActionResult UserQuery()
        {
            return View();
        }

        public JsonResult GetUserQueryList(string uid, string uname, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationEx<SmartBox.Console.Common.Entities.SMC_User> items = Bo.BoFactory.GetSMC_UserBo.GetUserAuthorizationList(uid, uname, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, lockStatus, enabledStatus, authStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
