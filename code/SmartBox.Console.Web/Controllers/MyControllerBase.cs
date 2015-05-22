using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartBox.Console.Bo;
using SmartBox.Console.Common.Entities;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.Mvc;
using SmartBox.Console.Common;
using ThoughtWorks.QRCode.Codec;
using System.Text;

namespace SmartBox.Console.Web.Controllers
{
    public class MyControllerBase : Beyondbit.MVC.MyControllerBase
    {

        //====================================================================
        protected string TEMPPATH = Path.Combine(AppConfig.PackUploadFolder, "Temp");
        protected string SAVEPATH = AppConfig.PackUploadFolder;
        //musictom 2014-05-17
        //====================================================================

        protected string SAVEOUTPATH = AppConfig.OutPackUploadFolder;
        protected string OUTWEBHOST = AppConfig.OutWebHost;//外网地址

        private Beyondbit.MVC.IUser _currentUser;
        public Beyondbit.MVC.IUser CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    if (Session["_CurrentUser_"] != null)
                    {
                        _currentUser = (Beyondbit.MVC.IUser)Session["_CurrentUser_"];
                    }
                    else
                    {
                        base.CurrentUser.FullName = User.Identity.Name;
                        base.CurrentUser.UserUId = User.Identity.Name;

                        Beyondbit.BUA.Client.IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();

                        try
                        {
                            Beyondbit.BUA.Client.User u = Beyondbit.BUA.Client.ServiceFactory.Instance().GetUserService().GetUserInfo(User.Identity.Name);
                            base.CurrentUser.OrgCode = u.OrgCode;
                            base.CurrentUser.UnitCode = u.UnitCode;
                            
                            Beyondbit.BUA.Client.Org org = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService().GetOrgBaseInfo(Beyondbit.BUA.Client.ObjectType.Org, u.OrgCode);
                            base.CurrentUser.OrgName = org.OrgName;

                            //string unitCode = os.GetUnitCode(Beyondbit.BUA.Client.ObjectType.User, CurrentUser.UserUId);
                            //base.CurrentUser.UnitCode = unitCode;
                            //Beyondbit.BUA.Client.Org unitOrg = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService().GetOrgBaseInfo(Beyondbit.BUA.Client.ObjectType.Org, unitCode);
                            //base.CurrentUser.UnitName = unitOrg.OrgName;
                        }
                        catch
                        {
                        }                        

                        _currentUser = base.CurrentUser;
                        Session["_CurrentUser_"] = _currentUser;
                    }
                }
                return _currentUser;
            }
        }

        private bool isSystemManager;
        public bool IsSystemManager
        {
            get
            {
                isSystemManager = BoFactory.GetVersionTrackBo.IsSystemManager(CurrentUser.UserUId);
                return isSystemManager;
            }
        }

        protected System.Drawing.Bitmap GetDimensionalCode(string link)
        {
            System.Drawing.Bitmap bmp = null;
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 4;
                qrCodeEncoder.QRCodeVersion = 7;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                bmp = qrCodeEncoder.Encode(link, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Invalid version !");
                throw ex;
            }

            return bmp;

        }

        protected string Save2DPicture(int pe_id, string downloadUri)
        {
            if (String.IsNullOrEmpty(downloadUri))
                return "";
            System.Drawing.Bitmap bmp = GetDimensionalCode(downloadUri);
            string vdir = "~/" + AppConfig.OutPackUploadFolder + "/" + pe_id + "/";
            string dir = Server.MapPath(vdir);
            string vpath = vdir + "二维码图片.jpeg";
            string imgPath = Server.MapPath(vpath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (System.IO.File.Exists(imgPath))
            {
                System.IO.File.Delete(imgPath);
            }

            bmp.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            return vpath;
        }

        protected IList<SmartBox.Console.Common.Entities.SMC_Unit> GetUnitData()
        {
            List<SmartBox.Console.Common.Entities.SMC_Unit> units = new List<Common.Entities.SMC_Unit>();
            SmartBox.Console.Common.Entities.SMC_Unit unitDefault = new Common.Entities.SMC_Unit();
            unitDefault.Unit_Name = "请选择单位";
            unitDefault.Unit_ID = "";
            units.Add(unitDefault);



            if (this.IsSystemManager)
            {
                IList<SmartBox.Console.Common.Entities.SMC_Unit> _units = Bo.BoFactory.GetSMC_UnitBo.GetAllUnits();
                units.AddRange(_units);
            }
            else
            {

                SmartBox.Console.Common.Entities.SMC_Unit unit = Bo.BoFactory.GetSMC_UnitBo.Get(this.CurrentUser.UnitCode);
                if (unit != null)
                    units.Add(unit);
            }
            return units;
        }

        # region 内外网同步方法
        

        public bool _SyncPackageExt(SMC_PackageExt ext)
        {
            Hashtable r = new Hashtable();
            int batch_no = BoFactory.GetSMC_AutoTableIDBo.GetMaxId("SMC_PackageExtSyncToOutside", "sync_bat_no");
            batch_no++;
            SyncPackageExtToAppCenter(ext, r, batch_no);
            return (bool)r["r"];
        }

        public ActionResult SyncPackageExts(string ids)
        {
            Hashtable r = new Hashtable();
            string[] ides = ids.Split(",".ToCharArray());
            List<int> pe_ids = new List<int>();
            foreach (string id in ides)
            {
                if (!String.IsNullOrEmpty(id))
                    pe_ids.Add(int.Parse(id));
            }
            _SyncPackageExts(pe_ids, r);
            return Json(r);
        }
        protected void _SyncPackageExts(List<int> pe_ids, Hashtable r)
        {
            int batch_no = BoFactory.GetSMC_AutoTableIDBo.GetMaxId("SMC_PackageExtSyncToOutside", "sync_bat_no");
            batch_no++;
            foreach (int pe_id in pe_ids) //同步选中的内容
            {
                SMC_PackageExt ext = BoFactory.GetVersionTrackBo.GetPackageExt(pe_id.ToString());
                SyncPackageExtToAppCenter(ext, r, batch_no);
            }
        }

        public void SyncPackages(SmartBox.Console.Bo.AppCenter.AppCenterBO bo, Service.ApplicationCenterWS.WebService ws, List<int> pe_ids)
        {            
            Hashtable r = new Hashtable();
            string smartbox_console_path = HttpRuntime.AppDomainAppPath;
            _SyncPackageExts(pe_ids, r);
        }

        protected bool DeleteAppCenterPackageExt(int pe_id)
        {
            Service.ApplicationCenterWS.WebService ws = new Service.ApplicationCenterWS.WebService();
            return ws.DeletePackageExt(pe_id);
        }

        protected void SyncPackageExtToAppCenter(SMC_PackageExt ext, Hashtable r, int batch_no)
        {
            SmartBox.Console.Bo.AppCenter.AppCenterBO bo = Bo.BoFactory.GetAppCenterBO;
            Service.ApplicationCenterWS.WebService ws = new Service.ApplicationCenterWS.WebService();

            string smartbox_console_path = HttpRuntime.AppDomainAppPath;
            Service.ApplicationCenterWS.SMC_PackageExt entity = CopyPackageExt(ext);
            bool flag = true;
            //Log4NetHelper.Info("wsurl:" + ws.Url);

            try
            {
                //同步安装包到外网应用中心
                ws.PackageExtSync(entity);

                Service.ApplicationCenterWS.SMC_PackageFAQ faqEntity = new Service.ApplicationCenterWS.SMC_PackageFAQ();


                //同步应用手册到外网应用中心
                IList<SMC_PackageManual> manuals = bo.GetPackageManuals(entity.pe_id);
                foreach (SMC_PackageManual _manual in manuals)
                {
                    Service.ApplicationCenterWS.SMC_PackageManual manual = CopyPackageManual(_manual);
                    bool f = ws.PackageManualSync(manual);
                    flag = flag && f;
                }

                //同步截图表到外网应用中心
                IList<SMC_PackagePicture> pics = bo.GetPackagePictures(entity.pe_id);

                if (pics.Count > 0)
                {
                    foreach (SMC_PackagePicture _pic in pics)
                    {
                        Service.ApplicationCenterWS.SMC_PackagePicture pic = CopyPackagePicture(_pic);
                        bool f = ws.PackagePictuerSync(pic);
                        flag = flag && f;
                    }
                }

                //同步附件到外网应用中心
                string entityFolder = smartbox_console_path + "\\PackageExt\\" + ext.pe_id;
                if (Directory.Exists(entityFolder))
                {
                    string[] files = System.IO.Directory.GetFiles(entityFolder);

                    List<Service.ApplicationCenterWS.FileEntity> fs = new List<Service.ApplicationCenterWS.FileEntity>();
                    foreach (string filePath in files)
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            FileStream s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                            Service.ApplicationCenterWS.FileEntity fEntity = new Service.ApplicationCenterWS.FileEntity();
                            fEntity.Content = new byte[s.Length];
                            s.Read(fEntity.Content, 0, (int)s.Length);
                            s.Close();
                            s.Dispose();
                            fEntity.FileName = Path.GetFileName(filePath);
                            fs.Add(fEntity);
                        }
                    }
                    bool f = true;
                    if (fs.Count > 0)
                    {
                        f = ws.PackageFilesSync(fs.ToArray(), ext.pe_id);
                    }
                    flag = flag && f;
                }
                r["r"] = flag;
                r["d"] = "同步" + (flag ? "成功！" : "失败！");
                //同步成功     
                ext.pe_SyncStatus = flag ? 1 : 2;
                BoFactory.GetVersionTrackBo.UpdatePackageExt(ext);

                SMC_PackageExtSyncToOutside sync = new SMC_PackageExtSyncToOutside();
                sync.sync_bat_no = batch_no;
                sync.pe_id = ext.pe_id;
                sync.pe_name = ext.pe_Name;
                sync.sync_status = true;
                sync.sync_time = DateTime.Now;
                sync.description = "同步成功";
                BoFactory.GetSMC_PackageExtSyncToOutsideBO.Insert(sync);

            }
            catch (Exception xe)
            {
                Log4NetHelper.Error(xe);
                //同步失败                   
                BoFactory.GetVersionTrackBo.UpdatePackageExt(ext);

                SMC_PackageExtSyncToOutside sync = new SMC_PackageExtSyncToOutside();
                sync.sync_bat_no = batch_no;
                sync.pe_id = ext.pe_id;
                sync.pe_name = ext.pe_Name;
                sync.sync_status = false;
                sync.sync_time = DateTime.Now;
                sync.description = xe.Message;
                BoFactory.GetSMC_PackageExtSyncToOutsideBO.Insert(sync);
                r["r"] = false;
                r["d"] = "同步失败！";
            }
        }

        public Service.ApplicationCenterWS.SMC_Unit CopySMC_Unit(SMC_Unit unit)
        {
            Service.ApplicationCenterWS.SMC_Unit entity = new Service.ApplicationCenterWS.SMC_Unit();
            entity.Unit_CreatedTime = unit.Unit_CreatedTime;
            entity.Unit_CreatedUser = unit.Unit_CreatedUser;
            entity.Unit_Demo = unit.Unit_Demo;
            entity.Unit_ID = unit.Unit_ID;
            entity.Unit_Name = unit.Unit_Name;
            entity.Unit_Path = unit.Unit_Path;
            entity.Unit_Sequence = unit.Unit_Sequence;
            entity.Unit_UpdateTime = unit.Unit_UpdateTime;
            entity.Unit_UpdateUser = unit.Unit_UpdateUser;
            entity.Upper_Unit_ID = unit.Upper_Unit_ID;

            return entity;
        }

        public Service.ApplicationCenterWS.SMC_PackageFAQ CopyPackageFAQ(SMC_PackageFAQ faq)
        {
            Service.ApplicationCenterWS.SMC_PackageFAQ entity = new Service.ApplicationCenterWS.SMC_PackageFAQ();
            entity.pe_id = faq.pe_id;
            entity.pf_answer = faq.pf_answer;
            entity.pf_askdate = faq.pf_askdate;
            entity.pf_askemail = faq.pf_askemail;
            entity.pf_askmobile = faq.pf_askmobile;
            entity.pf_id = faq.pf_id;
            entity.pf_peplyman = faq.pf_peplyman;
            entity.pf_question = faq.pf_question;
            entity.pf_uid = faq.pf_uid;
            entity.pf_uname = faq.pf_uname;

            return entity;
        }

        public Service.ApplicationCenterWS.SMC_User CopySMC_User(SMC_User user)
        {
            Service.ApplicationCenterWS.SMC_User entity = new Service.ApplicationCenterWS.SMC_User();
            entity.U_CREATEDDATE = user.U_CREATEDDATE;
            entity.U_ID = user.U_ID;
            entity.U_NAME = user.U_NAME;
            entity.U_PASSWORD = user.U_PASSWORD;
            entity.U_UID = user.U_UID;
            entity.U_UNITCODE = user.U_UNITCODE;

            return entity;
        }

        public Service.ApplicationCenterWS.SMC_PackageManual CopyPackageManual(SMC_PackageManual manual)
        {
            Service.ApplicationCenterWS.SMC_PackageManual entity = new Service.ApplicationCenterWS.SMC_PackageManual();
            entity.pe_id = manual.pe_id;
            entity.pm_createdtime = manual.pm_createdtime;
            entity.pm_id = manual.pm_id;
            entity.pm_name = manual.pm_name;
            entity.pm_updatetime = manual.pm_updatetime;
            entity.pm_url = manual.pm_url;
            return entity;
        }
        public Service.ApplicationCenterWS.SMC_PackagePicture CopyPackagePicture(SMC_PackagePicture pic)
        {
            Service.ApplicationCenterWS.SMC_PackagePicture entity = new Service.ApplicationCenterWS.SMC_PackagePicture();
            entity.pe_id = pic.pe_id;
            entity.pp_CreatedDate = pic.pp_CreatedDate;
            entity.pp_desc = pic.pp_desc;
            entity.pp_id = pic.pp_id;
            entity.pp_path = pic.pp_path;
            entity.pp_title = pic.pp_title;
            return entity;
        }
        protected Service.ApplicationCenterWS.SMC_PackageExt CopyPackageExt(SMC_PackageExt ext)
        {
            Service.ApplicationCenterWS.SMC_PackageExt entity = new Service.ApplicationCenterWS.SMC_PackageExt();
            entity.pe_2dPictureUrl = ext.pe_2dPictureUrl;
            entity.pe_BuildVer = ext.pe_BuildVer;
            entity.pe_Category = ext.pe_Category;
            entity.pe_CategoryID = ext.pe_CategoryID;
            entity.pe_ClientType = ext.pe_ClientType;
            entity.pe_CreatedTime = ext.pe_CreatedTime;
            entity.pe_CreateUid = ext.pe_CreateUid;
            entity.pe_Description = ext.pe_Description;
            entity.pe_DisplayName = ext.pe_DisplayName;
            entity.pe_DownCount = ext.pe_DownCount;
            entity.pe_DownloadUri = ext.pe_DownloadUri;
            entity.pe_Firmware = ext.pe_Firmware;
            entity.pe_id = ext.pe_id;
            entity.pe_IsBB = ext.pe_IsBB;
            entity.pe_IsTJ = ext.pe_IsTJ;
            entity.pe_Name = ext.pe_Name;
            entity.pe_PictureUrl = ext.pe_PictureUrl;
            entity.pe_Size = ext.pe_Size;
            entity.pe_UnitCode = ext.pe_UnitCode;
            entity.pe_UnitName = ext.pe_UnitName;
            entity.pe_UpdateTime = ext.pe_UpdateTime;
            entity.pe_UpdateUid = ext.pe_UpdateUid;
            entity.pe_Version = ext.pe_Version;
            entity.TableID = ext.TableID;
            entity.TableName = ext.TableName;
            entity.pe_UsefulStstus = ext.pe_UsefulStstus;
            return entity;
        }

        public void SyncUnits(SmartBox.Console.Bo.AppCenter.AppCenterBO bo, Service.ApplicationCenterWS.WebService ws)
        {
            IList<SMC_Unit> units = bo.GetNeedSyncSMC_Units();
            Service.ApplicationCenterWS.SMC_Unit[] _units = new Service.ApplicationCenterWS.SMC_Unit[units.Count];
            for (int i = 0; i < units.Count; ++i)
            {
                _units[i] = CopySMC_Unit(units[i]);
            }
            ws.SMC_UnitSync(_units.ToArray());
        }

        public void SyncUsers(SmartBox.Console.Bo.AppCenter.AppCenterBO bo, Service.ApplicationCenterWS.WebService ws)
        {
            IList<SMC_User> users = bo.GetNeedSyncSMC_Users();
            Service.ApplicationCenterWS.SMC_User[] _users = new Service.ApplicationCenterWS.SMC_User[users.Count];
            for (int i = 0; i < users.Count; ++i)
            {
                _users[i] = CopySMC_User(users[i]);
            }
            ws.SMC_UserSync(_users.ToArray());
        }



        #endregion
    }
}