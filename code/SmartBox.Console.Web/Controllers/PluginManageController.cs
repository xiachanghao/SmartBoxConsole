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
        #region ����б�

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

        #region ����û�����

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

        #endregion


        #region ����汾����

        #region ��һ�����汾��Ϣ�б�

        public ActionResult PluginVersionList()
        {
            return View();
        }
        //��һ������ð汾�б�
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetVersionTrackList(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryPluginNotWeb(view);
            return Json(data);
        }

        #endregion

        #region �ڶ������ϴ��ļ��б�

        public ActionResult UploadVersionFile(string id,string IsAdd)//�汾ID
        {
            VersionTrack VerInfo = null;
            ViewData["IsAdd"] = "";
            ViewData["versionId"] = "";//��ʼ��
            if (string.IsNullOrEmpty(id))
                VerInfo = new VersionTrack();
            else
                ViewData["versionId"] = id;//�汾ID

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

        #region ���������޸Ĳ��������Ϣ

        public ActionResult UpdatePluginInfo(string verid)
        {
            ViewData["actionCode"] = "";
            ViewData["summary"] = "";
            ViewData["IsAction"] = "";
            SearchVersionTrack search = new SearchVersionTrack();
            search.VID = verid;
            VersionTrack version = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];//��ȡ���ID

            PluginInfo pinfo = BoFactory.GetVersionTrackBo.GetPluginInfo(version.PluginCode.ToString());//��ȡ�����Ϣ
            if (pinfo.PluginCateCode.Equals(Constants.ActionCateCode))
                ViewData["IsAction"] = "1";

            IList<PluginCategory> list = BoFactory.GetVersionTrackBo.GetPluginCategoryInfo();//��ȡ��web����ķ�����Ϣ
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
            IList<ActionExtend> listAction = BoFactory.GetVersionTrackBo.QueryActionExtend(searchconfig);//��ȡ��չ��Ϣ
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
                actionExtend.Summary = form["ESummary"].ToString();//��չ��Ϣ����

                BoFactory.GetVersionTrackBo.UpdatePluginInfo(pluginInfo, actionExtend);

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

        #region ���Ĳ������������Ϣ

        public ActionResult UpdatePluginConfig(string id)
        {
            SearchVersionTrack search = new SearchVersionTrack();
            search.VID = id;
            ViewData["pId"] = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0].PluginCode.ToString();
            ViewData["verid"] = id;//�汾ID
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
            datas[0].Name = "������˲������";
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

        #region  ���岽������

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
                InsertXml(ver);//1��д��XML

                ver.LastModTime = DateTime.Now;
                ver.LastModUid = base.CurrentUser.UserUId;
                ver.VersionStatus = 1;

                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = ver.PreVersionId.ToString();
                IList<VersionTrack> list = BoFactory.GetVersionTrackBo.GetVersionTrack(search);//��ȡ��һ���汾��

                string name = BoFactory.GetVersionTrackBo.GetPluginInfo(ver.PluginCode.ToString()).PluginCode;//��ȡ���code
               
                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (list.Count > 0)//�������ڴ˼�¼˵���ǵ�һ�η���
                    pub.UpdateApplication(ver.FilePath, name);
                else
                    pub.CreateApplication(ver.FilePath, name);

                BoFactory.GetVersionTrackBo.UpdatePlushVersionTrack(ver);//����״̬

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
        /// ��ѹ����ȡ��Ϣ���ƶ��ļ���
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

            //string oldfilepath = zipPaht + @"\" + xmlInfo.PluginInfo.PluginCode + xmlInfo.PluginInfo.Version + ".zip";//���ϴ���zip�ļ�
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
            //string FilePath = AppConfig.filePath + xmlInfo.PluginInfo.PluginCode;//��ǰ���CODE�ļ���
            //while (!Directory.Exists(FilePath))
            //{
            //    Directory.CreateDirectory(FilePath);
            //}

            //string thispath = FilePath + @"\"; //+ xmlInfo.VersionName;//��ǰ�ƶ���ȥ���ļ���
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
            XmlConfigInfo x = CommonMethods.GetEntityFromXML(xmlpath + @"\" + Common.Entities.Constants.PluginPath);
            return x;
        }

        /// <summary>
        /// д��XML
        /// </summary>
        /// <param name="path"></param>
        private void InsertXml(VersionTrack ver)
        {
            XmlConfigInfo xml = new XmlConfigInfo();
            //xml.VersionName = ver.VersionName;
            //xml.PluginInfo = BoFactory.GetVersionTrackBo.GetPluginInfo(ver.PluginCode.ToString());//��ȡ�����Ϣ

            SearchConfig serach = new SearchConfig();
            serach.PluginCode = ver.PluginCode.ToString();
            //xml.configList = BoFactory.GetVersionTrackBo.GetConfigList(serach);//��ȡ������Ϣ

            CommonMethods.WritePluginfoConfigXml(xml, ver.FilePath.Trim() + @"\" + Constants.pluginName);
        }

        #endregion

    }
}
