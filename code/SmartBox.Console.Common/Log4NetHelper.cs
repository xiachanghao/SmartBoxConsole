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

        public static void Error(Exception ex)
        {//MethodBase.GetCurrentMethod().DeclaringType
            log4net.ILog log = log4net.LogManager.GetLogger("SmartBox.Console.Common.Log4NetHelper");
            string msg = String.Format("{0}\r\nStackTrace:\r\n{1}", ex.Message, ex.StackTrace);
            log.Error(msg);
            ex = ex.InnerException;
            while (ex != null)
            {
                msg = String.Format("\r\n{0}\r\nStackTrace:\r\n{1}", ex.Message, ex.StackTrace);
                log.Error(msg);

                ex = ex.InnerException;
            }
        }

        public static void Error(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("SmartBox.Console.Common.Log4NetHelper");
            log.Error(msg);
        }

        public static void Info(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("SmartBox.Console.Common.Log4NetHelper");
            log.Info(msg);
        }
    }
}
