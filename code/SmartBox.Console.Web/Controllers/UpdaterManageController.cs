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
using System.IO;
using System.Collections;

namespace SmartBox.Console.Web.Controllers
{
    public class UpdaterManageController : MyControllerBase
    {
        #region 2.0.1.0

        public ActionResult DelVersions(string vid)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                SearchVersionTrack searchv = new SearchVersionTrack();
                searchv.VID = vid;
                var p = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0];
                ArrayList arr = new ArrayList();
                arr.Add(vid);
                if (p.VersionStatus == 2)
                {
                    BoFactory.GetVersionTrackBo.DelVersion(arr);
                    vmobject.IsSuccess = true;
                }
                else if (p.VersionStatus == 1)
                {
                    BoFactory.GetVersionTrackBo.DelActiveVersionByUpdater(arr);
                    vmobject.IsSuccess = true;
                }
                else
                {
                    vmobject.IsSuccess = false;
                    vmobject.Msg = "不能删除未发布的版本,请先完成发布,或者删除整个升级程序！";
                }
            }
            catch (Exception e)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = e.Message;
            }

            return Json(vmobject);
        }

        public ActionResult DelAllVersions()
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.PluginCode = Constants.UpdaterCode;
                var p = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                ArrayList arr = new ArrayList();
                arr.Add(p.VersionId.ToString());
                BoFactory.GetVersionTrackBo.DelAllVersionByMain(arr);
                vmobject.IsSuccess = true;
            }
            catch (Exception e)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = e.Message;
            }

            return Json(vmobject);
        }

        public ActionResult ResumeVersions(string vid)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                SearchVersionTrack searchv = new SearchVersionTrack();
                searchv.VID = vid;
                var p = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0];
                if (p.VersionStatus != 2)
                {
                    vmobject.IsSuccess = false;
                    vmobject.Msg = "当前为已发布的版本或者还未发布的,请选择过期版本!";
                }
                ArrayList arr = new ArrayList();
                arr.Add(vid);

                BoFactory.GetVersionTrackBo.ResumeExpiredVesionByUpdater(arr);
                vmobject.IsSuccess = true;
            }
            catch (Exception e)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = e.Message;
            }

            return Json(vmobject);
        }

        #endregion

        #region 修改配置

        public ActionResult UpdateConfigInfo(string id)
        {
            ViewData["vid"] = id;
            IList<PluginInfo> plist = new List<PluginInfo>();
            ViewData["configList"] = 0;

            SearchVersionTrack searchv = new SearchVersionTrack();
            searchv.VID = id;
            VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0];

            SearchConfig search = new SearchConfig();
            search.PluginCode = Constants.UpdaterCode;
            search.ConfigCategoryCode = Constants.UpdaterConfig;
            IList<ConfigInfo> listconfigs = BoFactory.GetVersionTrackBo.GetConfigList(search);
            listconfigs = InitConfigs(listconfigs);//初始化
            v.configList = listconfigs;

            IEnumerable<IGrouping<string, ConfigInfo>> listccc = v.configList.GroupBy(T => T.ConfigCategoryCode).ToList();
            ViewData["keys"] = listccc.First().Key;
            ViewData["lists"] = listccc;
            ViewData["configList"] = v.configList.Count;

            return View(v);
        }

        [ValidateInput(false)]
        public ActionResult SaveConfigInfo(FormCollection form, string Vid)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                IList<ConfigInfo> list = new List<ConfigInfo>();
                string[] keys = form.AllKeys;
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].EndsWith(".Key1"))
                    {
                        ConfigInfo config = new ConfigInfo();
                        string prefix = keys[i].Split('.')[0];
                        UpdateModel(config, prefix);  //其他数据同时可以获得填充进实体对象
                        config.PluginCode = Constants.UpdaterCode;
                        list.Add(config);
                    }
                }
                //更新
                BoFactory.GetVersionTrackBo.UpdateUpdaterConfigInfos(list);//发布

                InsertConfigXml(Vid);//生成config

                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = Vid;
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                string name = codes[codes.Length - 2];//获取插件code

                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (Directory.Exists(Path.Combine(AppConfig.pubFolder, name + AppConfig.subFix)))//如果存在次文件夹
                    pub.UpdateApplication(v.FilePath, name);
                else
                    pub.CreateApplication(v.FilePath, name);

                vmobject.IsSuccess = true;
                vmobject.Msg = "操作成功";

            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }
            return Json(vmobject);
        }

        #endregion

        #region 列表

        public ActionResult UpdaterManage()
        {
            return View();
        }

        public ActionResult GetUpdaterInfo(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryUpdaterInfo(view);
            return Json(data);
        }

        #endregion

        #region 下载

        public ActionResult GetDownLoadFile(FormCollection form)
        {
            var vmobject = new JsonReturnMessages();

            try
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = form["vid"].ToString();
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                string filename = Path.Combine(AppConfig.SaveZipPath, codes[codes.Length - 1] + ".zip");
                MemoryStream m = new MemoryStream(System.IO.File.ReadAllBytes(filename));
                DownLoadFile(m.ToArray(), v.PluginCode + v.VersionName + ".zip", m);

            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }

            vmobject.IsSuccess = true;

            return Json(vmobject);
        }


        #endregion

        #region 发布向导

        public ActionResult PluginWizard(string Vid, string IsAdd, string IsUpdate)
        {
            ViewData["Vid"] = Vid;
            ViewData["IsUpdate"] = "";
            ViewData["IsVersion"] = "";
            if (!string.IsNullOrEmpty(IsUpdate))
            {
                ViewData["IsUpdate"] = IsUpdate;
                ViewData["IsVersion"] = "1";
            }
            ViewData["IsAdd"] = IsAdd;
            return View();
        }


        public ActionResult GetInfoTree(FormCollection form)
        {
            var nodes = TreeRootDefine.CreateMainDictInfoTreeNode();
            return Json(nodes);
        }

        #endregion

        #region 1。版本基本信息

        public ActionResult UpdaterInfo(string IsUpdate, string IsAdd, string Vid, string AddVid)
        {
            ViewData["IsUpdate"] = "";
            ViewData["IsAdd"] = IsAdd;
            ViewData["Vid"] = Vid;
            VersionTrack v = new VersionTrack();
            if (!string.IsNullOrEmpty(IsUpdate))//若为修改
            {
                string id = "";
                if (AddVid.Equals("-1"))
                    id = Vid;
                else
                    id = AddVid;
                ViewData["IsUpdate"] = IsUpdate;
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = id;
                v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
            }
            return View(v);
        }

        public ActionResult SaveVersions(string IsAdd, string IsUpdate, string Vid, VersionTrack version, FormCollection form)
        {
            var vmobject = new JsonReturnMessages();
            string vid = "";
            try
            {
                vid = BoFactory.GetVersionTrackBo.SaveUpdaterVerInfo(version, IsAdd, IsUpdate, base.CurrentUser.UserUId, Vid);
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
                return Json(vmobject, "text/html");
            }

            vmobject.Msg = vid;
            vmobject.IsSuccess = true;
            return Json(vmobject, "text/html");
        }

        #endregion

        #region 2。上传文件

        public ActionResult UploadVersionFile(string IsAdd, string Vid, string IsUpdate, string AddVid)
        {
            ViewData["IsAdd"] = IsAdd;
            ViewData["IsUpdate"] = "";
            if (!string.IsNullOrEmpty(IsUpdate))
                ViewData["IsUpdate"] = IsUpdate;
            if (AddVid.Equals("-1"))
                ViewData["Vid"] = Vid;
            else
                ViewData["Vid"] = AddVid;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveFileInfo(VersionTrack ver, string IsAdd, string IsUpdate, FormCollection form)
        {
            var vmobject = new JsonReturnMessages();
            string vid = form["VersionIds"].ToString();
            XmlMainConfigInfo xmlInfo = null;

            try
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = vid;
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                if (Request.Files.Count > 0)//若有上传文件
                    xmlInfo = Decompressing(v.VersionName);//解压

                vid = BoFactory.GetVersionTrackBo.SaveUpdaterZipInfo(ver, xmlInfo, IsAdd, IsUpdate, base.CurrentUser.UserUId, form["VersionIds"].ToString());

                if (Request.Files.Count > 0)//若有上传文件
                    MoveFolder(xmlInfo, v.VersionName);//移动文件夹及文件
            }
            catch (Exception ex)
            {
                try
                {
                    string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
                    while (Directory.Exists(thisFilePath))//先删除原由临时文件夹
                    {
                        Directory.Delete(thisFilePath, true);
                    }
                    if (!string.IsNullOrEmpty(xmlInfo.savefile))
                    {
                        FileInfo f = new FileInfo(xmlInfo.savefile);//删除原本保存的zip
                        if (f.Exists)
                            f.Delete();
                    }
                }
                catch (Exception fe)
                {
                    vmobject.IsSuccess = false;
                    vmobject.Msg = fe.Message;
                }

                vmobject.IsSuccess = false;
                vmobject.Msg = vmobject.Msg + "," + ex.Message;
                return Json(vmobject, "text/html");
            }

            vmobject.Msg = vid;
            vmobject.IsSuccess = true;
            return Json(vmobject, "text/html");
        }


        #endregion

        #region 3。修改配置信息

        public ActionResult UpdatePluginInfo(string Vid, string IsAdd)
        {
            ViewData["configList"] = 0;
            ViewData["IsAdd"] = IsAdd;
            SearchVersionTrack search = new SearchVersionTrack();
            search.VID = Vid;
            VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];

            SearchConfig searchc = new SearchConfig();
            searchc.PluginCode = v.PluginCode.ToString();
            searchc.ConfigCategoryCode = Constants.UpdaterConfig;
            IList<ConfigInfo> list = BoFactory.GetVersionTrackBo.GetConfigList(searchc);//获取原有参数

            Dictionary<string, ConfigInfo> dic = new Dictionary<string, ConfigInfo>();
            foreach (var item in list)
            {
                var key = item.Key1 + "_FG$SP_" + item.PluginCode + "_FG$SP_" + item.ConfigCategoryCode;
                ConfigInfo temp = new ConfigInfo();
                temp.Key1 = item.Key1;
                temp.OldValue = item.Value1;
                temp.ConfigCategoryCode = item.ConfigCategoryCode;
                temp.PluginCode = Constants.UpdaterCode;
                dic.Add(key, temp);
            }
            IList<ConfigTemp> listT = BoFactory.GetVersionTrackBo.GetConfigListTemp(searchc);//现有临时表数据
            foreach (var item in listT)
            {
                var key = item.Key1 + "_FG$SP_" + item.PluginCode + "_FG$SP_" + item.ConfigCategoryCode;
                if (dic.ContainsKey(key))
                    dic[key].Value1 = item.Value1;
                else
                {
                    ConfigInfo temp = new ConfigInfo();
                    temp.Key1 = item.Key1;
                    temp.Value1 = item.Value1;
                    temp.OldValue = "";
                    temp.PluginCode = Constants.UpdaterCode;
                    temp.ConfigCategoryCode = item.ConfigCategoryCode;
                    dic.Add(key, temp);
                }
            }

            IList<ConfigInfo> listconfigs = dic.Values.ToList<ConfigInfo>();
            listconfigs = InitConfigs(listconfigs);//初始化
            v.configList = listconfigs;

            IEnumerable<IGrouping<string, ConfigInfo>> listccc = v.configList.GroupBy(T => T.ConfigCategoryCode).ToList();
            listccc = listccc.OrderBy(T => T.Key);
            ViewData["keys"] = listccc.First().Key;
            ViewData["lists"] = listccc;
            ViewData["configList"] = v.configList.Count;

            return View(v);
        }



        [ValidateInput(false)]
        public ActionResult SavePluginInfos(FormCollection form, ConfigTemp c, string IsAdd)
        {
            var vmobject = new JsonReturnMessages();
            IList<ConfigTemp> list = new List<ConfigTemp>();

            try
            {
                string[] keys = form.AllKeys;
                //获取列表参数
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].EndsWith(".Key1"))
                    {
                        ConfigTemp config = new ConfigTemp();
                        string prefix = keys[i].Split('.')[0];
                        UpdateModel(config, prefix);  //其他数据同时可以获得填充进实体对象
                        config.PluginCode = Constants.UpdaterCode;
                        list.Add(config);
                    }
                }

                BoFactory.GetVersionTrackBo.UpdateUpdaterTempPlugin(list, base.CurrentUser.UserUId);
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
                return Json(vmobject);
            }

            vmobject.IsSuccess = true;
            vmobject.Msg = form["ver"].ToString();
            return Json(vmobject);
        }

        #endregion


        #region 4。发布

        public ActionResult PublishInfo(string Vid)
        {

            SearchVersionTrack search = new SearchVersionTrack();
            search.VID = Vid;
            VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];

            SearchConfig searchc = new SearchConfig();
            searchc.PluginCode = Constants.UpdaterCode;
            IList<ConfigTemp> list = BoFactory.GetVersionTrackBo.GetConfigListTemp(searchc);

            IList<ConfigInfo> listc = new List<ConfigInfo>();
            foreach (ConfigTemp c in list)
            {
                c.Value1 = c.Value1.Replace("<", "&lt;").Replace(">", "&gt;");
                listc.Add(CommonMethods.ConvertToConfigInfo(c));
            }

            listc = InitConfigs(listc);//初始化
            v.configList = listc;
            IEnumerable<IGrouping<string, ConfigInfo>> listccc = v.configList.GroupBy(T => T.ConfigCategoryCode).ToList();
            listccc = listccc.OrderBy(T => T.Key);

            ViewData["keys"] = listccc.First().Key;
            ViewData["lists"] = listccc;
            ViewData["configList"] = v.configList.Count;

            return View(v);
        }

        public ActionResult SavePublishInfo(string Vid, FormCollection form)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                XmlMainConfigInfo x = InsertXml(Vid);//写入XML
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = Vid;
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                string name = codes[codes.Length - 2];//获取插件code

                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (Directory.Exists(Path.Combine(AppConfig.pubFolder ,name + AppConfig.subFix)))//如果存在次文件夹
                    pub.UpdateApplication(v.FilePath, name);
                else
                    pub.CreateApplication(v.FilePath, name);

                BoFactory.GetVersionTrackBo.UpdateUpdaterPlushVersionTracks(x, Vid, base.CurrentUser.UserUId);//更新状态


                vmobject.IsSuccess = true;
                vmobject.Msg = "操作成功";
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }

            return Json(vmobject);
        }




        #endregion


        #region private function


        /// <summary>
        /// 解压、获取信息
        /// </summary>
        /// <returns></returns>
        private XmlMainConfigInfo Decompressing(string vername)
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

            string saveFile = Path.Combine(zipPaht, System.Guid.NewGuid().ToString() + ".zip");
            Request.Files[0].SaveAs(saveFile);//保存zip去文件夹下

            CommonMethods.Uncompress(saveFile, thisFilePath);//解压

            XmlMainConfigInfo xmlInfo = new XmlMainConfigInfo();
            xmlInfo.savefile = saveFile;

            xmlInfo = GetXmlInfo(thisFilePath);//获取解析出来的配置信息

            xmlInfo.oldCode = Constants.UpdaterCode;//否则以第一个CODE为文件夹
            //zip文件有第一个CODE+版本号命名
            string oldfilepath = Path.Combine(zipPaht, Constants.UpdaterCode + "_FG$SP_" + vername + ".zip");//重命名的zip文件
            xmlInfo.savefile = saveFile;
            xmlInfo.oldfile = oldfilepath;

            return xmlInfo;
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// <param name="xmlInfo"></param>
        private void MoveFolder(XmlMainConfigInfo xmlInfo, string vername)
        {
            FileInfo f = new FileInfo(xmlInfo.savefile);
            //移动前。需要删除移动去的位置是否存在原由文件
            FileInfo oldfile = new FileInfo(xmlInfo.oldfile);
            if (oldfile.Exists)//判断是否需要删除原由文件
                oldfile.Delete();
            f.MoveTo(xmlInfo.oldfile);//规则：code+版本名-------

            string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
            //创建文件以versionname为名的文件夹在指定根目录下
            string FilePath = AppConfig.filePath + xmlInfo.oldCode;//当前插件CODE文件夹
            while (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            //规则：以第一个插件的CODE为主文件夹，以第一个插件的CODE+版本命名的版本文件夹
            string thispath = Path.Combine(FilePath, Constants.UpdaterCode + "_FG$SP_" + vername);//当前移动的去的文件夹
            //移动前删除新CODE下是否存在该版本文件夹
            if (Directory.Exists(thispath))
                Directory.Delete(thispath, true);

            Directory.Move(thisFilePath, thispath);//移动--------
        }

        /// <summary>
        /// 解析XML获取信息
        /// </summary>
        /// <returns></returns>
        private XmlMainConfigInfo GetXmlInfo(string xmlpath)
        {
            XmlMainConfigInfo x = new XmlMainConfigInfo();
            IList<ConfigInfoPC> listG = CommonMethods.GetMainEntityFromXMLPC(Path.Combine(xmlpath, Common.Entities.Constants.UpdaterName));
            x.configList = listG;
            return x;
        }


        /// <summary>
        /// 写入XML
        /// </summary>
        /// <param name="path"></param>
        private XmlMainConfigInfo InsertXml(string Vid)
        {
            XmlMainConfigInfo xml = new XmlMainConfigInfo();
            SearchVersionTrack searchv = new SearchVersionTrack();
            SearchConfig serach = new SearchConfig();

            searchv.VID = Vid;
            VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0];

            serach.PluginCode = v.PluginCode.ToString();
            IList<ConfigTemp> listct = BoFactory.GetVersionTrackBo.GetConfigListTemp(serach);//获取配置信息

            IList<ConfigInfoPC> listc = new List<ConfigInfoPC>();
            foreach (ConfigTemp c in listct)
            {
                listc.Add(CommonMethods.ConvertToConfigInfoPC(c));//转换
            }
            xml.configList = listc;

            CommonMethods.WriteMaininfoConfigXml(listc, Path.Combine(v.FilePath.Trim() , Constants.UpdaterName));

            return xml;
        }


        /// <summary>
        /// 写入XML
        /// </summary>
        /// <param name="path"></param>
        private XmlMainConfigInfo InsertConfigXml(string Vid)
        {
            XmlMainConfigInfo xml = new XmlMainConfigInfo();
            SearchVersionTrack searchv = new SearchVersionTrack();
            SearchConfig serach = new SearchConfig();

            searchv.VID = Vid;
            VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0];

            serach.PluginCode = v.PluginCode.ToString();
            IList<ConfigInfoPC> listc = BoFactory.GetVersionTrackBo.GetConfigPCList(serach);//获取配置信息
           
            xml.configList = listc;

            CommonMethods.WriteMaininfoConfigXml(listc, Path.Combine(v.FilePath.Trim(), Constants.UpdaterName));

            return xml;
        }


        private IList<ConfigInfo> InitConfigs(IList<ConfigInfo> listconfigs)
        {
            ConfigInfo c1 = new ConfigInfo();
            c1.Key1 = "";
            c1.ConfigCategoryCode = Constants.UpdaterConfig;
            listconfigs.Add(c1);

            return listconfigs;
        }

        private void DownLoadFile(byte[] Buffer, string ReportTitle, MemoryStream ms)
        {
            ReportTitle = Server.UrlEncode(ReportTitle);
            Response.Clear();
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ReportTitle);
            // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
            Response.AddHeader("Content-Length", Buffer.Length.ToString());
            // 指定返回的是一个不能被客户端读取的流，必须被下载 
            Response.ContentType = "application/octet-stream";
            // 把文件流发送到客户端 
            Response.BinaryWrite(Buffer);
            Response.Flush();
            ms.Close();
            HttpContext.ApplicationInstance.CompleteRequest();
        }

        #endregion

    }
}
