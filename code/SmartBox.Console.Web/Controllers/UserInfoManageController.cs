using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Beyondbit.MVC;
using SmartBox.Console.Common;
using SmartBox.Console.Bo;
using SmartBox.Console.Common.Entities.Search;

namespace SmartBox.Console.Web.Controllers
{
    public class UserInfoManageController : MyControllerBase
    {
        public ActionResult UserInfoManage()
        {
            return View();
        }
        public ActionResult UserDeviceOnline()
        {
            return View();
        }
        public ActionResult UserInfoManage_()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            SelectListItem S = new SelectListItem();
            S.Text = "全部";
            S.Value = "";
            SelectListItem Si = new SelectListItem();
            Si.Text = "在线";
            Si.Value = "online";
            SelectListItem Siout = new SelectListItem();
            Siout.Text = "离线";
            Siout.Value = "offline";
            list.Add(S);
            list.Add(Si);
            list.Add(Siout);
            ViewData["list"] = list.OfType<SelectListItem>();
            //IEnumerable<SelectListItem> s1 = s.Select(c => new SelectListItem { Value = c.Value, Text = c.Text }); 
            return View();
        }


        public ActionResult GetUserInfo(FormCollection form, string UserStatus)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            SearchConfig search = new SearchConfig();
            search.status = UserStatus;
            data = BoFactory.GetVersionTrackBo.GetUserInfo(view, search);
            return Json(data);
        }
       
    }
}
