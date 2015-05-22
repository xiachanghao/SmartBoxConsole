using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beyondbit.Push.Service
{
    public static class Logger
    {
        private static ILog _logger = LogManager.GetLogger("Default");

        public static ILog Log
        {
            get
            {
                return _logger;
            }
        }
    }
}
