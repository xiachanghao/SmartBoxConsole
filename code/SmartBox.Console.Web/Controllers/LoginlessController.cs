using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using Beyondbit.BUA.Client;

namespace SmartBox.Console.Web.Controllers
{
    public class LoginlessController : Controller
    {
        //
        // GET: /Loginless/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUnitData()
        {
            IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
            Org orgTop = os.GetTopOrg();
            Org[] orgs = os.QuerySubOrgs(orgTop.OrgCode);
            return Json(orgs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnitUserData(string unitCode)
        {
            //Bo.BoFactory.GetVersionTrackBo.
            IUserService us = Beyondbit.BUA.Client.ServiceFactory.Instance().GetUserService();
            List<User> users = new List<Beyondbit.BUA.Client.User>();
            User[] _users = us.QueryUsersByObjectCode(ObjectType.Org, unitCode);
            return Json(_users, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetUnitUserData2(string unitCode)
        {
            //Bo.BoFactory.GetVersionTrackBo.
            IUserService us = Beyondbit.BUA.Client.ServiceFactory.Instance().GetUserService();
            IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
            Org orgUnit = os.GetOrgBaseInfo(ObjectType.Org, unitCode);
            Hashtable resultTable = new Hashtable();
            resultTable["id"] = "o_" + unitCode;
            resultTable["text"] = orgUnit.OrgName;

            List<Hashtable> lst = new List<Hashtable>();
            resultTable["children"] = lst; 

            List<User> users = new List<Beyondbit.BUA.Client.User>();
            User[] _users = null;
            try
            {
                _users = us.QueryUsersByObjectCode(ObjectType.Org, unitCode);
            }
            catch
            {
            }
            
            if (_users != null && _users.Length > 0)
            {
                foreach (Beyondbit.BUA.Client.User u in _users)
                {
                    Hashtable hUser = new Hashtable();
                    hUser["id"] = "u_" + u.UserUid;
                    hUser["text"] = u.UserName;
                    lst.Add(hUser);
                }
            }

            Org[] childOrgs = os.QuerySubOrgs(unitCode);
            if (childOrgs != null && childOrgs.Length > 0)
            {
                foreach (Org org in childOrgs)
                {
                    Hashtable hOrg = new Hashtable();
                    hOrg["id"] = "o_" + org.OrgCode;
                    hOrg["text"] = org.OrgName;
                    hOrg["children"] = new List<Hashtable>();
                    lst.Add(hOrg);
                    parseUnitUserData(hOrg, org, us, os);
                }
            }

            Hashtable[] arr = new Hashtable[1];
            arr[0] = resultTable;

            //List<User> users = new List<Beyondbit.BUA.Client.User>();
            //User[] _users = us.QueryUsersByObjectCode(ObjectType.Org, unitCode);
            return Json(arr, JsonRequestBehavior.AllowGet);
        }

        private void parseUnitUserData(Hashtable parent, Org org, IUserService us, IOrgService os)
        {
            //Hashtable hashOrg = new Hashtable();
            //parent["id"] = "o_" + org.OrgCode;
            //parent["text"] = org.OrgName;
            List<Hashtable> lst = parent["children"] as List<Hashtable>;

            //Hashtable hsOrg = new Hashtable();
            //hsOrg["id"] = "o_" + org.OrgCode;
            //hsOrg["text"] = org.OrgName;
            //hsOrg["children"] = new List<Hashtable>();
            //lst.Add(hsOrg);

            List<User> users = new List<Beyondbit.BUA.Client.User>();
            User[] _users = null;
            try
            {
                _users = us.QueryUsersByObjectCode(ObjectType.Org, org.OrgCode);
            }
            catch
            {
            }
            
            if (_users != null && _users.Length > 0)
            {
                foreach (Beyondbit.BUA.Client.User u in _users)
                {
                    Hashtable hUser = new Hashtable();
                    hUser["id"] = "u_" + u.UserUid;
                    hUser["text"] = u.UserName;
                    lst.Add(hUser);
                }
            }
            
            Org[] childOrgs = os.QuerySubOrgs(org.OrgCode);
            if (childOrgs != null && childOrgs.Length > 0)
            {
                foreach (Org o in childOrgs)
                {
                    Hashtable hOrg = new Hashtable();
                    hOrg["id"] = "o_" + o.OrgCode;
                    hOrg["text"] = o.OrgName;
                    hOrg["children"] = new List<Hashtable>();
                    lst.Add(hOrg);
                    parseUnitUserData(hOrg, o, us, os);
                }
            }
        }

        public ActionResult UnitManagerAdd()
        {
            IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
            Org orgTop = os.GetTopOrg();
            Org[] orgs = os.QuerySubOrgs(orgTop.OrgCode);
            ViewData["entity"] = Newtonsoft.Json.JsonConvert.SerializeObject(orgs);
            return View();
        }
        public ActionResult UnitAdd()
        {
            return View();
        }
        public JsonResult AddUnitManager(string UID, string UName, string UnitCode, string UnitName, string Password, string EMail, string Phone, bool IsFromBUAUser)
        {
            Hashtable r = new Hashtable();
            r["r"] = true;
            r["d"] = "新增管理员成功！";

            if (String.IsNullOrEmpty(UnitCode))
            {
                r["r"] = false;
                r["d"] = "未选择单位！";
                return Json(r);
            }

            if (!IsFromBUAUser && String.IsNullOrEmpty(EMail))
            {
                r["r"] = false;
                r["d"] = "请填写EMail！";
                return Json(r);
            }

            if (!IsFromBUAUser && System.Text.RegularExpressions.Regex.IsMatch(EMail, "^[^@]+@[^@]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase) == false)
            {
                r["r"] = false;
                r["d"] = "请正确填写EMail！";
                return Json(r);
            }

            if (!IsFromBUAUser && String.IsNullOrEmpty(Password))
            {
                r["r"] = false;
                r["d"] = "请填写密码！";
                return Json(r);
            }

            if (!IsFromBUAUser && String.IsNullOrEmpty(Phone))
            {
                r["r"] = false;
                r["d"] = "请填写电话号码！";
                return Json(r);
            }

            if (!IsFromBUAUser && System.Text.RegularExpressions.Regex.IsMatch(Phone, "^[0-9]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase) == false)
            {
                r["r"] = false;
                r["d"] = "请正确填写电话号码！";
                return Json(r);
            }

            SmartBox.Console.Common.Entities.SMC_UserList unitManager = new Common.Entities.SMC_UserList();
            unitManager.Unit_ID = UnitCode;
            unitManager.Unit_Name = UnitName;
            unitManager.UL_Name = UName;
            unitManager.UL_UID = UID;
            unitManager.UL_PWD = Password;
            unitManager.UL_MailAddress = EMail;
            unitManager.UL_MobilePhone = Phone;

            try
            {
                IUserService us = Beyondbit.BUA.Client.ServiceFactory.Instance().GetUserService();
                IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
                Beyondbit.BUA.Client.User usr = null;

                bool exists = Bo.BoFactory.GetSMC_UserListBo.ExistsByUID(UID);

                try
                {
                    usr = us.GetUserInfo(unitManager.UL_UID);
                }
                catch
                {
                }

                //Beyondbit.BUA.Client.User[] _users = us.QueryUsersByObjectCode(ObjectType.User, userList.UL_UID);
                if (usr == null)
                {
                    //新增用户到统一授权
                    Beyondbit.BUA.Client.User u = new Beyondbit.BUA.Client.User();
                    u.UserUid = unitManager.UL_UID;
                    u.UserName = unitManager.UL_Name;
                    u.UserFullName = unitManager.UL_Name;
                    u.UserEmail = unitManager.UL_MailAddress == null ? "" : unitManager.UL_MailAddress;
                    u.UserPersonal = new UserPersonal();
                    u.UserPersonal.UserMobilePhone = unitManager.UL_MobilePhone;
                    u.UnitCode = unitManager.Unit_ID;
                    u.OrgCode = unitManager.Unit_ID;
                    u.UserPassword = unitManager.UL_PWD;
                    us.AddUser(u);
                }
                else
                {
                    //bua里已有用户
                    if (IsFromBUAUser)
                    {
                        unitManager.Unit_ID = UnitCode;
                        unitManager.Unit_Name = UnitName;
                        unitManager.UL_Name = usr.UserName;
                        unitManager.UL_UID = UID;
                        unitManager.UL_PWD = usr.UserPassword;
                        unitManager.UL_MailAddress = usr.UserEmail;
                        if (usr.UserPersonal != null && usr.UserPersonal.UserMobilePhone != null)
                            unitManager.UL_MobilePhone = usr.UserPersonal.UserMobilePhone;
                        else
                            unitManager.UL_MobilePhone = "";

                        if (exists)
                        {
                            r["r"] = false;
                            r["d"] = "用户已经是单位管理员！";
                            return Json(r);
                        }
                        else
                        {
                            Bo.BoFactory.GetSMC_UserListBo.InsertOrUpdate(unitManager);
                        }
                    }
                    else
                    {
                        if (exists)
                        {
                            r["r"] = false;
                            r["d"] = "用户已经是单位管理员！";
                            return Json(r);
                        }
                        else
                        {
                            Bo.BoFactory.GetSMC_UserListBo.InsertOrUpdate(unitManager);
                        }
                        return Json(r);
                    }
                }


            }
            catch (Exception ex)
            {
                r["r"] = false;
                r["d"] = ex.Message;
                return Json(r);
            }

            return Json(r);
        }

        public JsonResult AddUnit(string unitCode, string unit)
        {
            IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
            Hashtable r = new Hashtable();
            try
            {
                Org orgExists = os.GetOrgBaseInfo(ObjectType.Org, unitCode);
            }
            catch (ClientException ex)
            {
                if (ex.ErrCode == "9001")
                {
                    //unitCodeExists = false;
                    //msg = ex.Message;
                }
            }
            //Org org = new Org();
            //org.OrgCode = unitCode;
            //org.OrgName = unit;
            //org.ParentOrgCode = os.GetTopOrg().OrgCode;
            try
            {
                //os.AddOrg(org);
            }
            catch (ClientException e)
            {
                //1002,1003
                r["r"] = false;
                r["d"] = e.Message;
                return Json(r);
            }

            SmartBox.Console.Common.Entities.SMC_Unit Unit = new Common.Entities.SMC_Unit();
            Unit.Unit_ID = unitCode;
            Unit.Unit_Name = unit;
            Bo.BoFactory.GetSMC_UnitBo.InsertOrUpdate(Unit);

            
            r["r"] = true;
            r["d"] = "新增单位成功！";
            return Json(r);
        }

        public JsonResult PreCalcUnitCode(string unitName)
        {
            Hashtable r = new Hashtable();
            string py = NPinyin.Pinyin.GetInitials(unitName, System.Text.Encoding.UTF8);
            IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
            bool unitCodeExists = true;
            string msg = "组织机构代码已存在";
            try
            {
                Org orgExists = os.GetOrgBaseInfo(ObjectType.Org, py);
            }
            catch (ClientException ex)
            {
                if (ex.ErrCode == "9001")
                {
                    unitCodeExists = false;
                    msg = ex.Message;
                }
            }

            if (unitCodeExists)
            {
                r["r"] = false;
                r["unitcode"] = "";
                r["d"] = msg;
                return Json(r);
            }

            if (String.IsNullOrEmpty(unitName))
            {
                r["r"] = false;
                r["unitcode"] = "";
                r["d"] = "计算单位代码失败！";
            }
            else
            {
                r["r"] = true;
                r["unitcode"] = py;
                r["d"] = "计算单位代码成功！";
            }
            return Json(r);
        }
    }
}
