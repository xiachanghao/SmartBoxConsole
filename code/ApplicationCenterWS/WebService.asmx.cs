using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Bo.AppCenter;
using System.IO;
using Beyondbit.Framework.Core.Proxy;
using SmartBox.Console.Common;

namespace ApplicationCenterWS
{
    public class FileEntity
    {
        public string FileName
        {
            get;
            set;
        }
        public byte[] Content
        {
            get;
            set;
        }
    }

    /// <summary>
    /// WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        IProxy proxy = null;
        SmartBox.Console.Bo.AppCenter.AppCenterBO bo = null;
        public WebService()
        {
            proxy = ProxyFactory.CreateProxy();
            bo = proxy.CreateObject<SmartBox.Console.Bo.AppCenter.AppCenterBO>();
        }

        /// <summary>
        /// 单位同步
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SMC_UnitSync(List<SMC_Unit> units)
        {
            try
            {
                return bo.SMC_UnitSyncInsideToOutside(units);
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                return false;
            }
        }

        /// <summary>
        /// 用户同步
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SMC_UserSync(List<SMC_User> users)
        {
            try
            {
                return bo.SMC_UserSyncInsideToOutside(users);
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                return false;
            }
        }

        /// <summary>
        /// 远程发布app
        /// </summary>
        /// <param name="content"></param>
        /// <param name="remotePublishPathWithFileName"></param>
        /// <returns></returns>
        [WebMethod]
        public bool RemotePublish(byte[] content, string remotePublishPathWithFileName)
        {
            try
            {
                if (String.IsNullOrEmpty(remotePublishPathWithFileName))
                    return false;
                string fileName = Path.GetFileName(remotePublishPathWithFileName);
                string directory = Path.GetFullPath(remotePublishPathWithFileName);
                directory = directory.Replace(fileName, "");
                if (Directory.Exists(directory) == false)
                {
                    Directory.CreateDirectory(directory);
                }
                using (FileStream fs = new FileStream(remotePublishPathWithFileName, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    fs.Write(content, 0, content.Length);
                    fs.Close();
                    fs.Dispose();
                }
                return true;
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                return false;
            }
        }

        /// <summary>
        /// 截图同步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        [WebMethod]
        public bool PackagePictuerSync(SMC_PackagePicture entity)
        {
            try
            {
                return bo.PackagePictuerSyncInsideToOutside(entity);
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                return false;
            }
        }

        /// <summary>
        /// 帮助手册同步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        [WebMethod]
        public bool PackageManualSync(SMC_PackageManual entity)
        {
            try
            {
                return bo.PackageManualSyncInsideToOutside(entity);
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                return false;
            }
        }

        /// <summary>
        /// 问题反馈同步
        /// </summary>
        /// <param name="faq"></param>
        /// <returns></returns>
        [WebMethod]
        public bool PackageFAQSync(SMC_PackageFAQ faq)
        {
            try
            {
                return bo.PackageFAQSyncInsideToOutside(faq);
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                return false;
            }
        }

        [WebMethod]
        public SMC_PackageFAQ[] GetNeedSyncToInsideFAQ()
        {
            try
            {
                return bo.GetNeedSyncToInsideFAQ().ToArray();
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                return null;
            }
        }

        [WebMethod]
        public bool DeletePackageExt(int pe_id)
        {
            try
            {
                //throw new Exception("asdf");
                return bo.DeletePackageExt(pe_id);
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                return false;
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="iconPictureContent"></param>
        /// <param name="d2PictureContent"></param>
        /// <returns></returns>
        [WebMethod]
        public bool PackageExtSync(SMC_PackageExt entity)
        {
            try
            {
                return bo.PackageExtSyncInsideToOutside(entity);
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                return false;
            }
        }

        /// <summary>
        /// 同步内网插件或主程序至外网
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [WebMethod]
        public bool AppFileSync(byte[] content, string filename)
        {
            try
            {
                return bo.AppFileSync(content, filename);
            }
            catch (Exception e)
            {
                //Log4NetHelper.Error(e);
                throw e;
            }
        }

        [WebMethod]
        public bool PackageFilesSync(List<FileEntity> files, int pe_id)
        {
            try
            {
                if (files == null || files.Count == 0)
                    return false;
                string attachPath = System.Configuration.ConfigurationManager.AppSettings["attachPath"];
                if (!Directory.Exists(attachPath))
                {
                    Directory.CreateDirectory(attachPath);
                }

                string attachPathEntity = attachPath + "\\" + pe_id;
                if (!Directory.Exists(attachPathEntity))
                {
                    Directory.CreateDirectory(attachPathEntity);
                }

                foreach (FileEntity entity in files)
                {
                    string filePath = attachPathEntity + "\\" + entity.FileName;

                    if (entity.Content != null && entity.Content.Length > 0)
                    {
                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            fs.Write(entity.Content, 0, entity.Content.Length);
                            fs.Close();
                            fs.Dispose();
                        }
                    }

                }

                return true;
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                return false;
            }
        }
    }
}
