using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Beyondbit.MVC;
using SmartBox.Console.Bo;
using Beyondbit.Framework.Core.Proxy;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common.Entities.Search;
using System.IO;
using Beyondbit.Framework.Biz.Resource;
using System.Text;

namespace SmartBox.Console.Web.Controllers
{
    public class PluginManageControllers : MyControllerBase
    {
        #region 插件列表

        public ActionResult PluginInfoList()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult PluginInfoList(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            SearchPlugin search = new SearchPlugin();
            data = BoFactory.GetVersionTrackBo.QueryPluginInfo(view, search);
            return Json(data);
        }

        #endregion

        #region 插件用户管理

        public ActionResult PluginUser(string id)
        {
            ViewData["pid"] = id;
            return View();
        }

        public JsonResult GetUserInfo(FormCollection form,string id)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            SearchConfig search = new SearchConfig();
            search.PluginCode = id;
            
            data = BoFactory.GetVersionTrackBo.QueryUserInfoByPId(search, view);
            return Json(data);
        }

        public ActionResult DelSomeUserInfos(string id,string pid)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    string[] idsArray = id.Split(',');
                    IList<UserPluginRef> list = new List<UserPluginRef>();

                    for (int i = 0; i < idsArray.Length; i++)
                    {
                        var item = idsArray[i];
                        if (!string.IsNullOrEmpty(item))
                        {
                            UserPluginRef c = new UserPluginRef();
                            c.UserUId = Convert.ToString(item);
                            c.PluginCode = pid;
                            list.Add(c);
                        }

                    }
                    BoFactory.GetVersionTrackBo.DelUserRef(list);
                    vmobject.IsSuccess = true;
                    vmobject.Msg = "操作成功!";
                }
                else
                {
                    vmobject.IsSuccess = false;
                    vmobject.Msg = "您没有选择";
                }
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }
            return Json(vmobject);
        }

        public ActionResult AddUserInfo(string pid)
        {
            ViewData["pid"] = pid;
            return View();
        }

        public JsonResult GetUserNotByIdInfo(FormCollection form, string id)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            SearchConfig search = new SearchConfig();
            search.PluginCode = id;
            SearchVersionTrack searchv = new SearchVersionTrack();
            string sid = form["SID"].Trim();
            string sname = form["Sname"].Trim();
            string sgust = form["Sgust"].Trim();
            if (sgust.Equals(sname))
                searchv.UserUid = sid;
            else
                searchv.UserName = sgust;
            data = BoFactory.GetVersionTrackBo.QueryUserInfoNotByPId(search,searchv, view);
            return Json(data);
        }

        public ContentResult GetNameList()
        {
            var r = new ContentResult();
            r.ContentType = "application/text";

            SearchVersionTrack search = new SearchVersionTrack();
            search.UserName = Request.QueryString["q"].ToString().Trim();
            IList<UserInfo> list = BoFactory.GetVersionTrackBo.GetStuNameList(search);
            if (list != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (UserInfo stu in list)
                {
  //                  sb.AppendLine(stu.UserName + "|" + stu.UserUId);
                }
                r.Content = sb.ToString();
            }

            return r;
        }



        public ActionResult AddSomeUserInfos(string id, string pid)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    string[] idsArray = id.Split(',');
                    IList<UserPluginRef> list = new List<UserPluginRef>();

                    for (int i = 0; i < idsArray.Length; i++)
                    {
                        var item = idsArray[i];
                        if (!string.IsNullOrEmpty(item))
                        {
                            UserPluginRef c = new UserPluginRef();
                            c.UserUId = Convert.ToString(item);
                            c.PluginCode = pid;
                            list.Add(c);
                        }

                    }
                    BoFactory.GetVersionTrackBo.InsertUserRef(list);
                    vmobject.IsSuccess = true;
                    vmobject.Msg = "操作成功!";
                }
                else
                {
                    vmobject.IsSuccess = false;
                    vmobject.Msg = "您没有选择";
                }
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }
            return Json(vmobject);
        }

        #endregion


        #region 插件版本管理

        #region 第一步，版本信息列表

        public ActionResult PluginVersionList()
        {
            return View();
        }
        //第一步。获得版本列表
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetVersionTrackList(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryPluginNotWeb(view);
            return Json(data);
        }

        #endregion

        #region 第二步，上传文件列表

        public ActionResult UploadVersionFile(string id,string IsAdd)//版本ID
        {
            VersionTrack VerInfo = null;
            ViewData["IsAdd"] = "";
            ViewData["versionId"] = "";//初始化
            if (string.IsNullOrEmpty(id))
                VerInfo = new VersionTrack();
            else
                ViewData["versionId"] = id;//版本ID

            if (!string.IsNullOrEmpty(IsAdd))
                ViewData["IsAdd"] = IsAdd;

            return View(VerInfo);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveVerInfo(VersionTrack ver,string IsAdd, FormCollection form)
        {
            var vmobject = new JsonReturnMessages();

                
                string vid = "";
                if (Request.Files.Count > 0)
                {
                    XmlConfigInfo xmlInfo = Decompressing();
                    try
                    {
                        //vid = BoFactory.GetVersionTrackBo.SavePluingZipInfo(ver, xmlInfo, IsAdd, base.CurrentUser.UserUId);
                        MoveFolder(xmlInfo);
                    }
                    catch (Exception ex)
                    {
                        string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
                        while (Directory.Exists(thisFilePath))//先删除原由临时文件夹
                        {
                            Directory.Delete(thisFilePath, true);
                        }
                        FileInfo f = new FileInfo(xmlInfo.savefile);//删除原本保存的zip
                        f.Delete();
                        vmobject.IsSuccess = false;
                        vmobject.Msg = ex.Message;
                        return Json(vmobject, "text/html");
                    }
                }
                else
                    vid = ver.VersionId.ToString();

                vmobject.IsSuccess = true;
                vmobject.Msg = vid;

            return Json(vmobject, "text/html");
        }

        #endregion

        #region 第三步，修改插件基本信息

        public ActionResult UpdatePluginInfo(string verid)
        {
            ViewData["actionCode"] = "";
            ViewData["summary"] = "";
            ViewData["IsAction"] = "";
            SearchVersionTrack search = new SearchVersionTrack();
            search.VID = verid;
            VersionTrack version = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];//获取插件ID

            PluginInfo pinfo = BoFactory.GetVersionTrackBo.GetPluginInfo(version.PluginCode.ToString());//获取插件信息
            if (pinfo.PluginCateCode.Equals(Constants.ActionCateCode))
                ViewData["IsAction"] = "1";

            IList<PluginCategory> list = BoFactory.GetVersionTrackBo.GetPluginCategoryInfo();//获取非web插件的分类信息
            if (list != null)
            {
                if (list.Count > 0)
                {
                    IResourceData[] datas = new IResourceData[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        datas[i] = ResourceDataFactory.GetResourceDataInstance();
                        datas[i].Code = list[i].PluginCateCode;
                        datas[i].Name = list[i].DisplayName;
                    }
                    ViewData["list"] = datas;
                }
            }
            
            SearchConfig searchconfig = new SearchConfig();
            searchconfig.PluginCode = version.PluginCode.ToString();
            IList<ActionExtend> listAction = BoFactory.GetVersionTrackBo.QueryActionExtend(searchconfig);//获取扩展信息
            if (listAction.Count > 0)
            {
                ViewData["actionCode"] = listAction[0].ActionCode;
                ViewData["summary"] = listAction[0].Summary;
            }

            ViewData["versionId"] = verid;
            ViewData["vername"] = version.VersionName;

            return View(pinfo);
        }

        public ActionResult SavePluginInfo(FormCollection form, PluginInfo pluginInfo,ActionExtend actionExtend)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                actionExtend.Summary = form["ESummary"].ToString();//扩展信息描述

                BoFactory.GetVersionTrackBo.UpdatePluginInfo(pluginInfo, actionExtend);

                vmobject.IsSuccess = true;
                vmobject.Msg = "操作成功!";
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }
            return Json(vmobject);
        }

        #endregion

        #region 第四步，插件配置信息

        public ActionResult UpdatePluginConfig(string id)
        {
            SearchVersionTrack search = new SearchVersionTrack();
            search.VID = id;
            ViewData["pId"] = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0].PluginCode.ToString();
            ViewData["verid"] = id;//版本ID
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetConfigInfo(FormCollection form, string id)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            SearchConfig search = new SearchConfig();
            search.PluginCode = id;//pluginId
            data = BoFactory.GetVersionTrackBo.QueryConfigInfo(view, search);
            return Json(data);
        }

        public ActionResult AddConfigInfo(string id, string pid)
        {
            ConfigInfo CInfo = null;
            if (string.IsNullOrEmpty(id))//id=cofingId
                CInfo = new ConfigInfo();
            else
                CInfo = BoFactory.GetVersionTrackBo.GetConfigInfoById(id);

            IResourceData[] datas = new IResourceData[1];
            datas[0] = ResourceDataFactory.GetResourceDataInstance();
            datas[0].Code = "PluginConfig";
            datas[0].Name = "常规个人插件配置";
            ViewData["list"] = datas;

            ViewData["pid"] = pid;//pluginId
            return View(CInfo);

        }

        public ActionResult DelSomeConfigInfos(string id)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    string[] idsArray = id.Split(',');
                    IList<ConfigInfo> list = new List<ConfigInfo>();

                    for (int i = 0; i < idsArray.Length; i++)
                    {
                        var item = idsArray[i];
                        if (!string.IsNullOrEmpty(item))
                        {
                            ConfigInfo c = new ConfigInfo();
                            c.ConfigId = Convert.ToString(item);
                            list.Add(c);
                        }

                    }
                    BoFactory.GetVersionTrackBo.DelConfigInfo(list);
                    vmobject.IsSuccess = true;
                    vmobject.Msg = "操作成功!";
                }
                else
                {
                    vmobject.IsSuccess = false;
                    vmobject.Msg = "您没有选择";
                }
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }
            return Json(vmobject);
        }

        public ActionResult EditConfigs(FormCollection form, ConfigInfo cog)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                cog.Key1 = form["[key]"];
                if (string.IsNullOrEmpty(cog.ConfigId))
                {
                    IList<ConfigInfo> list = new List<ConfigInfo>();
                    list.Add(cog);
                    //BoFactory.GetVersionTrackBo.InserConfigInfo(list, cog.PluginCode);
                }
                else
                {
                    BoFactory.GetVersionTrackBo.UpdateConfigInfo(cog);
                }
                vmobject.IsSuccess = true;
                vmobject.Msg = "操作成功!";
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }
            return Json(vmobject);
        }


        #endregion

        #region  第五步，发布

        public ActionResult PublishInfo(string id)
        {
            SearchVersionTrack search = new SearchVersionTrack();
            search.VID = id;
            VersionTrack ver = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
            return View(ver);
        }

        public ActionResult Publishs(VersionTrack ver)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                InsertXml(ver);//1。写入XML

                ver.LastModTime = DateTime.Now;
                ver.LastModUid = base.CurrentUser.UserUId;
                ver.VersionStatus = 1;

                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = ver.PreVersionId.ToString();
                IList<VersionTrack> list = BoFactory.GetVersionTrackBo.GetVersionTrack(search);//获取上一个版本。

                string name = BoFactory.GetVersionTrackBo.GetPluginInfo(ver.PluginCode.ToString()).PluginCode;//获取插件code
               
                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (list.Count > 0)//若不存在此记录说明是第一次发布
                    pub.UpdateApplication(ver.FilePath, name);
                else
                    pub.CreateApplication(ver.FilePath, name);

                BoFactory.GetVersionTrackBo.UpdatePlushVersionTrack(ver);//更新状态

                vmobject.IsSuccess = true;
                vmobject.Msg = "操作成功!";
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }
            return Json(vmobject);
        }

        #endregion

        #endregion


        #region 私有方法

        /// <summary>
        /// 解压、获取信息、移动文件夹
        /// </summary>
        /// <returns></returns>
        private XmlConfigInfo Decompressing()
        {
            string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
            while (!Directory.Exists(thisFilePath))//创建临时文件夹
            {
                Directory.CreateDirectory(thisFilePath);
            }
            string zipPaht = AppConfig.SaveZipPath;
            while (!Directory.Exists(zipPaht))//创建存放zip文件的文件夹
            {
                Directory.CreateDirectory(zipPaht);
            }
            string saveFile = zipPaht + @"\" + System.Guid.NewGuid().ToString() + ".zip";
            Request.Files[0].SaveAs(saveFile);//保存zip去文件夹下

            CommonMethods.Uncompress(saveFile, thisFilePath);//解压
            XmlConfigInfo xmlInfo = GetXmlInfo(thisFilePath);//获取解析出来的配置信息

            //string oldfilepath = zipPaht + @"\" + xmlInfo.PluginInfo.PluginCode + xmlInfo.PluginInfo.Version + ".zip";//已上传的zip文件
            //xmlInfo.savefile = saveFile;
            //xmlInfo.oldfile = oldfilepath;

            return xmlInfo;
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// <param name="xmlInfo"></param>
        private void MoveFolder(XmlConfigInfo xmlInfo)
        {
            FileInfo f = new FileInfo(xmlInfo.savefile);
            FileInfo oldfile = new FileInfo(xmlInfo.oldfile);
            if (oldfile.Exists)//判断是否需要删除原由文件
                oldfile.Delete();
            f.MoveTo(xmlInfo.oldfile);//规则：code+版本名

            string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
            //创建文件以versionname为名的文件夹在指定根目录下
            //string FilePath = AppConfig.filePath + xmlInfo.PluginInfo.PluginCode;//当前插件CODE文件夹
            //while (!Directory.Exists(FilePath))
            //{
            //    Directory.CreateDirectory(FilePath);
            //}

            //string thispath = FilePath + @"\"; //+ xmlInfo.VersionName;//当前移动的去的文件夹
            //if (Directory.Exists(thispath))
            //    Directory.Delete(thispath, true);

            //Directory.Move(thisFilePath, thispath);//移动
        }

        /// <summary>
        /// 解析XML获取信息
        /// </summary>
        /// <returns></returns>
        private XmlConfigInfo GetXmlInfo(string xmlpath)
        {
            XmlConfigInfo x = CommonMethods.GetEntityFromXML(xmlpath + @"\" + Common.Entities.Constants.PluginPath);
            return x;
        }

        /// <summary>
        /// 写入XML
        /// </summary>
        /// <param name="path"></param>
        private void InsertXml(VersionTrack ver)
        {
            XmlConfigInfo xml = new XmlConfigInfo();
            //xml.VersionName = ver.VersionName;
            //xml.PluginInfo = BoFactory.GetVersionTrackBo.GetPluginInfo(ver.PluginCode.ToString());//获取插件信息

            SearchConfig serach = new SearchConfig();
            serach.PluginCode = ver.PluginCode.ToString();
            //xml.configList = BoFactory.GetVersionTrackBo.GetConfigList(serach);//获取配置信息

            CommonMethods.WritePluginfoConfigXml(xml, ver.FilePath.Trim() + @"\" + Constants.pluginName);
        }

        #endregion

    }
}
