using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Core.Proxy;

namespace SmartBox.Console.UserMenu
{
    public static class UserMenuFactory
    {
        static readonly IUserMenu _iUserMenu = null;
        static UserMenuFactory()
        {
            IProxy proxy = ProxyFactory.CreateProxy();
            string userMenu = System.Web.Configuration.WebConfigurationManager.AppSettings["UserMenu"];
            if (!String.IsNullOrEmpty(userMenu) && userMenu.ToLower() == "usermenuold")
            {
                _iUserMenu = proxy.CreateObject<UserMenuOld>();
            }
            else
            {
                _iUserMenu = proxy.CreateObject<UserMenuNew>();
            }
        }

        public static IUserMenu GetUserMenuBO
        {
            get
            {
                return _iUserMenu;
            }
        }
    }
}
