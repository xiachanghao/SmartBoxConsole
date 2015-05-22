using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common.Entities.Search;
using System.Collections;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Dao
{
    public class ConfigCategoryDao:BaseDao<ConfigCategory>
    {
           /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">数据库链接字符串</param>
        public ConfigCategoryDao(string key)
            : base(key)
        {

        }

        public IList<ConfigCategory> GetMainCategoryList()
        {
            string Select = ""; //"select * from ConfigCategory where  ConfigCategoryCode='" + Constants.cofigParam + "' or  ConfigCategoryCode='" + Constants.cofigParam1 + "' ";
            return Query(Select);
        }

        public ConfigCategory GetInfo(string code)
        {
            ConfigCategory c = new ConfigCategory();
            switch (code)
            {
                case Constants.configCategory:
                    c.DisplayName = Constants.configCategoryName;
                    c.ConfigCategoryCode = Constants.configCategory;
                    break;
                case Constants.AppNameConfig:
                    c.DisplayName = Constants.AppNameConfigName;
                    c.ConfigCategoryCode = Constants.AppNameConfig;
                    break;
                case Constants.GlobalConfig:
                    c.DisplayName = Constants.GlobalConfigName;
                    c.ConfigCategoryCode = Constants.GlobalConfig;
                    break;
                case Constants.PCGlobalConfig:
                    c.DisplayName = Constants.PCGlobalConfigName;
                    c.ConfigCategoryCode = Constants.PCGlobalConfig;
                    break;
                case Constants.SystemConfig:
                    c.DisplayName = Constants.SystemConfigName;
                    c.ConfigCategoryCode = Constants.SystemConfig;
                    break;
                case Constants.UpdaterConfig:
                    c.DisplayName = Constants.UpdaterConfigName;
                    c.ConfigCategoryCode = Constants.UpdaterConfig;
                    break;
            }

            return c;
        }

    }
}
