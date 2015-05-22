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
                    vmobject.Msg = "����ɾ��δ�����İ汾,������ɷ���,����ɾ�����������";
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

        #region �޸����ã�������

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
                        UpdateModel(config, prefix);  //��������ͬʱ���Ի������ʵ�����
                        config.PluginCode = prefix.Split('|')[0];
                        config.ConfigCategoryCode = Constants.configCategory;
                        list.Add(config);
                    }
                }
                //��ò��CODE
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].EndsWith(".PluginCode"))
                    {
                        PluginInfo pi = new PluginInfo();
                        string prefix = keys[i].Split('.')[0];
                        UpdateModel(pi, prefix);  //��������ͬʱ���Ի������ʵ�����
                        pi.CompanyName = p.CompanyName;
                        pi.CompanyLinkman = p.CompanyLinkman;
                        pi.CompanyHomePage = p.CompanyHomePage;
                        pi.CompanyTel = p.CompanyTel;
                        listp.Add(pi);
                    }
                }
                //����
                BoFactory.GetVersionTrackBo.UpdateConfigInfos(list, listp);//����

                InsertXmlByConfig(vid);//����config

                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = vid;
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                string name = codes[codes.Length - 2];//��ȡ���code

                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (Directory.Exists(Path.Combine(AppConfig.pubFolder, name + AppConfig.subFix)))//������ڴ��ļ���
                    pub.UpdateApplication(v.FilePath, name);
                else
                    pub.CreateApplication(v.FilePath, name);

                vmobject.IsSuccess = true;
                vmobject.Msg = "�����ɹ�";
                
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }
            return Json(vmobject);
        }

        #endregion

        #region ����б�

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

        #region ��1����ѡ��������
        public ActionResult SelectType(string IsCate)
        {
            ViewData["IsCate"] = IsCate;

            return View();
        }
        #endregion

        #region ��2�����ϴ��ļ�

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">���ID</param>
        /// <param name="IsAdd">0��ʾ������1Ϊ������2Ϊ�޸�����</param>
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
                if (Request.Files.Count > 0)//�����ϴ��ļ�
                    xmlInfo = Decompressing(form["VersionIds"].ToString(), IsAdd);//��ѹ
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
                if (PluginCateCode.Equals("1"))//��Ϊweb���
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

                if (Request.Files.Count > 0)//�����ϴ��ļ�
                    MoveFolder(xmlInfo);//�ƶ��ļ��м��ļ�
            }
            catch (Exception ex)
            {
                try
                {
                    string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
                    while (Directory.Exists(thisFilePath))//��ɾ��ԭ����ʱ�ļ���
                    {
                        Directory.Delete(thisFilePath, true);
                    }
                    if (!string.IsNullOrEmpty(xmlInfo.savefile))
                    {
                        FileInfo f = new FileInfo(xmlInfo.savefile);//ɾ��ԭ�������zip
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
            //��ȡ��һ���汾����
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

        #region ��3�����޸Ĳ��������Ϣ��������Ϣ����չ��Ϣ

        public ActionResult UpdatePluginInfo(string Vid, string IsAdd)
        {
            ViewData["configList"] = 0;
            ViewData["IsAdd"] = IsAdd;
            SearchVersionTrack search = new SearchVersionTrack();
            IList<PluginInfoTemp> plist = new List<PluginInfoTemp>();

            ArrayList arrVid = GetArray(Vid);//����������VID

            foreach (string vid in arrVid)
            {
                search.VID = vid;
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                PluginInfoTemp p = BoFactory.GetVersionTrackBo.GetPluginTempInfo(v.PluginCode.ToString());

                if (p.IsIgnoreConfig == false)//��������������Ϣ
                {
                    SearchConfig searchc = new SearchConfig();
                    searchc.PluginCode = v.PluginCode.ToString();
                    searchc.ConfigCategoryCode = Constants.configCategory;
                    IList<ConfigInfo> list = BoFactory.GetVersionTrackBo.GetConfigList(searchc);//��ȡԭ�в���

                    Dictionary<string, ConfigInfo> dic = new Dictionary<string, ConfigInfo>();
                    foreach (var item in list)
                    {
                        var key = item.Key1 + "_FG$SP_" + item.PluginCode;
                        ConfigInfo temp = new ConfigInfo();
                        temp.Key1 = item.Key1;
                        temp.OldValue = item.Value1;
                        dic.Add(key, temp);
                    }
                    IList<ConfigTemp> listT = BoFactory.GetVersionTrackBo.GetConfigListTemp(searchc);//������ʱ������
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
                //��ȡ����б�
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].EndsWith(".PluginCode"))
                    {
                        PluginInfoTemp p = new PluginInfoTemp();
                        string prefix = keys[i].Split('.')[0];
                        UpdateModel(p, prefix);  //��������ͬʱ���Ի������ʵ�����
                        p.CompanyHomePage = pt.CompanyHomePage;
                        p.CompanyLinkman = pt.CompanyLinkman;
                        p.CompanyName = pt.CompanyName;
                        p.CompanyTel = pt.CompanyTel;
                        if (p.IsNeed == true)
                            p.IsDefault = true;
                        listtemp.Add(p);
                    }
                }
                //��ȡ�б����
                if (listtemp[0].IsIgnoreConfig == false)
                {
                    for (int i = 0; i < keys.Length; i++)
                    {
                        if (keys[i].EndsWith(".Key1"))
                        {
                            ConfigTemp config = new ConfigTemp();
                            string prefix = keys[i].Split('.')[0];
                            UpdateModel(config, prefix);  //��������ͬʱ���Ի������ʵ�����
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
            //��ȡ��һ���汾����
            SearchVersionTrack sea = new SearchVersionTrack();
            sea.PluginCode = listtemp[0].PluginCode;
            sea.VersionName = listtemp[0].Version;
            vids = BoFactory.GetVersionTrackBo.GetVersionTrack(sea)[0].VersionId.ToString();//Ĭ��Ϊ��ǰ��ʱ���һ��
            foreach (PluginInfoTemp pti in listtemp)
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.PluginCode = pti.PluginCode.ToString();
                search.VersionName = pti.Version;
                VersionTrack vst = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                if (vst.PreVersionId != 0)
                {
                    vids = vst.PreVersionId.ToString();//�����һ���汾�����޸�״̬ʱ��ֱ��ѡ�񷢲���õ�vidһ��Ϊ�ϸ��汾vid
                    break;
                }

            }
            vmobject.IsSuccess = true;
            vmobject.Msg = vids;
            return Json(vmobject);
        }

        #endregion

        #region ��4��������

        public ActionResult PublishInfo(string Vid)//vidһ������һ���汾��vid
        {
            ArrayList vids = GetArray(Vid);//����������VID

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
                XmlConfigInfo x = InsertXml(Vid);//д��XML
                SearchVersionTrack search = new SearchVersionTrack();
                ArrayList vids = GetArray(Vid);//����������VID
                search.VID = vids[0].ToString();
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                string name = codes[codes.Length - 2];//��ȡ���code

                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (Directory.Exists(Path.Combine(AppConfig.pubFolder, name + AppConfig.subFix)))//������ڴ��ļ���
                    pub.UpdateApplication(v.FilePath, name);
                else
                    pub.CreateApplication(v.FilePath, name);

                BoFactory.GetVersionTrackBo.UpdatePlushVersionTracks(x,Vid, base.CurrentUser.UserUId);//����״̬


                vmobject.IsSuccess = true;
                vmobject.Msg = "�����ɹ�";
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }

            return Json(vmobject);
        }

        #endregion

        #region �鿴�����ϸ��Ϣ

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
            //ת��
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

        #region �޸Ĳ��״̬
        public ActionResult SetDisableStatus(string pid, string status, FormCollection form)
        {
            var vmobject = new JsonReturnMessages();
            try
            {
                BoFactory.GetVersionTrackBo.UpdateStatusByPlugin(pid, Convert.ToBoolean(Convert.ToInt32(status)));
                vmobject.IsSuccess = true;
                vmobject.Msg = "�����ɹ�!";
            }
            catch (Exception ex)
            {
                vmobject.IsSuccess = false;
                vmobject.Msg = ex.Message;
            }
            return Json(vmobject);
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ѹ����ȡ��Ϣ
        /// </summary>
        /// <returns></returns>
        private XmlConfigInfo Decompressing(string vid,string isAdd)
        {
            string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
            while (!Directory.Exists(thisFilePath))//������ʱ�ļ���
            {
                Directory.CreateDirectory(thisFilePath);
            }
            string zipPaht = AppConfig.SaveZipPath;
            while (!Directory.Exists(zipPaht))//�������zip�ļ����ļ���
            {
                Directory.CreateDirectory(zipPaht);
            }

            string saveFile = Path.Combine(zipPaht, System.Guid.NewGuid().ToString() + ".zip");
            Request.Files[0].SaveAs(saveFile);//����zipȥ�ļ�����

            CommonMethods.Uncompress(saveFile, thisFilePath);//��ѹ

            XmlConfigInfo xmlInfo = new XmlConfigInfo();
            xmlInfo.savefile = saveFile;

            xmlInfo = GetXmlInfo(thisFilePath);//��ȡ����������������Ϣ
            //����������ԭ��CODE�ļ���+��һ������汾����
            if (isAdd.Equals("0"))
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = vid;
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                xmlInfo.oldCode = codes[codes.Length - 2];
            }
            else
                xmlInfo.oldCode = xmlInfo.PluginInfo[0].PluginCode;//�����Ե�һ��������CODEΪ�ļ���
            //zip�ļ��е�һ��CODE+�汾������
            string oldfilepath = Path.Combine(zipPaht, xmlInfo.PluginInfo[0].PluginCode + "_FG$SP_" + xmlInfo.PluginInfo[0].Version + ".zip");//��������zip�ļ�
            xmlInfo.savefile = saveFile;
            xmlInfo.oldfile = oldfilepath;

            return xmlInfo;
        }

        /// <summary>
        /// �ƶ��ļ���
        /// </summary>
        /// <param name="xmlInfo"></param>
        private void MoveFolder(XmlConfigInfo xmlInfo)
        {
            FileInfo f = new FileInfo(xmlInfo.savefile);
            //�ƶ�ǰ����Ҫɾ���ƶ�ȥ��λ���Ƿ����ԭ���ļ�
            FileInfo oldfile = new FileInfo(xmlInfo.oldfile);
            if (oldfile.Exists)//�ж��Ƿ���Ҫɾ��ԭ���ļ�
                oldfile.Delete();
            f.MoveTo(xmlInfo.oldfile);//����code+�汾��

            string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
            //�����ļ���versionnameΪ�����ļ�����ָ����Ŀ¼��

            string FilePath = Path.Combine(AppConfig.filePath, xmlInfo.oldCode);//��ǰ���CODE�ļ���
            while (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            //�����Ե�һ�������CODEΪ���ļ��У��Ե�һ�������CODE+�汾�����İ汾�ļ���
            string thispath = Path.Combine(FilePath, xmlInfo.PluginInfo[0].PluginCode + "_FG$SP_" + xmlInfo.PluginInfo[0].Version);//��ǰ�ƶ���ȥ���ļ���
            //�ƶ�ǰɾ����CODE���Ƿ���ڸð汾�ļ���
            if (Directory.Exists(thispath))
                Directory.Delete(thispath, true);

            Directory.Move(thisFilePath, thispath);//�ƶ�
        }

        /// <summary>
        /// ����XML��ȡ��Ϣ
        /// </summary>
        /// <returns></returns>
        private XmlConfigInfo GetXmlInfo(string xmlpath)
        {
            XmlConfigInfo x = CommonMethods.GetEntityFromXML(Path.Combine(xmlpath, Common.Entities.Constants.PluginPath));
            x.PluginInfo = x.PluginInfo.OrderBy(T => T.PluginCode).ToList();
            return x;
        }

        /// <summary>
        /// д��XML
        /// </summary>
        /// <param name="path"></param>
        private XmlConfigInfo InsertXml(string Vid)
        {
            XmlConfigInfo xml = new XmlConfigInfo();
            ArrayList vids = GetArray(Vid);//����������VID
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
                IList<ConfigTemp> listct = BoFactory.GetVersionTrackBo.GetConfigListTemp(serach);//��ȡ������Ϣ

                IList<ConfigInfo> listc = new List<ConfigInfo>();
                foreach (ConfigTemp c in listct)
                {
                    listc.Add(CommonMethods.ConvertToConfigInfo(c));//ת��
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
            ArrayList vids = GetArrays(Vid);//����������VID
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
                IList<ConfigInfo> listct = BoFactory.GetVersionTrackBo.GetConfigList(serach);//��ȡ������Ϣ
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

        //��ȡ��ǰ������vid
        private ArrayList GetArrays(string Vid)
        {
            SearchVersionTrack search = new SearchVersionTrack();
            //��õ�һ��vid�����ܶ��VID��Ҳ������1������������һ����������ͬ��filepath�����ص�VID��
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

        //��ȡ���°汾����VID����Ϊ�б����޸�ʱ��������ʵ������һ�汾
        private ArrayList GetArray(string Vid)
        {
            SearchVersionTrack search = new SearchVersionTrack();
            search.VID = Vid;
            VersionTrack vs = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
            string filepath = vs.FilePath;
            SearchVersionTrack setemp = new SearchVersionTrack();
            setemp.filepath = filepath;
            IList<VersionTrack> vlisttemp = BoFactory.GetVersionTrackBo.GetVersionTrack(setemp);//�����һ���в���汾
            //����ʱ�����������ϴ����CODE��������һ��CODEƥ��
            foreach (VersionTrack vtemp in vlisttemp)
            {
                SearchVersionTrack searcht = new SearchVersionTrack();
                searcht.PreVersionId = vtemp.VersionId.ToString();//������һ�����°汾�Ƿ����
                IList<VersionTrack> templist = BoFactory.GetVersionTrackBo.GetVersionTrack(searcht);
                if (templist.Count > 0)
                {
                    filepath = templist[0].FilePath;
                    break;
                }
            }
            //��δ�ҵ��������õ�ǰ����汾
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
            // ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ��� 
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ReportTitle);
            // ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ��� 
            Response.AddHeader("Content-Length", Buffer.Length.ToString());
            // ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻���� 
            Response.ContentType = "application/octet-stream";
            // ���ļ������͵��ͻ��� 
            Response.BinaryWrite(Buffer);
            Response.Flush();
            ms.Close();
            HttpContext.ApplicationInstance.CompleteRequest(); 
        }

     
        #endregion


    }
}
