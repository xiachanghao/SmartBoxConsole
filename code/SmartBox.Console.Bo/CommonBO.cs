//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_UserListBo.cs
// 
// 
// 
// 2014-03-05 04:11:59
//
// 
// 
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.Biz;
using Beyondbit.Framework.Biz.BO;
using Beyondbit.Framework.Core.InterceptorHandler;
using Beyondbit.Framework.DataAccess;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using SmartBox.Console.Dao;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using System.Data;
using System.Linq;
using System.Collections;
using Beyondbit.BUA.Client;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Configuration;

namespace SmartBox.Console.Bo
{
    public class CommonBO : BaseBO
    {
        [Frame(false, false)]
        public virtual bool SMC_PackageExtInternalRelease(SMC_PackageExt ext)
        {
            return _SMC_PackageExtInternalRelease(ext);
        }

        [Frame(false, false)]
        public virtual bool SMC_PackageExtInternalRelease(int pe_id)
        {
            SMC_PackageExtDao daoSMC_PackageExt = new SMC_PackageExtDao(AppConfig.statisticDBKey);
            List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
            pars.Add(new KeyValuePair<string, object>("pe_id", pe_id));
            SMC_PackageExt ext = daoSMC_PackageExt.Get(pars);
            return _SMC_PackageExtInternalRelease(ext);
        }

        protected SMC_Package4Out CopyPackageExt2SMC_Package4Out(SMC_PackageExt ext, SMC_Package4Out packageOut)
        {
            packageOut.BuildVer = int.Parse(ext.pe_BuildVer);
            packageOut.ClientType = ext.pe_ClientType;
            packageOut.CreateTime = ext.pe_CreatedTime;
            packageOut.CreateUid = ext.pe_CreateUid;
            packageOut.Description = ext.pe_Description;
            packageOut.DisplayName = ext.pe_DisplayName;
            packageOut.DownloadUri = ext.pe_DownloadUri;
            packageOut.Name = ext.pe_Name;
            packageOut.Type = ext.pe_Type;
            packageOut.UpdateTime = DateTime.Now;
            packageOut.UpdateUid = ext.pe_UpdateUid;
            packageOut.Version = ext.pe_Version;

            if (packageOut.Type == null)
                packageOut.Type = "plugin";
            return packageOut;
        }

        protected WebApplication CopyPackageExt2WebApplication(SMC_PackageExt ext, WebApplication webApplication)
        {
            //webApplication.AppID = ext.pe_ApplicationCode;
            webApplication.ClientType = ext.pe_ClientType;
            webApplication.CreateTime = ext.pe_CreatedTime;
            webApplication.CreateUid = ext.pe_CreateUid;
            webApplication.ShortName = ext.pe_Name;
            webApplication.Unit = ext.pe_UnitCode;
            webApplication.UpdateTime = ext.pe_UpdateTime;
            webApplication.UpdateUid = ext.pe_UpdateUid;
            //webApplication.Uri = ext.
            return webApplication;
        }

        protected Package4AI CopyPackageExt2Package4AI(SMC_PackageExt ext, Package4AI package4AI)
        {
            package4AI.BuildVer = int.Parse(ext.pe_BuildVer);
            package4AI.ClientType = ext.pe_ClientType;
            package4AI.CreateTime = ext.pe_CreatedTime;
            package4AI.CreateUid = ext.pe_CreateUid;
            package4AI.Description = ext.pe_Description;
            package4AI.DisplayName = ext.pe_DisplayName;
            package4AI.DownloadUri = ext.pe_DownloadUri;

            if (String.IsNullOrEmpty(package4AI.Type))
                package4AI.Type = ext.pe_Type;
            package4AI.UpdateTime = ext.pe_UpdateTime;
            package4AI.UpdateUid = ext.pe_UpdateUid;
            package4AI.Version = ext.pe_Version;
            package4AI.UpdateTime = DateTime.Now;

            if (package4AI.Type == null)
            {
                package4AI.Type = "plugin";
            }
            return package4AI;
        }

        private bool _SMC_PackageExtInternalRelease(SMC_PackageExt ext)
        {
            if (ext == null)
                return false;

            bool r = false;
            SMC_PackageExtDao daoSMC_PackageExt = new SMC_PackageExtDao(AppConfig.statisticDBKey);
            SMC_Package4OutDao daoSMC_Package4Out = new SMC_Package4OutDao(AppConfig.statisticDBKey);
            App4AIDao daoApp4AI = new App4AIDao(AppConfig.mainDbKey);
            Package4AIDao daoPackage4AI = new Package4AIDao(AppConfig.mainDbKey);
            ApplicationDao daoApplication = new ApplicationDao(AppConfig.mainDbKey);
            WebApplicationDao daoWebApplication = new WebApplicationDao(AppConfig.mainDbKey);
            Action4AndroidDao daoAction4AndroidDao = new Action4AndroidDao(AppConfig.mainDbKey);

            string tableName = ext.TableName.ToLower();
            int tableId = ext.TableID;
            List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
            switch (tableName)
            {
                case "webapplication":
                    pars.Add(new KeyValuePair<string, object>("id", tableId));
                    WebApplication webApplication = daoWebApplication.Get(pars);
                    if (webApplication == null)
                    {
                        webApplication = new WebApplication();
                        CopyPackageExt2WebApplication(ext, webApplication);
                        daoWebApplication.Insert(webApplication);
                        ext.TableID = webApplication.ID;
                    }
                    else
                    {
                        CopyPackageExt2WebApplication(ext, webApplication);
                        daoWebApplication.Update(webApplication);
                    }
                    break;
                case "package4ai":
                    pars.Add(new KeyValuePair<string, object>("id", tableId));
                    Package4AI package4AI = daoPackage4AI.Get(pars);
                    if (package4AI == null)
                    {
                        package4AI = new Package4AI();
                        CopyPackageExt2Package4AI(ext, package4AI);
                        daoPackage4AI.Insert(package4AI);
                        ext.TableID = package4AI.ID;
                    }
                    else
                    {
                        CopyPackageExt2Package4AI(ext, package4AI);
                        daoPackage4AI.Update(package4AI);
                    }

                    try
                    {
                        //daoAction4AndroidDao.DeleteAction4AndroidByPackage4AIID(package4AI.ID);

                        //List<Tuple<string, string, object>> _pars = new List<Tuple<string, string, object>>();
                        //_pars.Add(new Tuple<string, string, object>("package4aiid", "=", package4AI.ID));
                        //List<App4AI> app4AIList = daoApp4AI.QueryList(_pars);
                        //daoApp4AI.DeleteList(app4AIList);
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.Error(ex);
                    }

                    try
                    {
                        _AddApp4AI(package4AI, ext);
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.Error(ex);
                    }

                    try
                    {
                        _AddAction4Android(package4AI, ext);
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.Error(ex);
                    }

                    break;
                case "smc_package4out":
                    pars.Add(new KeyValuePair<string, object>("po_ID", tableId));
                    SMC_Package4Out package4Out = daoSMC_Package4Out.Get(pars);
                    if (package4Out == null)
                    {
                        package4Out = new SMC_Package4Out();
                        CopyPackageExt2SMC_Package4Out(ext, package4Out);
                        daoSMC_Package4Out.Insert(package4Out);
                        ext.TableID = package4Out.po_ID;
                    }
                    else
                    {
                        CopyPackageExt2SMC_Package4Out(ext, package4Out);
                        daoSMC_Package4Out.Update(package4Out);
                    }
                    break;
            }

            daoSMC_PackageExt.Update(ext);
            _CopyExtFileToUpdateFilesFolder(ext);

            return r;
        }

        /// <summary>
        /// 复制安装包到更新文件目录
        /// </summary>
        /// <param name="ext"></param>
        private void _CopyExtFileToUpdateFilesFolder(SMC_PackageExt ext)
        {
            string pathSource = HttpContext.Current.Server.MapPath(ext.pe_FileUrl);
            //string[] files = Directory.GetFiles(pathSource);

            Dao.Package4AIDao daoPackage4AI = new Package4AIDao(AppConfig.mainDbKey);
            IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
            pars.Add(new KeyValuePair<string, object>("id", ext.TableID));
            Package4AI package4AI = daoPackage4AI.Get(pars);

            //string sourceFileName = Path.GetFileName(ext.pe_FileUrl);
            //foreach (string file in files)
            {
                string sFileName = Path.GetFileName(pathSource);

                if (sFileName.EndsWith(".ipa") || sFileName.EndsWith(".apk"))
                {
                    string destFileName = Path.GetFileName(ext.pe_DownloadUri);
                    string destFolder = AppConfig.PackUploadFolder;
                    if (destFolder.EndsWith(@"\") == false)
                    {
                        destFolder += @"\";
                    }
                    destFolder += destFileName;
                    try
                    {
                        File.Copy(pathSource, destFolder, true);
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.Error(ex);
                    }
                }

                string savefilePath = AppConfig.PublishConfig[ext.pe_ClientType.ToLower()];
                //saveFileName = savefilePath;
                string saveFilePathOut = "";
                if (savefilePath.IndexOf("$") != -1)
                {
                    string[] savefilePaths = savefilePath.Split("$".ToCharArray());
                    savefilePath = savefilePaths[0];
                    saveFilePathOut = savefilePaths[1];
                }
                //内网本地存储固定名称的主程序

                try
                {
                    System.IO.File.Copy(pathSource, savefilePath, true);
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Error(ex);
                }

                Service.ApplicationCenterWS.WebService acws = new Service.ApplicationCenterWS.WebService();
                //StreamReader sr = new StreamReader();
                FileStream fs = new FileStream(pathSource, FileMode.Open);
                byte[] content = new byte[fs.Length];
                fs.Read(content, 0, (int)fs.Length - 1);
                fs.Close();
                fs.Dispose();

                //发布到外网存储固定名称的主程序
                if (package4AI != null && package4AI.Type.ToLower() == "main")
                    acws.RemotePublish(content, saveFilePathOut);
            }
        }

        private void _AddAction4Android(Package4AI package, SMC_PackageExt ext)
        {
            if (!String.IsNullOrEmpty(ext.pe_ApplicationCode))
            {
                string[] apps = ext.pe_ApplicationCode.Split(",".ToCharArray());
                if (apps != null && apps.Length > 0)
                {
                    ApplicationDao appDao = new ApplicationDao(AppConfig.mainDbKey);
                    App4AIDao app4AIDao = new App4AIDao(AppConfig.mainDbKey);
                    string TEMPPATH = Path.Combine(AppConfig.PackUploadFolder, "Temp");
                    int idx = 1;

                    Action4AndroidDao action4AndroidDao = new Action4AndroidDao(AppConfig.mainDbKey);
                    foreach (string appCode in apps)
                    {
                        if (String.IsNullOrEmpty(appCode))
                            continue;
                        List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                        pars.Add(new KeyValuePair<string, object>("[NAME]", appCode));
                        Application application = appDao.Get(pars);

                        //for (int activtyIndex = 0; activtyIndex < Convert.ToInt32(form["activityCount_" + appIndex.ToString()]); activtyIndex++)
                        //{
                        Action4Android action4Android = new Action4Android();
                        action4Android.Seq = idx;//Convert.ToInt32(form["ActivitySeq_" + appIndex.ToString() + "_" + activtyIndex.ToString()]);
                        action4Android.DisplayName = "";//form["ActivityDisplayName_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                        action4Android.IsLaunch = true;//Convert.ToBoolean(form["ActivityLaunch_" + appIndex.ToString() + "_" + activtyIndex.ToString()]);
                        action4Android.IconUri = "";//GetAndroidApplicationIcoUri(Path.Combine(TEMPPATH, package.DownloadUri), form["ActivityIco_" + appIndex.ToString() + "_" + activtyIndex.ToString()]);
                        action4Android.ShortName = "";//form["ActivityShortName_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                        action4Android.Name = "";//form["ActivityName_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                        action4Android.CreateTime = DateTime.Now;
                        action4Android.UpdateTime = DateTime.Now;
                        action4Android.CreateUid = "";//CurrentUser.UserUId;
                        action4Android.UpdateUid = "";//CurrentUser.UserUId;

                        action4AndroidDao.Insert(action4Android);
                        //}

                        ++idx;
                    }
                }
            }
        }

        private void _AddApp4AI(Package4AI package, SMC_PackageExt ext)
        {

            if (!String.IsNullOrEmpty(ext.pe_ApplicationCode))
            {
                string[] apps = ext.pe_ApplicationCode.Split(",".ToCharArray());
                if (apps != null && apps.Length > 0)
                {
                    ApplicationDao appDao = new ApplicationDao(AppConfig.mainDbKey);
                    App4AIDao app4AIDao = new App4AIDao(AppConfig.mainDbKey);
                    string TEMPPATH = Path.Combine(AppConfig.PackUploadFolder, "Temp");
                    int idx = 1;
                    Hashtable extendInfo = null;
                    JObject extObj = null;
                    if (!String.IsNullOrEmpty(ext.pe_ExtentInfo))
                    {
                        extendInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<Hashtable>(ext.pe_ExtentInfo);
                        extObj = JObject.Parse(ext.pe_ExtentInfo);
                    }

                    Newtonsoft.Json.Linq.JArray appsHash = (Newtonsoft.Json.Linq.JArray)extendInfo["Applications"];
                    JArray appsJobj = (JArray)extObj["Applications"];
                    foreach (string appCode in apps)
                    {
                        if (String.IsNullOrEmpty(appCode))
                            continue;

                        if (extendInfo.Count > 0)
                        {
                        }

                        //appsHash.Select(e => { e.});

                        List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                        pars.Add(new KeyValuePair<string, object>("[NAME]", appCode));
                        Application application = appDao.Get(pars);



                        App4AI app4AI = new App4AI();

                        app4AI.AppID = application.ID;
                        //Newtonsoft.Json.Linq.JToken.EqualityComparer = new Newtonsoft.Json.Linq.JTokenEqualityComparer();
                        //ec.

                        string AppIconUri = "";
                        foreach (var j in appsJobj)
                        {
                            var i = (JObject)j;
                            string ApplicationID = (string)i["ApplicationID"];
                            if (ApplicationID == application.ID.ToString())
                            {
                                var oIcon = (JObject)i["ApplicationIcon"];
                                AppIconUri = (string)oIcon["AppIconUri"];
                                break;
                            }
                        }
                        //Hashtable appHash = appsHash.Find(e => e["ApplicationID"] == application.ID.ToString());
                        //Hashtable appIconHash = (Hashtable)appHash["ApplicationIcon"];
                        //string AppIconUri = (string)appIconHash["AppIconUri"];


                        app4AI.Package4AIID = package.ID;
                        //app4AI.PackageName = package.Name;
                        app4AI.AppCode = appCode;
                        app4AI.ClientType = package.ClientType;//form["AppCheckClentType_" + appIndex.ToString()];
                        //string appIco = "";//form["AppIco_" + appIndex.ToString()]
                        app4AI.IconUri = AppIconUri;// GetAndroidApplicationIcoUri(Path.Combine(TEMPPATH, package.DownloadUri), appIco);
                        app4AI.Seq = idx;
                        app4AI.CreateTime = DateTime.Now;
                        app4AI.UpdateTime = DateTime.Now;
                        app4AI.CreateUid = "";
                        app4AI.UpdateUid = "";


                        IList<KeyValuePair<string, object>> _pars = new List<KeyValuePair<string, object>>();
                        _pars.Add(new KeyValuePair<string, object>("Package4AIID", app4AI.Package4AIID));
                        _pars.Add(new KeyValuePair<string, object>("AppCode", app4AI.AppCode));
                        _pars.Add(new KeyValuePair<string, object>("ClientType", app4AI.ClientType));
                        App4AI _app4ai = app4AIDao.Get(_pars);
                        if (_app4ai == null)
                        {
                            //package.App4AIList.Add(app4AI);
                            app4AIDao.Insert(app4AI);
                        }
                        else
                        {
                            app4AIDao.Update(app4AI);
                        }

                        ++idx;
                    }
                }
            }
        }
        private static readonly List<string> EXTENSION = new List<string> { ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".ico" };
        /// <summary>
        /// 获取AndroidApp图片Uri
        /// 将图片存储到数据库后返回图片Uri地址
        /// </summary>
        /// <param name="packagePath">安装包的物理地址</param>
        /// <param name="innerAppIcoUri">包内部AppIco名称</param>
        /// <returns></returns>
        public string GetAndroidApplicationIcoUri(string packagePath, string innerAppIco)
        {

            if (string.IsNullOrEmpty(packagePath))
            {
                throw new Exception("安装包路径不能为空");
            }
            if (string.IsNullOrEmpty(innerAppIco))
            {
                return string.Empty;
            }
            if (innerAppIco.StartsWith("Server://", StringComparison.CurrentCultureIgnoreCase) || innerAppIco.StartsWith("package://",StringComparison.CurrentCultureIgnoreCase))
            {
                return innerAppIco;
            }
            Image image = null;
            using (ZipFile zip = new ZipFile(packagePath))
            {
                foreach (ZipEntry entry in zip)
                {
                    if (Path.GetDirectoryName(entry.Name).StartsWith("res\\drawable", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(entry.Name);
                        //("package://com.beyondbit.smartbox.calendar/" + fileName).Equals(innerAppIco)
                        if (fileName.Equals(innerAppIco, StringComparison.CurrentCultureIgnoreCase) || ((!String.IsNullOrEmpty(innerAppIco)) && (innerAppIco.IndexOf("/" + fileName) != -1)))
                        {
                            if (EXTENSION.Contains(Path.GetExtension(entry.Name).ToLower()))
                            {
                                image = BoFactory.GetVersionTrackBo.InsertImage(zip.GetInputStream(entry));
                                break;
                            }
                        }
                    }
                }
            }

            if (image == null)
            {
                throw new Exception(string.Format("未找到{0}文件", innerAppIco));
            }

            return string.Format(@"Server://beyondbit.smartbox.server.image/{0}", image.ID); ;
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceAuthorization(string uid, string deviceid, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            ApplyDeviceBindDao dao = new ApplyDeviceBindDao(AppConfig.mainDbKey);
            return dao.GetDeviceAuthorization(uid, deviceid, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceAuthorizationNew(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetDeviceAuthorization(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceUser(string uid, string model, string deviceid, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetDeviceUser(uid, model, deviceid, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceEnableAuthorizationSys(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetDeviceEnableAuthorizationSys(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }
        
        

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetAppPackageAuthorization(string appName, string application, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, string categoryID, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetAppPackageAuthorization(appName, application, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, categoryID, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetAppPackageSyncList(string appName, string application, string unitcode, string auth_time_start, string auth_time_end, string syncstatus, string orderby, int pageIndex, int pageSize)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetAppPackageSyncList(appName, application, unitcode, auth_time_start, auth_time_end, syncstatus, orderby, pageIndex, pageSize);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetApplicationExtList(string appName, string application, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, string categoryID, string orderby, int pageSize, int pageIndex)
        {
            SMC_PackageExtDao dao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
            return dao.GetApplicationExtList(appName, application, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, categoryID, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual List<IDictionary<string, object>> GetApplicationCategory()
        {
            SMC_PackageExtDao dao = new SMC_PackageExtDao(AppConfig.mainDbKey);
            DataSet ds = dao.GetApplicationCategoryList(-1);
            List<IDictionary<string, object>> result = dao.TranslateTable(ds.Tables[0]);
            IDictionary<string, object> itemAll = new Dictionary<string, object>();
            itemAll["id"] = "";
            itemAll["displayname"] = "请选择类别";
            result.Insert(0, itemAll);
            return result;
        }

        [Frame(false, false)]
        public virtual List<IDictionary<string, object>> GetTaskCenter(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetTaskCenter(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceDisableAuthorizationSys(string uid, string model, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetDeviceDisableAuthorizationSys(uid, model, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceLost(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetDeviceLost(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceSync(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetDeviceSync(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceManage(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, int deviceUserStatus, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetDeviceManage(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, deviceUserStatus, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetTimeIndex(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetTimeIndex(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetStatisticsByUnit(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetStatisticsByUnit(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetUserDevice(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetUserDevice(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetUnImportedAppPackages(string name, string displayName, string clientType, string lost_time_start, string lost_time_end, string type, string orderby, int pageSize, int pageIndex)
        {
            Package4AIDao dao = new Package4AIDao(AppConfig.mainDbKey);
            SelectPagnationExDictionary result = dao.GetUnImportedAppPackages(name, displayName, clientType, lost_time_start, lost_time_end, type, orderby, pageSize, pageIndex);
            return result;
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetDeviceUnLock(string uid, string model, string u_unitcode, string lost_time_start, string lost_time_end, string unlost_time_start, string unlost_time_end, int status, string orderby, int pageSize, int pageIndex)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetDeviceUnLock(uid, model, u_unitcode, lost_time_start, lost_time_end, unlost_time_start, unlost_time_end, status, orderby, pageSize, pageIndex);
        }

        /// <summary>
        /// 挂失设备
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool LostDevice(string id, string checkUser)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            DeviceUserDao dudao = new DeviceUserDao(AppConfig.mainDbKey);
            Device device = dao.Get(id);
            if (device != null)
            {
                device.LostTime = DateTime.Now;
                device.Status = 2;
                dao.Update(device);

                List<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
                pars.Add(new Tuple<string, string, object>("deviceid", "=", id));
                List<DeviceUser> deviceUsers = dudao.QueryList(pars);

                try
                {
                    Service.ServiceReference1.ManagerServiceClient msc = new Service.ServiceReference1.ManagerServiceClient();
                    msc.ForceQuitClient(device.ID, device.Resource);
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Error(ex);
                }

                string[] uids = null;
                //if (deviceUsers != null && deviceUsers.Count > 0)
                //{
                //    uids = new string[deviceUsers.Count];
                //    for (int i = 0; i < deviceUsers.Count; ++i)
                //    {
                //        DeviceUser du = deviceUsers[i];
                //        uids[i] = du.UID;
                //    }
                //}
                //if (uids != null && uids.Length > 0)
                //{
                //    try
                //    {
                //        msc.ForceQuitUsers(uids);
                //    }
                //    catch (Exception ex)
                //    {
                //        Log4NetHelper.Error(ex);
                //    }
                //}

                string uid_online = dudao.GetDeviceOnlineUser(id);//根据设备id取最后登陆成功的用户

                string send_msg_after_device_lost = ConfigurationManager.AppSettings["send_msg_after_device_lost"];
                if (!String.IsNullOrEmpty(send_msg_after_device_lost) && send_msg_after_device_lost.ToLower() == "true")
                {
                    SmartBox.Console.Reminder.Reminder reminder = new Reminder.Reminder();
                    string send_msg_after_device_lost_content = ConfigurationManager.AppSettings["send_msg_after_device_lost_content"];
                    if (String.IsNullOrEmpty(send_msg_after_device_lost_content))
                        send_msg_after_device_lost_content = "您的设备已经挂失成功！";

                    //for (int i = 0; i < uids.Length; ++i)
                    //{
                    try
                    {
                        reminder.RemindByMobile(uid_online, "", "设备挂失", send_msg_after_device_lost_content);
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.Error(ex);
                    }
                    //}                    
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 解除挂失设备
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool UnLostDevice(string id, string checkUser)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);

            Device device = dao.Get(id);
            if (device != null)
            {
                //device.LostTime = DateTime.Now;
                device.UnLostTime = DateTime.Now;
                device.Status = 0;
                dao.Update(device);


                DeviceUserDao dudao = new DeviceUserDao(AppConfig.mainDbKey);
                List<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
                pars.Add(new Tuple<string, string, object>("deviceid", "=", id));
                //List<DeviceUser> deviceUsers = dudao.QueryList(pars);

                string uid_online = dudao.GetDeviceOnlineUser(id);//根据设备id取最后登陆成功的用户
                //if (deviceUsers != null && deviceUsers.Count > 0)
                {
                    SmartBox.Console.Reminder.Reminder reminder = new Reminder.Reminder();
                    //for (int i = 0; i < deviceUsers.Count; ++i)
                    {
                        //DeviceUser du = deviceUsers[i];
                        string send_msg_after_device_unlost = ConfigurationManager.AppSettings["send_msg_after_device_unlost"];
                        if (!String.IsNullOrEmpty(send_msg_after_device_unlost) && send_msg_after_device_unlost.ToLower() == "true")
                        {
                            try
                            {
                                reminder.RemindByMobile(uid_online, "", "设备解除挂失", "您的设备已经解除挂失成功！");
                            }
                            catch (Exception ex)
                            {
                                Log4NetHelper.Error(ex);
                            }
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 解除锁定
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool UnLockDevice(string id, string checkUser)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            Device device = dao.Get(id);
            if (device != null)
            {
                device.LostTime = DateTime.Now;
                device.Status = 0;
                dao.Update(device);
                return true;
            }
            else
            {
                return false;
            }
        }

        [Frame(false, false)]
        public virtual bool UnBindDevice(string deviceuser_id, string checkUser)
        {
            DeviceUserDao dao = new DeviceUserDao(AppConfig.mainDbKey);
            DeviceUser deviceUser = dao.Get(deviceuser_id);
            if (deviceUser != null)
            {
                deviceUser.Status = 0;
                deviceUser.LastUpdateTime = DateTime.Now;
                deviceUser.LastUpdateUID = checkUser;
                dao.Update(deviceUser);

                DeviceDao deviceDao = new DeviceDao(AppConfig.mainDbKey);
                List<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
                pars.Add(new Tuple<string, string, object>("id", "=", deviceUser.DeviceID));
                List<Device> devices = deviceDao.QueryList(pars);

                if (devices.Count > 0)
                {
                    try
                    {
                        //踢用户
                        SmartBox.Console.Service.ServiceReference1.ManagerServiceClient cli = new Service.ServiceReference1.ManagerServiceClient();
                        cli.ForceQuitClient(devices[0].ID, devices[0].Resource);
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.Error(ex);
                    }
                }

                string send_msg_after_device_unbind = ConfigurationManager.AppSettings["send_msg_after_device_unbind"];
                if (!String.IsNullOrEmpty(send_msg_after_device_unbind) && send_msg_after_device_unbind.ToLower() == "true")
                {
                    SmartBox.Console.Reminder.Reminder reminder = new Reminder.Reminder();

                    try
                    {
                        string s = "";
                        if (devices.Count > 0)
                            s = devices[0].Model + "(" + deviceUser.DeviceID + ")";
                        reminder.RemindByMobile(deviceUser.UID, "", "设备挂失", "您的账号与设备" + s + "已解除绑定！");
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.Error(ex);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通过审核设备
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool PassDevice(string duaid, string checkUser)
        {
            try
            {
                DeviceUserApplyDao dao = new DeviceUserApplyDao(AppConfig.mainDbKey);
                DeviceUserApply dua = dao.Get(duaid);

                if (dua != null)
                {
                    dua.CheckTime = DateTime.Now;
                    dua.CheckUid = checkUser;
                    dua.Status = 1;
                    dao.Update(dua);

                    DeviceUserDao duDao = new DeviceUserDao(AppConfig.mainDbKey);
                    List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                    pars.Add(new KeyValuePair<string, object>("ID", dua.DeviceUserID));

                    DeviceUser du = duDao.Get(pars);
                    du.Status = 1;
                    du.LastUpdateTime = DateTime.Now;
                    du.LastUpdateUID = checkUser;
                    duDao.Update(du);

                    string send_msg_after_device_user_auth = ConfigurationManager.AppSettings["send_msg_after_device_user_auth"];
                    if (!String.IsNullOrEmpty(send_msg_after_device_user_auth) && send_msg_after_device_user_auth.ToLower() == "true")
                    {
                        string send_msg_after_device_user_auth_content = ConfigurationManager.AppSettings["send_msg_after_device_user_auth_content"];
                        if (String.IsNullOrEmpty(send_msg_after_device_user_auth_content))
                            send_msg_after_device_user_auth_content = "设备审核通过";
                        SmartBox.Console.Reminder.Reminder reminder = new Reminder.Reminder();
                        try
                        {
                            reminder.RemindByMobile(du.UID, "", "设备审核通过", send_msg_after_device_user_auth_content);
                        }
                        catch (Exception ex)
                        {
                            Log4NetHelper.Error(ex);
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                return false;
            }
            //ApplyDeviceBindDao dao = new ApplyDeviceBindDao(AppConfig.mainDbKey);
            //ApplyDeviceBind u = dao.Get(id);
            //if (u != null)
            //{
            //    u.Status = ApplyDeviceBindStatus.Approval;
            //    u.CheckTime = DateTime.Now;
            //    u.CheckUser = checkUser;
            //    dao.Update(u);

            //    DeviceBindDao deviceBindDao = new DeviceBindDao(AppConfig.mainDbKey);
            //    DeviceBind deviceBind = deviceBindDao.Get(u.UserUid, u.DeviceId);
            //    if (deviceBind == null)
            //    {
            //        deviceBind = new DeviceBind();
            //        deviceBind.Id = Guid.NewGuid();
            //        deviceBind.UserUid = u.UserUid;
            //        deviceBind.Device = "*";
            //        deviceBind.DeviceId = u.DeviceId;
            //        deviceBind.Status = "ENABLE";
            //        deviceBind.Description = u.Description;
            //        deviceBindDao.Insert(deviceBind);
            //    }
            //    else
            //    {
            //        deviceBind.Id = Guid.NewGuid();
            //        deviceBind.UserUid = u.UserUid;
            //        deviceBind.Device = "*";
            //        deviceBind.DeviceId = u.DeviceId;
            //        deviceBind.Status = "ENABLE";
            //        deviceBind.Description = u.Description;
            //        deviceBindDao.Update(deviceBind);
            //    }

            //    return true;
            //}
            //else
            //    return false;
        }



        public virtual List<IDictionary<string, object>> GetUserDevices(string uid)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.GetUserDevices(uid);
        }

        public virtual bool LostUserDevicePost(string deviceid)
        {
            DeviceDao dao = new DeviceDao(AppConfig.mainDbKey);
            return dao.LostUserDevicePost(deviceid);
        }

        [Frame(false, false)]
        public virtual SelectPagnationEx<SystemConfig> GetServiceConfigList(string key, string orderby, int pageSize, int pageIndex)
        {
            SystemConfigDao dao = new SystemConfigDao(AppConfig.mainDbKey);
            return dao.GetServiceConfigList(key, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetPrivilegeManageList(string app, string unitcode, string orderby, int pageSize, int pageIndex)
        {
            ApplicationDao dao = new ApplicationDao(AppConfig.mainDbKey);

            return dao.GetPrivilegeManageList(app, unitcode, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetApplicationManageList(string app, string unitcode, string orderby, int pageSize, int pageIndex)
        {
            ApplicationDao dao = new ApplicationDao(AppConfig.mainDbKey);

            return dao.GetApplicationManageList(app, unitcode, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual void AppPrivilegeAdd(string appid, string appName, string appCode, string buaappid, string buaprivilege, string buaprivilegetext, string enablesync, string syncinterval)
        {
            if (buaprivilege == null)
                buaprivilege = "";
            AppPrivilegeDao dao = new AppPrivilegeDao(AppConfig.mainDbKey);
            ApplicationDao appDao = new ApplicationDao(AppConfig.mainDbKey);
            KeyValuePair<string, object> pk = new KeyValuePair<string, object>("name", appCode);
            IList<KeyValuePair<string, object>> par = new List<KeyValuePair<string, object>>();
            par.Add(pk);
            Application application = appDao.Get(par);
            int appPrivilegeid = application.PrivilegeID != null ? application.PrivilegeID.Value : 0;
            AppPrivilege ap = null;
            bool exists = dao.Exists(appPrivilegeid.ToString());
            if (exists)
            {
                IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                KeyValuePair<string, object> p = new KeyValuePair<string, object>("id", appPrivilegeid);

                pars.Add(p);
                ap = dao.Get(pars);
                ap.BuaAppCode = buaappid;
                ap.BuaPrivilegeCode = buaprivilege;
                ap.DisplayName = buaprivilegetext;

                ap.EnableSync = enablesync == "ENABLE" ? true : false;
                ap.SyncIntervalTime = String.IsNullOrEmpty(syncinterval) ? 0 : int.Parse(syncinterval);
                dao.Update(ap);
            }
            else
            {
                ap = new AppPrivilege();
                //ap.ID = int.Parse(appid);
                ap.BuaAppCode = buaappid;
                ap.BuaPrivilegeCode = buaprivilege;
                ap.EnableSync = enablesync == "ENABLE" ? true : false;
                ap.Name = appCode;
                ap.DisplayName = buaprivilegetext;
                ap.SyncIntervalTime = String.IsNullOrEmpty(syncinterval) ? 0 : int.Parse(syncinterval);
                ap.SyncLastTime = DateTime.Now;
                ap.SyncTime = DateTime.Now;
                ap.CreateUid = "";
                ap.CreateTime = DateTime.Now;
                ap.UpdateTime = DateTime.Now;
                ap.UpdateUid = "";

                dao.Insert(ap);
            }

            if (application != null)
            {
                application.PrivilegeID = ap.ID;
                appDao.Update(application);
            }

        }

        public virtual void CopyAppFilesToAppCenterServer(Hashtable r, int pe_id)
        {
            try
            {
                string packUploadFolder = ConfigurationManager.AppSettings["packUploadFolder"];
                string packUploadFolderApplicationCenter = ConfigurationManager.AppSettings["packUploadFolderApplicationCenter"];
                string s = "";
                if (pe_id > 0)
                {
                    SMC_PackageExtDao peDao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
                    SMC_PackageExt pe = peDao.Get(pe_id);

                    if (packUploadFolder.EndsWith("\\"))
                    {
                        s = packUploadFolder + Path.GetFileName(pe.pe_DownloadUri);
                    }
                    else
                    {
                        s = packUploadFolder + "\\" + Path.GetFileName(pe.pe_DownloadUri);
                    }
                }

                if (Directory.Exists(packUploadFolder))
                {
                    string[] sourceFiles = null;
                    if (pe_id <= 0)
                    {
                        sourceFiles = Directory.GetFiles(packUploadFolder);
                    }
                    else
                    {
                        sourceFiles = new string[] { s };
                    }

                    foreach (string file in sourceFiles)
                    {
                        string fileName = Path.GetFileName(file);

                        Service.ApplicationCenterWS.WebService ws = new Service.ApplicationCenterWS.WebService();

                        FileStream fs = null;
                        byte[] content = null;
                        try
                        {
                            fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                            content = new byte[fs.Length];
                            fs.Read(content, 0, (int)fs.Length - 1);
                        }
                        catch (Exception ex)
                        {
                            Log4NetHelper.Error(ex);
                            continue;
                        }
                        finally
                        {
                            fs.Close();
                            fs.Dispose();
                        }

                        try
                        {
                            ws.AppFileSync(content, fileName);
                        }
                        catch (Exception ex)
                        {
                            Log4NetHelper.Error(ex);
                            continue;
                        }
                    }

                    //更新server缓存
                    SmartBox.Console.Service.ServiceReference1.ManagerServiceClient cli = new Service.ServiceReference1.ManagerServiceClient();
                    cli.ResetClientVer();
                }
                else
                {
                    Log4NetHelper.Info("packUploadFolder不存在:" + packUploadFolder);
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }
        }
        [Frame(false, false)]
        public virtual void UnAuthAppPrivilege(string id)
        {
            AppPrivilegeDao apdao = new AppPrivilegeDao(AppConfig.mainDbKey);
            PrivilegeUserDao pudao = new PrivilegeUserDao(AppConfig.mainDbKey);
            ApplicationDao appDao = new ApplicationDao(AppConfig.mainDbKey);
            AppPrivilege appPrivilege = apdao.Get(id);
            if (appPrivilege != null)
            {
                IList<Tuple<string, string, object>> _pars = new List<Tuple<string, string, object>>();
                _pars.Add(new Tuple<string, string, object>("PrivilegeId", "=", appPrivilege.ID));

                List<Application> applications = appDao.QueryList(_pars);
                if (applications != null && applications.Count > 0)
                {
                    foreach (Application app in applications)
                    {
                        app.PrivilegeID = null;
                        appDao.Update(app);
                    }
                }

                IList<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
                pars.Add(new Tuple<string, string, object>("id", "=", appPrivilege.ID));
                List<PrivilegeUser> privilegeUsers = pudao.QueryList(pars);
                pudao.DeleteList(privilegeUsers);
                apdao.Delete(appPrivilege);
                //appPrivilege.BuaAppCode = "";
                //appPrivilege.BuaPrivilegeCode = "";
                //appPrivilege.DisplayName = "";
                //appPrivilege.EnableSync = false;
                //appPrivilege.SyncIntervalTime = 0;

                //apdao.Update(appPrivilege);
            }
        }
    }
}