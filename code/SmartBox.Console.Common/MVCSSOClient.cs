using System;
using System.Collections.Generic;
using Beyondbit.SingleSignOn.ClientForAspNet20;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using System.Web.Routing;

namespace SmartBox.Console.Common
{
    public class MVCSSOClient: SSOClient
    {
        protected override void OnAppEndVerifyUser(HttpApplication application)
        {
            base.OnAppEndVerifyUser(application);
            if (!string.IsNullOrEmpty(Beyondbit.SingleSignOn.ClientForAspNet20.SSOClient.UserUid))
            {
                if (!application.Context.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SetAuthCookie(Beyondbit.SingleSignOn.ClientForAspNet20.SSOClient.UserUid, false);
                }
            }
        }
        protected override bool IsVerifyPage(System.Web.HttpApplication application)
        {
            const string key = "controller";
            const string action = "action";
            const string type = "type";

            Queue<string> exitPath = new Queue<string>();
            foreach (string path in AppConfig.SSOExcludePath)
            {
                exitPath.Enqueue(path.ToLower());
            }

            bool fiterEnable = base.IsVerifyPage(application);
            if (fiterEnable)
            {
                return true;
            }
            else
            {
                //排除目录
                string[] ps = application.Request.Url.AbsoluteUri.Split('/');
                foreach (string c in ps)
                {
                    string lc = c.ToLower();
                    if (lc != "" && lc != "http:")
                    {
                        if (exitPath.Contains(lc))
                        {
                            return false;
                        }
                    }
                }
                if (application.Context == null || application.Context.Session == null)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(Beyondbit.SingleSignOn.ClientForAspNet20.SSOClient.UserUid))
                {
                    return true;
                }
                if (!application.Context.User.Identity.IsAuthenticated)
                {
                    //MVC的 验证逻辑
                    HttpContextBase context = new HttpContextWrapper(application.Context);
                    RouteData routeData = RouteTable.Routes.GetRouteData(context);
                    if (routeData != null)
                    {
                        object controllerName = routeData.Values[key];
                        object actionName = routeData.Values[action];
                        object typeName = routeData.Values[type];
                        if (controllerName != null)
                        {
                            string lowControllerName = controllerName.ToString().ToLower();
                            //if (actionName != null && typeName != null)
                            //{
                            //    string lowactionName = actionName.ToString().ToLower();
                            //    string lowtypeName = typeName.ToString().ToLower();
                            //}
                            return !exitPath.Contains(lowControllerName);
                        }

                    }
                }

            }
            return false;
        }
    }
}
