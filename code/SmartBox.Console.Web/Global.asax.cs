using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Win32;
using System.Globalization;

namespace SmartBox.Console.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        #region IIS检测
        const string IISRegKeyName = "Software\\Microsoft\\InetStp";
        const string IISRegKeyValue = "MajorVersion";

        private static bool TryGetRegValue<T>(RegistryHive hive, string key, string value, RegistryValueKind kind, out T data)
        {
            bool success = false;
            data = default(T);

            using (RegistryKey baseKey = RegistryKey.OpenRemoteBaseKey(hive, String.Empty))
            {
                if (baseKey != null)
                {
                    using (RegistryKey registryKey = baseKey.OpenSubKey(key, RegistryKeyPermissionCheck.ReadSubTree))
                    {
                        if (registryKey != null)
                        {
                            try
                            {
                                RegistryValueKind kindFound = registryKey.GetValueKind(value);
                                if (kindFound == kind)
                                {
                                    object regValue = registryKey.GetValue(value, null);
                                    if (regValue != null)
                                    {
                                        data = (T)Convert.ChangeType(regValue, typeof(T), CultureInfo.InvariantCulture);
                                        success = true;
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            return success;
        }
        private static int GetIISVersion()
        {
            int regValue;
            TryGetRegValue(RegistryHive.LocalMachine, IISRegKeyName, IISRegKeyValue, RegistryValueKind.DWord, out regValue);
            return regValue;
        }
        #endregion

        public static void RegisterRoutes(RouteCollection routes)
        {
            bool isIIS6 = false;//默认IIS7
            string ver = System.Configuration.ConfigurationManager.AppSettings["IIS"];
            if (!string.IsNullOrEmpty(ver))
            {
                isIIS6 = ver.Trim() == "6";
            }
            else
            {
                int version = 0;
                try
                {
                    version = GetIISVersion();
                }
                catch
                {
                }
                if (version == 6)
                    isIIS6 = true;
            }


            //#if IIS6
            if (isIIS6)
            {
                routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

                routes.MapRoute("NoAction", "{controller}", 
                    new { controller = "Home", action = "Index", id = "" }); //使Default.aspx转向

                routes.MapRoute(
                    "Default",                                              // Route name
                    "{controller}/{action}.mvc/{id}",                       // URL with parameters
                    new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
                );
            }
            else
            {
                //#else
                routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


                routes.MapRoute(
                    "Default",                                              // Route name
                    "{controller}/{action}/{id}",                           // URL with parameters
                    new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
                );
                //#endif
            }
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}