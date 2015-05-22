using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Beyondbit.MVC;
using SmartBox.Console.Common;
using SmartBox.Console.Bo;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Web.Controllers
{
    public class LogInfoManageController : MyControllerBase
    {
        public ActionResult LogInfoManage()
        {
            return View();
        }

        public ActionResult GetLogInfo(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.GetLogInfoList(view);
            return Json(data);
        }

        public ActionResult ViewLogInfo(string id)
        {
            LogInfo log = BoFactory.GetVersionTrackBo.GetLogById(id);
            return View(log);
        }

    }
}
