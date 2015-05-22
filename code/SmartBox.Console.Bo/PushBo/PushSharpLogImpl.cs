using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beyondbit.Push.Service
{
    public class PushSharpLogImpl : PushSharp.Core.ILogger
    {
        public void Debug(string format, params object[] objs)
        {
            Logger.Log.DebugFormat(format, objs);
        }

        public void Error(string format, params object[] objs)
        {
            Logger.Log.ErrorFormat(format, objs);
        }

        public void Info(string format, params object[] objs)
        {
            Logger.Log.InfoFormat(format, objs);
        }

        public void Warning(string format, params object[] objs)
        {
            Logger.Log.WarnFormat(format, objs);
        }
    }
}