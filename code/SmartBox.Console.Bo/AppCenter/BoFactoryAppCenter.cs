using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Core.Proxy;

namespace SmartBox.Console.Bo.AppCenter
{
    public static class BoFactoryAppCenter
    {
        static readonly SmartBox.Console.Bo.AppCenter.AppCenterBO _AppCenterBO = null;


        static BoFactoryAppCenter()
        {
            IProxy proxy = ProxyFactory.CreateProxy();
            //_AppCenterBO = proxy.CreateObject<SmartBox.Console.Bo.AppCenter.AppCenterBO>();
        }

        //public static SmartBox.Console.Bo.AppCenter.AppCenterBO GetAppCenterBO
        //{
        //    get
        //    {
        //        return _AppCenterBO;
        //    }
        //}
    }
}
