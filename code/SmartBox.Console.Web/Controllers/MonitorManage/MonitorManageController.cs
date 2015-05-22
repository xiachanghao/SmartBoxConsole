using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartBox.Console.Common;
using SmartBox.Console.Bo;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Web.Controllers.MonitorManage
{
    public class MonitorManageController : MyControllerBase
    {
        //
        // GET: /ConfigManage/


        public ActionResult ConfigManage()
        {
            return View();
        }

        public ActionResult GetConfigInfo(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = BoFactory.GetMonitorBO.QueryMonitorConfig(view);
            return Json(data);
        }

        public ActionResult AddConfig()
        {
            return View();
        }

        public JsonResult EditConfigSave(int cfg_id, string cfg_hostname, string cfg_hostip, string cfg_file, string Enable, string cfg_usedate)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            Monitor_Config monitorConfig = null;
            if (cfg_id == 0)
            {
                monitorConfig = new Monitor_Config();
                monitorConfig.cfg_createdate = DateTime.Now;
            }
            else
            {
                monitorConfig = BoFactory.GetMonitorBO.Get(cfg_id);
            }
            monitorConfig.cfg_hostname = cfg_hostname;
            monitorConfig.cfg_hostip = cfg_hostip;
            monitorConfig.cfg_file = cfg_file;
            monitorConfig.cfg_isuse = Enable;
            if (!String.IsNullOrEmpty(cfg_usedate))
                monitorConfig.cfg_usedate = DateTime.Parse(cfg_usedate);

            BoFactory.GetMonitorBO.Save(monitorConfig);
            return Json(data);
        }

        //
        // GET: /ConfigManage/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ConfigManage/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ConfigManage/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /ConfigManage/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ConfigManage/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ConfigManage/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ConfigManage/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
