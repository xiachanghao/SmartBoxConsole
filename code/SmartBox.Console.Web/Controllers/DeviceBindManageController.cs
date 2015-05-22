using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Beyondbit.MVC;
using SmartBox.Console.Common;
using SmartBox.Console.Bo;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common.Entities.Search;
using ICSharpCode.SharpZipLib.Zip;
using ConvertPlist;
using System.Xml;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace SmartBox.Console.Web.Controllers
{
    public class DeviceBindManageController : MyControllerBase
    {
        //
        // GET: /DeviceBindManage/

        #region 申请设备绑定
        public ViewResult ApplyDeviceBind()
        {
            return View();
        }

        public JsonResult QueryApplyDeviceBindList(FormCollection form)
        {
            string unit_id = "";
            if (!this.IsSystemManager)
            {
                unit_id = this.CurrentUser.UnitCode;
            }
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            try
            {
                data = BoFactory.GetVersionTrackBo.QueryApplyDeviceBindList(view);
            }
            catch { }
                        
            string columns = "operate,UserUid,DeviceId,Ip,Status,ApplyTime,Description,ID";

            var returnValue = ConvertJsonGrid.ConvertFlexGridToJqGrid(data, view, columns);
            var d= Json(returnValue);
            
            return d;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ApproveApply(string id, string Operation)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                BoFactory.GetVersionTrackBo.ApproveApplyDeviceBind(id, Operation);
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }
            return Json(data);
        }


        #endregion

        #region 已绑定设备管理
        public ViewResult ViewDeviceBind()
        {
            string mode = "white";
            SearchConfig search = new SearchConfig();
            search.PluginCode = Constants.MianName;
            search.ConfigCategoryCode = Constants.SystemConfig;
            search.key = "DeviceBindMode";

            IList<ConfigInfo> listconfigs = BoFactory.GetVersionTrackBo.GetConfigList(search);
            if (listconfigs.Count > 0)
            {
                mode = listconfigs[0].Value1;
            }
            ViewData["Mode"] = mode;

            return View();
        }

        public ViewResult ViewDeviceBind2(string pageIndex, string uid, string desc, string mode, string state)
        {
            //string mode = "white";
            SearchConfig search = new SearchConfig();
            search.PluginCode = Constants.MianName;
            search.ConfigCategoryCode = Constants.SystemConfig;
            search.key = "DeviceBindMode";

            IList<ConfigInfo> listconfigs = BoFactory.GetVersionTrackBo.GetConfigList(search);
            if (listconfigs.Count > 0)
            {
                mode = listconfigs[0].Value1;
            }
            ViewData["Mode"] = mode;

            int page = 1;
            if (!String.IsNullOrEmpty(pageIndex))
                page = Convert.ToInt32(pageIndex) + 1;
            var fc = new FormCollection();
            fc.Add("page", page.ToString());
            fc.Add("rp", "10");
            fc.Add("sortname", "UserUid");
            fc.Add("query", "");
            fc.Add("qtype", "");

            JsonFlexiGridData r = (JsonFlexiGridData)SearchDeviceBind(fc, uid, desc, state).Data;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonStr = serializer.Serialize(r);
            ViewData["data"] = jsonStr;
            

            return View();
        }

        public JsonResult QueryDeviceBindList(FormCollection form)
        {
            
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            try
            {
                data = BoFactory.GetVersionTrackBo.QueryDeviceBindList(view);
            }
            catch { }
                        
            
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SetStatus(string id, string Operation)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                BoFactory.GetVersionTrackBo.SetDeviceBindStatus(id, Operation);
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SetMode(string Mode)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "设置成功" };
            try
            {                
                ConfigInfo conf = new ConfigInfo();
                conf.UserUId = CurrentUser.UserUId;
                conf.ConfigCategoryCode = "SysCommonConfig";
                conf.Key1 = "DeviceBindMode";
                conf.Value1 = Mode;
                conf.PluginCode = "smartbox";

                BoFactory.GetVersionTrackBo.SetConfigOfMode(conf);
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }            
            return Json(data);
        }
        

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SearchDeviceBind(FormCollection form,string UserId, string Description, string Status)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            try
            {
                data = BoFactory.GetVersionTrackBo.SearchDeviceBindList(view,UserId,Description,Status);
            }
            catch { }

            return Json(data);
        }


        #endregion
    }
}
