using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SmartBox.Console.Common.Configuration;

namespace SmartBox.Console.Common
{
    public class AppConfig
    {
        /// <summary>
        /// 应用程序代码
        /// </summary>
        public static string Domain
        {
            get { return ConfigurationManager.AppSettings.Get("Domain"); }
        }


        /// <summary>
        /// 获取主题定义，如果没有设置则返回默认主题Default
        /// </summary>
        /// <value>The theme.</value>
        /// <Author>wangsm 2009/3/6</Author>
        public static string Theme
        {
            get
            {
                string theme = ConfigurationManager.AppSettings.Get("Theme");
                if (string.IsNullOrEmpty(theme))
                {
                    theme = "Default";
                }
                return theme;
            }
        }
        /// <summary>
        /// 数据库链接的Key
        /// </summary>
        public static string mainDbKey
        {
            get { return "mainDB"; }
        }

        public static string statisticDBKey
        {
            get { return "statisticDB"; }
        }

        public static string filePath
        {
            get
            {
                string name = ConfigurationManager.AppSettings.Get("FilePath");
                if (!name.EndsWith("\\"))
                    name += "\\";
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name);
                return filePath;
            }
        }

        public static string SaveZipPath
        {
            get
            {
                string name = ConfigurationManager.AppSettings.Get("SaveZipPath");
                if (!name.EndsWith("\\"))
                    name += "\\";
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name);
                return filePath;
            }
        }

        public static string pubFolder
        {
            get
            {
                string pubFolder = ConfigurationManager.AppSettings.Get("pubFolder");
                return pubFolder;
            }

        }


        public static string subFix
        {
            get
            {
                string SubFix = "." + ConfigurationManager.AppSettings.Get("extension");
                return SubFix;
            }
        }

        /// <summary>
        /// SSO中需要排除的目录，包括图片，脚本和CSS等
        /// </summary>
        /// <value>The SSO exclude path.</value>
        public static string[] SSOExcludePath
        {
            get
            {
                string paths = ConfigurationManager.AppSettings.Get("SSO_ExcludePath");
                if (string.IsNullOrEmpty(paths))
                {
                    return new[] { "theme", "javascripts", "images" };
                }
                else
                {
                    return paths.Split(',');
                }
            }

        }



        public static string PackUploadFolder
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("packUploadFolder");
            }
        }

        public static string OutPackUploadFolder
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("outpackUploadFolder");
            }
        }

        public static string FuncIconFolder
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("FuncIconFolder");
            }
        }

        public static string OutWebHost
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("OutWebHost");
            }
        }

        public static string PackUrl
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("packUrl");
            }
        }

        private static PublishConfigCollection _publishConfig = null;
        public static PublishConfigCollection PublishConfig
        {
            get
            {
                if (_publishConfig == null)
                {
                    IOSConfiguration config = (IOSConfiguration)ConfigurationManager.GetSection("PublishConfig");
                    if (config == null || config.IOSPublishs == null)
                    {
                        throw new ArgumentException("控制台配置有问题，请联系管理人员", "PublishConfig");
                    }
                    _publishConfig = new PublishConfigCollection();
                    foreach (IOSPublishElement item in config.IOSPublishs)
                    {
                        _publishConfig.Add(item.ClientType.ToLower(), item);
                    }
                }
                return _publishConfig;
            }
        }
        public class PublishConfigCollection
        {
            private Dictionary<string, IOSPublishElement> _config = new Dictionary<string, IOSPublishElement>();
            public void Add(string key, IOSPublishElement value)
            {
                _config.Add(key.ToLower(), value);
            }

            public IOSPublishElement GetValue(string key)
            {
                return _config[key.ToLower()];
            }

            public bool ContainsKey(string key)
            {
                return _config.ContainsKey(key.ToLower());
            }
        }

        public static string _debug_mode = System.Configuration.ConfigurationSettings.AppSettings["debug_mode"] ?? "false";
        public static bool DebugMode
        {
            get 
            {
                return _debug_mode.Equals("true", StringComparison.CurrentCultureIgnoreCase);
            }
        }

        public static string iOSDownloadUrl
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("iOSDownloadUrl");
            }
        }

        public static string deployAddress
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("deployAddress");
            }
        }

        public static string deployPath
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("deployPath");
            }
        }
    }
}
