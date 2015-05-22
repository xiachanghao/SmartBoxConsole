using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Beyondbit.MVC;
using SmartBox.Console.Common;
using SmartBox.Console.Bo;

namespace SmartBox.Console.Web.Controllers
{
    public class IMGroupManageController : MyControllerBase
    {
        //
        // GET: /IMGroupManage/

        public ActionResult IMGroupManage()
        {
            return View();
        }

        public ActionResult GetIMGroupInfo(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryIMGroupList(view);
            return Json(data);
        }

    }
}
