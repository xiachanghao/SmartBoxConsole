using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Principal;
using System.Web.Mvc;
using SmartBox.Console.Bo;
using Beyondbit.MVC;

namespace SmartBox.Console.Authentication
{
    public class SignOn
    {
        //public ActionResult LogOn(string userName, string password, ViewDataDictionary ViewData, MyControllerBase controller)
        //{            
        //    ViewData["msg"] = "";
        //    if (!HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        //验证数据库
        //        //if(true)
        //        if (BoFactory.GetVersionTrackBo.CheckUserName(userName, password))
        //        {
        //            System.Web.Security.FormsAuthentication.SetAuthCookie(userName, false);
        //            return controller.RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            bool r = BoFactory.GetSMC_UserListBo.CheckUserName(userName, password);
        //            if (r)
        //            {
        //                System.Web.Security.FormsAuthentication.SetAuthCookie(userName, false);
        //                return controller.RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ViewData["msg"] = "登陆失败，请检查用户名和密码";
        //                return controller.View();
        //            }

        //        }
        //    }
        //    else
        //        return controller.RedirectToAction("Index", "Home");
        //}
    }
}
