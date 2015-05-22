using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using SmartBox.Console.Bo.AppCenter;
using System.Web.Script.Serialization;
using System.Configuration;
using SmartBox.Console.Dao;
using SmartBox.Console.Common.Entities;
using System.Data;
using Beyondbit.Framework.Core.Proxy;

namespace ApplicationCenter.Controllers
{
    public class HomeController : Controller
    {
        SmartBox.Console.Bo.AppCenter.AppCenterBO bo = ProxyFactory.CreateProxy().CreateObject<SmartBox.Console.Bo.AppCenter.AppCenterBO>();

        public ActionResult MList()
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("MLogin", "Home");
            }
            bo.SetController(this);

            if (Session["__UserName__"] != null)
            {
                ViewBag.userName = Session["__UserName__"];
            }
            else
            {
                ViewBag.userName = "游客";
            }


            string unitCode = "";

            if (HttpContext.User.Identity.IsAuthenticated)
                unitCode = bo.GetUserUnitCode(HttpContext.User.Identity.Name);
            return View();
        }

        public ActionResult ML()
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("MLogin", "Home");
            }
            bo.SetController(this);

            if (Session["__UserName__"] != null)
            {
                ViewBag.userName = Session["__UserName__"];
            }
            else
            {
                ViewBag.userName = "游客";
            }


            string unitCode = "";

            if (HttpContext.User.Identity.IsAuthenticated)
                unitCode = bo.GetUserUnitCode(HttpContext.User.Identity.Name);
            return View();
        }

        public ActionResult ML3()
        {
            return View();
        }

        public ActionResult ML2()
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("MLogin", "Home");
            }
            bo.SetController(this);

            if (Session["__UserName__"] != null)
            {
                ViewBag.userName = Session["__UserName__"];
            }
            else
            {
                ViewBag.userName = "游客";
            }


            string unitCode = "";

            if (HttpContext.User.Identity.IsAuthenticated)
                unitCode = bo.GetUserUnitCode(HttpContext.User.Identity.Name);

            List<Hashtable> categoryData = bo.GetApplicationCategoryList(-1);
            string cateStr = Newtonsoft.Json.JsonConvert.SerializeObject(categoryData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.clsData = "{items:" + cateStr + "}";

            List<Hashtable> unitData = bo.GetUnitList(-1);
            string unitStr = Newtonsoft.Json.JsonConvert.SerializeObject(unitData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.unitData = "{items:" + unitStr + "}";

            return View();
        }

        public ActionResult MLLungo()
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("MLogin", "Home");
            }
            bo.SetController(this);

            if (Session["__UserName__"] != null)
            {
                ViewBag.userName = Session["__UserName__"];
            }
            else
            {
                ViewBag.userName = "游客";
            }


            string unitCode = "";

            if (HttpContext.User.Identity.IsAuthenticated)
                unitCode = bo.GetUserUnitCode(HttpContext.User.Identity.Name);

            List<Hashtable> categoryData = bo.GetApplicationCategoryList(-1);
            string cateStr = Newtonsoft.Json.JsonConvert.SerializeObject(categoryData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.clsData = "{items:" + cateStr + "}";

            List<Hashtable> unitData = bo.GetUnitList(-1);
            string unitStr = Newtonsoft.Json.JsonConvert.SerializeObject(unitData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.unitData = "{items:" + unitStr + "}";

            return View();
        }

        public ActionResult n()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GridList(string clientType)
        {
            string uid = "";
            if (HttpContext.User.Identity.IsAuthenticated)
                uid = HttpContext.User.Identity.Name;

            bo.SetController(this);
            SmartBox.Console.Common.Entities.Enum.PackageTrait trait = SmartBox.Console.Common.Entities.Enum.PackageTrait.All;
            SmartBox.Console.Common.Entities.Enum.PackageClientType _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.All;
            switch (clientType) {
                case "首页":
                    
                    break;
                case "Android手机":
                    _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.androidPhone;
                    break;
                case "Android平板":
                    _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.androidPad;
                    break;
                case "iPhone":
                    _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.iPhone;
                    break;
                case "iPad":
                    _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.iPad;
                    break;
                case "轻应用":
                    _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.web;
                    break;
            }
            DataSet ds = null;
            List<Hashtable> pics = new List<Hashtable>();
            string _unitCode = "";
            int page = 1, _pageSize = 0, pageCount = 0, recordCount = 0;
            ds = bo.SearchPackageList("", "", _unitCode, _clientType, trait, page, _pageSize, out pageCount, out recordCount, uid, 0);
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
            {
                Hashtable obj = new Hashtable();
                DataRow dr = ds.Tables[0].Rows[i];
                obj["appName"] = dr.IsNull("pe_displayname") ? "" : ds.Tables[0].Rows[i]["pe_displayname"].ToString();
                obj["appVersion"] = dr.IsNull("pe_version") ? "" : ds.Tables[0].Rows[i]["pe_version"].ToString();
                obj["appCate"] = dr.IsNull("pe_Category") ? "" : ds.Tables[0].Rows[i]["pe_Category"].ToString();
                obj["downloadUrl"] = dr.IsNull("pe_downloaduri") ? "" : ds.Tables[0].Rows[i]["pe_downloaduri"].ToString();
                obj["unit"] = dr.IsNull("pe_unitname") ? "" : ds.Tables[0].Rows[i]["pe_unitname"].ToString();
                obj["desc"] = dr.IsNull("pe_Description") ? "" : ds.Tables[0].Rows[i]["pe_Description"].ToString();
                obj["firmware"] = dr.IsNull("pe_Firmware") ? "" : ds.Tables[0].Rows[i]["pe_Firmware"].ToString();
                obj["appDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                obj["id"] = ds.Tables[0].Rows[i]["pe_id"].ToString();
                obj["manualuri"] = ds.Tables[0].Rows[i]["pe_id"].ToString();
                List<Hashtable> pictures = bo.GetAppPicture(Convert.ToInt32(obj["id"]));
                if (pictures != null && pictures.Count > 0)
                {
                    obj["picture"] = Url.Content(pictures[0]["picture"].ToString());
                }
                else
                    obj["picture"] = Url.Content("~/AppPictures/pictureno.png");
                pics.Add(obj);
            }
            return Json(pics);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetMobileListItems(int page, string clientType, string cateCode, int pageSize)
        {
            string uid = "";
            if (HttpContext.User.Identity.IsAuthenticated)
                uid = HttpContext.User.Identity.Name;

            bo.SetController(this);
            List<Hashtable> pics = new List<Hashtable>();
            string _unitCode = "";
            int _pageSize = pageSize, pageCount = 0, recordCount = 0;
            SmartBox.Console.Common.Entities.Enum.PackageTrait trait = SmartBox.Console.Common.Entities.Enum.PackageTrait.All;
            SmartBox.Console.Common.Entities.Enum.PackageClientType _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.All;
            DataSet ds = null;
            Hashtable result = new Hashtable();

            if (clientType != null)
            //{
            //    ds = bo.GetNewestPackageList("");
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
            //    {
            //        Hashtable obj = new Hashtable();
            //        DataRow dr = ds.Tables[0].Rows[i];
            //        obj["appName"] = dr.IsNull("pe_displayname") ? "" : ds.Tables[0].Rows[i]["displayname"].ToString();
            //        obj["appVersion"] = dr.IsNull("version") ? "" : ds.Tables[0].Rows[i]["version"].ToString();
            //        obj["appCate"] = dr.IsNull("pe_Category") ? "" : ds.Tables[0].Rows[i]["pe_Category"].ToString();
            //        obj["downloadUrl"] = dr.IsNull("downloaduri") ? "" : ds.Tables[0].Rows[i]["downloaduri"].ToString();
            //        obj["unit"] = dr.IsNull("pe_unitname") ? "" : ds.Tables[0].Rows[i]["pe_unitname"].ToString();
            //        obj["pictureurl"] = dr.IsNull("pe_pictureurl") ? "" : Url.Content(ds.Tables[0].Rows[i]["pe_pictureurl"].ToString());
            //        obj["appDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["pe_CreatedTime"]).ToString("yyyy-MM-dd");
            //        obj["pe_clienttype"] = dr.IsNull("pe_clienttype") ? "" : ds.Tables[0].Rows[i]["pe_clienttype"].ToString();
            //        obj["id"] = ds.Tables[0].Rows[i]["id"].ToString();

            //        pics.Add(obj);
            //    }
            //    result["record_count"] = ds.Tables[0].Rows.Count;
            //    result["page_count"] = 1;
            //    result["items"] = pics;
            //}
            //else
            {
                switch (clientType)
                {
                    case "":
                        _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.All;
                        break;
                    case "tj":
                        trait = SmartBox.Console.Common.Entities.Enum.PackageTrait.Tuijian;
                        break;
                    case "zx":
                        trait = SmartBox.Console.Common.Entities.Enum.PackageTrait.ZX;
                        break;
                    case "bb":
                        trait = SmartBox.Console.Common.Entities.Enum.PackageTrait.Bibei;
                        break;
                    case "sc":
                        trait = SmartBox.Console.Common.Entities.Enum.PackageTrait.Sc;
                        break;
                    case "qyy":
                        _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.web;
                        break;
                    case "fl":                        
                        break;
                    default:
                        _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.All;
                        break;
                }

                ds = bo.SearchPackageList("", cateCode, _unitCode, _clientType, trait, page, _pageSize, out pageCount, out recordCount, uid, 0);
                for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                {
                    Hashtable obj = new Hashtable();
                    DataRow dr = ds.Tables[0].Rows[i];
                    obj["appName"] = dr.IsNull("pe_displayname") ? "" : ds.Tables[0].Rows[i]["pe_displayname"].ToString();
                    obj["appVersion"] = dr.IsNull("pe_version") ? "" : ds.Tables[0].Rows[i]["pe_version"].ToString();
                    obj["appCate"] = dr.IsNull("pe_Category") ? "" : ds.Tables[0].Rows[i]["pe_Category"].ToString();
                    obj["downloadUrl"] = dr.IsNull("pe_downloaduri") ? "" : ds.Tables[0].Rows[i]["pe_downloaduri"].ToString();
                    obj["unit"] = dr.IsNull("pe_unitname") ? "" : ds.Tables[0].Rows[i]["pe_unitname"].ToString();
                    obj["appDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    obj["pe_clienttype"] = dr.IsNull("pe_clienttype") ? "" : ds.Tables[0].Rows[i]["pe_clienttype"].ToString();
                    obj["pictureurl"] = dr.IsNull("pe_pictureurl") ? "" : Url.Content(ds.Tables[0].Rows[i]["pe_pictureurl"].ToString());
                    obj["id"] = ds.Tables[0].Rows[i]["pe_id"].ToString();

                    pics.Add(obj);
                }
                result["record_count"] = recordCount;
                result["page_count"] = pageCount;
                result["items"] = pics;
            }
            //var picStr = Newtonsoft.Json.JsonConvert.SerializeObject(pics, Newtonsoft.Json.Formatting.Indented);
            return Json(result);
        }

        public ActionResult GridList()
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("MLogin", "Home");
            }
            bo.SetController(this);

            if (Session["__UserName__"] != null)
            {
                ViewBag.userName = Session["__UserName__"];
            }
            else
            {
                ViewBag.userName = "游客";
            }


            string unitCode = "";

            if (HttpContext.User.Identity.IsAuthenticated)
                unitCode = bo.GetUserUnitCode(HttpContext.User.Identity.Name);

            bo.SetController(this);
            SmartBox.Console.Common.Entities.Enum.PackageClientType _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.web;            
            IList<Hashtable> r = bo.GetAllPackageList(_clientType);            
            var webStr = Newtonsoft.Json.JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented);
            ViewBag.webData = "{items:" + webStr + ",pageIndex:1}";

            _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.androidPad;
            IList<Hashtable> randroidPad = bo.GetAllPackageList(_clientType);
            var androidPadStr = Newtonsoft.Json.JsonConvert.SerializeObject(randroidPad, Newtonsoft.Json.Formatting.Indented);
            ViewBag.androidPadData = "{items:" + androidPadStr + ",pageIndex:1}";
            
            _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.androidPhone;
            IList<Hashtable> randroidPhone = bo.GetAllPackageList(_clientType);
            var androidPhoneStr = Newtonsoft.Json.JsonConvert.SerializeObject(randroidPhone, Newtonsoft.Json.Formatting.Indented);
            ViewBag.androidPhoneData = "{items:" + androidPhoneStr + ",pageIndex:1}";
            
            _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.iPad;
            IList<Hashtable> riPad = bo.GetAllPackageList(_clientType);
            var iPadStr = Newtonsoft.Json.JsonConvert.SerializeObject(riPad, Newtonsoft.Json.Formatting.Indented);
            ViewBag.iPadData = "{items:" + iPadStr + ",pageIndex:1}";
            
            _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.iPhone;
            IList<Hashtable> riPhone = bo.GetAllPackageList(_clientType);
            var iPhoneStr = Newtonsoft.Json.JsonConvert.SerializeObject(riPhone, Newtonsoft.Json.Formatting.Indented);
            ViewBag.iPhoneData = "{items:" + iPhoneStr + ",pageIndex:1}";
            
            _clientType = SmartBox.Console.Common.Entities.Enum.PackageClientType.All;
            IList<Hashtable> rAll = bo.GetAllPackageList(_clientType);
            var AllStr = Newtonsoft.Json.JsonConvert.SerializeObject(rAll, Newtonsoft.Json.Formatting.Indented);
            ViewBag.AllData = "{items:" + AllStr + ",pageIndex:1}";
            return View();
        }

        public ActionResult MDetail(int pe_id)
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("MLogin", "Home");
            }
            bo.SetController(this);

            if (Session["__UserName__"] != null)
            {
                ViewBag.userName = Session["__UserName__"];
            }
            else
            {
                ViewBag.userName = "游客";
            }


            string unitCode = "";

            if (HttpContext.User.Identity.IsAuthenticated)
                unitCode = bo.GetUserUnitCode(HttpContext.User.Identity.Name);


            bo.SetController(this);

            bool collected = bo.IsCollected(pe_id, User.Identity.Name);
            ViewBag.Collected = collected;

            List<Hashtable> pics = bo.GetAppPicture(pe_id);
            var picStr = Newtonsoft.Json.JsonConvert.SerializeObject(pics, Newtonsoft.Json.Formatting.Indented);
            ViewBag.picData = "{items:" + picStr + ",pageIndex:1}";

            Hashtable _package = bo.GetPackage(pe_id);
            ViewBag.package = _package;
            return View();
        }

        public ActionResult MD(int pe_id)
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("MLogin", "Home");
            }
            bo.SetController(this);

            if (Session["__UserName__"] != null)
            {
                ViewBag.userName = Session["__UserName__"];
            }
            else
            {
                ViewBag.userName = "游客";
            }


            string unitCode = "";

            if (HttpContext.User.Identity.IsAuthenticated)
                unitCode = bo.GetUserUnitCode(HttpContext.User.Identity.Name);


            bo.SetController(this);

            bool collected = bo.IsCollected(pe_id, User.Identity.Name);
            ViewBag.Collected = collected;

            ViewBag.manuals = bo.GetPackageManuals(pe_id);

            List<Hashtable> pics = bo.GetAppPicture(pe_id);
            var picStr = Newtonsoft.Json.JsonConvert.SerializeObject(pics, Newtonsoft.Json.Formatting.Indented);
            ViewBag.picData = "{items:" + picStr + ",pageIndex:1}";

            Hashtable _package = bo.GetPackage(pe_id);
            ViewBag.package = _package;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Login()
        {

            if (Request.UserAgent.ToLower().IndexOf("mobile") != -1)
                return RedirectToAction("MLogin", "Home");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MLogin()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["__UserName__"] = "";
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult MLogout()
        {
            Session["__UserName__"] = "";
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("MLogin", "Home");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MLogin(string userTp, string tbName, string tbPassword)
        {
            if (!String.IsNullOrEmpty(userTp) && userTp == "guestUser")
            {
                tbName = "guest";
                string AuthSaveKey = ConfigurationManager.AppSettings["AuthSaveKey"];
                string UserSessionKey = System.Configuration.ConfigurationManager.AppSettings["UserSessionKey"];
                System.Web.HttpContext.Current.Session.Add(AuthSaveKey, tbName);
                this.ControllerContext.RequestContext.HttpContext.Session.Add(UserSessionKey, new { UID = tbName });
                System.Web.Security.FormsAuthentication.SetAuthCookie(tbName, true);
                Session["__UserName__"] = "游客";
                return RedirectToAction("MLlungo", "Home");
            }
            else if (!String.IsNullOrEmpty(userTp) && userTp == "authUser")
            {
                bo.SetController(this);

                SMC_User usr = bo.GetUser(tbName);
                if (usr == null)
                {
                    return RedirectToAction("MLogin", "Home");
                }
                else
                {
                    string AuthSaveKey = ConfigurationManager.AppSettings["AuthSaveKey"];
                    string UserSessionKey = System.Configuration.ConfigurationManager.AppSettings["UserSessionKey"];
                    System.Web.HttpContext.Current.Session.Add(AuthSaveKey, usr);
                    Session["__UserName__"] = usr.U_NAME;
                    this.ControllerContext.RequestContext.HttpContext.Session.Add(UserSessionKey, new { UID = tbName });
                    System.Web.Security.FormsAuthentication.SetAuthCookie(tbName, true);
                    return RedirectToAction("MLlungo", "Home");
                }
            }
            else
            {
                return RedirectToAction("MLogin", "Home");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string userTp, string tbName, string tbPassword)
        {
            if (!String.IsNullOrEmpty(userTp) && userTp == "guestUser")
            {
                tbName = "guest";
                string AuthSaveKey = ConfigurationManager.AppSettings["AuthSaveKey"];
                string UserSessionKey = System.Configuration.ConfigurationManager.AppSettings["UserSessionKey"];
                System.Web.HttpContext.Current.Session.Add(AuthSaveKey, tbName);
                this.ControllerContext.RequestContext.HttpContext.Session.Add(UserSessionKey, new { UID = tbName });
                System.Web.Security.FormsAuthentication.SetAuthCookie(tbName, true);
                Session["__UserName__"] = "游客";
                return RedirectToAction("Index", "Home");
            }
            else if (!String.IsNullOrEmpty(userTp) && userTp == "authUser")
            {
                bo.SetController(this);

                SMC_User usr = bo.GetUser(tbName);
                if (usr == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    string AuthSaveKey = ConfigurationManager.AppSettings["AuthSaveKey"];
                    string UserSessionKey = System.Configuration.ConfigurationManager.AppSettings["UserSessionKey"];
                    System.Web.HttpContext.Current.Session.Add(AuthSaveKey, usr);
                    Session["__UserName__"] = usr.U_NAME;
                    this.ControllerContext.RequestContext.HttpContext.Session.Add(UserSessionKey, new { UID = tbName });
                    System.Web.Security.FormsAuthentication.SetAuthCookie(tbName, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Detail()
        {
            bo.SetController(this);

            if (Session["__UserName__"] != null)
            {
                ViewBag.userName = Session["__UserName__"];
            }
            else
            {
                ViewBag.userName = "游客";
            }

            int packageid = 0;
            if (Request.QueryString["packageid"] != null) {
                packageid = Convert.ToInt32(Request.QueryString["packageid"]);
            }
            Hashtable entity = bo.GetPackage(packageid);
            ViewBag.entityObj = entity;

            ViewBag.manuals = bo.GetPackageManuals(packageid);

            bool collected = bo.IsCollected(packageid, User.Identity.Name);
            ViewBag.Collected = collected;

            int _pageCount, _recordCount;
            List<Hashtable> objs = new List<Hashtable>();
            if (entity["displayname"] != null)
            {
                DataSet dss = bo.SearchPackageList(entity["displayname"].ToString(), "", "", SmartBox.Console.Common.Entities.Enum.PackageClientType.All, SmartBox.Console.Common.Entities.Enum.PackageTrait.All, 1, 5, out _pageCount, out _recordCount, "", 0);
                if (dss != null && dss.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dss.Tables[0].Rows)
                    {
                        string pe_id = dr["pe_id"].ToString();
                        if (pe_id == packageid.ToString())
                            continue;
                        Hashtable obj = new Hashtable();
                        obj["ItemName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                        obj["ItemVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                        obj["Desc"] = dr.IsNull("pe_description") ? "" : dr["pe_description"].ToString();
                        obj["Class"] = dr.IsNull("pe_category") ? "" : dr["pe_category"].ToString();
                        obj["Unit"] = dr.IsNull("pe_unitname") ? "" : dr["pe_unitname"].ToString();
                        obj["ItemDate"] = dr.IsNull("pe_createdtime") ? "" : dr["pe_createdtime"].ToString();
                        obj["os"] = dr.IsNull("pe_firmware") ? "" : dr["pe_firmware"].ToString();
                        obj["DownloadCount"] = dr.IsNull("pe_downcount") ? "" : dr["pe_downcount"].ToString();
                        obj["downloadurl"] = dr.IsNull("pe_downloaduri") ? "" : dr["pe_downloaduri"].ToString();
                        obj["url"] = Url.Content("~/Home/Detail?packageid=" + pe_id);
                        obj["HelpUrl"] = @Url.Content("Home/Detail?packageid=") + pe_id + "#manual";
                        objs.Add(obj);
                    }
                }
            }

            
            Hashtable source = new Hashtable();
            source["listItems"] = objs;
            source["pageCount"] = 4;
            ViewBag.source = source;

            List<Hashtable> pics = bo.GetAppPicture(packageid);
            var picStr = Newtonsoft.Json.JsonConvert.SerializeObject(pics, Newtonsoft.Json.Formatting.Indented);
            ViewBag.picData = "{items:" + picStr + ",pageIndex:1}";
            
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PostQuestion(string question, int pe_id)
        {
            bo.SetController(this);
            string uid = "";
            string uname = "";
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Name != "guest")
                {
                    uid = User.Identity.Name;
                    SMC_User u = bo.GetUser(uid);
                    uname = u.U_NAME;
                }
                else
                {
                    uid = User.Identity.Name;
                    uname = "游客";
                }
            }
            
            bool r = bo.PostQuestion(question, pe_id, uid, uname);
            return Json(new { d = r});
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateDownCount(int pe_id)
        {
            bo.SetController(this);
            bool r = bo.UpdateDownCount(pe_id);
            string desc = "更新安装包的下载数量成功！";
            if (!r)
            {
                desc = "更新安装包的下载数量失败！";
            }
            return Json(new { d = desc, r });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SCPost(int pe_id)
        {
            bo.SetController(this);
            string uid = "";
            string uname = "";
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Name != "guest")
                {
                    uid = User.Identity.Name;
                    SMC_User u = bo.GetUser(uid);
                    uname = u.U_NAME;
                }
                else
                {
                    uid = User.Identity.Name;
                    uname = "游客";
                }
            }

            string desc = "";
            bool r = false;
            if (uname != "游客")
            {
                int s = bo.SCPost(pe_id, uid, uname);
                switch (s)
                {
                    case 10:
                        desc = "收藏失败！";
                        break;
                    case 11:
                        desc = "收藏成功！";
                        r = true;
                        break;
                    case 20:
                        desc = "取消收藏失败！";
                        break;
                    case 21:
                        desc = "取消收藏成功！";
                        r = true;
                        break;
                }
            }
            else
            {
                desc = "请先登录再收藏！";
            }
            
            return Json(new { d = desc, r });
        }

        public ActionResult List()
        {
            bo.SetController(this);
            if (Session["__UserName__"] != null)
            {
                ViewBag.userName = Session["__UserName__"];
            }
            else
            {
                ViewBag.userName = "游客";
            }
            string uid = "";
            
            string unitCode = "";

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.User.Identity.Name.ToLower() == "guest")
                    unitCode = "";
                else
                    unitCode = bo.GetUserUnitCode(HttpContext.User.Identity.Name);
                uid = HttpContext.User.Identity.Name;
            }
            if (uid == "guest")
                uid = "";
            string keyword = this.ControllerContext.RequestContext.HttpContext.Request.QueryString["keyword"];
            string _unitcode = this.ControllerContext.RequestContext.HttpContext.Request.QueryString["unitcode"];
            unitCode = _unitcode;
            string _page = this.ControllerContext.RequestContext.HttpContext.Request.QueryString["page"];
            int page = 1;
            if (!String.IsNullOrEmpty(_page)) {
                page = int.Parse(_page);
            }

            SmartBox.Console.Common.Entities.Enum.PackageClientType pct = SmartBox.Console.Common.Entities.Enum.PackageClientType.All;
            string clientType = this.ControllerContext.RequestContext.HttpContext.Request.QueryString["cat"];
            string category = this.ControllerContext.RequestContext.HttpContext.Request.QueryString["cat3"];
            string cate2 = this.ControllerContext.RequestContext.HttpContext.Request.QueryString["cat2"];
            SmartBox.Console.Common.Entities.Enum.PackageTrait trait = SmartBox.Console.Common.Entities.Enum.PackageTrait.All;
            DataSet ds = null;
            if (!String.IsNullOrEmpty(cate2)) {
                switch (cate2) {
                    case "bb":
                        trait = SmartBox.Console.Common.Entities.Enum.PackageTrait.Bibei;
                        break;
                    case "tj":
                        trait = SmartBox.Console.Common.Entities.Enum.PackageTrait.Tuijian;
                        break;
                    case "sc":
                        //ds = bo.GetCollectedPackageList("");
                        trait = SmartBox.Console.Common.Entities.Enum.PackageTrait.Sc;
                        break;
                }
            }

            if (!String.IsNullOrEmpty(clientType)) {
                switch (clientType) {
                    case "adphone":
                        pct = SmartBox.Console.Common.Entities.Enum.PackageClientType.androidPhone;
                        break;
                    case "adpad":
                        pct = SmartBox.Console.Common.Entities.Enum.PackageClientType.androidPad;
                        break;
                    case "iphone":
                        pct = SmartBox.Console.Common.Entities.Enum.PackageClientType.iPhone;
                        break;
                    case "ipad":
                        pct = SmartBox.Console.Common.Entities.Enum.PackageClientType.iPad;
                        break;
                    case "web":
                        pct = SmartBox.Console.Common.Entities.Enum.PackageClientType.web;
                        break;
                }
            }
            int pageSize = 6;
            int pageCount = 0;
            int recordCount = 0;
            ds = bo.SearchPackageList(keyword, category, unitCode, pct, trait, page, pageSize, out pageCount, out recordCount, uid, 0);

            List<Hashtable> objs = new List<Hashtable>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["pe_id"] = dr.IsNull("pe_id") ? "" : dr["pe_id"].ToString();
                    hash["ItemName"] = dr.IsNull("pe_Name") ? "" : dr["pe_Name"].ToString();// "PPS影音";
                    hash["ItemVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();// "android手机版";
                    hash["Class"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();//"办公/政务";
                    hash["Unit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();// "上海互联网软件";
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["ItemDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["ItemDate"] = "";
                    hash["os"] = dr.IsNull("pe_Firmware") ? "" : dr["pe_Firmware"].ToString();
                    hash["DownloadCount"] = dr.IsNull("pe_DownCount") ? "" : dr["pe_DownCount"].ToString();
                    string pe_PictureUrl = dr.IsNull("pe_PictureUrl") ? "" : dr["pe_PictureUrl"].ToString();
                    if (String.IsNullOrEmpty(pe_PictureUrl))
                    {
                        hash["appImage"] = Url.Content("~/AppIcons/No.png");
                    }
                    else
                    {
                        hash["appImage"] = Url.Content(pe_PictureUrl);
                    }

                    string downUrl = dr.IsNull("pe_downloaduri") ? "" : dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString();
                    hash["HelpUrl"] = "";
                    if (String.IsNullOrEmpty(downUrl))
                        hash["downUrl"] = "";
                    else
                        hash["downUrl"] = Url.Content(downUrl); //"~/Home/Detail?packageid=5"
                    hash["pe_ClientType"] = dr.IsNull("pe_ClientType") ? "" : dr["pe_ClientType"].ToString();
                    objs.Add(hash);
                }
            }

            Hashtable source = new Hashtable();
            //for (var i = 0; i < 6; ++i)
            //{
            //    Hashtable obj = new Hashtable();
            //    obj["ItemName"] = "酷我音乐";
            //    obj["ItemVersion"] = "iPhone版";
            //    obj["Desc"] = "管理个人通讯录管理个人通讯录";
            //    obj["Class"] = "办公政务";
            //    obj["Unit"] = "浦东新区环保局";
            //    obj["ItemDate"] = "2014-12-12";
            //    obj["os"] = "ios7.1以上";
            //    obj["DownloadCount"] = "32323";
            //    obj["url"] = Url.Content("~/Home/Detail?packageid=5"); 
            //    obj["HelpUrl"] = "#";
            //    objs.Add(obj);
            //}
            source["listItems"] = objs;
            source["pageCount"] = pageCount;
            source["recordCount"] = recordCount;
            ViewBag.source = source;
            ViewBag.pageCount = pageCount;
            ViewBag.recordCount = recordCount;

            List<Hashtable> categoryData = bo.GetApplicationCategoryList(-1);
            string cateStr = Newtonsoft.Json.JsonConvert.SerializeObject(categoryData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.clsData = "{items:" + cateStr + "}";

            List<Hashtable> unitData = bo.GetUnitList(-1);
            string unitStr = Newtonsoft.Json.JsonConvert.SerializeObject(unitData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.unitData = "{items:" + unitStr + "}";

            int sccnt = bo.GetSCPackageCount(uid);
            ViewBag.sccnt = sccnt;

            int tjcnt = bo.GetTJPackageCount();
            ViewBag.tjcnt = tjcnt;

            int bbcnt = bo.GetBBPackageCount();
            ViewBag.bbcnt = bbcnt;

            return View();
        }

        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                if (Request.UserAgent.ToLower().IndexOf("mobile") != -1)
                    return RedirectToAction("MLogin", "Home");
                else
                    return RedirectToAction("Login", "Home");
            }
            else
            {
                if (Request.UserAgent.ToLower().IndexOf("mobile") != -1)
                    return RedirectToAction("ML", "Home");
            }
            bo.SetController(this);

            if (Session["__UserName__"] != null)
            {
                ViewBag.userName = Session["__UserName__"];
            }
            else
            {
                ViewBag.userName = "游客";
            }


            string unitCode = "";

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.User.Identity.Name.ToLower() == "guest")
                    unitCode = "";
                else
                    unitCode = bo.GetUserUnitCode(HttpContext.User.Identity.Name);
            }

            List<Hashtable> commentAllData = new List<Hashtable>();// bo.GetCommentAllAppList();
            DataSet ds = bo.GetTJPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.All, unitCode, 0);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();// "PPS影音";
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();// "android手机版";
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();//"办公/政务";
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();// "上海互联网软件";
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    //hash["appUrl"] = Url.Content(downUrl); //"~/Home/Detail?packageid=5"
                    commentAllData.Add(hash);
                }
            }
            //Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //string jsonStr = serializer.Serialize(commentAllData);
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(commentAllData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.commentAll = "{items:" + jsonStr + ",pageIndex:1}";

            //List<Hashtable> obj = bo.GetCommentAllAppList();
            List<Hashtable> obj = null;

            List<Hashtable> commentAndroidPhoneData = new List<Hashtable>();
            ds = bo.GetTJPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.androidPhone, unitCode, -1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    if (!dr.IsNull("pe_displayname"))
                        hash["appName"] = dr["pe_displayname"].ToString();
                    else
                        hash["appName"] = "";
                    if (!dr.IsNull("pe_version"))
                        hash["appVersion"] = dr["pe_version"].ToString();
                    else
                        hash["appVersion"] = "";
                    if (!dr.IsNull("pe_Category"))
                        hash["appClass"] = dr["pe_Category"].ToString();
                    else
                        hash["appClass"] = "";
                    if (!dr.IsNull("pe_UnitName"))
                        hash["appUnit"] = dr["pe_UnitName"].ToString();
                    else
                        hash["appUnit"] = "";
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (!dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    else
                        hash["appImage"] = "";

                    //if (!dr.IsNull("pe_downloaduri"))
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    commentAndroidPhoneData.Add(hash);
                }
            }

            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(commentAndroidPhoneData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.commentAndroidData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> commentAndroidPadData = new List<Hashtable>();
            ds = bo.GetTJPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.androidPad, unitCode, -1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";

                    if (dr.IsNull("pe_PictureUrl"))
                    {
                        hash["appImage"] = "";
                    }
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    commentAndroidPadData.Add(hash);
                }
            }

            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(commentAndroidPadData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.commentAndroidPadData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> commentiPhoneData = new List<Hashtable>();
            ds = bo.GetTJPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.iPhone, unitCode, -1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    if (dr.IsNull("pe_displayname") == false)
                        hash["appName"] = dr["pe_displayname"].ToString();
                    else
                        hash["appName"] = "";

                    if (dr.IsNull("pe_version") == false)
                        hash["appVersion"] = dr["pe_version"].ToString();
                    else
                        hash["appVersion"] = "";

                    if (dr.IsNull("pe_Category") == false)
                        hash["appClass"] = dr["pe_Category"].ToString();
                    else
                        hash["appClass"] = "";
                    if (dr.IsNull("pe_UnitName") == false)
                        hash["appUnit"] = dr["pe_UnitName"].ToString();
                    else
                        hash["appUnit"] = "";
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl") == false)
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    else
                        hash["appImage"] = "";
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    commentiPhoneData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(commentiPhoneData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.commentiPhoneData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> commentiPadData = new List<Hashtable>();
            ds = bo.GetTJPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.iPad, unitCode, -1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    hash["appImage"] = dr.IsNull("pe_PictureUrl") ? "" : Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    commentiPadData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(commentiPadData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.commentiPadData = "{items:" + jsonStr + ",pageIndex:1}";

            List<Hashtable> commentLightAppData = new List<Hashtable>();
            ds = bo.GetTJPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.web, unitCode, 1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    hash["appImage"] = dr.IsNull("pe_PictureUrl") ? "" : Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    commentLightAppData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(commentLightAppData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.commentlightData = "{items:" + jsonStr + ",pageIndex:1}";




            obj = bo.GetCommentAllAppList();
            List<Hashtable> mustAllData = new List<Hashtable>();
            ds = bo.GetBBPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.All, unitCode, 0);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";

                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    mustAllData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(mustAllData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.mustAllData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> mustAndroidPhoneData = new List<Hashtable>();
            ds = bo.GetBBPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.androidPhone, unitCode, -1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    mustAndroidPhoneData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(mustAndroidPhoneData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.mustAndroidData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> mustAndroidPadData = new List<Hashtable>();
            ds = bo.GetBBPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.androidPad, unitCode, -1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    mustAndroidPadData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(mustAndroidPadData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.mustAndroidPadData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> mustiPhoneData = new List<Hashtable>();
            ds = bo.GetBBPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.iPhone, unitCode, -1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    mustiPhoneData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(mustiPhoneData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.mustiPhoneData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> mustiPadData = new List<Hashtable>();
            ds = bo.GetBBPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.iPad, unitCode, -1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    mustiPadData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(mustiPadData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.mustiPadData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> mustLightAppData = new List<Hashtable>();
            ds = bo.GetBBPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.web, unitCode, 1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    mustLightAppData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(mustLightAppData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.mustlightData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> newestAppData = new List<Hashtable>();
            ds = bo.GetNewestPackageList(unitCode);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    //hash["appVersion"] = dr["version"].ToString();
                    //hash["appClass"] = dr["pe_Category"].ToString();
                    //hash["appUnit"] = dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    newestAppData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(newestAppData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.newAppData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> androidAreaAppData = new List<Hashtable>();
            ds = bo.GetAndroidPhoneAreaPackageList(unitCode);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    androidAreaAppData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(androidAreaAppData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.androidAreaData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> iphoneAreaAppData = new List<Hashtable>();
            ds = bo.GetiPhoneAreaPackageList(unitCode);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    iphoneAreaAppData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(iphoneAreaAppData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.iphoneAreaData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> androidPadAreaAppData = new List<Hashtable>();
            ds = bo.GetAndroidPadAreaPackageList(unitCode);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    androidPadAreaAppData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(androidPadAreaAppData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.androidPadAreaData = "{items:" + jsonStr + ",pageIndex:1}";


            List<Hashtable> iPadAreaAppData = new List<Hashtable>();
            ds = bo.GetiPadAreaPackageList(unitCode);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    iPadAreaAppData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(iPadAreaAppData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.iPadAreaData = "{items:" + jsonStr + ",pageIndex:1}";

            List<Hashtable> lightAppAreaData = new List<Hashtable>();
            ds = bo.GetLightAppAreaPackageList(unitCode);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["appName"] = dr.IsNull("pe_displayname") ? "" : dr["pe_displayname"].ToString();
                    hash["appVersion"] = dr.IsNull("pe_version") ? "" : dr["pe_version"].ToString();
                    hash["appClass"] = dr.IsNull("pe_Category") ? "" : dr["pe_Category"].ToString();
                    hash["appUnit"] = dr.IsNull("pe_UnitName") ? "" : dr["pe_UnitName"].ToString();
                    if (!dr.IsNull("pe_CreatedTime"))
                        hash["appDate"] = Convert.ToDateTime(dr["pe_CreatedTime"]).ToString("yyyy-MM-dd");
                    else
                        hash["appDate"] = "";
                    if (dr.IsNull("pe_PictureUrl"))
                        hash["appImage"] = @Url.Content("~/AppIcons/no.png");
                    else
                        hash["appImage"] = Url.Content(dr["pe_PictureUrl"].ToString());
                    //string downUrl = dr["pe_downloaduri"].ToString();
                    hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["pe_id"].ToString(); ;
                    lightAppAreaData.Add(hash);
                }
            }
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(lightAppAreaData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.lightAppAreaData = "{items:" + jsonStr + ",pageIndex:1}";

            List<Hashtable> categoryData = bo.GetApplicationCategoryList(10);
            string cateStr = Newtonsoft.Json.JsonConvert.SerializeObject(categoryData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.category = "{items:" + cateStr + "}";

            List<Hashtable> unitData = bo.GetUnitList(5);
            string unitStr = Newtonsoft.Json.JsonConvert.SerializeObject(unitData, Newtonsoft.Json.Formatting.Indented);
            ViewBag.unitData = "{items:" + unitStr + "}";

            ViewBag.Message = "";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }

        public ActionResult MList2()
        {
            return View();
        }
    }
}
