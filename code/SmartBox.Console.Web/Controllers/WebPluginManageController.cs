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

        #region ��һ�����汾��Ϣ�б�

        public ActionResult WebPluginVersionList()
        {
            return View();
        }
        //��һ������ð汾�б�
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetVersionTrackList(FormCollection form)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryPluginInWeb(view);
            return Json(data);
        }

        #endregion

        #region �ڶ���������汾���������Ϣ

        public ActionResult UpdatePluginInfo(string verid, string isAdd)
        {
            PluginInfo pinfo = null;
            ViewData["vername"] = "";
            ViewData["actionCode"] = "";
            ViewData["summary"] = "";
            ViewData["versionId"] = "";
            ViewData["isAdd"] = "";//�Ƿ�����

            IResourceData[] datas = new IResourceData[1];
            datas[0] = ResourceDataFactory.GetResourceDataInstance();
            datas[0].Code = "WebPlugin";
            datas[0].Name = "Webҳ����";
            ViewData["list"] = datas;

            if (string.IsNullOrEmpty(verid))//��Ϊ����
                pinfo = new PluginInfo();
            else
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = verid;
                VersionTrack vsion = BoFactory.GetVersionTrackBo.GetVersionTrack(search)[0];//��ȡ�汾��Ϣ

                pinfo = BoFactory.GetVersionTrackBo.GetPluginInfo(vsion.PluginCode.ToString());//��ȡ�����Ϣ

                if (isAdd.Equals("0"))//��Ϊ�޸ģ����޸İ汾��Ϣ,��չ��Ϣ�������Ϣ��
                {
                    ViewData["vername"] = vsion.VersionName;//��ȡ�汾��Ϣ

                    SearchConfig searchconfig = new SearchConfig();
                    searchconfig.PluginCode = vsion.PluginCode.ToString();
                    IList<ActionExtend> listAction = BoFactory.GetVersionTrackBo.QueryActionExtend(searchconfig);//��ȡ��չ��Ϣ
                    if (listAction.Count > 0)
                    {
                        ViewData["actionCode"] = listAction[0].ActionCode;
                        ViewData["summary"] = listAction[0].Summary;
                    }
                }
                else//��ֻ���޸Ĳ����Ϣ��
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
                actionExtend.Summary = form["ESummary"].ToString();//��չ��Ϣ����
                //��ȡͨ�õİ汾��Ϣ���޸ģ�������������
                version1.VersionStatus = 0;
                version1.FilePath = AppConfig.filePath + pluginInfo.PluginCode + @"\" + version1.VersionName;
                version1.CreateUid = base.CurrentUser.UserUId;
                version1.LastModUid = base.CurrentUser.UserUId;
                string filepaht = version1.FilePath;

                if (version1.VersionId == 0)//������
                {
                    int vid = BoFactory.GetVersionTrackBo.InsertWebPluginInfo(pluginInfo, actionExtend, version1, base.CurrentUser.UserUId);
                    vmobject.Msg = vid.ToString();
                }
                else
                {
                    filepaht = BoFactory.GetVersionTrackBo.UpdateWebPluginInfo(pluginInfo, actionExtend, version1, isAdd, base.CurrentUser.UserUId);
                    vmobject.Msg = version1.VersionId.ToString();
                }

                //�����ļ���
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
                InsertXml(ver);

                string name = BoFactory.GetVersionTrackBo.GetPluginInfo(ver.PluginCode.ToString()).PluginCode;

                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = ver.PreVersionId.ToString();
                IList<VersionTrack> list = BoFactory.GetVersionTrackBo.GetVersionTrack(search);//��ȡ��һ���汾��

                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (list.Count > 0)//�������ڴ˼�¼˵���ǵ�һ�η���
                    pub.UpdateApplication(ver.FilePath, name);
                else
                    pub.CreateApplication(ver.FilePath, name);

                ver.LastModTime = DateTime.Now;
                ver.LastModUid = base.CurrentUser.UserUId;
                ver.VersionStatus = 1;
                BoFactory.GetVersionTrackBo.UpdatePlushVersionTrack(ver);

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
