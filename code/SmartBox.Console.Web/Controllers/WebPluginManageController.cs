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
using SmartBox.Console.Common.Entities;
using Beyondbit.Framework.Biz.Resource;
using System.IO;

namespace SmartBox.Console.Web.Controllers
{
    public class WebPluginManageController : MyControllerBase
    {

        #region 第一步，版本信息列表

        public ActionResult WebPluginVersionList()
        {
            return View();
        }
        //第一步。获得版本列表
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetVersionTrackList(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryPluginInWeb(view);
            return Json(data);
        }

        #endregion

        #region 第二步，插入版本表，插件表信息

        public ActionResult UpdatePluginInfo(string verid, string isAdd)
        {
            PluginInfo pinfo = null;
            ViewData["vername"] = "";
            ViewData["actionCode"] = "";
            ViewData["summary"] = "";
            ViewData["versionId"] = "";
            ViewData["isAdd"] = "";//是否升级

            IResourceData[] datas = new IResourceData[1];
            datas[0] = ResourceDataFactory.GetResourceDataInstance();
            datas[0].Code = "WebPlugin";
            datas[0].Name = "Web页面插件";
            ViewData["list"] = datas;

            if (string.IsNullOrEmpty(verid))//若为新增
                pinfo = new PluginInfo();
            else
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = verid;
                VersionTrack vsion = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];//获取版本信息

                pinfo = BoFactory.GetVersionTrackBo.GetPluginInfo(vsion.PluginCode.ToString());//获取插件信息

                if (isAdd.Equals("0"))//若为修改（可修改版本信息,扩展信息，插件信息）
                {
                    ViewData["vername"] = vsion.VersionName;//获取版本信息

                    SearchConfig searchconfig = new SearchConfig();
                    searchconfig.PluginCode = vsion.PluginCode.ToString();
                    IList<ActionExtend> listAction = BoFactory.GetVersionTrackBo.QueryActionExtend(searchconfig);//获取扩展信息
                    if (listAction.Count > 0)
                    {
                        ViewData["actionCode"] = listAction[0].ActionCode;
                        ViewData["summary"] = listAction[0].Summary;
                    }
                }
                else//（只可修改插件信息）
                { }
                ViewData["versionId"] = verid;
            }
            if (!string.IsNullOrEmpty(isAdd))
                ViewData["isAdd"] = isAdd;

            return View(pinfo);
        }


        public ActionResult SavePluginInfo(FormCollection form, PluginInfo pluginInfo, ActionExtend actionExtend, VersionTrack version1, string isAdd)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                actionExtend.Summary = form["ESummary"].ToString();//扩展信息描述
                //获取通用的版本信息（修改，升级，新赠）
                version1.VersionStatus = 0;
                version1.FilePath = AppConfig.filePath + pluginInfo.PluginCode + @"\" + version1.VersionName;
                version1.CreateUid = base.CurrentUser.UserUId;
                version1.LastModUid = base.CurrentUser.UserUId;
                string filepaht = version1.FilePath;

                if (version1.VersionId == 0)//即新增
                {
                    int vid = BoFactory.GetVersionTrackBo.InsertWebPluginInfo(pluginInfo, actionExtend, version1, base.CurrentUser.UserUId);
                    vmobject.Msg = vid.ToString();
                }
                else
                {
                    filepaht = BoFactory.GetVersionTrackBo.UpdateWebPluginInfo(pluginInfo, actionExtend, version1, isAdd, base.CurrentUser.UserUId);
                    vmobject.Msg = version1.VersionId.ToString();
                }

                //创建文件夹
                while (!Directory.Exists(filepaht))
                {
                    Directory.CreateDirectory(filepaht);
                }

                vmobject.IsSuccess = true;
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
                InsertXml(ver);

                string name = BoFactory.GetVersionTrackBo.GetPluginInfo(ver.PluginCode.ToString()).PluginCode;

                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = ver.PreVersionId.ToString();
                IList<VersionTrack> list = BoFactory.GetVersionTrackBo.GetVersionTrack(search);//获取上一个版本。

                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (list.Count > 0)//若不存在此记录说明是第一次发布
                    pub.UpdateApplication(ver.FilePath, name);
                else
                    pub.CreateApplication(ver.FilePath, name);

                ver.LastModTime = DateTime.Now;
                ver.LastModUid = base.CurrentUser.UserUId;
                ver.VersionStatus = 1;
                BoFactory.GetVersionTrackBo.UpdatePlushVersionTrack(ver);

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


        #region 私有方法
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
