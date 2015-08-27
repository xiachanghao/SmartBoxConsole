using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using SmartBox.Console.Bo;
using Beyondbit.MVC;
using System.Configuration;
using Beyondbit.Framework.Biz;
namespace SmartBox.Console.Web.Controllers
{

    [HandleError]
    public class AccountController : MyControllerBase
    {

        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.

        public AccountController()
        {
        }


        public ActionResult LogOn()
        {
            if (!String.IsNullOrEmpty(Beyondbit.SingleSignOn.ClientForAspNet20.SSOClient.UserUid))
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(Beyondbit.SingleSignOn.ClientForAspNet20.SSOClient.UserUid, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string SSO_SignOnUrl = ConfigurationManager.AppSettings["SSO_SignOnUrl"];
                if (!String.IsNullOrEmpty(SSO_SignOnUrl))
                    return Redirect(SSO_SignOnUrl);
            }

            Queue<string> exitPath = new Queue<string>();
            foreach (string path in Common.AppConfig.SSOExcludePath)
            {
                exitPath.Enqueue(path.ToLower());
            }
            string url = Server.UrlDecode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(url))
            {
                string[] ps = url.Split('/');
                foreach (string c in ps)
                {
                    string lc = c.ToLower();
                    if (lc != "" && lc != "http:")
                    {
                        if (exitPath.Contains(lc))
                        {
                            System.Web.Security.FormsAuthentication.SetAuthCookie("guest", false);
                            return new RedirectResult(@"http://" + Request.Url.Authority + url);
                        }
                    }
                }
            }
            ViewData["msg"] = "";
            return View();
        }

        public ActionResult Logout()
        {
            string SSO_SignOnUrl = ConfigurationManager.AppSettings["SSO_SignOnUrl"];

            HttpRuntime.Cache.Remove(CurrentUser.UserUId.ToLower());
            FormsAuthentication.SignOut();
            this.Session.Clear();
            this.Session.Abandon();


            if (!String.IsNullOrEmpty(SSO_SignOnUrl))
            {
                //Beyondbit.SingleSignOn.ClientForAspNet20.SSOClient.
                string SSO_SignOutUrl = ConfigurationManager.AppSettings["SSO_SignOutUrl"];
                if (!String.IsNullOrEmpty(SSO_SignOutUrl))
                    return Redirect(SSO_SignOutUrl);
            }

            return Redirect(FormsAuthentication.LoginUrl);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogOn(string userName, string password)
        {
            string SSO_SignOnUrl = ConfigurationManager.AppSettings["SSO_SignOnUrl"];
            if (!String.IsNullOrEmpty(SSO_SignOnUrl))
            {
                return _LogOnWithSSO(userName, password);
            }
            else
            {
                return _LogOnWithOutSSO(userName, password);
            }
        }

        private ActionResult _LogOnWithSSO(string userName, string password)
        {
            ViewData["msg"] = "";
            if (!User.Identity.IsAuthenticated)
            {
                //验证数据库
                //if(true)
                if (String.IsNullOrEmpty(Beyondbit.SingleSignOn.ClientForAspNet20.SSOClient.UserUid))
                {
                    ViewData["msg"] = "登陆失败，请检查用户名和密码";
                    return View();
                }
                else
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(userName, false);
                    return RedirectToAction("Index", "Home");
                }

                //if (BoFactory.GetVersionTrackBo.CheckUserName(userName, password))
                //{
                //    System.Web.Security.FormsAuthentication.SetAuthCookie(userName, false);
                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    bool r = BoFactory.GetSMC_UserListBo.CheckUserName(userName, password);
                //    if (r)
                //    {
                //        System.Web.Security.FormsAuthentication.SetAuthCookie(userName, false);
                //        return RedirectToAction("Index", "Home");
                //    }
                //    else
                //    {
                //        ViewData["msg"] = "登陆失败，请检查用户名和密码";
                //        return View();
                //    }

                //}
            }
            else
                return RedirectToAction("Index", "Home");
        }

        private ActionResult _LogOnWithOutSSO(string userName, string password)
        {
            ViewData["msg"] = "";
            if (!User.Identity.IsAuthenticated)
            {
                //验证数据库
                //if(true)
                try
                {

                    if (HttpRuntime.Cache[userName.ToLower()] != null && ((DateTime)HttpRuntime.Cache[userName.ToLower()]) > DateTime.Now)
                    {
                        ViewData["msg"] = "该用户已在其他浏览器中登录";
                        return View();
                    }
                    if (BoFactory.GetVersionTrackBo.CheckUserName(userName, password, Request.UserHostAddress)
                        || BoFactory.GetSMC_UserListBo.CheckUserName(userName, password))
                    {
                        System.Web.Security.FormsAuthentication.SetAuthCookie(userName, false);
                    }
                    else
                    {
                        ViewData["msg"] = "登陆失败，请检查用户名和密码";
                        return View();
                    }
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ViewData["msg"] = "登陆失败，" + ex.Message;
                    SmartBox.Console.Common.Log4NetHelper.Error(ex);
                    return View();
                }
            }
            else
                return RedirectToAction("Index", "Home");
        }


    }
}
