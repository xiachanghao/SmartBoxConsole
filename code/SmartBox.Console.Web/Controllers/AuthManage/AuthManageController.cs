using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartBox.Console.Bo;
using SmartBox.Console.Common;
using System.Web.Script.Serialization;
using System.Collections;
using SmartBox.Console.Common.Entities;
using Beyondbit.Framework.Core.Proxy;
using System.IO;
using Beyondbit.BUA.Client;

namespace SmartBox.Console.Web.Controllers.AuthManage
{
    public partial class AuthManageController : MyControllerBase
    {
        //
        // GET: /AuthManage/

        public ActionResult Index() 
        {
            return View();
        }
        public ActionResult SyncBUAUserToInside()
        {
            //IProxy proxy = ProxyFactory.CreateProxy();
            //SMC_BUAUserSyncToInsideBO buaSyncBO = proxy.CreateObject<SMC_BUAUserSyncToInsideBO>();
            //buaSyncBO.BUAUserSyncToInside();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SyncBUAUserToInside(string ids)
        {
            IProxy proxy = ProxyFactory.CreateProxy();
            SMC_BUAUserSyncToInsideBO buaSyncBO = Bo.BoFactory.GetSMC_BUAUserSyncToInsideBO;//proxy.CreateObject<SMC_BUAUserSyncToInsideBO>();
            Hashtable result = new Hashtable();
            result["IsSuccess"] = true;
            result["Msg"] = "操作已提交！";

            try
            {
                buaSyncBO.BUAUserSyncToInside(ids, result);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                result["Msg"] += ex.Message;
            }

            try
            {
                buaSyncBO.BUAUserSyncToOutside(ids);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }
            return Json(result);
        }

        private void GetBUAChildUnit(Hashtable pHash, IOrgService os, string orgCode)
        {
            List<Hashtable> lst = new List<Hashtable>();
            Org[] orgs = os.QuerySubOrgs(orgCode);
            if (orgs != null && orgs.Length > 0)
            {
                foreach (Org org in orgs)
                {
                    Hashtable horg = new Hashtable();
                    horg["id"] = org.OrgCode;
                    horg["text"] = org.OrgName;
                    lst.Add(horg);
                    GetBUAChildUnit(horg, os, org.OrgCode);
                }
            }

            pHash["children"] = lst;
        }

        public ActionResult GetSyncBUAUnitTree()
        {
            Hashtable r = new Hashtable();
            r["id"] = "";
            r["text"] = "单位";
            
            Bo.BoFactory.GetSMC_UnitBo.GetUnitHashTree(r);

            List<Hashtable> rl = new List<Hashtable>();
            rl.Add(r);
            return Json(rl, JsonRequestBehavior.AllowGet); 
            
            //IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
            //Org topOrg = os.GetTopOrg();
            //if (!this.IsSystemManager)
            //{
            //    //单位管理员取自己单位的组织树
            //    string unitCode = os.GetUnitCode(ObjectType.User, CurrentUser.UserUId);
            //    topOrg = os.GetOrgBaseInfo(ObjectType.Org, unitCode);
            //}
            

            //GetBUAChildUnit(r, os, topOrg.OrgCode);

                       
        }

        public ActionResult GetSyncBUAUser(string orgCode, int pageSize, int pageIndex)
        {
            IUserService us = Beyondbit.BUA.Client.ServiceFactory.Instance().GetUserService();
            //Beyondbit.BUA.Client.User[] users = us.QueryUsersByObjectCode(ObjectType.Org, orgCode, "", orgCode, true, UserLockedType.UnLocked, pageSize, pageIndex);
            Beyondbit.BUA.Client.User[] users = Bo.BoFactory.GetSMC_UserBo.GetBUAUsersByOrgCode(orgCode, pageSize, pageIndex);
            int recordCount = users.Count();
            int startIndex = (pageIndex - 1) * pageSize;
            int endIndex = 0;
            if (recordCount < pageSize)
            {
                endIndex = recordCount - 1;
            }
            else if (recordCount >= pageSize * pageIndex)
            {
                endIndex = (pageIndex) * pageSize - 1;
            }
            else if (recordCount < pageSize * pageIndex)
            {
                endIndex = recordCount - 1;
            }

            List<Beyondbit.BUA.Client.User> pagedList = new List<Beyondbit.BUA.Client.User>();
            for (int i = startIndex; i <= endIndex; ++i)
            {
                pagedList.Add(users[i]);
            }

            Hashtable result = new Hashtable();
            result["total"] = recordCount;
            result["rows"] = pagedList.ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AuthMain(string t)
        {
            bool isSystemManager = BoFactory.GetVersionTrackBo.IsSystemManager(this.User.Identity.Name);
            string root_unit_id = "";
            string current_unit_id = "";

            t = Server.UrlEncode(t);
            var url = Server.UrlEncode(Request.QueryString["url"]);

            if (String.IsNullOrEmpty(t))
                t = "unit";
            //var u = Request.QueryString["Unit_ID"];
            Hashtable data = new Hashtable();

            if (t.ToLower() == "function")
            {
                if (!isSystemManager)
                {
                    data = null;
                }
                else
                {
                    data = BoFactory.GetSMC_FunctionsBo.QueryFunctionsTreeDataByUpperFNID(0);
                }
            }
            else
            {
                string unitId = BoFactory.GetSMC_UserListBo.GetUnitIdByUID(this.User.Identity.Name);
                //root_unit_id = unitId;
                data = BoFactory.GetSMC_UnitBo.QueryUnitTreeData(t, root_unit_id, unitId, !isSystemManager);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonStr = serializer.Serialize(data);

            this.ViewData["nodesFunctions"] = jsonStr;
            this.ViewData["url"] = url;
            return View();
        }

        public ActionResult UserList()
        {
            bool show_button = true;
            string unit_id = Request.QueryString["Unit_ID"];
            bool isSystemManager = BoFactory.GetVersionTrackBo.IsSystemManager(this.User.Identity.Name);
            string manager_Unit_ID = BoFactory.GetSMC_UserListBo.GetUnitIdByUID(this.User.Identity.Name);
            
            if (!isSystemManager && !String.IsNullOrEmpty(unit_id) && unit_id == "")
            {
                show_button = false;
            }

            if (!String.IsNullOrEmpty(unit_id))
            {
                SMC_Unit unit = BoFactory.GetSMC_UnitBo.Get(unit_id);
                bool isunit_in_managerunit = unit.Unit_Path.IndexOf(manager_Unit_ID.ToString()) != -1;
                if (!isSystemManager && !isunit_in_managerunit)
                {
                    show_button = false;
                }
            }

            this.ViewData["show_button"] = show_button;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DragNodeAsChild(int childNodeId, int parentNodeId, string t)
        {
            bool isSystemManager = BoFactory.GetVersionTrackBo.IsSystemManager(this.User.Identity.Name);
            string root_unit_id = "";
            bool r = BoFactory.GetSMC_FunctionsBo.DragNodeAsChild(childNodeId, parentNodeId);
            Hashtable rr = new Hashtable();
            rr["d"] = r ? "移动模块成功！" : "移动模块失败！";
            rr["r"] = r;

            if (String.IsNullOrEmpty(t))
                t = "unit";
            //var u = Request.QueryString["Unit_ID"];
            Hashtable data = new Hashtable();

            if (t.ToLower() == "function")
            {
                data = BoFactory.GetSMC_FunctionsBo.QueryFunctionsTreeDataByUpperFNID(0);
            }
            else
            {
                string unitId = BoFactory.GetSMC_UserListBo.GetUnitIdByUID(this.User.Identity.Name);
                data = BoFactory.GetSMC_UnitBo.QueryUnitTreeData(t, root_unit_id, unitId, !isSystemManager);
            }
            rr["nodeData"] = data;
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //string jsonStr = serializer.Serialize(data);

            //this.ViewData["nodesFunctions"] = jsonStr;
            return Json(rr);
        }

        public ActionResult UnitList()
        {
            string upper_unit_id = Request.QueryString["upper_unit_id"];
            //if (upper_unit_id == "") { upper_unit_id = "0"; }
            ViewData["UNIT_ID"] = upper_unit_id;

            return View();
        }

        public ActionResult RoleList()
        {
            return View();
        }

        public JsonResult SavePersonsTree(string ids, string ids_changed, int roleId)
        {

            string[] idsArray = ids.Split(",".ToCharArray());
            string[] idsArray_changed = ids_changed.Split(",".ToCharArray());
            foreach (string sId in idsArray)
            {
                if (!sId.StartsWith("u_"))
                    continue;
                if (!String.IsNullOrEmpty(sId))
                {
                    SMC_UserList user = BoFactory.GetSMC_UserListBo.Get(int.Parse(sId.Replace("u_", "")));
                    user.Role_ID = roleId;

                    BoFactory.GetSMC_UserListBo.InsertOrUpdate(user);
                }
            }

            foreach (string sId in idsArray_changed)
            {
                if (!sId.StartsWith("u_"))
                    continue;
                if (!String.IsNullOrEmpty(sId))
                {
                    SMC_UserList user = BoFactory.GetSMC_UserListBo.Get(int.Parse(sId.Replace("u_", "")));
                    user.Role_ID = 0;

                    BoFactory.GetSMC_UserListBo.InsertOrUpdate(user);
                }
            }
            return Json(new { result = true, d = "保存成功！" });
        }

        public JsonResult SaveFunctionsTree(string ids, string ids_changed, int roleId)
        {

            string[] idsArray = ids.Split(",".ToCharArray());
            string[] idsArray_changed = ids_changed.Split(",".ToCharArray());
            foreach (string sId in idsArray)
            {
                if (!String.IsNullOrEmpty(sId))
                {
                    SMC_FunctionRole fr = new SMC_FunctionRole();
                    fr.FN_ID = int.Parse(sId);
                    fr.Role_ID = roleId;
                    fr.FR_CreatedTime = DateTime.Now;
                    fr.FR_CreatedUser = "";
                    fr.FR_UpdateTime = DateTime.Now;
                    fr.FR_UpdateUser = "";
                    fr.FR_Sequence = 0;
                    BoFactory.GetSMC_FunctionRoleBo.InsertOrUpdate(fr);
                }
            }

            foreach (string sId in idsArray_changed)
            {
                if (!String.IsNullOrEmpty(sId))
                {
                    BoFactory.GetSMC_FunctionRoleBo.Delete(int.Parse(sId), roleId);
                }
            }
            return Json(new { result = true, d = "保存成功！" });
        }

        public ActionResult PersonsTreeSelect()
        {
            string sRole = Request.QueryString["RoleID"];
            string sUnit = Request.QueryString["UnitID"];
            int role_id = 0; string unit_id = "";
            if (!String.IsNullOrEmpty(sRole))
            {
                role_id = int.Parse(sRole);
            }
            if (!String.IsNullOrEmpty(sUnit))
            {
                unit_id = sUnit;
            }
            Hashtable data = BoFactory.GetSMC_UserListBo.QueryPersonsTreeDataByUnit(unit_id, role_id);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonStr = serializer.Serialize(data);
            this.ViewData["nodesPersons"] = jsonStr;
            return View();
        }

        public ActionResult FunctionsTreeSelect()
        {
            string sRole = Request.QueryString["RoleID"];
            string sUnit = Request.QueryString["UnitID"];
            int role_id = 0, unit_id = 1;
            if (!String.IsNullOrEmpty(sRole))
            {
                role_id = int.Parse(sRole);
            }

            Hashtable data = BoFactory.GetSMC_FunctionsBo.QueryFunctionsTreeDataByUnit(sUnit, role_id);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonStr = serializer.Serialize(data);
            this.ViewData["nodesFunctions"] = jsonStr;
            return View();
        }

        public ActionResult SetUserListRole(FormCollection form)
        {
            List<SelectListItem> roleList = new List<SelectListItem>();
            roleList.Add(new SelectListItem { Text = "请选择角色", Value = "0", Selected = true });
            PageView view = new PageView(form);
            string sUser = Request.QueryString["ul_id"];
            string sUnit = Request.QueryString["UnitID"];
            int user_id = 0; string unit_id = "";
            if (!String.IsNullOrEmpty(sUser))
            {
                user_id = int.Parse(sUser);
            }
            if (!String.IsNullOrEmpty(sUnit))
            {
                unit_id = sUnit;
            }
            SMC_UserList su = BoFactory.GetSMC_UserListBo.Get(user_id);
            JsonFlexiGridData data = BoFactory.GetSMC_RoleBo.QueryRolesByUnitID(view,unit_id);
            if (data.total > 0)
            {
                foreach (FlexiGridRow r in data.rows)
                {
                    roleList.Add(new SelectListItem { Text = r.cell[0], Value = r.id,Selected= r.cell[0]== su.Role_ID.ToString()});
                }
            }

            ViewData["Role_ID"] = roleList;
            ViewData["user_id"] = su.UL_ID;
            return View(su);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SetUserListRole(string user_id, string Role_ID)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };

            try
            {
                SMC_UserList su = BoFactory.GetSMC_UserListBo.Get(Int32.Parse(user_id));
                su.Role_ID = Int32.Parse(Role_ID);

                BoFactory.GetSMC_UserListBo.InsertOrUpdate(su);
            }
            catch (Exception e)
            {
                data.IsSuccess = false;
                data.Msg = e.Message;
            }

            return Json(data);
        }
        //public ActionResult AllUnits()
        //{
        //    Hashtable data = new Hashtable();
        //    data = BoFactory.GetSMC_UnitBo.QueryUnitTreeData();
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    string jsonStr = serializer.Serialize(data);

        //    this.ViewData["nodesFunctions"] = jsonStr;
        //    return View();
        //}

        public ActionResult Allusers()
        {
            string sUnit = Request.QueryString["UnitID"];
            int unitId = 0;
            if (!String.IsNullOrEmpty(sUnit))
            {
                unitId = int.Parse(sUnit);
            }
            this.ViewData["unitId"] = unitId;
            return View();
        }

        public ActionResult GetAllUsers(FormCollection form)
        {
            string sUnit = Request.QueryString["UnitID"];
            string unitId = "";
            if (!String.IsNullOrEmpty(sUnit))
            {
                unitId = sUnit;
            }
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);

            data = BoFactory.GetSMC_UserListBo.QueryUserListByUnitID(view, unitId);

            return Json(data);
        }

        public ActionResult AllRoles()
        {
            string sUnit = Request.QueryString["UnitID"];
            int unitId = 0;
            if (!String.IsNullOrEmpty(sUnit))
            {
                unitId = int.Parse(sUnit);
            }
            this.ViewData["unitId"] = unitId;
            return View();
        }

        public ActionResult GetAllRoles(FormCollection form)
        {
            string sUnit = Request.QueryString["UnitID"];
            string unitId = "";
            if (!String.IsNullOrEmpty(sUnit))
            {
                unitId = sUnit;
            }
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);

            data = BoFactory.GetSMC_RoleBo.QueryRolesByUnitID(view, unitId);

            return Json(data);
        }

        public ActionResult RedirectAction(string unit_id)
        {
            return View();
        }

        public ActionResult GetUserListByUnitCode(FormCollection form, string unit_id)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            string _unit_id = "";
            if (unit_id != null)
            {
                _unit_id = unit_id;
            }
            data = BoFactory.GetSMC_UserListBo.QueryUserListByUnitID(view, _unit_id);
            bool isSystemManager = BoFactory.GetVersionTrackBo.IsSystemManager(this.User.Identity.Name);
            if (!isSystemManager && unit_id == "")
            {
                data = null;
            }

            //获取角色名称
            if (data != null && data.total > 0)
            {
                foreach (var r in data.rows)
                {
                    int role_id = 0;
                    if (r.cell[3] != null)
                    {
                        role_id = Int32.Parse(r.cell[3]);
                    }
                    if (role_id > 0)
                    {
                        r.cell[3] = BoFactory.GetSMC_RoleBo.Get(role_id).Role_Name;
                    }
                    else
                    {
                        r.cell[3] = "未分配角色";
                    }
                }
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult GetRoleListByUnitCode(FormCollection form, string unit_id)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            //int _unit_id = 1;
            //if (unit_id != null)
            //{
            //    _unit_id = int.Parse(unit_id);
            //}
            data = BoFactory.GetSMC_RoleBo.QueryRolesByUnitID(view, Server.UrlEncode(unit_id));
            return Json(data);
        }

        public ActionResult AddModifyUnit()
        {
            string upper_Unit_ID = Request.QueryString["Upper_Unit_ID"];
            string unit_id = Request.QueryString["Unit_ID"];

            if (!String.IsNullOrEmpty(upper_Unit_ID))
            {
                string upper_unit_name = BoFactory.GetSMC_UnitBo.Get(upper_Unit_ID).Unit_Name;
                ViewData["upper_unit_name"] = upper_unit_name;
            }
            else
            {
                ViewData["upper_unit_name"] = "";
            }

            if (String.IsNullOrEmpty(unit_id))
                return View();

            SMC_Unit unit = BoFactory.GetSMC_UnitBo.Get(unit_id);
            if (unit == null)
            {
                return View();
            }
            else
            {               
                return View(unit);
            }
        }


        

        public ActionResult AddModifyRole()
        {
            string sRole = Request.QueryString["RoleID"];
            string sUnit = Request.QueryString["UnitID"];
            int role_id = 0; string unit_id = "";
            if (!String.IsNullOrEmpty(sRole))
            {
                role_id = int.Parse(sRole);
            }
            if (!String.IsNullOrEmpty(sUnit))
            {
                unit_id = sUnit;
            }
            this.ViewData["role_id"] = role_id;
            this.ViewData["unit_id"] = unit_id;
            if (role_id == 0)
            {
                return View();
            }
            else
            {
                SMC_Role sr = BoFactory.GetSMC_RoleBo.Get(role_id);
                return View(sr);
            }
        }

        public ActionResult CopyBUAUnit()
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            Beyondbit.BUA.Client.IOrgService svc = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
            Org[] orgs = svc.QueryUnits("");
            return Json(data);
        }

        /// <summary>
        /// 同步单位至应用中心
        /// </summary>
        /// <returns></returns>
        public ActionResult SyncUnitToAppCenter()
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            
            Service.ApplicationCenterWS.WebService ws = new Service.ApplicationCenterWS.WebService();
            IList<SMC_Unit> units = BoFactory.GetSMC_UnitBo.GetAllUnits();
            if (units != null && units.Count > 0) {
                Service.ApplicationCenterWS.SMC_Unit[] _units = new Service.ApplicationCenterWS.SMC_Unit[units.Count];
                for (int i = 0; i < units.Count; ++ i)
                {
                    SMC_Unit unit = units[i];
                    Service.ApplicationCenterWS.SMC_Unit _unit = new Service.ApplicationCenterWS.SMC_Unit();
                    _unit.Unit_CreatedTime = unit.Unit_CreatedTime;
                    _unit.Unit_CreatedUser = unit.Unit_CreatedUser;
                    _unit.Unit_Demo = unit.Unit_Demo;
                    _unit.Unit_ID = unit.Unit_ID;
                    _unit.Unit_Name = unit.Unit_Name;
                    _unit.Unit_Path = unit.Unit_Path;
                    _unit.Unit_Sequence = unit.Unit_Sequence;
                    _unit.Unit_UpdateTime = unit.Unit_UpdateTime;
                    _unit.Unit_UpdateUser = unit.Unit_UpdateUser;
                    _unit.Upper_Unit_ID = unit.Upper_Unit_ID;

                    _units[i] = _unit;
                }
                ws.SMC_UnitSync(_units);
            }
            return Json(data);
        }

        public ActionResult AddModifyRoleSave(int Role_ID, string Unit_ID, string Role_Name, int Role_Sequence, string Role_Demo)
        {

            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            SMC_Role role = null;
            if (Role_ID == 0)
            {
                role = new SMC_Role();
                role.Role_CreatedTime = DateTime.Now;
            }
            else
            {
                role = BoFactory.GetSMC_RoleBo.Get(Role_ID);
            }

            role.Role_ID = Role_ID;
            role.Unit_ID = Unit_ID;
            role.Role_Name = Role_Name;
            role.Role_Sequence = Role_Sequence;
            role.Role_Demo = Role_Demo;

            BoFactory.GetSMC_RoleBo.InsertOrUpdate(role);

            return Json(data);
        }

        public ActionResult AddModifyUser()
        {
            string sUser = Request.QueryString["UserID"];
            string sUnit = Request.QueryString["Upper_Unit_ID"];
            int user_id = 0; string unit_id = "";
            if (!String.IsNullOrEmpty(sUser))
            {
                user_id = int.Parse(sUser);
            }
            if (!String.IsNullOrEmpty(sUnit))
            {
                unit_id = sUnit;
            }

            //string unit_Name = BoFactory.GetSMC_UnitBo.Get(unit_id).Unit_Name;
            this.ViewData["user_id"] = user_id;
            this.ViewData["unit_id"] = unit_id;
            //this.ViewData["Unit_Name"] = unit_Name;

            List<SelectListItem> unitList = new List<SelectListItem>();
            IList<SMC_Unit> units = BoFactory.GetSMC_UnitBo.GetAllUnits();


         

            ViewData["Unit_Name"] = unitList;
           

            if (user_id == 0)
            {
                foreach (var r in units)
                {
                    unitList.Add(new SelectListItem { Text = r.Unit_Name, Value = r.Unit_ID.ToString(), Selected = r.Unit_ID == unit_id });
                }
                return View();
            }
            else
            {
                SMC_UserList ur = BoFactory.GetSMC_UserListBo.Get(user_id);
                this.ViewData["unit_id"] = ur.Unit_ID;
                SMC_Unit un = BoFactory.GetSMC_UnitBo.Get(ur.Unit_ID);
                string unit_Name = un == null ? "" : un.Unit_Name;
                ur.Unit_Name = unit_Name;
                foreach (var r in units)
                {
                    unitList.Add(new SelectListItem { Text = r.Unit_Name, Value = r.Unit_ID.ToString(), Selected = r.Unit_ID == ur.Unit_ID });
                }

                return View(ur);
            }

        }

        public ActionResult AddModifyUserSave(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            
            SMC_UserList ur = null;
            int userId =Convert.ToInt32(form[0]);
            if (userId == 0)
            {
                ur = new SMC_UserList();
                ur.UL_CreatedTime = DateTime.Now;
                ur.UL_ID = -1;
                ur.UL_UID = form["UL_UID"];
                ur.UL_PWD = form["UL_PWD"];
                ur.Unit_ID = form["Unit_Name"];
            }
            else
            {
                ur = BoFactory.GetSMC_UserListBo.Get(userId);
            }

            ur.Unit_ID =form["unit_id"];                    
            ur.UL_Name = form["UL_Name"];
            ur.UL_MobilePhone = form["UL_MobilePhone"];
            ur.UL_MailAddress = form["UL_MailAddress"];
            ur.UL_Sequence = Convert.ToInt32(form["UL_Sequence"]);
            ur.UL_Demo = form["UL_Demo"];
            ur.Unit_ID = form["Unit_Name"];
            ur.UL_UID = ur.UL_UID.Trim();
            try
            {
                BoFactory.GetSMC_UserListBo.InsertOrUpdate(ur);
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }

            return Json(data);
        }



        public ActionResult ResetUserPassWord()
        {
            string sUser = Request.QueryString["UserID"];
            int user_id = 0;
            if (!String.IsNullOrEmpty(sUser))
            {
                user_id = int.Parse(sUser);
            }
            SMC_UserList ur = BoFactory.GetSMC_UserListBo.Get(user_id);
            var isM = BoFactory.GetVersionTrackBo.IsSystemManager(CurrentUser.UserUId);
            this.ViewData["user_id"] = user_id;
            if (isM)
            {
                this.ViewData["IsManager"] = "manager";
            }
            else
            {
                this.ViewData["IsManager"] = "notmanager";
            }
            return View(ur);
        }

        public ActionResult ResetUserPassWordSave(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "修改成功" };
            SMC_UserList ur = null;
            int userId = Convert.ToInt32(form["user_id"]);           
            ur = BoFactory.GetSMC_UserListBo.Get(userId);
            var isM = BoFactory.GetVersionTrackBo.IsSystemManager(CurrentUser.UserUId);
            if (!isM && ur.UL_PWD != form["UL_OPWD"])
            {
                data.IsSuccess = false;
                data.Msg = "原密码输入错误";
                return Json(data);
            }

            ur.UL_PWD = form["UL_NPWD"];
            BoFactory.GetSMC_UserListBo.InsertOrUpdate(ur);

            return Json(data);
        }

        public ActionResult SetSqence(string pe_ids, string pe_seqs,string type)
        {
             JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "批量更新成功" };

            string[] ids = pe_ids.Trim(',').Split(',');
            string[] swqs = pe_seqs.Trim(',').Split(',');
            int length = ids.Length;
            int swq = 0;
            foreach (string s in swqs)
            {
                if (!Int32.TryParse(s, out swq))
                {
                    //不是数值类型
                    data.IsSuccess = false;
                    data.Msg = "排序号格式是自然数.";
                    return Json(data);

                }else if (swq <0)
                {
                    //负数不接受
                    data.IsSuccess = false;
                    data.Msg = "排序号格式必须是自然数.";
                    return Json(data);

                }
            }

            //更新unit排序号
            if (type == "unit")
            {
                for (int i = 0; i < length; i++)
                {
                    string unit_id = ids[i];
                    int sequens = Int32.Parse(swqs[i]);
                    var unit = BoFactory.GetSMC_UnitBo.Get(unit_id);
                    unit.Unit_Sequence = sequens;
                    BoFactory.GetSMC_UnitBo.Update(unit);
                }
            }

            //更新user排序号
            if (type == "user")
            {
                for (int i = 0; i < length; i++)
                {
                    int ul_id = Int32.Parse(ids[i]);
                    int sequens = Int32.Parse(swqs[i]);
                    var user = BoFactory.GetSMC_UserListBo.Get(ul_id);
                    user.UL_Sequence = sequens;
                    BoFactory.GetSMC_UserListBo.Update(user);
                }
            }
            
            //更新role排序
            if (type == "role")
            {
                for (int i = 0; i < length; i++)
                {
                    int role_id = Int32.Parse(ids[i]);
                    int sequens = Int32.Parse(swqs[i]);
                    var role = BoFactory.GetSMC_RoleBo.Get(role_id);
                    role.Role_Sequence = sequens;
                    BoFactory.GetSMC_RoleBo.Update(role);
                }
            }

            //更新function排序
            if (type == "function")
            {
                for (int i = 0; i < length; i++)
                {
                    int func_id = Int32.Parse(ids[i]);
                    int sequens = Int32.Parse(swqs[i]);
                    var func = BoFactory.GetSMC_FunctionsBo.Get(func_id);
                    func.FN_Sequence = sequens;
                    BoFactory.GetSMC_FunctionsBo.Update(func);
                }
            }
            
            return Json(data,"text/html");
        }

    }
}
