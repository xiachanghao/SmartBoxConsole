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
                    vmobject.Msg = "����ɾ��δ�����İ汾,������ɷ���,����ɾ��������������";
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
                    vmobject.Msg = "��ǰΪ�ѷ����İ汾���߻�δ������,��ѡ����ڰ汾!";
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

        #region �޸�����

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
            listconfigs = InitConfigs(listconfigs);//��ʼ��
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
                        UpdateModel(config, prefix);  //��������ͬʱ���Ի������ʵ�����
                        config.PluginCode = Constants.UpdaterCode;
                        list.Add(config);
                    }
                }
                //����
                BoFactory.GetVersionTrackBo.UpdateUpdaterConfigInfos(list);//����

                InsertConfigXml(Vid);//����config

                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = Vid;
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

        #region �б�

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

        #region ����

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

        #region ������

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

        #region 1���汾������Ϣ

        public ActionResult UpdaterInfo(string IsUpdate, string IsAdd, string Vid, string AddVid)
        {
            ViewData["IsUpdate"] = "";
            ViewData["IsAdd"] = IsAdd;
            ViewData["Vid"] = Vid;
            VersionTrack v = new VersionTrack();
            if (!string.IsNullOrEmpty(IsUpdate))//��Ϊ�޸�
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

        #region 2���ϴ��ļ�

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
                if (Request.Files.Count > 0)//�����ϴ��ļ�
                    xmlInfo = Decompressing(v.VersionName);//��ѹ

                vid = BoFactory.GetVersionTrackBo.SaveUpdaterZipInfo(ver, xmlInfo, IsAdd, IsUpdate, base.CurrentUser.UserUId, form["VersionIds"].ToString());

                if (Request.Files.Count > 0)//�����ϴ��ļ�
                    MoveFolder(xmlInfo, v.VersionName);//�ƶ��ļ��м��ļ�
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

            vmobject.Msg = vid;
            vmobject.IsSuccess = true;
            return Json(vmobject, "text/html");
        }


        #endregion

        #region 3���޸�������Ϣ

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
            IList<ConfigInfo> list = BoFactory.GetVersionTrackBo.GetConfigList(searchc);//��ȡԭ�в���

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
            IList<ConfigTemp> listT = BoFactory.GetVersionTrackBo.GetConfigListTemp(searchc);//������ʱ������
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
            listconfigs = InitConfigs(listconfigs);//��ʼ��
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
                //��ȡ�б����
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].EndsWith(".Key1"))
                    {
                        ConfigTemp config = new ConfigTemp();
                        string prefix = keys[i].Split('.')[0];
                        UpdateModel(config, prefix);  //��������ͬʱ���Ի������ʵ�����
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


        #region 4������

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

            listc = InitConfigs(listc);//��ʼ��
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
                XmlMainConfigInfo x = InsertXml(Vid);//д��XML
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = Vid;
                VersionTrack v = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];
                string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                string name = codes[codes.Length - 2];//��ȡ���code

                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (Directory.Exists(Path.Combine(AppConfig.pubFolder ,name + AppConfig.subFix)))//������ڴ��ļ���
                    pub.UpdateApplication(v.FilePath, name);
                else
                    pub.CreateApplication(v.FilePath, name);

                BoFactory.GetVersionTrackBo.UpdateUpdaterPlushVersionTracks(x, Vid, base.CurrentUser.UserUId);//����״̬


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


        #region private function


        /// <summary>
        /// ��ѹ����ȡ��Ϣ
        /// </summary>
        /// <returns></returns>
        private XmlMainConfigInfo Decompressing(string vername)
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

            XmlMainConfigInfo xmlInfo = new XmlMainConfigInfo();
            xmlInfo.savefile = saveFile;

            xmlInfo = GetXmlInfo(thisFilePath);//��ȡ����������������Ϣ

            xmlInfo.oldCode = Constants.UpdaterCode;//�����Ե�һ��CODEΪ�ļ���
            //zip�ļ��е�һ��CODE+�汾������
            string oldfilepath = Path.Combine(zipPaht, Constants.UpdaterCode + "_FG$SP_" + vername + ".zip");//��������zip�ļ�
            xmlInfo.savefile = saveFile;
            xmlInfo.oldfile = oldfilepath;

            return xmlInfo;
        }

        /// <summary>
        /// �ƶ��ļ���
        /// </summary>
        /// <param name="xmlInfo"></param>
        private void MoveFolder(XmlMainConfigInfo xmlInfo, string vername)
        {
            FileInfo f = new FileInfo(xmlInfo.savefile);
            //�ƶ�ǰ����Ҫɾ���ƶ�ȥ��λ���Ƿ����ԭ���ļ�
            FileInfo oldfile = new FileInfo(xmlInfo.oldfile);
            if (oldfile.Exists)//�ж��Ƿ���Ҫɾ��ԭ���ļ�
                oldfile.Delete();
            f.MoveTo(xmlInfo.oldfile);//����code+�汾��-------

            string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
            //�����ļ���versionnameΪ�����ļ�����ָ����Ŀ¼��
            string FilePath = AppConfig.filePath + xmlInfo.oldCode;//��ǰ���CODE�ļ���
            while (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            //�����Ե�һ�������CODEΪ���ļ��У��Ե�һ�������CODE+�汾�����İ汾�ļ���
            string thispath = Path.Combine(FilePath, Constants.UpdaterCode + "_FG$SP_" + vername);//��ǰ�ƶ���ȥ���ļ���
            //�ƶ�ǰɾ����CODE���Ƿ���ڸð汾�ļ���
            if (Directory.Exists(thispath))
                Directory.Delete(thispath, true);

            Directory.Move(thisFilePath, thispath);//�ƶ�--------
        }

        /// <summary>
        /// ����XML��ȡ��Ϣ
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
        /// д��XML
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
            IList<ConfigTemp> listct = BoFactory.GetVersionTrackBo.GetConfigListTemp(serach);//��ȡ������Ϣ

            IList<ConfigInfoPC> listc = new List<ConfigInfoPC>();
            foreach (ConfigTemp c in listct)
            {
                listc.Add(CommonMethods.ConvertToConfigInfoPC(c));//ת��
            }
            xml.configList = listc;

            CommonMethods.WriteMaininfoConfigXml(listc, Path.Combine(v.FilePath.Trim() , Constants.UpdaterName));

            return xml;
        }


        /// <summary>
        /// д��XML
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
            IList<ConfigInfoPC> listc = BoFactory.GetVersionTrackBo.GetConfigPCList(serach);//��ȡ������Ϣ
           
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
