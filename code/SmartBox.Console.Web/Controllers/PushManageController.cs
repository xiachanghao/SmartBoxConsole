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
using System.Text;
using Beyondbit.BUA.Client;
using ThoughtWorks.QRCode.Codec;
using Beyondbit.Framework.Core.Proxy;
using System.Configuration;
using System.Collections;
using Beyondbit.Push.Service.Bo;
using Newtonsoft.Json;
using Beyondbit.Framework.DataAccess.ObjectDAO;

namespace SmartBox.Console.Web.Controllers
{
    public class PushManageController : MyControllerBase
    {
        public ActionResult PushNotificationReport()
        {
            return View();
        }

        public ActionResult FileTransUpload()
        {
            return View();
        }

        public ActionResult JobCommandCtrl()
        {
            return View();
        }

        public ActionResult PushConfig()
        {
            return View();
        }

        public ActionResult ViewLogs(string cid)
        {
            ViewData["cid"] = cid;
            return View();
        }

        public ActionResult PushDLLList()
        {
            return View();
        }

        public JsonResult GetPushDLLList(int pageSize, int pageIndex)
        {
            SplitPageResult<SMC_PushDll> users = BoFactory.GetSMC_PushDllBO.QueryPushDllList(pageSize, pageIndex);
            Hashtable result = new Hashtable();
            result["total"] = users.TotalCount;
            result["rows"] = users.Items;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PushDLLDelete(string ids)
        {
            string[] idlist = ids.Split(",".ToCharArray());
            if (idlist != null && idlist.Length > 0) {
                foreach (string id in idlist) {
                    if (String.IsNullOrEmpty(id))
                        continue;
                    SMC_PushDll dll = BoFactory.GetSMC_PushDllBO.Get(int.Parse(id));
                    if (dll != null)
                    {
                        BoFactory.GetSMC_PushDllBO.Delete(dll);
                    }
                }
            }
            Hashtable result = new Hashtable();
            result["IsSuccess"] = true;
            result["Msg"] = "删除插件成功！";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PushProcess(string m)
        {
            string method = m;
            var res = new JsonResult();

            object resp = null;
            var context = HttpContext;

            if ("QueryChannels".Equals(method, StringComparison.CurrentCultureIgnoreCase))
            {
                resp = PushBo.Instance.QueryDeletedChannels(false);
            }
            else if ("SaveChannel".Equals(method, StringComparison.CurrentCultureIgnoreCase))
            {
                SaveChannel(context);
            }
            else if ("UpdateChannelState".Equals(method, StringComparison.CurrentCultureIgnoreCase))
            {
                PushBo.Instance.UpdateChannelState(Convert.ToInt32(context.Request.Form["Id"]), Convert.ToInt32(context.Request.Form["State"]));
                Beyondbit.Push.Service.PushBrokerManager.Restart();
            }
            else if ("TestChannel".Equals(method, StringComparison.CurrentCultureIgnoreCase))
            {
                TestChannel(context);
            }
            else if ("QueryLogs".Equals(method, StringComparison.CurrentCultureIgnoreCase))
            {
                resp = PushBo.Instance.QueryLogs(Convert.ToInt32(context.Request.Form["ChannelId"]), context.Request.Form["Level"]);
            }
                      
            
            res.Data = resp;
            return res;
        }

        private void SaveChannel(HttpContextBase context)
        {
            var form = context.Request.Form;
            var channel = new Channel();
            string idStr = form["Id"];
            if (!string.IsNullOrEmpty(idStr))
                channel.Id = Convert.ToInt32(idStr);
            channel.Title = form["Title"];
            channel.ApplicationId = form["ApplicationId"];
            channel.PlatformType = (Beyondbit.Push.Service.PlatformType)Enum.Parse(typeof(Beyondbit.Push.Service.PlatformType), form["PlatformType"], true);
            channel.CertPassword = form["CertPassword"];
            channel.State = 1;

            var cert = context.Request.Files["Cert"];
            string certName = form["CertName"];
            if (cert == null || string.IsNullOrEmpty(cert.FileName))
            {
                PushBo.Instance.SaveChannel(channel, channel.Id == 0 || string.IsNullOrEmpty(certName));
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    cert.InputStream.CopyTo(ms);
                    channel.Cert = ms.ToArray();
                }
                channel.CertName = cert.FileName;
                PushBo.Instance.SaveChannel(channel, true);
            }
        }

        private void TestChannel(HttpContextBase context)
        {
            int channelId = Convert.ToInt32(context.Request.Form["ChannelId"]);
            string uids = context.Request.Form["Uids"];
            var svc = new Beyondbit.Push.Service.PushService();// PushService();
            var payload = new Beyondbit.Push.Service.Payload();
            payload.Id = context.Request.Form["Id"];
            payload.Alert = context.Request.Form["Alert"];
            payload.Custom = context.Request.Form["Custom"];

            try
            {
                payload.Badge = Convert.ToInt32(context.Request.Form["Badge"]);
            }
            catch (FormatException)
            {
            }
            payload.Sound = context.Request.Form["Sound"];

            foreach (string uid in uids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                foreach (var subscription in PushBo.Instance.QueryDeviceSubscriptions(new List<int> { channelId }, uid))
                {
                    svc.Push(subscription, payload);
                }
            }
        }

        private void changeDllStatus(SMC_PushDll dll, string jostype, bool executeSucced)
        {
            switch (jostype)
            {
                case "AddJobPluginGroup":
                    if (executeSucced)
                        dll.pd_dll_status = "已载入";
                    break;
                case "RemoveJobPluginGroup":
                    if (executeSucced)
                        dll.pd_dll_status = "已载出";
                    break;
                case "RestartJobPluginGroup":
                    if (executeSucced)
                        dll.pd_dll_status = "已重载";
                    break;
                case "SetTargetJobTime":
                    break;
                case "PauseTargetJob":
                    if (executeSucced)
                        dll.pd_dll_status = "已暂停";
                    break;
                case "ContinueTargetJob":
                    if (executeSucced)
                        dll.pd_dll_status = "已继续";
                    break;
            }
            BoFactory.GetSMC_PushDllBO.Update(dll);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JobCommandCtrl(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "" };
            string s1 = form["JosType"];
            string s2 = form["ComdArgs"];
            string spd_id = form["pd_id"];
            int pd_id = 0;
            if (!String.IsNullOrEmpty(spd_id)) {
                pd_id = int.Parse(spd_id);
            }
            SMC_PushDll dll = BoFactory.GetSMC_PushDllBO.Get(pd_id);
            s2 = dll.pd_xml_filename + ".xml" + s2;
            string rtv = "";

            if (s1 == "AddJobPluginGroup")
            {
                TransferFile(dll);
            }
            if (s1 != "" && s2 != "")
            {
                try
                {
                    CommandSvcClient cmdClient = new CommandSvcClient();

                    rtv = cmdClient.ExecCommand(s1, s2);

                    if (rtv == "文件名参数不正确，请重试！")
                    {
                        data.IsSuccess = false;
                        data.Msg = rtv;
                    }
                    cmdClient.Close();
                }
                catch (Exception e)
                {
                    data.IsSuccess = false;
                    data.Msg = e.Message;
                }
                if (data.IsSuccess)
                {
                    data.Msg = rtv;
                }

                changeDllStatus(dll, s1, data.IsSuccess);
            }
            else
            {
                data.Msg = "请选择任务类型和输入参数文件名!";
            }
                                    

            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFile(FormCollection form)
        {            
            //return View(true);
            if (HttpContext.Request != null && HttpContext.Request.Files.Count > 0) {
                HttpPostedFileBase file = HttpContext.Request.Files[0];
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                SMC_PushDll dll = new SMC_PushDll();
                dll.pd_name = fileName;
                //bool exists = BoFactory.GetSMC_PushDllBO.Exists(dll);

                string path = Server.MapPath("~/PushZipPacks/") + fileName + "/";
                SmartBox.Console.Common.ZipHelper.UnpackFiles(file.InputStream, path);
                //TransferFile(file.InputStream);
            }

            Hashtable r = new Hashtable();
            r["result"] = true;
            return Json(r);
        }

        public ActionResult PushDllDeleteFile(string pd_id)
        {
            Hashtable r = new Hashtable();

            SMC_PushDll dll = BoFactory.GetSMC_PushDllBO.Get(int.Parse(pd_id));
            if (dll != null)
            {
                string path = Server.MapPath("~/PushZipPacks/") + dll.pd_id + "\\";
                try
                {
                    System.IO.Directory.Delete(path, true);
                    r["result"] = true;
                }
                catch (Exception ex)
                {
                    r["result"] = false;
                }                
            }
            else
            {
                r["result"] = false;
            }
            
            return Json(r);
        }

        public ActionResult PushDllUploadFile(string pd_id)
        {
            //return View(true);
            //string pd_id = form["pd_id"];
            Hashtable r = null;
            if (String.IsNullOrEmpty(pd_id))
            {
                r = new Hashtable();
                r["result"] = false;

                return View(r);
            }

            if (HttpContext.Request != null && HttpContext.Request.Files.Count > 0)
            {
                HttpPostedFileBase file = HttpContext.Request.Files[0];
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName);

                
                SMC_PushDll dll = BoFactory.GetSMC_PushDllBO.Get(int.Parse(pd_id));
                if (dll != null)
                {
                    dll.pd_zip_filename = System.IO.Path.GetFileName(file.FileName);
                    dll.pd_zip_extension = System.IO.Path.GetExtension(file.FileName);
                    dll.pd_zip_size = file.ContentLength;
                    dll.pd_zip_contenttype = file.ContentType;
                }

                string path = Server.MapPath("~/PushZipPacks/") + pd_id + "\\";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                try
                {
                    file.SaveAs(path + System.IO.Path.GetFileName(file.FileName));
                }
                catch
                {
                }
                //if (System.IO.Directory.Exists(path))
                //{
                //    throw new DirectoryNotFoundException("目录已经存在！");
                //    r = new Hashtable();
                //    r["result"] = false;
                //    return Json(r);
                //}
                dll.pd_path = path;
                SmartBox.Console.Common.ZipHelper.UnpackFiles(file.InputStream, path);
                string[] files = System.IO.Directory.GetFiles(path);
                if (files != null && files.Length > 0)
                {
                    foreach (string _file in files)
                    {
                        if (_file.EndsWith(".xml"))
                            dll.pd_xml_filename = System.IO.Path.GetFileName(_file);
                        else
                            dll.pd_dll_filename = System.IO.Path.GetFileName(_file);
                    }
                }
                BoFactory.GetSMC_PushDllBO.Update(dll);
                //TransferFile(file.InputStream);
            }

            r = new Hashtable();
            r["result"] = true;
            return View(r);
        }

        public JsonResult CleanTrashPushDll()
        {
            BoFactory.GetSMC_PushDllBO.CleanTrashPushDll();
            Hashtable p = new Hashtable();
            p["r"] = true;
            return Json(p);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PushDllAddPost(string pd_id, string pd_name, string pd_dll_filename, string pd_xml_filename)
        {
            Hashtable p = new Hashtable();
            
            if (!String.IsNullOrEmpty(pd_id))
            {
                int _pd_id = int.Parse(pd_id);
                SMC_PushDll dll = BoFactory.GetSMC_PushDllBO.Get(int.Parse(pd_id));
                dll.pd_name = pd_name;
                dll.pd_dll_filename = pd_dll_filename;
                dll.pd_xml_filename = pd_xml_filename;
                dll.pd_updatetime = DateTime.Now;

                bool existsXml = BoFactory.GetSMC_PushDllBO.ExistsXml(dll);
                if (existsXml)
                {
                    p["r"] = false;
                    p["d"] = "插件Job配置文件已存在,请检查是否重复!";
                    return Json(p);
                }

                dll.pd_status = true;
                BoFactory.GetSMC_PushDllBO.Update(dll);
                p["r"] = true;
                p["d"] = "保存插件成功!";
            }
            else
            {
                p["r"] = false;
                p["d"] = "保存插件成功!";
            }
            return Json(p);
        }

        public JsonResult GetPushDLLEntity(string pd_id)
        {
            SMC_PushDll _dll = BoFactory.GetSMC_PushDllBO.Get(int.Parse(pd_id));
            return Json(_dll);
        }

        public ActionResult PushDllAdd(string pd_id)
        {
            if (String.IsNullOrEmpty(pd_id))
            {
                SMC_PushDll dll = new SMC_PushDll();
                dll.pd_createdtime = DateTime.Now;
                dll.pd_status = false;
                BoFactory.GetSMC_PushDllBO.Insert(dll);
                ViewData["pd_id"] = dll.pd_id;
                Hashtable initialFiles = new Hashtable();
                initialFiles["files"] = new ArrayList();
                string fstr = Newtonsoft.Json.JsonConvert.SerializeObject(new ArrayList(), Newtonsoft.Json.Formatting.Indented);
                ViewData["f"] = fstr;
                string entity_fstr = Newtonsoft.Json.JsonConvert.SerializeObject(dll, Newtonsoft.Json.Formatting.Indented);
                ViewData["entity"] = entity_fstr;
            }
            else
            {
                ViewData["pd_id"] = pd_id;
                SMC_PushDll _dll = BoFactory.GetSMC_PushDllBO.Get(int.Parse(pd_id));
                Hashtable initialFiles = new Hashtable();
                initialFiles["name"] = _dll.pd_zip_filename;
                initialFiles["extension"] = _dll.pd_zip_extension;
                initialFiles["size"] = _dll.pd_zip_size;
                string fstr = Newtonsoft.Json.JsonConvert.SerializeObject(initialFiles, Newtonsoft.Json.Formatting.Indented);
                ViewData["f"] = "[" + fstr + "]";

                string entity_fstr = Newtonsoft.Json.JsonConvert.SerializeObject(_dll, Newtonsoft.Json.Formatting.Indented);
                ViewData["entity"] = entity_fstr;
            }
            return View();
        }

        private void TransferFile(SMC_PushDll dll)
        {
            if (dll == null)
                return;

            string path = dll.pd_path + dll.pd_zip_filename;
            if (!System.IO.File.Exists(path))
                return;
            FileStream fs = new FileStream(path, FileMode.Open);
            FileTransSvcClient ftsc = new FileTransSvcClient();
            
            ftsc.Open();

            #region 上传文件

            FileUploadMessage myFileMessage = new FileUploadMessage();

            myFileMessage.FileName = dll.pd_zip_filename;//"PushSharp-master.zip"; //文件名
            {
                myFileMessage.FileData = fs;
                IFileTransSvc intfFileTrans = ftsc.ChannelFactory.CreateChannel();
                try
                {
                    intfFileTrans.UploadFileMethod(myFileMessage);
                }
                catch { }
                finally
                {
                    fs.Close();
                    fs.Dispose();
                }

            }

            #endregion



            ftsc.Close();

        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult QueryPushNotificationList(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            data = BoFactory.GetPushManageBO.QueryPushNotificationList(view);
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeletePushNotifications(string ids)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "" };

            try
            {
                string[] idarray = ids.Split(",".ToCharArray());
                VersionTrackBo verBO = BoFactory.GetVersionTrackBo;

                List<SMC_PackageExt> exts = new List<SMC_PackageExt>();
                foreach (string id in idarray)
                {
                    if (String.IsNullOrEmpty(id))
                        continue;
                    BoFactory.GetPushManageBO.DeletePushNotifications(id);                    
                }

                //BoFactory.GetAppCenterBO.AddSMC_PackageExt(exts);
                data.IsSuccess = true;
                data.Msg = "删除推送日志成功！";
            }
            catch (Exception e)
            {
                data.IsSuccess = false;
                data.Msg = e.Message;
            }
            return Json(data);
        }
    }
}
