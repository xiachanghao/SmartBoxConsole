using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Beyondbit.Push.Service
{
    public class ConfigHelp
    {

        private static string _connectionString = ConfigurationManager.ConnectionStrings["BeyondbitPush"].ConnectionString;
        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        private static bool _production = Convert.ToBoolean(ConfigurationManager.AppSettings["Production"]);
        public static bool Production
        {
            get
            {
                return _production;
            }
        }
    }
}