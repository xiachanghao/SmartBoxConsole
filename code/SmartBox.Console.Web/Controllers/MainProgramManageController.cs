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
       
        #region ���������

        #region ��һ�����汾��Ϣ�б�
        //��һ������ð汾�б�
        public ActionResult ProgramList()
        {
            SearchVersionTrack search = new SearchVersionTrack();
            search.PluginCode = Common.Entities.Constants.MianPluginId.ToString();
            search.VersionStatus = "0";
            IList<VersionTrack> list = BoFactory.GetVersionTrackBo.GetVersionTrack(search);//��ȡδ�����İ汾��Ϣ
            ViewData["versionId"] = "";
            if (list.Count > 0)
                ViewData["versionId"] = list[0].VersionId;//Ϊ�ṩҳ���ж���Ҫ�����޸Ļ��������ϴ�

            return View();
        }
        //��һ������ð汾�б�
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

        #region �ڶ������ϴ��ļ��б�

        public ActionResult UploadVersionFile(string id)//�汾ID
        {
            VersionTrack VerInfo = null;
            if (string.IsNullOrEmpty(id))
            {
                VerInfo = new VersionTrack();
                ViewData["versionId"] = "";//��ʼ��
            }
            else
                ViewData["versionId"] = id;//�汾ID
            return View(VerInfo);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveVerInfo(VersionTrack ver, FormCollection form)
        {
            var vmobject = new JsonReturnMessages();
            string vid = "";
            if (Request.Files.Count > 0)//�����ļ��ϴ�
            {
                XmlConfigInfo xmlInfo = Decompressing(); //1.��ѹ����ȡ��Ϣ
                try
                {
                    //vid = BoFactory.GetVersionTrackBo.SaveZipInfo(ver, xmlInfo, base.CurrentUser.UserUId);
                    MoveFolder(xmlInfo);//���ƶ��ļ���
                }
                catch (Exception ex)
                {
                    string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
                    while (Directory.Exists(thisFilePath))//��ɾ��ԭ����ʱ�ļ���
                    {
                        Directory.Delete(thisFilePath, true);
                    }
                    FileInfo f = new FileInfo(xmlInfo.savefile);//ɾ��ԭ�������zip
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

        #region ���������������Ӧ��������Ϣ����������������ɾ��

        public ActionResult UpdateConfigInfo(string verid, string IsLoadFile)
        {
            ViewData["pId"] = -1;//���PluginCode
            ViewData["verid"] = verid;//�汾ID
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

            IList<ConfigCategory> list = BoFactory.GetVersionTrackBo.GetMainCategoryList();//��ȡ���������÷�����Ϣ
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
                    vmobject.Msg = "�����ɹ�!";
                }
                else
                {
                    vmobject.IsSuccess = false;
                    vmobject.Msg = "��û��ѡ��";
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

        #region  ���Ĳ�������

        public ActionResult PublishInfo(string id, string IsLoadFile)
        {
            ViewData["IsLoadFile"] = "";
            if (!string.IsNullOrEmpty(IsLoadFile))
                ViewData["IsLoadFile"] = IsLoadFile;//�Ƿ񵥸��ϴ��ļ�
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
                //1��д��XML
                InsertXml(ver.FilePath);
                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (!string.IsNullOrEmpty(IsLoadFile))//��Ϊ�����ϴ��ļ�����
                {
                    //��ȥver.FilePath���������ļ�
                    pub.UpdateFile(ver.FilePath + @"\" + Common.Entities.Constants.globalName, Constants.MianName, "./");
                    pub.UpdateFile(ver.FilePath + @"\" + Common.Entities.Constants.systemName, Constants.MianName, "./");
                }
                else
                {
                    //2������
                    SearchVersionTrack search = new SearchVersionTrack();
                    search.VID = ver.PreVersionId.ToString();//��ȡ��һ���汾��
                    IList<VersionTrack> list = BoFactory.GetVersionTrackBo.GetVersionTrack(search);
                    if (list.Count > 0)//�������ڴ˼�¼˵���ǵ�һ�η���
                        pub.UpdateApplication(ver.FilePath, Common.Entities.Constants.MianName);
                    else
                        pub.CreateApplication(ver.FilePath, Common.Entities.Constants.MianName);

                    //3������
                    ver.LastModTime = DateTime.Now;
                    ver.LastModUid = base.CurrentUser.UserUId;
                    ver.VersionStatus = 1;
                    BoFactory.GetVersionTrackBo.UpdatePlushVersionTrack(ver);
                }
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

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ѹ����ȡ��Ϣ
        /// </summary>
        /// <returns></returns>
        private XmlConfigInfo Decompressing()
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
            string saveFile = zipPaht + @"\" + System.Guid.NewGuid().ToString() + ".zip";
            Request.Files[0].SaveAs(saveFile);//����zipȥ�ļ�����

            CommonMethods.Uncompress(saveFile, thisFilePath);//��ѹ
            XmlConfigInfo xmlInfo = GetXmlInfo(thisFilePath);//��ȡ����������������Ϣ

            //string oldfilepath = zipPaht + @"\" + Constants.MianName + xmlInfo.PluginInfo.Version + ".zip";//���ϴ���zip�ļ�
            //xmlInfo.savefile = saveFile;
            //xmlInfo.oldfile = oldfilepath;

          
            return xmlInfo;
        }

        /// <summary>
        /// �ƶ��ļ���
        /// </summary>
        /// <param name="xmlInfo"></param>
        private void MoveFolder(XmlConfigInfo xmlInfo)
        {

            FileInfo f = new FileInfo(xmlInfo.savefile);
            FileInfo oldfile = new FileInfo(xmlInfo.oldfile);
            if (oldfile.Exists)//�ж��Ƿ���Ҫɾ��ԭ���ļ�
                oldfile.Delete();
            f.MoveTo(xmlInfo.oldfile);//����code+�汾��

            string thisFilePath = AppConfig.filePath + Common.Entities.Constants.TempName;
            //�����ļ���versionnameΪ�����ļ�����ָ����Ŀ¼��
            string FilePath = AppConfig.filePath + Common.Entities.Constants.MianName;//��ǰ�������ļ���
            while (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            //string thispath = FilePath + @"\" + xmlInfo.PluginInfo.Version;//��ǰ�ƶ�ȥ���ļ���
            //if (Directory.Exists(thispath))
            //    Directory.Delete(thispath, true);

            //Directory.Move(thisFilePath, thispath);//�ƶ�
        }

        /// <summary>
        /// ����XML��ȡ��Ϣ
        /// </summary>
        /// <returns></returns>
        private XmlConfigInfo GetXmlInfo(string xmlpath)
        {
            XmlConfigInfo x = CommonMethods.GetEntityFromXML(xmlpath + @"\" + Common.Entities.Constants.MianPath);
            return x;
        }

        /// <summary>
        /// д��XML
        /// </summary>
        /// <param name="path"></param>
        private void InsertXml(string path)
        {/*
          * ��ʱ������-- by ���� @ 20100413
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
