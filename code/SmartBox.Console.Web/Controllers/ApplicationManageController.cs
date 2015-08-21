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
using System.Net;
using System.Text;
using Beyondbit.BUA.Client;
using ThoughtWorks.QRCode.Codec;
using Beyondbit.Framework.Core.Proxy;
using System.Configuration;
using System.Collections;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SmartBox.Console.Web.Controllers
{
    public class ApplicationManageController : MyControllerBase
    {
        //
        // GET: /ApplicationManage/

        #region 应用管理
        public ActionResult ApplicationManage()
        {
            return View();
        }
        public ActionResult ApplicationManage_()
        {
            return View();
        }

        public JsonResult QueryApplicationList(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            JsonFlexiGridData ndata = new JsonFlexiGridData();
            PageView view = new PageView(form);
            string uid = CurrentUser.UserUId;
            Dictionary<string, string> re = BoFactory.GetSMC_UserListBo.GetUnitByUL_UID(uid);
            string unitcode = "";
            if (re != null)
            {
                foreach (KeyValuePair<string, string> p in re)
                {
                    unitcode = p.Value;
                }
            }

            try
            {
                data = BoFactory.GetVersionTrackBo.QueryApplicationList(view, unitcode);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }

            //if (!BoFactory.GetVersionTrackBo.IsSystemManager(uid))
            //{
            //    //非超级管理员,实行过滤
            //    foreach (FlexiGridRow r in data.rows)
            //    {
            //        //加入用户创建的app
            //        var createId = r.cell[8];//创建者uid
            //        if (createId == uid)
            //        {
            //            ndata.rows.Add(r);
            //            continue;
            //        }

            //        //加入用户所属单位的app
            //        Dictionary<string, int> unitInfo = BoFactory.GetSMC_UserListBo.GetUnitByUL_UID(CurrentUser.UserUId);
            //        string unit = null;
            //        if (unitInfo.Count > 0)
            //        {
            //            unit = unitInfo.ElementAt(0).Value.ToString();
            //        }

            //        //if (unit == r.cell[13])
            //        //{
            //        //    ndata.rows.Add(r);
            //        //    continue;
            //        //}

            //    }
            //    return Json(data);
            //}
            //else
            {
                return Json(data);
            }
        }

        public ActionResult EditAppPackage(string pe_id)
        {
            SMC_PackageExt ext = Bo.BoFactory.GetSMC_PackageExtBO.Get(pe_id);
            if (ext == null)
            {
                ext = new SMC_PackageExt();
            }

            string packageName = "";
            string packageType = "";

            if (ext.TableID == 0)
            {
                Package4AI package = null;
                string path = Server.MapPath("~/PackageSerialization/" + pe_id + "/package.xml");
                if (System.IO.File.Exists(path))
                {
                    using (Stream fStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer xmlFormat = new XmlSerializer(typeof(Package4AI));
                        object o = xmlFormat.Deserialize(fStream);
                        package = (Package4AI)o;
                        fStream.Close();
                        fStream.Dispose();
                    }
                }

                if (package != null)
                {
                    packageName = package.Name;
                    packageType = package.Type;
                }
            }
            ViewData["packageName"] = packageName;
            ViewData["packageType"] = packageType;
            return View(ext);
        }

        public ActionResult EditApplication(string id)
        {
            List<SelectListItem> unitList = new List<SelectListItem>();
            IList<SmartBox.Console.Common.Entities.SMC_Unit> js = GetUnitData();

            foreach (SmartBox.Console.Common.Entities.SMC_Unit r in js)
            {
                //unitList.Add(new SelectListItem { Text = "全局", Value = "" });
                unitList.Add(new SelectListItem { Text = r.Unit_Name, Value = r.Unit_ID });
            }
            //ViewData["unitData"] = Newtonsoft.Json.JsonConvert.SerializeObject(js);
            //获取当前用户的本单位
            //Dictionary<string, string> unitInfo = BoFactory.GetSMC_UserListBo.GetUnitByUL_UID(CurrentUser.UserUId);
            //if (unitInfo.Count > 0)
            //{
            //    string unitName = "本单位-" + unitInfo.ElementAt(0).Key;
            //    string unitId = unitInfo.ElementAt(0).Value;
            //    unitList.Add(new SelectListItem { Text = unitName, Value = unitId, Selected = true });
            //}

            //if (BoFactory.GetVersionTrackBo.IsSystemManager(CurrentUser.UserUId))
            //{
            //    //加入所有单位列表
            //    PageView view = new PageView();
            //    view.PageSize = 15;
            //    JsonFlexiGridData units = BoFactory.GetSMC_UnitBo.QueryUnitByUpperUnitCode(view, "");
            //    foreach (FlexiGridRow r in units.rows)
            //    {
            //        //unitList.Add(new SelectListItem { Text = "全局", Value = "" });
            //        unitList.Add(new SelectListItem { Text = r.cell[1], Value = r.id });
            //    }
            //} 

            Application app;
            SelectHelper ddlPrivilege = new SelectHelper(BoFactory.GetVersionTrackBo.QueryAppPrivilegeList().ToDataTable(), "未指定", "");
            SelectHelper chkCategoryIDs = new SelectHelper(BoFactory.GetVersionTrackBo.QueryApplicationCategoryList().ToDataTable());
            if (string.IsNullOrEmpty(id))
            {
                app = new Application();
            }
            else
            {
                app = BoFactory.GetVersionTrackBo.GetApplication(id);

                foreach (SelectListItem item in unitList)
                {
                    if (item.Value == app.Unit)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(app.CategoryIDs))
            {
                ViewData["CategoryIDs"] = chkCategoryIDs.GetSelectListItem("DisplayName", "ID", false);
            }
            else
            {
                ViewData["CategoryIDs"] = chkCategoryIDs.GetSelectListItem("DisplayName", "ID", app.CategoryIDs.Split(',').ToList(), false);
            }

            ViewData["Privilege"] = ddlPrivilege.GetSelectListItem("DisplayName", "ID", true);
            ViewData["Unit"] = unitList;
            return View(app);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditApplication(Application app)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };

            try
            {
                app.UpdateTime = DateTime.Now;
                app.UpdateUid = CurrentUser.UserUId;
                if (string.IsNullOrEmpty(Convert.ToString(app.ID)) || app.ID == 0)
                {//新增
                    app.CreateTime = DateTime.Now;
                    app.CreateUid = CurrentUser.UserUId;
                    BoFactory.GetVersionTrackBo.InsertApplication(app);
                }
                else
                {//修改
                    BoFactory.GetVersionTrackBo.UpdateApplication(app);
                }
            }
            catch (Exception ex)
            {
                data.Msg = ex.Message;
                data.IsSuccess = false;
                Log4NetHelper.Error(ex);
            }

            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteApplication(string id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                BoFactory.GetVersionTrackBo.DeleteApplication(id);
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
                Log4NetHelper.Error(ex);
            }
            return Json(data);
        }
        #endregion

        #region 权限管理
        public ActionResult PrivilegeManage()
        {
            GlobalParam parm = BoFactory.GetGlobalParamBO.Get("bua_auth_url");
            if (parm == null)
            {
                ViewData["bua_auth_url_err_msg"] = "全局参赛表GlobalParam里缺少bua授权地址配置项，ConfigKey为：bua_auth_url，请在数据库里加入配置项！";
                ViewData["bua_auth_url_err"] = "true";
            }
            else
            {
                ViewData["bua_auth_url"] = parm.ConfigValue;
            }
            return View();
        }

        public ActionResult PrivilegeManage_()
        {
            return View();
        }

        public ActionResult PrivilegeUser()
        {
            return View();
        }

        public JsonResult GetPrivilegeUser(string privilegeCode, int pageSize, int pageIndex)
        {
            Hashtable result = new Hashtable();
            try
            {
                SplitPageResult<PrivilegeUser> users = BoFactory.GetVersionTrackBo.QueryPrivilegeUser(privilegeCode, pageSize, pageIndex);

                result["total"] = users.TotalCount;
                result["rows"] = users.Items;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                result["total"] = 0;
                result["rows"] = null;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult QueryPrivilegeList(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();

            try
            {
                PageView view = new PageView(form);
                data = BoFactory.GetVersionTrackBo.QueryAppPrivilegeList(view);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }

            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AppPrivilegeAddPost(string appid, string appName, string appCode, string buaappid, string buaprivilege, string buaprivilegetext, string enablesync, string syncinterval)
        {
            Hashtable result = new Hashtable();
            try
            {
                result["r"] = true;
                result["d"] = "保存成功！";
                Bo.BoFactory.GetCommonBO.AppPrivilegeAdd(appid, appName, appCode, buaappid, buaprivilege, buaprivilegetext, enablesync, syncinterval);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                result["r"] = false;
                result["d"] = "保存失败！";
            }
            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UnAuthAppPrivilege(string id)
        {
            Hashtable result = new Hashtable();
            try
            {
                result["r"] = true;
                result["d"] = "解除授权成功！";
                Bo.BoFactory.GetCommonBO.UnAuthAppPrivilege(id);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                result["r"] = false;
                result["d"] = "解除授权失败！";
            }
            return Json(result);
        }

        public ActionResult EditAppPrivilege(string id)
        {
            try
            {
                string bua_sys_account = ConfigurationManager.AppSettings["bua_sys_account"];
                if (String.IsNullOrEmpty(bua_sys_account))
                {
                    throw new Exception("bua_sys_account为null，请检查web.config是否存在配置节bua_sys_account");
                }
                App[] apps = ServiceFactory.Instance().GetAuthorizationService().QueryAppByUser(IsSystemManager ? bua_sys_account : this.CurrentUser.UserUId);
                string s = Newtonsoft.Json.JsonConvert.SerializeObject(apps);
                ViewData["apps"] = s;
                //ServiceFactory.Instance().GetAuthorizationService().QueryAllPrivilegeTree("", "", "Privilege");
                string ss = "";// ServiceFactory.Instance().GetAuthorizationService().QuerySubPrivilegeTree(bua_sys_account, "", "Privilege", true);
                ss = ServiceFactory.Instance().GetAuthorizationService().QueryAllPrivilegeTree(bua_sys_account);
                Log4NetHelper.Info("QuerySubPrivilegeTree:" + ss);
                List<Hashtable> privileges = new List<Hashtable>();

                Hashtable p = new Hashtable();
                p.Add("privilegeid", "");
                p.Add("privilegename", "所有人权限");
                privileges.Add(p);

                if (!String.IsNullOrEmpty(ss))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(ss);
                    XmlNodeList nodeList = null;
                    nodeList = xmlDoc.DocumentElement.SelectNodes("node");


                    //for (int i = 0; i < nodeList.Count; ++i)
                    //{
                    //    string privilegeid = xmlDoc.DocumentElement.ChildNodes[i].Attributes["id"].Value;
                    //    string text = xmlDoc.DocumentElement.ChildNodes[i].Attributes["text"].Value;
                    //    Hashtable pri = new Hashtable();
                    //    pri.Add("privilegeid", privilegeid);
                    //    pri.Add("privilegename", text);
                    //    privileges.Add(pri);
                    //}

                    for (int i = 0; i < nodeList.Count; ++i)
                    {
                        string privilegeid = nodeList[i].Attributes["id"].Value;
                        string text = nodeList[i].Attributes["text"].Value;
                        string privilegetype = nodeList[i].Attributes["privilegetype"].Value;
                        if (privilegetype.ToLower() != "privilege")
                            continue;
                        Hashtable pri = new Hashtable();
                        pri.Add("privilegeid", privilegeid);
                        pri.Add("privilegename", text);
                        privileges.Add(pri);
                    }
                    nodeList = xmlDoc.DocumentElement.SelectNodes("node/node");
                    for (int i = 0; i < nodeList.Count; ++i)
                    {
                        string privilegeid = nodeList[i].Attributes["id"].Value;
                        string text = nodeList[i].Attributes["text"].Value;
                        string privilegetype = nodeList[i].Attributes["privilegetype"].Value;
                        if (privilegetype.ToLower() != "privilege")
                            continue;
                        Hashtable pri = new Hashtable();
                        pri.Add("privilegeid", privilegeid);
                        pri.Add("privilegename", text);
                        privileges.Add(pri);
                    }

                    nodeList = xmlDoc.DocumentElement.SelectNodes("node/node/node");
                    for (int i = 0; i < nodeList.Count; ++i)
                    {
                        string privilegeid = nodeList[i].Attributes["id"].Value;
                        string text = nodeList[i].Attributes["text"].Value;
                        string privilegetype = nodeList[i].Attributes["privilegetype"].Value;
                        if (privilegetype.ToLower() != "privilege")
                            continue;
                        Hashtable pri = new Hashtable();
                        pri.Add("privilegeid", privilegeid);
                        pri.Add("privilegename", text);
                        privileges.Add(pri);
                    }

                    nodeList = xmlDoc.DocumentElement.SelectNodes("node/node/node/node");
                    for (int i = 0; i < nodeList.Count; ++i)
                    {
                        string privilegeid = nodeList[i].Attributes["id"].Value;
                        string text = nodeList[i].Attributes["text"].Value;
                        string privilegetype = nodeList[i].Attributes["privilegetype"].Value;
                        if (privilegetype.ToLower() != "privilege")
                            continue;
                        Hashtable pri = new Hashtable();
                        pri.Add("privilegeid", privilegeid);
                        pri.Add("privilegename", text);
                        privileges.Add(pri);
                    }
                }
                else
                {
                    Hashtable pri = new Hashtable();
                    pri.Add("privilegeid", "");
                    pri.Add("privilegename", "请选择");
                    privileges.Add(pri);
                }
                string privilegs = Newtonsoft.Json.JsonConvert.SerializeObject(privileges);
                ViewData["privilegs"] = privilegs;
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
            }
            //AppPrivilege privilege;
            //if (string.IsNullOrEmpty(id))
            //{
            //    privilege = new AppPrivilege();
            //}
            //else
            //{
            //    privilege = BoFactory.GetVersionTrackBo.GetAppPrivilege(id);
            //}
            return View();
        }

        public ActionResult EditAppPrivilege_(string id)
        {
            AppPrivilege privilege;
            if (string.IsNullOrEmpty(id))
            {
                privilege = new AppPrivilege();
            }
            else
            {
                privilege = BoFactory.GetVersionTrackBo.GetAppPrivilege(id);
            }
            return View(privilege);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditAppPrivilege(AppPrivilege privilege)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            privilege.UpdateTime = DateTime.Now;
            privilege.UpdateUid = CurrentUser.UserUId;
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(privilege.ID)) || privilege.ID == 0)
                {//新增
                    privilege.CreateTime = DateTime.Now;
                    privilege.CreateUid = CurrentUser.UserUId;
                    privilege.SyncLastTime = DateTime.Now;
                    privilege.SyncTime = DateTime.Now;
                    BoFactory.GetVersionTrackBo.InsertAppPrivilege(privilege);
                }
                else
                {
                    BoFactory.GetVersionTrackBo.UpdateAppPrivilege(privilege);
                }
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
                Log4NetHelper.Error(ex);
            }
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteAppPrivilege(string id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                BoFactory.GetVersionTrackBo.DeleteAppPrivilege(id);
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
                Log4NetHelper.Error(ex);
            }
            return Json(data);
        }

        private void _AsyncPrivilege(string privilegeId, JsonReturnMessages data)
        {
            if (String.IsNullOrEmpty(privilegeId))
            {
                data.IsSuccess = false;
                data.Msg = "权限代码为空，未同步！";
                return;
            }

            BUAClientConfig config = RuntimeConfig.Instance;
            var backAppCode = config.ApplicationCode;
            try
            {
                if (privilegeId == "all")
                {
                    IList<AppPrivilege> List = BoFactory.GetVersionTrackBo.QueryAppPrivilegeList();
                    foreach (AppPrivilege ap in List)
                    {
                        //config.ApplicationCode = ap.BuaAppCode;
                        var users = ServiceFactory.Instance().GetAuthorizationService().QueryUsersByPrivilege(ap.BuaPrivilegeCode);
                        List<string> uids = new List<string>();
                        foreach (var user in users)
                        {
                            uids.Add(user.UserUid);
                        }
                        BoFactory.GetVersionTrackBo.AsyncPrivilege(ap.ID.ToString(), uids, CurrentUser.UserUId);
                    }
                }
                else
                {
                    var privilege = BoFactory.GetVersionTrackBo.GetAppPrivilege(privilegeId);
                    string privilegeCode = privilege.BuaPrivilegeCode;
                    //config.ApplicationCode = privilege.BuaAppCode;
                    var users = ServiceFactory.Instance().GetAuthorizationService().QueryUsersByPrivilege(privilegeCode);

                    List<string> uids = new List<string>();
                    if (users != null && users.Length > 0)
                        foreach (var user in users)
                        {
                            uids.Add(user.UserUid);
                        }
                    BoFactory.GetVersionTrackBo.AsyncPrivilege(privilegeId, uids, CurrentUser.UserUId);
                    data.IsSuccess = true;
                    data.Msg = "";
                }
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
                Log4NetHelper.Error(ex);
            }
            finally
            {
                config.ApplicationCode = backAppCode;
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AsyncPrivilege(string privilegeId)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "同步成功" };
            try
            {
                _AsyncPrivilege(privilegeId, data);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = "同步失败";
            }
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AsyncSelectedPrivilege(string privilegeIds)
        {
            string[] privelegs = privilegeIds.Split(",".ToCharArray());

            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "" };
            try
            {
                if (privelegs != null && privelegs.Length > 0)
                {
                    bool err = false;
                    string errMsg = "";
                    string okMsg = "";
                    foreach (string pid in privelegs)
                    {
                        JsonReturnMessages _data = new JsonReturnMessages() { IsSuccess = true, Msg = "同步成功" };
                        _AsyncPrivilege(pid, _data);
                        if (!_data.IsSuccess)
                        {
                            if (String.IsNullOrEmpty(_data.Msg))
                                continue;
                            err = true;
                            errMsg += "<br/>权限代码" + pid + "的授权访问用户同步失败，原因：" + _data.Msg + "！";
                        }
                        else
                        {
                            okMsg += "<br/>权限代码" + pid + "的授权访问用户同步成功！";
                        }
                    }

                    data.Msg = okMsg + "<br/>" + errMsg;
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = "同步失败";
            }
            return Json(data);
        }

        #endregion

        public ApplicationManageController()
        {
        }

        public ActionResult ApplicationPackageManage()
        {
            return View();
        }

        public ActionResult ImportPackage()
        {
            return View();
        }

        public JsonResult QueryApplicationPackageList(FormCollection form)
        {
            SearchApp4AI search = new SearchApp4AI(form);
            JsonFlexiGridData data = new JsonFlexiGridData();
            data = BoFactory.GetVersionTrackBo.QueryPackage4AIList(search);
            return Json(data);
        }

        public JsonResult QueryOutPackageList(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryOutPackageList(view);
            return Json(data);
        }

        public ActionResult ApplicationExt()
        {
            return View();
        }

        public ActionResult ApplicationExt_()
        {
            //获取所有内部的pid
            var plists = BoFactory.GetSMC_PackageExtBO.GetNotP4I();
            string ids = "";
            foreach (var p in plists)
            {
                if (p.TableName != "Package4AI")
                    ids += "," + p.pe_id.ToString();
            }
            ViewData["ids"] = ids.TrimStart(',');//所有不需要更新的pid
            return View();
        }

        /// <summary>
        /// 上架
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Operation"></param>
        /// <param name="data"></param>
        public void _SetUserfulStatus(string id, string Operation, JsonReturnMessages data)
        {
            try
            {
                GlobalParam parm = BoFactory.GetGlobalParamBO.Get("app_sj_need_auth");
                bool app_sj_need_auth = parm.ConfigValue == "1";
                SMC_PackageExt ext = BoFactory.GetSMC_PackageExtBO.Get(id);
                //SMC_PackageExt ext = BoFactory.GetVersionTrackBo.GetPackageExt(id);

                switch (Operation)
                {
                    case "ENABLE":
                        ext.pe_Direction = "上架";
                        if (app_sj_need_auth)
                        {
                            //app上架需要审核
                            ext.pe_AuthStatus = 0;
                            ext.pe_AuthSubmitTime = DateTime.Now;
                            ext.pe_AuthSubmitUID = this.CurrentUser.UserUId;
                            ext.pe_AuthSubmitName = this.CurrentUser.FullName;
                            BoFactory.GetSMC_PackageExtBO.Update(ext);
                        }
                        else
                        {
                            ext.pe_UsefulStstus = "1";//上架
                            ext.pe_UsefulOperatorUID = this.CurrentUser.UserUId;
                            ext.pe_UsefulOperatorName = this.CurrentUser.FullName;
                            ext.pe_UsefulTime = DateTime.Now;
                            //app上架不需要审核
                            ext.pe_AuthStatus = 1;
                            BoFactory.GetSMC_PackageExtBO.Update(ext);

                            //内网数据更新
                            BoFactory.GetVersionTrackBo.SetUserfulStatus(id, Operation);

                            //同步至外网数据
                            IProxy proxy = ProxyFactory.CreateProxy();
                            SmartBox.Console.Bo.AppCenter.AppCenterBO bo = proxy.CreateObject<SmartBox.Console.Bo.AppCenter.AppCenterBO>();
                            Service.ApplicationCenterWS.WebService ws = new Service.ApplicationCenterWS.WebService();

                            Service.ApplicationCenterWS.SMC_PackageExt entity = CopyPackageExt(ext);

                            //同步安装包到外网应用中心
                            List<int> ids = new List<int>();
                            ids.Add(entity.pe_id);
                            //ws.PackageExtSync(entity);
                            SyncPackages(bo, ws, ids);
                        }
                        break;
                    case "DISABLE":
                        ext.pe_UsefulStstus = "0";//下架
                        ext.pe_UsefulOperatorUID = this.CurrentUser.UserUId;
                        ext.pe_UsefulOperatorName = this.CurrentUser.FullName;
                        ext.pe_UsefulTime = DateTime.Now;
                        ext.pe_Direction = "下架";
                        BoFactory.GetSMC_PackageExtBO.Update(ext);
                        break;
                }

            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
                Log4NetHelper.Error(ex);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SetUserfulStatus(string id, string Operation)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                _SetUserfulStatus(id, Operation, data);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = "操作失败";
            }
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SetUserfulStatusx(string ids, string Operation)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功！" };
            try
            {
                string[] idx = ids.Split(",".ToCharArray());
                if (idx != null && idx.Length > 0)
                {
                    string err = "id为";
                    bool erred = false;
                    foreach (string id in idx)
                    {
                        JsonReturnMessages _data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
                        _SetUserfulStatus(id, Operation, _data);
                        if (_data.IsSuccess == false)
                        {
                            err += id + "、";
                            erred = true;
                        }
                    }
                    if (erred)
                    {
                        data.Msg += err;
                    }
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.Msg = "操作失败";
                data.IsSuccess = false;
            }
            return Json(data);
        }

        public JsonResult QueryPackageExtAsyncList(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            try
            {
                PageView view = new PageView(form);
                string uid = HttpContext.User.Identity.Name;
                Dictionary<string, string> r = BoFactory.GetSMC_UserListBo.GetUnitByUL_UID(uid);
                string unitcode = "";
                if (r != null)
                {
                    foreach (KeyValuePair<string, string> p in r)
                    {
                        unitcode = p.Value;
                    }
                }

                data = BoFactory.GetVersionTrackBo.QueryPackageExtAsyncList(view, unitcode);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);

            }
            return Json(data);
        }

        public JsonResult QueryPackageAsyncResultList(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            try
            {
                PageView view = new PageView(form);
                string sync_bat_no = Request.QueryString["sync_bat_no"];
                string sync_time_start = Request.QueryString["sync_time_start"];
                string sync_time_end = Request.QueryString["sync_time_end"];
                string sync_status = Request.QueryString["sync_status"];
                string packageName = Request.QueryString["packageName"];
                data = BoFactory.GetSMC_PackageExtSyncToOutsideBO.QueryPackageAsyncResultList(view, sync_bat_no, sync_time_start, sync_time_end, sync_status, packageName);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }
            return Json(data);
        }

        public JsonResult QueryAsyncResultListBUAUserToInside(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            try
            {
                PageView view = new PageView(form);
                string sync_bat_no = Request.QueryString["sync_bat_no"];
                string sync_time_start = Request.QueryString["sync_time_start"];
                string sync_time_end = Request.QueryString["sync_time_end"];
                string sync_status = Request.QueryString["sync_status"];
                string userName = Request.QueryString["userName"];
                data = BoFactory.GetSMC_BUAUserSyncToInsideBO.QueryBUAUserAsyncToInsideResultList(view, sync_bat_no, sync_time_start, sync_time_end, sync_status, userName);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }
            return Json(data);
        }

        public JsonResult QueryAsyncResultListBUAUserToOutside(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();

            try
            {
                PageView view = new PageView(form);
                string sync_bat_no = Request.QueryString["sync_bat_no"];
                string sync_time_start = Request.QueryString["sync_time_start"];
                string sync_time_end = Request.QueryString["sync_time_end"];
                string sync_status = Request.QueryString["sync_status"];
                string userName = Request.QueryString["userName"];
                data = BoFactory.GetSMC_BUAUserSyncToOutsideBO.QueryBUAUserAsyncToOutsideResultList(view, sync_bat_no, sync_time_start, sync_time_end, sync_status, userName);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult QueryNeedImportedPackageList(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            data = BoFactory.GetAppCenterBO.QueryNeedImportedPackageList(view);
            return Json(data);
        }

        public JsonResult QueryPackageExtList(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            string uid = HttpContext.User.Identity.Name;
            Dictionary<string, string> r = BoFactory.GetSMC_UserListBo.GetUnitByUL_UID(uid);
            string unitcode = "";
            if (r != null)
            {
                foreach (KeyValuePair<string, string> p in r)
                {
                    unitcode = p.Value;
                }
            }
            data = BoFactory.GetVersionTrackBo.QueryPackageExtList(view, "Mobile", unitcode);
            return Json(data);
        }

        public ActionResult EditOutPackage(string id)
        {
            id = Request.QueryString["id"];
            SMC_Package4Out package4Out = new SMC_Package4Out();
            try
            {
                package4Out = BoFactory.GetVersionTrackBo.GetPackage4Out(id);
                string clientType = package4Out.ClientType;

                string pe_id = BoFactory.GetVersionTrackBo.GetPeId(id, "SMC_Package4Out");
                SMC_PackageExt pe = BoFactory.GetVersionTrackBo.GetPackageExt(pe_id);
                ViewData["DownUrl"] = pe != null ? pe.pe_DownloadUri : "";
                if (pe != null && !String.IsNullOrEmpty(pe.pe_PictureUrl))
                {
                    ViewData["IconUrl"] = pe.pe_PictureUrl;
                }
                else
                {
                    ViewData["IconUrl"] = "";
                }

                List<SelectListItem> clientTypes = new List<SelectListItem>();
                IList<ClientTypes> ClientTypes = BoFactory.GetVersionTrackBo.QueryClientTypeList();
                foreach (var ct in ClientTypes)
                {
                    clientTypes.Add(new SelectListItem { Text = ct.DisplayName, Value = ct.ClientType, Selected = (clientType.Contains(ct.ClientType)) });
                }


                ViewData["ClientType"] = clientTypes;

                List<SelectListItem> IsRecom = new List<SelectListItem>();
                IsRecom.Add(new SelectListItem { Text = "推荐", Value = "1" });
                IsRecom.Add(new SelectListItem { Text = "不推荐", Value = "0" });

                if (pe.pe_IsTJ == "False")
                {
                    IsRecom[1].Selected = true;
                }
                else
                {
                    IsRecom[0].Selected = true;
                }

                List<SelectListItem> IsMust = new List<SelectListItem>();
                IsMust.Add(new SelectListItem { Text = "必备", Value = "1" });
                IsMust.Add(new SelectListItem { Text = "不必备", Value = "0" });

                if (pe.pe_IsBB == "False")
                {
                    IsMust[1].Selected = true;
                }
                else
                {
                    IsMust[0].Selected = true;
                }

                List<SelectListItem> unitList = new List<SelectListItem>();

                IList<SmartBox.Console.Common.Entities.SMC_Unit> js = GetUnitData();

                foreach (SmartBox.Console.Common.Entities.SMC_Unit r in js)
                {
                    //unitList.Add(new SelectListItem { Text = "全局", Value = "" });
                    unitList.Add(new SelectListItem { Text = r.Unit_Name, Value = r.Unit_ID });
                }

                //获取当前用户的本单位
                //Dictionary<string, string> unitInfo = BoFactory.GetSMC_UserListBo.GetUnitByUL_UID(CurrentUser.UserUId);
                //if (unitInfo.Count > 0)
                //{
                //    string unitName = "本单位-" + unitInfo.ElementAt(0).Key;
                //    string unitId = unitInfo.ElementAt(0).Value;
                //    unitList.Add(new SelectListItem { Text = unitName, Value = unitId, Selected = true });
                //}
                //if (BoFactory.GetVersionTrackBo.IsSystemManager(CurrentUser.UserUId))
                //{
                //    //加入所有单位列表
                //    PageView view = new PageView();
                //    view.PageSize = 15;
                //    JsonFlexiGridData units = BoFactory.GetSMC_UnitBo.QueryUnitByUpperUnitCode(view, "");
                //    foreach (FlexiGridRow r in units.rows)
                //    {
                //        unitList.Add(new SelectListItem { Text = r.cell[1], Value = r.id });
                //    }
                //}

                var ddlApplicationSelect = BoFactory.GetVersionTrackBo.QueryApplicationCategoryList();
                List<SelectListItem> appTypes = new List<SelectListItem>();
                foreach (var ad in ddlApplicationSelect)
                {
                    appTypes.Add(new SelectListItem { Text = ad.DisplayName, Value = ad.ID.ToString(), Selected = (ad.DisplayName == pe.pe_Category) });
                }
                ViewData["AppID"] = appTypes;
                ViewData["pe_IsTJ"] = IsRecom;
                ViewData["pe_IsBB"] = IsMust;
                ViewData["Unit"] = unitList;

            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                Response.Write(ex.Message);
                Response.End();
            }

            return View(package4Out);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditOutPackage(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            string id = form["po_ID"];//外部应用ID

            string packageId = BoFactory.GetVersionTrackBo.GetPeId(id, "SMC_Package4Out");
            SMC_PackageExt pe = BoFactory.GetVersionTrackBo.GetPackageExt(packageId);

            SMC_Package4Out packageOut = BoFactory.GetVersionTrackBo.GetPackage4Out(id);
            packageOut.ClientType = form["cType"];
            packageOut.DisplayName = form["DisplayName"];
            packageOut.Description = form["Description"];
            pe.pe_LastVersion = pe.pe_Version;
            pe.pe_Version = form["Version"];
            packageOut.Version = form["Version"];

            //文件更新
            int i = Request.Files.Count;

            HttpPostedFileBase file = null;

            string filePath = "";
            string packageFileName = "";
            string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径
            string pDir = SAVEOUTPATH + @"/" + packageId;//相对路径 用安装包ID做文件夹名
            string saveDir = conSolePath + pDir;


            string svpath = "~/PackageExt/" + pe.pe_id + "/";
            string spath = Server.MapPath(svpath);
            string dvpath = "~/PackageExtHistory/" + pe.pe_id + "/";
            string dpath = Server.MapPath(dvpath);

            if (Directory.Exists(dpath) == false)
                Directory.CreateDirectory(dpath);

            SMC_PackageExtHistory his = null;
            bool is_pe_his_new = false;

            List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
            pars.Add(new KeyValuePair<string, object>("pe_id", pe.pe_id));
            pars.Add(new KeyValuePair<string, object>("pe_Version", pe.pe_LastVersion));

            his = Bo.BoFactory.GetSMC_PackageExtHistoryBO.Get(pars);
            if (his == null)
            {
                his = new SMC_PackageExtHistory();
                his.pe_Version = pe.pe_LastVersion;
                his.pe_id = pe.pe_id;
                is_pe_his_new = true;
            }
            his.pe_CreateTime = DateTime.Now;
            his.pe_Version = pe.pe_LastVersion;

            if (i > 0)
            {
                //有新安装包文件更新
                if (Request.Files[0].ContentLength > 0)
                {
                    //删除旧文件
                    //string oldDir = conSolePath + SAVEOUTPATH + packageOut.DownloadUri;
                    string oldDir = Server.MapPath(packageOut.DownloadUri.Replace(AppConfig.OutWebHost, ""));
                    if (System.IO.File.Exists(oldDir))
                    {
                        //System.IO.File.Delete(oldDir);
                    }

                    //创建新文件
                    file = Request.Files[0];
                    filePath = file.FileName;
                    packageFileName = "外部安装包文件-" + Path.GetFileName(filePath);
                    string savePath = System.IO.Path.Combine(saveDir, packageFileName);

                    //移动附件到历史文件夹                        
                    string[] sfiles = Directory.GetFiles(spath);
                    if (sfiles != null && sfiles.Length > 0)
                    {
                        foreach (string f in sfiles)
                        {
                            if (f.EndsWith(".apk") || f.EndsWith(".ipa"))
                            {
                                string filename = Path.GetFileName(f);
                                try
                                {
                                    System.IO.File.Move(spath + filename, dpath + filename);
                                }
                                catch
                                {
                                }
                                his.pe_PackageUrl = dvpath + filename;
                            }
                        }
                    }

                    if (!System.IO.Directory.Exists(saveDir))
                    {
                        System.IO.Directory.CreateDirectory(saveDir);
                    }
                    if (System.IO.File.Exists(savePath))
                    {
                        System.IO.File.Delete(savePath);
                    }
                    pe.pe_FileUrl = svpath + Path.GetFileName(file.FileName);
                    file.SaveAs(savePath);
                    string sOutWebHost = AppConfig.OutWebHost;
                    if (sOutWebHost.EndsWith("/"))
                        sOutWebHost = AppConfig.OutWebHost.TrimEnd("/".ToCharArray());
                    packageOut.DownloadUri = sOutWebHost + "/" + AppConfig.OutPackUploadFolder + "/" + packageId.ToString() + @"/" + packageFileName;
                    pe.pe_DownloadUri = packageOut.DownloadUri;
                }

                //有新的图标更新
                if (Request.Files[1].ContentLength > 0)
                {
                    //删除旧文件
                    string oldDir = "~/" + AppConfig.OutPackUploadFolder + "/" + packageId + "/AppIcon.png";
                    oldDir = Server.MapPath(oldDir);
                    if (System.IO.File.Exists(oldDir))
                    {
                        System.IO.File.Delete(oldDir);
                    }

                    //创建新文件
                    file = Request.Files[1];
                    packageFileName = "AppIcon.png";
                    string savePath = System.IO.Path.Combine(saveDir, packageFileName);

                    if (!System.IO.Directory.Exists(saveDir))
                    {
                        System.IO.Directory.CreateDirectory(saveDir);
                    }
                    if (System.IO.File.Exists(savePath))
                    {
                        System.IO.File.Delete(savePath);
                    }
                    file.SaveAs(savePath);
                    pe.pe_PictureUrl = "~/" + AppConfig.OutPackUploadFolder + "/" + packageId.ToString() + @"/" + packageFileName;
                }
            }

            if (is_pe_his_new)
                Bo.BoFactory.GetSMC_PackageExtHistoryBO.Insert(his);
            else
                Bo.BoFactory.GetSMC_PackageExtHistoryBO.Update(his);

            BoFactory.GetVersionTrackBo.UpdatePackage4Out(packageOut);
            pe.pe_UpdateTime = DateTime.Now;
            pe.pe_UpdateUid = CurrentUser.UserUId;
            pe.pe_IsTJ = form["pe_IsTJ"];
            pe.pe_IsBB = form["pe_IsBB"];
            pe.pe_ClientType = form["cType"];
            pe.pe_UnitCode = form["Unit"];
            pe.pe_UnitName = BoFactory.GetSMC_UnitBo.Get(pe.pe_UnitCode).Unit_Name;
            pe.pe_CategoryID = form["AppID"];
            pe.pe_Category = BoFactory.GetVersionTrackBo.GetApplicationCategory(form["AppID"]).DisplayName;
            BoFactory.GetVersionTrackBo.UpdatePackageExt(pe);

            return Json(data, "text/html");
        }

        public ActionResult EditApplicationPackage(string id)
        {
            id = Request.QueryString["id"];
            Package4AI package = new Package4AI();
            try
            {
                package = BoFactory.GetVersionTrackBo.GetPackage4AI(id);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                Response.Write(ex.Message);
                Response.End();
            }
            string pe_id = BoFactory.GetVersionTrackBo.GetPeId(id, "Package4AI");
            SMC_PackageExt pe = BoFactory.GetVersionTrackBo.GetPackageExt(pe_id);

            List<SelectListItem> IsRecom = new List<SelectListItem>();
            IsRecom.Add(new SelectListItem { Text = "推荐", Value = "1" });
            IsRecom.Add(new SelectListItem { Text = "不推荐", Value = "0" });
            if (pe.pe_IsTJ == "False")
            {
                IsRecom[1].Selected = true;
            }
            else
            {
                IsRecom[0].Selected = true;
            }
            List<SelectListItem> IsMust = new List<SelectListItem>();
            IsMust.Add(new SelectListItem { Text = "必备", Value = "1" });
            IsMust.Add(new SelectListItem { Text = "不必备", Value = "0" });
            if (pe.pe_IsBB == "False")
            {
                IsMust[1].Selected = true;
            }
            else
            {
                IsMust[0].Selected = true;
            }
            List<SelectListItem> unitList = new List<SelectListItem>();
            //获取当前用户的本单位
            //Dictionary<string, string> unitInfo = BoFactory.GetSMC_UserListBo.GetUnitByUL_UID(CurrentUser.UserUId);
            //if (unitInfo.Count > 0)
            //{
            //    string unitName = "本单位-" + unitInfo.ElementAt(0).Key;
            //    string unitId = unitInfo.ElementAt(0).Value;
            //    unitList.Add(new SelectListItem { Text = unitName, Value = unitId, Selected = true });
            //}
            //if (BoFactory.GetVersionTrackBo.IsSystemManager(CurrentUser.UserUId))
            //{
            //    //加入所有单位列表
            //    PageView view = new PageView();
            //    view.PageSize = 15;
            //    JsonFlexiGridData units = BoFactory.GetSMC_UnitBo.QueryUnitByUpperUnitCode(view, "");
            //    foreach (FlexiGridRow r in units.rows)
            //    {
            //        unitList.Add(new SelectListItem { Text = r.cell[1], Value = r.id });
            //    }
            //}

            //获取当前用户的本单位
            IList<SmartBox.Console.Common.Entities.SMC_Unit> js = GetUnitData();

            foreach (SmartBox.Console.Common.Entities.SMC_Unit r in js)
            {
                //unitList.Add(new SelectListItem { Text = "全局", Value = "" });
                SelectListItem itm = new SelectListItem { Text = r.Unit_Name, Value = r.Unit_ID };

                if (pe.pe_UnitCode == r.Unit_ID)
                {
                    itm.Selected = true;
                }
                unitList.Add(itm);
            }

            var ddlApplicationSelect = BoFactory.GetVersionTrackBo.QueryApplicationCategoryList();
            List<SelectListItem> appTypes = new List<SelectListItem>();
            foreach (var ad in ddlApplicationSelect)
            {
                appTypes.Add(new SelectListItem { Text = ad.DisplayName, Value = ad.ID.ToString(), Selected = (ad.DisplayName == pe.pe_Category) });
            }

            ViewData["AppID"] = appTypes;
            ViewData["pe_IsTJ"] = IsRecom;
            ViewData["pe_IsBB"] = IsMust;
            ViewData["Unit"] = unitList;

            ViewData["Application"] = BoFactory.GetVersionTrackBo.QueryApplicationList().ToDataTable();
            ViewData["ClientType"] = BoFactory.GetVersionTrackBo.QueryClientTypeList().ToDataTable();
            return View(package);
        }

        #region 上传包
        private static List<string> EXTENSION = new List<string> { ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".ico" };

        public ActionResult CreateApplicationPackage()
        {
            List<SelectListItem> unitList = new List<SelectListItem>();
            IList<SmartBox.Console.Common.Entities.SMC_Unit> js = GetUnitData();

            foreach (SmartBox.Console.Common.Entities.SMC_Unit r in js)
            {
                //unitList.Add(new SelectListItem { Text = "全局", Value = "" });
                unitList.Add(new SelectListItem { Text = r.Unit_Name, Value = r.Unit_ID });
            }
            //获取当前用户的本单位
            //Dictionary<string, string> unitInfo = BoFactory.GetSMC_UserListBo.GetUnitByUL_UID(CurrentUser.UserUId);
            //if (unitInfo.Count > 0)
            //{
            //    string unitName = "本单位-" + unitInfo.ElementAt(0).Key;
            //    string unitId = unitInfo.ElementAt(0).Value;
            //    unitList.Add(new SelectListItem { Text = unitName, Value = unitId, Selected = true });
            //}
            //if (BoFactory.GetVersionTrackBo.IsSystemManager(CurrentUser.UserUId))
            //{
            //    //加入所有单位列表
            //    PageView view = new PageView();
            //    view.PageSize = 15;
            //    JsonFlexiGridData units = BoFactory.GetSMC_UnitBo.QueryUnitByUpperUnitCode(view, "");
            //    if (units != null && units.total > 0)
            //    foreach (FlexiGridRow r in units.rows)
            //    {
            //        //unitList.Add(new SelectListItem { Text = "全局", Value = "" });
            //        unitList.Add(new SelectListItem { Text = r.cell[1], Value = r.id });
            //    }
            //}

            SelectHelper ddlApplicationSelect = new SelectHelper(BoFactory.GetVersionTrackBo.QueryApplicationCategoryList().ToDataTable());
            ViewData["AppID"] = ddlApplicationSelect.GetSelectList("DisplayName", "ID", false);


            SelectHelper dllAppSelect = new SelectHelper(BoFactory.GetVersionTrackBo.QueryApplicationList().ToDataTable(), "未指定", "");
            ViewData["AppCode_"] = dllAppSelect.GetSelectListItem("Name", "ID", true);
            SelectHelper chkClientTypeSelect = new SelectHelper(BoFactory.GetVersionTrackBo.QueryClientTypeList().ToDataTable());
            ViewData["ClientType"] = chkClientTypeSelect.GetSelectListItem("DisplayName", "ClientType", false);

            List<SelectListItem> IsRecom = new List<SelectListItem>();
            IsRecom.Add(new SelectListItem { Text = "推荐", Value = "1", Selected = true });
            IsRecom.Add(new SelectListItem { Text = "不推荐", Value = "0" });

            List<SelectListItem> IsMust = new List<SelectListItem>();
            IsMust.Add(new SelectListItem { Text = "必备", Value = "1", Selected = true });
            IsMust.Add(new SelectListItem { Text = "不必备", Value = "0" });
            ViewData["IsRecom"] = IsRecom;
            ViewData["IsMust"] = IsMust;
            ViewData["Unit"] = unitList;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ClearPrivilegeUser(string privilegecode)
        {
            Hashtable result = new Hashtable();
            result["r"] = true;
            result["d"] = "操作成功！";

            try
            {
                Bo.BoFactory.GetVersionTrackBo.ClearPrivilegeUser(privilegecode);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                result["r"] = false;
                result["d"] = "操作失败！";
            }


            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CreateApplicationPackage(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            Package4AI package = new Package4AI();

            Hashtable extentInfo = new Hashtable();
            try
            {
                package.CreateUid = CurrentUser.UserUId;
                package.UpdateUid = CurrentUser.UserUId;
                package.CreateTime = DateTime.Now;
                package.UpdateTime = DateTime.Now;
                package.DownloadUri = form["packageFileName"];
                package.Name = form["packageName"];
                package.DisplayName = form["packageDisplayName"];
                package.ClientType = form["packageClientType"];
                package.Type = form["packageType"];
                package.Version = form["packageVersion"];
                package.BuildVer = Convert.ToInt32(form["packageBuildVer"]);
                package.Description = form["packageDescription"];

                List<KeyValuePair<string, object>> _pars = new List<KeyValuePair<string, object>>();

                _pars.Add(new KeyValuePair<string, object>("Name", package.Name));
                _pars.Add(new KeyValuePair<string, object>("ClientType", package.ClientType));

                Package4AI pai = BoFactory.GetPackage4AIBO.Get(_pars);
                if (pai != null)
                {
                    throw new ArgumentException("不能发布重复的包");
                }

                SMC_PackageExt packageExt = new SMC_PackageExt();

                List<Hashtable> applications = new List<Hashtable>();
                extentInfo["Applications"] = applications;
                for (int appIndex = 0; appIndex < Convert.ToInt32(form["applicationCount"]); appIndex++)
                {
                    Hashtable application = new Hashtable();
                    applications.Add(application);
                    Hashtable AppIcons = new Hashtable();
                    application["ApplicationIcon"] = AppIcons;

                    App4AI app4AI = new App4AI();
                    if (!"".Equals(form["AppCode_" + appIndex.ToString()]))
                    {
                        app4AI.AppID = Convert.ToInt32(form["AppCode_" + appIndex.ToString()]);
                    }
                    app4AI.AppCode = form["AppName_" + appIndex.ToString()];
                    packageExt.pe_ApplicationCode += app4AI.AppCode + ",";
                    List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                    KeyValuePair<string, object> par = new KeyValuePair<string, object>("Name", app4AI.AppCode);
                    pars.Add(par);
                    Application app = BoFactory.GetApplicationBO.Get(pars);
                    string applicationName = "";
                    if (app != null)
                        applicationName = app.DisplayName;
                    packageExt.pe_ApplicationName += applicationName + ",";
                    app4AI.ClientType = form["AppCheckClentType_" + appIndex.ToString()];

                    application["ApplicationID"] = form["AppCode_" + appIndex.ToString()];
                    AppIcons["AppIconName"] = form["AppIco_" + appIndex.ToString()];
                    //AppIcons.Add(new Tuple<string, string, object>("ApplicationID", form["AppCode_" + appIndex.ToString()], form["AppIco_" + appIndex.ToString()]));

                    app4AI.IconUri = GetAndroidApplicationIcoUri(Path.Combine(TEMPPATH, package.DownloadUri), form["AppIco_" + appIndex.ToString()]);
                    AppIcons["AppIconUri"] = app4AI.IconUri;

                    app4AI.Seq = appIndex + 1;
                    app4AI.CreateTime = DateTime.Now;
                    app4AI.UpdateTime = DateTime.Now;
                    app4AI.CreateUid = CurrentUser.UserUId;
                    app4AI.UpdateUid = CurrentUser.UserUId;

                    List<Hashtable> activities = new List<Hashtable>();
                    application["activities"] = activities;
                    for (int activtyIndex = 0; activtyIndex < Convert.ToInt32(form["activityCount_" + appIndex.ToString()]); activtyIndex++)
                    {
                        Action4Android action4Android = new Action4Android();
                        action4Android.Seq = Convert.ToInt32(form["ActivitySeq_" + appIndex.ToString() + "_" + activtyIndex.ToString()]);
                        action4Android.DisplayName = form["ActivityDisplayName_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                        action4Android.IsLaunch = Convert.ToBoolean(form["ActivityLaunch_" + appIndex.ToString() + "_" + activtyIndex.ToString()]);
                        action4Android.IconUri = GetAndroidApplicationIcoUri(Path.Combine(TEMPPATH, package.DownloadUri), form["ActivityIco_" + appIndex.ToString() + "_" + activtyIndex.ToString()]);
                        action4Android.ShortName = form["ActivityShortName_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                        action4Android.Name = form["ActivityName_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                        action4Android.CreateTime = DateTime.Now;
                        action4Android.UpdateTime = DateTime.Now;
                        action4Android.CreateUid = CurrentUser.UserUId;
                        action4Android.UpdateUid = CurrentUser.UserUId;

                        Hashtable activity = new Hashtable();
                        activity["DisplayName"] = form["ActivityDisplayName_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                        activity["ActivitySeq"] = form["ActivitySeq_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                        activity["IsLaunch"] = action4Android.IsLaunch;
                        activity["IconUri"] = action4Android.IconUri;
                        activity["ShortName"] = action4Android.ShortName;
                        activity["Name"] = action4Android.Name;
                        activity["CreateTime"] = action4Android.CreateTime;
                        activity["UpdateTime"] = action4Android.UpdateTime;
                        activity["CreateUid"] = action4Android.CreateUid;
                        activity["UpdateUid"] = action4Android.UpdateUid;

                        activities.Add(activity);
                        app4AI.ActionList.Add(action4Android);
                    }
                    package.App4AIList.Add(app4AI);
                }

                string saveFileName = string.Format("{0}_v{1}_{2}{3}", package.Name, package.BuildVer, DateTime.Now.ToString("yyyyMMddHHmmss"), Path.GetExtension(package.DownloadUri));
                string tempFilePath = Path.Combine(TEMPPATH, package.DownloadUri);
                string saveFilePath = Path.Combine(SAVEPATH, saveFileName);

                if (package.Type.Equals("Main", StringComparison.CurrentCultureIgnoreCase))
                {
                    var mainConfig = AppConfig.PublishConfig.GetValue(package.ClientType);
                    if (package.ClientType.EndsWith("ios", StringComparison.CurrentCultureIgnoreCase))
                    {
                        mainConfig.Url = IOS_URL_PREFIX + mainConfig.Url;
                    }
                    package.DownloadUri = mainConfig.Url;
                    saveFilePath = mainConfig.Path;
                }
                else
                {
                    package.DownloadUri = Path.Combine(AppConfig.PackUrl, saveFileName);
                }

                GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("app_sj_need_auth");
                packageExt.pe_id = BoFactory.GetVersionTrackBo.GetMaxPackageExtId() + 1;
                packageExt.pe_ExtentInfo = Newtonsoft.Json.JsonConvert.SerializeObject(extentInfo);

                if (parm.ConfigValue == "0")
                {
                    //不需要审核
                    BoFactory.GetVersionTrackBo.InsertPackage4AI(package, tempFilePath, saveFilePath);
                    packageExt.pe_UsefulStstus = "1";//上架状态
                    packageExt.pe_UsefulTime = DateTime.Now;
                    packageExt.pe_UsefulOperatorUID = CurrentUser.UserUId;
                    packageExt.pe_UsefulOperatorName = CurrentUser.UnitName;
                    packageExt.pe_AuthStatus = 1;//审核通过
                    packageExt.pe_AuthTime = DateTime.Now;
                    packageExt.pe_AuthSubmitName = CurrentUser.FullName;
                    packageExt.pe_AuthSubmitUID = CurrentUser.UserUId;
                    packageExt.pe_AuthManUID = CurrentUser.UserUId;
                    packageExt.pe_AuthMan = CurrentUser.FullName;
                    packageExt.pe_SyncStatus = 0;
                    packageExt.pe_Direction = "发布";
                    packageExt.pe_AuthSubmitTime = DateTime.Now;
                }
                else
                {
                    string serializationPath = Server.MapPath("~/PackageSerialization/") + packageExt.pe_id + "/";
                    if (!Directory.Exists(serializationPath))
                        Directory.CreateDirectory(serializationPath);
                    using (Stream fStream = new FileStream(serializationPath + "package.xml", FileMode.Create, FileAccess.ReadWrite))
                    {
                        XmlSerializer xmlFormat = new XmlSerializer(typeof(Package4AI));
                        xmlFormat.Serialize(fStream, package);
                        fStream.Close();
                        fStream.Dispose();
                    }
                    XmlDocument xmlDoc = new XmlDocument();
                    string content = String.Format("<PackagePath><TempFilePath>{0}</TempFilePath><SaveFileName>{1}</SaveFileName></PackagePath>", tempFilePath, saveFilePath);
                    xmlDoc.LoadXml(content);
                    xmlDoc.Save(serializationPath + "path.xml");

                    //审核通过后再同步到Package4AI表
                    packageExt.pe_UsefulStstus = "0";//上架状态
                    packageExt.pe_Direction = "上架";
                    packageExt.pe_AuthStatus = 0;//待审核
                    packageExt.pe_AuthSubmitTime = DateTime.Now;
                    packageExt.pe_AuthSubmitName = CurrentUser.FullName;
                    packageExt.pe_AuthSubmitUID = CurrentUser.UserUId;
                    packageExt.pe_Direction = "发布";
                    packageExt.pe_UsefulTime = DateTime.Now;
                    packageExt.pe_UsefulOperatorUID = CurrentUser.UserUId;
                    packageExt.pe_UsefulOperatorName = CurrentUser.FullName;

                    System.IO.File.Copy(tempFilePath, saveFilePath, true);
                }


                //插入扩展表
                packageExt.pe_ClientType = package.ClientType;
                var s1 = form["IsRecom"];
                var s2 = form["IsMust"];
                packageExt.pe_IsTJ = form["IsRecom"];
                packageExt.pe_IsBB = form["IsMust"];
                FileInfo fi = new FileInfo(saveFilePath);
                packageExt.pe_Size = (int)fi.Length;
                packageExt.TableName = "Package4AI";
                packageExt.TableID = package.ID;
                packageExt.pe_UnitCode = form["Unit"];
                packageExt.pe_CategoryID = form["AppID"];
                packageExt.pe_Category = BoFactory.GetVersionTrackBo.GetApplicationCategory(form["AppID"]).DisplayName;
                packageExt.pe_UnitName = BoFactory.GetSMC_UnitBo.Get(packageExt.pe_UnitCode).Unit_Name;
                packageExt.pe_CreateUid = CurrentUser.UserUId;
                packageExt.pe_CreatedTime = DateTime.Now;
                packageExt.pe_UpdateUid = CurrentUser.UserUId;
                packageExt.pe_UpdateTime = DateTime.Now;
                packageExt.pe_Version = form["packageVersion"];
                packageExt.pe_BuildVer = form["packageBuildVer"];
                packageExt.pe_Name = form["packageDisplayName"];
                packageExt.pe_DisplayName = form["packageDisplayName"];
                packageExt.pe_Description = form["packageDescription"];
                packageExt.pe_Firmware = form["Firmware"];
                packageExt.pe_Type = package.Type;
                

                string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径
                string pDir = Path.Combine(SAVEOUTPATH, packageExt.pe_id.ToString());//相对路径 用安装包ID做文件夹名
                string saveDir = Path.Combine(conSolePath , pDir);

                if (!System.IO.Directory.Exists(saveDir))
                {
                    System.IO.Directory.CreateDirectory(saveDir);
                }
                //生成下载url,并生成二维码
                string url = package.DownloadUri;

                string pe_2dPictureUrl = Save2DPicture(packageExt.pe_id, url);

                //保存安装包
                string packageName = "Package4AI-" + Path.GetFileName(tempFilePath);
                string packageFilePath = System.IO.Path.Combine(saveDir, packageName);
                System.IO.File.Copy(saveFilePath, packageFilePath, true);

                packageExt.pe_FileUrl = "~/PackageExt/" + packageExt.pe_id + "/" + packageName;

                packageExt.pe_2dPictureUrl = pe_2dPictureUrl;// @"~/" + pDir + "/二维码图片.jpeg";
                //packageExt.pe_2dPictureUrl = packageExt.pe_2dPictureUrl.Replace(@"\\", "/");
                packageExt.pe_DownloadUri = url.Replace(@"\", "/");

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

                BoFactory.GetVersionTrackBo.InsertPackageExt(packageExt);

                if (parm.ConfigValue == "0")
                {
                    //不需要审核,直接同步到应用中心
                    //同步至外网数据
                    _SyncPackageExt(packageExt);
                }

                if (package.Type.Equals("Main", StringComparison.CurrentCultureIgnoreCase))
                {
                    try
                    {
                        //更新server缓存
                        SmartBox.Console.Service.ServiceReference1.ManagerServiceClient cli = new Service.ServiceReference1.ManagerServiceClient();
                        cli.ResetClientVer();
                    }
                    catch
                    {
                        data.Msg = "操作成功,更新SmartBox服务缓存失败。";
                    }
                }
                
                //app更新到外网
                BoFactory.GetCommonBO.CopyAppFilesToAppCenterServer(saveFilePath, packageExt.pe_id);

                #region pad布局检查
                if (package.ClientType.StartsWith("Pad", StringComparison.CurrentCultureIgnoreCase))
                {
                    PageView view = new PageView();
                    view.PageSize = 15;
                    view.PageIndex = 0;
                    int HomePlans = BoFactory.GetVersionTrackBo.QueryHomePlanList(view).rows.Count;
                    if (HomePlans < 1)
                    {
                        data.Msg += " \n没有页面布局,请在Home布局管理中增加.";
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }

            return Json(data, "text/html");
        }

        /// <summary>
        /// 获取AndroidApp图片Uri
        /// 将图片存储到数据库后返回图片Uri地址
        /// </summary>
        /// <param name="packagePath">安装包的物理地址</param>
        /// <param name="innerAppIcoUri">包内部AppIco名称</param>
        /// <returns></returns>
        private string GetAndroidApplicationIcoUri(string packagePath, string innerAppIco)
        {
            if (string.IsNullOrEmpty(packagePath))
            {
                throw new Exception("安装包路径不能为空");
            }
            if (string.IsNullOrEmpty(innerAppIco))
            {
                return string.Empty;
            }
            if (innerAppIco.StartsWith("Server://"))
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
                        //if (fileName.Equals(innerAppIco, StringComparison.CurrentCultureIgnoreCase) || ((!String.IsNullOrEmpty(innerAppIco)) && (innerAppIco.IndexOf("/" + fileName) != -1)))
                        if (fileName.Equals(innerAppIco, StringComparison.CurrentCultureIgnoreCase) || ((!String.IsNullOrEmpty(innerAppIco)) && (innerAppIco.EndsWith("/" + fileName))))
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

        public ActionResult CopyAppFilesToAppCenterServer()
        {
            Hashtable r = new Hashtable();
            r["r"] = true;
            r["d"] = "复制发布成功!";
            Bo.BoFactory.GetCommonBO.CopyAppFilesToAppCenterServer(string.Empty, 0);
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadPackage(FormCollection form, string filePath, bool useLocalPath)
        {
            JsonReturnMessages data = new JsonReturnMessages();
            data.IsSuccess = true;
            data.Msg = "操作成功";
            string tempPackagePath = "";
            string packageFileName = "";
            HttpPostedFileBase file = null;
            try
            {
                if (useLocalPath)
                {
                    packageFileName = Path.GetFileName(filePath);
                }
                else
                {
                    file = Request.Files[0];
                    packageFileName = Path.GetFileName(file.FileName);
                }
                tempPackagePath = System.IO.Path.Combine(TEMPPATH, packageFileName);

                bool isAndroidPackage = true;
                if (".apk".Equals(Path.GetExtension(packageFileName), StringComparison.CurrentCultureIgnoreCase))
                {
                    isAndroidPackage = true;
                }
                else if (".ipa".Equals(Path.GetExtension(packageFileName), StringComparison.CurrentCultureIgnoreCase))
                {
                    isAndroidPackage = false;
                }
                else
                {
                    throw new Exception("只能上传Android和IOS的安装包");
                }

                //保存文件
                string saveDir = Path.GetDirectoryName(tempPackagePath);
                if (!System.IO.Directory.Exists(saveDir))
                {
                    System.IO.Directory.CreateDirectory(saveDir);
                }
                if (System.IO.File.Exists(tempPackagePath))
                {
                    System.IO.File.Delete(tempPackagePath);
                }
                if (useLocalPath)
                {
                    System.IO.File.Copy(filePath, tempPackagePath);
                }
                else
                {
                    file.SaveAs(tempPackagePath);
                }

                //读取文件配置
                Package4AI package;

                if (isAndroidPackage)
                {
                    package = ReadAndroidPackage(tempPackagePath);
                }
                else
                {
                    package = ReadIOSPackage(tempPackagePath);
                }

                //TODO:增加安装包主程序判断,阻止同平台主程序多次上传
                if ("Main".Equals(package.Type, StringComparison.CurrentCultureIgnoreCase) && BoFactory.GetPackage4AIBO.HasMainPackage(package.ClientType))
                    throw new Exception("已存在主程序包,不能重复上传!");

                data.Data = package;

                List<SelectListItem> IsRecom = new List<SelectListItem>();
                IsRecom.Add(new SelectListItem { Text = "推荐", Value = "1", Selected = true });
                IsRecom.Add(new SelectListItem { Text = "不推荐", Value = "0" });

                List<SelectListItem> IsMust = new List<SelectListItem>();
                IsMust.Add(new SelectListItem { Text = "必备", Value = "1", Selected = true });
                IsMust.Add(new SelectListItem { Text = "不必备", Value = "0" });
                ViewData["IsRecom"] = IsRecom;
                ViewData["IsMust"] = IsMust;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }
            return Json(data, "text/html");
        }

        public ActionResult BatchCreateApplicationPackage()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult BatchCreateApplicationPackage(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "" };
            try
            {
                //保存ZIP文件
                var file = Request.Files[0];
                string zipName = Path.GetFileName(file.FileName);
                string tempPackagePath = System.IO.Path.Combine(TEMPPATH, zipName);
                if (!".zip".Equals(Path.GetExtension(zipName), StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new Exception("只能上传ZIP压缩包");
                }
                if (!System.IO.Directory.Exists(TEMPPATH))
                {
                    System.IO.Directory.CreateDirectory(TEMPPATH);
                }
                if (System.IO.File.Exists(tempPackagePath))
                {
                    System.IO.File.Delete(tempPackagePath);
                }
                file.SaveAs(tempPackagePath);

                List<string> filePathList = new List<string>();
                string saveDir = Path.Combine(Path.GetDirectoryName(tempPackagePath), zipName.Substring(0, zipName.Length - 4));
                if (!System.IO.Directory.Exists(saveDir))
                {
                    System.IO.Directory.CreateDirectory(saveDir);
                }
                using (ZipFile zip = new ZipFile(tempPackagePath))
                {
                    foreach (ZipEntry entry in zip)
                    {
                        if (entry.IsFile)
                        {
                            string filePath = Path.Combine(saveDir, Path.GetFileName(entry.Name));
                            filePathList.Add(filePath);
                            using (FileStream fs = System.IO.File.Create(filePath))
                            {
                                bool flag = true;
                                Stream source = zip.GetInputStream(entry.ZipFileIndex);
                                byte[] buffer = new byte[1024];
                                while (flag)
                                {
                                    int count = source.Read(buffer, 0, buffer.Length);
                                    if (count > 0)
                                    {
                                        fs.Write(buffer, 0, count);
                                    }
                                    else
                                    {
                                        fs.Flush();
                                        flag = false;
                                    }
                                }
                            }
                        }
                    }
                }
                IList<Application> applicationList = BoFactory.GetVersionTrackBo.QueryApplicationList();
                foreach (string packagePath in filePathList)
                {
                    try
                    {
                        bool isAndroidPackage = true;
                        if (".apk".Equals(Path.GetExtension(packagePath), StringComparison.CurrentCultureIgnoreCase))
                        {
                            isAndroidPackage = true;
                        }
                        else if (".ipa".Equals(Path.GetExtension(packagePath), StringComparison.CurrentCultureIgnoreCase))
                        {
                            isAndroidPackage = false;
                        }
                        else
                        {
                            throw new Exception("只能上传Android和IOS的安装包");
                        }

                        //读取文件配置
                        Package4AI package;
                        if (isAndroidPackage)
                        {
                            package = ReadAndroidPackage(packagePath);
                        }
                        else
                        {
                            package = ReadIOSPackage(packagePath);
                        }

                        package.CreateUid = CurrentUser.UserUId;
                        package.UpdateUid = CurrentUser.UserUId;
                        package.CreateTime = DateTime.Now;
                        package.UpdateTime = DateTime.Now;
                        if (string.IsNullOrEmpty(package.DisplayName))
                        {
                            package.DisplayName = package.Name;
                        }
                        package.App4AIList.ForEach(app4AI =>
                        {//设置Application属性
                            app4AI.Seq = 1;
                            app4AI.CreateTime = DateTime.Now;
                            app4AI.UpdateTime = DateTime.Now;
                            app4AI.CreateUid = CurrentUser.UserUId;
                            app4AI.UpdateUid = CurrentUser.UserUId;

                            if (isAndroidPackage)
                            {//是Andriod包则修改Application路径
                                app4AI.IconUri = GetAndroidApplicationIcoUri(packagePath, app4AI.IconUri);
                            }
                            string appCode = "";
                            if (!string.IsNullOrEmpty(app4AI.AppName))
                            {
                                appCode = app4AI.AppName;
                            }
                            else if (!string.IsNullOrEmpty(app4AI.AppCode))
                            {
                                appCode = app4AI.AppCode;
                            }
                            if (!string.IsNullOrEmpty(appCode))
                            {
                                List<Application> application = applicationList.Where(app => app.Name.Equals(appCode, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                if (application.Count > 0)
                                {
                                    app4AI.AppID = application[0].ID;
                                }
                            }
                            app4AI.ActionList.ForEach(action4Android =>
                            {//设置Action属性
                                action4Android.CreateTime = DateTime.Now;
                                action4Android.UpdateTime = DateTime.Now;
                                action4Android.CreateUid = CurrentUser.UserUId;
                                action4Android.UpdateUid = CurrentUser.UserUId;
                            });
                        });
                        //生成存储的文件名
                        string saveFileName = string.Format("{0}_v{1}_{2}{3}", package.Name, package.BuildVer, DateTime.Now.ToString("yyyyMMddHHmmss"), Path.GetExtension(package.DownloadUri));
                        //生成存储路径
                        string saveFilePath = Path.Combine(SAVEPATH, saveFileName);
                        if (package.ClientType.EndsWith("ios", StringComparison.CurrentCultureIgnoreCase))
                        {
                            package.DownloadUri = AppConfig.iOSDownloadUrl;
                        }
                        else
                        {
                            package.DownloadUri = Path.Combine(AppConfig.PackUrl, saveFileName);
                        }
                        BoFactory.GetVersionTrackBo.InsertPackage4AI(package, packagePath, saveFilePath);
                    }
                    catch (Exception ex)
                    {
                        data.Msg += string.Format("[{0}]{1}<br/>", Path.GetFileName(packagePath), ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.Msg = ex.Message;
                data.IsSuccess = false;
            }
            return Json(data, "text/html");
        }

        private Package4AI ReadIOSPackage(string tempPackagePath)
        {
            XmlDocument xml = new XmlDocument();
            XmlDocument plist = new XmlDocument();

            using (ZipFile zip = new ZipFile(tempPackagePath))
            {
                const string IPHONEPLIST = "Info.plist";
                const string IPHONEXML = "package.xml";
                ZipEntry zipXML = null;
                ZipEntry zipPlist = null;
                foreach (ZipEntry item in zip)
                {
                    string[] filename = item.Name.Split('/');
                    if (filename.Length == 3 && filename[2].Equals(IPHONEPLIST, StringComparison.CurrentCultureIgnoreCase))
                    {
                        zipPlist = item;
                    }
                    else if (IPHONEXML.Equals(filename[filename.Length - 1], StringComparison.CurrentCultureIgnoreCase))
                    {
                        zipXML = item;
                    }
                    if (zipXML != null && zipPlist != null)
                    {
                        break;
                    }
                }
                if (zipXML == null)
                {
                    throw new Exception("安装包中未找到package.xml文件!,请上传正确的安装包!");
                }
                if (zipPlist == null)
                {
                    throw new Exception("安装包中未找到Info.plist文件!,请上传正确的安装包!");
                }

                Stream xmlStream = zip.GetInputStream(zipXML);
                xml.LoadXml(ConvertPlist.ConvertPlist.plist2xml(xmlStream));

                Stream plistStream = zip.GetInputStream(zipPlist);
                var plistXml = ConvertPlist.ConvertPlist.plist2xml(plistStream);
                using (var reader = new StringReader(plistXml))
                {
                    XmlReaderSettings xs = new XmlReaderSettings();
                    xs.XmlResolver = null;
                    xs.ProhibitDtd = false;
                    plist.Load(XmlReader.Create(reader, xs));
                }
            }
            XmlNode xmlPackage = xml.GetElementsByTagName("package")[0];
            XmlNode buildVer = plist.SelectSingleNode("/plist/dict/key[.='CFBundleVersion']");
            XmlNode version = plist.SelectSingleNode("/plist/dict/key[.='CFBundleShortVersionString']");
            Package4AI package = new Package4AI();
            package.CreateUid = CurrentUser.UserUId;
            package.UpdateUid = CurrentUser.UserUId;
            package.CreateTime = DateTime.Now;
            package.UpdateTime = DateTime.Now;
            package.DownloadUri = Path.GetFileName(tempPackagePath);
            package.Description = xmlPackage.Attributes["description"] != null ? xmlPackage.Attributes["description"].Value : string.Empty;
            package.DisplayName = xmlPackage.Attributes["displayName"] != null ? xmlPackage.Attributes["displayName"].Value : string.Empty;
            if (xmlPackage.Attributes["name"] == null)
            {
                throw new Exception("未找到安装包客户端名称,请上传正确的安装包!");
            }
            package.Name = xmlPackage.Attributes["name"].Value;
            if (xmlPackage.Attributes["clientType"] == null)
            {
                throw new Exception("未找到安装包客户端类型,请上传正确的安装包!");
            }
            package.ClientType = xmlPackage.Attributes["clientType"].Value;
            if (xmlPackage.Attributes["type"] == null)
            {
                throw new Exception("未找到安装包类型,请上传正确的安装包!");
            }
            package.Type = xmlPackage.Attributes["type"].Value;
            package.Version = version != null ? version.NextSibling.InnerText : string.Empty;
            if (buildVer == null)
            {
                throw new Exception("未找到内部版本号,请上传正确的安装包!");
            }
            int _buildVer = Convert.ToInt32(float.Parse(buildVer.NextSibling.InnerText));
            package.BuildVer = _buildVer;

            foreach (XmlNode application in xml.GetElementsByTagName("application"))
            {
                App4AI app4AI = new App4AI();
                app4AI.AppCode = application.Attributes["name"] != null ? application.Attributes["name"].Value : string.Empty;
                app4AI.ClientType = application.Attributes["type"] != null ? application.Attributes["type"].Value : package.ClientType;
                app4AI.IconUri = application.Attributes["ico"] != null ? application.Attributes["ico"].Value : string.Empty;
                app4AI.AppName = application.Attributes["code"] != null ? application.Attributes["code"].Value : string.Empty;
                app4AI.Seq = 1;
                app4AI.CreateTime = DateTime.Now;
                app4AI.UpdateTime = DateTime.Now;
                app4AI.CreateUid = CurrentUser.UserUId;
                app4AI.UpdateUid = CurrentUser.UserUId;

                foreach (XmlNode action in application.SelectNodes("activity/action"))
                {
                    Action4Android action4Android = new Action4Android();
                    action4Android.Seq = action.Attributes["sort"] != null ? Convert.ToInt32(action.Attributes["sort"].Value) : 1;
                    action4Android.DisplayName = action.Attributes["label"] != null ? action.Attributes["label"].Value : string.Empty;
                    action4Android.IsLaunch = action.Attributes["isLaunch"] != null ? Convert.ToBoolean(action.Attributes["isLaunch"].Value) : false;
                    action4Android.IconUri = action.Attributes["ico"] != null ? action.Attributes["ico"].Value : string.Empty;
                    action4Android.ShortName = action.Attributes["shortName"] != null ? action.Attributes["shortName"].Value : string.Empty;
                    action4Android.Name = action.Attributes["name"] != null ? action.Attributes["name"].Value : string.Empty;

                    app4AI.ActionList.Add(action4Android);
                }
                package.App4AIList.Add(app4AI);
            }
            return package;
        }

        private Package4AI ReadAndroidPackage(string tempPackagePath)
        {
            XmlDocument xmlPlugin = new XmlDocument();

            const string ANDROID = "assets/plugin.xml";
            using (ZipFile zip = new ZipFile(tempPackagePath))
            {
                ZipEntry zipPlugin = zip.GetEntry(ANDROID);
                if (zipPlugin == null)
                {
                    throw new Exception("请检查上传的包是否是内部app!");
                }
                Stream stream = zip.GetInputStream(zipPlugin);
                xmlPlugin.Load(stream);
            }
            XmlNode xmlPackage = xmlPlugin.GetElementsByTagName("package")[0];
            Package4AI package = new Package4AI();
            package.CreateUid = CurrentUser.UserUId;
            package.UpdateUid = CurrentUser.UserUId;
            package.CreateTime = DateTime.Now;
            package.UpdateTime = DateTime.Now;
            package.DownloadUri = Path.GetFileName(tempPackagePath);
            package.Description = xmlPackage.Attributes["description"] != null ? xmlPackage.Attributes["description"].Value : string.Empty;
            package.DisplayName = xmlPackage.Attributes["displayName"] != null ? xmlPackage.Attributes["displayName"].Value : string.Empty;
            if (xmlPackage.Attributes["name"] == null)
            {
                throw new Exception("未找到安装包客户端名称,请上传正确的安装包!");
            }
            package.Name = xmlPackage.Attributes["name"].Value;
            if (xmlPackage.Attributes["clientType"] == null)
            {
                throw new Exception("未找到安装包客户端类型,请上传正确的安装包!");
            }
            package.ClientType = xmlPackage.Attributes["clientType"].Value;
            if (xmlPackage.Attributes["type"] == null)
            {
                throw new Exception("未找到安装包类型,请上传正确的安装包!");
            }
            package.Type = xmlPackage.Attributes["type"].Value;
            package.Version = xmlPackage.Attributes["ver"] != null ? xmlPackage.Attributes["ver"].Value : string.Empty;
            if (xmlPackage.Attributes["buildver"] == null)
            {
                throw new Exception("未找到内部版本号,请上传正确的安装包!");
            }
            package.BuildVer = Convert.ToInt32(xmlPackage.Attributes["buildver"].Value);

            foreach (XmlNode application in xmlPlugin.GetElementsByTagName("application"))
            {
                App4AI app4AI = new App4AI();
                app4AI.AppName = application.Attributes["code"] != null ? application.Attributes["code"].Value : string.Empty;
                app4AI.AppCode = application.Attributes["name"] != null ? application.Attributes["name"].Value : string.Empty;
                app4AI.ClientType = package.ClientType;
                app4AI.IconUri = application.Attributes["ico"] != null ? BoFactory.GetCommonBO.GetAndroidApplicationIcoUri(tempPackagePath, application.Attributes["ico"].Value) : string.Empty;
                app4AI.Seq = 1;
                app4AI.CreateTime = DateTime.Now;
                app4AI.UpdateTime = DateTime.Now;
                app4AI.CreateUid = CurrentUser.UserUId;
                app4AI.UpdateUid = CurrentUser.UserUId;

                foreach (XmlNode action in application.SelectNodes("activity/action"))
                {
                    Action4Android action4Android = new Action4Android();
                    action4Android.Seq = action.Attributes["sort"] != null ? Convert.ToInt32(action.Attributes["sort"].Value) : 1;
                    action4Android.DisplayName = action.Attributes["label"] != null ? action.Attributes["label"].Value : string.Empty;
                    action4Android.IsLaunch = action.Attributes["isLaunch"] != null ? Convert.ToBoolean(action.Attributes["isLaunch"].Value) : false;
                    action4Android.IconUri = action.Attributes["ico"] != null ? BoFactory.GetCommonBO.GetAndroidApplicationIcoUri(tempPackagePath, action.Attributes["ico"].Value) : string.Empty;
                    action4Android.ShortName = action.Attributes["shortName"] != null ? action.Attributes["shortName"].Value : string.Empty;
                    action4Android.Name = action.Attributes["name"] != null ? action.Attributes["name"].Value : string.Empty;

                    app4AI.ActionList.Add(action4Android);
                }
                package.App4AIList.Add(app4AI);
            }
            return package;
        }
        #endregion

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditApplicationPackage(FormCollection form)
        {
            Package4AI package = new Package4AI();
            package.UpdateUid = CurrentUser.UserUId;
            package.UpdateTime = DateTime.Now;
            package.CreateUid = form["packageCreateUser"];
            package.CreateTime = Convert.ToDateTime(form["packageCreateDateTime"]);
            package.ID = Convert.ToInt32(form["packageID"]);
            package.DownloadUri = form["packageDownloadUrl"];
            package.Name = form["packageName"];
            package.DisplayName = form["packageDisplayName"];
            package.ClientType = form["packageClientType"];
            package.Type = form["packageType"];
            package.Version = form["packageVersion"];
            package.BuildVer = Convert.ToInt32(form["packageBuildVer"]);
            package.Description = form["packageDescription"];

            for (int appIndex = 0; appIndex < Convert.ToInt32(form["applicationCount"]); appIndex++)
            {
                string appIndexStr = appIndex.ToString();
                App4AI app4AI = new App4AI();
                app4AI.ID = Convert.ToInt32(form["AppID_" + appIndexStr]);
                if (!"".Equals(form["AppCode_" + appIndexStr]))
                {
                    app4AI.AppID = Convert.ToInt32(form["AppCode_" + appIndexStr]);
                }
                app4AI.AppCode = form["AppName_" + appIndexStr];
                app4AI.ClientType = form["AppCheckClentType_" + appIndexStr];
                app4AI.IconUri = form["AppIco_" + appIndexStr];
                app4AI.Seq = Convert.ToInt32(form["AppSeq_" + appIndexStr]);
                app4AI.Package4AIID = Convert.ToInt32(form["AppPackageID_" + appIndexStr]);
                app4AI.PackageName = form["AppPackageName_" + appIndexStr];
                app4AI.CreateTime = Convert.ToDateTime(form["AppCreateDateTime_" + appIndexStr]);
                app4AI.CreateUid = form["AppCreateUser_" + appIndexStr];
                app4AI.UpdateTime = DateTime.Now;
                app4AI.UpdateUid = CurrentUser.UserUId;

                for (int activtyIndex = 0; activtyIndex < Convert.ToInt32(form["activityCount_" + appIndex.ToString()]); activtyIndex++)
                {
                    string activtyIndexStr = activtyIndex.ToString();
                    Action4Android action4Android = new Action4Android();
                    action4Android.App4AIID = Convert.ToInt32(form["ActivityAppID_" + appIndexStr + "_" + activtyIndexStr]);
                    action4Android.Seq = Convert.ToInt32(form["ActivitySeq_" + appIndexStr + "_" + activtyIndexStr]);
                    action4Android.DisplayName = form["ActivityDisplayName_" + appIndexStr + "_" + activtyIndexStr];
                    action4Android.IsLaunch = Convert.ToBoolean(form["ActivityLaunch_" + appIndexStr + "_" + activtyIndexStr]);
                    action4Android.IconUri = form["ActivityIco_" + appIndexStr + "_" + activtyIndexStr];
                    action4Android.ShortName = form["ActivityShortName_" + appIndexStr + "_" + activtyIndexStr];
                    action4Android.Name = form["ActivityName_" + appIndexStr + "_" + activtyIndexStr];
                    action4Android.CreateTime = Convert.ToDateTime(form["ActivityCreateDateTime_" + appIndexStr + "_" + activtyIndexStr]);
                    action4Android.UpdateTime = DateTime.Now;
                    action4Android.CreateUid = form["ActivityCreateUser_" + appIndexStr + "_" + activtyIndexStr];
                    action4Android.UpdateUid = CurrentUser.UserUId;
                    app4AI.ActionList.Add(action4Android);
                }
                package.App4AIList.Add(app4AI);
            }
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };

            //package数据
            string pe_id = BoFactory.GetVersionTrackBo.GetPeId(package.ID.ToString(), "Package4AI");
            SMC_PackageExt pe = BoFactory.GetVersionTrackBo.GetPackageExt(pe_id);
            pe.pe_UpdateUid = CurrentUser.UserUId;
            pe.pe_UpdateTime = DateTime.Now;
            pe.pe_IsTJ = form["pe_IsTJ"];
            pe.pe_IsBB = form["pe_IsBB"];
            pe.pe_CategoryID = form["AppID"];
            pe.pe_Category = BoFactory.GetVersionTrackBo.GetApplicationCategory(form["AppID"]).DisplayName;
            pe.pe_UnitCode = form["Unit"];
            pe.pe_Name = package.Name;
            pe.pe_DisplayName = package.DisplayName;
            SMC_Unit unit = BoFactory.GetSMC_UnitBo.Get(pe.pe_UnitCode);

            package.DownloadUri = pe.pe_DownloadUri;
            if (unit != null && !string.IsNullOrEmpty(unit.Unit_Name))
            {
                pe.pe_UnitName = unit.Unit_Name;
            }

            try
            {
                BoFactory.GetVersionTrackBo.UpdatePackage4AI(package);
                BoFactory.GetVersionTrackBo.UpdatePackageExt(pe);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }

            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteApplicationPackage(string id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            //BoFactory.GetVersionTrackBo.DeletePackage4AI(id);
            PageView view = new PageView();
            view.PageIndex = 0;
            view.PageSize = 15;

            //删除内容 outPackage packageExt 收藏 截图 手册  反馈
            string pe_id = BoFactory.GetVersionTrackBo.GetPeId(id, "Package4AI");//packageExt
            try
            {
                //删除收藏
                BoFactory.GetVersionTrackBo.DeletePackageCollectByPEID(pe_id);

                //删除反馈
                BoFactory.GetVersionTrackBo.DeletePackageFAQByPEID(pe_id);

                //删除截图文件和截图
                var picList = BoFactory.GetVersionTrackBo.QueryPackageGifList(view, pe_id);
                if (picList.total > 0)
                {
                    foreach (FlexiGridRow r in picList.rows)
                    {
                        string pid = r.cell[0];
                        string pUrl = r.cell[3];

                        if (System.IO.File.Exists(pUrl))
                        {
                            System.IO.File.Delete(pUrl);
                        }
                        BoFactory.GetVersionTrackBo.DeletePackagePicture(pid);
                    }
                }

                //删除手册文件和手册
                var manualList = BoFactory.GetVersionTrackBo.QueryPackageManualList(view, pe_id);
                if (manualList.total > 0)
                {
                    foreach (FlexiGridRow r in manualList.rows)
                    {
                        string mid = r.cell[0];
                        string mUrl = r.cell[2];

                        if (System.IO.File.Exists(mUrl))
                        {
                            System.IO.File.Delete(mUrl);
                        }
                        BoFactory.GetVersionTrackBo.DeletePackageManual(mid);
                    }
                }

                //删除二维码图片
                SMC_PackageExt packageExt = BoFactory.GetVersionTrackBo.GetPackageExt(pe_id);
                string url2Pic = "Url";
                if (packageExt != null)
                {
                    url2Pic = packageExt.pe_2dPictureUrl;

                    if (System.IO.File.Exists(url2Pic))
                    {
                        System.IO.File.Delete(url2Pic);
                    }
                }


                //删除WebApplication和SMC_PackageExt
                BoFactory.GetVersionTrackBo.DeletePackage4AI(id);
                BoFactory.GetVersionTrackBo.DeletePackageExt(pe_id);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }
            return Json(data);
        }

        #region 更新包
        public ViewResult UpdatePackage(string id)
        {
            //var pe = BoFactory.GetVersionTrackBo.GetPackageExt(id);
            //if (pe.TableName != "Package4AI")
            //{
            //    return RedirectToAction("ModifyPackageExt", new {id=id });               
            //}

            ViewData["packageID"] = id;
            SelectHelper dllAppSelect = new SelectHelper(BoFactory.GetVersionTrackBo.QueryApplicationList().ToDataTable(), "未指定", "");
            ViewData["AppCode_"] = dllAppSelect.GetSelectListItem("Name", "ID", true);
            SelectHelper chkClientTypeSelect = new SelectHelper(BoFactory.GetVersionTrackBo.QueryClientTypeList().ToDataTable());
            ViewData["ClientType"] = chkClientTypeSelect.GetSelectListItem("DisplayName", "ClientType", false);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadUpdatePackage(FormCollection form, string packageID, string filePath)
        {
            JsonReturnMessages data = new JsonReturnMessages();
            data.IsSuccess = true;
            data.Msg = "操作成功";
            string packageFileName = "";
            string tempPackagePath = "";
            HttpPostedFileBase file = null;

            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    file = Request.Files[0];
                    packageFileName = Path.GetFileName(file.FileName);
                }
                else
                {
                    packageFileName = Path.GetFileName(filePath);
                }
                tempPackagePath = System.IO.Path.Combine(TEMPPATH, packageFileName);

                //取出老的安装包信息
                string packageid = packageID;//form["packageID"];
                SMC_PackageExt ext = BoFactory.GetAppCenterBO.GetPackageExt(Convert.ToInt32(packageid));
                string tableName = ext.TableName.ToLower();
                if (tableName != "webapplication" && tableName != "smc_package4out")
                {
                    packageid = ext.TableID.ToString();
                }
                if (string.IsNullOrEmpty(packageid))
                {
                    throw new Exception("未找到参数packageID");
                }
                Package4AI oldPackage = BoFactory.GetVersionTrackBo.GetPackage4AI(packageid);
                if (oldPackage == null)
                {
                    //throw new Exception("未找到要更新的安装包!请稍候再试");
                }

                //上传新安装包
                bool isAndroidPackage = true;
                if (".apk".Equals(Path.GetExtension(packageFileName), StringComparison.CurrentCultureIgnoreCase))
                {
                    isAndroidPackage = true;
                }
                else if (".ipa".Equals(Path.GetExtension(packageFileName), StringComparison.CurrentCultureIgnoreCase))
                {
                    isAndroidPackage = false;
                }
                else
                {
                    throw new Exception("只能上传Android和IOS的安装包");
                }

                SMC_PackageExtHistory history = null;
                List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                pars.Add(new KeyValuePair<string, object>("pe_id", ext.pe_id));
                pars.Add(new KeyValuePair<string, object>("pe_version", ext.pe_Version));

                history = Bo.BoFactory.GetSMC_PackageExtHistoryBO.Get(pars);
                bool newhistory = false;
                if (history == null)
                {
                    history = new SMC_PackageExtHistory();
                    newhistory = true;
                }
                history.pe_Version = ext.pe_Version;
                string vhistorypath = "~/PackageExtHistory/" + ext.pe_id + "/" + ext.pe_Version + "/";
                string vsourcepath = "~/PackageExt/" + ext.pe_id + "/";
                string historypath = Server.MapPath(vhistorypath);
                if (!Directory.Exists(historypath))
                {
                    Directory.CreateDirectory(historypath);
                }

                string sourcepath = Server.MapPath(vsourcepath);
                if (!Directory.Exists(sourcepath))
                {
                    Directory.CreateDirectory(sourcepath);
                }

                string[] files = Directory.GetFiles(sourcepath);
                string fPath = "";
                if (files != null && files.Length > 0)
                {
                    foreach (string f in files)
                    {
                        if (f.EndsWith(".ipa") || f.EndsWith(".apk"))
                        {
                            fPath = f;
                            break;
                        }
                    }
                }

                string filename = Path.GetFileName(fPath);
                historypath = historypath + filename;
                vhistorypath = vhistorypath + filename;
                try
                {
                    if (!String.IsNullOrEmpty(fPath) && System.IO.File.Exists(fPath))
                    {
                        //将原来的文件复制到历史目录里
                        System.IO.File.Move(fPath, historypath);
                    }
                }
                catch (Exception ex)
                {
                }

                if (vhistorypath.EndsWith(".apk") || vhistorypath.EndsWith(".ipa"))
                    history.pe_PackageUrl = vhistorypath;
                history.pe_Version = ext.pe_Version;
                history.pe_CreateTime = DateTime.Now;
                history.pe_id = ext.pe_id;
                if (newhistory)
                    Bo.BoFactory.GetSMC_PackageExtHistoryBO.Insert(history);
                else
                    Bo.BoFactory.GetSMC_PackageExtHistoryBO.Update(history);

                //if (string.IsNullOrEmpty(filePath))
                //{
                //保存文件
                string saveDir = Path.GetDirectoryName(tempPackagePath);
                if (!System.IO.Directory.Exists(saveDir))
                {
                    System.IO.Directory.CreateDirectory(saveDir);
                }
                if (System.IO.File.Exists(tempPackagePath))
                {
                    System.IO.File.Delete(tempPackagePath);
                }
                if (!string.IsNullOrEmpty(filePath))
                {
                    System.IO.File.Copy(filePath, tempPackagePath);
                    System.IO.File.Copy(tempPackagePath, sourcepath + Path.GetFileName(tempPackagePath));//复制到PackageExt目录
                }
                else
                {
                    file.SaveAs(tempPackagePath);
                    System.IO.File.Copy(tempPackagePath, sourcepath + Path.GetFileName(tempPackagePath), true);//复制到PackageExt目录
                }
                //}

                //读取文件配置
                Package4AI newpackage;
                if (isAndroidPackage)
                {
                    newpackage = ReadAndroidPackage(tempPackagePath);
                }
                else
                {
                    newpackage = ReadIOSPackage(tempPackagePath);
                }

                string debug_mode = ConfigurationManager.AppSettings["debug_mode"];
                if (!String.IsNullOrEmpty(debug_mode) && debug_mode.ToLower() == "true")
                {
                }
                else
                {
                    if (!newpackage.Name.Equals(oldPackage.Name))
                    {
                        throw new Exception("安装包名称不匹配,不能更新!");
                    }
                    if (newpackage.BuildVer <= oldPackage.BuildVer)
                    {
                        throw new Exception("当前版本号是" + oldPackage.BuildVer + " ! 上传包版本号小于当前版本号,不能更新!");
                    }
                }

                //将旧安装包信息覆盖到新安装包中
                if (oldPackage != null)
                {
                    newpackage.ID = oldPackage.ID;
                    newpackage.DisplayName = oldPackage.DisplayName;
                    newpackage.Description = oldPackage.Description;
                    newpackage.CreateTime = oldPackage.CreateTime;
                    newpackage.CreateUid = oldPackage.CreateUid;

                    foreach (App4AI app4ai in newpackage.App4AIList)
                    {
                        List<App4AI> oldApp4AIList = oldPackage.App4AIList.Where(x => x.AppCode.Equals(app4ai.AppCode, StringComparison.CurrentCultureIgnoreCase)).ToList();
                        if (oldApp4AIList.Count <= 0)
                        {
                            app4ai.AppID = -1;
                            continue;
                        }
                        App4AI oldApp4AI = oldApp4AIList[0];
                        app4ai.AppID = oldApp4AI.AppID;
                        app4ai.CreateUid = oldApp4AI.CreateUid;
                        app4ai.CreateTime = oldApp4AI.CreateTime;
                        app4ai.IconUri = oldApp4AI.IconUri;
                        app4ai.Seq = oldApp4AI.Seq;

                        foreach (Action4Android action in app4ai.ActionList)
                        {
                            List<Action4Android> oldActionList = oldApp4AI.ActionList.Where(x => x.Name.Equals(action.Name)).ToList();
                            if (oldActionList.Count <= 0)
                            {
                                continue;
                            }
                            Action4Android oldAction = oldActionList[0];
                            action.DisplayName = oldAction.DisplayName;
                            action.IconUri = oldAction.IconUri;
                            action.Seq = oldAction.Seq;
                            action.CreateTime = oldAction.CreateTime;
                            action.CreateUid = oldAction.CreateUid;
                        }
                    }
                }
                else
                {
                    newpackage.CreateTime = DateTime.Now;
                }

                data.Data = newpackage;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }
            return Json(data, "text/html");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdatePackage(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            Package4AI package = new Package4AI();
            SMC_PackageExtHistory history = new SMC_PackageExtHistory();

            package.CreateUid = form["packageCreateUser"];
            package.UpdateUid = CurrentUser.UserUId;
            package.CreateTime = Convert.ToDateTime(form["packageCreateDateTime"]);
            package.UpdateTime = DateTime.Now;
            package.DownloadUri = form["packageFileName"];
            package.Name = form["packageName"];
            package.DisplayName = form["packageDisplayName"];
            package.ClientType = form["packageClientType"];
            package.Type = form["packageType"];
            package.Version = form["packageVersion"];
            package.BuildVer = Convert.ToInt32(form["packageBuildVer"]);
            package.Description = form["packageDescription"];
            package.ID = Convert.ToInt32(form["packageID"]);

            SMC_PackageExt ext = BoFactory.GetAppCenterBO.GetPackageExt(package.ID);
            string tableName = ext.TableName.ToLower();
            if (tableName != "webapplication" && tableName != "smc_package4out")
            {
                package.ID = ext.TableID;
            }

            for (int appIndex = 0; appIndex < Convert.ToInt32(form["applicationCount"]); appIndex++)
            {
                App4AI app4AI = new App4AI();
                if (!"".Equals(form["AppCode_" + appIndex.ToString()]))
                {
                    app4AI.AppID = Convert.ToInt32(form["AppCode_" + appIndex.ToString()]);
                }
                app4AI.AppCode = form["AppName_" + appIndex.ToString()];
                app4AI.ClientType = form["AppCheckClentType_" + appIndex.ToString()];
                app4AI.IconUri = form["AppIco_" + appIndex.ToString()];
                app4AI.Seq = 1;
                app4AI.CreateTime = package.CreateTime;
                app4AI.UpdateTime = DateTime.Now;
                app4AI.CreateUid = package.CreateUid;
                app4AI.UpdateUid = CurrentUser.UserUId;

                for (int activtyIndex = 0; activtyIndex < Convert.ToInt32(form["activityCount_" + appIndex.ToString()]); activtyIndex++)
                {
                    Action4Android action4Android = new Action4Android();
                    action4Android.Seq = Convert.ToInt32(form["ActivitySeq_" + appIndex.ToString() + "_" + activtyIndex.ToString()]);
                    action4Android.DisplayName = form["ActivityDisplayName_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                    action4Android.IsLaunch = Convert.ToBoolean(form["ActivityLaunch_" + appIndex.ToString() + "_" + activtyIndex.ToString()]);
                    action4Android.IconUri = form["ActivityIco_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                    action4Android.ShortName = form["ActivityShortName_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                    action4Android.Name = form["ActivityName_" + appIndex.ToString() + "_" + activtyIndex.ToString()];
                    action4Android.CreateTime = package.CreateTime;
                    action4Android.UpdateTime = DateTime.Now;
                    action4Android.CreateUid = package.CreateUid;
                    action4Android.UpdateUid = CurrentUser.UserUId;
                    app4AI.Package4AIID = package.ID;
                    action4Android.App4AIID = package.ID;
                    Action4Android a4a = BoFactory.GetAction4AndroidBO.Get(action4Android.Name);
                    if (a4a == null)
                        app4AI.ActionList.Add(action4Android);
                }

                IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                pars.Add(new KeyValuePair<string, object>("Package4AIID", app4AI.Package4AIID));
                pars.Add(new KeyValuePair<string, object>("AppCode", app4AI.AppCode));
                pars.Add(new KeyValuePair<string, object>("ClientType", app4AI.ClientType));
                App4AI _app4ai = BoFactory.GetApp4AIBO.Get(pars);
                if (_app4ai == null)
                {
                    package.App4AIList.Add(app4AI);
                }
                else
                {
                    _app4ai.AppID = app4AI.AppID;
                    BoFactory.GetApp4AIBO.Update(_app4ai);
                }
            }

            try
            {
                string saveFileName = string.Format("{0}_v{1}_{2}{3}", package.Name, package.BuildVer, DateTime.Now.ToString("yyyyMMddHHmmss"), Path.GetExtension(package.DownloadUri));
                string tempFilePath = Path.Combine(TEMPPATH, package.DownloadUri);
                string saveFilePath = Path.Combine(SAVEPATH, saveFileName);

                if (package.ClientType.EndsWith("ios", StringComparison.CurrentCultureIgnoreCase))
                {
                    string padUrl = string.Empty;
                    string phoneUrl = string.Empty;
                    Common.Configuration.IOSConfiguration iosConfig = (Common.Configuration.IOSConfiguration)System.Configuration.ConfigurationManager.GetSection("PublishConfig");
                    if (iosConfig != null && iosConfig.IOSPublishs != null)
                    {
                        foreach (Common.Configuration.IOSPublishElement item in iosConfig.IOSPublishs)
                        {
                            if (item.ClientType.StartsWith("Pad/iOS", StringComparison.CurrentCultureIgnoreCase))
                                padUrl = item.Url;
                            if (item.ClientType.StartsWith("Phone/iOS", StringComparison.CurrentCultureIgnoreCase))
                                phoneUrl = item.Url;
                        }
                    }
                    if (package.Type.Equals("Main", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (package.ClientType.StartsWith("Pad", StringComparison.CurrentCultureIgnoreCase))
                        {
                            package.DownloadUri = @"itms-services://?action=download-manifest&url=" + padUrl;
                        }
                        else if (package.ClientType.StartsWith("Phone", StringComparison.CurrentCultureIgnoreCase))
                        {
                            package.DownloadUri = @"itms-services://?action=download-manifest&url=" + phoneUrl;
                        }
                    }
                }
                else
                {
                    string padUrl = string.Empty;
                    string phoneUrl = string.Empty;
                    Common.Configuration.IOSConfiguration iosConfig = (Common.Configuration.IOSConfiguration)System.Configuration.ConfigurationManager.GetSection("PublishConfig");
                    if (iosConfig != null && iosConfig.IOSPublishs != null)
                    {
                        foreach (Common.Configuration.IOSPublishElement item in iosConfig.IOSPublishs)
                        {
                            if (item.ClientType.StartsWith("Pad/Android", StringComparison.CurrentCultureIgnoreCase))
                                padUrl = item.Url;
                            if (item.ClientType.StartsWith("Phone/Android", StringComparison.CurrentCultureIgnoreCase))
                                phoneUrl = item.Url;
                        }
                    }
                    if (package.Type.Equals("Main", StringComparison.CurrentCultureIgnoreCase))
                    {
                        package.DownloadUri = Path.Combine(AppConfig.PackUrl, saveFileName);
                    }
                }

                SMC_PackageExt _ext = BoFactory.GetAppCenterBO.GetPackage("Package4AI", package.ID.ToString());
                _ext.pe_LastVersion = _ext.pe_Version;
                _ext.pe_Version = package.Version;
                _ext.pe_FileUrl = "~/PackageExt/" + _ext.pe_id + "/" + Path.GetFileName(tempFilePath);
                GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("app_sj_need_auth");


                if (parm.ConfigValue != "0")
                {
                    string serializationPath = Server.MapPath("~/PackageSerialization/") + _ext.pe_id + "/";
                    if (!Directory.Exists(serializationPath))
                        Directory.CreateDirectory(serializationPath);
                    using (Stream fStream = new FileStream(serializationPath + "package.xml", FileMode.Create, FileAccess.ReadWrite))
                    {
                        XmlSerializer xmlFormat = new XmlSerializer(typeof(Package4AI));
                        xmlFormat.Serialize(fStream, package);
                        fStream.Close();
                        fStream.Dispose();
                    }
                    XmlDocument xmlDoc = new XmlDocument();
                    string content = String.Format("<PackagePath><TempFilePath>{0}</TempFilePath><SaveFileName>{1}</SaveFileName></PackagePath>", tempFilePath, saveFilePath);
                    xmlDoc.LoadXml(content);
                    xmlDoc.Save(serializationPath + "path.xml");

                    //审核通过后再同步到Package4AI表
                    _ext.pe_UsefulStstus = "0";//上架状态
                    _ext.pe_AuthStatus = 0;
                    _ext.pe_AuthSubmitName = CurrentUser.FullName;
                    _ext.pe_AuthSubmitTime = DateTime.Now;
                    _ext.pe_AuthSubmitUID = CurrentUser.UserUId;
                    _ext.pe_UsefulTime = DateTime.Now;
                    _ext.pe_UsefulOperatorUID = CurrentUser.UserUId;
                    _ext.pe_UsefulOperatorName = CurrentUser.FullName;
                    if (package.ClientType.ToLower().IndexOf("ios") != -1)
                        _ext.pe_DownloadUri = package.DownloadUri;
                }

                _ext.pe_Version = form["packageVersion"];
                _ext.pe_BuildVer = form["packageBuildVer"];
                Bo.BoFactory.GetSMC_PackageExtBO.Save(_ext);

                if (parm.ConfigValue == "0")
                {
                    //不需要审核,直接同步到应用中心
                    //同步至外网数据

                    BoFactory.GetVersionTrackBo.UpdatePackage(package, tempFilePath, saveFilePath);
                    _ext.pe_UsefulStstus = "1";//上架状态
                    _ext.pe_UsefulTime = DateTime.Now;
                    _ext.pe_UsefulOperatorUID = CurrentUser.UserUId;
                    _ext.pe_UsefulOperatorName = CurrentUser.FullName;
                    _ext.pe_AuthStatus = 1;//审核通过
                    _ext.pe_AuthTime = DateTime.Now;
                    _ext.pe_AuthManUID = CurrentUser.UserUId;
                    _ext.pe_AuthMan = CurrentUser.FullName;
                    _ext.pe_DownloadUri = ConfigurationManager.AppSettings["packUrl"] + saveFileName;
                    if (package.ClientType.ToLower().IndexOf("ios") != -1)
                        _ext.pe_DownloadUri = package.DownloadUri;



                    //内部发布到Package4AI
                    BoFactory.GetCommonBO.SMC_PackageExtInternalRelease(_ext);

                    //同步到应用中心
                    _SyncPackageExt(_ext);
                }



                #region 上传文件自动发布
                if (package.Type.Equals("Main", StringComparison.CurrentCultureIgnoreCase))
                {
                    //更新server缓存
                    try
                    {
                        SmartBox.Console.Service.ServiceReference1.ManagerServiceClient cli = new Service.ServiceReference1.ManagerServiceClient();
                        cli.ResetClientVer();
                    }
                    catch
                    {
                        data.Msg = "操作成功，更新Server缓存失败。如遇到未提示版本更新问题请联系管理员重启服务。";
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }

            return Json(data);
        }
        #endregion


        #region Web应用管理
        public ActionResult WebApplicationManage()
        {
            return View();
        }

        public JsonResult QueryWebApplicationList(FormCollection form)
        {
            //JsonFlexiGridData data = new JsonFlexiGridData();
            //SearchWebApplication view = new SearchWebApplication(form);
            //data = BoFactory.GetVersionTrackBo.QueryWebApplicationList(view);

            //return Json(data);
            JsonFlexiGridData data = new JsonFlexiGridData();
            JsonFlexiGridData ndata = new JsonFlexiGridData();
            SearchWebApplication view = new SearchWebApplication(form);
            string uid = CurrentUser.UserUId;

            try
            {
                data = BoFactory.GetVersionTrackBo.QueryWebApplicationList(view);
            }
            catch (Exception ex) { Log4NetHelper.Error(ex); }

            if (!BoFactory.GetVersionTrackBo.IsSystemManager(uid))
            {
                //非超级管理员,实行过滤
                foreach (FlexiGridRow r in data.rows)
                {
                    //加入用户创建的app
                    var createId = r.cell[6];//创建者uid
                    if (createId == uid)
                    {
                        ndata.rows.Add(r);
                        continue;
                    }

                    //加入用户所属单位的app
                    Dictionary<string, string> unitInfo = BoFactory.GetSMC_UserListBo.GetUnitByUL_UID(CurrentUser.UserUId);
                    string unit = null;
                    if (unitInfo.Count > 0)
                    {
                        unit = unitInfo.ElementAt(0).Value.ToString();
                    }

                    if (unit == r.cell[11])
                    {
                        ndata.rows.Add(r);
                        continue;
                    }

                }
                return Json(ndata);
            }
            else
            {
                return Json(data);
            }
        }

        public ActionResult EditWebApplication(string id)
        {
            id = Request.QueryString["id"];
            List<SelectListItem> unitList = new List<SelectListItem>();
            //获取当前用户的本单位
            IList<SmartBox.Console.Common.Entities.SMC_Unit> js = GetUnitData();

            foreach (SmartBox.Console.Common.Entities.SMC_Unit r in js)
            {
                //unitList.Add(new SelectListItem { Text = "全局", Value = "" });
                unitList.Add(new SelectListItem { Text = r.Unit_Name, Value = r.Unit_ID });
            }
            ViewData["Application"] = BoFactory.GetVersionTrackBo.QueryApplicationList().ToDataTable();
            //Dictionary<string, string> unitInfo = BoFactory.GetSMC_UserListBo.GetUnitByUL_UID(CurrentUser.UserUId);
            //if (unitInfo.Count > 0)
            //{
            //    string unitName = "本单位-" + unitInfo.ElementAt(0).Key;
            //    string unitId = unitInfo.ElementAt(0).Value;
            //    unitList.Add(new SelectListItem { Text = unitName, Value = unitId, Selected = true });
            //}
            //if (BoFactory.GetVersionTrackBo.IsSystemManager(CurrentUser.UserUId))
            //{
            //    //加入所有单位列表
            //    PageView view = new PageView();
            //    view.PageSize = 15;
            //    JsonFlexiGridData units = BoFactory.GetSMC_UnitBo.QueryUnitByUpperUnitCode(view, "");
            //    foreach (FlexiGridRow r in units.rows)
            //    {
            //        //unitList.Add(new SelectListItem { Text = "全局", Value = "" });
            //        unitList.Add(new SelectListItem { Text = r.cell[1], Value = r.id });
            //    }
            //}
            List<SelectListItem> IsRecom = new List<SelectListItem>();
            IsRecom.Add(new SelectListItem { Text = "推荐", Value = "1", Selected = true });
            IsRecom.Add(new SelectListItem { Text = "不推荐", Value = "0" });

            List<SelectListItem> IsMust = new List<SelectListItem>();
            IsMust.Add(new SelectListItem { Text = "必备", Value = "1", Selected = true });
            IsMust.Add(new SelectListItem { Text = "不必备", Value = "0" });


            WebApplication webApplication;
            SMC_PackageExt entity = null;
            if (string.IsNullOrEmpty(id))
            {
                webApplication = new WebApplication();
                webApplication.Seq = 1;
            }
            else
            {
                webApplication = BoFactory.GetVersionTrackBo.GetWebApplication(id);
                entity = BoFactory.GetAppCenterBO.GetPackage("webapplication", id);
                string unit = "";
                if (webApplication != null)
                {
                    unit = webApplication.Unit;
                    if (entity.pe_IsTJ == "False")
                    {
                        IsRecom[1].Selected = true;
                    }
                    else
                    {
                        IsRecom[0].Selected = true;
                    }
                    if (entity.pe_IsBB == "False")
                    {
                        IsMust[1].Selected = true;
                    }
                    else
                    {
                        IsMust[0].Selected = true;
                    }

                }
                else
                {
                    if (entity != null && !String.IsNullOrEmpty(entity.pe_UnitCode))
                        unit = entity.pe_UnitCode;
                }

                foreach (SelectListItem item in unitList)
                {
                    if (item.Value == unit)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }

            ViewData["pe_IsTJ"] = IsRecom;
            ViewData["pe_IsBB"] = IsMust;

            if (entity != null && !String.IsNullOrEmpty(entity.pe_PictureUrl))
            {
                ViewData["IconUrl"] = entity.pe_PictureUrl;
            }
            else
            {
                ViewData["IconUrl"] = "";
            }
            ViewData["entity"] = entity;
            if (webApplication != null && string.IsNullOrEmpty(webApplication.ClientType))
            {
                webApplication.ClientType = string.Empty;
            }
            SelectHelper ddlApplicationSelect = new SelectHelper(BoFactory.GetVersionTrackBo.QueryApplicationCategoryList().ToDataTable());
            SelectHelper chkClientTypeSelect = new SelectHelper(BoFactory.GetVersionTrackBo.QueryClientTypeList().ToDataTable());
            int appId = 0;
            if (webApplication != null)
                appId = webApplication.AppID;
            else
            {
                if (entity != null)
                    appId = Convert.ToInt32(entity.pe_CategoryID);
            }
            ViewData["AppID"] = ddlApplicationSelect.GetSelectList("DisplayName", "ID", appId, false);
            if (webApplication == null)
            {
                List<string> vals = new List<string>();

                if (entity != null && !String.IsNullOrEmpty(entity.pe_ClientType))
                    vals.Add(entity.pe_ClientType);

                ViewData["ClientType"] = chkClientTypeSelect.GetSelectListItem("DisplayName", "ClientType", vals, false);
            }
            else
            {
                ViewData["ClientType"] = chkClientTypeSelect.GetSelectListItem("DisplayName", "ClientType", webApplication.ClientType.Split('|').ToList(), false);
            }
            ViewData["Unit"] = unitList;
            return View(webApplication);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditWebApplication(WebApplication webApplication, string pe_IsTJ, string pe_IsBB, string id)
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
                    BoFactory.GetVersionTrackBo.InsertWebApplication(webApplication);


                    //插入packageExt
                    SMC_PackageExt pe = new SMC_PackageExt();
                    pe.pe_id = BoFactory.GetVersionTrackBo.GetMaxPackageExtId() + 1;
                    pe.pe_IsTJ = pe_IsTJ;
                    pe.pe_IsBB = pe_IsBB;
                    pe.pe_PictureUrl = webApplication.IconUri;
                    pe.pe_ClientType = webApplication.ClientType;
                    pe.pe_DownloadUri = webApplication.Uri;
                    pe.TableName = "WebApplication";
                    pe.TableID = webApplication.ID;
                    pe.pe_UnitCode = webApplication.Unit;
                    pe.pe_ApplicationCode = Request.Form["ApplicationID"];

                    if (!String.IsNullOrEmpty(pe.pe_ApplicationCode))
                    {
                        string[] appIds = pe.pe_ApplicationCode.Split(",".ToCharArray());
                        string appNames = "";
                        foreach (string appid in appIds)
                        {
                            Application application = Bo.BoFactory.GetVersionTrackBo.GetApplication(appid);
                            if (application != null)
                            {
                                appNames += application.DisplayName + ",";
                            }
                        }
                        pe.pe_ApplicationName = appNames;
                    }

                    pe.pe_UnitName = BoFactory.GetSMC_UnitBo.Get(pe.pe_UnitCode).Unit_Name;
                    pe.pe_CreatedTime = webApplication.CreateTime;
                    pe.pe_CreateUid = webApplication.CreateUid;
                    pe.pe_UpdateTime = webApplication.UpdateTime;
                    pe.pe_LastVersion = "";
                    pe.pe_Version = "";
                    pe.pe_BuildVer = "";
                    pe.pe_Type = "web";
                    pe.pe_CategoryID = webApplication.AppID.ToString();
                    pe.pe_ClientType = webApplication.ClientType;
                    pe.pe_Category = BoFactory.GetVersionTrackBo.GetApplicationCategory(webApplication.AppID.ToString()).DisplayName;
                    pe.pe_DisplayName = webApplication.ShortName;
                    pe.pe_Description = webApplication.ShortName;
                    pe.pe_Name = webApplication.ShortName;
                    pe.pe_Firmware = "";
                    GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("app_sj_need_auth");
                    if (parm.ConfigValue == "1")
                    {
                        //需要审核
                        pe.pe_AuthStatus = 0;//待审核
                        pe.pe_AuthSubmitTime = DateTime.Now;
                        pe.pe_AuthSubmitName = CurrentUser.FullName;
                        pe.pe_AuthSubmitUID = CurrentUser.UserUId;
                        pe.pe_Direction = "发布";
                        pe.pe_UsefulStstus = "0";
                        pe.pe_UsefulTime = DateTime.Now;
                        pe.pe_UsefulOperatorUID = CurrentUser.UserUId;
                        pe.pe_UsefulOperatorName = CurrentUser.FullName;
                    }
                    else
                    {
                        pe.pe_AuthStatus = 1;//审核通过
                        pe.pe_AuthSubmitTime = DateTime.Now;
                        pe.pe_AuthTime = DateTime.Now;
                        pe.pe_AuthSubmitName = CurrentUser.FullName;
                        pe.pe_AuthSubmitUID = CurrentUser.UserUId;
                        pe.pe_AuthManUID = CurrentUser.UserUId;
                        pe.pe_AuthMan = CurrentUser.FullName;
                        pe.pe_SyncStatus = 0;
                        pe.pe_Direction = "发布";
                        pe.pe_UsefulStstus = "1";
                        pe.pe_UsefulTime = DateTime.Now;
                        pe.pe_UsefulOperatorUID = CurrentUser.UserUId;
                        pe.pe_UsefulOperatorName = CurrentUser.FullName;
                    }

                    string pe_2dPictureUrl = Save2DPicture(pe.pe_id, pe.pe_DownloadUri);
                    pe.pe_2dPictureUrl = pe_2dPictureUrl;

                    int i = Request.Files.Count;
                    if (i > 0)
                    {
                        HttpPostedFileBase icon = null;
                        int packageId = pe.pe_id;
                        string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径
                        string pDir = SAVEOUTPATH + @"\" + packageId.ToString();//相对路径 用安装包ID做文件夹名
                        string saveDir = conSolePath + pDir;
                        //图标文件
                        icon = Request.Files[0];
                        string iconFileName = "AppIcon.png";
                        string savePath = System.IO.Path.Combine(saveDir, iconFileName);
                        if (!System.IO.Directory.Exists(saveDir))
                        {
                            System.IO.Directory.CreateDirectory(saveDir);
                        }
                        if (System.IO.File.Exists(savePath))
                        {
                            System.IO.File.Delete(savePath);
                        }
                        icon.SaveAs(savePath);

                        pe.pe_PictureUrl = @"~/" + pDir + "/AppIcon.png";
                        pe.pe_PictureUrl = pe.pe_PictureUrl.Replace(@"\\", "/");
                    }
                    BoFactory.GetVersionTrackBo.InsertPackageExt(pe);

                    if (parm.ConfigValue == "0")
                    {
                        //不需要审核,直接同步到应用中心
                        //同步至外网数据
                        _SyncPackageExt(pe);
                    }

                }
                else
                {  //修改
                    BoFactory.GetVersionTrackBo.UpdateWebApplication(webApplication);
                    string pe_id = BoFactory.GetVersionTrackBo.GetPeId(webApplication.ID.ToString(), "WebApplication");
                    SMC_PackageExt pe = BoFactory.GetVersionTrackBo.GetPackageExt(pe_id);
                    pe.pe_IsBB = pe_IsBB;
                    pe.pe_IsTJ = pe_IsTJ;
                    pe.pe_UpdateTime = DateTime.Now;
                    pe.pe_UpdateUid = CurrentUser.UserUId;
                    pe.pe_CategoryID = webApplication.AppID.ToString();
                    pe.pe_ClientType = webApplication.ClientType;
                    pe.pe_Category = BoFactory.GetVersionTrackBo.GetApplicationCategory(webApplication.AppID.ToString()).DisplayName;
                    pe.pe_DisplayName = webApplication.ShortName;
                    pe.pe_Description = webApplication.ShortName;
                    pe.pe_Name = webApplication.ShortName;
                    pe.pe_UnitCode = webApplication.Unit;
                    pe.pe_UnitName = BoFactory.GetSMC_UnitBo.Get(pe.pe_UnitCode).Unit_Name;

                    int i = Request.Files.Count;
                    pe.pe_ApplicationCode = Request.Form["ApplicationID"];
                    if (!String.IsNullOrEmpty(pe.pe_ApplicationCode))
                    {
                        string[] appIds = pe.pe_ApplicationCode.Split(",".ToCharArray());
                        string appNames = "";
                        foreach (string appid in appIds)
                        {
                            Application application = Bo.BoFactory.GetVersionTrackBo.GetApplication(appid);
                            if (application != null)
                            {
                                appNames += application.DisplayName + ",";
                            }
                        }
                        pe.pe_ApplicationName = appNames;
                    }

                    GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("app_sj_need_auth");
                    if (parm.ConfigValue == "1")
                    {
                        //需要审核
                        pe.pe_AuthStatus = 0;//待审核
                        pe.pe_AuthSubmitTime = DateTime.Now;
                        pe.pe_AuthSubmitName = CurrentUser.FullName;
                        pe.pe_AuthSubmitUID = CurrentUser.UserUId;
                        pe.pe_Direction = "发布";
                        pe.pe_Type = "web";
                        pe.pe_UsefulStstus = "0";
                        pe.pe_UsefulTime = DateTime.Now;
                        pe.pe_UsefulOperatorUID = CurrentUser.UserUId;
                        pe.pe_UsefulOperatorName = CurrentUser.FullName;
                    }
                    else
                    {
                        pe.pe_AuthStatus = 1;//审核通过
                        pe.pe_AuthSubmitTime = DateTime.Now;
                        pe.pe_AuthTime = DateTime.Now;
                        pe.pe_AuthSubmitName = CurrentUser.FullName;
                        pe.pe_AuthSubmitUID = CurrentUser.UserUId;
                        pe.pe_AuthManUID = CurrentUser.UserUId;
                        pe.pe_AuthMan = CurrentUser.FullName;
                        pe.pe_SyncStatus = 0;
                        pe.pe_Direction = "发布";
                        pe.pe_Type = "web";
                        pe.pe_UsefulStstus = "1";
                        pe.pe_UsefulTime = DateTime.Now;
                        pe.pe_UsefulOperatorUID = CurrentUser.UserUId;
                        pe.pe_UsefulOperatorName = CurrentUser.FullName;
                    }

                    string pe_2dPictureUrl = Save2DPicture(pe.pe_id, pe.pe_DownloadUri);
                    pe.pe_2dPictureUrl = pe_2dPictureUrl;

                    if (i > 0)
                    {
                        HttpPostedFileBase icon = null;
                        int packageId = pe.pe_id;
                        string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径
                        string pDir = SAVEOUTPATH + @"\" + packageId.ToString();//相对路径 用安装包ID做文件夹名
                        string saveDir = conSolePath + pDir;
                        //图标文件
                        icon = Request.Files[0];
                        string iconFileName = "AppIcon.png";
                        string savePath = System.IO.Path.Combine(saveDir, iconFileName);
                        if (!System.IO.Directory.Exists(saveDir))
                        {
                            System.IO.Directory.CreateDirectory(saveDir);
                        }
                        if (System.IO.File.Exists(savePath))
                        {
                            System.IO.File.Delete(savePath);
                        }
                        icon.SaveAs(savePath);

                        pe.pe_PictureUrl = @"~/" + pDir + "/AppIcon.png";
                        pe.pe_PictureUrl = pe.pe_PictureUrl.Replace(@"\\", "/");
                    }
                    pe.pe_ClientType = webApplication.ClientType;
                    BoFactory.GetVersionTrackBo.UpdatePackageExt(pe);


                    if (parm.ConfigValue == "0")
                    {
                        //不需要审核,直接同步到应用中心
                        //同步至外网数据
                        _SyncPackageExt(pe);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }
            return Json(data, "text/html");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteWebApplication(string id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            //try
            //{
            //    BoFactory.GetVersionTrackBo.DeleteWebApplication(id);
            PageView view = new PageView();
            view.PageIndex = 0;
            view.PageSize = 15;

            //删除内容 outPackage packageExt 收藏 截图 手册  反馈
            string pe_id = BoFactory.GetVersionTrackBo.GetPeId(id, "webapplication");//packageExt
            try
            {
                //删除收藏
                BoFactory.GetVersionTrackBo.DeletePackageCollectByPEID(pe_id);

                //删除反馈
                BoFactory.GetVersionTrackBo.DeletePackageFAQByPEID(pe_id);

                //删除截图文件和截图
                var picList = BoFactory.GetVersionTrackBo.QueryPackageGifList(view, pe_id);
                if (picList.total > 0)
                {
                    foreach (FlexiGridRow r in picList.rows)
                    {
                        string pid = r.cell[0];
                        string pUrl = r.cell[3];

                        if (System.IO.File.Exists(pUrl))
                        {
                            System.IO.File.Delete(pUrl);
                        }
                        BoFactory.GetVersionTrackBo.DeletePackagePicture(pid);
                    }
                }

                //删除手册文件和手册
                var manualList = BoFactory.GetVersionTrackBo.QueryPackageManualList(view, pe_id);
                if (manualList.total > 0)
                {
                    foreach (FlexiGridRow r in manualList.rows)
                    {
                        string mid = r.cell[0];
                        string mUrl = r.cell[2];

                        if (System.IO.File.Exists(mUrl))
                        {
                            System.IO.File.Delete(mUrl);
                        }
                        BoFactory.GetVersionTrackBo.DeletePackageManual(mid);
                    }
                }

                //删除二维码图片
                SMC_PackageExt packageExt = BoFactory.GetVersionTrackBo.GetPackageExt(pe_id);
                string url2Pic = "Url";
                if (packageExt != null)
                {
                    url2Pic = packageExt.pe_2dPictureUrl;

                    if (System.IO.File.Exists(url2Pic))
                    {
                        System.IO.File.Delete(url2Pic);
                    }
                }


                //删除WebApplication和SMC_PackageExt
                BoFactory.GetVersionTrackBo.DeleteWebApplication(id);
                BoFactory.GetVersionTrackBo.DeletePackageExt(pe_id);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }
            return Json(data);
        }
        #endregion

        public ActionResult OutApplication()
        {
            return View();
        }

        public ActionResult AsyncManage()
        {
            return View();
        }

        public ActionResult AsyncResultList()
        {
            return View();
        }

        public ActionResult AsyncResultListBUAUserToInside()
        {
            return View();
        }

        public ActionResult AsyncResultListBUAUserToOutside()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ImportOldPackage4AI(string ids)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "" };

            try
            {
                string[] idarray = ids.Split(",".ToCharArray());
                VersionTrackBo verBO = BoFactory.GetVersionTrackBo;

                List<SMC_PackageExt> exts = new List<SMC_PackageExt>();

                GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("app_sj_need_auth");
                foreach (string id in idarray)
                {
                    if (String.IsNullOrEmpty(id))
                        continue;
                    Package4AI pack = verBO.GetPackage4AI(id);
                    App4AI app4ai = verBO.GetApp4AI(pack.ID.ToString());

                    Application app = null;
                    if (app4ai.AppID != null)
                        app = verBO.GetApplication(app4ai.AppID.ToString());
                    else
                    {
                        List<KeyValuePair<string, object>> _pars = new List<KeyValuePair<string, object>>();
                        _pars.Add(new KeyValuePair<string, object>("[name]", app4ai.AppCode));
                        app = Bo.BoFactory.GetApplicationBO.Get(_pars);
                    }
                    bool is_pe_insert = true;

                    SMC_PackageExt ext = new SMC_PackageExt();
                    List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                    pars.Add(new KeyValuePair<string, object>("tablename", "Package4AI"));
                    pars.Add(new KeyValuePair<string, object>("tableid", id));
                    SMC_PackageExt _ext = Bo.BoFactory.GetSMC_PackageExtBO.Get(pars);
                    if (_ext != null)
                    {
                        ext = _ext;
                        is_pe_insert = false;
                    }
                    ext.TableName = "Package4AI";
                    ext.TableID = pack.ID;
                    ext.pe_Version = pack.Version;
                    ext.pe_UnitName = "";
                    ext.pe_Name = pack.Name;
                    ext.pe_DisplayName = pack.DisplayName;
                    ext.pe_Description = pack.Description;
                    ext.pe_BuildVer = pack.BuildVer.ToString();
                    ext.pe_ClientType = pack.ClientType;
                    if (app != null)
                    {
                        ext.pe_UnitCode = app.Unit;
                    }
                    else
                        ext.pe_UnitCode = "";
                    string cateid = "";
                    if (app != null && !String.IsNullOrEmpty(app.CategoryIDs))
                    {
                        string[] cateids = app.CategoryIDs.Split(",".ToCharArray());
                        if (cateids != null && cateids.Length > 0)
                        {
                            foreach (string cat in cateids)
                            {
                                if (!String.IsNullOrEmpty(cat))
                                {
                                    cateid = cat;
                                    break;
                                }
                            }
                        }
                    }
                    ext.pe_CategoryID = cateid;
                    ext.pe_CreatedTime = DateTime.Now;
                    ext.pe_CreateUid = "";
                    ext.pe_UpdateTime = DateTime.Now;
                    ext.pe_UpdateUid = "";
                    ext.pe_Firmware = "";
                    ext.pe_IsBB = "1";
                    ext.pe_IsTJ = "1";

                    if (parm.ConfigValue == "1")
                    {
                        //需要审核
                        ext.pe_AuthStatus = 0;//待审核
                        ext.pe_AuthSubmitTime = DateTime.Now;
                        ext.pe_AuthSubmitName = CurrentUser.FullName;
                        ext.pe_AuthSubmitUID = CurrentUser.UserUId;
                        ext.pe_Direction = "发布";
                        ext.pe_Type = pack.Type;
                        ext.pe_UsefulStstus = "0";
                        ext.pe_UsefulTime = DateTime.Now;
                        ext.pe_UsefulOperatorUID = CurrentUser.UserUId;
                        ext.pe_UsefulOperatorName = CurrentUser.FullName;
                    }
                    else
                    {
                        ext.pe_AuthStatus = 1;//审核通过
                        ext.pe_AuthSubmitTime = DateTime.Now;
                        ext.pe_AuthTime = DateTime.Now;
                        ext.pe_AuthSubmitName = CurrentUser.FullName;
                        ext.pe_AuthSubmitUID = CurrentUser.UserUId;
                        ext.pe_AuthManUID = CurrentUser.UserUId;
                        ext.pe_AuthMan = CurrentUser.FullName;
                        ext.pe_SyncStatus = 0;
                        ext.pe_Direction = "发布";
                        ext.pe_Type = pack.Type;
                        ext.pe_UsefulStstus = "1";
                        ext.pe_UsefulTime = DateTime.Now;
                        ext.pe_UsefulOperatorUID = CurrentUser.UserUId;
                        ext.pe_UsefulOperatorName = CurrentUser.FullName;
                    }
                    //exts.Add(ext);

                    if (is_pe_insert)
                        BoFactory.GetSMC_PackageExtBO.Insert(ext);
                    else
                        BoFactory.GetSMC_PackageExtBO.Update(ext);

                    //复制安装包
                    string pubFolder = System.Configuration.ConfigurationManager.AppSettings["pubFolder"];
                    string pubUrl = System.Configuration.ConfigurationManager.AppSettings["pubUrl"];
                    string OutWebHost = System.Configuration.ConfigurationManager.AppSettings["OutWebHost"];
                    string packUrl = System.Configuration.ConfigurationManager.AppSettings["packUrl"];
                    string outpackUploadFolder = System.Configuration.ConfigurationManager.AppSettings["outpackUploadFolder"];
                    string outPackUrl = Path.Combine(OutWebHost, outpackUploadFolder);
                    outPackUrl = Path.Combine(outPackUrl, ext.pe_id.ToString());
                    outPackUrl = outPackUrl.Replace("\\", "/") + "/";
                    ext.pe_DownloadUri = pack.DownloadUri.Replace(packUrl, outPackUrl);

                    string fileFolder = Path.Combine("~/", outpackUploadFolder);
                    if (!Directory.Exists(Server.MapPath(fileFolder)))
                        Directory.CreateDirectory(Server.MapPath(fileFolder));

                    fileFolder = Path.Combine(fileFolder, ext.pe_id.ToString());
                    fileFolder = Server.MapPath(fileFolder);

                    string fileName = Path.GetFileName(ext.pe_DownloadUri);
                    if (!Directory.Exists(fileFolder))
                        Directory.CreateDirectory(fileFolder);
                    string destFile = Path.Combine(fileFolder, fileName);
                    string sourceFile = Path.Combine(pubFolder, Path.GetFileName(ext.pe_DownloadUri));
                    if (System.IO.File.Exists(sourceFile))
                        System.IO.File.Copy(sourceFile, destFile, true);

                    //图标
                    string destIconFile = Path.Combine(fileFolder, "No.png");
                    string sourceIconFile = Server.MapPath("~/Images/no.png");
                    if (System.IO.File.Exists(sourceIconFile))
                        System.IO.File.Copy(sourceIconFile, destIconFile, true);
                    ext.pe_PictureUrl = "~/" + ext.pe_id + @"/" + "No.png";

                    System.Drawing.Bitmap bmp = GetDimensionalCode(ext.pe_DownloadUri);

                    //保存文件
                    string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径


                    string savePath = System.IO.Path.Combine(fileFolder, "二维码图片.jpeg");

                    if (System.IO.File.Exists(savePath))
                    {
                        System.IO.File.Delete(savePath);
                    }
                    bmp.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    ext.pe_2dPictureUrl = "~/" + ext.pe_id + @"/" + "二维码图片.jpeg";

                    BoFactory.GetSMC_PackageExtBO.Update(ext);

                    if (parm.ConfigValue == "0")
                    {
                        //不需要审核,直接同步到应用中心
                        //同步至外网数据
                        _SyncPackageExt(ext);
                    }
                }

                //BoFactory.GetAppCenterBO.AddSMC_PackageExt(exts);
                data.IsSuccess = true;
                data.Msg = "导入数据成功！";
            }
            catch (Exception e)
            {
                data.IsSuccess = false;
                data.Msg = e.Message;
                Log4NetHelper.Error(e);
            }
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AsyncManage(string pe_ids)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "" };
            string[] ids = pe_ids.Split(',');
            List<int> ayncpe_ids = new List<int>();
            int id = -1;
            foreach (string s in ids)
            {
                if (int.TryParse(s, out id) == true)
                {
                    ayncpe_ids.Add(id);
                }
            }

            try
            {
                IProxy proxy = ProxyFactory.CreateProxy();
                SmartBox.Console.Bo.AppCenter.AppCenterBO bo = proxy.CreateObject<SmartBox.Console.Bo.AppCenter.AppCenterBO>();

                Service.ApplicationCenterWS.WebService ws = new Service.ApplicationCenterWS.WebService();

                SyncPackages(bo, ws, ayncpe_ids);
                SyncUnits(bo, ws);
                SyncUsers(bo, ws);
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                data.IsSuccess = false;
                data.Msg = e.Message;
            }
            //获取同步成功的ext列表
            string succMsg = "";
            int batch_no = BoFactory.GetSMC_AutoTableIDBo.GetMaxId("SMC_PackageExtSyncToOutside", "sync_bat_no");
            var syncPs = BoFactory.GetSMC_PackageExtSyncToOutsideBO.GetByBatNo(batch_no);
            foreach (var psync in syncPs)
            {
                if (psync == null || psync.sync_status == false)
                {
                    data.Msg += psync.pe_name + "同步失败，原因是" + psync.description + "\n";
                }
                else if (psync.sync_status == true)
                {
                    succMsg += psync.pe_name + "同步成功\n";
                }
            }
            data.Msg = succMsg + data.Msg;
            return Json(data);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteAsyncResult(string ids)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "" };
            string[] _ids = ids.Split(',');
            List<int> needDeletedids = new List<int>();
            int id = -1;
            foreach (string s in _ids)
            {
                if (int.TryParse(s, out id) == true)
                {
                    needDeletedids.Add(id);
                }
            }

            try
            {
                IProxy proxy = ProxyFactory.CreateProxy();
                SmartBox.Console.Bo.SMC_PackageExtSyncToOutsideBO bo = proxy.CreateObject<SmartBox.Console.Bo.SMC_PackageExtSyncToOutsideBO>();

                bo.DeleteList(needDeletedids);
                data.Msg = "删除所选择同步结果操作成功！";
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                data.IsSuccess = false;
                data.Msg = e.Message;
            }


            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteBUAUserAsyncToInsideResult(string ids)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "" };
            string[] _ids = ids.Split(',');
            List<int> needDeletedids = new List<int>();
            int id = -1;
            foreach (string s in _ids)
            {
                if (int.TryParse(s, out id) == true)
                {
                    needDeletedids.Add(id);
                }
            }

            try
            {
                IProxy proxy = ProxyFactory.CreateProxy();
                SmartBox.Console.Bo.SMC_BUAUserSyncToInsideBO bo = proxy.CreateObject<SmartBox.Console.Bo.SMC_BUAUserSyncToInsideBO>();

                bo.DeleteList(needDeletedids);
                data.Msg = "删除所选择同步结果操作成功！";
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                data.IsSuccess = false;
                data.Msg = e.Message;
            }


            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteBUAUserAsyncToOutsideResult(string ids)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "" };
            string[] _ids = ids.Split(',');
            List<int> needDeletedids = new List<int>();
            int id = -1;
            foreach (string s in _ids)
            {
                if (int.TryParse(s, out id) == true)
                {
                    needDeletedids.Add(id);
                }
            }

            try
            {
                IProxy proxy = ProxyFactory.CreateProxy();
                SmartBox.Console.Bo.SMC_BUAUserSyncToOutsideBO bo = proxy.CreateObject<SmartBox.Console.Bo.SMC_BUAUserSyncToOutsideBO>();

                bo.DeleteList(needDeletedids);
                data.Msg = "删除所选择同步结果操作成功！";
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                data.IsSuccess = false;
                data.Msg = e.Message;
            }


            return Json(data);
        }

        public ActionResult ViewPackage(string id)
        {
            //二维码 截图 反馈 收藏 手册
            ViewData["PID"] = id;
            return View();
        }

        public ActionResult ViewPackage2(string id)
        {
            //二维码 截图 反馈 收藏 手册
            ViewData["PID"] = id;
            return View();
        }



        public ActionResult CreateOutApplicationPackage()
        {
            List<SelectListItem> unitList = new List<SelectListItem>();
            //获取当前用户的本单位
            IList<SmartBox.Console.Common.Entities.SMC_Unit> js = GetUnitData();

            foreach (SmartBox.Console.Common.Entities.SMC_Unit r in js)
            {
                //unitList.Add(new SelectListItem { Text = "全局", Value = "" });
                unitList.Add(new SelectListItem { Text = r.Unit_Name, Value = r.Unit_ID });
            }

            ViewData["Application"] = BoFactory.GetVersionTrackBo.QueryApplicationList().ToDataTable();

            ViewData["unitData"] = Newtonsoft.Json.JsonConvert.SerializeObject(js);

            //Dictionary<string, string> unitInfo = BoFactory.GetSMC_UserListBo.GetUnitByUL_UID(CurrentUser.UserUId);
            //if (unitInfo.Count > 0)
            //{
            //    string unitName = "本单位-" + unitInfo.ElementAt(0).Key;
            //    string unitId = unitInfo.ElementAt(0).Value;
            //    unitList.Add(new SelectListItem { Text = unitName, Value = unitId, Selected = true });
            //}
            //if (BoFactory.GetVersionTrackBo.IsSystemManager(CurrentUser.UserUId))
            //{
            //    //加入所有单位列表
            //    PageView view = new PageView();
            //    view.PageSize = 15;
            //    JsonFlexiGridData units = BoFactory.GetSMC_UnitBo.QueryUnitByUpperUnitCode(view, "");
            //    foreach (FlexiGridRow r in units.rows)
            //    {
            //        //unitList.Add(new SelectListItem { Text = "全局", Value = "" });
            //        unitList.Add(new SelectListItem { Text = r.cell[1], Value = r.id });
            //    }
            //}

            SelectHelper ddlApplicationSelect = new SelectHelper(BoFactory.GetVersionTrackBo.QueryApplicationCategoryList().ToDataTable());
            ViewData["AppID"] = ddlApplicationSelect.GetSelectList("DisplayName", "ID", false);

            SelectHelper chkClientTypeSelect = new SelectHelper(BoFactory.GetVersionTrackBo.QueryClientTypeList().ToDataTable());
            ViewData["ClientType"] = chkClientTypeSelect.GetSelectListItem("DisplayName", "ClientType", false);

            List<SelectListItem> IsRecom = new List<SelectListItem>();
            IsRecom.Add(new SelectListItem { Text = "推荐", Value = "1", Selected = true });
            IsRecom.Add(new SelectListItem { Text = "不推荐", Value = "0" });

            List<SelectListItem> IsMust = new List<SelectListItem>();
            IsMust.Add(new SelectListItem { Text = "必备", Value = "1", Selected = true });
            IsMust.Add(new SelectListItem { Text = "不必备", Value = "0" });
            ViewData["IsRecom"] = IsRecom;
            ViewData["IsMust"] = IsMust;
            ViewData["Unit"] = unitList;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadOutPackage(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            int i = Request.Files.Count;
            HttpPostedFileBase file = null;
            HttpPostedFileBase icon = null;
            string filePath = "";
            string packageFileName = "";

            SMC_PackageExt packageExt = new SMC_PackageExt();

            int packageId = BoFactory.GetVersionTrackBo.GetMaxPackageExtId() + 1;
            string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径
            string pDir = SAVEOUTPATH + @"\" + packageId.ToString();//相对路径 用安装包ID做文件夹名
            string saveDir = conSolePath + pDir;
            string fileUrl = "";

            try
            {
                if (i > 0)
                {
                    file = Request.Files[0];//安装包文件
                    filePath = file.FileName;
                    packageFileName = "Package4Out-" + Path.GetFileName(filePath);
                    string savePath = System.IO.Path.Combine(saveDir, packageFileName);

                    if (!System.IO.Directory.Exists(saveDir))
                    {
                        System.IO.Directory.CreateDirectory(saveDir);
                    }
                    if (System.IO.File.Exists(savePath))
                    {
                        System.IO.File.Delete(savePath);
                    }

                    file.SaveAs(savePath);
                    fileUrl = packageFileName;


                    //图标文件
                    icon = Request.Files[1];
                    string iconFileName = "AppIcon.png";
                    savePath = System.IO.Path.Combine(saveDir, iconFileName);
                    if (System.IO.File.Exists(savePath))
                    {
                        System.IO.File.Delete(savePath);
                    }
                    icon.SaveAs(savePath);

                    //插入数据
                    SMC_Package4Out package = new SMC_Package4Out();
                    package.po_ID = BoFactory.GetVersionTrackBo.GetMaxOutPackageId() + 1;
                    package.Name = packageFileName;
                    package.Type = "main";
                    package.ClientType = form["ClientType"];
                    package.DisplayName = form["DisplayName"];
                    package.Description = form["Description"];
                    package.Version = form["Version"];
                    package.BuildVer = -1;
                    package.DownloadUri = packageId.ToString() + @"\" + packageFileName;// savePath;
                    package.CreateUid = CurrentUser.UserUId;
                    package.CreateTime = DateTime.Now;
                    package.UpdateUid = CurrentUser.UserUId;
                    package.UpdateTime = DateTime.Now;

                    BoFactory.GetVersionTrackBo.InsertOutPackage(package);

                    //插入扩展表

                    packageExt.pe_id = packageId;
                    packageExt.pe_ClientType = package.ClientType;
                    packageExt.pe_IsTJ = form["IsRecom"];
                    packageExt.pe_IsBB = form["IsMust"];
                    packageExt.pe_Size = file.ContentLength;
                    packageExt.pe_Firmware = form["Firmware"];
                    packageExt.pe_UnitCode = form["Unit"];
                    packageExt.pe_CategoryID = form["AppID"];
                    packageExt.pe_Category = BoFactory.GetVersionTrackBo.GetApplicationCategory(form["AppID"]).DisplayName;
                    packageExt.pe_UnitName = BoFactory.GetSMC_UnitBo.Get(packageExt.pe_UnitCode).Unit_Name;
                    packageExt.pe_DisplayName = form["DisplayName"];
                    packageExt.pe_Description = form["Description"];
                    packageExt.pe_Version = form["Version"];
                    packageExt.pe_BuildVer = form["Version"];
                    packageExt.pe_Name = packageExt.pe_DisplayName;

                    //packageExt.pe_PictureUrl = form["IconUri"];
                    packageExt.TableName = "SMC_Package4Out";
                    packageExt.TableID = package.po_ID;
                    packageExt.pe_CreateUid = CurrentUser.UserUId;
                    packageExt.pe_CreatedTime = DateTime.Now;
                    packageExt.pe_UpdateUid = CurrentUser.UserUId;
                    packageExt.pe_UpdateTime = DateTime.Now;
                    packageExt.pe_ApplicationCode = Request.Form["ApplicationID"];

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
                    GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("app_sj_need_auth");
                    if (parm.ConfigValue == "1")
                    {
                        //需要审核
                        packageExt.pe_AuthStatus = 0;//待审核
                        packageExt.pe_AuthSubmitTime = DateTime.Now;
                        packageExt.pe_AuthSubmitName = CurrentUser.FullName;
                        packageExt.pe_AuthSubmitUID = CurrentUser.UserUId;
                        packageExt.pe_Direction = "发布";
                        packageExt.pe_Type = "";
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
                        packageExt.pe_Type = "";
                        packageExt.pe_UsefulStstus = "1";
                        packageExt.pe_UsefulTime = DateTime.Now;
                        packageExt.pe_UsefulOperatorUID = CurrentUser.UserUId;
                        packageExt.pe_UsefulOperatorName = CurrentUser.FullName;


                    }

                    //生成外网下载url,并生成二维码
                    string outWebHost = AppConfig.OutWebHost;
                    if (outWebHost.EndsWith("/") == false)
                        outWebHost = outWebHost + "/";
                    string url = outWebHost + AppConfig.OutPackUploadFolder + "/" + packageId.ToString() + '/' + packageFileName;
                    System.Drawing.Bitmap bmp = GetDimensionalCode(url);
                    string imgPath = System.IO.Path.Combine(saveDir, "二维码图片.jpeg");
                    bmp.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    packageExt.pe_2dPictureUrl = @"~/" + pDir + "/二维码图片.jpeg";
                    packageExt.pe_2dPictureUrl = packageExt.pe_2dPictureUrl.Replace(@"\\", "/");
                    packageExt.pe_DownloadUri = url.Replace(@"\", "/");
                    packageExt.pe_PictureUrl = @"~/" + pDir + "/AppIcon.png";
                    packageExt.pe_PictureUrl = packageExt.pe_PictureUrl.Replace(@"\\", "/");

                    BoFactory.GetVersionTrackBo.InsertPackageExt(packageExt);
                    packageExt.pe_FileUrl = "~/PackageExt/" + packageExt.pe_id + "/" + fileUrl;
                    BoFactory.GetSMC_PackageExtBO.Update(packageExt);
                    if (parm.ConfigValue == "0")
                    {
                        //不需要审核,直接同步到应用中心
                        //同步至外网数据
                        _SyncPackageExt(packageExt);
                    }
                }
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                data.IsSuccess = false;
                data.Msg = e.Message;
            }

            return Json(data, "text/html");
        }

        public ActionResult PackageGifList(string id)
        {
            //string TableName = Request.QueryString["TableName"];
            //ViewData["TableName"] = TableName;
            ////此id为packageoutId,再获取对应peID            
            //ViewData["id"] = BoFactory.GetVersionTrackBo.GetPeId(id, TableName);
            ViewData["id"] = id;
            return View();
        }

        public ActionResult PackageManualList(string id)
        {
            //string TableName = Request.QueryString["TableName"];
            //ViewData["TableName"] = TableName;
            //ViewData["id"] = BoFactory.GetVersionTrackBo.GetPeId(id, TableName);
            ViewData["id"] = id;
            return View();
        }

        public JsonResult OutPackageManualList(FormCollection form, string id)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryPackageManualList(view, id);
            return Json(data);
        }

        public ActionResult CreatePackageManual(string id)
        {
            ViewData["id"] = id;
            return View();
        }

        public ActionResult ModifyPackageManual(string id)
        {
            SMC_PackageManual pm = BoFactory.GetVersionTrackBo.GetPackageManual(id);
            ViewData["id"] = id;
            return View(pm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ModifyPackageManual(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                string pm_id = form[0];
                string pm_name = form[1];
                SMC_PackageManual pm = BoFactory.GetVersionTrackBo.GetPackageManual(pm_id);
                pm.pm_name = pm_name;
                pm.pm_updatetime = DateTime.Now;
                BoFactory.GetVersionTrackBo.UpdatePackageManual(pm);

            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }

            return Json(data, "text/html");
        }

        public ActionResult ModifyPackageExt(string id)
        {
            SMC_PackageExt pe = BoFactory.GetVersionTrackBo.GetPackageExt(id);
            string tableName = pe.TableName;


            if (tableName.ToUpper() == "SMC_PACKAGE4OUT")
            {
                //定向至外部包的修改地址     
                return RedirectToAction("EditOutPackage", "ApplicationManage", new { id = pe.TableID });
            }
            if (tableName.ToUpper() == "PACKAGE4AI")
            {
                //定向至内部包的修改地址
                return RedirectToAction("EditApplicationPackage", "ApplicationManage", new { id = pe.TableID });
            }
            if (tableName.ToUpper() == "WEBAPPLICATION")
            {
                return RedirectToAction("EditWebApplication", "ApplicationManage", new { id = pe.TableID });
            }

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CreatePackageManual(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            int i = Request.Files.Count;
            HttpPostedFileBase file = null;
            string filePath = "";
            string packageFileName = "";

            string packageId = form[0];

            string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径
            string pDir = SAVEOUTPATH + @"\" + packageId.ToString();//相对路径 用安装包ID做文件夹名
            string saveDir = conSolePath + pDir;

            try
            {
                if (i > 0)
                {
                    file = Request.Files[0];
                    filePath = file.FileName;
                    packageFileName = "手册-" + Path.GetFileName(filePath);
                    string savePath = System.IO.Path.Combine(saveDir, packageFileName);

                    if (!System.IO.Directory.Exists(saveDir))
                    {
                        System.IO.Directory.CreateDirectory(saveDir);
                    }
                    if (System.IO.File.Exists(savePath))
                    {
                        System.IO.File.Delete(savePath);
                    }

                    file.SaveAs(savePath);

                    //插入数据
                    SMC_PackageManual pm = new SMC_PackageManual();
                    pm.pm_id = BoFactory.GetVersionTrackBo.GetMaxPackageManualId() + 1;
                    pm.pe_id = Convert.ToInt32(packageId);
                    pm.pm_name = form[1];
                    if (SAVEOUTPATH.EndsWith("/") == false)
                        SAVEOUTPATH = SAVEOUTPATH + "/";
                    pm.pm_url = "~/" + SAVEOUTPATH + packageId.ToString() + @"\" + packageFileName;
                    pm.pm_url = pm.pm_url.Replace(@"\", "/");

                    pm.pm_createdtime = DateTime.Now;

                    BoFactory.GetVersionTrackBo.InsertPackageManual(pm);
                }
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                data.IsSuccess = false;
                data.Msg = e.Message;
            }

            return Json(data, "text/html");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeletePackageExts(string pe_ids)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功！" };
            string[] ids = pe_ids.Split(",".ToCharArray());

            string s = "ID为";
            if (ids != null && ids.Length > 0)
                foreach (string pe_id in ids)
                {
                    JsonReturnMessages _data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
                    _DeletePackageExt(pe_id, _data);
                    if (_data.IsSuccess == false)
                    {
                        //data.IsSuccess = false;
                        s += pe_id + "、";
                    }
                }
            s += "的app删除未成功！";
            data.Msg += s;
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeletePackageExt(string pe_id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };

            _DeletePackageExt(pe_id, data);
            return Json(data);
        }

        private void _DeletePackageExt(string pe_id, JsonReturnMessages data)
        {
            PageView view = new PageView();
            view.PageIndex = 0;
            view.PageSize = 15;
            string conSolePath = HttpRuntime.AppDomainAppPath;
            conSolePath = conSolePath + SAVEOUTPATH;
            //删除内容 outPackage packageExt 收藏 截图 手册  反馈           
            string id = BoFactory.GetVersionTrackBo.GetPackageExt(pe_id).TableID.ToString();//
            string tableName = BoFactory.GetVersionTrackBo.GetPackageExt(pe_id).TableName;

            try
            {
                //删除收藏
                BoFactory.GetVersionTrackBo.DeletePackageCollectByPEID(pe_id);

                //删除反馈
                BoFactory.GetVersionTrackBo.DeletePackageFAQByPEID(pe_id);

                //删除截图文件和截图
                var picList = BoFactory.GetVersionTrackBo.QueryPackageGifList(view, pe_id);
                if (picList.total > 0)
                {
                    foreach (FlexiGridRow r in picList.rows)
                    {
                        string pid = r.cell[0];
                        string pUrl = HttpRuntime.AppDomainAppPath + r.cell[3].TrimStart('~');
                        pUrl = pUrl.Replace("/", @"\");
                        if (System.IO.File.Exists(pUrl))
                        {
                            System.IO.File.Delete(pUrl);
                        }
                        BoFactory.GetVersionTrackBo.DeletePackagePicture(pid);
                    }
                }

                //删除手册文件和手册
                var manualList = BoFactory.GetVersionTrackBo.QueryPackageManualList(view, pe_id);
                if (manualList.total > 0)
                {
                    foreach (FlexiGridRow r in manualList.rows)
                    {
                        string mid = r.cell[0];
                        string mUrl = HttpRuntime.AppDomainAppPath + r.cell[2].TrimStart('~');
                        mUrl = mUrl.Replace("/", @"\");
                        if (System.IO.File.Exists(mUrl))
                        {
                            System.IO.File.Delete(mUrl);
                        }
                        BoFactory.GetVersionTrackBo.DeletePackageManual(mid);
                    }
                }

                //删除二维码图片和图标
                SMC_PackageExt packageExt = BoFactory.GetVersionTrackBo.GetPackageExt(pe_id);
                string url2Pic = "Url";
                string appIcon = "";
                if (packageExt != null && packageExt.pe_2dPictureUrl != null)
                {
                    url2Pic = HttpRuntime.AppDomainAppPath + packageExt.pe_2dPictureUrl.TrimStart('~');
                    url2Pic = url2Pic.Replace("/", @"\");
                    if (System.IO.File.Exists(url2Pic))
                    {
                        System.IO.File.Delete(url2Pic);
                    }
                }
                if (packageExt != null && packageExt.pe_PictureUrl != null)
                {

                    appIcon = HttpRuntime.AppDomainAppPath + packageExt.pe_PictureUrl.TrimStart('~');
                    appIcon = appIcon.Replace("/", @"\");
                    if (System.IO.File.Exists(appIcon))
                    {
                        System.IO.File.Delete(appIcon);
                    }
                }


                //删除Package和SMC_PackageExt
                if (tableName.ToUpper() == "SMC_PACKAGE4OUT")
                {
                    //删除安装包
                    SMC_Package4Out packageOut = null;
                    try
                    {
                        packageOut = BoFactory.GetVersionTrackBo.GetPackage4Out(id);
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.Error(ex);
                    }
                    if (packageOut != null)
                    {
                        string package4OutUrl = conSolePath + packageOut.DownloadUri;
                        if (System.IO.File.Exists(package4OutUrl))
                        {
                            System.IO.File.Delete(package4OutUrl);
                        }
                    }

                    BoFactory.GetVersionTrackBo.DeleteOutPackage(id);
                }
                if (tableName.ToUpper() == "PACKAGE4AI")
                {
                    Package4AI pi = null;
                    try
                    {
                        pi = BoFactory.GetVersionTrackBo.GetPackage4AI(id);
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.Error(ex);
                    }
                    //删除安装包
                    if (pi != null)
                    {
                        string package4In = pi.DownloadUri;
                        int index = package4In.LastIndexOf('/') + 1;
                        package4In = package4In.Remove(0, index);
                        string saveFilePath = Path.Combine(SAVEPATH, package4In);
                        if (System.IO.File.Exists(saveFilePath))
                        {
                            System.IO.File.Delete(saveFilePath);
                        }
                        BoFactory.GetVersionTrackBo.DeletePackage4AI(id);
                    }
                }
                if (tableName.ToUpper() == "WEBAPPLICATION")
                {
                    BoFactory.GetVersionTrackBo.DeleteWebApplication(id);
                }

                BoFactory.GetSMC_PackageExtSyncToOutsideBO.DeleteByPEID(pe_id);

                BoFactory.GetVersionTrackBo.DeletePackageExt(pe_id);

                try
                {
                    //删除应用中心的安装包
                    DeleteAppCenterPackageExt(int.Parse(pe_id));
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Info("删除外网应用中心的安装包失败:DeleteAppCenterPackageExt(int.Parse(pe_id));\r\n");
                    Log4NetHelper.Error(ex);
                }
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                data.IsSuccess = false;
                data.Msg = e.Message;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteOutPackage(string id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            PageView view = new PageView();
            view.PageIndex = 0;
            view.PageSize = 15;
            string conSolePath = HttpRuntime.AppDomainAppPath;
            //删除内容 outPackage packageExt 收藏 截图 手册  反馈
            string pe_id = BoFactory.GetVersionTrackBo.GetPeId(id, "SMC_Package4Out");//packageExt
            try
            {
                //删除收藏
                BoFactory.GetVersionTrackBo.DeletePackageCollectByPEID(pe_id);

                //删除反馈
                BoFactory.GetVersionTrackBo.DeletePackageFAQByPEID(pe_id);

                //删除截图文件和截图
                var picList = BoFactory.GetVersionTrackBo.QueryPackageGifList(view, pe_id);
                if (picList.total > 0)
                {
                    foreach (FlexiGridRow r in picList.rows)
                    {
                        string pid = r.cell[0];
                        string pUrl = conSolePath + r.cell[3];

                        if (System.IO.File.Exists(pUrl))
                        {
                            System.IO.File.Delete(pUrl);
                        }
                        BoFactory.GetVersionTrackBo.DeletePackagePicture(pid);
                    }
                }

                //删除手册文件和手册
                var manualList = BoFactory.GetVersionTrackBo.QueryPackageManualList(view, pe_id);
                if (manualList.total > 0)
                {
                    foreach (FlexiGridRow r in manualList.rows)
                    {
                        string mid = r.cell[0];
                        string mUrl = conSolePath + r.cell[2];

                        if (System.IO.File.Exists(mUrl))
                        {
                            System.IO.File.Delete(mUrl);
                        }
                        BoFactory.GetVersionTrackBo.DeletePackageManual(mid);
                    }
                }

                //删除二维码图片
                SMC_PackageExt packageExt = BoFactory.GetVersionTrackBo.GetPackageExt(pe_id);
                string url2Pic = "Url";
                if (packageExt != null)
                {
                    url2Pic = conSolePath + packageExt.pe_2dPictureUrl;

                    if (System.IO.File.Exists(url2Pic))
                    {
                        System.IO.File.Delete(url2Pic);
                    }
                }

                //删除安装包
                string package4OutUrl = conSolePath + SAVEOUTPATH + BoFactory.GetVersionTrackBo.GetPackage4Out(id).DownloadUri;
                if (System.IO.File.Exists(package4OutUrl))
                {
                    System.IO.File.Delete(package4OutUrl);
                }

                //删除SMC_Package4Out和SMC_PackageExt
                BoFactory.GetVersionTrackBo.DeleteOutPackage(id);
                BoFactory.GetVersionTrackBo.DeletePackageExt(pe_id);

            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                data.IsSuccess = false;
                data.Msg = e.Message;
            }

            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeletePackageManual(string id)
        {
            JsonReturnMessages date = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                SMC_PackageManual packageManual = BoFactory.GetVersionTrackBo.GetPackageManual(id);
                string filePath = packageManual.pm_url;
                string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径
                filePath = (conSolePath + filePath).Replace("~/", "");
                filePath = filePath.Replace("/", @"\");

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                BoFactory.GetVersionTrackBo.DeletePackageManual(id);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                date.IsSuccess = false;
                date.Msg = ex.Message;
            }
            return Json(date);
        }

        public JsonResult OutPackageGifList(FormCollection form, string id)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryPackageGifList(view, id);
            return Json(data);
        }

        public ActionResult CreatePackagePicture(string id)
        {
            ViewData["id"] = id;

            return View();
        }

        public ActionResult PackageCode(string id)
        {
            //string TableName = Request.QueryString["TableName"];
            //ViewData["TableName"] = TableName;
            //ViewData["id"] = BoFactory.GetVersionTrackBo.GetPeId(id, TableName);
            ViewData["id"] = id;
            SMC_PackageExt pe = BoFactory.GetVersionTrackBo.GetPackageExt(id);
            if (pe.pe_2dPictureUrl != null)
            {
                ViewData["ImgSrc"] = @"../../" + SAVEOUTPATH.TrimEnd('\\') + "/" + id + "/" + "二维码图片.jpeg";
                ViewData["url"] = pe.pe_DownloadUri;
            }
            else
            {
                ViewData["ImgSrc"] = "NoCode";
            }
            return View();
        }

        public JsonResult CreatePackageCode(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };

            string packageId = form[0];
            string url = form[1];
            try
            {
                System.Drawing.Bitmap bmp = GetDimensionalCode(url);

                //保存文件
                string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径
                string pDir = SAVEOUTPATH + @"\" + packageId.ToString();//相对路径 用安装包ID做文件夹名
                string saveDir = conSolePath + pDir;

                string savePath = System.IO.Path.Combine(saveDir, "二维码图片.jpeg");
                if (!System.IO.Directory.Exists(saveDir))
                {
                    System.IO.Directory.CreateDirectory(saveDir);
                }
                if (System.IO.File.Exists(savePath))
                {
                    System.IO.File.Delete(savePath);
                }
                bmp.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                SMC_PackageExt pe = BoFactory.GetVersionTrackBo.GetPackageExt(packageId);
                pe.pe_DownloadUri = url;
                pe.pe_2dPictureUrl = packageId + @"\" + "二维码图片.jpeg";
                BoFactory.GetVersionTrackBo.UpdatePackageExt(pe);
                //data.Msg = "创建二维码成功.";../../Images/icons/arrow_down.png
                //data.Msg = SAVEOUTPATH + packageId + @"\" + "二维码图片.jpeg";
                data.Msg = @"../../" + SAVEOUTPATH.TrimEnd('\\') + "/" + packageId + "/" + "二维码图片.jpeg";
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
            }
            return Json(data);
        }




        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CreatePackagePicture(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            int i = Request.Files.Count;
            HttpPostedFileBase file = null;
            string filePath = "";
            string packageFileName = "";

            string packageId = form[0];
            string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径
            string pDir = SAVEOUTPATH + @"\" + packageId.ToString();//相对路径 用安装包ID做文件夹名
            string saveDir = conSolePath + pDir;

            try
            {
                if (i > 0)
                {
                    file = Request.Files[0];
                    filePath = file.FileName;
                    packageFileName = "截图-" + Path.GetFileName(filePath);
                    string savePath = System.IO.Path.Combine(saveDir, packageFileName);

                    if (!System.IO.Directory.Exists(saveDir))
                    {
                        System.IO.Directory.CreateDirectory(saveDir);
                    }
                    if (System.IO.File.Exists(savePath))
                    {
                        System.IO.File.Delete(savePath);
                    }

                    file.SaveAs(savePath);

                    //插入数据
                    SMC_PackagePicture spp = new SMC_PackagePicture();
                    spp.pp_id = BoFactory.GetVersionTrackBo.GetMaxPackagePicId() + 1;
                    spp.pe_id = Convert.ToInt32(packageId);
                    spp.pp_path = "~/" + SAVEOUTPATH + packageId.ToString() + @"\" + packageFileName;
                    spp.pp_path = spp.pp_path.Replace(@"\", "/");
                    spp.pp_title = form[1];
                    spp.pp_desc = form[2];
                    spp.pp_CreatedDate = DateTime.Now;

                    BoFactory.GetVersionTrackBo.InsertPackagePicture(spp);
                }
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
                data.IsSuccess = false;
                data.Msg = e.Message;
            }

            return Json(data, "text/html");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeletePackagePicture(string id)
        {
            JsonReturnMessages date = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                SMC_PackagePicture packagePic = BoFactory.GetVersionTrackBo.GetPackagePicture(id);
                string filePath = packagePic.pp_path;
                string conSolePath = HttpRuntime.AppDomainAppPath;//服务器路径
                filePath = (conSolePath + filePath).Replace("~/", "");
                filePath = filePath.Replace("/", @"\");

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                BoFactory.GetVersionTrackBo.DeletePackagePicture(id);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                date.IsSuccess = false;
                date.Msg = ex.Message;
            }
            return Json(date);
        }

        public ActionResult PreviewPackagePicture(string id)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(100, 35);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
            g.Clear(System.Drawing.Color.White);
            g.FillRectangle(System.Drawing.Brushes.Red, 2, 2, 65, 31);
            g.DrawString("学习MVC", new System.Drawing.Font("黑体", 15f), System.Drawing.Brushes.Yellow, new System.Drawing.PointF(5f, 5f));
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            g.Dispose();
            bmp.Dispose();
            return File(ms.ToArray(), "image/jpeg");

        }

        public ActionResult PackageFAQList(string id)
        {
            //string TableName = Request.QueryString["TableName"];
            //ViewData["TableName"] = TableName;
            //ViewData["id"] = BoFactory.GetVersionTrackBo.GetPeId(id, TableName); 
            Service.ApplicationCenterWS.WebService ws = new Service.ApplicationCenterWS.WebService();
            Service.ApplicationCenterWS.SMC_PackageFAQ[] faqs = ws.GetNeedSyncToInsideFAQ();
            if (faqs != null && faqs.Length > 0)
            {
                for (int i = 0; i < faqs.Length; ++i)
                {
                    SMC_PackageFAQ faq = new SMC_PackageFAQ();
                    faq.pe_id = faqs[i].pe_id;
                    faq.pf_answer = faqs[i].pf_answer;
                    faq.pf_askdate = faqs[i].pf_askdate;
                    faq.pf_askemail = faqs[i].pf_askemail;
                    faq.pf_askmobile = faqs[i].pf_askmobile;
                    faq.pf_id = faqs[i].pf_id;
                    faq.pf_need_syncto_inside = false;
                    faq.pf_need_syncto_outside = false;
                    faq.pf_peplyman = faqs[i].pf_peplyman;
                    faq.pf_question = faqs[i].pf_question;
                    faq.pf_uid = faqs[i].pf_uid;
                    faq.pf_uname = faqs[i].pf_uname;

                    List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                    pars.Add(new KeyValuePair<string, object>("pf_id", faq.pf_id));
                    SMC_PackageFAQ _faq = BoFactory.GetSMC_PackageFAQBO.Get(pars);
                    if (_faq == null)
                    {
                        BoFactory.GetSMC_PackageFAQBO.Insert(faq);
                    }
                    if (faqs[i].pf_need_syncto_inside)
                    {
                        faqs[i].pf_need_syncto_inside = false;
                        ws.PackageFAQSync(faqs[i]);
                    }
                }
            }
            ViewData["id"] = id;
            return View();
        }

        public JsonResult OutPackageFAQList(FormCollection form, string id)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryPackageFAQList(view, id);
            return Json(data);
        }

        public ActionResult ReplyPackageFAQ(string id)
        {
            SMC_PackageFAQ packageFAQ;
            packageFAQ = BoFactory.GetVersionTrackBo.GetPackageFAQ(id);

            return View(packageFAQ);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ReplyPackageFAQ(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            string answer = form[0];
            string id = form[1];
            try
            {
                SMC_PackageFAQ packageFAQ = BoFactory.GetVersionTrackBo.GetPackageFAQ(id);
                packageFAQ.pf_answer = answer;
                if (answer != "")
                {
                    packageFAQ.pf_peplyman = CurrentUser.FullName;
                    packageFAQ.pf_need_syncto_outside = true;
                    packageFAQ.pf_need_syncto_inside = false;
                }
                else
                {
                    packageFAQ.pf_peplyman = "";
                }
                packageFAQ.pf_need_syncto_outside = false;
                packageFAQ.pf_need_syncto_inside = false;

                BoFactory.GetVersionTrackBo.UpdatePackageFAQ(packageFAQ);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }

            return Json(data);
        }

        public ActionResult PackageCollectionList(string id)
        {
            //string TableName = Request.QueryString["TableName"];
            //ViewData["TableName"] = TableName;
            //ViewData["id"] = BoFactory.GetVersionTrackBo.GetPeId(id, TableName); 
            ViewData["id"] = id;
            return View();
        }

        public JsonResult OutPackageCollectionList(FormCollection form, string id)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryPackageCollectList(view, id);
            return Json(data);
        }

        #region 分类管理
        public ActionResult ApplicationCategoryManage()
        {
            return View();
        }

        public JsonResult QueryApplicationCategoryList(FormCollection form)
        {
            JsonFlexiGridData data = new JsonFlexiGridData();
            PageView view = new PageView(form);
            data = BoFactory.GetVersionTrackBo.QueryApplicationCategoryList(view);
            return Json(data);
        }

        public ActionResult EditApplicationCategory(string id)
        {
            ApplicationCategory category;
            if (string.IsNullOrEmpty(id))
            {//新增
                category = new ApplicationCategory();
                category.Seq = 1;
            }
            else
            {//修改
                category = BoFactory.GetVersionTrackBo.GetApplicationCategory(id);
            }
            return View(category);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditApplicationCategory(ApplicationCategory category)
        {
            JsonReturnMessages date = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            category.UpdateTime = DateTime.Now;
            category.UpdateUid = CurrentUser.UserUId;
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(category.ID)) || category.ID == 0)
                {//新增
                    category.CreateTime = DateTime.Now;
                    category.CreateUid = CurrentUser.UserUId;
                    BoFactory.GetVersionTrackBo.InsertApplicationCategory(category);
                }
                else
                {//修改
                    BoFactory.GetVersionTrackBo.UpdateApplicationCategory(category);
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                date.IsSuccess = false;
                date.Msg = ex.Message;
            }
            return Json(date);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteApplicationCategory(string id)
        {
            JsonReturnMessages date = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                BoFactory.GetVersionTrackBo.DeleteApplicationCategory(id);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                date.IsSuccess = false;
                date.Msg = ex.Message;
            }
            return Json(date);
        }
        #endregion

        #region Home布局

        public ActionResult HomePlanList()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult QueryHomePlanList(FormCollection form)
        {
            PageView view = new PageView(form);
            JsonFlexiGridData data = BoFactory.GetVersionTrackBo.QueryHomePlanList(view);
            return Json(data);
        }

        public ActionResult EditHomePlan(string id)
        {
            HomePlan plan;
            if (string.IsNullOrEmpty(id))
            {//新增
                plan = new HomePlan();
            }
            else
            {//修改
                plan = BoFactory.GetVersionTrackBo.GetHomePlan(id);
            }
            return View(plan);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditHomePlan(HomePlan plan)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                plan.UpdateTime = DateTime.Now;
                plan.UpdateUid = CurrentUser.UserUId;
                if (plan.ID <= 0)
                {//新增
                    plan.CreateTime = DateTime.Now;
                    plan.CreateUid = CurrentUser.UserUId;
                    BoFactory.GetVersionTrackBo.InsertHomePlan(plan);
                }
                else
                {//修改
                    BoFactory.GetVersionTrackBo.UpdateHomePlan(plan);
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.Msg = ex.Message;
                data.IsSuccess = false;
            }
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteHomePlan(string id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                BoFactory.GetVersionTrackBo.DeleteHomePlan(id);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.Msg = ex.Message;
                data.IsSuccess = false;
            }
            return Json(data);
        }

        public ActionResult CopyHomePlan(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Response.Write("未找到对应的布局");
                Response.End();
            }
            HomePlan plan = BoFactory.GetVersionTrackBo.GetHomePlan(id);
            return View(plan);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CopyHomePlan(HomePlan copyPlan, string id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                copyPlan.CreateTime = DateTime.Now;
                copyPlan.UpdateTime = DateTime.Now;
                copyPlan.CreateUid = CurrentUser.UserUId;
                copyPlan.UpdateUid = CurrentUser.UserUId;
                BoFactory.GetVersionTrackBo.CopyHomePlan(copyPlan, id);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.Msg = ex.Message;
                data.IsSuccess = false;
            }
            return Json(data);
        }

        public ActionResult EditHomePlanDesign(string planID)
        {
            ViewData["PlanID"] = planID;
            IList<HomePlanDesign> designList = BoFactory.GetVersionTrackBo.QueryHomePlanDesignList(planID);
            List<Application> applicationList = new List<Application>();
            BoFactory.GetVersionTrackBo.QueryApplicationList().ForEach(a =>
            {
                bool exist = false;
                designList.ForEach(d =>
                {
                    if (d.AppID == a.ID)
                    {
                        exist = true;
                        return;
                    }
                });
                if (!exist)
                {
                    applicationList.Add(a);
                }
            });
            SelectHelper ddlAppSelect = new SelectHelper(applicationList.ToDataTable());
            string[] formats = BoFactory.GetVersionTrackBo.GetHomePlan(planID).Format.Split(',');
            ViewData["PageW"] = formats[0];
            ViewData["PageH"] = formats[1];
            ViewData["AppList"] = ddlAppSelect.GetSelectListItem("DisplayName", "ID", false);
            ViewData["DesignList"] = BoFactory.GetVersionTrackBo.QueryHomePlanDesignList(planID);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteHomePlanDesign(string planID, string appID)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                if (string.IsNullOrEmpty(planID))
                {
                    throw new ArgumentNullException("布局ID不能为空");
                }
                if (string.IsNullOrEmpty(appID))
                {
                    throw new ArgumentNullException("应用ID不能为空");
                }
                BoFactory.GetVersionTrackBo.DeleteHomePlanDesign(planID, appID);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.Msg = ex.Message;
                data.IsSuccess = false;
            }
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CreateHomePlanDesign(int pageW, int pageH, int page, int x, int y, int w, int h, string planID, string appID, string imgID)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                if (page < 1)
                {
                    throw new Exception("请填入正确的页数");
                }
                if (x > (pageW - 1) || x < 0)
                {
                    throw new Exception("请填入正确的X坐标");
                }
                if (y > (pageH - 1) || y < 0)
                {
                    throw new Exception("请填入正确的Y坐标");
                }
                if (w > pageW || w < 0)
                {
                    throw new Exception("请填入正确的宽");
                }
                if (h > pageH || h < 0)
                {
                    throw new Exception("请填入正确的高");
                }
                if (x + w > pageW)
                {
                    throw new Exception("宽超过边界,请重新输入");
                }
                if (y + h > pageH)
                {
                    throw new Exception("高超过边界,请重新输入");
                }

                if (CheckPlan(planID, pageW, page, x, y, w, h))
                {
                    throw new Exception("坐标上已存在其他应用");
                }
                x = pageW * (page - 1) + x;
                HomePlanDesign design = new HomePlanDesign();
                design.UpdateTime = DateTime.Now;
                design.CreateTime = DateTime.Now;
                design.UpdateUid = CurrentUser.UserUId;
                design.CreateUid = CurrentUser.UserUId;
                design.AppID = Convert.ToInt32(appID);
                design.PlanID = Convert.ToInt32(planID);
                design.Location = string.Format("{0},{1}", x, y);
                design.Size = string.Format("{0},{1}", w, h);
                design.Type = "Image";
                if (!string.IsNullOrEmpty(imgID))
                {
                    design.ValueUri = string.Format(@"Server://beyondbit.smartbox.server.image/{0}", imgID);
                }
                BoFactory.GetVersionTrackBo.InsertHomePlanDesign(design);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.Msg = ex.Message;
                data.IsSuccess = false;
            }
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InterchangeHomePlanDesign(int pageW, int pageH, string app1ID, string app2ID, string planID)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            try
            {
                if (string.IsNullOrEmpty(planID))
                {
                    throw new ArgumentNullException("布局ID不能为空");
                }
                if (string.IsNullOrEmpty(app1ID) || string.IsNullOrEmpty(app2ID))
                {
                    throw new ArgumentNullException("应用ID不能为空");
                }
                BoFactory.GetVersionTrackBo.InterchangeHomePlanDesign(pageW, pageH, app1ID, app2ID, planID);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                data.Msg = ex.Message;
                data.IsSuccess = false;
            }
            return Json(data);
        }

        private bool CheckPlan(string planID, int pageW, int page, int x, int y, int w, int h)
        {
            bool isCrash = false;
            IList<HomePlanDesign> designList = BoFactory.GetVersionTrackBo.QueryHomePlanDesignList(planID);
            designList.ForEach(d =>
            {
                int tpage = (Convert.ToInt32(d.Location.Split(',')[0]) / pageW) + 1;
                int tx = Convert.ToInt32(d.Location.Split(',')[0]) % pageW;
                int ty = Convert.ToInt32(d.Location.Split(',')[1]);
                int tw = Convert.ToInt32(d.Size.Split(',')[0]);
                int th = Convert.ToInt32(d.Size.Split(',')[1]);
                if (page == tpage && ((x >= tx && x < tx + tw) || (x + w > tx && x + w <= tx + tw))
                                  && ((y >= ty && y < ty + th) || (y + h > ty && y + h <= ty + th)))
                {
                    isCrash = true;
                    return;
                }
            });
            return isCrash;
        }
        #endregion

        #region 全局配置

        public ViewResult GlobalConfigManager()
        {
            SearchConfig search = new SearchConfig();
            search.PluginCode = Constants.MobileMainName;
            search.ConfigCategoryCode = Constants.GlobalConfig;
            IList<ConfigInfo> listconfigs = BoFactory.GetVersionTrackBo.GetConfigList(search);
            return View();
        }

        public JsonResult SaveGlobalConfig()
        {
            return Json("");
        }
        #endregion
    }
}
