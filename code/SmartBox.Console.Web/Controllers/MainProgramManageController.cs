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
using SmartBox.Console.Common.Entities.Search;
using SmartBox.Console.Common.Entities;
using System.IO;
using Beyondbit.Framework.Biz.Resource;

namespace SmartBox.Console.Web.Controllers
{
    public class MainProgramManageController2 : MyControllerBase
    {
       
        #region 主程序管理

        #region 第一步，版本信息列表
        //第一步。获得版本列表
        public ActionResult ProgramList()
        {
            SearchVersionTrack search = new SearchVersionTrack();
            search.PluginCode = Common.Entities.Constants.MianPluginId.ToString();
            search.VersionStatus = "0";
            IList<VersionTrack> list = BoFactory.GetVersionTrackBo.GetVersionTrack(search);//获取未发布的版本信息
            ViewData["versionId"] = "";
            if (list.Count > 0)
                ViewData["versionId"] = list[0].VersionId;//为提供页面判断需要进行修改还是重新上传

            return View();
        }
        //第一步。获得版本列表
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetVersionTrackList(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            SearchVersionTrack search = new SearchVersionTrack();
            search.PluginCode = Common.Entities.Constants.MianPluginId.ToString();
            data = BoFactory.GetVersionTrackBo.QueryVersionTrackInfo(view, search);
            return Json(data);
        }

        #endregion

        #region 第二步，上传文件列表

        public ActionResult UploadVersionFile(string id)//版本ID
        {
            VersionTrack VerInfo = null;
            if (string.IsNullOrEmpty(id))
            {
                VerInfo = new VersionTrack();
                ViewData["versionId"] = "";//初始化
            }
            else
                ViewData["versionId"] = id;//版本ID
            return View(VerInfo);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveVerInfo(VersionTrack ver, FormCollection form)
        {
            var vmobject = new JsonReturnMessages();
            string vid = "";
            if (Request.Files.Count > 0)//若有文件上传
            {
                XmlConfigInfo xmlInfo = Decompressing(); //1.解压、获取信息
                try
                {
                    //vid = BoFactory.GetVersionTrackBo.SaveZipInfo(ver, xmlInfo, base.CurrentUser.UserUId);
                    MoveFolder(xmlInfo);//、移动文件夹
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

        #region 第三步，主程序对应的配置信息表（包括管理，新增改删）

        public ActionResult UpdateConfigInfo(string verid, string IsLoadFile)
        {
            ViewData["pId"] = -1;//存放PluginCode
            ViewData["verid"] = verid;//版本ID
            ViewData["IsLoadFile"] = "";
            if (!string.IsNullOrEmpty(IsLoadFile))
                ViewData["IsLoadFile"] = IsLoadFile;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetConfigInfo(FormCollection form,string id)
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

            IList<ConfigCategory> list = BoFactory.GetVersionTrackBo.GetMainCategoryList();//获取主程序配置分类信息
            if (list != null)
            {
                if (list.Count > 0)
                {
                    IResourceData[] datas = new IResourceData[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        datas[i] = ResourceDataFactory.GetResourceDataInstance();
                        datas[i].Code = list[i].ConfigCategoryCode;
                        datas[i].Name = list[i].DisplayName;
                    }
                    ViewData["list"] = datas;
                }
            }
            ViewData["pid"] = pid;//PluginCode

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

        public ActionResult EditConfigs(FormCollection form, ConfigInfo cog,PluginInfo plu)
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

        #region  第四步，发布

        public ActionResult PublishInfo(string id, string IsLoadFile)
        {
            ViewData["IsLoadFile"] = "";
            if (!string.IsNullOrEmpty(IsLoadFile))
                ViewData["IsLoadFile"] = IsLoadFile;//是否单个上传文件
            SearchVersionTrack search = new SearchVersionTrack();
            search.VID = id;
            VersionTrack ver = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
            return View(ver);
        }

        public ActionResult Publishs(VersionTrack ver, string IsLoadFile)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                //1。写入XML
                InsertXml(ver.FilePath);
                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (!string.IsNullOrEmpty(IsLoadFile))//若为单个上传文件发布
                {
                    //读去ver.FilePath发布单个文件
                    pub.UpdateFile(ver.FilePath + @"\" + Common.Entities.Constants.globalName, Constants.MianName, "./");
                    pub.UpdateFile(ver.FilePath + @"\" + Common.Entities.Constants.systemName, Constants.MianName, "./");
                }
                else
                {
                    //2。发布
                    SearchVersionTrack search = new SearchVersionTrack();
                    search.VID = ver.PreVersionId.ToString();//获取上一个版本。
                    IList<VersionTrack> list = BoFactory.GetVersionTrackBo.GetVersionTrack(search);
                    if (list.Count > 0)//若不存在此记录说明是第一次发布
                        pub.UpdateApplication(ver.FilePath, Common.Entities.Constants.MianName);
                    else
                        pub.CreateApplication(ver.FilePath, Common.Entities.Constants.MianName);

                    //3。更新
                    ver.LastModTime = DateTime.Now;
                    ver.LastModUid = base.CurrentUser.UserUId;
                    ver.VersionStatus = 1;
                    BoFactory.GetVersionTrackBo.UpdatePlushVersionTrack(ver);
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

        #endregion

        #region 私有方法

        /// <summary>
        /// 解压、获取信息
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

            //string oldfilepath = zipPaht + @"\" + Constants.MianName + xmlInfo.PluginInfo.Version + ".zip";//已上传的zip文件
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
            string FilePath = AppConfig.filePath + Common.Entities.Constants.MianName;//当前主程序文件夹
            while (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            //string thispath = FilePath + @"\" + xmlInfo.PluginInfo.Version;//当前移动去的文件夹
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
            XmlConfigInfo x = CommonMethods.GetEntityFromXML(xmlpath + @"\" + Common.Entities.Constants.MianPath);
            return x;
        }

        /// <summary>
        /// 写入XML
        /// </summary>
        /// <param name="path"></param>
        private void InsertXml(string path)
        {/*
          * 暂时不处理-- by 宝玉 @ 20100413
            path = path.Trim();
            SearchConfig serach = new SearchConfig();
            serach.ConfigCategoryCode = Common.Entities.Constants.cofigParam;
            IList<ConfigInfo> list = BoFactory.GetVersionTrackBo.GetConfigList(serach);
            Common.CommonMethods.WriteConfigListXml(list, path + @"\" + Common.Entities.Constants.globalName);
            serach.ConfigCategoryCode = Common.Entities.Constants.cofigParam1;
            list = BoFactory.GetVersionTrackBo.GetConfigList(serach);
            Common.CommonMethods.WriteConfigListXml(list, path + @"\" + Common.Entities.Constants.systemName);
          * */
        }

        #endregion

    }
}
