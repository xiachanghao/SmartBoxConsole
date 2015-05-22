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
using System.Xml;
using SmartBox.Console.Common;
using ThoughtWorks.QRCode.Codec;
using System.Text;

namespace SmartBox.Console.Web.Controllers
{
    public partial class DemoController : MyControllerBase
    {
        //
        // GET: /Demo/

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult TaskCenter()
        {
            return View();
        }
        
        public ActionResult TaskCenter_()
        {
            return View();
        }

        public ActionResult MobileStyleList()
        {
            return View();
        }
        public ActionResult LogDemo()
        {
            try
            {
                String s = null;
                s.ToString();
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }
            return View();
        }

        public ActionResult UserAuthorization()
        {
            return View();
        }

        public ActionResult DeviceExceptionAdd()
        {
            return View();
        }

        public ActionResult UserExceptionAdd()
        {
            return View();
        }
        
        public ActionResult LogoManage()
        {
            return View();
        }
        
        public ActionResult AppLog()
        {
            return View();
        }
        
        public ActionResult UserDevice()
        {
            return View();
        }
        
        public ActionResult ServiceLog()
        {
            return View();
        }
        
        public ActionResult PhoneLog()
        {
            return View();
        }

        public ActionResult UserManage()
        {
            return View();
        }

        public ActionResult StyleDeleteItem(int id)
        {
            Hashtable r = new Hashtable();
            r["r"] = true;
            r["d"] = "删除布局操作成功!";

            Bo.StyleBO bo = Bo.BoFactory.GetStyleBO;
            try
            {
                int styleHomeCnt = bo.StyleHomeCount(id);
                if (styleHomeCnt > 0)
                {
                    r["r"] = false;
                    r["d"] = "布局已关联应用,不能删除,请先取消关联后再删除!";
                }
                else 
                    bo.DeleteStyleItem(id);
            }
            catch (Exception ex)
            {
                r["r"] = false;
                r["d"] = "删除布局操作失败!可能原因:" + ex.Message;
                Log4NetHelper.Error(ex);
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult StyleItemDeleteItem(int StyleID, string App4AIID)
        {
            Hashtable r = new Hashtable();
            r["r"] = true;
            r["d"] = "删除布局设置操作成功!";

            Bo.StyleHomeItemBO bo = Bo.BoFactory.GetStyleHomeItemBO;
            try
            {
                bo.DeleteStyleHomeItem(StyleID, App4AIID);
                //int styleHomeCnt = bo.StyleHomeCount(id);
                //if (styleHomeCnt > 0)
                //{
                //    r["r"] = false;
                //    r["d"] = "布局已关联应用,不能删除,请先取消关联后再删除!";
                //}
                //else
                //    bo.DeleteStyleItem(id);
            }
            catch (Exception ex)
            {
                r["r"] = false;
                r["d"] = "删除布局设置操作失败!可能原因:" + ex.Message;
                Log4NetHelper.Error(ex);
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult StyleDeleteItems(string ids)
        {
            Hashtable r = new Hashtable();
            r["r"] = true;
            r["d"] = "删除布局操作成功!";

            Bo.StyleBO bo = Bo.BoFactory.GetStyleBO;

            if (String.IsNullOrEmpty(ids))
            {
                r["d"] = "参数不正确,未执行删除操作!";
            }
            else
            {
                string strSuccess = "";
                string strFailed = "";
                string strCant = "";
                string[] idlist = ids.Split(",".ToCharArray());
                if (idlist != null)
                foreach (string s in idlist)
                {
                    if (string.IsNullOrEmpty(s))
                        continue;
                    
                        int styleId = int.Parse(s);
                        SmartBox.Console.Common.Entities.Style item = bo.GetEntity(styleId);
                        if (item == null)
                            continue;
                        int styleHomeCnt = bo.StyleHomeCount(styleId);
                        if (styleHomeCnt > 0)
                        {
                            strCant += item.DisplayName + ",";
                        }
                        else
                        {
                            try
                            {
                                bool re = bo.DeleteStyleItem(styleId);
                                if (re)
                                {
                                    r["r"] = true;
                                    strSuccess += item.DisplayName + ",";
                                }
                                else
                                {
                                    strFailed += item.DisplayName + ",";
                                }
                            }
                            catch (Exception ex)
                            {
                                //strFailed += item.DisplayName + ",";
                                Log4NetHelper.Error(ex);
                            }
                        }
                    
                }

                if (!String.IsNullOrEmpty(strSuccess) && strSuccess.EndsWith(","))
                    strSuccess = strSuccess.TrimEnd(",".ToCharArray());
                if (!String.IsNullOrEmpty(strFailed) && strFailed.EndsWith(","))
                    strFailed = strFailed.TrimEnd(",".ToCharArray());
                if (!String.IsNullOrEmpty(strCant) && strCant.EndsWith(","))
                    strCant = strCant.TrimEnd(",".ToCharArray());
                strSuccess += "已成功删除!";
                strFailed += "删除失败!";
                strCant += "布局已关联应用,不能删除,请先取消关联后再删除!";
                r["d"] = strSuccess + strFailed + strCant;
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BackgroundImage()
        {
            return View();
        }
        
        public ActionResult IconManage()
        {
            return View();
        }
        
        public ActionResult EditDevice(string deviceId)
        {
            Device device = Bo.BoFactory.GetDeviceBO.Get(deviceId);
            if (device == null)
            {
                device = new Device();
            }
            return View(device);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditDevice(Device app)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };

            try
            {
                BoFactory.GetDeviceBO.Save(app);
            }
            catch (Exception ex)
            {
                data.Msg = ex.Message;
                data.IsSuccess = false;
                Log4NetHelper.Error(ex);
            }

            return Json(data);
        }

        public ActionResult MobileStyleHomeItemList()
        {
            return View();
        }

        public ActionResult MobileStyleHomeItemAdd(int App4AIID, int StyleID)
        {
            SmartBox.Console.Common.Entities.StyleHomeItem item = null;
            
            if (App4AIID > 0)
            {
                item = Bo.BoFactory.GetStyleHomeItemBO.GetStyleHomeItemEntity(StyleID, App4AIID.ToString());
                //item = dao.GetStyleHomeItem(StyleID, App4AIID.ToString());
            }
            else
            {
                item = new Common.Entities.StyleHomeItem();
            }

            ViewData["entity"] = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            return View();
        }

        public JsonResult StyleHomeItemPost(string StyieID, string App4AIID, string DisplayName, string ImageAddress, string Seq)
        {
            Hashtable r = new Hashtable();
            r["r"] = true;
            r["d"] = "保存成功!";
            try
            {
                Bo.BoFactory.GetStyleHomeItemBO.Save(StyieID, App4AIID, DisplayName, ImageAddress, Seq);
            }
            catch (Exception ex)
            {
                r["r"] = false;
                r["d"] = "保存失败!可能的原因:" + ex.Message;
                Log4NetHelper.Error(ex);
            }
            return Json(r);
        }

        public JsonResult GetApplicationData()
        {
            IList<SmartBox.Console.Common.Entities.Application> result = Bo.BoFactory.GetVersionTrackBo.GetApplications();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserUnLock()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">设备id</param>
        /// <param name="hour">锁定的小时数</param>
        /// <returns></returns>
        public ActionResult LockSelectedDevice(string ids, string hour)
        {
            Hashtable r = new Hashtable();
            r["r"] = true;
            r["d"] = "锁定设备操作成功!";
            string[] idlist = ids.Split(",".ToCharArray());
            if (idlist != null && idlist.Length > 0)
            {
                for (int i = 0; i < idlist.Length; ++i)
                {
                    string id = idlist[i];
                    if (!String.IsNullOrEmpty(id))
                    {
                        try
                        {
                            Bo.BoFactory.GetDeviceBO.LockDevice(id, hour, r);
                        }
                        catch (Exception ex)
                        {
                            Log4NetHelper.Error(ex);
                        }
                    }
                }
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LockDevice(string id, string hour)
        {
            Hashtable r = new Hashtable();
            r["r"] = true;
            r["d"] = "锁定设备操作成功!";

            try
            {
                Bo.BoFactory.GetDeviceBO.LockDevice(id, hour, r);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeviceUnLock()
        {
            return View();
        }
        
        public ActionResult DeviceManage()
        {
            return View();
        }
        
        public ActionResult DeviceUnLock_()
        {
            return View();
        }

        public ActionResult MonitorCmd()
        {
            return View();
        }
        
        public ActionResult DeviceRetryLock()
        {
            IList<GlobalParam> parms = Bo.BoFactory.GetGlobalParamBO.LoadGlobalParam();
            string str = Newtonsoft.Json.JsonConvert.SerializeObject(parms, Newtonsoft.Json.Formatting.Indented);
            ViewData["globalparm"] = str;
            return View();
        }
        
        public ActionResult DeviceRetryLock_()
        {
            return View();
        }
        
        public ActionResult MonitorDefined()
        {
            return View();
        }
        
        public ActionResult MonitorLinkman()
        {
            return View();
        }
        
        public ActionResult UserEnableAuthorization()
        {
            GlobalParam p = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("user_need_auth");
            string user_need_auth = "1";
            if (p != null)
            {
                user_need_auth = p.ConfigValue;
            }
            ViewData["user_need_auth"] = user_need_auth;
            return View();
        }

        public ActionResult UserEnableAuthorization_()
        {
            return View();
        }
        
        public ActionResult UserRetryLock()
        {
            IList<GlobalParam> parms = Bo.BoFactory.GetGlobalParamBO.LoadGlobalParam();
            string str = Newtonsoft.Json.JsonConvert.SerializeObject(parms, Newtonsoft.Json.Formatting.Indented);
            ViewData["globalparm"] = str;
            return View();
        }
        public ActionResult UserRetryLock_()
        {
            return View();
        }

        public ActionResult UserEnableAuthException()
        {
            return View();
        }
        
        public ActionResult UserEnableAuthException_()
        {
            return View();
        }
        
        public ActionResult UserDisableAuthException()
        {
            return View();
        }
        
        public ActionResult UserDisableAuthorization()
        {
            return View();
        }

        public ActionResult UserDisableAuthorization_()
        {
            return View();
        }

        public ActionResult MonitorConfig()
        {
            return View();
        }

        public ActionResult DeviceLost()
        {
            return View();
        }
        
        public ActionResult DeviceLost_()
        {
            return View();
        }

        public ActionResult DeviceSync()
        {
            return View();
        }
        
        public ActionResult DeviceSync_()
        {
            return View();
        }

        public ActionResult AppPackageSync()
        {
            return View();
        }
        
        public ActionResult DeviceLostAdd()
        {
            return View();
        }
        
        public ActionResult MonitorCmdAdd()
        {
            return View();
        }
        
        public ActionResult MonitorLinkmanAdd()
        {
            return View();
        }
        
        public ActionResult BackgroundImageAdd()
        {
            return View();
        }
        
        public ActionResult IconManageAdd()
        {
            return View();
        }
        
        public ActionResult DeviceUser()
        {
            return View();
        }
        
        public ActionResult LogoManageAdd()
        {
            return View();
        }

        public ActionResult DeviceEnableAuthorizationSys()
        {
            GlobalParam p = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("device_need_auth");
            string device_need_auth = "1";
            if (p != null)
            {
                device_need_auth = p.ConfigValue;
            }
            ViewData["device_need_auth"] = device_need_auth;
            return View();
        }
        
        public ActionResult DeviceEnableAuthorizationSys_()
        {
            return View();
        }
        
        public ActionResult MonitorConfigAdd()
        {
            MonitorBO monitorBO = BoFactory.GetMonitorBO;
            Monitor_Config config = null;
            string id = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(id))
                config = monitorBO.Get(int.Parse(id));
            if (config == null)
                config = new Monitor_Config();

            string entity = Newtonsoft.Json.JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented).Replace("\\r\\n", "");
            this.ViewData["entity"] = entity;
            return View(config);
        }

        public ActionResult MonitorDefinedAdd()
        {
            MonitorDefindBO monitorBO = BoFactory.GetMonitorDefindBO;
            Monitor_Defind config = null;
            string id = Request.QueryString["pd_id"];
            if (!String.IsNullOrEmpty(id))
                config = monitorBO.Get(int.Parse(id));
            if (config == null)
                config = new Monitor_Defind();

            string entity = Newtonsoft.Json.JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented).Replace("\\r\\n", "");
            this.ViewData["entity"] = entity;
            return View(config);
        }
        
        public ActionResult MonitorWarning()
        {
            return View();
        }
        
        public ActionResult MonitorLog()
        {
            return View();
        }
        
        public ActionResult C()
        {
            return View();
        }
        
        public ActionResult ManageLog()
        {
            return View();
        }

        public ActionResult SecurityAudit()
        {
            return View();
        }

        public ActionResult UserLoginUnLock()
        {
            return View();
        }

        public ActionResult AppPackageAuthorization()
        {
            return View();
        }
        
        public ActionResult AppPackageAuthorization_()
        {
            return View();
        }

        public ActionResult DeviceAuthorization()
        {
            return View();
        }
        public JsonResult GetTaskCenterList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["taskcate"] = "设备审核";
            device["taskcount"] = "5";
            device["datetime"] = "2014-01-24 13:05:19.000";
            device["taskurl"] = "/Demo/DeviceAuthorization";

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["taskcate"] = "用户启用审核";
            device["taskcount"] = "5";
            device["datetime"] = "2014-01-24 13:05:19.000";
            device["taskurl"] = "/Demo/UserEnableAuthorization";

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["taskcate"] = "用户禁用审核";
            device["taskcount"] = "5";
            device["datetime"] = "2014-01-24 13:05:19.000";
            device["taskurl"] = "/Demo/UserDisableAuthException";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMonitorLogList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 1;
            device["log_df_item"] = "hostmem";
            device["log_monitorvalue"] = "40";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_df_kind"] = "hostsys";
            device["log_df_code"] = "blue";            
            device["log_df_lever"] = "蓝色";
            device["log_status"] = "1";
            device["log_hostip"] = "192.168.200.142";
            device["log_hostname"] = "APP02.rdc.com";

            device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 2;
            device["log_df_item"] = "hostmem";
            device["log_monitorvalue"] = "40";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_df_kind"] = "hostsys";
            device["log_df_code"] = "blue";
            device["log_df_lever"] = "蓝色";
            device["log_status"] = "1";
            device["log_hostip"] = "192.168.200.142";
            device["log_hostname"] = "APP02.rdc.com";

            device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 3;
            device["log_df_item"] = "hostmem";
            device["log_monitorvalue"] = "40";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_df_kind"] = "hostsys";
            device["log_df_code"] = "blue";
            device["log_df_lever"] = "蓝色";
            device["log_status"] = "1";
            device["log_hostip"] = "192.168.200.142";
            device["log_hostname"] = "APP02.rdc.com";

            device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 4;
            device["log_df_item"] = "hostmem";
            device["log_monitorvalue"] = "40";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_df_kind"] = "hostsys";
            device["log_df_code"] = "blue";
            device["log_df_lever"] = "蓝色";
            device["log_status"] = "1";
            device["log_hostip"] = "192.168.200.142";
            device["log_hostname"] = "APP02.rdc.com";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSecurityAuditList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["event"] = "用户登录";
            device["operator"] = "单位管理员";
            device["operator_uid"] = "dwgly";
            device["operator_time"] = "2014-01-24 13:05:19.000";
            device["operator_kind"] = "登陆";
    
            device = new Hashtable();
            devices.Add(device);
            device["id"] = 2;
            device["event"] = "用户登录";
            device["operator"] = "单位管理员";
            device["operator_uid"] = "dwgly";
            device["operator_time"] = "2014-01-24 13:05:19.000";
            device["operator_kind"] = "登陆";
    
            device = new Hashtable();
            devices.Add(device);
            device["id"] = 3;
            device["event"] = "用户登录";
            device["operator"] = "单位管理员";
            device["operator_uid"] = "dwgly";
            device["operator_time"] = "2014-01-24 13:05:19.000";
            device["operator_kind"] = "登陆";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetManageLogList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 1;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "warning";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 2;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "error";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 3;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "info";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppLogList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 1;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "warning";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 2;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "error";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 3;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "info";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetServiceLogList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 1;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "warning";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 2;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "error";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 3;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "info";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPhoneLogList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 1;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "warning";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 2;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "error";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            device = new Hashtable();
            devices.Add(device);
            device["log_id"] = 3;
            device["log_title"] = "hostmem";
            device["log_text"] = "日志内容日志内容日志内容日志内容...";
            device["log_level"] = "info";
            device["log_datetime"] = "2014-01-24 13:05:19.000";
            device["log_kind"] = "管理";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeviceDisableAuthorizationSys()
        {
            return View();
        }
        
        public ActionResult DeviceDisableAuthException()
        {
            return View();
        }
        
        public ActionResult DeviceEnableAuthException()
        {
            return View();
        }
        
        public ActionResult DeviceDisableAuthException_()
        {
            return View();
        }
        
        public ActionResult DeviceEnableAuthException_()
        {
            return View();
        }
        
        public ActionResult MonitorService()
        {
            return View();
        }

        public ActionResult PushService()
        {
            return View();
        }
        
        public ActionResult MonitorServiceAdd()
        {
            return View();
        }

        public ActionResult GlobalParmConfig()
        {
            IList<GlobalParam> parms = Bo.BoFactory.GetGlobalParamBO.LoadGlobalParam();
            string str = Newtonsoft.Json.JsonConvert.SerializeObject(parms, Newtonsoft.Json.Formatting.Indented);
            ViewData["globalparm"] = str;
            return View();
        }

        public JsonResult GetMonitorLinkmanList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["lm_id"] = 1;
            device["lm_uid"] = "redman";
            device["lm_uname"] = "红色警报接收人";
            device["lm_udept"] = @"";
            device["lm_mobile"] = "13761832598";
            device["lm_email"] = "baojie@beyondbit.com";

            device = new Hashtable();
            devices.Add(device);
            device["lm_id"] = 2;
            device["lm_uid"] = "blueman";
            device["lm_uname"] = "蓝色警报接收人";
            device["lm_udept"] = @"";
            device["lm_mobile"] = "13761832598";
            device["lm_email"] = "baojie@beyondbit.com";

            device = new Hashtable();
            devices.Add(device);
            device["lm_id"] = 3;
            device["lm_uid"] = "yellowman";
            device["lm_uname"] = "黄色警报接收人";
            device["lm_udept"] = @"";
            device["lm_mobile"] = "13761832598";
            device["lm_email"] = "baojie@beyondbit.com";
            

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMonitorDefinedList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["df_id"] = 1;
            device["df_kind"] = "hostsys";
            device["df_item"] = "hostcpu";
            device["df_maxvalue"] = @"20";
            device["df_minvalue"] = "10";
            device["df_lever"] = "蓝色";
            device["df_code"] = "blue";
            device["df_sendway"] = "sms";
            device["df_receptman"] = "2,1,3";
            device["df_startsenddate"] = "09:00:00";
            device["df_endsenddate"] = "17:00:00";
            device["df_issend"] = "y";

            device = new Hashtable();
            devices.Add(device);
            device["df_id"] = 2;
            device["df_kind"] = "hostsys";
            device["df_item"] = "hostcpu";
            device["df_maxvalue"] = @"20";
            device["df_minvalue"] = "10";
            device["df_lever"] = "蓝色";
            device["df_code"] = "blue";
            device["df_sendway"] = "sms";
            device["df_receptman"] = "2,1,3";
            device["df_startsenddate"] = "09:00:00";
            device["df_endsenddate"] = "17:00:00";
            device["df_issend"] = "y";

            device = new Hashtable();
            devices.Add(device);
            device["df_id"] = 3;
            device["df_kind"] = "hostsys";
            device["df_item"] = "hostcpu";
            device["df_maxvalue"] = @"20";
            device["df_minvalue"] = "10";
            device["df_lever"] = "蓝色";
            device["df_code"] = "blue";
            device["df_sendway"] = "sms";
            device["df_receptman"] = "2,1,3";
            device["df_startsenddate"] = "09:00:00";
            device["df_endsenddate"] = "17:00:00";
            device["df_issend"] = "y";

            device = new Hashtable();
            devices.Add(device);
            device["df_id"] = 4;
            device["df_kind"] = "hostsys";
            device["df_item"] = "hostcpu";
            device["df_maxvalue"] = @"20";
            device["df_minvalue"] = "10";
            device["df_lever"] = "蓝色";
            device["df_code"] = "blue";
            device["df_sendway"] = "sms";
            device["df_receptman"] = "2,1,3";
            device["df_startsenddate"] = "09:00:00";
            device["df_endsenddate"] = "17:00:00";
            device["df_issend"] = "y";

            device = new Hashtable();
            devices.Add(device);
            device["df_id"] = 5;
            device["df_kind"] = "hostsys";
            device["df_item"] = "hostcpu";
            device["df_maxvalue"] = @"20";
            device["df_minvalue"] = "10";
            device["df_lever"] = "蓝色";
            device["df_code"] = "blue";
            device["df_sendway"] = "sms";
            device["df_receptman"] = "2,1,3";
            device["df_startsenddate"] = "09:00:00";
            device["df_endsenddate"] = "17:00:00";
            device["df_issend"] = "y";

            device = new Hashtable();
            devices.Add(device);
            device["df_id"] = 6;
            device["df_kind"] = "hostsys";
            device["df_item"] = "hostcpu";
            device["df_maxvalue"] = @"20";
            device["df_minvalue"] = "10";
            device["df_lever"] = "蓝色";
            device["df_code"] = "blue";
            device["df_sendway"] = "sms";
            device["df_receptman"] = "2,1,3";
            device["df_startsenddate"] = "09:00:00";
            device["df_endsenddate"] = "17:00:00";
            device["df_issend"] = "y";
            

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ServiceConfig()
        {
            return View();
        }

        public JsonResult GetMonitorConfigList(string hostname, string updatestatus, string isuse, string enalbe_time_start, string enalbe_time_end, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetMonitorBO.GetMonitorConfigList(hostname, updatestatus, isuse, enalbe_time_start,enalbe_time_end, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetMonitorConfigList_()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["cfg_id"] = 1;
            device["cfg_hostname"] = "APP02.rdc.com";
            device["cfg_hostip"] = "192.168.200.145";
            device["cfg_file"] = @"<Monitors>
  <CfgFileName>App.Xml</CfgFileName>
  <SBSApp ProcessName=""Beyondbit.SmartBox.Host.WinService"" ListenPort=""6061"" />
  <SBESApp ProcessName=""Beyondbit.SmartBox.EdgeHost.WinService"" />
  <Host Refresh=""3000"">
    <cpu>3000</cpu>
    <mem>3000</mem>
  </Host>
  <SBS Refresh=""5000"">
    <cpu>5000</cpu>
    <mem>5000</mem>
    <tcp>3000</tcp>
  </SBS>
  <SBES Refresh=""5000"">
    <cpu>5000</cpu>
    <mem>5000</mem>
  </SBES>
  <SBSReset>
    <RstItvVal>1</RstItvVal>
    <RstItvUnit>m</RstItvUnit>
    <SBSSvcName>SmartBoxHost</SBSSvcName>
  </SBSReset>
  <AlmSender>
    <MailSender>http://MailSendSvc</MailSender>
    <SMSSender>http://SmsSendSvc</SMSSender>
    <TryCount>3</TryCount>
  </AlmSender>
  <MemAlarmList ReadInterval=""300000"" />
  <DBAlarmList WriteInterval=""300000"" ReadInterval=""300000"" />
  <RemoteCommands>
    <DownLoadConfig>dlcfg</DownLoadConfig>
    <ResetSvc>rstsvc</ResetSvc>
    <KillProcess>kp</KillProcess>
    <Unblocked>sbsub</Unblocked>
  </RemoteCommands>
</Monitors>";
            device["cfg_createdate"] = "2014-01-24 13:10:56.000";
            device["cfg_createman"] = "rocky";
            device["cfg_updatedate"] = "2014-01-24 13:10:56";
            device["cfg_updateman"] = "192.168.200.100";
            device["cfg_updatestatus"] = "成功";
            device["cfg_isuse"] = "0";
            device["cfg_usedate"] = "2014-01-24 13:10:56.000";
            device["cfgid"] = 1;


            device = new Hashtable();
            devices.Add(device);
            device["cfg_id"] = 2;
            device["cfg_hostname"] = "APP02.rdc.com";
            device["cfg_hostip"] = "192.168.200.145";
            device["cfg_file"] = @"<Monitors>
  <CfgFileName>App.Xml</CfgFileName>
  <SBSApp ProcessName=""Beyondbit.SmartBox.Host.WinService"" ListenPort=""6061"" />
  <SBESApp ProcessName=""Beyondbit.SmartBox.EdgeHost.WinService"" />
  <Host Refresh=""3000"">
    <cpu>3000</cpu>
    <mem>3000</mem>
  </Host>
  <SBS Refresh=""5000"">
    <cpu>5000</cpu>
    <mem>5000</mem>
    <tcp>3000</tcp>
  </SBS>
  <SBES Refresh=""5000"">
    <cpu>5000</cpu>
    <mem>5000</mem>
  </SBES>
  <SBSReset>
    <RstItvVal>1</RstItvVal>
    <RstItvUnit>m</RstItvUnit>
    <SBSSvcName>SmartBoxHost</SBSSvcName>
  </SBSReset>
  <AlmSender>
    <MailSender>http://MailSendSvc</MailSender>
    <SMSSender>http://SmsSendSvc</SMSSender>
    <TryCount>3</TryCount>
  </AlmSender>
  <MemAlarmList ReadInterval=""300000"" />
  <DBAlarmList WriteInterval=""300000"" ReadInterval=""300000"" />
  <RemoteCommands>
    <DownLoadConfig>dlcfg</DownLoadConfig>
    <ResetSvc>rstsvc</ResetSvc>
    <KillProcess>kp</KillProcess>
    <Unblocked>sbsub</Unblocked>
  </RemoteCommands>
</Monitors>";
            device["cfg_createdate"] = "2014-01-24 13:10:56.000";
            device["cfg_createman"] = "rocky";
            device["cfg_updatedate"] = "2014-01-24 13:10:56";
            device["cfg_updateman"] = "192.168.200.100";
            device["cfg_updatestatus"] = "成功";
            device["cfg_isuse"] = "0";
            device["cfg_usedate"] = "2014-01-24 13:10:56.000";
            device["cfgid"] = 2;

            
            

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMonitorServiceList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["title"] = "应用同步服务";
            device["path"] = "d:/abc/svc";
            device["ip"] = "192.168.200.144";
            device["createddate"] = "2014-01-24 13:10:56.000";
            device["updatedate"] = "2014-01-24 13:10:56.000";
            device["desc"] = "local";
            device["status"] = "1";

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 2;
            device["title"] = "应用同步服务";
            device["path"] = "d:/abc/svc";
            device["ip"] = "192.168.200.144";
            device["createddate"] = "2014-01-24 13:10:56.000";
            device["updatedate"] = "2014-01-24 13:10:56.000";
            device["desc"] = "local";
            device["status"] = "1";

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 3;
            device["title"] = "应用同步服务";
            device["path"] = "d:/abc/svc";
            device["ip"] = "192.168.200.144";
            device["createddate"] = "2014-01-24 13:10:56.000";
            device["updatedate"] = "2014-01-24 13:10:56.000";
            device["desc"] = "local";
            device["status"] = "1";
            

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PushServiceAdd()
        {
            return View();
        }

        public JsonResult GetPushServiceList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["title"] = "推送同步服务";
            device["path"] = "d:/abc/svc";
            device["ip"] = "192.168.200.144";
            device["createddate"] = "2014-01-24 13:10:56.000";
            device["updatedate"] = "2014-01-24 13:10:56.000";
            device["desc"] = "local";
            device["status"] = "1";

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 2;
            device["title"] = "推送同步服务";
            device["path"] = "d:/abc/svc";
            device["ip"] = "192.168.200.144";
            device["createddate"] = "2014-01-24 13:10:56.000";
            device["updatedate"] = "2014-01-24 13:10:56.000";
            device["desc"] = "local";
            device["status"] = "1";

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 3;
            device["title"] = "推送同步服务";
            device["path"] = "d:/abc/svc";
            device["ip"] = "192.168.200.144";
            device["createddate"] = "2014-01-24 13:10:56.000";
            device["updatedate"] = "2014-01-24 13:10:56.000";
            device["desc"] = "local";
            device["status"] = "1";
            

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetMonitorCmdList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["cmd_id"] = 1;
            device["cmd_title"] = "加载配置";
            device["cmd_code"] = "1";
            device["cmd_senddate"] = "2014-01-24 13:10:56.000";
            device["cmd_excudedate"] = "2014-01-24 13:10:56.000";
            device["cmd_excuderesult"] = "成功";
            device["cmd_hostname"] = "local";
            device["cmd_hostip"] = "192.168.200.100";
            device["cmd_discription"] = "13761832598";
            device["cmdid"] = 1;

            device = new Hashtable();
            devices.Add(device);
            device["cmd_id"] = 2;
            device["cmd_title"] = "重启服务";
            device["cmd_code"] = "2";
            device["cmd_senddate"] = "2014-01-24 13:10:56.000";
            device["cmd_excudedate"] = "2014-01-24 13:10:56.000";
            device["cmd_excuderesult"] = "成功";
            device["cmd_hostname"] = "local";
            device["cmd_hostip"] = "192.168.200.100";
            device["cmd_discription"] = "13761832598";
            device["cmdid"] = 2;

            device = new Hashtable();
            devices.Add(device);
            device["cmd_id"] = 3;
            device["cmd_title"] = "关闭进程";
            device["cmd_code"] = "3";
            device["cmd_senddate"] = "2014-01-24 13:10:56.000";
            device["cmd_excudedate"] = "2014-01-24 13:10:56.000";
            device["cmd_excuderesult"] = "成功";
            device["cmd_hostname"] = "local";
            device["cmd_hostip"] = "192.168.200.100";
            device["cmd_discription"] = "13761832598";
            device["cmdid"] = 3;

           
            

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMonitorWarningList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["sw_id"] = 1;
            device["sw_log_id"] = "";
            device["sw_senddate"] = "2014-01-24 13:10:56";
            device["sw_sendresult"] = "成功";
            device["sw_receptman"] = "蓝色警报接收人";
            device["sw_sendway"] = "sms";
            device["sw_createdate"] = "2014-01-24 13:10:56.000";
            device["sw_lastsenddate"] = "2014-01-24";
            device["sw_mobile"] = "13761832598";
            device["sw_email"] = "baojie@beyondbit.com";
            device["swid"] = 1;
            
            device = new Hashtable();
            devices.Add(device);
            device["sw_id"] = 2;
            device["sw_log_id"] = "";
            device["sw_senddate"] = "2014-01-24 13:10:56";
            device["sw_sendresult"] = "成功";
            device["sw_receptman"] = "红色警报接收人";
            device["sw_sendway"] = "sms";
            device["sw_createdate"] = "2014-01-24 13:10:56.000";
            device["sw_lastsenddate"] = "2014-01-24";
            device["sw_mobile"] = "13761832598";
            device["sw_email"] = "baojie@beyondbit.com";
            device["swid"] = 2;
            
            device = new Hashtable();
            devices.Add(device);
            device["sw_id"] = 3;
            device["sw_log_id"] = "";
            device["sw_senddate"] = "2014-01-24 13:10:56";
            device["sw_sendresult"] = "成功";
            device["sw_receptman"] = "黄色警报接收人";
            device["sw_sendway"] = "sms";
            device["sw_createdate"] = "2014-01-24 13:10:56.000";
            device["sw_lastsenddate"] = "2014-01-24";
            device["sw_mobile"] = "13761832598";
            device["sw_email"] = "baojie@beyondbit.com";
            device["swid"] = 3;
            
            device = new Hashtable();
            devices.Add(device);
            device["sw_id"] = 4;
            device["sw_log_id"] = "";
            device["sw_senddate"] = "2014-01-24 13:10:56";
            device["sw_sendresult"] = "成功";
            device["sw_receptman"] = "蓝色警报接收人";
            device["sw_sendway"] = "sms";
            device["sw_createdate"] = "2014-01-24 13:10:56.000";
            device["sw_lastsenddate"] = "2014-01-24";
            device["sw_mobile"] = "13761832598";
            device["sw_email"] = "baojie@beyondbit.com";
            device["swid"] = 4;
            
            device = new Hashtable();
            devices.Add(device);
            device["sw_id"] = 5;
            device["sw_log_id"] = "";
            device["sw_senddate"] = "2014-01-24 13:10:56";
            device["sw_sendresult"] = "失败";
            device["sw_receptman"] = "红色警报接收人";
            device["sw_sendway"] = "sms";
            device["sw_createdate"] = "2014-01-24 13:10:56.000";
            device["sw_lastsenddate"] = "2014-01-24";
            device["sw_mobile"] = "13761832598";
            device["sw_email"] = "baojie@beyondbit.com";
            device["swid"] = 5;
            
            device = new Hashtable();
            devices.Add(device);
            device["sw_id"] = 6;
            device["sw_log_id"] = "";
            device["sw_senddate"] = "2014-01-24 13:10:56";
            device["sw_sendresult"] = "失败";
            device["sw_receptman"] = "黄色警报接收人";
            device["sw_sendway"] = "sms";
            device["sw_createdate"] = "2014-01-24 13:10:56.000";
            device["sw_lastsenddate"] = "2014-01-24";
            device["sw_mobile"] = "13761832598";
            device["sw_email"] = "baojie@beyondbit.com";
            device["swid"] = 6;


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        

        public JsonResult GetAppPackageSyncList(string appName, string application, string unitcode, string auth_time_start, string auth_time_end, string syncstatus, string orderby, int pageIndex, int pageSize)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetAppPackageSyncList(appName, application, unitcode, auth_time_start, auth_time_end, syncstatus, orderby, pageIndex, pageSize);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetApplicationExtList(string appName, string application, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, string categoryID, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetApplicationExtList(appName, application, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, categoryID, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ServiceConfigAdd(string key)
        {
            SystemConfig config = null;
            
            if (!String.IsNullOrEmpty(key))
            {
                List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                pars.Add(new KeyValuePair<string, object>("[key]", key));
                config = Bo.BoFactory.GetSystemConfigBO.Get(pars);                
            }

            if (config == null)
            {
                config = new SystemConfig();
                config.Key = "";
                config.Value = "";
            }

            string s = Newtonsoft.Json.JsonConvert.SerializeObject(config);
            this.ViewData["entity"] = s;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ServiceConfigAddPost(string key, string val)
        {
            Hashtable result = new Hashtable();
            bool r = Bo.BoFactory.GetSystemConfigBO.SaveSystemConfig(key, val, result);         

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult MonitorConfigAddPost(int cfg_id, string cfg_hostname, string cfg_hostip, string cfg_file, string cfg_isuse)
        {
            Hashtable result = new Hashtable();
            Monitor_Config config = new Monitor_Config();
            config.cfg_id = cfg_id;
            config.cfg_hostname = cfg_hostname;
            config.cfg_hostip = cfg_hostip;
            config.cfg_file = Server.HtmlDecode(cfg_file);
            config.cfg_file = System.Text.RegularExpressions.Regex.Replace(config.cfg_file, ">[\\s]+", ">");
            config.cfg_file = System.Text.RegularExpressions.Regex.Replace(config.cfg_file, "[\\s]+<", "<");
            config.cfg_file = System.Text.RegularExpressions.Regex.Replace(config.cfg_file, "[\\s]+", " ");
            config.cfg_isuse = cfg_isuse;
            config.cfg_updatedate = DateTime.Now;
            config.cfg_updateman = CurrentUser.FullName;
            if (config.cfg_id == 0)
            {
                config.cfg_createdate = DateTime.Now;
                config.cfg_createman = CurrentUser.FullName;
            }

            if (config.cfg_isuse == "1")
                config.cfg_usedate = DateTime.Now;
            bool r = Bo.BoFactory.GetMonitorBO.Save(config) > 0;
            result["d"] = r ? "保存成功!" : "保存失败!";
            result["r"] = r;

            return Json(result);
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DelConfig(string key)
        {
            Hashtable result = new Hashtable();
            List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
            pars.Add(new KeyValuePair<string, object>("[key]", key));
            SystemConfig config = Bo.BoFactory.GetSystemConfigBO.Get(pars);

            int i = Bo.BoFactory.GetSystemConfigBO.Delete(config);
            result["r"] = true;
            result["d"] = "删除成功！";

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DelMonitorConfig(string id)
        {
            Hashtable result = new Hashtable();

            Monitor_Config config = Bo.BoFactory.GetMonitorBO.Get(int.Parse(id));

            Bo.BoFactory.GetMonitorBO.Delete(config);
            result["r"] = true;
            result["d"] = "删除成功！";

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DelMonitorConfigSelected(string ids)
        {
            Hashtable result = new Hashtable();
            string[] idlist = ids.Split(",".ToCharArray());
            if (idlist != null && idlist.Length > 0)
            {
                foreach (string key in idlist)
                {
                    if (!String.IsNullOrEmpty(key))
                    {
                        Monitor_Config config = Bo.BoFactory.GetMonitorBO.Get(int.Parse(key));

                        Bo.BoFactory.GetMonitorBO.Delete(config);
                    }
                }
            }
            result["r"] = true;
            result["d"] = "删除成功！";

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ResetConfigs()
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "重置客户端配置成功！";

            Bo.BoFactory.GetSystemConfigBO.ResetConfigs();

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ResetClientVer()
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "重置客户端版本成功！";

            Bo.BoFactory.GetSystemConfigBO.ResetClientVer();

            return Json(result);
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DelConfigs(string ids)
        {
            Hashtable result = new Hashtable();
            string[] idlist = ids.Split(",".ToCharArray());
            if (idlist != null && idlist.Length > 0)
            {
                foreach (string key in idlist)
                {
                    if (!String.IsNullOrEmpty(key))
                    {
                        
                        List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                        pars.Add(new KeyValuePair<string, object>("[key]", key));
                        SystemConfig config = Bo.BoFactory.GetSystemConfigBO.Get(pars);

                        int i = Bo.BoFactory.GetSystemConfigBO.Delete(config);
                    }
                }
            }
            result["r"] = true;
            result["d"] = "删除成功！";

            return Json(result);
        }
        //public JsonResult GetAppPackageAuthorizationList()
        //{
        //    Hashtable result = new Hashtable();
        //    result["total"] = 6;

        //    List<Hashtable> devices = new List<Hashtable>();
        //    result["rows"] = devices;

        //    Hashtable device = new Hashtable();
        //    devices.Add(device);
        //    device["id"] = 1;
        //    device["appname"] = "通讯录";
        //    device["packagename"] = "Contact苹果版";
        //    device["submit_time"] = "2014-07-23 16:09";
        //    device["clienttype"] = "pad/ios";
        //    device["cate"] = "办公系统";
        //    device["tj"] = "是";
        //    device["bb"] = "否";
        //    device["status"] = "待审核";
        //    device["id2"] = 1;

        //    device = new Hashtable();
        //    devices.Add(device);
        //    device["id"] = 2;
        //    device["appname"] = "通讯录";
        //    device["packagename"] = "Contact苹果版";
        //    device["submit_time"] = "2014-07-23 16:09";
        //    device["clienttype"] = "pad/ios";
        //    device["cate"] = "办公系统";
        //    device["tj"] = "是";
        //    device["bb"] = "否";
        //    device["status"] = "待审核";
        //    device["id2"] = 2;

        //    device = new Hashtable();
        //    devices.Add(device);
        //    device["id"] = 3;
        //    device["appname"] = "通讯录";
        //    device["packagename"] = "Contact苹果版";
        //    device["submit_time"] = "2014-07-23 16:09";
        //    device["clienttype"] = "pad/ios";
        //    device["cate"] = "办公系统";
        //    device["tj"] = "是";
        //    device["bb"] = "否";
        //    device["status"] = "待审核";
        //    device["id2"] = 3;

        //    device = new Hashtable();
        //    devices.Add(device);
        //    device["id"] = 4;
        //    device["appname"] = "通讯录";
        //    device["packagename"] = "Contact苹果版";
        //    device["submit_time"] = "2014-07-23 16:09";
        //    device["clienttype"] = "pad/ios";
        //    device["cate"] = "办公系统";
        //    device["tj"] = "是";
        //    device["bb"] = "否";
        //    device["status"] = "待审核";
        //    device["id2"] = 4;

        //    device = new Hashtable();
        //    devices.Add(device);
        //    device["id"] = 5;
        //    device["appname"] = "通讯录";
        //    device["packagename"] = "Contact苹果版";
        //    device["submit_time"] = "2014-07-23 16:09";
        //    device["clienttype"] = "pad/ios";
        //    device["cate"] = "办公系统";
        //    device["tj"] = "是";
        //    device["bb"] = "否";
        //    device["status"] = "待审核";
        //    device["id2"] = 5;

        //    device = new Hashtable();
        //    devices.Add(device);
        //    device["id"] = 6;
        //    device["appname"] = "通讯录";
        //    device["packagename"] = "Contact苹果版";
        //    device["submit_time"] = "2014-07-23 16:09";
        //    device["clienttype"] = "pad/ios";
        //    device["cate"] = "办公系统";
        //    device["tj"] = "是";
        //    device["bb"] = "否";
        //    device["status"] = "待审核";
        //    device["id2"] = 6;



            


        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetDeviceSyncList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["sync_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 1;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 2;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["sync_time"] = "2014-07-29 10:20";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 2;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 3;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["sync_time"] = "2014-07-29 10:20";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 3;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 4;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["sync_time"] = "2014-07-29 10:20";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 4;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 5;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["sync_time"] = "2014-07-29 10:20";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 5;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 6;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["sync_time"] = "2014-07-29 10:20";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 6;


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeviceUnlockList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["lock_time"] = "2014-07-29 10:20";
            device["unlock_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 1;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 2;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["lock_time"] = "2014-07-29 10:20";
            device["unlock_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 2;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 3;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["lock_time"] = "2014-07-29 10:20";
            device["unlock_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 3;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 4;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["lock_time"] = "2014-07-29 10:20";
            device["unlock_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 4;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 5;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["lock_time"] = "2014-07-29 10:20";
            device["unlock_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 5;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 6;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["lock_time"] = "2014-07-29 10:20";
            device["unlock_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 6;

            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeviceInfoList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 1;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 2;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 2;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 3;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 3;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 4;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 4;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 5;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 5;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 6;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "锁定";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 6;

            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeviceAuthorizationList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["submit_time"] = "2014-07-29 10:20";
            device["auth_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 1;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 2;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["submit_time"] = "2014-07-29 10:20";
            device["auth_time"] = "2014-07-29 10:20";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 2;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 3;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["submit_time"] = "2014-07-29 10:20";
            device["auth_time"] = "2014-07-29 10:20";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 3;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 4;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["submit_time"] = "2014-07-29 10:20";
            device["auth_time"] = "2014-07-29 10:20";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 4;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 5;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["submit_time"] = "2014-07-29 10:20";
            device["auth_time"] = "2014-07-29 10:20";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 5;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 6;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["submit_time"] = "2014-07-29 10:20";
            device["auth_time"] = "2014-07-29 10:20";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 6;


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeviceLostList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已挂失";
            device["lost_time"] = "2014-07-29 10:20";
            device["unlost_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 1;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 2;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已挂失";
            device["lost_time"] = "2014-07-29 10:20";
            device["unlost_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 2;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 3;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已挂失";
            device["lost_time"] = "2014-07-29 10:20";
            device["unlost_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 3;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 4;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已挂失";
            device["lost_time"] = "2014-07-29 10:20";
            device["unlost_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 4;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 5;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已挂失";
            device["lost_time"] = "2014-07-29 10:20";
            device["unlost_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 5;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 6;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已挂失";
            device["lost_time"] = "2014-07-29 10:20";
            device["unlost_time"] = "2014-07-29 10:20";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 6;


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetDeviceEnableAuthorizationSysList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 1;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 2;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 2;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 3;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 3;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 4;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 4;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 5;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 5;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 6;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 6;


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeviceDisableAuthorizationSysList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;

            List<Hashtable> devices = new List<Hashtable>();
            result["rows"] = devices;

            Hashtable device = new Hashtable();
            devices.Add(device);
            device["id"] = 1;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 1;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 2;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 2;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 3;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 3;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 4;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 4;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 5;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 5;

            device = new Hashtable();
            devices.Add(device);
            device["id"] = 6;
            device["useruid"] = "zhangm";
            device["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            device["device_status"] = "已审核";
            device["desc"] = "Canius 的 iPhone";
            device["id2"] = 6;


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetUserUnlockList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;
            
            List<Hashtable> users = new List<Hashtable>();
            result["rows"] = users;

            Hashtable user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 1;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "启用";
            user["lock_time"] = "2014-07-29 10:20";
            user["unlock_time"] = "2014-07-29 10:20";
            user["created_time"] = "2014-07-29 10:20";
            user["updated_time"] = "2014-07-29 10:20";
            user["pdid"] = 1;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 2;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "启用";
            user["lock_time"] = "2014-07-29 10:20";
            user["unlock_time"] = "2014-07-29 10:20";
            user["created_time"] = "2014-07-29 10:20";
            user["updated_time"] = "2014-07-29 10:20";
            user["pdid"] = 2;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 3;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "启用";
            user["lock_time"] = "2014-07-29 10:20";
            user["unlock_time"] = "2014-07-29 10:20";
            user["created_time"] = "2014-07-29 10:20";
            user["updated_time"] = "2014-07-29 10:20";
            user["pdid"] = 3;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 4;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "启用";
            user["lock_time"] = "2014-07-29 10:20";
            user["unlock_time"] = "2014-07-29 10:20";
            user["created_time"] = "2014-07-29 10:20";
            user["updated_time"] = "2014-07-29 10:20";
            user["pdid"] = 4;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 5;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "启用";
            user["lock_time"] = "2014-07-29 10:20";
            user["unlock_time"] = "2014-07-29 10:20";
            user["created_time"] = "2014-07-29 10:20";
            user["updated_time"] = "2014-07-29 10:20";
            user["pdid"] = 5;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 6;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "启用";
            user["lock_time"] = "2014-07-29 10:20";
            user["unlock_time"] = "2014-07-29 10:20";
            user["created_time"] = "2014-07-29 10:20";
            user["updated_time"] = "2014-07-29 10:20";
            user["pdid"] = 6;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserAuthorizationList(string uid, string uname, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationEx<SmartBox.Console.Common.Entities.SMC_User> items = Bo.BoFactory.GetSMC_UserBo.GetUserAuthorizationList(uid, uname, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, lockStatus, enabledStatus, authStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);            
        }

        public JsonResult GetUserComparerationList(string uid, string uname, string u_unitcode, string u_compare_time_start, string u_compare_time_end, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationEx<SmartBox.Console.Common.Entities.SMC_User> items = Bo.BoFactory.GetSMC_UserBo.GetUserComparerationList(uid, uname, u_unitcode, u_compare_time_start,u_compare_time_end, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);            
        }

        public JsonResult GetServiceConfigList(string key, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationEx<SmartBox.Console.Common.Entities.SystemConfig> items = Bo.BoFactory.GetCommonBO.GetServiceConfigList(key, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);            
        }

        public JsonResult GetUserManageList(string uid, string uname, string u_unitcode, string u_disable_time_start, string u_disable_time_end, string u_enable_time_start, string u_enable_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationEx<SmartBox.Console.Common.Entities.SMC_User> items = Bo.BoFactory.GetSMC_UserBo.GetUserManageList(uid, uname, u_unitcode, u_disable_time_start, u_disable_time_end, u_enable_time_start, u_enable_time_end, lockStatus, enabledStatus, authStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetUserInfoManageList(string uid, string uname, string u_unitcode, string u_disable_time_start, string u_disable_time_end, string u_enable_time_start, string u_enable_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationEx<SmartBox.Console.Common._OnLineUser> items = Bo.BoFactory.GetSMC_UserBo.GetUserInfoManageList(uid, uname, u_unitcode, u_disable_time_start, u_disable_time_end, u_enable_time_start, u_enable_time_end, lockStatus, enabledStatus, authStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetUserDeviceOnline(string uid, string uname, string u_unitcode, string u_disable_time_start, string u_disable_time_end, string u_enable_time_start, string u_enable_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationEx<UserOnline> items = Bo.BoFactory.GetSMC_UserBo.GetUserDeviceOnline(uid, uname, u_unitcode, u_disable_time_start, u_disable_time_end, u_enable_time_start, u_enable_time_end, lockStatus, enabledStatus, authStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetPrivilegeManageList(string app, string unitcode, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetPrivilegeManageList(app, unitcode, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet); 
        }
        
        public JsonResult GetApplicationManageList(string app, string unitcode, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetApplicationManageList(app, unitcode, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult GetUserLoginUnLockList(string uid, string uname, string u_unitcode, string u_lock_time_start, string u_lock_time_end, string u_lock_expire_time_start, string u_lock_expire_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationEx<SmartBox.Console.Common.Entities.SMC_User> items = Bo.BoFactory.GetSMC_UserBo.GetUserLoginUnLockList(uid, uname, u_unitcode, u_lock_time_start, u_lock_time_end, u_lock_expire_time_start, u_lock_expire_time_end, lockStatus, enabledStatus, authStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeviceAuthorization(string uid, string deviceid, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetDeviceAuthorization(uid, deviceid, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeviceLost(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetDeviceLost(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetDeviceSync(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetDeviceSync(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeviceManage(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, int deviceUserStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetDeviceManage(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, deviceUserStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetTimeIndex(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetTimeIndex(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetStatisticsByUnit(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetStatisticsByUnit(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetUserDevice(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetUserDevice(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnImportedAppPackages(string name, string displayName, string clientType, string lost_time_start, string lost_time_end, string type, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetUnImportedAppPackages(name, displayName, clientType, lost_time_start, lost_time_end, type, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetDeviceUnLock(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetDeviceUnLock(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeviceAuthorizationNew(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetDeviceAuthorizationNew(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetDeviceUser(string uid, string model, string deviceid, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetDeviceUser(uid, model, deviceid, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetDeviceEnableAuthorizationSys(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetDeviceEnableAuthorizationSys(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppPackageAuthorization(string appName, string application, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, string categoryID, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetAppPackageAuthorization(appName, application, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, categoryID, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApplicationCategory()
        {
            var o = Bo.BoFactory.GetCommonBO.GetApplicationCategory();
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetTaskCenter(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            List<IDictionary<string, object>> items = Bo.BoFactory.GetCommonBO.GetTaskCenter(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items;
            result["total"] = 4;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeviceRetryLock(string uid, string model, string u_unitcode, string u_lock_time_start, string u_lock_time_end, int deviceStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetDeviceBO.GetDeviceRetryLock(uid, model, u_unitcode, u_lock_time_start, u_lock_time_end, deviceStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        /*
        public JsonResult GetUserEnableAuthorizationSys(string uid, string username, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetSMC_UserBo.GetUserEnableAuthorizationSys(uid, username, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }*/
        
        public JsonResult GetSelectUser(string uid, string username, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetSMC_UserBo.GetSelectUser(uid, username, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserRetryLock(string uid, string username, string u_unitcode, string u_lock_time_start, string u_lock_time_end, int lockStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetSMC_UserBo.GetUserRetryLock(uid, username, u_unitcode, u_lock_time_start, u_lock_time_end, lockStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetDeviceEnableAuthorizationException(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetDeviceExceptionBO.GetDeviceEnableAuthorizationException(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectUser()
        {
            return View();
        }
        
        public JsonResult GetUserEnableAuthorizationException(string uid, string username, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetSMC_UserExceptionBo.GetUserEnableAuthorizationException(uid, username, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetUserDisableAuthorizationException(string uid, string username, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetSMC_UserExceptionBo.GetUserDisableAuthorizationException(uid, username, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetDeviceDisableAuthorizationException(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetDeviceExceptionBO.GetDeviceDisableAuthorizationException(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetDeviceDisableAuthorizationSys(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SmartBox.Console.Common.SelectPagnationExDictionary items = Bo.BoFactory.GetCommonBO.GetDeviceDisableAuthorizationSys(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Result;
            result["total"] = items.RecordCount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PassSelectedUser(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string uid in idlist)
            {
                if (String.IsNullOrEmpty(uid))
                    continue;

                Bo.BoFactory.GetSMC_UserBo.PassUser(uid);                
            }

            return Json(result);
        }

        public JsonResult UnLockSelectedUser(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string uid in idlist)
            {
                if (String.IsNullOrEmpty(uid))
                    continue;

                Bo.BoFactory.GetSMC_UserBo.UnLockUser(uid);
                Bo.BoFactory.GetUserInfoBO.UnLockUser(uid);
            }

            return Json(result);
        }
        
        public JsonResult LockSelectedUser(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string uid in idlist)
            {
                if (String.IsNullOrEmpty(uid))
                    continue;

                Bo.BoFactory.GetSMC_UserBo.LockUser(uid);
                Bo.BoFactory.GetUserInfoBO.LockUser(uid);
            }

            return Json(result);
        }

        public JsonResult KickoutSelected(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            string[] idlist = ids.Split(",".ToCharArray());

            SmartBox.Console.Service.ServiceReference1.ManagerServiceClient client = new SmartBox.Console.Service.ServiceReference1.ManagerServiceClient();
            string successStr = "";
            int successCnt = 0;
            string failedStr = "";
            int failedCnt = 0;
            foreach (string uid in idlist)
            {
                if (String.IsNullOrEmpty(uid))
                    continue;

                Hashtable r = new Hashtable();
                Bo.BoFactory.GetSMC_UserBo.KickoutUser(uid, r, client);
                if ((bool)r["r"]) {
                    successStr += uid + "、";
                    successCnt++;
                } else {
                    failedStr += uid + "、";
                    failedCnt++;
                }

                
                //Bo.BoFactory.GetSMC_UserBo.PassUser(uid);                
            }
            if (successCnt > 0)
                result["d"] += successStr + "已强制退出！ ";
            
            if (failedCnt > 0)
                result["d"] += failedStr + "强制退出失败！";

            return Json(result);
        }

        public JsonResult NotPassSelectedUser(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string uid in idlist)
            {
                if (String.IsNullOrEmpty(uid))
                    continue;

                Bo.BoFactory.GetSMC_UserBo.NotPassUser(uid);
            }

            return Json(result);
        }

        public JsonResult NotPassSelectedDevice(string ids, string refuse_msg)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());

            GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("disable_device_after_notpass_device");
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                Bo.BoFactory.GetDeviceUserApplyBO.NotPassDevice(id, CurrentUser.UserUId, parm, refuse_msg);
            }

            return Json(result);
        }
        
        public JsonResult DeleteSelectedEnableDeviceException(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                Bo.BoFactory.GetDeviceExceptionBO.DeleteEnableDeviceException(id, CurrentUser.UserUId);
            }

            return Json(result);
        }
        
        public JsonResult DeleteSelectedEnableUserException(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                Bo.BoFactory.GetSMC_UserExceptionBo.DeleteEnableUserException(id, CurrentUser.UserUId);
            }

            return Json(result);
        }
        
        public JsonResult DeleteSelectedDisableUserException(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                Bo.BoFactory.GetSMC_UserExceptionBo.DeleteDisableUserException(id, CurrentUser.UserUId);
            }

            return Json(result);
        }
        
        public JsonResult DeleteSelectedDisableDeviceException(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                Bo.BoFactory.GetDeviceExceptionBO.DeleteDisableDeviceException(id, CurrentUser.UserUId);
            }

            return Json(result);
        }

        public JsonResult GetUnitData()
        {
            List<SmartBox.Console.Common.Entities.SMC_Unit> units = new List<Common.Entities.SMC_Unit>();
            //SmartBox.Console.Common.Entities.SMC_Unit unitDefault = new Common.Entities.SMC_Unit();
            //unitDefault.Unit_Name = "请选择单位";
            //unitDefault.Unit_ID = "";
            //units.Add(unitDefault);


            IList<SmartBox.Console.Common.Entities.SMC_Unit> _units = base.GetUnitData();
            units.AddRange(_units);

            return Json(units, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserDevices(string uid)
        {
            List<IDictionary<string, object>> re = Bo.BoFactory.GetCommonBO.GetUserDevices(uid);
            return Json(re, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LostUserDevicePost(string deviceid)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            bool re = Bo.BoFactory.GetCommonBO.LostUserDevicePost(deviceid);
            result["r"] = re;
            result["d"] = re ? "设备挂失成功!" : "设备挂失失败!";
            return Json(result);
        }
        
        public ActionResult DeviceExceptionAddPost(string deviceid, string t)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            if (String.IsNullOrEmpty(t) || (t.ToLower() != "e" && t.ToLower() != "d"))
            {
                result["r"] = false;
                result["d"] = "设备例外保存失败!";
                return Json(result);
            }

            t = t.ToLower();
            bool re = false;
            switch (t)
            {
                case "e":
                    re = Bo.BoFactory.GetDeviceExceptionBO.AddDeviceEnableException(deviceid);
                    break;
                case "d":
                    re = Bo.BoFactory.GetDeviceExceptionBO.AddDeviceDisableException(deviceid);
                    break;
            }
            result["r"] = re;
            result["d"] = re ? "设备例外保存成功!" : "设备例外保存失败!";
            return Json(result);
        }
        
        public ActionResult UserExceptionAddPost(string useruid, string t)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            if (String.IsNullOrEmpty(t) || (t.ToLower() != "e" && t.ToLower() != "d"))
            {
                result["r"] = false;
                result["d"] = "用户例外保存失败!";
                return Json(result);
            }

            t = t.ToLower();
            bool re = false;
            switch (t)
            {
                case "e":
                    re = Bo.BoFactory.GetSMC_UserExceptionBo.AddUserEnableException(useruid);
                    break;
                case "d":
                    re = Bo.BoFactory.GetSMC_UserExceptionBo.AddUserDisableException(useruid);
                    break;
            }
            result["r"] = re;
            result["d"] = re ? "用户例外保存成功!" : "用户例外保存失败!";
            return Json(result);
        }
        public JsonResult PassUser(string uid, string uname)
        {
            Hashtable result = new Hashtable();

            bool r = Bo.BoFactory.GetSMC_UserBo.PassUser(uid);
            Bo.BoFactory.GetUserInfoBO.UnLockUser(uid);
            result["r"] = r;
            result["d"] = "用户" + uname + (r ? "当前状态为已启用" : "当前状态为已禁用");

            return Json(result);
        }

        public JsonResult PassDevice(string id)
        {
            Hashtable result = new Hashtable();

            bool r = Bo.BoFactory.GetCommonBO.PassDevice(id, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "设备审核" + (r ? "已通过！" : "未通过！");

            return Json(result);
        }

        public System.Drawing.Bitmap GetDimensionalCode(string link)
        {
            System.Drawing.Bitmap bmp = null;
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 4;
                qrCodeEncoder.QRCodeVersion = 7;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                bmp = qrCodeEncoder.Encode(link, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Invalid version !");
                Log4NetHelper.Error(ex);
            }

            return bmp;

        }

        public ActionResult SyncPackageExt(string id)
        {
            Hashtable r = new Hashtable();
            SMC_PackageExt ext = BoFactory.GetVersionTrackBo.GetPackageExt(id);
            int batch_no = BoFactory.GetSMC_AutoTableIDBo.GetMaxId("SMC_PackageExtSyncToOutside", "sync_bat_no");
            batch_no++;
            SyncPackageExtToAppCenter(ext, r, batch_no);
            return Json(r);
        }

        private bool _PassAppPackage(string id, Hashtable result)
        {
            SMC_PackageExt ext = BoFactory.GetSMC_PackageExtBO.Get(id);
            bool r = Bo.BoFactory.GetSMC_PackageExtBO.PassAppPackage(id, CurrentUser.UserUId, CurrentUser.FullName);
            if (r)
            {
                ext.pe_AuthStatus = 1;
                //内网数据更新
                BoFactory.GetVersionTrackBo.SetUserfulStatus(id, "ENABLE");

                //同步至外网数据
                //IProxy proxy = ProxyFactory.CreateProxy();
                //SmartBox.Console.Bo.AppCenter.AppCenterBO bo = proxy.CreateObject<SmartBox.Console.Bo.AppCenter.AppCenterBO>();
                //OutsideWS.WebService ws = new OutsideWS.WebService();


                //OutsideWS.SMC_PackageExt entity = CopyPackageExt(ext);

                ////同步安装包到外网应用中心
                //ws.PackageExtSync(entity);
                base._SyncPackageExt(ext);
                bool need_update_package4ai = true;
                need_update_package4ai = (ext.TableName == "Package4AI");

                GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("app_sj_need_auth");
                if (parm.ConfigValue == "1")
                {
                    string serializationPath = Server.MapPath("~/PackageSerialization/") + ext.pe_id + "\\";
                    if (!Directory.Exists(serializationPath))
                        Directory.CreateDirectory(serializationPath);
                    Package4AI package = null;
                    if (need_update_package4ai)
                    {
                        try
                        {
                            using (Stream fStream = new FileStream(serializationPath + "package.xml", FileMode.Open, FileAccess.Read))
                            {
                                XmlSerializer xmlFormat = new XmlSerializer(typeof(Package4AI), new Type[] { typeof(List<App4AI>), typeof(List<Action4Android>) });

                                package = (Package4AI)xmlFormat.Deserialize(fStream);

                                fStream.Close();
                                fStream.Dispose();
                            }
                        }
                        catch (Exception e)
                        {
                            Log4NetHelper.Error(e);
                        }
                    }

                    string tempFilePath = "";
                    string saveFileName = "";
                    if (package != null)
                    {

                        try
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.Load(serializationPath + "path.xml");
                            tempFilePath = xmlDoc.DocumentElement.SelectSingleNode("//PackagePath/TempFilePath").InnerText;
                            saveFileName = xmlDoc.DocumentElement.SelectSingleNode("//PackagePath/SaveFileName").InnerText;
                        }
                        catch (Exception e)
                        {
                            Log4NetHelper.Error(e);
                        }

                        if (package.ID == 0)
                        {//新增
                            BoFactory.GetVersionTrackBo.InsertPackage4AI(package, tempFilePath, saveFileName);
                            ext.TableID = package.ID;
                        }
                        else
                        {//修改
                            BoFactory.GetVersionTrackBo.UpdatePackage(package, tempFilePath, saveFileName);                            
                        }                        
                    }


                    if (ext.pe_id != 0)
                    {
                        //=====================================================================================
                        //复制文件到PackageExt目录
                        //musictom 2014-05-17 

                        if (ext != null)
                        {
                            //string fileName = Path.GetFileName(saveFileName);
                            //string destPath = Server.MapPath("~/") + AppConfig.OutPackUploadFolder + "\\" + ext.pe_id + "\\" + fileName;
                            //System.IO.File.Copy(saveFileName, destPath);
                            //string outHost = AppConfig.OutWebHost;
                            //if (outHost.EndsWith("/") == false)
                            //    outHost += "/";
                            //ext.pe_DownloadUri = outHost + AppConfig.OutPackUploadFolder + "/" + ext.pe_id + "/" + fileName;
                            //BoFactory.GetAppCenterBO.Save(ext);
                            //System.Drawing.Bitmap bitmap = GetDimensionalCode(ext.pe_DownloadUri);
                            //string bitmapFileUrl = Server.MapPath("~/") + AppConfig.OutPackUploadFolder + "\\" + ext.pe_id + "\\" + "二维码图片.jpeg";
                            //bitmap.Save(bitmapFileUrl);
                        }
                        //=====================================================================================
                    }
                    ext.pe_AuthStatus = 1;
                    ext.pe_AuthManUID = CurrentUser.UserUId;
                    ext.pe_AuthMan = CurrentUser.FullName;
                    ext.pe_AuthTime = DateTime.Now;
                    ext.pe_UsefulStstus = "1";
                    ext.pe_UsefulOperatorUID = CurrentUser.UserUId;
                    ext.pe_UsefulOperatorName = CurrentUser.FullName;
                    ext.pe_UsefulTime = DateTime.Now;
                    ext.pe_DownloadUri = ConfigurationManager.AppSettings["packUrl"] + Path.GetFileName(saveFileName);
                    Bo.BoFactory.GetSMC_PackageExtBO.Save(ext);
                    //需要审核
                    
                }
            }
            return r;
        }
        public JsonResult PassAppPackage(string id)
        {
            Hashtable result = new Hashtable();
            bool r = _PassAppPackage(id, result);
            result["r"] = r;
            result["d"] = (r ? "已审核通过！" : "已拒绝通过！");

            return Json(result);
        }

        public JsonResult NotPassAppPackage(string id)
        {
            Hashtable result = new Hashtable();

            bool r = Bo.BoFactory.GetSMC_PackageExtBO.NotPassAppPackage(id, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "发布审核" + (r ? "已拒绝通过！" : "拒绝失败！");

            return Json(result);
        }

        public JsonResult LostDevice(string deviceid)
        {
            Hashtable result = new Hashtable();
            SmartBox.Console.Common.Entities.SMC_UserException ex = new Common.Entities.SMC_UserException();
            ex.ID = 54;
            bool r = Bo.BoFactory.GetCommonBO.LostDevice(deviceid, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "设备" + (r ? "挂失成功！" : "挂失失败！");

            return Json(result);
        }
        
        public JsonResult UnLostDevice(string deviceid)
        {
            Hashtable result = new Hashtable();

            bool r = Bo.BoFactory.GetCommonBO.UnLostDevice(deviceid, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "设备" + (r ? "解除挂失成功！" : "解除挂失失败！");

            return Json(result);
        }
        
        public JsonResult SyncDevice(string deviceid)
        {
            Hashtable result = new Hashtable();

            //bool r = Bo.BoFactory.GetCommonBO.UnLostDevice(deviceid, CurrentUser.UserUId);
            result["r"] = true;
            result["d"] = "设备" + (true ? "同步成功！" : "同步失败！");

            return Json(result);
        }

        public JsonResult UnLockDevice(string deviceid)
        {
            Hashtable result = new Hashtable();

            bool r = Bo.BoFactory.GetCommonBO.UnLockDevice(deviceid, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "设备" + (r ? "解除锁定成功！" : "解除锁定失败！");
            
            return Json(result);
        }
        public JsonResult UnBindDevice(string deviceuser_id)
        {
            Hashtable result = new Hashtable();

            bool r = Bo.BoFactory.GetCommonBO.UnBindDevice(deviceuser_id, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "设备" + (r ? "解除绑定成功！" : "解除绑定失败！");

            return Json(result);
        }

        /// <summary>
        /// 批量解除绑定
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public JsonResult UnBindSelectedDevice(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;
                bool r = Bo.BoFactory.GetCommonBO.UnBindDevice(id, CurrentUser.UserUId);
            }

            return Json(result);
        }

        public JsonResult SaveUserAuthSetting(string enableUserAuth)
        {
            GlobalParam p = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("user_need_auth");
            if (p == null)
            {
                p = new GlobalParam();
                p.ConfigKey = "user_need_auth";
            }
            p.ConfigValue = enableUserAuth;
            Bo.BoFactory.GetGlobalParamBO.Save(p);

            Hashtable result = new Hashtable();

            result["r"] = true;
            result["d"] = "用户审核设置成功！";

            return Json(result);
        }
        
        public JsonResult SaveDeviceAuthSetting(string enableDeviceAuth)
        {
            GlobalParam p = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("device_need_auth");
            if (p == null)
            {
                p = new GlobalParam();
                p.ConfigKey = "device_need_auth";
            }
            p.ConfigValue = enableDeviceAuth;
            Bo.BoFactory.GetGlobalParamBO.Save(p);
            string val = enableDeviceAuth == "1" ? "true" : "false";
            Hashtable hr = new Hashtable();
            Bo.BoFactory.GetSystemConfigBO.SaveSystemConfig("com.beyondbit.user.auth.apply.enable", val, hr);
            Service.ServiceReference1.ManagerServiceClient msc = new Service.ServiceReference1.ManagerServiceClient();
            msc.ResetRuntimeConfigs();

            Hashtable result = new Hashtable();

            result["r"] = true;
            result["d"] = "设备审核设置成功！";

            return Json(result);
        }

        public JsonResult GlobalParamPost(string app_sj_need_auth, string user_default_status, string namelist_black_or_white, string user_unlock_auto_enabled, string user_unlock_auto_hours,
            string lock_user_farto_lasttime_hours, string lock_count_mode, string allowed_wrong_times_when_login, string disable_device_after_notpass_device, string bua_auth_url)
        {
            Hashtable result = new Hashtable();
            List<GlobalParam> parms = new List<GlobalParam>();//Bo.BoFactory.GetGlobalParamBO.LoadGlobalParam();

            GlobalParam parm = new GlobalParam();
           
            parm.ConfigKey = "app_sj_need_auth";
            parm.ConfigValue = app_sj_need_auth;
            parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "user_default_status";
            parm.ConfigValue = user_default_status;
            parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "namelist_black_or_white";
            parm.ConfigValue = namelist_black_or_white;
            parms.Add(parm);
            
            parm = new GlobalParam();
            parm.ConfigKey = "disable_device_after_notpass_device";
            parm.ConfigValue = disable_device_after_notpass_device;
            parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "bua_auth_url";
            parm.ConfigValue = bua_auth_url;
            parms.Add(parm);

            //parm = new GlobalParam();
            //parm.ConfigKey = "user_unlock_auto_hours";
            //parm.ConfigValue = user_unlock_auto_hours;
            //parms.Add(parm);

            //parm = new GlobalParam();
            //parm.ConfigKey = "lock_user_farto_lasttime_hours";
            //parm.ConfigValue = lock_user_farto_lasttime_hours;
            //parms.Add(parm);

            //parm = new GlobalParam();
            //parm.ConfigKey = "lock_count_mode";
            //parm.ConfigValue = lock_count_mode;
            //parms.Add(parm);

            //parm = new GlobalParam();
            //parm.ConfigKey = "allowed_wrong_times_when_login";
            //parm.ConfigValue = allowed_wrong_times_when_login;
            //parms.Add(parm);

            Bo.BoFactory.GetGlobalParamBO.SaveGlobalParam(parms);
            bool r = true;// Bo.BoFactory.GetCommonBO.UnLockDevice(deviceid, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "全局参数设置" + (r ? "成功！" : "失败！");

            return Json(result);
        }

        public JsonResult DeviceRetryLockPost(string app_sj_need_auth, string user_default_status, string namelist_black_or_white, string user_unlock_auto_enabled, string user_unlock_auto_hours,
            string lock_user_farto_lasttime_hours, string lock_count_mode, string allowed_wrong_times_when_login)
        {
            Hashtable result = new Hashtable();
            List<GlobalParam> parms = new List<GlobalParam>();//Bo.BoFactory.GetGlobalParamBO.LoadGlobalParam();

            GlobalParam parm = new GlobalParam();
           
            //parm.ConfigKey = "app_sj_need_auth";
            //parm.ConfigValue = app_sj_need_auth;
            //parms.Add(parm);

            //parm = new GlobalParam();
            //parm.ConfigKey = "user_default_status";
            //parm.ConfigValue = user_default_status;
            //parms.Add(parm);

            //parm = new GlobalParam();
            //parm.ConfigKey = "namelist_black_or_white";
            //parm.ConfigValue = namelist_black_or_white;
            //parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "device_unlock_auto_enabled";
            parm.ConfigValue = user_unlock_auto_enabled;
            parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "device_unlock_auto_hours";
            parm.ConfigValue = user_unlock_auto_hours;
            parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "lock_device_farto_lasttime_hours";
            parm.ConfigValue = lock_user_farto_lasttime_hours;
            parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "lock_device_count_mode";
            parm.ConfigValue = lock_count_mode;
            parms.Add(parm);

            //parm = new GlobalParam();
            //parm.ConfigKey = "allowed_wrong_times_when_login";
            //parm.ConfigValue = allowed_wrong_times_when_login;
            //parms.Add(parm);

            Bo.BoFactory.GetGlobalParamBO.SaveGlobalParam(parms);
            bool r = true;// Bo.BoFactory.GetCommonBO.UnLockDevice(deviceid, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "设备重试锁定设置" + (r ? "成功！" : "失败！");

            return Json(result);
        }

        
        public JsonResult UserRetryLockPost(string app_sj_need_auth, string user_default_status, string namelist_black_or_white, string user_unlock_auto_enabled, string user_unlock_auto_hours,
            string lock_user_farto_lasttime_hours, string lock_count_mode, string allowed_wrong_times_when_login)
        {
            Hashtable result = new Hashtable();
            List<GlobalParam> parms = new List<GlobalParam>();//Bo.BoFactory.GetGlobalParamBO.LoadGlobalParam();

            GlobalParam parm = new GlobalParam();
           
            //parm.ConfigKey = "app_sj_need_auth";
            //parm.ConfigValue = app_sj_need_auth;
            //parms.Add(parm);

            //parm = new GlobalParam();
            //parm.ConfigKey = "user_default_status";
            //parm.ConfigValue = user_default_status;
            //parms.Add(parm);

            //parm = new GlobalParam();
            //parm.ConfigKey = "namelist_black_or_white";
            //parm.ConfigValue = namelist_black_or_white;
            //parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "user_unlock_auto_enabled";
            parm.ConfigValue = user_unlock_auto_enabled;
            parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "user_unlock_auto_hours";
            parm.ConfigValue = user_unlock_auto_hours;
            parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "lock_user_farto_lasttime_hours";
            parm.ConfigValue = lock_user_farto_lasttime_hours;
            parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "lock_user_count_mode";
            parm.ConfigValue = lock_count_mode;
            parms.Add(parm);

            parm = new GlobalParam();
            parm.ConfigKey = "allowed_wrong_times_when_login";
            parm.ConfigValue = allowed_wrong_times_when_login;
            parms.Add(parm);

            Bo.BoFactory.GetGlobalParamBO.SaveGlobalParam(parms);
            bool r = true;// Bo.BoFactory.GetCommonBO.UnLockDevice(deviceid, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "用户重试锁定设置" + (r ? "成功！" : "失败！");

            return Json(result);
        }

        public JsonResult PassSelectedDevice(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                Bo.BoFactory.GetCommonBO.PassDevice(id, CurrentUser.UserUId);
            }

            return Json(result);
        }
        
        public JsonResult PassSelectedAppPackage(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                Hashtable _result = new Hashtable();
                _result["r"] = true;
                _result["d"] = "操作成功！";
                _PassAppPackage(id, _result);
            }

            return Json(result);
        }
        
        public JsonResult NotPassSelectedAppPackage(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                Bo.BoFactory.GetSMC_PackageExtBO.NotPassAppPackage(id, CurrentUser.UserUId);
            }

            return Json(result);
        }
        
        /// <summary>
        /// 批量挂失
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public JsonResult LostSelectedDevice(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                Bo.BoFactory.GetCommonBO.LostDevice(id, CurrentUser.UserUId);
            }

            return Json(result);
        }
        
        /// <summary>
        /// 批量解除挂失
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public JsonResult UnLostSelectedDevice(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                Bo.BoFactory.GetCommonBO.UnLostDevice(id, CurrentUser.UserUId);
            }

            return Json(result);
        }
        
        /// <summary>
        /// 批量同步设备
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public JsonResult SyncSelectedDevice(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                //Bo.BoFactory.GetCommonBO.UnLostDevice(id, CurrentUser.UserUId);
            }

            return Json(result);
        }
        
        /// <summary>
        /// 批量解除锁定
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public JsonResult UnLockSelectedDevice(string ids)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";
            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string id in idlist)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                Bo.BoFactory.GetCommonBO.UnLockDevice(id, CurrentUser.UserUId);
            }

            return Json(result);
        }

        public JsonResult UnLockUser(string uid, string uname)
        {
            Hashtable result = new Hashtable();

            bool r = Bo.BoFactory.GetSMC_UserBo.UnLockUser(uid);
            Bo.BoFactory.GetUserInfoBO.UnLockUser(uid);
            result["r"] = r;
            result["d"] = "用户" + uname + (r ? "解锁成功!" : "未成功解锁!");

            return Json(result);
        }
        
        public JsonResult LockUser(string uid, string uname)
        {
            Hashtable result = new Hashtable();

            bool r = Bo.BoFactory.GetSMC_UserBo.LockUser(uid);
            Bo.BoFactory.GetUserInfoBO.LockUser(uid);
            result["r"] = r;
            result["d"] = "用户" + uname + (r ? "锁定成功!" : "锁定失败!");

            return Json(result);
        }

        public JsonResult Kickout(string uid, string uname)
        {
            Hashtable result = new Hashtable();
            SmartBox.Console.Service.ServiceReference1.ManagerServiceClient client = new SmartBox.Console.Service.ServiceReference1.ManagerServiceClient();
            Bo.BoFactory.GetSMC_UserBo.KickoutUser(uid, result, client);
            
            result["d"] = "用户" + uname + ((bool)result["r"] ? "已强制退出！" : "未强制退出！");

            return Json(result);
        }

        public JsonResult NotPassUser(string uid, string uname)
        {
            Hashtable result = new Hashtable();

            bool r = Bo.BoFactory.GetSMC_UserBo.NotPassUser(uid);
            Bo.BoFactory.GetUserInfoBO.LockUser(uid);
            result["r"] = r;
            result["d"] = "用户" + uname + (r ? "当前状态为已禁用" : "当前状态为已启用");

            return Json(result);
        }

        public JsonResult DeleteEnableDeviceException(string id)
        {
            Hashtable result = new Hashtable();

            bool r = Bo.BoFactory.GetDeviceExceptionBO.DeleteEnableDeviceException(id, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "设备启用审核例外" + (r ? "删除成功！" : "删除失败！");

            return Json(result);
        }
        
        public JsonResult DeleteEnableUserException(string id)
        {
            Hashtable result = new Hashtable();
            bool r = Bo.BoFactory.GetSMC_UserExceptionBo.DeleteEnableUserException(id, CurrentUser.UserUId);
            //bool r = Bo.BoFactory.GetDeviceExceptionBO.DeleteEnableDeviceException(id, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "用户启用审核例外" + (r ? "删除成功！" : "删除失败！");

            return Json(result);
        }
        
        public JsonResult DeleteDisableUserException(string id)
        {
            Hashtable result = new Hashtable();
            bool r = Bo.BoFactory.GetSMC_UserExceptionBo.DeleteDisableUserException(id, CurrentUser.UserUId);
            //bool r = Bo.BoFactory.GetDeviceExceptionBO.DeleteEnableDeviceException(id, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "用户禁用审核例外" + (r ? "删除成功！" : "删除失败！");

            return Json(result);
        }
        
        public JsonResult DeleteDisableDeviceException(string id)
        {
            Hashtable result = new Hashtable();

            bool r = Bo.BoFactory.GetDeviceExceptionBO.DeleteDisableDeviceException(id, CurrentUser.UserUId);
            result["r"] = r;
            result["d"] = "设备禁用审核例外" + (r ? "删除成功！" : "删除失败！");

            return Json(result);
        }

        public JsonResult NotPassDevice(string deviceuserapplyid, string refuse_msg)
        {
            Hashtable result = new Hashtable();
            GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("disable_device_after_notpass_device");
            bool r = Bo.BoFactory.GetDeviceUserApplyBO.NotPassDevice(deviceuserapplyid, CurrentUser.UserUId, parm, refuse_msg);
            result["r"] = r;
            result["d"] = "设备" + (r ? "已拒绝通过！" : "未拒绝通过！");

            return Json(result);
        }
        //public JsonResult GetUserInfoList()
        //{
        //    Hashtable result = new Hashtable();
        //    result["total"] = 6;
            
        //    List<Hashtable> users = new List<Hashtable>();
        //    result["rows"] = users;

        //    Hashtable user = new Hashtable();
        //    users.Add(user);
        //    user["pd_id"] = 1;
        //    user["username"] = "张明";
        //    user["useruid"] = "zhangm";
        //    user["unit"] = "浦东新区环保局";
        //    user["user_status"] = "启用";
        //    user["enabled_time"] = "2014-07-29 10:20";
        //    user["updated_time"] = "2014-07-29 10:20";
        //    user["created_time"] = "2014-07-29 10:20";
        //    user["disabled_time"] = "2014-07-29 10:20";
        //    user["pdid"] = 1;

        //    user = new Hashtable();
        //    users.Add(user);
        //    user["pd_id"] = 2;
        //    user["username"] = "张明";
        //    user["useruid"] = "zhangm";
        //    user["unit"] = "浦东新区环保局";
        //    user["user_status"] = "启用";
        //    user["enabled_time"] = "2014-07-29 10:20";
        //    user["updated_time"] = "2014-07-29 10:20";
        //    user["enabled_time"] = "2014-07-29 10:20";
        //    user["disabled_time"] = "2014-07-29 10:20";
        //    user["pdid"] = 2;

        //    user = new Hashtable();
        //    users.Add(user);
        //    user["pd_id"] = 3;
        //    user["username"] = "张明";
        //    user["useruid"] = "zhangm";
        //    user["unit"] = "浦东新区环保局";
        //    user["user_status"] = "启用";
        //    user["enabled_time"] = "2014-07-29 10:20";
        //    user["updated_time"] = "2014-07-29 10:20";
        //    user["enabled_time"] = "2014-07-29 10:20";
        //    user["disabled_time"] = "2014-07-29 10:20";
        //    user["pdid"] = 3;

        //    user = new Hashtable();
        //    users.Add(user);
        //    user["pd_id"] = 4;
        //    user["username"] = "张明";
        //    user["useruid"] = "zhangm";
        //    user["unit"] = "浦东新区环保局";
        //    user["user_status"] = "启用";
        //    user["enabled_time"] = "2014-07-29 10:20";
        //    user["updated_time"] = "2014-07-29 10:20";
        //    user["enabled_time"] = "2014-07-29 10:20";
        //    user["disabled_time"] = "2014-07-29 10:20";
        //    user["pdid"] = 4;

        //    user = new Hashtable();
        //    users.Add(user);
        //    user["pd_id"] = 5;
        //    user["username"] = "张明";
        //    user["useruid"] = "zhangm";
        //    user["unit"] = "浦东新区环保局";
        //    user["user_status"] = "启用";
        //    user["enabled_time"] = "2014-07-29 10:20";
        //    user["updated_time"] = "2014-07-29 10:20";
        //    user["enabled_time"] = "2014-07-29 10:20";
        //    user["disabled_time"] = "2014-07-29 10:20";
        //    user["pdid"] = 5;

        //    user = new Hashtable();
        //    users.Add(user);
        //    user["pd_id"] = 6;
        //    user["username"] = "张明";
        //    user["useruid"] = "zhangm";
        //    user["unit"] = "浦东新区环保局";
        //    user["user_status"] = "启用";
        //    user["enabled_time"] = "2014-07-29 10:20";
        //    user["updated_time"] = "2014-07-29 10:20";
        //    user["enabled_time"] = "2014-07-29 10:20";
        //    user["disabled_time"] = "2014-07-29 10:20";
        //    user["pdid"] = 6;
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetUserEnableAuthorizationList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;
            
            List<Hashtable> users = new List<Hashtable>();
            result["rows"] = users;

            Hashtable user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 1;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待审核";
            user["pdid"] = 1;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 2;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待审核";
            user["pdid"] = 2;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 3;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "已审核-通过";
            user["pdid"] = 3;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 4;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "已审核-通过";
            user["pdid"] = 4;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 5;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "已审核-拒绝";
            user["pdid"] = 5;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 6;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "已审核-拒绝";
            user["pdid"] = 6;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMobileStyleList(string clientType, string code, string displayName, int pageSize, int pageIndex)
        {
            Beyondbit.Framework.DataAccess.ObjectDAO.SplitPageResult<Common.Entities.Style> items = Bo.BoFactory.GetStyleBO.QueryList(clientType, code, displayName, "id desc", pageSize, pageIndex);

            //List<SmartBox.Console.Common.Entities.Style> rows = new List<Common.Entities.Style>();

            //for (int i = 0; i < items.Result.Tables[0].Rows.Count; ++i)
            //{
            //    System.Data.DataRow dr = items.Result.Tables[0].Rows[i];
            //    SmartBox.Console.Common.Entities.Style sty = new Common.Entities.Style();
            //    sty.ClientType = dr["ClientType"].ToString();
            //    sty.Code = dr["Code"].ToString();
            //    sty.DisplayName = dr["DipalsyName"].ToString();
            //    sty.ID = Convert.ToInt32(dr["ID"]);
            //    rows.Add(sty);
            //}
            Hashtable result = new Hashtable();
            result["rows"] = items.Items;
            result["total"] = items.TotalCount;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMobileStyleHomeItemList(string styleId, int pageSize, int pageIndex)
        {
            Beyondbit.Framework.DataAccess.ObjectDAO.SplitPageResult<Common.Entities.StyleHomeItem> items = Bo.BoFactory.GetStyleHomeItemBO.QueryStyleHomeItemList(styleId, "styleid desc", pageSize, pageIndex);

            Hashtable result = new Hashtable();
            result["rows"] = items.Items;
            result["total"] = items.TotalCount;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetUserDisableAuthorizationList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;
            
            List<Hashtable> users = new List<Hashtable>();
            result["rows"] = users;

            Hashtable user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 1;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待审核";
            user["pdid"] = 1;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 2;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待审核";
            user["pdid"] = 2;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 3;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "已审核-拒绝";
            user["pdid"] = 3;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 4;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "已审核-拒绝";
            user["pdid"] = 4;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 5;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "已审核-通过";
            user["pdid"] = 5;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 6;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "已审核-通过";
            user["pdid"] = 6;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeviceEnableAuthExceptionList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;
            
            List<Hashtable> users = new List<Hashtable>();
            result["rows"] = users;

            Hashtable user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 1;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 1;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 2;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 2;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 3;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 3;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 4;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 4;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 5;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 5;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 6;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 6;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUserRetryLockList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;
            
            List<Hashtable> users = new List<Hashtable>();
            result["rows"] = users;

            Hashtable user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 1;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 1;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 2;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 2;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 3;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 3;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 4;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 4;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 5;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 5;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 6;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 6;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeviceDisableAuthExceptionList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;
            
            List<Hashtable> users = new List<Hashtable>();
            result["rows"] = users;

            Hashtable user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 1;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 1;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 2;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 2;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 3;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 3;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 4;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 4;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 5;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 5;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 6;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["user_status"] = "待启用";
            user["pdid"] = 6;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeviceRetryLockList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;
            
            List<Hashtable> users = new List<Hashtable>();
            result["rows"] = users;

            Hashtable user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 1;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            user["devicedesc"] = "Canius 的 iPhone";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 1;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 2;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            user["devicedesc"] = "Canius 的 iPhone";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 2;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 3;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 3;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 4;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            user["devicedesc"] = "Canius 的 iPhone";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 4;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 5;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            user["devicedesc"] = "Canius 的 iPhone";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 5;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 6;
            user["username"] = "张明";
            user["useruid"] = "zhangm";
            user["deviceno"] = "8c0db524123542ff0c143cbf448a489cf7aa394b";
            user["devicedesc"] = "Canius 的 iPhone";
            user["unit"] = "浦东新区环保局";
            user["locktime"] = "2014-07-25 09:51";
            user["retrycount"] = 3;
            user["user_status"] = "已锁定";
            user["pdid"] = 6;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetBackgroundImageList()
        {
            Hashtable result = new Hashtable();
            result["total"] = 6;
            
            List<Hashtable> users = new List<Hashtable>();
            result["rows"] = users;

            Hashtable user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 1;
            user["src"] = "PackageExt/13/截图-3.jpg";
            user["app"] = "通讯录";
            user["package"] = "通讯录_ipad";
            user["clientType"] = "Pad/IOS";
            user["uploadtime"] = "2014-07-25 09:51";
            user["pdid"] = 1;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 2;
            user["src"] = "PackageExt/13/截图-2.jpg";
            user["app"] = "通讯录";
            user["package"] = "通讯录_ipad";
            user["clientType"] = "Pad/IOS";
            user["uploadtime"] = "2014-07-25 09:51";
            user["pdid"] = 2;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 3;
            user["src"] = "PackageExt/13/截图-4.jpg";
            user["app"] = "通讯录";
            user["package"] = "通讯录_ipad";
            user["clientType"] = "Pad/IOS";
            user["uploadtime"] = "2014-07-25 09:51";
            user["pdid"] = 3;

            user = new Hashtable();
            users.Add(user);
            user["pd_id"] = 4;
            user["src"] = "PackageExt/13/截图-5.jpg";
            user["app"] = "通讯录";
            user["package"] = "通讯录_ipad";
            user["clientType"] = "Pad/IOS";
            user["uploadtime"] = "2014-07-25 09:51";
            user["pdid"] = 4;


            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
