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
using SmartBox.Console.Common.Entities.Search;

namespace SmartBox.Console.Web.Controllers
{
    public class StatisticManageController : MyControllerBase
    {
        StatisticsBO SBO = BoFactory.GetStatisticsBO;
        Statist entity = new Statist();

        private Nullable<DateTime> end = null;
        private Nullable<DateTime> start = null;
        private string unitName = string.Empty;

        //获取统计列表(无查询条件)
        public ActionResult ShowIndex()
        {
            return View();
        }

        public virtual JsonResult QueryShow(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = SBO.QueryShow(view);

            return Json(data);
        }

        public JsonResult SearchShow(string unitName, FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView();
            view.CurrentUserId = null;
            view.PageIndex = 0;
            view.PageSize = 15;
            view.RecordCount = 0;


            data = SBO.QueryShow(view,unitName);

            return Json(data);
        }

       //访问子系统(无查询条件)
        public ActionResult AppNameIndex()
        {
            List<string> app = SBO.GetAppList();
            
            int len = app.Count / 2;
            //List<string> AppNameList = new List<string>();//所有应用
            //List<string> AppDisplayNamelist = new List<string>();//所有应用名称
            string AppNameList = "";
            string AppDisplayNamelist = "";
            for (int i = 0; i < len; i++)
            {
                //AppNameList.Add(app.ElementAt(i));
                //AppDisplayNamelist.Add(app.ElementAt(i + len));
                AppNameList += "," + app.ElementAt(i);
                AppDisplayNamelist += "," + app.ElementAt(i + len);
            }
            ViewData["AppNameList"] = AppNameList.TrimStart(',');
            ViewData["AppDisplayNamelist"] = AppDisplayNamelist.TrimStart(',');

            return View();
        }


        public JsonResult QueryAppName(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = SBO.QueryAppName(view);
                      

            return Json(data);
        }

        //在线时长统计
        public ActionResult TimeIndex()
        {
            return View();
        }
        public ActionResult TimeIndex_()
        {
            return View();
        }
        
        public ActionResult ShowIndex_()
        {
            return View();
        }

        public JsonResult QueryTime(FormCollection form)
        {
            JsonFlexiGridData data = null;
            SearchStatisticOnlineTime view = new SearchStatisticOnlineTime(form);
            data = SBO.QueryTime(view);

            return Json(data);
        }

        //用户访问量统计
        public ActionResult UserIndex()
        {
            return View();
        }

        public JsonResult QueryUser(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = SBO.QueryUser(view);

            return Json(data);

        }
    }
}
