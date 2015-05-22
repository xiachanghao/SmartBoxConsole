using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using System.IO;
using Beyondbit.Framework.Core.Proxy;

namespace SyncInsideDataToOutside
{
    public class SyncProgram
    {
        static OutsideWS.SMC_Unit CopySMC_Unit(SMC_Unit unit)
        {
            OutsideWS.SMC_Unit entity = new OutsideWS.SMC_Unit();
            entity.Unit_CreatedTime = unit.Unit_CreatedTime;
            entity.Unit_CreatedUser = unit.Unit_CreatedUser;
            entity.Unit_Demo = unit.Unit_Demo;
            entity.Unit_ID = Convert.ToInt32(unit.Unit_ID);
            entity.Unit_Name = unit.Unit_Name;
            entity.Unit_Path = unit.Unit_Path;
            entity.Unit_Sequence = unit.Unit_Sequence;
            entity.Unit_UpdateTime = unit.Unit_UpdateTime;
            entity.Unit_UpdateUser = unit.Unit_UpdateUser;
            entity.Upper_Unit_ID = Convert.ToInt32(unit.Upper_Unit_ID);

            return entity;
        }

        static OutsideWS.SMC_PackageFAQ CopyPackageFAQ(SMC_PackageFAQ faq)
        {
            OutsideWS.SMC_PackageFAQ entity = new OutsideWS.SMC_PackageFAQ();
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

        static OutsideWS.SMC_User CopySMC_User(SMC_User user)
        {
            OutsideWS.SMC_User entity = new OutsideWS.SMC_User();
            entity.U_CREATEDDATE = user.U_CREATEDDATE;
            entity.U_ID = user.U_ID;
            entity.U_NAME = user.U_NAME;
            entity.U_PASSWORD = user.U_PASSWORD;
            entity.U_UID = user.U_UID;
            entity.U_UNITCODE = user.U_UNITCODE;

            return entity;
        }

        static OutsideWS.SMC_PackageManual CopyPackageManual(SMC_PackageManual manual)
        {
            OutsideWS.SMC_PackageManual entity = new OutsideWS.SMC_PackageManual();
            entity.pe_id = manual.pe_id;
            entity.pm_createdtime = manual.pm_createdtime;
            entity.pm_id = manual.pm_id;
            entity.pm_name = manual.pm_name;
            entity.pm_updatetime = manual.pm_updatetime;
            entity.pm_url = manual.pm_url;
            return entity;
        }
        static OutsideWS.SMC_PackagePicture CopyPackagePicture(SMC_PackagePicture pic)
        {
            OutsideWS.SMC_PackagePicture entity = new OutsideWS.SMC_PackagePicture();
            entity.pe_id = pic.pe_id;
            entity.pp_CreatedDate = pic.pp_CreatedDate;
            entity.pp_desc = pic.pp_desc;
            entity.pp_id = pic.pp_id;
            entity.pp_path = pic.pp_path;
            entity.pp_title = pic.pp_title;
            return entity;
        }
        static OutsideWS.SMC_PackageExt CopyPackageExt(SMC_PackageExt ext)
        {
            OutsideWS.SMC_PackageExt entity = new OutsideWS.SMC_PackageExt();
            entity.pe_2dPictureUrl = ext.pe_2dPictureUrl;
            entity.pe_BuildVer = ext.pe_BuildVer;
            entity.pe_Category = ext.pe_Category;
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
            return entity;
        }

        static void Main(string[] args)
        {
            IProxy proxy = ProxyFactory.CreateProxy();
            SmartBox.Console.Bo.AppCenter.AppCenterBO bo = proxy.CreateObject<SmartBox.Console.Bo.AppCenter.AppCenterBO>();

            OutsideWS.WebService ws = new OutsideWS.WebService();
            //SyncInsideDataToOutside.SyncProgram.SyncPackages(bo, ws);
            SyncInsideDataToOutside.SyncProgram.SyncUnits(bo, ws);
            SyncInsideDataToOutside.SyncProgram.SyncUsers(bo, ws);
        }

        public static void SyncUnits(SmartBox.Console.Bo.AppCenter.AppCenterBO bo, OutsideWS.WebService ws)
        {
            IList<SMC_Unit> units = bo.GetNeedSyncSMC_Units();
            OutsideWS.SMC_Unit[] _units = new OutsideWS.SMC_Unit[units.Count];
            for (int i = 0; i < units.Count; ++i)
            {
                _units[i] = CopySMC_Unit(units[i]);
            }
            ws.SMC_UnitSync(_units.ToArray());
        }

        public static void SyncUsers(SmartBox.Console.Bo.AppCenter.AppCenterBO bo, OutsideWS.WebService ws)
        {
            IList<SMC_User> users = bo.GetNeedSyncSMC_Users();
            OutsideWS.SMC_User[] _users = new OutsideWS.SMC_User[users.Count];
            for (int i = 0; i < users.Count; ++i)
            {
                _users[i] = CopySMC_User(users[i]);
            }
            ws.SMC_UserSync(_users.ToArray());
        }

        public static void SyncPackages(SmartBox.Console.Bo.AppCenter.AppCenterBO bo, OutsideWS.WebService ws)
        {
            string smartbox_console_path = System.Configuration.ConfigurationManager.AppSettings["smartbox_console_path"];
            IList<SMC_PackageExt> data = bo.GetNeedSyncPackageList();

            foreach (SMC_PackageExt ext in data)
            {
                OutsideWS.SMC_PackageExt entity = CopyPackageExt(ext);

                //同步安装包到外网应用中心
                ws.PackageExtSync(entity);

                OutsideWS.SMC_PackageFAQ faqEntity = new OutsideWS.SMC_PackageFAQ();

                //同步问题反馈到外网应用中心
                //IList<SMC_PackageFAQ> faqs = bo.GetPackageFAQs(entity.pe_id);
                //foreach (SMC_PackageFAQ _faq in faqs)
                //{
                //    OutsideWS.SMC_PackageFAQ faq = CopyPackageFAQ(_faq);
                //    ws.PackageFAQSync(faq);
                //}

                //同步应用手册到外网应用中心
                IList<SMC_PackageManual> manuals = bo.GetPackageManuals(entity.pe_id);
                foreach (SMC_PackageManual _manual in manuals)
                {
                    OutsideWS.SMC_PackageManual manual = CopyPackageManual(_manual);
                    ws.PackageManualSync(manual);
                }

                //同步截图表到外网应用中心
                IList<SMC_PackagePicture> pics = bo.GetPackagePictures(entity.pe_id);
                foreach (SMC_PackagePicture _pic in pics)
                {
                    OutsideWS.SMC_PackagePicture pic = CopyPackagePicture(_pic);
                    ws.PackagePictuerSync(pic);
                }

                //同步附件到外网应用中心
                string entityFolder = smartbox_console_path + "\\PackageExt\\" + ext.pe_id;
                if (Directory.Exists(entityFolder))
                {
                    string[] files = System.IO.Directory.GetFiles(entityFolder);

                    List<OutsideWS.FileEntity> fs = new List<OutsideWS.FileEntity>();
                    foreach (string filePath in files)
                    {
                        if (File.Exists(filePath))
                        {
                            FileStream s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                            OutsideWS.FileEntity fEntity = new OutsideWS.FileEntity();
                            fEntity.Content = new byte[s.Length];
                            s.Read(fEntity.Content, 0, (int)s.Length);
                            fEntity.FileName = Path.GetFileName(filePath);
                            fs.Add(fEntity);
                        }
                    }

                    try
                    {
                        ws.PackageFilesSync(fs.ToArray(), ext.pe_id);
                    }
                    catch(Exception ex) {
                    }
                }
            }
        }
    }
}
