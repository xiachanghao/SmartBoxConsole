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
using System.IO;
using Beyondbit.Framework.Biz.Resource;
using System.Collections;

namespace SmartBox.Console.Web.Controllers
{
    public class PluginInfoManageController : MyControllerBase
    {

        #region 2.0.1.0

        public ActionResult GetPluginNames(string vid)
        {
            var vmobject = new JsonReturnMessages();
            ArrayList arr = GetArrays(vid);
            string status = "";
            foreach (string a in arr)
            {
                SearchVersionTrack searchv = new SearchVersionTrack();
                searchv.VID = a;
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0];
                vmobject.Msg += v.PluginCode + ",";
                status = v.VersionStatus.ToString();
            }
            vmobject.IsSuccess = true;
            vmobject.Msg = vmobject.Msg.Substring(0, vmobject.Msg.Length - 1) + "|" + status;
            return Json(vmobject);
        }

        public ActionResult DelVersions(string vid)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                ArrayList arr = GetArrays(vid);
                SearchVersionTrack searchv = new SearchVersionTrack();
                searchv.VID = arr[0].ToString();
                var p = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0];
                if (p.VersionStatus == 2)
                {
                    BoFactory.GetVersionTrackBo.DelVersion(arr);
                    vmobject.IsSuccess = true;
                }
                else if (p.VersionStatus == 1)
                {
                    BoFactory.GetVersionTrackBo.DelActiveVersion(arr);
                    vmobject.IsSuccess = true;
                }
                else
                {
                    vmobject.IsSuccess = false;
                    vmobject.Msg = "不能删除未发布的版本,请先完成发布,或者删除整个插件！";
                }
            }
            catch (Exception e)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = e.Message;
            }

            return Json(vmobject);
        }


        public ActionResult DelAllVersions(string vid)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                ArrayList arr = GetArrays(vid);
                BoFactory.GetVersionTrackBo.DelAllVersion(arr);
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
                ArrayList arr = GetArrays(vid);
                BoFactory.GetVersionTrackBo.ResumeExpiredVesion(arr);
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

        #region 修改配置（参数）

        public ActionResult UpdateConfigInfo(string id)
        {
            ViewData["vid"] = id;
            ArrayList vids = GetArrays(id);
            IList<PluginInfo> plist = new List<PluginInfo>();
            ViewData["configList"] = 0;
            foreach (string vid in vids)
            {
                SearchVersionTrack searchv = new SearchVersionTrack();
                searchv.VID = vid;
                id = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0].PluginCode.ToString();
                PluginInfo p = BoFactory.GetVersionTrackBo.GetPluginInfo(id);
                p.VersionSummary = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0].VersionSummary;
                SearchConfig search = new SearchConfig();
                if (p.IsIgnoreConfig == false)
                {
                    search.PluginCode = id;
                    search.ConfigCategoryCode = Constants.configCategory;
                    p.configList = BoFactory.GetVersionTrackBo.GetConfigList(search);
                }
                if (p.PluginCateCode.Equals(Constants.ActionCateCode))
                {
                    search.PluginCode = id;
                    IList<ActionExtend> alist = BoFactory.GetVersionTrackBo.QueryActionExtend(search);
                    if (alist.Count > 0)
                    {
                        ActionExtend a = alist[0];
                        p.ActionCode = a.ActionCode;
                        p.ActionSummary = a.Summary;
                    }
                }
                SearchPlugin sh = new SearchPlugin();
                sh.PluginCateCode = p.PluginCateCode;
                p.PCname = BoFactory.GetVersionTrackBo.GetPluginCategoryInfos(sh)[0].DisplayName;

                plist.Add(p);


                ViewData["configList"] = (int)ViewData["configList"] + p.configList.Count;
            }
            return View(plist);
        }
        [ValidateInput(false)]
        public ActionResult SaveConfigInfo(FormCollection form,PluginInfo p,string vid)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                IList<ConfigInfo> list = new List<ConfigInfo>();
                IList<PluginInfo> listp = new List<PluginInfo>();
                string[] keys = form.AllKeys;
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].EndsWith(".Key1"))
                    {
                        ConfigInfo config = new ConfigInfo();
                        string prefix = keys[i].Split('.')[0];
                        UpdateModel(config, prefix);  //其他数据同时可以获得填充进实体对象
                        config.PluginCode = prefix.Split('|')[0];
                        config.ConfigCategoryCode = Constants.configCategory;
                        list.Add(config);
                    }
                }
                //获得插件CODE
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].EndsWith(".PluginCode"))
                    {
                        PluginInfo pi = new PluginInfo();
                        string prefix = keys[i].Split('.')[0];
                        UpdateModel(pi, prefix);  //其他数据同时可以获得填充进实体对象
                        pi.CompanyName = p.CompanyName;
                        pi.CompanyLinkman = p.CompanyLinkman;
                        pi.CompanyHomePage = p.CompanyHomePage;
                        pi.CompanyTel = p.CompanyTel;
                        listp.Add(pi);
                    }
                }
                //更新
                BoFactory.GetVersionTrackBo.UpdateConfigInfos(list, listp);//发布

                InsertXmlByConfig(vid);//生成config

                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = vid;
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

        #region 插件列表

        public ActionResult PluginInfoManage()
        {
            return View();
        }

        public ActionResult GetPluginInfo(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryPluginInfos(view);
            return Json(data);
        }

        #endregion

        #region 第1步，选择插件类型
        public ActionResult SelectType(string IsCate)
        {
            ViewData["IsCate"] = IsCate;

            return View();
        }
        #endregion

        #region 第2步，上传文件

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">插件ID</param>
        /// <param name="IsAdd">0表示升级。1为新赠，2为修改配置</param>
        /// <returns></returns>
        public ActionResult PluginWizard(string Vid, string IsAdd,string IsCate,string IsUpdate)
        {
            ViewData["Vid"] = "";
            ViewData["IsUpdate"] = "";
            ViewData["type"] = "";
            if (!string.IsNullOrEmpty(Vid))
            {
                ViewData["Vid"] = Vid;

                SearchVersionTrack searchv = new SearchVersionTrack();
                searchv.VID = Vid.Split(',')[0];
                string pid = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0].PluginCode.ToString();
                PluginInfo p = BoFactory.GetVersionTrackBo.GetPluginInfo(pid);
                if (p.PluginCateCode.Equals(Constants.PluginCateCode))
                    ViewData["type"]="1";
                else
                    ViewData["type"] = "0";

            }
            if (!string.IsNullOrEmpty(IsUpdate))
                ViewData["IsUpdate"] = IsUpdate;
            ViewData["IsAdd"] = IsAdd;
            ViewData["IsCate"] = IsCate;
            return View();
        }


        public ActionResult GetInfoTree(FormCollection form)
        {
            var nodes = TreeRootDefine.CreateDictInfoTreeNode();
            return Json(nodes);
        }

        public ActionResult UploadVersionFile(string IsAdd, string Vid, string IsUpdate, string PluginCateCode)
        {
            ViewData["code"] = "";
            ViewData["ver"] = "";
            ViewData["weburl"] = "";
            ViewData["IsAdd"] = IsAdd;
            ViewData["PluginCateCode"] = PluginCateCode;
            ViewData["IsUpdate"] = "";
            if (!string.IsNullOrEmpty(Vid) && !Vid.Equals("undefined"))
            {
                ViewData["Vid"] = Vid;
                SearchVersionTrack searchv = new SearchVersionTrack();
                searchv.VID = Vid.Split(',')[0];
                string pid = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0].PluginCode.ToString();
                PluginInfo p = BoFactory.GetVersionTrackBo.GetPluginInfo(pid);
                PluginInfoTemp pt = BoFactory.GetVersionTrackBo.GetPluginTempInfo(pid);
                ViewData["code"] = p.PluginCode;

                if (pt != null)
                {
                    ViewData["weburl"] = pt.PluginUrl;
                    ViewData["ver"] = pt.Version;
                }
                else
                {
                    ViewData["weburl"] = p.PluginUrl;
                    ViewData["ver"] = p.Version;
                }
            }
            if (!string.IsNullOrEmpty(IsUpdate))
                ViewData["IsUpdate"] = IsUpdate;

          
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveVerInfo(VersionTrack ver, string IsAdd, string IsUpdate, FormCollection form, string PluginCateCode)
        {
            var vmobject = new JsonReturnMessages();
            string vid = "";
            XmlConfigInfo xmlInfo = null;

            try
            {
                if (Request.Files.Count > 0)//若有上传文件
                    xmlInfo = Decompressing(form["VersionIds"].ToString(), IsAdd);//解压
                else
                {
                    IList<PluginInfoTemp> listp = new List<PluginInfoTemp>();
                    PluginInfoTemp p = new PluginInfoTemp();
                    p.Version = form["VersionName"];
                    p.PluginUrl = form["PluginUrl"];
                    p.PluginCode = form["PluginCode"];
                    listp.Add(p);
                    xmlInfo = new XmlConfigInfo();
                    xmlInfo.PluginInfo = listp;
                }
                if (PluginCateCode.Equals("1"))//若为web插件
                {
                    xmlInfo.PluginInfo[0].PluginCateCode = Constants.PluginCateCode;
                    xmlInfo.oldCode = xmlInfo.PluginInfo[0].PluginCode;
                }
                foreach (PluginInfoTemp ps in xmlInfo.PluginInfo)
                {
                    ps.IsPublic = true;
                    ps.IsIgnoreConfig = Convert.ToBoolean(form["ck"]);
                }

                vid = BoFactory.GetVersionTrackBo.SavePluingZipInfo(ver, xmlInfo, IsAdd, IsUpdate, base.CurrentUser.UserUId, form["VersionIds"].ToString());

                if (Request.Files.Count > 0)//若有上传文件
                    MoveFolder(xmlInfo);//移动文件夹及文件
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
            //获取上一个版本数据
            SearchVersionTrack sea = new SearchVersionTrack();
            sea.VID = vid;
            VersionTrack vt = BoFactory.GetVersionTrackBo.GetVersionTrack(sea)[0];
            if (vt != null)
            {
                if (vt.PreVersionId != 0)
                    vid = vt.PreVersionId.ToString();
            }
           
            vmobject.Msg = vid;
            vmobject.IsSuccess = true;
            return Json(vmobject, "text/html");
        }

        #endregion

        #region 第3步，修改插件基本信息，配置信息和扩展信息

        public ActionResult UpdatePluginInfo(string Vid, string IsAdd)
        {
            ViewData["configList"] = 0;
            ViewData["IsAdd"] = IsAdd;
            SearchVersionTrack search = new SearchVersionTrack();
            IList<PluginInfoTemp> plist = new List<PluginInfoTemp>();

            ArrayList arrVid = GetArray(Vid);//获得相关联的VID

            foreach (string vid in arrVid)
            {
                search.VID = vid;
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                PluginInfoTemp p = BoFactory.GetVersionTrackBo.GetPluginTempInfo(v.PluginCode.ToString());

                if (p.IsIgnoreConfig == false)//若不忽略配置信息
                {
                    SearchConfig searchc = new SearchConfig();
                    searchc.PluginCode = v.PluginCode.ToString();
                    searchc.ConfigCategoryCode = Constants.configCategory;
                    IList<ConfigInfo> list = BoFactory.GetVersionTrackBo.GetConfigList(searchc);//获取原有参数

                    Dictionary<string, ConfigInfo> dic = new Dictionary<string, ConfigInfo>();
                    foreach (var item in list)
                    {
                        var key = item.Key1 + "_FG$SP_" + item.PluginCode;
                        ConfigInfo temp = new ConfigInfo();
                        temp.Key1 = item.Key1;
                        temp.OldValue = item.Value1;
                        dic.Add(key, temp);
                    }
                    IList<ConfigTemp> listT = BoFactory.GetVersionTrackBo.GetConfigListTemp(searchc);//现有临时表数据
                    foreach (var item in listT)
                    {
                        var key = item.Key1 + "_FG$SP_" + item.PluginCode;
                        if (dic.ContainsKey(key))
                            dic[key].Value1 = item.Value1;
                        else
                        {
                            ConfigInfo temp = new ConfigInfo();
                            temp.Key1 = item.Key1;
                            temp.Value1 = item.Value1;
                            temp.OldValue = "";
                            dic.Add(key, temp);
                        }
                    }
                    p.configList = dic.Values.ToList<ConfigInfo>();
                    ViewData["configList"] = (int)ViewData["configList"] + p.configList.Count;

                    SearchPlugin sh = new SearchPlugin();
                    sh.PluginCateCode = p.PluginCateCode;
                    p.PCname = BoFactory.GetVersionTrackBo.GetPluginCategoryInfos(sh)[0].DisplayName;
                }
                plist.Add(p);
            }
            return View(plist);

        }
        [ValidateInput(false)]
        public ActionResult SavePluginInfos(PluginInfoTemp pt, FormCollection form, ConfigTemp c,string IsAdd)
        {
            var vmobject = new JsonReturnMessages();
            IList<PluginInfoTemp> listtemp = new List<PluginInfoTemp>();
            IList<ConfigTemp> list = new List<ConfigTemp>();
            string vids = "";

            try
            {
                string[] keys = form.AllKeys;
                //获取插件列表
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].EndsWith(".PluginCode"))
                    {
                        PluginInfoTemp p = new PluginInfoTemp();
                        string prefix = keys[i].Split('.')[0];
                        UpdateModel(p, prefix);  //其他数据同时可以获得填充进实体对象
                        p.CompanyHomePage = pt.CompanyHomePage;
                        p.CompanyLinkman = pt.CompanyLinkman;
                        p.CompanyName = pt.CompanyName;
                        p.CompanyTel = pt.CompanyTel;
                        if (p.IsNeed == true)
                            p.IsDefault = true;
                        listtemp.Add(p);
                    }
                }
                //获取列表参数
                if (listtemp[0].IsIgnoreConfig == false)
                {
                    for (int i = 0; i < keys.Length; i++)
                    {
                        if (keys[i].EndsWith(".Key1"))
                        {
                            ConfigTemp config = new ConfigTemp();
                            string prefix = keys[i].Split('.')[0];
                            UpdateModel(config, prefix);  //其他数据同时可以获得填充进实体对象
                            config.PluginCode = prefix.Split('|')[0];
                            list.Add(config);
                        }
                    }
                }

                BoFactory.GetVersionTrackBo.UpdateTempPlugin(listtemp, list, IsAdd, base.CurrentUser.UserUId);
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
                return Json(vmobject);
            }
            //获取上一个版本数据
            SearchVersionTrack sea = new SearchVersionTrack();
            sea.PluginCode = listtemp[0].PluginCode;
            sea.VersionName = listtemp[0].Version;
            vids = BoFactory.GetVersionTrackBo.GetVersionTrack(sea)[0].VersionId.ToString();//默认为当前临时表第一个
            foreach (PluginInfoTemp pti in listtemp)
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.PluginCode = pti.PluginCode.ToString();
                search.VersionName = pti.Version;
                VersionTrack vst = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                if (vst.PreVersionId != 0)
                {
                    vids = vst.PreVersionId.ToString();//获得上一个版本，与修改状态时候，直接选择发布获得的vid一致为上个版本vid
                    break;
                }

            }
            vmobject.IsSuccess = true;
            vmobject.Msg = vids;
            return Json(vmobject);
        }

        #endregion

        #region 第4步，发布

        public ActionResult PublishInfo(string Vid)//vid一定是上一个版本的vid
        {
            ArrayList vids = GetArray(Vid);//获得相关联的VID

            IList<PluginInfoTemp> listtemp = new List<PluginInfoTemp>();
            foreach (string vid in vids)
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = vid;
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                PluginInfoTemp p = BoFactory.GetVersionTrackBo.GetPluginTempInfo(v.PluginCode.ToString());

                SearchConfig searchc = new SearchConfig();
                searchc.PluginCode = p.PluginCode.ToString();
                IList<ConfigTemp> list = BoFactory.GetVersionTrackBo.GetConfigListTemp(searchc);

                IList<ConfigInfo> listc = new List<ConfigInfo>();
                foreach (ConfigTemp c in list)
                {
                    c.Value1 = c.Value1.Replace("<", "&lt;").Replace(">", "&gt;");
                    listc.Add(CommonMethods.ConvertToConfigInfo(c));
                }
                p.configList = listc;

                listtemp.Add(p);
            }
            ViewData["vids"] = Vid;
            return View(listtemp);
        }

        public ActionResult SavePublishInfo(string Vid, FormCollection form)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                XmlConfigInfo x = InsertXml(Vid);//写入XML
                SearchVersionTrack search = new SearchVersionTrack();
                ArrayList vids = GetArray(Vid);//获得相关联的VID
                search.VID = vids[0].ToString();
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                string name = codes[codes.Length - 2];//获取插件code

                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (Directory.Exists(Path.Combine(AppConfig.pubFolder, name + AppConfig.subFix)))//如果存在次文件夹
                    pub.UpdateApplication(v.FilePath, name);
                else
                    pub.CreateApplication(v.FilePath, name);

                BoFactory.GetVersionTrackBo.UpdatePlushVersionTracks(x,Vid, base.CurrentUser.UserUId);//更新状态


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

        #region 查看插件详细信息

        public ActionResult ViewPluginInfo(string id)
        {
            ViewData["Vid"] = id;
            SearchVersionTrack search = new SearchVersionTrack();
            search.VID = id;
            VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
            
            PluginInfo p = BoFactory.GetVersionTrackBo.GetPluginInfo(v.PluginCode);
            SearchConfig searchc = new SearchConfig();
            searchc.PluginCode = v.PluginCode;
            IList<ConfigInfo> list = BoFactory.GetVersionTrackBo.GetConfigList(searchc);
            //转换
            foreach (ConfigInfo c in list)
            {
                c.Value1 = c.Value1.Replace("<", "&lt;").Replace(">", "&gt;");
            }
            p.configList = list;
            p.VersionSummary = v.VersionSummary;

            SearchPlugin sh = new SearchPlugin();
            sh.PluginCateCode = p.PluginCateCode;
            p.PCname = BoFactory.GetVersionTrackBo.GetPluginCategoryInfos(sh)[0].DisplayName;

            return View(p);
        }


        public ActionResult GetVersionTrackList(FormCollection form, string id)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            SearchVersionTrack search = new SearchVersionTrack();
            search.PluginCode = id;
            data = BoFactory.GetVersionTrackBo.QueryVersionTrackInfo(view, search);
            return Json(data);
        }




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

        #region 修改插件状态
        public ActionResult SetDisableStatus(string pid, string status, FormCollection form)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                BoFactory.GetVersionTrackBo.UpdateStatusByPlugin(pid, Convert.ToBoolean(Convert.ToInt32(status)));
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
        /// 解压、获取信息
        /// </summary>
        /// <returns></returns>
        private XmlConfigInfo Decompressing(string vid,string isAdd)
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

            XmlConfigInfo xmlInfo = new XmlConfigInfo();
            xmlInfo.savefile = saveFile;

            xmlInfo = GetXmlInfo(thisFilePath);//获取解析出来的配置信息
            //若升级，以原由CODE文件夹+第一个插件版本名命
            if (isAdd.Equals("0"))
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = vid;
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                xmlInfo.oldCode = codes[codes.Length - 2];
            }
            else
                xmlInfo.oldCode = xmlInfo.PluginInfo[0].PluginCode;//否则以第一个排序后的CODE为文件夹
            //zip文件有第一个CODE+版本号命名
            string oldfilepath = Path.Combine(zipPaht, xmlInfo.PluginInfo[0].PluginCode + "_FG$SP_" + xmlInfo.PluginInfo[0].Version + ".zip");//重命名的zip文件
            xmlInfo.savefile = saveFile;
            xmlInfo.oldfile = oldfilepath;

            return xmlInfo;
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// <param name="xmlInfo"></param>
        private void MoveFolder(XmlConfigInfo xmlInfo)
        {
            FileInfo f = new FileInfo(xmlInfo.savefile);
            //移动前。需要删除移动去的位置是否存在原由文件
            FileInfo oldfile = new FileInfo(xmlInfo.oldfile);
            if (oldfile.Exists)//判断是否需要删除原由文件
                oldfile.Delete();
            f.MoveTo(xmlInfo.oldfile);//规则：code+版本名

            string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
            //创建文件以versionname为名的文件夹在指定根目录下

            string FilePath = Path.Combine(AppConfig.filePath, xmlInfo.oldCode);//当前插件CODE文件夹
            while (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            //规则：以第一个插件的CODE为主文件夹，以第一个插件的CODE+版本命名的版本文件夹
            string thispath = Path.Combine(FilePath, xmlInfo.PluginInfo[0].PluginCode + "_FG$SP_" + xmlInfo.PluginInfo[0].Version);//当前移动的去的文件夹
            //移动前删除新CODE下是否存在该版本文件夹
            if (Directory.Exists(thispath))
                Directory.Delete(thispath, true);

            Directory.Move(thisFilePath, thispath);//移动
        }

        /// <summary>
        /// 解析XML获取信息
        /// </summary>
        /// <returns></returns>
        private XmlConfigInfo GetXmlInfo(string xmlpath)
        {
            XmlConfigInfo x = CommonMethods.GetEntityFromXML(Path.Combine(xmlpath, Common.Entities.Constants.PluginPath));
            x.PluginInfo = x.PluginInfo.OrderBy(T => T.PluginCode).ToList();
            return x;
        }

        /// <summary>
        /// 写入XML
        /// </summary>
        /// <param name="path"></param>
        private XmlConfigInfo InsertXml(string Vid)
        {
            XmlConfigInfo xml = new XmlConfigInfo();
            ArrayList vids = GetArray(Vid);//获得相关联的VID
            IList<PluginInfoTemp> list = new List<PluginInfoTemp>();
            SearchVersionTrack searchv = new SearchVersionTrack();
            VersionTrack v = null;
            SearchConfig serach = new SearchConfig();
            foreach (string vid in vids)
            {
                searchv.VID = vid;
                v = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0];
                PluginInfoTemp p = BoFactory.GetVersionTrackBo.GetPluginTempInfo(v.PluginCode.ToString());
                serach.PluginCode = v.PluginCode.ToString();
                IList<ConfigTemp> listct = BoFactory.GetVersionTrackBo.GetConfigListTemp(serach);//获取配置信息

                IList<ConfigInfo> listc = new List<ConfigInfo>();
                foreach (ConfigTemp c in listct)
                {
                    listc.Add(CommonMethods.ConvertToConfigInfo(c));//转换
                }
                p.configList = listc;

                list.Add(p);
            }
            xml.PluginInfo = list;
            xml.PluginInfo = xml.PluginInfo.OrderBy(T => T.PluginCode).ToList();

            CommonMethods.WritePluginfoConfigXml(xml, Path.Combine(v.FilePath.Trim(), Constants.pluginName));

            return xml;
        }

        private XmlConfigInfo InsertXmlByConfig(string Vid)
        {
            XmlConfigInfo xml = new XmlConfigInfo();
            ArrayList vids = GetArrays(Vid);//获得相关联的VID
            IList<PluginInfoTemp> list = new List<PluginInfoTemp>();
            SearchVersionTrack searchv = new SearchVersionTrack();
            VersionTrack v = null;
            SearchConfig serach = new SearchConfig();
            foreach (string vid in vids)
            {
                searchv.VID = vid;
                v = BoFactory.GetVersionTrackBo.GetVersionTrack(searchv)[0];
                PluginInfo p = BoFactory.GetVersionTrackBo.GetPluginInfo(v.PluginCode.ToString());
                serach.PluginCode = v.PluginCode.ToString();
                IList<ConfigInfo> listct = BoFactory.GetVersionTrackBo.GetConfigList(serach);//获取配置信息
                p.configList = listct;
                p.VersionSummary = v.VersionSummary;
                if (p.PluginCateCode.Equals(Constants.ActionCateCode))
                {
                    SearchConfig search = new SearchConfig();
                    search.PluginCode = p.PluginCode;
                    ActionExtend a = BoFactory.GetVersionTrackBo.QueryActionExtend(search)[0];
                    p.ActionCode = a.ActionCode;
                    p.ActionSummary = a.Summary;
                }

                list.Add(TPluginInfo(p));
            }
            xml.PluginInfo = list;
            xml.PluginInfo = xml.PluginInfo.OrderBy(T => T.PluginCode).ToList();

            CommonMethods.WritePluginfoConfigXml(xml, Path.Combine(v.FilePath.Trim(), Constants.pluginName));

            return xml;
        }

        private PluginInfoTemp TPluginInfo(PluginInfo plugin)
        {
            PluginInfoTemp p = new PluginInfoTemp();
            p.CompanyHomePage = plugin.CompanyHomePage;
            p.CompanyLinkman = plugin.CompanyLinkman;
            p.CompanyName = plugin.CompanyName;
            p.CompanyTel = plugin.CompanyTel;
            p.DirectoryName = plugin.DirectoryName;
            p.DisplayName = plugin.DisplayName;
            p.FileName = plugin.FileName;
            p.HashCode = plugin.HashCode;
            p.IsDefault = plugin.IsDefault;
            p.IsNeed = plugin.IsNeed;
            p.PluginCateCode = plugin.PluginCateCode;
            p.PluginUrl = plugin.PluginUrl;
            p.Sequence = plugin.Sequence;
            p.PluginSummary = plugin.Summary;
            p.TypeFullName = plugin.TypeFullName;
            p.Version = plugin.Version;
            p.PluginCode = plugin.PluginCode;
            p.IsNew = plugin.IsNew;
            p.IsUse = plugin.IsUse;
            p.IsIgnoreConfig = plugin.IsIgnoreConfig;
            p.configList = plugin.configList;
            p.ActionCode = plugin.ActionCode;
            p.ActionSummary = plugin.ActionSummary;
            p.VersionSummary = plugin.VersionSummary;
            return p;
        }

        //获取当前关联的vid
        private ArrayList GetArrays(string Vid)
        {
            SearchVersionTrack search = new SearchVersionTrack();
            //获得第一个vid（肯能多个VID，也可能是1个，但至少有一个，根据相同的filepath获得相关的VID）
            search.VID = Vid;
            VersionTrack vs = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
            SearchVersionTrack se = new SearchVersionTrack();
            se.filepath = vs.FilePath;
            IList<VersionTrack> vlist = BoFactory.GetVersionTrackBo.GetVersionTrack(se);
            ArrayList arrVid = new ArrayList();
            foreach (VersionTrack v1 in vlist)
            {
                arrVid.Add(v1.VersionId.ToString());
            }

            return arrVid;
        }

        //获取最新版本关联VID，因为列表中修改时，数据现实的是上一版本
        private ArrayList GetArray(string Vid)
        {
            SearchVersionTrack search = new SearchVersionTrack();
            search.VID = Vid;
            VersionTrack vs = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
            string filepath = vs.FilePath;
            SearchVersionTrack setemp = new SearchVersionTrack();
            setemp.filepath = filepath;
            IList<VersionTrack> vlisttemp = BoFactory.GetVersionTrackBo.GetVersionTrack(setemp);//获得上一所有插件版本
            //升级时，必须满足上传插件CODE中至少有一个CODE匹配
            foreach (VersionTrack vtemp in vlisttemp)
            {
                SearchVersionTrack searcht = new SearchVersionTrack();
                searcht.PreVersionId = vtemp.VersionId.ToString();//查找下一个最新版本是否存在
                IList<VersionTrack> templist = BoFactory.GetVersionTrackBo.GetVersionTrack(searcht);
                if (templist.Count > 0)
                {
                    filepath = templist[0].FilePath;
                    break;
                }
            }
            //若未找到，就沿用当前这个版本
            SearchVersionTrack se = new SearchVersionTrack();
            se.filepath = filepath;
            IList<VersionTrack> vlist = BoFactory.GetVersionTrackBo.GetVersionTrack(se);
            ArrayList arrVid = new ArrayList();
            foreach (VersionTrack v1 in vlist)
            {
                arrVid.Add(v1.VersionId.ToString());
            }

            return arrVid;
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
