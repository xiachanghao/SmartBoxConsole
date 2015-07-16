using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SmartBox.Console.Common
{
    public static class Log4NetHelper
    {
        static Log4NetHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private static log4net.ILog log = log4net.LogManager.GetLogger("SmartBox.Console.Common.Log4NetHelper");
        public static void Error(Exception ex)
        {
            log.Error(ex.ToString());
        }

        public static void Error(string msg, Exception ex)
        {
            log.Error(msg, ex);
        }

        public static void Error(string msg)
        {
            log.Error(msg);
        }

        public static void Info(string msg)
        {
            log.Info(msg);
        }
    }
}
