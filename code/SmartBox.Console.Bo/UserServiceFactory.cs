using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.MVC;
using Beyondbit.Framework.Core.Proxy;

namespace SmartBox.Console.Bo
{

    public class UserServiceFactory : IUserServiceFactory
    {
        #region IUserServiceFactory 成员

        public IUserService GetUserService()
        {
            IProxy proxy = ProxyFactory.CreateProxy();
            return proxy.CreateObject<UserService>();
        }

        #endregion
    }
}
