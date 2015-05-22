using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Beyondbit.MVC;
using SmartBox.Console.Common;
using SmartBox.Console.Bo;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common.Entities.Search;
using ICSharpCode.SharpZipLib.Zip;
using ConvertPlist;
using System.Xml;
using System.IO;
using Beyondbit.Framework.Core.Proxy;
using System.Configuration;

namespace SmartBox.Console.Web.Controllers
{
    public class IOSOutsideAppManageController : MyControllerBase
    {
        //
        // GET: /IOSOutsideAppManage/

        public ActionResult IOSOutsideAppManage()
        {
            return View();
        }

        public JsonResult QueryIOSOutsideAppList(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            try
            {
                data = BoFactory.GetVersionTrackBo.QueryIOSOutsideAppList(view);
            }
            catch { }

            return Json(data);
        }

        public ActionResult EditIOSOutsideApp(string id)
        {
            IOSOutsideApp app;
            id = Request.QueryString["id"];

            if (string.IsNullOrEmpty(id))
            {
                app = new IOSOutsideApp();
            }
            else
            {
                app = BoFactory.GetVersionTrackBo.GetIOSOutsideApp(id);
            }
            SMC_PackageExt ext = null;
            if (!String.IsNullOrEmpty(id))
            {
                List<KeyValuePair<string, object>> objs = new List<KeyValuePair<string, object>>();
                objs.Add(new KeyValuePair<string, object>("TableName", "IOSOutsideApplication"));
                objs.Add(new KeyValuePair<string, object>("TableID", id));
                ext = Bo.BoFactory.GetSMC_PackageExtBO.Get(objs);
            }

            //获取ClientType,只包含ios类型
            var clientTypes = BoFactory.GetVersionTrackBo.QueryClientTypeList();
            IList<ClientTypes> cTypes = new List<ClientTypes>();
            foreach (var cT in clientTypes)
            {
                if (cT.ClientType.ToLower().Contains("ios"))
                    cTypes.Add(cT);
            }

            List<SelectListItem> unitList = new List<SelectListItem>();
            //获取当前用户的本单位
            IList<SmartBox.Console.Common.Entities.SMC_Unit> js = GetUnitData();


            foreach (SmartBox.Console.Common.Entities.SMC_Unit r in js)
            {
                if (r == null)
                    continue;
                //unitList.Add(new SelectListItem { Text = "全局", Value = "" });
                unitList.Add(new SelectListItem { Text = r.Unit_Name, Value = r.Unit_ID });
            }

            List<SelectListItem> IsRecom = new List<SelectListItem>();
            IsRecom.Add(new SelectListItem { Text = "推荐", Value = "1", Selected = true });
            IsRecom.Add(new SelectListItem { Text = "不推荐", Value = "0" });

            List<SelectListItem> IsMust = new List<SelectListItem>();
            IsMust.Add(new SelectListItem { Text = "必备", Value = "1", Selected = true });
            IsMust.Add(new SelectListItem { Text = "不必备", Value = "0" });
            ViewData["IsRecom"] = IsRecom;
            ViewData["IsMust"] = IsMust;

            ViewData["Unit"] = unitList;
            ViewData["Application"] = BoFactory.GetVersionTrackBo.QueryApplicationList().ToDataTable();
            ViewData["ClientType"] = cTypes.ToDataTable();

            SelectHelper ddlApplicationSelect = new SelectHelper(BoFactory.GetVersionTrackBo.QueryApplicationCategoryList().ToDataTable());
            ViewData["Cate"] = ddlApplicationSelect.GetSelectList("DisplayName", "ID", false);
            ViewData["DispName"] = (ext == null ? "" : ext.pe_DisplayName);
            return View(app);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditIOSOutsideApp(IOSOutsideApp webApplication)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            webApplication.UpdateTime = DateTime.Now;
            webApplication.UpdateUid = CurrentUser.UserUId;
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(webApplication.ID)) || webApplication.ID == 0)
                {//新增
                    webApplication.CreateTime = DateTime.Now;
                    webApplication.CreateUid = CurrentUser.UserUId;
                    BoFactory.GetVersionTrackBo.InsertIOSOutSideApp(webApplication);
                }
                else
                {
                    BoFactory.GetVersionTrackBo.UpdateIOSOutSideApp(webApplication);
                }

                SMC_PackageExt packageExt = new SMC_PackageExt();
                packageExt.pe_id = BoFactory.GetVersionTrackBo.GetMaxPackageExtId() + 1;
                packageExt.pe_ClientType = webApplication.ClientType;

                packageExt.pe_IsTJ = Request.Form["IsRecom"];
                packageExt.pe_IsBB = Request.Form["IsMust"];
                //FileInfo fi = new FileInfo(saveFilePath);
                packageExt.pe_Size = 0;// (int)fi.Length;
                packageExt.TableName = "IOSOutsideApplication";
                packageExt.TableID = webApplication.ID;
                packageExt.pe_UnitCode = Request.Form["Unit"];
                packageExt.pe_CategoryID = Request.Form["Cate"];
                packageExt.pe_Category = BoFactory.GetVersionTrackBo.GetApplicationCategory(Request.Form["Cate"]).DisplayName;
                packageExt.pe_ApplicationCode = Request.Form["AppID"];
                
                if (!String.IsNullOrEmpty(packageExt.pe_ApplicationCode))
                {
                    string[] appIds = packageExt.pe_ApplicationCode.Split(",".ToCharArray());
                    string appNames = "";
                    foreach (string appid in appIds)
                    {
                        Application application = Bo.BoFactory.GetVersionTrackBo.GetApplication(appid);
                        if (application != null)
                        {
                            appNames += application.DisplayName + ",";
                        }
                    }
                    packageExt.pe_ApplicationName = appNames;
                }

                packageExt.pe_UnitName = BoFactory.GetSMC_UnitBo.Get(packageExt.pe_UnitCode).Unit_Name;
                packageExt.pe_CreateUid = CurrentUser.UserUId;
                packageExt.pe_CreatedTime = DateTime.Now;
                packageExt.pe_UpdateUid = CurrentUser.UserUId;
                packageExt.pe_UpdateTime = DateTime.Now;
                packageExt.pe_Version = Request.Form["Version"];// form["packageVersion"];
                packageExt.pe_BuildVer = Request.Form["BuildVer"];// form["packageBuildVer"];
                packageExt.pe_Name = Request.Form["DisplayName"];// form["packageDisplayName"];
                packageExt.pe_DisplayName = Request.Form["DisplayName"];// form["packageDisplayName"];
                packageExt.pe_Description = "";// form["packageDescription"];
                packageExt.pe_Firmware = Request.Form["Firmware"];
                packageExt.pe_DownloadUri = Request.Form["Uri"];

                string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径
                string pDir = SAVEOUTPATH + @"\" + packageExt.pe_id.ToString();//相对路径 用安装包ID做文件夹名
                string saveDir = conSolePath + pDir;

                //生成下载url,并生成二维码
                string url = packageExt.pe_DownloadUri;
                string uri = base.Save2DPicture(packageExt.pe_id, url);
                packageExt.pe_2dPictureUrl = uri;

                int i = Request.Files.Count;
                string iconSavePath = "";
                if (i > 0)
                {
                    //保存安装包图标
                    HttpPostedFileBase icon = Request.Files[0];
                    string iconFileName = "AppIcon.png";
                    iconSavePath = System.IO.Path.Combine(saveDir, iconFileName);
                    if (System.IO.File.Exists(iconSavePath))
                    {
                        System.IO.File.Delete(iconSavePath);
                    }
                    icon.SaveAs(iconSavePath);

                    packageExt.pe_PictureUrl = @"~/" + pDir + "/AppIcon.png";
                    packageExt.pe_PictureUrl = packageExt.pe_PictureUrl.Replace(@"\\", "/");
                }

                GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("app_sj_need_auth");
                if (parm.ConfigValue == "1")
                {
                    //需要审核
                    packageExt.pe_AuthStatus = 0;//待审核
                    packageExt.pe_AuthSubmitTime = DateTime.Now;
                    packageExt.pe_AuthSubmitName = CurrentUser.FullName;
                    packageExt.pe_AuthSubmitUID = CurrentUser.UserUId;
                    packageExt.pe_Direction = "发布";
                    packageExt.pe_Type = "ios_out";
                    packageExt.pe_UsefulStstus = "0";
                    packageExt.pe_UsefulTime = DateTime.Now;
                    packageExt.pe_UsefulOperatorUID = CurrentUser.UserUId;
                    packageExt.pe_UsefulOperatorName = CurrentUser.FullName;
                }
                else
                {
                    packageExt.pe_AuthStatus = 1;//审核通过
                    packageExt.pe_AuthSubmitTime = DateTime.Now;
                    packageExt.pe_AuthTime = DateTime.Now;
                    packageExt.pe_AuthSubmitName = CurrentUser.FullName;
                    packageExt.pe_AuthSubmitUID = CurrentUser.UserUId;
                    packageExt.pe_AuthManUID = CurrentUser.UserUId;
                    packageExt.pe_AuthMan = CurrentUser.FullName;
                    packageExt.pe_SyncStatus = 0;
                    packageExt.pe_Direction = "发布";
                    packageExt.pe_Type = "ios_out";
                    packageExt.pe_UsefulStstus = "1";
                    packageExt.pe_UsefulTime = DateTime.Now;
                    packageExt.pe_UsefulOperatorUID = CurrentUser.UserUId;
                    packageExt.pe_UsefulOperatorName = CurrentUser.FullName;
                }

                BoFactory.GetVersionTrackBo.InsertPackageExt(packageExt);
                if (parm.ConfigValue == "0")
                {
                    //不需要审核,直接同步到应用中心
                    //同步至外网数据
                    _SyncPackageExt(packageExt);
                }
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message.Replace("'", "`");
                Log4NetHelper.Error(ex);
            }
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteIOSOutsideApp(string id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                BoFactory.GetVersionTrackBo.DeleteIOSOutSideApp(id);
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }
            return Json(data);
        }

    }
}
