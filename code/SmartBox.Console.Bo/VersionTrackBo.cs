using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Dao;
using SmartBox.Console.Common;
using Beyondbit.Framework.Core.InterceptorHandler;
using SmartBox.Console.Common.Entities.Search;
using Beyondbit.Framework.Biz;
using Beyondbit.Framework.DataAccess;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
using System.Net;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using Beyondbit.Cryptography;

namespace SmartBox.Console.Bo
{
    public class VersionTrackBo
    {

        #region 变量与属性
        private LogDao _logDao;
        protected LogDao logDao
        {
            get
            {
                if (_logDao == null)
                { _logDao = new LogDao(AppConfig.mainDbKey); }
                return _logDao;
            }
        }

        private VersionTrackDao _VersionTrackDao;
        protected VersionTrackDao versionTrackDao
        {
            get
            {
                if (_VersionTrackDao == null)
                {
                    _VersionTrackDao = new VersionTrackDao(AppConfig.mainDbKey);
                }
                return _VersionTrackDao;
            }
        }
        private PluginDao _pluginDao;
        protected PluginDao pluginDao
        {
            get
            {
                if (_pluginDao == null)
                {
                    _pluginDao = new PluginDao(AppConfig.mainDbKey);
                }
                return _pluginDao;
            }
        }

        private PluginInfoTempDao _plugintempDao;
        protected PluginInfoTempDao plugintempDao
        {
            get
            {
                if (_plugintempDao == null)
                {
                    _plugintempDao = new PluginInfoTempDao(AppConfig.mainDbKey);
                }
                return _plugintempDao;
            }
        }

        private ConfigTempDao _configTempDao;
        protected ConfigTempDao configTempDao
        {
            get
            {
                if (_configTempDao == null)
                {
                    _configTempDao = new ConfigTempDao(AppConfig.mainDbKey);
                }
                return _configTempDao;
            }
        }

        private ConfigInfoDao _configInfoDao;
        protected ConfigInfoDao configInfoDao
        {
            get
            {
                if (_configInfoDao == null)
                {
                    _configInfoDao = new ConfigInfoDao(AppConfig.mainDbKey);
                }
                return _configInfoDao;
            }
        }

        private ConfigInfoPCDao _configInfoPCDao;
        protected ConfigInfoPCDao configInfoPCDao
        {
            get
            {
                if (_configInfoPCDao == null)
                {
                    _configInfoPCDao = new ConfigInfoPCDao(AppConfig.mainDbKey);
                }
                return _configInfoPCDao;
            }
        }

        private ConfigCategoryDao _configCategory;
        protected ConfigCategoryDao configCategoryDao
        {
            get
            {
                if (_configCategory == null)
                {
                    _configCategory = new ConfigCategoryDao(AppConfig.mainDbKey);
                }
                return _configCategory;
            }
        }

        private ActionExtendDao _ActionExtendDao;
        protected ActionExtendDao actionExtendDao
        {
            get
            {
                if (_ActionExtendDao == null)
                {
                    _ActionExtendDao = new ActionExtendDao(AppConfig.mainDbKey);
                }
                return _ActionExtendDao;
            }
        }

        private UserInfoDao _UserInfoDao;
        protected UserInfoDao userInfoDao
        {
            get
            {
                if (_UserInfoDao == null)
                {
                    _UserInfoDao = new UserInfoDao(AppConfig.mainDbKey);
                }
                return _UserInfoDao;
            }
        }

        private IMGroupDao _imGroupDao;
        private IMGroupDao IMGroupDao
        {
            get
            {
                if (_imGroupDao == null)
                {
                    _imGroupDao = new IMGroupDao(AppConfig.mainDbKey);
                }
                return _imGroupDao;
            }
        }

        private UserPluginRefDao _UserPluginRefDao;
        protected UserPluginRefDao userPluginRefDao
        {
            get
            {
                if (_UserPluginRefDao == null)
                {
                    _UserPluginRefDao = new UserPluginRefDao(AppConfig.mainDbKey);
                }
                return _UserPluginRefDao;
            }
        }

        private ApplicationDao _applicationDao;
        protected ApplicationDao applicationDao
        {
            get
            {
                if (_applicationDao == null)
                {
                    _applicationDao = new ApplicationDao(AppConfig.mainDbKey);
                }
                return _applicationDao;
            }
        }

        private AppPrivilegeDao _appPrivilegeDao;
        protected AppPrivilegeDao appPrivilegeDao
        {
            get
            {
                if (_appPrivilegeDao == null)
                {
                    _appPrivilegeDao = new AppPrivilegeDao(AppConfig.mainDbKey);
                }
                return _appPrivilegeDao;
            }
        }

        private PrivilegeUserDao _privilegeUserDao;
        protected PrivilegeUserDao privilegeUserDao
        {
            get
            {
                if (_privilegeUserDao == null)
                {
                    _privilegeUserDao = new PrivilegeUserDao(AppConfig.mainDbKey);
                }
                return _privilegeUserDao;
            }
        }

        private App4AIDao _app4AIDao;
        protected App4AIDao app4AIDao
        {
            get
            {
                if (_app4AIDao == null)
                {
                    _app4AIDao = new App4AIDao(AppConfig.mainDbKey);
                }
                return _app4AIDao;
            }
        }

        private WebApplicationDao _webApplicationDao;
        protected WebApplicationDao webApplicationDao
        {
            get
            {
                if (_webApplicationDao == null)
                {
                    _webApplicationDao = new WebApplicationDao(AppConfig.mainDbKey);
                }
                return _webApplicationDao;
            }
        }

        private ApplicationCategoryDao _applicationCategoryDao;
        protected ApplicationCategoryDao applicationCategoryDao
        {
            get
            {
                if (_applicationCategoryDao == null)
                {
                    _applicationCategoryDao = new ApplicationCategoryDao(AppConfig.mainDbKey);
                }
                return _applicationCategoryDao;
            }
        }

        private ClientTypeDao _clientTypeDao;
        protected ClientTypeDao clientTypeDao
        {
            get
            {
                if (_clientTypeDao == null)
                {
                    _clientTypeDao = new ClientTypeDao(AppConfig.mainDbKey);
                }
                return _clientTypeDao;
            }
        }

        private Package4AIDao _package4AIDao;
        protected Package4AIDao package4AIDao
        {
            get
            {
                if (_package4AIDao == null)
                {
                    _package4AIDao = new Package4AIDao(AppConfig.mainDbKey);
                }
                return _package4AIDao;
            }
        }

        private SMC_Package4OutDao _package4OutDao;
        protected SMC_Package4OutDao package4OutDao
        {
            get
            {
                if (_package4OutDao == null)
                {
                    _package4OutDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
                }
                return _package4OutDao;
            }
        }

        private SMC_PackageExtDao _packageExtDao;
        protected SMC_PackageExtDao packageExtDao
        {
            get
            {
                if (_packageExtDao == null)
                {
                    _packageExtDao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
                }
                return _packageExtDao;
            }
        }

        private SMC_PackageSyncDao _packageSyncDao;
        protected SMC_PackageSyncDao packageSyncDao
        {
            get
            {
                if (_packageSyncDao == null)
                {
                    _packageSyncDao = new SMC_PackageSyncDao(AppConfig.statisticDBKey);
                }
                return _packageSyncDao;
            }
        }

        private SMC_PackagePictureDao _packagePictureDao;
        protected SMC_PackagePictureDao packagePictureDao
        {
            get
            {
                if (_packagePictureDao == null)
                {
                    _packagePictureDao = new SMC_PackagePictureDao(AppConfig.statisticDBKey);
                }
                return _packagePictureDao;
            }
        }

        private SMC_PackageFAQDao _packageFAQDao;
        protected SMC_PackageFAQDao packageFAQDao
        {
            get
            {
                if (_packageFAQDao == null)
                {
                    _packageFAQDao = new SMC_PackageFAQDao(AppConfig.statisticDBKey);
                }
                return _packageFAQDao;
            }
        }

        private SMC_CollectDao _collectDao;
        protected SMC_CollectDao collectDao
        {
            get
            {
                if (_collectDao == null)
                {
                    _collectDao = new SMC_CollectDao(AppConfig.statisticDBKey);
                }
                return _collectDao;
            }
        }

        private SMC_PackageManualDao _packageManualDao;
        protected SMC_PackageManualDao packageManualDao
        {
            get
            {
                if (_packageManualDao == null)
                {
                    _packageManualDao = new SMC_PackageManualDao(AppConfig.statisticDBKey);
                }
                return _packageManualDao;
            }
        }

        private Action4AndroidDao _action4AndroidDao;
        protected Action4AndroidDao action4AndroidDao
        {
            get
            {
                if (_action4AndroidDao == null)
                {
                    _action4AndroidDao = new Action4AndroidDao(AppConfig.mainDbKey);
                }
                return _action4AndroidDao;
            }
        }

        private HomePlanDao _homePlanDao;
        protected HomePlanDao homePlanDao
        {
            get
            {
                if (_homePlanDao == null)
                {
                    _homePlanDao = new HomePlanDao(AppConfig.mainDbKey);
                }
                return _homePlanDao;
            }
        }

        private HomePlanDesignDao _homePlanDesignDao;
        protected HomePlanDesignDao homePlanDesignDao
        {
            get
            {
                if (_homePlanDesignDao == null)
                {
                    _homePlanDesignDao = new HomePlanDesignDao(AppConfig.mainDbKey);
                }
                return _homePlanDesignDao;
            }
        }

        private ImageDao _imageDao;
        protected ImageDao imageDao
        {
            get
            {
                if (_imageDao == null)
                {
                    _imageDao = new ImageDao(AppConfig.mainDbKey);
                }
                return _imageDao;
            }
        }

        private IOSOutsideAppDao _iosOutsideAppDao;
        protected IOSOutsideAppDao iosOutsideAppDao
        {
            get
            {
                if (_iosOutsideAppDao == null)
                {
                    _iosOutsideAppDao = new IOSOutsideAppDao(AppConfig.mainDbKey);
                }
                return _iosOutsideAppDao;
            }
        }

        private ApplyDeviceBindDao _applyDeviceBindDao;
        protected ApplyDeviceBindDao applyDeviceBindDao
        {
            get
            {
                if (_applyDeviceBindDao == null)
                {
                    _applyDeviceBindDao = new ApplyDeviceBindDao(AppConfig.mainDbKey);
                }
                return _applyDeviceBindDao;
            }
        }

        private DeviceBindDao _deviceBindDao;
        protected DeviceBindDao deviceBindDao
        {
            get
            {
                if (_deviceBindDao == null)
                {
                    _deviceBindDao = new DeviceBindDao(AppConfig.mainDbKey);
                }
                return _deviceBindDao;
            }
        }

        private ManagerDao _managerDao;
        protected ManagerDao managerDao
        {
            get
            {
                if (_managerDao == null)
                {
                    _managerDao = new ManagerDao(AppConfig.mainDbKey);
                }
                return _managerDao;
            }
        }
        #endregion

        #region 2.0.1.0

        #region 插件

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pcode">插件code</param>
        /// <param name="arr">相关版本ID</param>
        [Frame(true, true)]
        public virtual void DelVersion(ArrayList arr)
        {
            DelVersionInfo(arr);
        }

        [Frame(true, true)]
        public virtual void DelAllVersion(ArrayList arr)
        {
            try
            {
                string vpath = "";
                foreach (string a in arr)
                {
                    SearchVersionTrack se1 = new SearchVersionTrack();
                    se1.VID = a;
                    VersionTrack v = versionTrackDao.GetVersionTrackList(se1)[0];
                    vpath = v.FilePath;
                    string pcode = v.PluginCode;//查出当前插件code

                    SearchConfig search = new SearchConfig();
                    search.PluginCode = pcode;
                    configInfoDao.DeleteConfigInfo(search);//配置删除
                    actionExtendDao.DelActionExtendInfo(search);//action删除
                    SearchVersionTrack se = new SearchVersionTrack();
                    se.PluginCode = pcode;
                    versionTrackDao.DeleteInfo(se);//删版本
                    userPluginRefDao.DeleteUPInfo(pcode);
                    pluginDao.DeleteInfo(search);//删插件
                    //删临时表
                    plugintempDao.DeleteInfo(search);
                    configTempDao.DeleteInfo(search);
                }

                string[] codes = vpath.Split(new string[] { "\\" }, StringSplitOptions.None);
                string name = codes[codes.Length - 2];//获取插件code
                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (Directory.Exists(Path.Combine(AppConfig.pubFolder, name + AppConfig.subFix)))//如果存在次文件夹
                    pub.DeleteApplication(name);

                //zip不删，由原来存在相同会自动删除
                vpath = vpath.Substring(0, vpath.LastIndexOf("\\"));

                if (Directory.Exists(vpath)) //删除原由文件夹
                    Directory.Delete(vpath, true);

            }
            catch (DalException ex)
            {
                throw new BOException("删除版本出错", ex);
            }
        }


        [Frame(true, true)]
        public virtual void DelActiveVersion(ArrayList arr)
        {
            ArrayList activeVersion = null;//正在使用的版本
            ArrayList resumeVersion = new ArrayList();//恢复的版本
            XmlConfigInfo xml = null;
            //获取需要恢复的上个版本ID
            foreach (string a in arr)
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = a;
                resumeVersion.Add(versionTrackDao.GetVersionTrackList(search)[0].PreVersionId);
            }
            //解析
            SearchVersionTrack searchr = new SearchVersionTrack();
            searchr.VID = resumeVersion[0].ToString();
            IList<VersionTrack> v = versionTrackDao.GetVersionTrackList(searchr);
            if (v.Count == 0)
                throw new Exception("请确保该版本的上一版本是否存在");
            xml = GetXmlInfo(v[0].FilePath);

            DelVersionInfo(arr); //删除正在使用的version
            resumeVersionInfo(activeVersion, resumeVersion, xml);//恢复
        }




        [Frame(true, true)]
        public virtual void ResumeExpiredVesion(ArrayList arr)
        {
            ArrayList activeVersion = new ArrayList();
            ArrayList resumeVersion = arr;
            XmlConfigInfo xml = null;
            //获取需要恢复的版本ID
            foreach (string a in arr)
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = a;
                string pcode = versionTrackDao.GetVersionTrackList(search)[0].PluginCode;
                search.VID = "";
                search.PluginCode = pcode;
                search.VersionStatus = "1";
                activeVersion.Add(versionTrackDao.GetVersionTrackList(search)[0].VersionId);
            }
            //解析
            SearchVersionTrack searchr = new SearchVersionTrack();
            searchr.VID = resumeVersion[0].ToString();
            xml = GetXmlInfo(versionTrackDao.GetVersionTrackList(searchr)[0].FilePath);

            resumeVersionInfo(activeVersion, resumeVersion, xml);//恢复
        }


        #endregion


        #region 主程序

        [Frame(true, true)]
        public virtual void ResumeExpiredVesionByMain(ArrayList arr)
        {
            ArrayList activeVersion = new ArrayList();
            ArrayList resumeVersion = arr;
            XmlMainConfigInfo xml = null;
            //获取需要恢复的版本ID
            foreach (string a in arr)
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = a;
                string pcode = versionTrackDao.GetVersionTrackList(search)[0].PluginCode;
                search.VID = "";
                search.PluginCode = pcode;
                search.VersionStatus = "1";
                activeVersion.Add(versionTrackDao.GetVersionTrackList(search)[0].VersionId);
            }
            //解析
            SearchVersionTrack searchr = new SearchVersionTrack();
            searchr.VID = resumeVersion[0].ToString();
            xml = GetXmlInfoByMain(versionTrackDao.GetVersionTrackList(searchr)[0].FilePath);

            resumeVersionInfoByMain(activeVersion, resumeVersion, xml, Constants.MianName);//恢复
        }

        [Frame(true, true)]
        public virtual void DelActiveVersionByMain(ArrayList arr)
        {
            ArrayList activeVersion = null;//正在使用的版本
            ArrayList resumeVersion = new ArrayList();//恢复的版本
            XmlMainConfigInfo xml = null;
            //获取需要恢复的上个版本ID
            foreach (string a in arr)
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = a;
                resumeVersion.Add(versionTrackDao.GetVersionTrackList(search)[0].PreVersionId);
            }
            //解析
            SearchVersionTrack searchr = new SearchVersionTrack();
            searchr.VID = resumeVersion[0].ToString();
            IList<VersionTrack> v = versionTrackDao.GetVersionTrackList(searchr);
            if (v.Count == 0)
                throw new Exception("请确保该版本的上一版本是否存在");
            xml = GetXmlInfoByMain(v[0].FilePath);

            DelVersionInfo(arr); //删除正在使用的version
            resumeVersionInfoByMain(activeVersion, resumeVersion, xml, Constants.MianName);//恢复
        }

        [Frame(true, true)]
        public virtual void DelAllVersionByMain(ArrayList arr)
        {
            try
            {
                string vpath = "";
                foreach (string a in arr)
                {
                    SearchVersionTrack se1 = new SearchVersionTrack();
                    se1.VID = a;
                    VersionTrack v = versionTrackDao.GetVersionTrackList(se1)[0];
                    vpath = v.FilePath;
                    string pcode = v.PluginCode;//查出当前插件code
                    SearchConfig search = new SearchConfig();
                    search.PluginCode = pcode;
                    configInfoDao.DeleteConfigInfo(search);//配置删除
                    SearchVersionTrack se = new SearchVersionTrack();
                    se.PluginCode = pcode;
                    versionTrackDao.DeleteInfo(se);//删版本
                    //删临时表
                    configTempDao.DeleteInfo(search);
                }

                string[] codes = vpath.Split(new string[] { "\\" }, StringSplitOptions.None);
                string name = codes[codes.Length - 2];//获取插件code
                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                if (Directory.Exists(Path.Combine(AppConfig.pubFolder, name + AppConfig.subFix)))//如果存在次文件夹
                    pub.DeleteApplication(name);


                //zip不删，由原来存在相同会自动删除
                vpath = vpath.Substring(0, vpath.LastIndexOf("\\"));

                if (Directory.Exists(vpath)) //删除原由文件夹
                    Directory.Delete(vpath, true);

            }
            catch (DalException ex)
            {
                throw new BOException("删除版本出错", ex);
            }
        }

        #endregion

        #region 升级程序

        [Frame(true, true)]
        public virtual void ResumeExpiredVesionByUpdater(ArrayList arr)
        {
            ArrayList activeVersion = new ArrayList();
            ArrayList resumeVersion = arr;
            XmlMainConfigInfo xml = null;
            //获取需要恢复的版本ID
            foreach (string a in arr)
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = a;
                string pcode = versionTrackDao.GetVersionTrackList(search)[0].PluginCode;
                search.VID = "";
                search.PluginCode = pcode;
                search.VersionStatus = "1";
                activeVersion.Add(versionTrackDao.GetVersionTrackList(search)[0].VersionId);
            }
            //解析
            SearchVersionTrack searchr = new SearchVersionTrack();
            searchr.VID = resumeVersion[0].ToString();
            xml = GetXmlInfoByUpdater(versionTrackDao.GetVersionTrackList(searchr)[0].FilePath);

            resumeVersionInfoByMain(activeVersion, resumeVersion, xml, Constants.UpdaterCode);//恢复
        }

        [Frame(true, true)]
        public virtual void DelActiveVersionByUpdater(ArrayList arr)
        {
            ArrayList activeVersion = null;//正在使用的版本
            ArrayList resumeVersion = new ArrayList();//恢复的版本
            XmlMainConfigInfo xml = null;
            //获取需要恢复的上个版本ID
            foreach (string a in arr)
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.VID = a;
                resumeVersion.Add(versionTrackDao.GetVersionTrackList(search)[0].PreVersionId);
            }
            //解析
            SearchVersionTrack searchr = new SearchVersionTrack();
            searchr.VID = resumeVersion[0].ToString();
            IList<VersionTrack> v = versionTrackDao.GetVersionTrackList(searchr);
            if (v.Count == 0)
                throw new Exception("请确保该版本的上一版本是否存在");
            xml = GetXmlInfoByUpdater(v[0].FilePath);

            DelVersionInfo(arr); //删除正在使用的version
            resumeVersionInfoByMain(activeVersion, resumeVersion, xml, Constants.UpdaterCode);//恢复
        }

        #endregion

        #endregion

        #region 配置管理

        [Frame(false, false)]
        public virtual IList<ConfigTemp> GetConfigListTemp(SearchConfig search)
        {
            try
            {
                return configTempDao.GetConfigList(search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取配置表信息", ex);
            }
        }

        [Frame(true, true)]
        public virtual void UpdateConfigInfos(IList<ConfigInfo> list, IList<PluginInfo> listp)
        {
            try
            {
                SearchConfig search = new SearchConfig();
                foreach (PluginInfo p in listp)
                {
                    search.PluginCode = p.PluginCode;
                    configInfoDao.DeleteConfigInfo(search);

                    PluginInfo pInfo = pluginDao.Get(p.PluginCode);
                    pInfo.CompanyTel = p.CompanyTel;
                    pInfo.CompanyName = p.CompanyName;
                    pInfo.CompanyLinkman = p.CompanyLinkman;
                    pInfo.CompanyHomePage = p.CompanyHomePage;
                    //新增 
                    pInfo.Sequence = p.Sequence;

                    pluginDao.Update(pInfo);
                }

                foreach (ConfigInfo c in list)
                {
                    if (string.IsNullOrEmpty(c.Key1))
                        throw new BOException("配置信息中的健值不能为空");
                    SearchConfig searchc = new SearchConfig();
                    searchc.key = c.Key1;
                    searchc.PluginCode = c.PluginCode;
                    searchc.ConfigCategoryCode = c.ConfigCategoryCode;
                    IList<ConfigInfo> listc = configInfoDao.GetConfigList(searchc);
                    if (listc != null)
                    {
                        if (listc.Count > 0)
                            throw new BOException("单个插件的关键值不能重复");
                    }
                    configInfoDao.Insert(c);
                }

            }
            catch (DalException ex)
            {
                throw new BOException("更新配置信息出错", ex);
            }
        }


        private void InserConfigInfo(IList<ConfigTemp> coglist, string PluginCode, string cate)
        {
            try
            {
                foreach (ConfigTemp cog in coglist)
                {
                    if (string.IsNullOrEmpty(cog.Key1))
                        throw new BOException("配置信息中的健值不能为空");
                    //验证同一个插件的key不能重复
                    SearchConfig search = new SearchConfig();
                    search.key = cog.Key1;
                    search.PluginCode = cog.PluginCode;
                    if (string.IsNullOrEmpty(cog.PluginCode))
                    {
                        search.PluginCode = PluginCode;
                        cog.PluginCode = PluginCode;
                    }
                    search.ConfigCategoryCode = cate;
                    IList<ConfigTemp> list = configTempDao.GetConfigList(search);
                    if (list != null)
                    {
                        if (list.Count > 0)
                            throw new BOException("该主程序或者插件配置信息中的键已经存在！");
                    }
                    configTempDao.Insert(cog);
                }

            }
            catch (DalException ex)
            {
                throw new BOException("新赠配置表信息", ex);
            }
        }

        private void InserConfigInfo(IList<ConfigTemp> coglist, string PluginCode)
        {
            try
            {
                foreach (ConfigTemp cog in coglist)
                {
                    if (string.IsNullOrEmpty(cog.Key1))
                        throw new BOException("配置信息中的健值不能为空");
                    //验证同一个插件的key不能重复
                    SearchConfig search = new SearchConfig();
                    search.key = cog.Key1;
                    search.PluginCode = cog.PluginCode;
                    if (string.IsNullOrEmpty(cog.PluginCode))
                    {
                        search.PluginCode = PluginCode;
                        cog.PluginCode = PluginCode;
                    }
                    search.ConfigCategoryCode = cog.ConfigCategoryCode;
                    IList<ConfigTemp> list = configTempDao.GetConfigList(search);
                    if (list != null)
                    {
                        if (list.Count > 0)
                            throw new BOException("该主程序配置信息中的键已经存在！");
                    }
                    configTempDao.Insert(cog);
                }

            }
            catch (DalException ex)
            {
                throw new BOException("新赠配置表信息", ex);
            }
        }

        [Frame(true, true)]
        public virtual void SetConfigOfMode(ConfigInfo conf)
        {
            try
            {
                SearchConfig search = new SearchConfig();
                if (string.IsNullOrEmpty(conf.Key1))
                    throw new BOException("配置信息中的健值不能为空");
                SearchConfig searchc = new SearchConfig();
                searchc.key = conf.Key1;
                searchc.PluginCode = conf.PluginCode;
                searchc.ConfigCategoryCode = conf.ConfigCategoryCode;
                IList<ConfigInfo> listc = configInfoDao.GetConfigList(searchc);
                if (listc != null)
                {
                    if (listc.Count > 0)
                    {
                        configInfoDao.Delete(listc[0]);
                    }
                }
                configInfoDao.Insert(conf);
            }
            catch (DalException ex)
            {
                throw new BOException("更新配置信息出错", ex);
            }
        }

        #endregion

        #region 配置分类表

        [Frame(false, false)]
        public virtual ConfigCategory GetConfig(string id)
        {
            try
            {
                return configCategoryDao.GetInfo(id);
            }
            catch (DalException ex)
            {
                throw new BOException("获取配置分类表信息", ex);
            }
        }


        #endregion

        public virtual JsonFlexiGridData QueryIMGroupList(PageView view)
        {
            return IMGroupDao.QueryGroupList(view);
        }

        #region 插件管理
        [Frame(true, true)]
        public virtual void UpdateStatusByPlugin(string code, bool status)
        {
            try
            {
                PluginInfo p = pluginDao.Get(code);
                p.IsUse = !status;
                pluginDao.Update(p);
            }
            catch (DalException ex)
            {
                throw new BOException("更新插件禁用状态失败", ex);
            }
        }


        public IList<PluginInfoTemp> QueryActionExtends(SearchConfig search)
        {
            try
            {
                return plugintempDao.GetTempList(search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取插件扩展信息出错", ex);
            }
        }


        [Frame(true, true)]
        public virtual void UpdatePlushVersionTracks(XmlConfigInfo xml, string vid, string uid)
        {
            try
            {
                foreach (PluginInfoTemp pt in xml.PluginInfo)
                {
                    if (pt.IsIgnoreConfig == false)
                    {
                        SearchConfig search = new SearchConfig();
                        search.PluginCode = pt.PluginCode;
                        configInfoDao.DeleteConfigInfo(search);//先删除原由真实参数表数据
                    }
                }

                string[] arrs = xml.PluginInfo[0].PreVersionPCs.Split(new string[] { "_FG$SP_" }, StringSplitOptions.None);
                //设置原有版本的插件为未发布。再设置当前的插件为发布
                if (!string.IsNullOrEmpty(xml.PluginInfo[0].PreVersionPCs))
                {
                    foreach (string arr in arrs)
                    {
                        PluginInfo pp = pluginDao.Get(arr);
                        if (pp != null)
                        {
                            pp.IsUse = false;
                            pluginDao.Update(pp);
                        }
                    }
                }
                //更新原由版本为过期
                SearchVersionTrack searchtemps = new SearchVersionTrack();
                searchtemps.VID = vid;
                VersionTrack vsss = versionTrackDao.GetVersionTrackList(searchtemps)[0];
                string filepath = vsss.FilePath;
                SearchVersionTrack setemp = new SearchVersionTrack();
                setemp.filepath = filepath;
                IList<VersionTrack> vlisttemp = versionTrackDao.GetVersionTrackList(setemp);//获得上一所有插件版本
                foreach (VersionTrack v in vlisttemp)
                {
                    v.VersionStatus = 2;
                    versionTrackDao.Update(v);
                }

                foreach (PluginInfoTemp pt in xml.PluginInfo)
                {
                    PluginInfo p = TPluginInfo(pt);
                    p.CreateTime = DateTime.Now;
                    p.CreateUid = uid;
                    p.LastModTime = DateTime.Now;
                    p.LastModUid = uid;
                    int a = 0;
                    foreach (string arr in arrs)
                    {
                        if (pt.PluginCode.Equals(arr))
                        {
                            a = 1;
                            break;
                        }
                    }
                    //更新插件信息
                    if (a == 1)
                    {
                        PluginInfo oldP = pluginDao.Get(p.PluginCode);
                        p.CreateUid = oldP.CreateUid;
                        p.CreateTime = oldP.CreateTime;
                        p.IsUse = true;//设置为发布
                        pluginDao.Update(p);
                    }
                    else
                    {
                        p.IsUse = true;//设置为发布
                        InsertPluginInfo(p, uid);
                    }

                    plugintempDao.Delete(pt);//删除插件临时表

                    if (pt.IsIgnoreConfig == false)
                    {
                        SearchConfig search = new SearchConfig();
                        search.PluginCode = pt.PluginCode;

                        //没修改基本信息，直接发布,需要覆盖
                        foreach (ConfigInfo c in pt.configList)
                        {
                            SearchConfig searchcc = new SearchConfig();
                            searchcc.PluginCode = pt.PluginCode;
                            searchcc.ConfigCategoryCode = Constants.configCategory;
                            searchcc.key = c.Key1;
                            IList<ConfigInfo> listct = configInfoDao.GetConfigList(searchcc);
                            c.ConfigCategoryCode = Constants.configCategory;
                            if (listct.Count > 0)
                                configInfoDao.Update(c);
                            else
                                configInfoDao.Insert(c);
                        }

                        configTempDao.DeleteInfo(search);//删除当前插件临时表数据
                    }

                    //修改扩展信息
                    if (pt.PluginCateCode.Equals(Constants.ActionCateCode))
                    {
                        ActionExtend action = new ActionExtend();
                        action.PluginCode = pt.PluginCode;
                        action.ActionCode = pt.ActionCode;
                        action.Summary = pt.ActionSummary;

                        SearchConfig searchconfig = new SearchConfig();
                        searchconfig.PluginCode = pt.PluginCode.ToString();
                        IList<ActionExtend> listAction = BoFactory.GetVersionTrackBo.QueryActionExtend(searchconfig);
                        if (listAction.Count > 0)//若存在记录，即更新
                            UpdateActionExtend(action);
                        else
                            InsertActionExtend(action);
                    }

                    SearchVersionTrack searchv = new SearchVersionTrack();
                    searchv.PluginCode = pt.PluginCode;
                    searchv.VersionName = pt.Version;
                    VersionTrack ver = versionTrackDao.GetVersionTrackList(searchv)[0];//当前版本
                    ver.VersionStatus = 1;
                    ver.VersionSummary = pt.VersionSummary;
                    versionTrackDao.Update(ver);
                }
            }
            catch (DalException ex)
            {
                throw new BOException("修改主程序版本信息出错", ex);
            }
        }


        [Frame(false, false)]
        public virtual IList<PluginCategory> GetPluginCategoryInfos(SearchPlugin search)
        {
            try
            {
                return pluginDao.GetPluginCategoryInfo(search);
            }
            catch (DalException ex)
            {
                throw new BOException("查询插件分类信息出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPluginInfos(PageView view)
        {
            try
            {
                return pluginDao.QueryPluginInfos(view);
            }
            catch (DalException ex)
            {
                throw new BOException("获取插件相关所有信息出错", ex);
            }
        }



        [Frame(false, false)]
        public virtual PluginInfoTemp GetPluginTempInfo(string id)
        {
            try
            {
                return plugintempDao.Get(id);
            }
            catch (DalException ex)
            {
                throw new BOException("根据ID获取插件信息出错", ex);
            }
        }

        [Frame(true, true)]
        public virtual void UpdateTempPlugin(IList<PluginInfoTemp> plist, IList<ConfigTemp> listc, string IsAdd, string uid)
        {
            try
            {
                foreach (PluginInfoTemp p in plist)
                {
                    if (!string.IsNullOrEmpty(p.ActionCode))
                    {
                        SearchConfig search = new SearchConfig();
                        search.code = p.ActionCode;
                        IList<ActionExtend> list = QueryActionExtend(search);
                        if (list != null)
                        {
                            if (list.Count > 0 && !list[0].PluginCode.Equals(p.PluginCode))
                                throw new BOException("插件扩展信息标识不能重复");
                        }
                        IList<PluginInfoTemp> listtemp = QueryActionExtends(search);
                        if (listtemp != null)
                        {
                            if (listtemp.Count > 0 && !listtemp[0].PluginCode.Equals(p.PluginCode))
                                throw new BOException("插件扩展信息标识不能重复");
                        }
                    }
                    plugintempDao.Update(p);//更新临时插件

                    if (p.IsIgnoreConfig == false)
                    {
                        SearchConfig searchc = new SearchConfig();
                        searchc.PluginCode = p.PluginCode;
                        configTempDao.DeleteInfo(searchc);
                    }
                }

                if (plist[0].IsIgnoreConfig == false)
                {
                    if (listc != null)
                    {
                        if (listc.Count > 0)
                            InserConfigInfo(listc, plist[0].PluginCode, Constants.configCategory);//更新配置
                    }
                }
            }
            catch (DalException ex)
            {
                throw new BOException("新增插件扩展信息出错", ex);
            }
        }

        [Frame(true, true)]
        public virtual string SavePluingZipInfo(VersionTrack version, XmlConfigInfo xmlInfo, string IsAdd, string IsUpdate, string uid, string vids)
        {
            string oldfp = "";
            string oldname = "";
            string PluginCode = "";
            IList<PluginInfoTemp> plist = xmlInfo.PluginInfo;
            SearchVersionTrack search = new SearchVersionTrack();
            ArrayList arrVid = new ArrayList();
            string arrCode = "";

            try
            {
                //升级（修改）的时候
                //获得关联的vid和code（根据相同的filepath获得相关的VID）
                search.VID = vids.Split(',')[0];
                if (!string.IsNullOrEmpty(vids))
                {
                    VersionTrack vs = versionTrackDao.GetVersionTrackList(search)[0];

                    if (IsAdd.Equals("0"))
                    {
                        int ass = 0;
                        for (int i = 0; i < plist.Count; i++)
                        {
                            if (vs.PluginCode.Equals(plist[i].PluginCode))
                            {
                                ass = 1;
                                break;
                            }
                        }
                        if (ass == 0)
                        {
                            throw new BOException("上传插件code不匹配");
                        }
                    }

                    string filepath = vs.FilePath;
                    SearchVersionTrack setemp = new SearchVersionTrack();
                    setemp.filepath = filepath;
                    IList<VersionTrack> vlisttemp = versionTrackDao.GetVersionTrackList(setemp);//获得当前关联所有插件版本
                    foreach (VersionTrack vtemp in vlisttemp)
                    {
                        SearchVersionTrack searcht = new SearchVersionTrack();
                        searcht.PreVersionId = vtemp.VersionId.ToString();//查找下一个最新版本是否存在
                        IList<VersionTrack> templist = versionTrackDao.GetVersionTrackList(searcht);
                        if (templist.Count > 0)
                        {
                            filepath = templist[0].FilePath;
                            break;
                        }
                    }

                    SearchVersionTrack se = new SearchVersionTrack();
                    se.filepath = filepath;
                    IList<VersionTrack> vlist = versionTrackDao.GetVersionTrackList(se);//获取最新当前所有关联版本信息
                    foreach (VersionTrack v1 in vlist)
                    {
                        arrVid.Add(v1.VersionId);//获取要删除的版本ID（最新版本）

                    }

                    se.filepath = vs.FilePath;
                    IList<VersionTrack> vlistc = versionTrackDao.GetVersionTrackList(se);
                    foreach (VersionTrack v1 in vlistc)
                    {
                        arrCode += v1.PluginCode + "_FG$SP_";//获取插件CODE（原由版本）

                    }
                    arrCode = arrCode.Substring(0, arrCode.Length - 7);
                }
                //删除
                if (!string.IsNullOrEmpty(IsUpdate))//若为修改
                {
                    for (int i = 0; i < arrVid.Count; i++)
                    {
                        search.VID = arrVid[i].ToString();
                        VersionTrack v = versionTrackDao.GetVersionTrackList(search)[0];//2.获取表一记录
                        PluginCode = v.PluginCode;
                        if (i == 0)//第一个插件CODE
                        {
                            string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                            oldfp = v.FilePath;
                            oldname = codes[codes.Length - 1];//版本名
                        }
                        //1.先删除插件临时表
                        SearchConfig searchc = new SearchConfig();
                        searchc.PluginCode = PluginCode.ToString();
                        configTempDao.DeleteInfo(searchc);

                        plugintempDao.DeleteInfo(searchc);//删除配置信息临时表
                        //2.删除版本表信息
                        versionTrackDao.DeleteInfo(search);
                        //3.若是新增（修改）
                        if (IsAdd.Equals("1"))
                        {
                            pluginDao.DeleteInfo(searchc);//.删除插件表
                        }
                    }
                }
                //这里开始新增或者升级或者修改的共同部分处理
                for (int i = 0; i < plist.Count; i++)
                {
                    PluginInfo p = TPluginInfo(plist[i]);//转换
                    if (IsAdd.Equals("1"))//若新增
                    {
                        p.IsUse = false;//设置为未发布
                        plist[i].IsUse = false;
                        PluginCode = InsertPluginInfo(p, uid);//.新增插件表
                    }
                    else//若升级
                    {
                        p.IsUse = false;
                        plist[i].IsUse = false;
                        PluginCode = p.PluginCode;
                    }
                    //4.新增版本表
                    version.PluginCode = PluginCode;
                    version.FilePath = Path.Combine(AppConfig.filePath + xmlInfo.oldCode, xmlInfo.PluginInfo[0].PluginCode + "_FG$SP_" + xmlInfo.PluginInfo[0].Version);//以第一个插件CODE+版本号记录，3个版本存放位置相同
                    version.VersionName = plist[i].Version;
                    version.CreateUid = uid;
                    version.LastModUid = uid;
                    version.VersionStatus = 3;
                    version.VersionSummary = plist[i].VersionSummary;
                    if (IsAdd.Equals("0"))
                        version.VersionStatus = 0;
                    if (i == 0)
                        vids = InsertVersionTrack(version).ToString();//记录第一个vid
                    else
                        InsertVersionTrack(version).ToString();//升级可以重复覆盖原由版本,新赠插件的时候,不可能出现重复版本
                    //5.新赠插件临时表
                    PluginInfoTemp pt = plist[i];
                    pt.PluginCode = PluginCode;
                    //记录原先的上一版本的所有插件CODE，用于更新发布状态
                    if (IsAdd.Equals("0"))//若升级
                    {
                        pt.PreVersionPCs = arrCode;
                    }
                    else
                    {
                        foreach (PluginInfoTemp pi in plist)
                        {
                            pt.PreVersionPCs += pi.PluginCode + "_FG$SP_";
                        }
                        pt.PreVersionPCs = pt.PreVersionPCs.Substring(0, pt.PreVersionPCs.Length - 7);
                    }

                    plugintempDao.Insert(pt);

                    //若不是web插件 
                    if (!plist[i].PluginCateCode.Equals(Constants.PluginCateCode))
                    {
                        if (plist[i].IsIgnoreConfig == false)//非忽略配置信息
                        {
                            //转换
                            IList<ConfigTemp> listts = new List<ConfigTemp>();
                            foreach (ConfigInfo c in plist[i].configList)
                            {
                                listts.Add(CommonMethods.ConvertToConfigInfo(c));
                            }
                            InserConfigInfo(listts, PluginCode.ToString(), Constants.configCategory);//新赠插件配置临时表
                        }
                    }
                }

                if (Directory.Exists(oldfp)) //5.需要删除原由文件夹
                    Directory.Delete(oldfp, true);
                //需要删除存在的位置的原由文件
                FileInfo f = new FileInfo(Path.Combine(AppConfig.SaveZipPath, oldname + ".zip"));
                if (f.Exists)
                    f.Delete();

                if (plist[0].PluginCateCode.Equals(Constants.PluginCateCode))//若是web插件
                {
                    while (!Directory.Exists(version.FilePath))//单独创建文件夹
                    {
                        Directory.CreateDirectory(version.FilePath);
                    }
                }

                return vids;
            }
            catch (DalException ex)
            {
                throw new BOException("上传插件zip文件出错", ex);
            }
        }




        #endregion

        #region 主程序管理
        [Frame(false, false)]
        public virtual VersionTrack GetCurrentSmartBoxInfo()
        {
            try
            {
                return versionTrackDao.GetCurrentSmartBoxInfo();
            }
            catch (DalException ex)
            {
                throw new BOException("获取主程序当前版本信息出错", ex);
            }
        }


        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryMiansInfo(PageView view)
        {
            try
            {
                return versionTrackDao.QueryMiansInfo(view);
            }
            catch (DalException ex)
            {
                throw new BOException("获取主程序相关所有信息出错", ex);
            }
        }

        [Frame(true, true)]
        public virtual string SaveMainZipInfo(VersionTrack version, XmlMainConfigInfo xmlInfo, string IsAdd, string IsUpdate, string uid, string vids)
        {
            string oldfp = "";
            string oldname = "";
            SearchVersionTrack search = new SearchVersionTrack();
            try
            {
                search.VID = vids;
                VersionTrack v = versionTrackDao.GetVersionTrackList(search)[0];
                //删除
                if (!string.IsNullOrEmpty(IsUpdate))//若为修改
                {
                    if (IsUpdate.Equals("1"))//若已有上传文件
                    {
                        string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                        oldfp = v.FilePath;
                        oldname = codes[codes.Length - 1];//版本名
                        SearchConfig searchc = new SearchConfig();
                        searchc.PluginCode = Constants.MianName;
                        configTempDao.DeleteInfo(searchc);
                    }
                }
                //更新版本状态
                if (IsAdd.Equals("1"))
                    v.VersionStatus = 3;
                else
                    v.VersionStatus = 0;
                versionTrackDao.Update(v);
                //转换
                IList<ConfigTemp> listts = new List<ConfigTemp>();
                foreach (ConfigInfoPC c in xmlInfo.configList)
                {
                    listts.Add(CommonMethods.ConvertToConfigInfoPC(c));
                }
                InserConfigInfo(listts, Constants.MianName);//新赠插件配置临时表

                if (Directory.Exists(oldfp)) //5.需要删除原由文件夹
                    Directory.Delete(oldfp, true);
                //需要删除存在的位置的原由文件
                FileInfo f = new FileInfo(Path.Combine(AppConfig.SaveZipPath, oldname + ".zip"));
                if (f.Exists)
                    f.Delete();

                return vids;
            }
            catch (DalException ex)
            {
                throw new BOException("上传插件zip文件出错", ex);
            }
        }


        [Frame(true, true)]
        public virtual string SaveMainVerInfo(VersionTrack version, string IsAdd, string IsUpdate, string uid, string Vid)
        {
            try
            {
                string vids = "";
                SearchVersionTrack search = new SearchVersionTrack();
                if (!string.IsNullOrEmpty(IsUpdate))//若为修改
                {
                    search.VID = Vid;
                    //1.修改版本表信息
                    VersionTrack v = versionTrackDao.GetVersionTrackList(search)[0];
                    v.VersionSummary = version.VersionSummary;
                    versionTrackDao.Update(v);
                    vids = v.VersionId.ToString();
                }
                else
                {
                    //这里开始新增或者升级
                    //4.新增版本表
                    version.PluginCode = Constants.MianName;
                    version.FilePath = Path.Combine(AppConfig.filePath + version.PluginCode, version.PluginCode + "_FG$SP_" + version.VersionName);
                    version.CreateUid = uid;
                    version.LastModUid = uid;
                    if (IsAdd.Equals("1"))
                        version.VersionStatus = 4;
                    else
                        version.VersionStatus = 5;
                    vids = InsertVersionTrack(version).ToString();
                }
                return vids;
            }
            catch (DalException ex)
            {
                throw new BOException("插入主程序版本出错", ex);
            }
        }


        [Frame(true, true)]
        public virtual void UpdateMainTempPlugin(IList<ConfigTemp> listc, string uid)
        {
            try
            {
                SearchConfig searchc = new SearchConfig();
                searchc.PluginCode = Constants.MianName;
                configTempDao.DeleteInfo(searchc);
                if (listc != null)
                {
                    if (listc.Count > 0)
                        InserConfigInfo(listc, Constants.MianName);//更新配置
                }
            }
            catch (DalException ex)
            {
                throw new BOException("新增插件扩展信息出错", ex);
            }
        }



        [Frame(true, true)]
        public virtual void UpdateMainPlushVersionTracks(XmlMainConfigInfo xml, string vid, string uid)
        {
            try
            {
                SearchConfig search = new SearchConfig();
                search.PluginCode = Constants.MianName;
                configInfoDao.DeleteConfigInfo(search);//先删除原由真实参数表数据

                IList<ConfigTemp> listtemp = configTempDao.GetConfigList(search);
                //没修改基本信息，直接发布,需要覆盖
                foreach (ConfigTemp c in listtemp)
                {
                    SearchConfig searchcc = new SearchConfig();
                    searchcc.PluginCode = Constants.MianName;
                    searchcc.ConfigCategoryCode = c.ConfigCategoryCode;
                    searchcc.key = c.Key1;
                    IList<ConfigInfo> listct = configInfoDao.GetConfigList(searchcc);
                    if (listct.Count > 0)
                        configInfoDao.Update(CommonMethods.ConvertToConfigInfo(c));
                    else
                        configInfoDao.Insert(CommonMethods.ConvertToConfigInfo(c));
                }

                configTempDao.DeleteInfo(search);//删除当前插件临时表数据

                //更新原由版本为过期,当前版本为正在使用
                SearchVersionTrack searchtemps = new SearchVersionTrack();
                searchtemps.VID = vid;
                VersionTrack vsss = versionTrackDao.GetVersionTrackList(searchtemps)[0];//当前版本
                VersionTrack oldver = versionTrackDao.Get(vsss.PreVersionId);//获取上个版本即正在使用的版本
                if (oldver != null)
                {
                    oldver.VersionStatus = 2;
                    versionTrackDao.Update(oldver);
                }
                vsss.VersionStatus = 1;
                versionTrackDao.Update(vsss);
            }
            catch (DalException ex)
            {
                throw new BOException("修改主程序版本信息出错", ex);
            }
        }


        [Frame(true, true)]
        public virtual void UpdateMainConfigInfos(IList<ConfigInfo> list)
        {
            try
            {
                SearchConfig search = new SearchConfig();
                search.PluginCode = Constants.MianName;
                configInfoDao.DeleteConfigInfo(search);
                //configInfoPCDao.DeleteConfigInfo(search);
                foreach (ConfigInfo c in list)
                {
                    if (string.IsNullOrEmpty(c.Key1))
                        throw new BOException("配置信息中的健值不能为空");
                    SearchConfig searchc = new SearchConfig();
                    searchc.key = c.Key1;
                    searchc.PluginCode = c.PluginCode;
                    searchc.ConfigCategoryCode = c.ConfigCategoryCode;

                    if (searchc.ConfigCategoryCode != "PCGlobalConfig")
                    {
                        IList<ConfigInfo> listc = configInfoDao.GetConfigList(searchc);
                        if (listc != null)
                        {
                            if (listc.Count > 0)
                                throw new BOException("单个插件的关键值不能重复");
                        }
                        configInfoDao.Insert(c);
                    }
                    else
                    {
                        //插入ConfigInfoPC表
                        IList<ConfigInfo> listc = configInfoDao.GetConfigList(searchc);
                        if (listc != null)
                        {
                            if (listc.Count > 0)
                                throw new BOException("单个插件的关键值不能重复");
                        }
                        ConfigInfo pc = new ConfigInfo();
                        pc.ConfigCategoryCode = c.ConfigCategoryCode;
                        pc.Key1 = c.Key1;
                        pc.Value1 = c.Value1;
                        pc.OldValue = c.OldValue;
                        pc.PluginCode = c.PluginCode;
                        pc.UserUId = c.UserUId;
                        pc.Summary = c.Summary;

                        configInfoDao.Insert(pc);
                    }
                }

            }
            catch (DalException ex)
            {
                throw new BOException("更新配置信息出错", ex);
            }
        }

        [Frame(true, true)]
        public virtual void UpdateMainConfigInfoPCs(IList<ConfigInfoPC> list)
        {
            try
            {
                ConfigInfoPCDao configInfoDao = new ConfigInfoPCDao(AppConfig.mainDbKey);
                SearchConfig search = new SearchConfig();
                search.PluginCode = Constants.MianName;
                //configInfoDao.DeleteConfigInfo(search);
                configInfoPCDao.DeleteConfigInfo(search);
                foreach (ConfigInfoPC c in list)
                {
                    if (string.IsNullOrEmpty(c.Key1))
                        throw new BOException("配置信息中的健值不能为空");
                    SearchConfig searchc = new SearchConfig();
                    searchc.key = c.Key1;
                    searchc.PluginCode = c.PluginCode;
                    searchc.ConfigCategoryCode = c.ConfigCategoryCode;

                    if (searchc.ConfigCategoryCode != "PCGlobalConfig")
                    {
                        IList<ConfigInfoPC> listc = configInfoDao.GetConfigList(searchc);
                        if (listc != null)
                        {
                            if (listc.Count > 0)
                                throw new BOException("单个插件的关键值不能重复");
                        }
                        configInfoDao.Insert(c);
                    }
                    else
                    {
                        //插入ConfigInfoPC表
                        IList<ConfigInfoPC> listc = configInfoPCDao.GetConfigList(searchc);
                        if (listc != null)
                        {
                            if (listc.Count > 0)
                                throw new BOException("单个插件的关键值不能重复");
                        }
                        ConfigInfoPC pc = new ConfigInfoPC();
                        pc.ConfigCategoryCode = c.ConfigCategoryCode;
                        pc.Key1 = c.Key1;
                        pc.Value1 = c.Value1;
                        pc.OldValue = c.OldValue;
                        pc.PluginCode = c.PluginCode;
                        pc.UserUId = c.UserUId;
                        pc.Summary = c.Summary;

                        configInfoPCDao.Insert(pc);
                    }
                }

            }
            catch (DalException ex)
            {
                throw new BOException("更新配置信息出错", ex);
            }
        }



        #endregion

        #region 升级程序管理

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryUpdaterInfo(PageView view)
        {
            try
            {
                return versionTrackDao.QueryUpdaterInfo(view);
            }
            catch (DalException ex)
            {
                throw new BOException("获取升级程序相关所有信息出错", ex);
            }
        }



        [Frame(true, true)]
        public virtual string SaveUpdaterVerInfo(VersionTrack version, string IsAdd, string IsUpdate, string uid, string Vid)
        {
            try
            {
                string vids = "";
                SearchVersionTrack search = new SearchVersionTrack();
                if (!string.IsNullOrEmpty(IsUpdate))//若为修改
                {
                    search.VID = Vid;
                    //1.修改版本表信息
                    VersionTrack v = versionTrackDao.GetVersionTrackList(search)[0];
                    v.VersionSummary = version.VersionSummary;
                    versionTrackDao.Update(v);
                    vids = v.VersionId.ToString();
                }
                else
                {
                    //这里开始新增或者升级
                    //4.新增版本表
                    version.PluginCode = Constants.UpdaterCode;
                    version.FilePath = AppConfig.filePath + Path.Combine(version.PluginCode, version.PluginCode + "_FG$SP_" + version.VersionName);
                    version.CreateUid = uid;
                    version.LastModUid = uid;
                    if (IsAdd.Equals("1"))
                        version.VersionStatus = 4;
                    else
                        version.VersionStatus = 5;
                    vids = InsertVersionTrack(version).ToString();
                }
                return vids;
            }
            catch (DalException ex)
            {
                throw new BOException("插入主程序版本出错", ex);
            }
        }


        //  [Frame(true, true)]
        public virtual string SaveUpdaterZipInfo(VersionTrack version, XmlMainConfigInfo xmlInfo, string IsAdd, string IsUpdate, string uid, string vids)
        {
            string oldfp = "";
            string oldname = "";
            SearchVersionTrack search = new SearchVersionTrack();
            try
            {
                search.VID = vids;
                VersionTrack v = versionTrackDao.GetVersionTrackList(search)[0];
                //删除
                if (!string.IsNullOrEmpty(IsUpdate))//若为修改
                {
                    if (IsUpdate.Equals("1"))//若已有上传文件
                    {
                        string[] codes = v.FilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                        oldfp = v.FilePath;
                        oldname = codes[codes.Length - 1];//版本名
                        SearchConfig searchc = new SearchConfig();
                        searchc.PluginCode = Constants.UpdaterCode;
                        configTempDao.DeleteInfo(searchc);
                    }
                }
                //更新版本状态
                if (IsAdd.Equals("1"))
                    v.VersionStatus = 3;
                else
                    v.VersionStatus = 0;
                versionTrackDao.Update(v);
                //转换
                IList<ConfigTemp> listts = new List<ConfigTemp>();
                foreach (ConfigInfoPC c in xmlInfo.configList)
                {
                    listts.Add(CommonMethods.ConvertToConfigInfoPC(c));
                }
                InserConfigInfo(listts, Constants.UpdaterCode);//新赠插件配置临时表

                if (Directory.Exists(oldfp)) //5.需要删除原由文件夹
                    Directory.Delete(oldfp, true);
                //需要删除存在的位置的原由文件
                FileInfo f = new FileInfo(Path.Combine(AppConfig.SaveZipPath, oldname + ".zip"));
                if (f.Exists)
                    f.Delete();

                return vids;
            }
            catch (DalException ex)
            {
                throw new BOException("上传插件zip文件出错", ex);
            }
        }


        [Frame(true, true)]
        public virtual void UpdateUpdaterTempPlugin(IList<ConfigTemp> listc, string uid)
        {
            try
            {
                SearchConfig searchc = new SearchConfig();
                searchc.PluginCode = Constants.UpdaterCode;
                configTempDao.DeleteInfo(searchc);
                if (listc != null)
                {
                    if (listc.Count > 0)
                        InserConfigInfo(listc, Constants.UpdaterCode);//更新配置
                }
            }
            catch (DalException ex)
            {
                throw new BOException("新增插件扩展信息出错", ex);
            }
        }


        [Frame(true, true)]
        public virtual void UpdateUpdaterPlushVersionTracks(XmlMainConfigInfo xml, string vid, string uid)
        {
            try
            {
                SearchConfig search = new SearchConfig();
                search.PluginCode = Constants.UpdaterCode;
                configInfoDao.DeleteConfigInfo(search);//先删除原由真实参数表数据

                IList<ConfigTemp> listtemp = configTempDao.GetConfigList(search);
                //没修改基本信息，直接发布,需要覆盖
                foreach (ConfigTemp c in listtemp)
                {
                    SearchConfig searchcc = new SearchConfig();
                    searchcc.PluginCode = Constants.UpdaterCode;
                    searchcc.ConfigCategoryCode = c.ConfigCategoryCode;
                    searchcc.key = c.Key1;
                    IList<ConfigInfo> listct = configInfoDao.GetConfigList(searchcc);
                    if (listct.Count > 0)
                        configInfoDao.Update(CommonMethods.ConvertToConfigInfo(c));
                    else
                        configInfoDao.Insert(CommonMethods.ConvertToConfigInfo(c));
                }

                configTempDao.DeleteInfo(search);//删除当前插件临时表数据

                //更新原由版本为过期,当前版本为正在使用
                SearchVersionTrack searchtemps = new SearchVersionTrack();
                searchtemps.VID = vid;
                VersionTrack vsss = versionTrackDao.GetVersionTrackList(searchtemps)[0];//当前版本
                VersionTrack oldver = versionTrackDao.Get(vsss.PreVersionId);//获取上个版本即正在使用的版本
                if (oldver != null)
                {
                    oldver.VersionStatus = 2;
                    versionTrackDao.Update(oldver);
                }
                vsss.VersionStatus = 1;
                versionTrackDao.Update(vsss);
            }
            catch (DalException ex)
            {
                throw new BOException("修改主程序版本信息出错", ex);
            }
        }



        [Frame(true, true)]
        public virtual void UpdateUpdaterConfigInfos(IList<ConfigInfo> list)
        {
            try
            {
                SearchConfig search = new SearchConfig();
                search.PluginCode = Constants.UpdaterCode;
                configInfoDao.DeleteConfigInfo(search);
                foreach (ConfigInfo c in list)
                {
                    if (string.IsNullOrEmpty(c.Key1))
                        throw new BOException("配置信息中的健值不能为空");
                    SearchConfig searchc = new SearchConfig();
                    searchc.key = c.Key1;
                    searchc.PluginCode = c.PluginCode;
                    searchc.ConfigCategoryCode = c.ConfigCategoryCode;
                    IList<ConfigInfo> listc = configInfoDao.GetConfigList(searchc);
                    if (listc != null)
                    {
                        if (listc.Count > 0)
                            throw new BOException("单个插件的关键值不能重复");
                    }
                    configInfoDao.Insert(c);
                }

            }
            catch (DalException ex)
            {
                throw new BOException("更新配置信息出错", ex);
            }
        }


        #endregion

        #region 日志查看

        public virtual JsonFlexiGridData GetLogInfoList(PageView view)
        {
            try
            {
                return logDao.GetLogInfoList(view);
            }
            catch (DalException ex)
            {
                throw new BOException("查询日志出错", ex);
            }
        }

        public virtual LogInfo GetLogById(string id)
        {
            try
            {
                return logDao.Get(id);
            }
            catch (DalException ex)
            {
                throw new BOException("根据ID查询日志出错", ex);
            }
        }

        #endregion

        #region 用户摘要

        public virtual JsonFlexiGridData GetUserInfo(PageView view, SearchConfig search)
        {
            try
            {
                return userInfoDao.QueryUserInfoByPId(search, view);
            }
            catch (DalException ex)
            {
                throw new BOException("根据条件查询用户摘要出错", ex);
            }
        }

        #endregion

        #region 本地用户登陆

        [Frame(false, false)]
        public virtual bool CheckUserName(string uid, string pwd)
        {
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings["PwdEncrypted"].Equals("true", StringComparison.CurrentCultureIgnoreCase)) ;
                {
                    pwd = BeyondbitCrypto.Encryptor(pwd);
                }
                return userInfoDao.CheckUserName(uid, pwd);
            }
            catch (DalException ex)
            {
                throw new BOException("检查用户出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool IsSystemManager(string uid)
        {
            try
            {
                return userInfoDao.IsSystemManager(uid);
            }
            catch (DalException ex)
            {
                throw new BOException("检查用户出错", ex);
            }
        }

        #endregion

        #region 应用管理
        #region 应用管理
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryApplicationList(PageView view, string unitcode)
        {
            return applicationDao.QueryApplicationList(view, unitcode);
        }

        [Frame(false, false)]
        public virtual IList<Application> QueryApplicationList()
        {
            return applicationDao.QueryApplicationList();
        }

        [Frame(false, false)]
        public virtual Application GetApplication(string id)
        {
            return applicationDao.Get(id);
        }

        [Frame(false, true)]
        public virtual void InsertApplication(Application app)
        {
            applicationDao.Insert(app);
        }

        [Frame(false, true)]
        public virtual void UpdateApplication(Application app)
        {
            applicationDao.Update(app);
        }

        [Frame(false, true)]
        public virtual void DeleteApplication(string id)
        {
            DeleteApplicationByID(id);
        }

        [Frame(false, true)]
        public virtual void DeleteApplication(string ids, char separator)
        {
            foreach (string id in ids.Split(separator))
            {
                DeleteApplicationByID(id);
            }
        }

        private void DeleteApplicationByID(string id)
        {
            Application app = applicationDao.Get(id);
            if (app == null)
            {
                throw new Exception("未找到指定的应用");
            }
            if (app4AIDao.HasApp4AI(id))
            {
                throw new Exception("该应用下还有关联的扩展应用,不能删除");
            }
            if (webApplicationDao.HasWebApplication(id))
            {
                throw new Exception("该应用下还有关联的Web应用,不能删除");
            }

            if (app.PrivilegeID != null && app.PrivilegeID.HasValue)
            {
                List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                pars.Add(new KeyValuePair<string, object>("id", app.PrivilegeID.Value));
                AppPrivilege appPrivilege = appPrivilegeDao.Get(pars);


                List<Tuple<string, string, object>> pars2 = new List<Tuple<string, string, object>>();
                pars2.Add(new Tuple<string, string, object>("id", "=", appPrivilege.ID));
                List<PrivilegeUser> pulist = privilegeUserDao.QueryList((pars2));
                if (pulist != null && pulist.Count > 0)
                {
                    foreach (PrivilegeUser pu in pulist)
                    {
                        privilegeUserDao.Delete(pu);
                    }
                }
                app.PrivilegeID = null;
                applicationDao.Update(app);
                appPrivilegeDao.Delete(appPrivilege);
            }
            applicationDao.Delete(app);
        }
        #endregion

        #region 权限管理
        [Frame(false, false)]
        public virtual IList<AppPrivilege> QueryAppPrivilegeList()
        {
            return appPrivilegeDao.QueryAppPrivilegeList();
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryAppPrivilegeList(PageView view)
        {
            return appPrivilegeDao.QueryAppPrivilegeList(view);
        }

        //查询权限用户
        [Frame(false, false)]
        public virtual SplitPageResult<PrivilegeUser> QueryPrivilegeUser(string privilegeCode, int pageSize, int pageIndex)
        {
            return appPrivilegeDao.QueryPrivilegeUser(privilegeCode, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual AppPrivilege GetAppPrivilege(string id)
        {
            return appPrivilegeDao.Get(id);
        }

        [Frame(false, true)]
        public virtual void InsertAppPrivilege(AppPrivilege privilege)
        {
            appPrivilegeDao.Insert(privilege);
        }

        [Frame(false, true)]
        public virtual void UpdateAppPrivilege(AppPrivilege privilege)
        {
            appPrivilegeDao.Update(privilege);
        }

        [Frame(false, true)]
        public virtual void DeleteAppPrivilege(string id)
        {
            DeleteAppPrivilegeByID(id);
        }

        [Frame(false, true)]
        public virtual void DeleteAppPrivilege(string ids, char separator)
        {
            foreach (string id in ids.Split(separator))
            {
                DeleteAppPrivilegeByID(id);
            }
        }

        private void DeleteAppPrivilegeByID(string id)
        {
            AppPrivilege privilege = appPrivilegeDao.Get(id);
            if (privilege == null)
            {
                throw new Exception("未找到指定的权限");
            }
            if (applicationDao.HasApplication(id))
            {
                throw new Exception("有应用分配了该权限,不能删除");
            }
            if (privilegeUserDao.ExitFk(id))
            {
                throw new Exception("有用户分配了该权限,不能删除");
            }
            appPrivilegeDao.Delete(privilege);
        }

        [Frame(false, true)]
        public virtual void AsyncPrivilege(string privilegeCode, List<string> uids, string createUid)
        {
            privilegeUserDao.RemoveAsyncPrivilege(privilegeCode);
            var privilegeUserList = new List<PrivilegeUser>();
            foreach (string uid in uids)
            {
                //privilegeUserList.Add(new PrivilegeUser()
                //{
                //    ID = Convert.ToInt32(privilegeCode),
                //    Uid = uid,
                //    CreateUid = createUid,
                //    UpdateUid = createUid,
                //    CreateTime = DateTime.Now,
                //    UpdateTime = DateTime.Now
                //});
                IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                pars.Add(new KeyValuePair<string, object>("id", Convert.ToInt32(privilegeCode)));
                pars.Add(new KeyValuePair<string, object>("uid", uid));

                PrivilegeUser _pu = privilegeUserDao.Get(pars);
                if (_pu == null)
                {
                    PrivilegeUser pu = new PrivilegeUser();
                    pu.ID = Convert.ToInt32(privilegeCode);
                    pu.Uid = uid;
                    pu.CreateUid = createUid;
                    pu.UpdateUid = createUid;
                    pu.CreateTime = DateTime.Now;
                    pu.UpdateTime = DateTime.Now;
                    privilegeUserDao.Insert(pu);
                }
            }

            //privilegeUserDao.InsertList(privilegeUserList);
        }

        #endregion

        #region 分类管理
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryApplicationCategoryList(PageView view)
        {
            return applicationCategoryDao.QueryApplicationCategoryList(view);
        }

        [Frame(false, false)]
        public virtual IList<ApplicationCategory> QueryApplicationCategoryList()
        {
            return applicationCategoryDao.QueryApplicationCategoryList();
        }

        [Frame(false, false)]
        public virtual ApplicationCategory GetApplicationCategory(string id)
        {
            return applicationCategoryDao.Get(id);
        }

        [Frame(false, true)]
        public virtual void InsertApplicationCategory(ApplicationCategory category)
        {
            applicationCategoryDao.Insert(category);
        }

        [Frame(false, true)]
        public virtual void UpdateApplicationCategory(ApplicationCategory category)
        {
            applicationCategoryDao.Update(category);
        }

        [Frame(false, true)]
        public virtual void DeleteApplicationCategory(string id)
        {
            ApplicationCategory category = applicationCategoryDao.Get(id);
            if (category == null)
            {
                throw new Exception("未找到指定的分类");
            }
            applicationCategoryDao.Delete(category);
        }
        #endregion

        #region Application扩展
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPackage4AIList(PageView view)
        {
            return package4AIDao.QueryPackage4AIList(view);
        }

        [Frame(false, false)]
        public virtual Package4AI GetPackage4AI(string id)
        {
            Package4AI package = package4AIDao.Get(id);
            if (package == null)
            {
                //throw new Exception("未找到指定的安装包");
                Log4NetHelper.Info("在表Package4AI里未找到指定的安装包，id为：" + id);
            }
            else
            {
                List<App4AI> app4AIList = app4AIDao.QueryApp4AIListByPackageID(id).ToList<App4AI>();
                foreach (App4AI app4AI in app4AIList)
                {
                    app4AI.ActionList = action4AndroidDao.QueryAction4AndroidListByApp4AIID(app4AI.ID.ToString()).ToList();
                }
                package.App4AIList = app4AIList;
            }
            return package;
        }

        [Frame(false, true)]
        public virtual App4AI GetApp4AI(string package4aiid)
        {
            return app4AIDao.GetApp4AI(package4aiid);
        }

        [Frame(false, true)]
        public virtual void InsertPackage4AI(Package4AI package4AI, string tempFilePath, string saveFilePath)
        {
            string debug_mode = System.Configuration.ConfigurationSettings.AppSettings["debug_mode"] ?? "false";
            if (debug_mode.Equals("false", StringComparison.CurrentCultureIgnoreCase) && package4AIDao.Exists(package4AI))
                throw new Exception("不能上传重复的安装包!");

            //插入安装包数据
            package4AIDao.Insert(package4AI);
            //给安装包的Application对象赋值
            package4AI.App4AIList.ForEach(x =>
            {
                x.Package4AIID = package4AI.ID;
                x.PackageName = package4AI.Name;
            });

            package4AI.App4AIList.ForEach(app4ai =>
            {
                //插入Application
                app4AIDao.Insert(app4ai);
                //给Application的Action赋值
                app4ai.ActionList.ForEach(action => action.App4AIID = app4ai.ID);

                //插入Action
                //action4AndroidDao.InsertList(app4ai.ActionList);
                if (app4ai.ActionList != null && app4ai.ActionList.Count > 0)
                    foreach (Action4Android a4a in app4ai.ActionList)
                    {
                        Action4Android _a4a = action4AndroidDao.Get(a4a.Name);
                        if (_a4a != null)
                        {
                            action4AndroidDao.Update(a4a);
                        }
                        else
                        {
                            action4AndroidDao.Insert(a4a);
                        }
                    }
            });

            if (!System.IO.File.Exists(tempFilePath))
            {
                throw new Exception("未找到上传的安装包,请重新上传者联系管理员");
            }
            int dollar = saveFilePath.IndexOf('$');
            if (dollar == -1)//本地地址
            {
                if (!string.IsNullOrEmpty(saveFilePath))
                {
                    string dirPath = Path.GetDirectoryName(saveFilePath);
                    if (!System.IO.Directory.Exists(dirPath))
                    {
                        System.IO.Directory.CreateDirectory(dirPath);
                    }
                    System.IO.File.Copy(tempFilePath, saveFilePath, true);
                }

                //if (System.IO.File.Exists(saveFilePath))
                //{
                //    System.IO.File.Delete(saveFilePath);
                //}
                //System.IO.File.Copy(tempFilePath, saveFilePath);
            }
            else
            {//远程地址
                try
                {
                    string configPath = saveFilePath;
                    dollar = dollar + 1;

                    string path = "";
                    path = configPath.Remove(0, dollar);
                    string fileSaveName = Path.GetFileName(path);

                    string deployUrl = configPath.Remove(dollar - 1);
                    string deployPath = Path.GetDirectoryName(path) + '\\';

                    string filePath = tempFilePath;
                    string boundary = "--------------------------" + DateTime.Now.Ticks.ToString("x");
                    WebRequest request = WebRequest.Create(deployUrl);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    request.Method = "POST";
                    request.ContentType = "multipart/form-data; boundary=" + boundary;

                    FileStream fs = new FileStream(filePath, FileMode.Open);
                    byte[] buffur = new byte[fs.Length];
                    try
                    {
                        fs.Read(buffur, 0, (int)fs.Length);
                    }
                    catch (Exception ex)
                    {
                        ;
                    }
                    finally
                    {
                        if (fs != null)
                        {
                            fs.Close();
                        }
                    }

                    MemoryStream stream = new MemoryStream();
                    byte[] line = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                    string format = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";
                    //把文件名写入流
                    string s = string.Format(format, "SaveName", fileSaveName);
                    byte[] fdata = Encoding.UTF8.GetBytes(s);
                    stream.Write(fdata, 0, fdata.Length);
                    //把保存地址写入流
                    s = string.Format(format, "SavePath", deployPath);
                    fdata = Encoding.UTF8.GetBytes(s);
                    stream.Write(fdata, 0, fdata.Length);

                    stream.Write(line, 0, line.Length);

                    string fformat = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
                    s = string.Format(fformat, filePath, Path.GetFileName(filePath));
                    fdata = Encoding.UTF8.GetBytes(s);
                    stream.Write(fdata, 0, fdata.Length);
                    stream.Write(buffur, 0, buffur.Length);
                    //stream.Write(line, 0, line.Length);
                    request.ContentLength = stream.Length;

                    Stream requestStream = request.GetRequestStream();
                    stream.Position = 0L;
                    stream.WriteTo(requestStream);

                    stream.Close();
                    requestStream.Close();

                    // Get the response.
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    // Display the status.
                    //Console.WriteLine(response.StatusDescription);
                    // Get the stream containing content returned by the server.
                    Stream dataStream = response.GetResponseStream();


                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    //Console.WriteLine(responseFromServer);
                    // Cleanup the streams and the response.
                    reader.Close();
                    dataStream.Close();
                    response.Close();



                }
                catch (Exception ECopy)
                {
                    ECopy.ToString();
                }
            }
            System.IO.File.Delete(tempFilePath);
        }

        [Frame(false, true)]
        public virtual void UpdatePackage4AI(Package4AI package4AI)
        {
            package4AIDao.Update(package4AI);
            app4AIDao.UpdateList(package4AI.App4AIList);
            package4AI.App4AIList.ForEach(x => action4AndroidDao.UpdateList(x.ActionList));
        }

        [Frame(false, true)]
        public virtual void UpdatePackage(Package4AI package4AI, string tempFilePath, string saveFilePath)
        {
            package4AIDao.Update(package4AI);


            IList<App4AI> delApp4AIList = app4AIDao.QueryApp4AIListByPackageID(package4AI.ID.ToString());

            //=================================================================================
            //musictom 2014-05-17
            if (delApp4AIList.Count > 0)
            {
                //将同一应用的相同客户端类型的安装包都干掉
                string appId = "";
                if (delApp4AIList[0].AppID.HasValue)
                    appId = delApp4AIList[0].AppID.Value.ToString();

                if (delApp4AIList[0].AppID == null && delApp4AIList[0].Package4AIID > 0)
                {
                    IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                    pars.Add(new KeyValuePair<string, object>("Package4AIID", delApp4AIList[0].Package4AIID));
                    App4AI app4ai = app4AIDao.Get(pars);
                    if (app4ai != null && app4ai.AppID.HasValue)
                        appId = app4ai.AppID.Value.ToString();
                }
                delApp4AIList = app4AIDao.QueryApp4AIList(package4AI.ClientType, appId);
            }
            //=================================================================================

            if (delApp4AIList != null && delApp4AIList.Count > 0)
            {
                delApp4AIList.ForEach(app4AI =>
                {
                    app4AI.ActionList = action4AndroidDao.QueryAction4AndroidListByApp4AIID(app4AI.ID.ToString()).ToList();
                });
                //删除Action
                //delApp4AIList.ForEach(x => action4AndroidDao.DeleteList(x.ActionList));
                //删除App关联
                //app4AIDao.DeleteList(delApp4AIList);
            }

            //给安装包的Application对象赋值
            package4AI.App4AIList.ForEach(x =>
           {
               x.Package4AIID = package4AI.ID;
               x.PackageName = package4AI.Name;
           });

            package4AI.App4AIList.ForEach(app4ai =>
           {
               //插入Application
               IList<KeyValuePair<string, object>> par = new List<KeyValuePair<string, object>>();
               par.Add(new KeyValuePair<string, object>("ClientType", app4ai.ClientType));
               par.Add(new KeyValuePair<string, object>("AppID", app4ai.AppID));
               par.Add(new KeyValuePair<string, object>("Package4AIID", app4ai.Package4AIID));
               App4AI oldAPP4AI = app4AIDao.Get(par);
               if (oldAPP4AI == null)
                   app4AIDao.Insert(app4ai);
               else
               {
                   app4ai.ID = oldAPP4AI.ID;
                   app4ai.Package4AIID = oldAPP4AI.Package4AIID;
                   app4ai.PackageName = oldAPP4AI.PackageName;
                   app4ai.UpdateTime = oldAPP4AI.UpdateTime;
                   app4ai.UpdateUid = oldAPP4AI.UpdateUid;
                   app4ai.Version = oldAPP4AI.Version;
                   app4ai.AppCode = oldAPP4AI.AppCode;
                   app4ai.AppDisplayName = oldAPP4AI.AppDisplayName;
                   app4ai.AppID = oldAPP4AI.AppID;
                   app4ai.AppName = oldAPP4AI.AppName;
                   app4ai.ClientType = oldAPP4AI.ClientType;
                   app4ai.CreateTime = oldAPP4AI.CreateTime;
                   app4AIDao.Update(app4ai);
               }
               //给Application的Action赋值
               app4ai.ActionList.ForEach(action => action.App4AIID = app4ai.ID);
               //插入Action
               action4AndroidDao.InsertList(app4ai.ActionList);
           });

            if (!System.IO.File.Exists(tempFilePath))
            {
                throw new Exception("未找到上传的安装包,请重新上传者联系管理员");
            }
            if (package4AI.Type.Equals("Main", StringComparison.CurrentCultureIgnoreCase)
                && AppConfig.PublishConfig.ContainsKey(package4AI.ClientType.ToLower()))
            {
                string path = AppConfig.PublishConfig[package4AI.ClientType.ToLower()];
                if (!string.IsNullOrEmpty(path))
                {
                    string configPath = path;
                    int dollar = configPath.IndexOf('$') + 1;

                    path = configPath.Remove(0, dollar);
                    string dirPath = Path.GetDirectoryName(path);
                    if (!System.IO.Directory.Exists(dirPath))
                    {
                        System.IO.Directory.CreateDirectory(dirPath);
                    }
                    System.IO.File.Copy(tempFilePath, path, true);
                }
            }
            if (System.IO.File.Exists(saveFilePath))
            {
                System.IO.File.Delete(saveFilePath);
            }
            System.IO.File.Move(tempFilePath, saveFilePath);
        }

        [Frame(false, true)]
        public virtual void DeletePackage4AI(string id)
        {
            Package4AI package = package4AIDao.Get(id);
            if (package == null)
            {
                throw new Exception("未找到指定的安装包");
            }
            IList<App4AI> app4AIList = app4AIDao.QueryApp4AIListByPackageID(id);
            app4AIList.ForEach(app4AI =>
            {
                app4AI.ActionList = action4AndroidDao.QueryAction4AndroidListByApp4AIID(app4AI.ID.ToString()).ToList();
            });
            //删除Action
            app4AIList.ForEach(x => action4AndroidDao.DeleteList(x.ActionList));
            //删除App关联
            app4AIDao.DeleteList(app4AIList);
            //删除包
            package4AIDao.Delete(package);
        }

        #endregion

        #region Web形式的应用
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryWebApplicationList(SearchWebApplication view)
        {
            return webApplicationDao.QueryWebApplicationList(view);
        }

        [Frame(false, false)]
        public virtual IList<WebApplication> QueryWebApplicationListByAppID(string appID)
        {
            return webApplicationDao.QueryWebApplicationListByAppID(appID);
        }

        [Frame(false, false)]
        public virtual WebApplication GetWebApplication(string id)
        {
            return webApplicationDao.Get(id);
        }

        [Frame(false, true)]
        public virtual void UpdateWebApplication(WebApplication webApplication)
        {
            webApplicationDao.Update(webApplication);
        }

        [Frame(false, true)]
        public virtual void InsertWebApplication(WebApplication webApplication)
        {
            webApplicationDao.Insert(webApplication);
        }

        [Frame(false, true)]
        public virtual void DeleteWebApplication(string id)
        {
            WebApplication webApplication = webApplicationDao.Get(id);
            if (webApplication != null)
            {
                webApplicationDao.Delete(webApplication);
            }
        }

        #endregion

        #region 客户端类型
        [Frame(false, false)]
        public virtual IList<ClientTypes> QueryClientTypeList()
        {
            return clientTypeDao.QueryClientTypeList();
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryClientTypeList(PageView view)
        {
            return clientTypeDao.QueryClientTypeList(view);
        }
        #endregion

        #region Home布局

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryHomePlanList(PageView view)
        {
            return homePlanDao.QueryHomePlanList(view);
        }

        [Frame(false, false)]
        public virtual HomePlan GetHomePlan(string id)
        {
            return homePlanDao.Get(id);
        }

        [Frame(false, false)]
        public virtual IList<HomePlanDesign> QueryHomePlanDesignList(string planID)
        {
            return homePlanDesignDao.QueryHomePlanDesign(planID);
        }

        [Frame(false, true)]
        public virtual void InsertHomePlan(HomePlan homePlan)
        {
            if (homePlan.IsDefault)
            {
                var defaultPlan = homePlanDao.QueryDefaultHomePlan(homePlan.ID.ToString(), string.Empty, homePlan.Format);
                defaultPlan.ForEach((x) => { x.IsDefault = false; });
                homePlanDao.UpdateList(defaultPlan);
            }
            homePlanDao.Insert(homePlan);
        }

        [Frame(false, true)]
        public virtual void InsertHomePlanDesign(HomePlanDesign planDesign)
        {
            homePlanDesignDao.Insert(planDesign);
        }

        [Frame(false, true)]
        public virtual void UpdateHomePlan(HomePlan homePlan)
        {
            if (homePlan.IsDefault)
            {
                var defaultPlan = homePlanDao.QueryDefaultHomePlan(homePlan.ID.ToString(), string.Empty, homePlan.Format);
                defaultPlan.ForEach((x) => { x.IsDefault = false; });
                homePlanDao.UpdateList(defaultPlan);
            }
            homePlanDao.Update(homePlan);
        }

        [Frame(false, true)]
        public virtual void UpdateHomePlanDesignList(List<HomePlanDesign> planDesignList)
        {
            homePlanDesignDao.UpdateList(planDesignList);
        }

        [Frame(false, true)]
        public virtual void DeleteHomePlan(string id)
        {
            HomePlan plan = homePlanDao.Get(id);
            homePlanDao.Delete(plan);
        }

        [Frame(false, true)]
        public virtual void CopyHomePlan(HomePlan copyPlan, string id)
        {
            if (copyPlan.IsDefault)
            {
                var defaultPlan = homePlanDao.QueryDefaultHomePlan(copyPlan.ID.ToString(), string.Empty, copyPlan.Format);
                defaultPlan.ForEach((x) => { x.IsDefault = false; });
                homePlanDao.UpdateList(defaultPlan);
            }
            IList<HomePlanDesign> designs = homePlanDesignDao.QueryHomePlanDesign(id);

            homePlanDao.Insert(copyPlan);
            designs.ForEach(x =>
            {
                x.PlanID = copyPlan.ID;
            });
            homePlanDesignDao.InsertList(designs);
        }

        [Frame(false, true)]
        public virtual void DeleteHomePlanDesign(string planID, string appID)
        {
            HomePlanDesign design = homePlanDesignDao.Get(planID, appID);
            if (design == null)
            {
                throw new Exception("未找到指定的对象");
            }
            homePlanDesignDao.Delete(design);
        }

        [Frame(false, true)]
        public virtual void InterchangeHomePlanDesign(int pageW, int pageH, string app1ID, string app2ID, string planID)
        {
            bool page1Crash = false;
            bool page2Crash = false;
            bool thisCrash = false;
            HomePlanDesign design1 = homePlanDesignDao.Get(planID, app1ID);
            HomePlanDesign design2 = homePlanDesignDao.Get(planID, app2ID);
            int page1Index = Convert.ToInt32(design1.Location.Split(',')[0]) / pageW;
            int page2Index = Convert.ToInt32(design2.Location.Split(',')[0]) / pageW;

            string[] design1Location = design1.Location.Split(',');
            string[] design2Location = design2.Location.Split(',');
            string[] design1Size = design1.Size.Split(',');
            string[] design2Size = design2.Size.Split(',');
            int design1X = Convert.ToInt32(design1Location[0]);
            int design1Y = Convert.ToInt32(design1Location[1]);
            int design1W = Convert.ToInt32(design1Size[0]);
            int design1H = Convert.ToInt32(design1Size[1]);
            int design1Page = design1X / pageW;

            int design2X = Convert.ToInt32(design2Location[0]);
            int design2Y = Convert.ToInt32(design2Location[1]);
            int design2W = Convert.ToInt32(design2Size[0]);
            int design2H = Convert.ToInt32(design2Size[1]);
            int design2Page = design2X / pageW;

            IList<HomePlanDesign> design1PageList = homePlanDesignDao.QueryHomePlanDesignByPage(planID, page1Index, pageW);
            design1PageList.ForEach(f =>
            {
                if (!f.Location.Equals(design1.Location))
                {
                    int tpage = Convert.ToInt32(f.Location.Split(',')[0]) / pageW;
                    int tx = Convert.ToInt32(f.Location.Split(',')[0]) % pageW;
                    int ty = Convert.ToInt32(f.Location.Split(',')[1]);
                    int tw = Convert.ToInt32(f.Size.Split(',')[0]);
                    int th = Convert.ToInt32(f.Size.Split(',')[1]);
                    if (design1Page == tpage && ((design1X >= tx && design1X < tx + tw) || (design1X + design1W > tx && design1X + design1W <= tx + tw))
                                      && ((design1Y >= ty && design1Y < ty + th) || (design1Y + design1H > ty && design1Y + design1H <= ty + th)))
                    {
                        page1Crash = true;
                        return;
                    }
                }
            });

            IList<HomePlanDesign> design2PageList = homePlanDesignDao.QueryHomePlanDesignByPage(planID, page2Index, pageW);
            design2PageList.ForEach(f =>
            {
                if (!f.Location.Equals(design2.Location))
                {
                    int tpage = Convert.ToInt32(f.Location.Split(',')[0]) / pageW;
                    int tx = Convert.ToInt32(f.Location.Split(',')[0]) % pageW;
                    int ty = Convert.ToInt32(f.Location.Split(',')[1]);
                    int tw = Convert.ToInt32(f.Size.Split(',')[0]);
                    int th = Convert.ToInt32(f.Size.Split(',')[1]);
                    if (design2Page == tpage && ((design2X >= tx && design2X < tx + tw) || (design2X + design2W > tx && design2X + design2W <= tx + tw))
                                      && ((design2Y >= ty && design2Y < ty + th) || (design2Y + design2H > ty && design2Y + design2H <= ty + th)))
                    {
                        page2Crash = true;
                        return;
                    }
                }
            });

            if (design2Page == design1Page && ((design2X >= design1X && design2X < design1X + design2W) || (design2X + design1W > design1X && design2X + design1W <= design1X + design2W))
            && ((design2Y >= design1Y && design2Y < design1Y + design2H) || (design2Y + design1H > design1Y && design2Y + design1H <= design1Y + design2H)))
            {
                thisCrash = true;
            }

            if (page1Crash || page2Crash || thisCrash)
            {
                throw new ArgumentException("不能移动");
            }
            string tempLocation = design1.Location;
            design1.Location = design2.Location;
            design2.Location = tempLocation;

            homePlanDesignDao.Update(design1);
            homePlanDesignDao.Update(design2);
        }
        #endregion
        #endregion

        #region 图片

        [Frame(false, false)]
        public virtual System.IO.MemoryStream GetImage(string id)
        {
            Image dbimg = imageDao.Get(id);
            //System.Drawing.Image tempImage;
            //using (MemoryStream stream = new MemoryStream(dbimg.Data))
            //{
            //    tempImage = System.Drawing.Bitmap.FromStream(stream);
            //}
            return new MemoryStream(dbimg.Data);
        }

        [Frame(false, false)]
        public virtual IList<Image> QueryImageList()
        {
            return imageDao.QueryImageList();
        }

        [Frame(false, true)]
        public virtual Image InsertImage(Stream stream)
        {
            List<byte> imgByte = new List<byte>();
            byte[] buffer = new byte[1024];
            while (true)
            {
                int count = stream.Read(buffer, 0, 1024);
                if (count <= 0)
                {
                    break;
                }
                byte[] temp = new byte[count];
                Array.Copy(buffer, temp, count);
                imgByte.AddRange(temp);
            }
            stream.Close();
            stream.Dispose();
            if (imgByte.Count == 0)
            {
                throw new Exception("上传过程中发生错误!");
            }
            Image image = new Image();
            image.Data = imgByte.ToArray();
            //计算image.Data的hashcode
            string hashcode = GetHashCode(imgByte.ToArray());
            image.HashCode = hashcode;

            //查看hashCode是否已存在
            var imageQ = imageDao.QueryByHashCode(image.HashCode);

            if (imageQ.Count > 0)
            {
                image = imageQ[0];
            }
            else
            {
                imageDao.Insert(image);
            }
            return image;

        }

        //获取md5运算code
        private string GetHashCode(byte[] data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] resultEncrypt = md5CSP.ComputeHash(data);

            string hash = BitConverter.ToString(resultEncrypt);
            return hash;
        }


        [Frame(false, true)]
        public virtual void DeleteImage(string id)
        {
            Image image = imageDao.Get(id);
            if (image == null)
            {
                throw new Exception("未找到指定的图片");
            }
            imageDao.Delete(image);
        }

        #endregion

        #region 私有方法

        private XmlMainConfigInfo GetXmlInfoByMain(string xmlpath)
        {
            XmlMainConfigInfo x = new XmlMainConfigInfo();
            IList<ConfigInfoPC> listG = CommonMethods.GetMainEntityFromXMLPC(Path.Combine(xmlpath, Common.Entities.Constants.globalName));
            IList<ConfigInfoPC> listS = CommonMethods.GetMainEntityFromXMLPC(Path.Combine(xmlpath, Common.Entities.Constants.systemName));
            IList<ConfigInfoPC> listA = CommonMethods.GetMainEntityFromXMLPC(Path.Combine(xmlpath, Common.Entities.Constants.AppName));
            foreach (var c in listS)
            {
                listG.Add(c);
            }
            foreach (var c in listA)
            {
                listG.Add(c);
            }
            x.configList = listG;
            return x;
        }

        private XmlMainConfigInfo GetXmlInfoByUpdater(string xmlpath)
        {
            XmlMainConfigInfo x = new XmlMainConfigInfo();
            IList<ConfigInfoPC> listG = CommonMethods.GetMainEntityFromXMLPC(Path.Combine(xmlpath, Common.Entities.Constants.UpdaterName));
            x.configList = listG;
            return x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activeVersion">正在使用的版本</param>
        /// <param name="resumeVersion">恢复的版本</param>
        private void resumeVersionInfo(ArrayList activeVersion, ArrayList resumeVersion, XmlConfigInfo xml)
        {
            try
            {
                string filepaht = "";
                if (activeVersion != null)//若有正在使用的版本(未删除正在使用的版本)
                {
                    foreach (var a in activeVersion)
                    {
                        SearchVersionTrack searchv = new SearchVersionTrack();
                        searchv.VID = a.ToString();
                        VersionTrack v = versionTrackDao.GetVersionTrackList(searchv)[0];
                        v.VersionStatus = 2;
                        versionTrackDao.Update(v);//更新正在使用的版本为过期
                    }
                }
                foreach (var a in resumeVersion)
                {
                    SearchVersionTrack searchv = new SearchVersionTrack();
                    searchv.VID = a.ToString();
                    VersionTrack v = versionTrackDao.GetVersionTrackList(searchv)[0];
                    v.VersionStatus = 1;
                    filepaht = v.FilePath;
                    versionTrackDao.Update(v);//更新正在使用的版本为正在使用
                }
                IList<PluginInfoTemp> plist = xml.PluginInfo;
                //更新除了versiontrack之外的所有表信息
                for (int i = 0; i < plist.Count; i++)
                {
                    SearchConfig search = new SearchConfig();
                    search.PluginCode = plist[i].PluginCode;
                    configInfoDao.DeleteConfigInfo(search);//删除配置

                    ActionExtend action = null;
                    if (plist[i].PluginCateCode.Equals(Constants.ActionCateCode))
                    {
                        actionExtendDao.DelActionExtendInfo(search);//删除action
                        action = new ActionExtend();
                        action.PluginCode = plist[i].PluginCode;
                        action.ActionCode = plist[i].ActionCode;
                        action.Summary = plist[i].ActionSummary;
                    }
                    PluginInfo p = TPluginInfo(plist[i]);//转换
                    p.IsUse = true;//设置使用
                    pluginDao.Delete(p);//删除插件(可能前一个版本是1个插件,当前版本2个插件)
                    ///////////////////////////////////////////////////////////////////////
                    pluginDao.Insert(p);//新增插件
                    foreach (ConfigInfo c in plist[i].configList)
                    {
                        c.ConfigCategoryCode = Constants.configCategory;
                        c.PluginCode = plist[i].PluginCode;
                        configInfoDao.Insert(c);//插入配置
                    }
                    if (plist[i].PluginCateCode.Equals(Constants.ActionCateCode))
                    {
                        InsertActionExtend(action);// 新增action
                    }
                }
                //发布更新
                if (!plist[0].PluginCateCode.Equals(Constants.PluginCateCode))
                {
                    string[] codes = filepaht.Split(new string[] { "\\" }, StringSplitOptions.None);
                    string name = codes[codes.Length - 2];//获取插件code
                    Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                    pub.UpdateApplication(filepaht, name);
                }
            }
            catch (DalException ex)
            {
                throw new BOException("恢复版本出错", ex);
            }
            catch (Exception e)
            {
                throw new BOException("恢复版本出错", e);
            }
        }

        private void resumeVersionInfoByMain(ArrayList activeVersion, ArrayList resumeVersion, XmlMainConfigInfo xml, string code)
        {
            try
            {
                string filepaht = "";
                if (activeVersion != null)//若有正在使用的版本(未删除正在使用的版本)
                {
                    foreach (var a in activeVersion)
                    {
                        SearchVersionTrack searchv = new SearchVersionTrack();
                        searchv.VID = a.ToString();
                        VersionTrack v = versionTrackDao.GetVersionTrackList(searchv)[0];
                        v.VersionStatus = 2;
                        versionTrackDao.Update(v);//更新正在使用的版本为过期
                    }
                }
                foreach (var a in resumeVersion)
                {
                    SearchVersionTrack searchv = new SearchVersionTrack();
                    searchv.VID = a.ToString();
                    VersionTrack v = versionTrackDao.GetVersionTrackList(searchv)[0];
                    v.VersionStatus = 1;
                    filepaht = v.FilePath;
                    versionTrackDao.Update(v);//更新正在使用的版本为正在使用
                }
                IList<ConfigInfoPC> plist = xml.configList;
                //更新除了versiontrack之外的所有表信息
                for (int i = 0; i < plist.Count; i++)
                {
                    SearchConfig search = new SearchConfig();
                    search.PluginCode = code;
                    configInfoDao.DeleteConfigInfo(search);//删除配置
                    foreach (ConfigInfoPC c in plist)
                    {
                        c.PluginCode = code;
                        configInfoPCDao.Insert(c);//插入配置
                    }
                }
                //发布更新
                string[] codes = filepaht.Split(new string[] { "\\" }, StringSplitOptions.None);
                string name = codes[codes.Length - 2];//获取插件code
                Beyondbit.AutoUpdate.IPublisher pub = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                pub.UpdateApplication(filepaht, name);
            }
            catch (DalException ex)
            {
                throw new BOException("恢复版本出错", ex);
            }
            catch (Exception e)
            {
                throw new BOException("恢复版本出错", e);
            }
        }

        private void DelVersionInfo(ArrayList arr)
        {
            try
            {
                string vpath = "";
                foreach (string a in arr)
                {
                    SearchVersionTrack searchv = new SearchVersionTrack();
                    searchv.VID = a;
                    VersionTrack v = versionTrackDao.GetVersionTrackList(searchv)[0];
                    vpath = v.FilePath;
                    versionTrackDao.Delete(v);//删版本
                }
                string[] codes = vpath.Split(new string[] { "\\" }, StringSplitOptions.None);
                string filename = Path.Combine(AppConfig.SaveZipPath, codes[codes.Length - 1] + ".zip");
                FileInfo f = new FileInfo(filename);
                if (f.Exists)
                    f.Delete();//删除zip
                if (Directory.Exists(vpath)) //删除原由文件夹
                    Directory.Delete(vpath, true);

            }
            catch (DalException ex)
            {
                throw new BOException("删除版本出错", ex);
            }
        }

        private XmlConfigInfo GetXmlInfo(string xmlpath)
        {
            XmlConfigInfo x = CommonMethods.GetEntityFromXML(Path.Combine(xmlpath, Common.Entities.Constants.PluginPath));
            x.PluginInfo = x.PluginInfo.OrderBy(T => T.PluginCode).ToList();
            return x;
        }

        private PluginInfo TPluginInfo(PluginInfoTemp plugin)
        {
            PluginInfo p = new PluginInfo();
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
            p.Summary = plugin.PluginSummary;
            p.TypeFullName = plugin.TypeFullName;
            p.Version = plugin.Version;
            p.PluginCode = plugin.PluginCode;
            p.IsNew = plugin.IsNew;
            p.IsUse = plugin.IsUse;
            p.IsIgnoreConfig = plugin.IsIgnoreConfig;
            p.IsPublic = plugin.IsPublic;
            p.AppCode = plugin.AppCode;
            p.PrivilegeCode = plugin.PrivilegeCode;
            return p;
        }

        #endregion

        # region 外部应用

        [Frame(false, true)]
        public virtual void InsertOutPackage(SMC_Package4Out outPackage)
        {
            package4OutDao.Insert(outPackage);
        }

        [Frame(false, true)]
        public virtual SMC_Package4Out GetPackage4Out(string id)
        {
            return package4OutDao.Get(id);
        }

        [Frame(false, true)]
        public virtual void DeleteOutPackage(string id)
        {
            SMC_Package4Out Outpackage = package4OutDao.Get(id);
            if (Outpackage != null)
            {
                package4OutDao.Delete(Outpackage);
            }
        }

        [Frame(false, true)]
        public virtual void DeletePackageExt(string id)
        {
            SMC_PackageExt packageExt = packageExtDao.Get(id);
            if (packageExt != null)
            {
                packageExtDao.Delete(packageExt);
            }
        }

        public virtual int GetMaxOutPackageId()
        {
            return package4OutDao.GetMaxId();
        }

        public virtual int GetMaxPackageExtId()
        {
            return packageExtDao.GetMaxId();
        }

        public virtual int GetMaxPackagePicId()
        {
            return packagePictureDao.GetMaxId();
        }

        public virtual int GetMaxPackageManualId()
        {
            return packageManualDao.GetMaxId();
        }

        public virtual string GetPeId(string tableId, string tableName)
        {
            return packageExtDao.GetPeId(tableId, tableName);
        }

        [Frame(false, false)]
        public virtual SMC_PackageFAQ GetPackageFAQ(string id)
        {
            return packageFAQDao.Get(id);
        }

        [Frame(false, false)]
        public virtual SMC_PackageExt GetPackageExt(string id)
        {
            return packageExtDao.Get(id);
        }

        [Frame(false, false)]
        public virtual SMC_PackageSync GetPackageSync(string id)
        {
            return packageSyncDao.Get(id);
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPackageExtAsyncList(PageView pageview, string unitcode)
        {
            try
            {
                return packageExtDao.QueryPackageExtAsyncList(pageview, unitcode);
            }
            catch (DalException ex)
            {
                throw new BOException("查询包的同步信息出错", ex);
            }
        }



        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPackageExtList(PageView pageview, string clientType, string unitcode)
        {
            try
            {
                return packageExtDao.QueryPackageExtList(pageview, clientType, unitcode);
            }
            catch (DalException ex)
            {
                throw new BOException("查询应用包信息出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryOutPackageList(PageView pageview)
        {
            try
            {
                return package4OutDao.QueryOutPackageList(pageview);
            }
            catch (DalException ex)
            {
                throw new BOException("查询外部应用包信息出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPackageGifList(PageView pageview, string id)
        {
            try
            {
                return package4OutDao.QueryPackageGifList(pageview, id);
            }
            catch (DalException ex)
            {
                throw new BOException("查询外部应用包截图出错", ex);
            }
        }


        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPackageManualList(PageView pageview, string id)
        {
            try
            {
                return packageManualDao.QueryPackageManualList(pageview, id);
            }
            catch (DalException ex)
            {
                throw new BOException("查询外部应用包截图出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPackageFAQList(PageView pageview, string id)
        {
            try
            {
                return packageFAQDao.QueryPackageFAQList(pageview, id);
            }
            catch (DalException ex)
            {
                throw new BOException("查询应用反馈信息出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPackageCollectList(PageView pageview, string id)
        {
            try
            {
                return collectDao.QueryPackageCollectList(pageview, id);
            }
            catch (DalException ex)
            {
                throw new BOException("查询应用收藏信息出错", ex);
            }
        }

        [Frame(false, true)]
        public virtual void UpdatePackageFAQ(SMC_PackageFAQ packageFAQ)
        {
            packageFAQDao.Update(packageFAQ);
            Service.ApplicationCenterWS.WebService ws = new Service.ApplicationCenterWS.WebService();
            Service.ApplicationCenterWS.SMC_PackageFAQ faq = new Service.ApplicationCenterWS.SMC_PackageFAQ();
            faq.pe_id = packageFAQ.pe_id;
            faq.pf_answer = packageFAQ.pf_answer;
            faq.pf_askdate = packageFAQ.pf_askdate;
            faq.pf_askemail = packageFAQ.pf_askemail;
            faq.pf_askmobile = packageFAQ.pf_askmobile;
            faq.pf_id = packageFAQ.pf_id;
            faq.pf_need_syncto_inside = packageFAQ.pf_need_syncto_inside;
            faq.pf_need_syncto_outside = packageFAQ.pf_need_syncto_outside;
            faq.pf_peplyman = packageFAQ.pf_peplyman;
            faq.pf_question = packageFAQ.pf_question;
            faq.pf_uid = packageFAQ.pf_uid;
            faq.pf_uname = packageFAQ.pf_uname;
            //同步问题反馈回复至外网

            ws.PackageFAQSync(faq);
        }

        [Frame(false, true)]
        public virtual void InsertPackageFAQ(SMC_PackageFAQ packageFAQ)
        {
            packageFAQDao.Insert(packageFAQ);
        }

        [Frame(false, true)]
        public virtual IList<SMC_PackageFAQ> GetNeedSyncToOutsideFAQ()
        {
            return packageFAQDao.GetNeedSyncToOutsideFAQ();
        }

        [Frame(false, true)]
        public virtual void InsertPackageExt(SMC_PackageExt packageExt)
        {
            packageExtDao.Insert(packageExt);
        }

        [Frame(false, true)]
        public virtual void InsertOrUpdatePackageSync(SMC_PackageSync packageSync)
        {
            var sync = packageSyncDao.Get(packageSync.pe_id.ToString());
            if (sync == null)
            {
                packageSyncDao.Insert(packageSync);
            }
            else
            {
                packageSyncDao.Update(packageSync);
            }

        }


        [Frame(false, true)]
        public virtual void UpdatePackageExt(SMC_PackageExt packageExt)
        {
            packageExtDao.Update(packageExt);
        }

        [Frame(false, true)]
        public virtual void SetUserfulStatus(string id, string Operation)
        {
            try
            {
                SMC_PackageExt pe = packageExtDao.Get(id);

                if (Operation == "ENABLE")
                {
                    pe.pe_UsefulStstus = "1";
                }
                else
                {
                    pe.pe_UsefulStstus = "0";
                }


                packageExtDao.Update(pe);
            }
            catch (DalException ex)
            {
                throw new BOException("设置应用上架状态出错", ex);
            }
        }


        [Frame(false, true)]
        public virtual void UpdatePackageManual(SMC_PackageManual packageManual)
        {
            packageManualDao.Update(packageManual);
        }

        [Frame(false, true)]
        public virtual void UpdatePackage4Out(SMC_Package4Out package4Out)
        {
            package4OutDao.Update(package4Out);
        }

        [Frame(false, true)]
        public virtual void InsertPackagePicture(SMC_PackagePicture packagePic)
        {
            packagePictureDao.Insert(packagePic);
        }

        [Frame(false, true)]
        public virtual void InsertPackageManual(SMC_PackageManual packageManual)
        {
            packageManualDao.Insert(packageManual);
        }

        [Frame(false, true)]
        public virtual void DeletePackageCollectByPEID(string id)
        {
            collectDao.DeleteByPEID(id);
        }

        [Frame(false, true)]
        public virtual void DeletePackageFAQByPEID(string id)
        {
            packageFAQDao.DeleteByPEID(id);
        }

        [Frame(false, true)]
        public virtual void DeletePackageManual(string id)
        {
            SMC_PackageManual packageManual = packageManualDao.Get(id);
            if (packageManual == null)
            {
                throw new Exception("未找到指定的手册");
            }
            packageManualDao.Delete(packageManual);
        }

        [Frame(false, true)]
        public virtual void DeletePackagePicture(string id)
        {
            SMC_PackagePicture packagePic = packagePictureDao.Get(id);
            if (packagePic == null)
            {
                throw new Exception("未找到指定的截图");
            }
            packagePictureDao.Delete(packagePic);
        }

        [Frame(false, false)]
        public virtual SMC_PackagePicture GetPackagePicture(string id)
        {
            return packagePictureDao.Get(id);
        }

        [Frame(false, false)]
        public virtual SMC_PackageManual GetPackageManual(string id)
        {
            return packageManualDao.Get(id);
        }

        #endregion


        #region 主程序管理



        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryVersionTrackInfo(PageView pageview, SearchVersionTrack search)
        {
            try
            {
                return versionTrackDao.QueryVersionTrackInfo(pageview, search);
            }
            catch (DalException ex)
            {
                throw new BOException("查询主程序版本列表信息出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual IList<VersionTrack> GetVersionTrack(SearchVersionTrack search)
        {
            try
            {
                return versionTrackDao.GetVersionTrackList(search);
            }
            catch (DalException ex)
            {
                throw new BOException("查询主程序版本信息出错", ex);
            }
        }

        [Frame(true, true)]
        public virtual void UpdateVersionTrack(VersionTrack ver)
        {
            try
            {
                IList<VersionTrack> ck = versionTrackDao.CheckVersionName(ver.PluginCode, ver.VersionName);
                if (ck != null)
                {
                    if (ck.Count > 0 && ck[0].VersionStatus != 0)//有记录，。但是不是未发布的版本，是已有的版本
                        throw new BOException("该主程序或者插件的版本号已经存在！");
                }
                versionTrackDao.Update(ver);
            }
            catch (DalException ex)
            {
                throw new BOException("修改主程序版本信息出错", ex);
            }
        }


        [Frame(true, true)]
        public virtual void UpdatePlushVersionTrack(VersionTrack ver)
        {
            try
            {
                VersionTrack oldver = versionTrackDao.Get(ver.PreVersionId);//获取上个版本即正在使用的版本
                if (oldver != null)
                {
                    oldver.VersionStatus = 2;
                    versionTrackDao.Update(oldver);
                }
                versionTrackDao.Update(ver);
            }
            catch (DalException ex)
            {
                throw new BOException("修改主程序版本信息出错", ex);
            }
        }

        public int InsertUpdateVersionTrack(VersionTrack ver)
        {
            try
            {
                if (string.IsNullOrEmpty(ver.VersionName))
                    throw new BOException("版本号不能为空！");
                SearchVersionTrack search = new SearchVersionTrack();
                search.PluginCode = ver.PluginCode.ToString();
                search.VersionStatus = "1";
                IList<VersionTrack> preid = versionTrackDao.GetVersionTrackList(search);//获取正在使用的版本
                if (preid != null)
                {
                    if (preid.Count > 0)
                        ver.PreVersionId = preid[0].VersionId;
                    else
                        ver.PreVersionId = 0;
                }

                ver.LastModTime = DateTime.Now;
                ver.CreateTime = DateTime.Now;

                //#region 2.0.1.0
                ////重复就更新版本(上传之后,不做版本号修改,因为发布时候会根据临时表中的插件code和版本号去查,再更新,改版本后会找不到次版本信息)
                //IList<VersionTrack> ck = versionTrackDao.CheckVersionName(ver.PluginCode, ver.VersionName);
                //if (ck != null)
                //{
                //    if (ck.Count > 0)
                //    {
                //        ck[0].PluginCode = ver.PluginCode;
                //        ck[0].FilePath = ver.FilePath;
                //        ck[0].VersionName = ver.VersionName;
                //        ck[0].LastModUid = ver.LastModUid;
                //        ck[0].LastModTime = ver.LastModTime;
                //        ck[0].VersionStatus = ver.VersionStatus;
                //        ck[0].VersionSummary = ver.VersionSummary;
                //        versionTrackDao.Update(ck[0]);
                //        return ck[0].VersionId;
                //    }
                //}
                //#endregion

                return versionTrackDao.Insert(ver);
            }
            catch (DalException ex)
            {
                throw new BOException("新增主程序版本信息出错", ex);
            }
        }

        public int InsertVersionTrack(VersionTrack ver)
        {
            try
            {
                if (string.IsNullOrEmpty(ver.VersionName))
                    throw new BOException("版本号不能为空！");
                IList<VersionTrack> ck = versionTrackDao.CheckVersionName(ver.PluginCode, ver.VersionName);
                if (ck != null)
                {
                    if (ck.Count > 0)
                        throw new BOException("该主程序或者插件的版本号已经存在！");
                }
                SearchVersionTrack search = new SearchVersionTrack();
                search.PluginCode = ver.PluginCode.ToString();
                search.VersionStatus = "1";
                IList<VersionTrack> preid = versionTrackDao.GetVersionTrackList(search);//获取正在使用的版本
                if (preid != null)
                {
                    if (preid.Count > 0)
                        ver.PreVersionId = preid[0].VersionId;
                    else
                        ver.PreVersionId = 0;
                }
                ver.CreateTime = DateTime.Now;
                ver.LastModTime = DateTime.Now;

                return versionTrackDao.Insert(ver);
            }
            catch (DalException ex)
            {
                throw new BOException("新增主程序版本信息出错", ex);
            }
        }

        #endregion

        #region 插件管理



        //更新或者升级插件
        [Frame(true, true)]
        public virtual void UpdatePluginInfo(PluginInfo pInfo, ActionExtend action)
        {
            try
            {
                SearchConfig searchconfig = new SearchConfig();
                searchconfig.PluginCode = pInfo.PluginCode.ToString();
                IList<ActionExtend> listAction = BoFactory.GetVersionTrackBo.QueryActionExtend(searchconfig);

                if (!string.IsNullOrEmpty(action.ActionCode))//判断是否输入标识
                {
                    if (listAction.Count > 0)//若存在记录，即更新
                        UpdateActionExtend(action);
                    else
                        InsertActionExtend(action);
                }
                else
                    DelActionExtend(action);

                UpdatePluginInfo(pInfo);//更新
            }
            catch (DalException ex)
            {
                throw new BOException("更新插件相关所有信息出错", ex);
            }
        }

        //更新或者升级web插件
        [Frame(true, true)]
        public virtual string UpdateWebPluginInfo(PluginInfo pInfo, ActionExtend action, VersionTrack version, string isAdd, string uid)
        {
            try
            {
                string verfilepath = "";
                string oldfilepath = "";
                if (isAdd.Equals("0"))//若为修改
                {
                    SearchVersionTrack search = new SearchVersionTrack();
                    search.VID = version.VersionId.ToString();
                    VersionTrack ver = GetVersionTrack(search)[0];
                    oldfilepath = ver.FilePath;
                    ver.VersionName = version.VersionName;//修改的版本号
                    ver.FilePath = Path.Combine(AppConfig.filePath + pInfo.PluginCode, version.VersionName);
                    ver.LastModTime = DateTime.Now;
                    ver.LastModUid = uid;

                    verfilepath = ver.FilePath;

                    UpdateVersionTrack(ver);//更新版本表
                }
                else//若为升级
                {
                    verfilepath = version.FilePath;
                    InsertVersionTrack(version);
                }

                UpdatePluginInfo(pInfo, action);//更新插件表和扩展信息表

                SearchConfig searchcof = new SearchConfig();
                searchcof.PluginCode = pInfo.PluginCode.ToString();
                BoFactory.GetVersionTrackBo.DeleteConfigInfo(searchcof);//先删除原由配置信息，若存在

                if (Directory.Exists(oldfilepath)) //5.修改需要删除原由文件
                    Directory.Delete(oldfilepath, true);

                return verfilepath;
            }
            catch (DalException ex)
            {
                throw new BOException("更新web插件相关所有信息出错", ex);
            }
        }

        [Frame(true, true)]
        public virtual int InsertWebPluginInfo(PluginInfo pInfo, ActionExtend action, VersionTrack version, string uid)
        {
            try
            {
                //int pid = InsertPluginInfo(pInfo, uid);
                //action.PluginCode = pid.ToString();
                //version.PluginCode = pid;
                if (!string.IsNullOrEmpty(action.ActionCode))//若有填写扩展信息code的话
                    InsertActionExtend(action);

                return InsertVersionTrack(version);
            }
            catch (DalException ex)
            {
                throw new BOException("新增web插件相关所有信息出错", ex);
            }
        }


        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPluginNotWeb(PageView view)
        {
            try
            {
                SearchVersionTrack search = new SearchVersionTrack();
                search.NotPluginId = Common.Entities.Constants.MianPluginId.ToString();//排除主程序

                //获取web插件ID
                SearchPlugin searchP = new SearchPlugin();
                searchP.PluginCateCode = Common.Entities.Constants.PluginCateCode;
                IList<PluginInfo> list = BoFactory.GetVersionTrackBo.GetPluginInfoList(searchP);
                string pluginids = "";
                if (list != null)
                {
                    foreach (PluginInfo p in list)
                    {
                        pluginids += p.PluginCode + ",";
                    }
                }

                search.NotPluginIdForCategory = pluginids.Substring(0, pluginids.Length - 1);

                return QueryPluginVersionTrackInfo(view, search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取非web插件报错", ex);
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPluginInWeb(PageView view)
        {
            try
            {
                SearchVersionTrack search = new SearchVersionTrack();

                //获取web插件ID
                SearchPlugin searchP = new SearchPlugin();
                searchP.PluginCateCode = Common.Entities.Constants.PluginCateCode;
                IList<PluginInfo> list = BoFactory.GetVersionTrackBo.GetPluginInfoList(searchP);
                string pluginids = "";
                if (list != null)
                {
                    foreach (PluginInfo p in list)
                    {
                        pluginids += p.PluginCode + ",";
                    }
                }

                search.InPluginIdForCategory = pluginids.Substring(0, pluginids.Length - 1);

                return QueryPluginVersionTrackInfo(view, search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取web插件报错", ex);
            }
        }

        public string InsertPluginInfo(PluginInfo pInfo, string uid)
        {
            try
            {
                SearchPlugin search = new SearchPlugin();
                search.Pcode = pInfo.PluginCode;
                IList<PluginInfo> list = pluginDao.GetPluginInfoList(search);
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        throw new BOException("该插件标识已经存在,或者上传xml信息格式不正确");
                    }
                }
                pInfo.CreateTime = DateTime.Now;
                pInfo.CreateUid = uid;
                pInfo.LastModTime = DateTime.Now;
                pInfo.LastModUid = uid;
                pluginDao.Insert(pInfo);
                return pInfo.PluginCode;
            }
            catch (DalException ex)
            {
                throw new BOException("新增插件信息出错", ex);
            }
        }

        public void UpdatePluginInfo(PluginInfo pInfo)
        {
            try
            {
                pluginDao.Update(pInfo);
            }
            catch (DalException ex)
            {
                throw new BOException("新增插件信息出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual PluginInfo GetPluginInfo(string id)
        {
            try
            {
                return pluginDao.Get(id);
            }
            catch (DalException ex)
            {
                throw new BOException("根据ID获取插件信息出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual IList<PluginInfo> GetPluginInfoList(SearchPlugin search)
        {
            try
            {
                return pluginDao.GetPluginInfoList(search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取插件信息出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPluginInfo(PageView pageview, SearchPlugin search)
        {
            try
            {
                return pluginDao.QueryPluginInfo(pageview, search);
            }
            catch (DalException ex)
            {
                throw new BOException("查询插件列表信息出错", ex);
            }
        }



        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryPluginVersionTrackInfo(PageView pageview, SearchVersionTrack search)
        {
            try
            {
                return versionTrackDao.QueryPluginVersionTrackInfo(pageview, search);
            }
            catch (DalException ex)
            {
                throw new BOException("查询插件版本列表信息出错", ex);
            }
        }

        #endregion

        #region 配置表管理

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryConfigInfo(PageView view, SearchConfig search)
        {
            try
            {
                return configInfoDao.QueryConfigInfo(view, search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取配置表信息", ex);
            }
        }

        [Frame(false, false)]
        public virtual ConfigInfo GetConfigInfoById(string configId)
        {
            try
            {
                return configInfoDao.Get(configId);
            }
            catch (DalException ex)
            {
                throw new BOException("根据ID获取配置表信息", ex);
            }
        }

        [Frame(false, false)]
        public virtual IList<ConfigInfo> GetConfigList(SearchConfig search)
        {
            try
            {
                return configInfoDao.GetConfigList(search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取配置表信息", ex);
            }
        }

        [Frame(false, false)]
        public virtual IList<ConfigInfoPC> GetConfigPCList(SearchConfig search)
        {
            try
            {
                return configInfoPCDao.GetConfigList(search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取配置表信息", ex);
            }
        }

        [Frame(false, false)]
        public virtual IList<ConfigInfo> GetMobileConfigList(SearchConfig search)
        {
            try
            {
                return configInfoDao.GetMobileConfigList(search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取配置表信息", ex);
            }
        }

        [Frame(false, false)]
        public virtual IList<ConfigInfo> GetPCConfigList(SearchConfig search)
        {
            try
            {
                return configInfoDao.GetPCConfigList(search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取配置表信息", ex);
            }
        }

        [Frame(true, true)]
        public virtual void InserConfigInfo(IList<ConfigInfo> coglist, string PluginCode, string cate)
        {
            try
            {
                //foreach (ConfigInfo cog in coglist)
                //{
                //    //验证同一个插件的key不能重复
                //    SearchConfig search = new SearchConfig();
                //    search.key = cog.Key1;
                //    search.PluginCode = PluginCode;
                //    search.ConfigCategoryCode = cate;
                //    IList<ConfigInfo> list = configInfoDao.GetConfigList(search);
                //    if (list != null)
                //    {
                //        if (list.Count > 0)
                //            throw new BOException("该主程序或者插件配置信息中的键已经存在！");
                //    }
                //    cog.PluginCode = PluginCode;
                //    configInfoDao.Insert(cog);
                //}

            }
            catch (DalException ex)
            {
                //throw new BOException("新赠配置表信息", ex);
            }
        }


        [Frame(true, true)]
        public virtual void UpdateConfigInfo(ConfigInfo cog)
        {
            try
            {
                configInfoDao.Update(cog);
            }
            catch (DalException ex)
            {
                throw new BOException("修改配置表信息", ex);
            }
        }

        [Frame(true, true)]
        public virtual void DelConfigInfo(IList<ConfigInfo> cog)
        {
            try
            {
                configInfoDao.DeleteList(cog);
            }
            catch (DalException ex)
            {
                throw new BOException("删除配置表信息", ex);
            }
        }

        [Frame(true, true)]
        public virtual void DeleteConfigInfo(SearchConfig search)
        {
            try
            {
                configInfoDao.DeleteConfigInfo(search);
            }
            catch (DalException ex) { throw new BOException("删除配置表所有相关插件信息", ex); }
        }


        #endregion

        #region 主程序分类表管理

        [Frame(false, false)]
        public virtual IList<ConfigCategory> GetMainCategoryList()
        {
            try
            {
                return configCategoryDao.GetMainCategoryList();
            }
            catch (DalException ex)
            {
                throw new BOException("查询主程序分类信息出错", ex);
            }
        }

        #endregion

        #region 插件分类表

        [Frame(false, false)]
        public virtual IList<PluginCategory> GetPluginCategoryInfo()
        {
            try
            {
                //获取web插件ID
                SearchPlugin searchP = new SearchPlugin();
                searchP.PluginCateCode = Common.Entities.Constants.PluginCateCode;
                IList<PluginInfo> list = BoFactory.GetVersionTrackBo.GetPluginInfoList(searchP);
                string pluginids = "";
                if (list != null)
                {
                    foreach (PluginInfo p in list)
                    {
                        pluginids += p.PluginCateCode + ",";
                    }
                }
                SearchPlugin searchnot = new SearchPlugin();
                searchnot.NotPluginCateCode = pluginids.Substring(0, pluginids.Length - 1);
                return pluginDao.GetPluginCategoryInfo(searchnot);//查询不包括web插件的信息
            }
            catch (DalException ex)
            {
                throw new BOException("查询插件分类信息出错", ex);
            }
        }

        #endregion

        #region 插件扩展信息

        public virtual void InsertActionExtend(ActionExtend ae)
        {
            try
            {
                if (string.IsNullOrEmpty(ae.ActionCode))
                    throw new BOException("插件扩展信息标识不能为空");
                SearchConfig search = new SearchConfig();
                search.code = ae.ActionCode;
                IList<ActionExtend> list = QueryActionExtend(search);
                if (list != null)
                {
                    if (list.Count > 0)
                        throw new BOException("插件扩展信息标识不能重复");
                }
                actionExtendDao.Insert(ae);
            }
            catch (DalException ex)
            {
                throw new BOException("新增插件扩展信息出错", ex);
            }
        }

        public virtual void DelActionExtend(ActionExtend ae)
        {
            try
            {
                actionExtendDao.Delete(ae);
            }
            catch (DalException ex)
            {
                throw new BOException("删除插件扩展信息出错", ex);
            }
        }

        public virtual void UpdateActionExtend(ActionExtend ae)
        {
            try
            {
                if (string.IsNullOrEmpty(ae.ActionCode))
                    throw new BOException("插件扩展信息标识不能为空");
                SearchConfig search = new SearchConfig();
                search.code = ae.ActionCode;
                IList<ActionExtend> list = QueryActionExtend(search);
                if (list != null)
                {
                    if (list.Count > 0 && !list[0].PluginCode.Equals(ae.PluginCode))
                        throw new BOException("插件扩展信息标识不能重复");
                }
                actionExtendDao.Update(ae);
            }
            catch (DalException ex)
            {
                throw new BOException("新增插件扩展信息出错", ex);
            }
        }



        public virtual IList<ActionExtend> QueryActionExtend(SearchConfig search)
        {
            try
            {
                return actionExtendDao.QueryActionExtendInfo(search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取插件扩展信息出错", ex);
            }
        }

        #endregion

        #region 插件相关用户管理

        [Frame(false, false)]
        public virtual IList<UserInfo> GetStuNameList(SearchVersionTrack search)
        {
            try
            {
                return userInfoDao.GetStuNameList(search);
            }
            catch (DalException ex)
            {
                throw new BOException("获取用户信息出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryUserInfoByPId(SearchConfig search, PageView view)
        {
            try
            {
                return userInfoDao.QueryUserInfoByPId(search, view);
            }
            catch (DalException ex)
            {
                throw new BOException("获取插件用户信息出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryUserInfoNotByPId(SearchConfig search1, SearchVersionTrack search, PageView view)
        {
            try
            {
                return userInfoDao.QueryUserInfoNotByPId(search1, search, view);
            }
            catch (DalException ex)
            {
                throw new BOException("获取插件未设置的用户信息出错", ex);
            }
        }

        [Frame(true, true)]
        public virtual void InsertUserRef(IList<UserPluginRef> userRef)
        {
            try
            {
                foreach (UserPluginRef u in userRef)
                {
                    userPluginRefDao.Insert(u);
                }
            }
            catch (DalException ex)
            {
                throw new BOException("新增插件用户信息出错", ex);
            }
        }

        [Frame(true, true)]
        public virtual void DelUserRef(IList<UserPluginRef> userRef)
        {
            try
            {
                foreach (UserPluginRef u in userRef)
                {
                    userPluginRefDao.Delete(u);
                }
            }
            catch (DalException ex)
            {
                throw new BOException("删除插件用户信息出错", ex);
            }
        }

        #endregion

        #region IOSOutsideApp管理
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryIOSOutsideAppList(PageView view)
        {
            return iosOutsideAppDao.QueryIOSOutsideAppList(view);
        }

        [Frame(false, false)]
        public virtual IOSOutsideApp GetIOSOutsideApp(string id)
        {
            IOSOutsideApp iosApp = iosOutsideAppDao.Get(id);
            if (iosApp == null)
            {
                throw new Exception("未找到指定的IOSOutsideApplication");
            }

            return iosApp;
        }

        [Frame(false, true)]
        public virtual void InsertIOSOutSideApp(IOSOutsideApp iosOutsideApp)
        {
            //插入数据
            iosOutsideAppDao.Insert(iosOutsideApp);

            //if (!System.IO.File.Exists(tempFilePath))
            //{
            //    throw new Exception("未找到上传的IOSOutSideApplication,请重新上传者联系管理员");
            //}
            //if (iosOutsideApp.ClientType.Equals("Main", StringComparison.CurrentCultureIgnoreCase)
            //    && AppConfig.PublishConfig.ContainsKey(iosOutsideApp.ClientType.ToLower()))
            //{
            //    string path = AppConfig.PublishConfig[iosOutsideApp.ClientType.ToLower()];
            //    if (!string.IsNullOrEmpty(path))
            //    {
            //        string dirPath = Path.GetDirectoryName(path);
            //        if (!System.IO.Directory.Exists(dirPath))
            //        {
            //            System.IO.Directory.CreateDirectory(dirPath);
            //        }
            //        System.IO.File.Copy(tempFilePath, path, true);
            //    }
            //}
            //if (System.IO.File.Exists(saveFilePath))
            //{
            //    System.IO.File.Delete(saveFilePath);
            //}
            //System.IO.File.Move(tempFilePath, saveFilePath);
        }

        [Frame(false, true)]
        public virtual void UpdateIOSOutSideApp(IOSOutsideApp iosOutsideApp)
        {
            iosOutsideAppDao.Update(iosOutsideApp);
        }

        [Frame(false, true)]
        public virtual void DeleteIOSOutSideApp(string id)
        {
            IOSOutsideApp iosApp = iosOutsideAppDao.Get(id);
            if (iosApp == null)
            {
                throw new Exception("未找到指定的安装包");
            }

            //删除
            iosOutsideAppDao.Delete(iosApp);
        }

        #endregion

        #region 设备绑定管理

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryApplyDeviceBindList(PageView pageview)
        {
            try
            {
                return applyDeviceBindDao.QueryApplyDeviceBindList(pageview);
            }
            catch (DalException ex)
            {
                throw new BOException("查询未审批设备绑定列表出错", ex);
            }
        }

        [Frame(false, true)]
        public virtual void ApproveApplyDeviceBind(string id, string Operation)
        {
            try
            {
                ApplyDeviceBind adb = applyDeviceBindDao.Get(id);
                if (Operation == "1")
                {
                    adb.Status = ApplyDeviceBindStatus.Approval;

                    DeviceBind deviceBind = new DeviceBind();
                    deviceBind.Id = Guid.NewGuid();
                    deviceBind.UserUid = adb.UserUid;
                    deviceBind.Device = "*";
                    deviceBind.DeviceId = adb.DeviceId;
                    deviceBind.Status = "ENABLE";
                    deviceBind.Description = adb.Description;
                    deviceBindDao.Insert(deviceBind);
                }
                else if (Operation == "2")
                { adb.Status = ApplyDeviceBindStatus.UnApproval; }
                applyDeviceBindDao.Update(adb);
            }
            catch (DalException ex)
            {
                throw new BOException("审批设备绑定出错", ex);
            }
        }

        //已审批管理
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryDeviceBindList(PageView pageview)
        {
            try
            {
                return deviceBindDao.QueryDeviceBindList(pageview);
            }
            catch (DalException ex)
            {
                throw new BOException("查询已审批设备绑定列表出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData SearchDeviceBindList(PageView pageview, string UserId, string Description, string Status)
        {
            try
            {
                return deviceBindDao.SearchDeviceBindList(pageview, UserId, Description, Status);
            }
            catch (DalException ex)
            {
                throw new BOException("查询已审批设备绑定列表出错", ex);
            }
        }


        [Frame(false, true)]
        public virtual void SetDeviceBindStatus(string id, string Operation)
        {
            try
            {
                DeviceBind adb = deviceBindDao.Get(id);

                adb.Status = Operation;

                deviceBindDao.Update(adb);
            }
            catch (DalException ex)
            {
                throw new BOException("审批设备绑定出错", ex);
            }
        }

        #endregion

        [Frame(false, true)]
        public virtual IList<Application> GetApplications()
        {
            return versionTrackDao.GetApplications();
        }

        /// <summary>
        /// 清除所有权限的用户
        /// </summary>
        /// <param name="privilegecode"></param>
        [Frame(false, true)]
        public virtual void ClearPrivilegeUser(string privilegecode)
        {
            PrivilegeUserDao puDao = new PrivilegeUserDao(AppConfig.mainDbKey);
            IList<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
            pars.Add(new Tuple<string, string, object>("id", "=", privilegecode));
            List<PrivilegeUser> pulist = puDao.QueryList(pars);
            if (pulist != null && pulist.Count > 0)
            {
                foreach (PrivilegeUser pu in pulist)
                {
                    puDao.Delete(pu);
                }
            }
        }

        [Frame(false, true)]
        public virtual void ChangeUserPassword(string useruid, string password)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["PwdEncrypted"].Equals("true", StringComparison.CurrentCultureIgnoreCase)) ;
            {
                password = BeyondbitCrypto.Encryptor(password);
            }
            managerDao.ChangeManagePassowrd(useruid, password);
            //Beyondbit.BUA.Client.ServiceFactory.Instance().GetUserService().ResetUserPassword(useruid, password);
        }
    }
}
