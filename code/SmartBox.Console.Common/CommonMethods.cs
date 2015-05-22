using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Common
{
    public static class CommonMethods
    {

        #region 主程序


        #region 获取xml

        //获取配置信息  
        public static IList<ConfigInfo> GetMainEntityFromXML(string xmlPath)
        {
            IList<ConfigInfo> list = new List<ConfigInfo>();
            var xml = new XmlDocument();
            xml.Load(xmlPath);
            string[] codes = xmlPath.Split(new string[] { "\\" }, StringSplitOptions.None);
            string name = codes[codes.Length - 1];//获取配置文件类型
            switch (name.ToLower())
            {
                case "global.config":
                    list = GetConfigInfoForS(xml, list, Constants.GlobalConfig);
                    break;
                case "system.config":
                    list = GetConfigInfoForS(xml, list, Constants.SystemConfig);
                    break;
                case "beyondbit.smartbox.ui.exe.config":
                    list = GetConfigInfoForA(xml, list, Constants.AppNameConfig, Constants.MianName);
                    break;
                case "launch.exe.config":
                    list = GetConfigInfoForA(xml, list, Constants.UpdaterConfig,Constants.UpdaterCode);
                    break;
            }

            return list;
        }
        
        public static IList<ConfigInfoPC> GetMainEntityFromXMLPC(string xmlPath)
        {
            IList<ConfigInfoPC> list = new List<ConfigInfoPC>();
            var xml = new XmlDocument();
            xml.Load(xmlPath);
            string[] codes = xmlPath.Split(new string[] { "\\" }, StringSplitOptions.None);
            string name = codes[codes.Length - 1];//获取配置文件类型
            switch (name.ToLower())
            {
                case "global.config":
                    list = GetConfigInfoForS(xml, list, Constants.GlobalConfig);
                    break;
                case "system.config":
                    list = GetConfigInfoForS(xml, list, Constants.SystemConfig);
                    break;
                case "beyondbit.smartbox.ui.exe.config":
                    list = GetConfigInfoForA(xml, list, Constants.AppNameConfig, Constants.MianName);
                    break;
                case "launch.exe.config":
                    list = GetConfigInfoForA(xml, list, Constants.UpdaterConfig,Constants.UpdaterCode);
                    break;
            }

            return list;
        }

        private static IList<ConfigInfo> GetConfigInfoForS(XmlDocument xml, IList<ConfigInfo> list, string cate)
        {
            var configInfonodes = xml.SelectNodes("//SmartBoxConfig/*");
            ConfigInfo c = null;
            if (configInfonodes == null)
            {
                throw new ArgumentNullException("没有SmartBoxConfig节点");
            }
            foreach (XmlNode item in configInfonodes)
            {
                c = new ConfigInfo();
                c.Key1 = GetNodeValue(item.SelectSingleNode("./key"));
                c.Value1 = GetNodeValue(item.SelectSingleNode("./value"));
                c.PluginCode = Constants.MianName;
                c.ConfigCategoryCode = cate;
                list.Add(c);
            }
            return list;
        }
            
        private static IList<ConfigInfoPC> GetConfigInfoForS(XmlDocument xml, IList<ConfigInfoPC> list, string cate)
        {
            var configInfonodes = xml.SelectNodes("//SmartBoxConfig/*");
            ConfigInfoPC c = null;
            if (configInfonodes == null)
            {
                throw new ArgumentNullException("没有SmartBoxConfig节点");
            }
            foreach (XmlNode item in configInfonodes)
            {
                c = new ConfigInfoPC();
                c.Key1 = GetNodeValue(item.SelectSingleNode("./key"));
                c.Value1 = GetNodeValue(item.SelectSingleNode("./value"));
                c.PluginCode = Constants.MianName;
                c.ConfigCategoryCode = cate;
                list.Add(c);
            }
            return list;
        }

        private static IList<ConfigInfo> GetConfigInfoForA(XmlDocument xml, IList<ConfigInfo> list, string cate,string code)
        {
            var configInfonodes = xml.SelectNodes("//appSettings/*");
            ConfigInfo c = null;
            if (configInfonodes == null)
            {
                throw new ArgumentNullException("没有appSettings节点");
            }
            foreach (XmlNode item in configInfonodes)
            {
                c = new ConfigInfo();
                c.Key1 = item.Attributes["key"].Value;
                c.Value1 = item.Attributes["value"].Value;
                c.PluginCode = code;
                c.ConfigCategoryCode = cate;
                list.Add(c);
            }
            return list;
        }

        private static IList<ConfigInfoPC> GetConfigInfoForA(XmlDocument xml, IList<ConfigInfoPC> list, string cate, string code)
        {
            var configInfonodes = xml.SelectNodes("//appSettings/*");
            ConfigInfoPC c = null;
            if (configInfonodes == null)
            {
                throw new ArgumentNullException("没有appSettings节点");
            }
            foreach (XmlNode item in configInfonodes)
            {
                c = new ConfigInfoPC();
                c.Key1 = item.Attributes["key"].Value;
                c.Value1 = item.Attributes["value"].Value;
                c.PluginCode = code;
                c.ConfigCategoryCode = cate;
                list.Add(c);
            }
            return list;
        }

        #endregion


        #region 写入xml

        public static void WriteMaininfoConfigXml(IList<ConfigInfoPC> list, string fileName)
        {
            var xml = new XmlDocument();
            xml.Load(fileName);//获取原由配置

            string[] codes = fileName.Split(new string[] { "\\" }, StringSplitOptions.None);
            string name = codes[codes.Length - 1];//获取配置文件类型
            name = name.ToLower();
            switch (name)
            {
                case "global.config":
                    WriteConfigNodes(xml, list, fileName);
                    break;
                case "system.config":
                    WriteConfigNodes(xml, list, fileName);
                    break;
                case "beyondbit.smartbox.ui.exe.config":
                    WriteAppSettingNodes(xml, list, fileName);
                    break;
                case "launch.exe.config":
                    WriteAppSettingNodes(xml, list, fileName);
                    break;
            }
        }

        private static void WriteAppSettingNodes(XmlDocument xml, IList<ConfigInfoPC> list, string fileName)
        {
            //先删除原由appsetting下面的项
            var configInfonodes = xml.SelectSingleNode("//appSettings");
            configInfonodes.RemoveAll();
            //追加appsetting下面的项
            foreach (var c in list)
            {
                XmlElement configElement = xml.CreateElement("add");
                configElement.SetAttribute("key", c.Key1);
                configElement.SetAttribute("value", c.Value1);
                configInfonodes.AppendChild(configElement);
            }

            xml.Save(fileName);
        }

        private static void WriteConfigNodes(XmlDocument xml, IList<ConfigInfoPC> list, string fileName)
        {
            //先删除原由appsetting下面的项
            var configInfonodes = xml.SelectSingleNode("//SmartBoxConfig");
            configInfonodes.RemoveAll();
            //追加appsetting下面的项
            foreach (var c in list)
            {
                XmlElement configElement = xml.CreateElement("Config");
                configInfonodes.AppendChild(configElement);
                XmlElement configElementKey = xml.CreateElement("key");
                configElementKey.InnerText = c.Key1;
                configElement.AppendChild(configElementKey);
                XmlElement configElementValue = xml.CreateElement("value");
                configElementValue.InnerText = c.Value1;
                configElement.AppendChild(configElementValue);
            }

            xml.Save(fileName);
        }

        #endregion


        #endregion



        public static ConfigTemp ConvertToConfigInfo(ConfigInfo model)
        {
            ConfigTemp configInfo = new ConfigTemp();

            Type ToModel = configInfo.GetType();
            Type FromModel = model.GetType();
            var fileds = ToModel.GetProperties();
            foreach (var item in fileds)
            {
                object fromValue = Convert.ChangeType(model.GetType().GetProperty(item.Name).GetValue(model, null), item.PropertyType);

                if (fromValue != null)
                {
                    configInfo.GetType().GetProperty(item.Name).SetValue(configInfo, fromValue, null);
                }
            }

            return configInfo;
        }
        
        public static ConfigTemp ConvertToConfigInfoPC(ConfigInfoPC model)
        {
            ConfigTemp configInfo = new ConfigTemp();

            Type ToModel = configInfo.GetType();
            Type FromModel = model.GetType();
            var fileds = ToModel.GetProperties();
            foreach (var item in fileds)
            {
                object fromValue = Convert.ChangeType(model.GetType().GetProperty(item.Name).GetValue(model, null), item.PropertyType);

                if (fromValue != null)
                {
                    configInfo.GetType().GetProperty(item.Name).SetValue(configInfo, fromValue, null);
                }
            }

            return configInfo;
        }

        public static ConfigInfo ConvertToConfigInfo(ConfigTemp model)
        {
            ConfigInfo configInfo = new ConfigInfo();

            Type ToModel = configInfo.GetType();
            Type FromModel = model.GetType();
            var fileds = ToModel.GetProperties();
            foreach (var item in fileds)
            {
                object fromValue = Convert.ChangeType(model.GetType().GetProperty(item.Name).GetValue(model, null), item.PropertyType);

                if (fromValue != null)
                {
                    configInfo.GetType().GetProperty(item.Name).SetValue(configInfo, fromValue, null);
                }
            }

            return configInfo;
        }

        public static ConfigInfoPC ConvertToConfigInfoPC(ConfigTemp model)
        {
            ConfigInfoPC configInfo = new ConfigInfoPC();

            Type ToModel = configInfo.GetType();
            Type FromModel = model.GetType();
            var fileds = ToModel.GetProperties();
            foreach (var item in fileds)
            {
                object fromValue = Convert.ChangeType(model.GetType().GetProperty(item.Name).GetValue(model, null), item.PropertyType);

                if (fromValue != null)
                {
                    configInfo.GetType().GetProperty(item.Name).SetValue(configInfo, fromValue, null);
                }
            }

            return configInfo;
        }



        private const string PluginConfigType = "PluginConfig";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zipFileName">要解压的绝对路径+文件名</param>
        /// <param name="targetDir">要释放到的路径名</param>
        public static void Uncompress(string zipFileName, string targetDir)
        {
            FastZip zip = new FastZip();
            zip.ExtractZip(zipFileName, targetDir, "");
        }

        /// <summary>
        /// 根据xml生成XmlConfigInfo实体
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static XmlConfigInfo GetEntityFromXML(string xmlPath)
        {

            var configInfo = new XmlConfigInfo();
            var xml = new XmlDocument();
            xml.Load(xmlPath);
            configInfo.PluginInfo = GetPluginInfoFromXml(xml);


            // configInfo.configList = GetPluginConfigListByXml(xml);
            return configInfo;

        }

        /// <summary>
        /// 根据xml 获取pluginInfo
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static List<PluginInfoTemp> GetPluginInfoFromXml(XmlDocument xml)
        {
            List<PluginInfoTemp> pluginInfoList = new List<PluginInfoTemp>();

            // GetBaseInfo(pluginInfo, xml);

            var pluginInfonodes = xml.SelectNodes("//Plugins/*");
            if (pluginInfonodes == null)
            {
                throw new ArgumentNullException("没有Plugins节点");
            }
            foreach (XmlNode item in pluginInfonodes)
            {

                var pluginInfo = new PluginInfoTemp();
                pluginInfoList.Add(pluginInfo);
                GetBaseInfo(pluginInfo, xml);

                pluginInfo.DisplayName = GetNodeValue(item.SelectSingleNode("./Name"));
                pluginInfo.PluginCode = GetNodeValue(item.SelectSingleNode("./PluginCode"));
                pluginInfo.PluginCateCode = GetNodeValue(item.SelectSingleNode("./PluginCateCode"));
                pluginInfo.TypeFullName = GetNodeValue(item.SelectSingleNode("./TypeFullName"));
                pluginInfo.FileName = GetNodeValue(item.SelectSingleNode("./MainFileName"));
                //  pluginInfo.PluginSummary = GetNodeValue(item.SelectSingleNode("./PluginDescription"));
                pluginInfo.PluginSummary = GetNodeValue(item.SelectSingleNode("./Description"));
                pluginInfo.IsNeed = GetNodeValue(item.SelectSingleNode("./IsNeed")).ToLower() == "true";
                //web插件独有
                pluginInfo.PluginUrl = GetNodeValue(item.SelectSingleNode("./Url"));
                //Action插件独有
                pluginInfo.ActionCode = GetNodeValue(item.SelectSingleNode("./ActionCode"));
                pluginInfo.ActionSummary = GetNodeValue(item.SelectSingleNode("./ActionDescription"));
                //版本信息 必须
                pluginInfo.Version = GetNodeValue(item.SelectSingleNode("./VersionCode"));
                pluginInfo.VersionSummary = GetNodeValue(item.SelectSingleNode("./VersionDescription"));

                //是否需要权限控制
                if (item.SelectSingleNode("./IsPublic") != null && !string.IsNullOrEmpty(GetNodeValue(item.SelectSingleNode("./IsPublic"))))
                    pluginInfo.IsPublic = Convert.ToBoolean(GetNodeValue(item.SelectSingleNode("./IsPublic")));
                else
                    pluginInfo.IsPublic = true;
                pluginInfo.AppCode = GetNodeValue(item.SelectSingleNode("./AppCode"));
                pluginInfo.PrivilegeCode = GetNodeValue(item.SelectSingleNode("./PrivilegeCode"));
                // 获取配置信息
                pluginInfo.configList = GetPluginConfigListByXml(item);

            }
            return pluginInfoList;
        }

        private static string GetNodeValue(XmlNode item)
        {

            if (item == null)
            {
                return "";
            }
            return item.InnerText;
        }

        private static void GetBaseInfo(PluginInfoTemp pluginInfo, XmlDocument xml)
        {
            if (pluginInfo == null)
                throw new ArgumentNullException("请给要添加BaseInfo的实体赋值！");
            var pluginBaseInfonodes = xml.SelectNodes("//BaseInfo/*");
            if (pluginBaseInfonodes == null)
            {
                throw new ArgumentNullException("输入的Xml不存在BaseInfo节点");
            }
            foreach (XmlNode item in pluginBaseInfonodes)
            {
                var value = item.InnerText;
                switch (item.Name)
                {
                    case "CompanyName":
                        pluginInfo.CompanyName = value;
                        break;
                    case "CompanyLinkman":
                        pluginInfo.CompanyLinkman = value;
                        break;
                    case "CompanyTel":
                        pluginInfo.CompanyTel = value;
                        break;
                    case "CompanyHomePage":
                        pluginInfo.CompanyHomePage = value;
                        break;
                }

            }
        }

        private static string GetTextByNode(XmlDocument xml, string nodeName)
        {
            var node = xml.SelectSingleNode("//" + nodeName);
            if (node == null)
            {
                return string.Empty;
            }
            return node.InnerText;
        }

        private static IList<ConfigInfo> GetPluginConfigListByXml(XmlNode xml)
        {
            var configList = new List<ConfigInfo>();

            var configListNodes = xml.SelectNodes("./Configs/*");
            if (configListNodes == null)
            {
                throw new ArgumentNullException();
            }
            foreach (XmlNode node in configListNodes)
            {
                var key = node.SelectSingleNode("./key");
                var value = node.SelectSingleNode("./value");
                //      var category = node.SelectSingleNode("./category");

                if (key != null && value != null && !string.IsNullOrEmpty(key.InnerText))
                {
                    var config = new ConfigInfo { Key1 = key.InnerText, Value1 = value.InnerText, ConfigCategoryCode = PluginConfigType };
                    configList.Add(config);
                }
            }

            return configList;
        }
        /*
        public static void WriteConfigListXml(IList<ConfigInfo> list, string fileName)
        {
            XmlDocument xml = SetXmlByConfigList(list);
            xml.Save(fileName);

        }
        */
        public static void WritePluginfoConfigXml(XmlConfigInfo configInfo, string fileName)
        {
            XmlDocument xml = new XmlDocument();
            XmlNode declare = xml.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xml.AppendChild(declare);

            XmlElement pluginInfoElement = xml.CreateElement("PluginInfo");
            xml.AppendChild(pluginInfoElement);

            XmlElement baseInfoElement = xml.CreateElement("BaseInfo");
            pluginInfoElement.AppendChild(baseInfoElement);

            XmlElement Plugins = xml.CreateElement("Plugins");
            pluginInfoElement.AppendChild(Plugins);

            int i = 0;
            foreach (var PluginInfo in configInfo.PluginInfo)
            {
                #region  BaseInfo

                i++;
                if (i <= 1)
                {
                    XmlElement CompanyNameElement = xml.CreateElement("CompanyName");

                    CompanyNameElement.InnerText = PluginInfo.CompanyName;
                    baseInfoElement.AppendChild(CompanyNameElement);

                    XmlElement CompanyLinkmanElement = xml.CreateElement("CompanyLinkman");
                    CompanyLinkmanElement.InnerText = PluginInfo.CompanyLinkman;
                    baseInfoElement.AppendChild(CompanyLinkmanElement);

                    XmlElement CompanyTelElement = xml.CreateElement("CompanyTel");
                    CompanyTelElement.InnerText = PluginInfo.CompanyTel;
                    baseInfoElement.AppendChild(CompanyTelElement);

                    XmlElement CompanyHomePageElement = xml.CreateElement("CompanyHomePage");
                    CompanyHomePageElement.InnerText = PluginInfo.CompanyHomePage;
                    baseInfoElement.AppendChild(CompanyHomePageElement);
                }
                #endregion

                #region Plugin


                XmlElement Plugin = xml.CreateElement("Plugin");
                Plugins.AppendChild(Plugin);

                XmlElement NameElement = xml.CreateElement("Name");
                NameElement.InnerText = PluginInfo.DisplayName;
                Plugin.AppendChild(NameElement);
                XmlElement CodeElement = xml.CreateElement("PluginCode");
                CodeElement.InnerText = PluginInfo.PluginCode;
                Plugin.AppendChild(CodeElement);
                XmlElement PluginCateCodeElement = xml.CreateElement("PluginCateCode");
                PluginCateCodeElement.InnerText = PluginInfo.PluginCateCode;
                Plugin.AppendChild(PluginCateCodeElement);
                XmlElement TypeFullNameElement = xml.CreateElement("TypeFullName");
                TypeFullNameElement.InnerText = PluginInfo.TypeFullName;
                Plugin.AppendChild(TypeFullNameElement);
                XmlElement MainFileNameElement = xml.CreateElement("MainFileName");
                MainFileNameElement.InnerText = PluginInfo.FileName;
                Plugin.AppendChild(MainFileNameElement);
                XmlElement DescriptionElement = xml.CreateElement("Description");
                DescriptionElement.InnerText = PluginInfo.PluginSummary;
                Plugin.AppendChild(DescriptionElement);
                XmlElement IsNeedElement = xml.CreateElement("IsNeed");
                IsNeedElement.InnerText = PluginInfo.IsNeed.ToString().ToLower();
                Plugin.AppendChild(IsNeedElement);
                XmlElement FileHashElement = xml.CreateElement("FileHash");
                FileHashElement.InnerText = PluginInfo.HashCode;
                Plugin.AppendChild(FileHashElement);
                XmlElement UrlElement = xml.CreateElement("Url");
                UrlElement.InnerText = PluginInfo.PluginUrl;
                Plugin.AppendChild(UrlElement);

                //IsPublic
                XmlElement IsPublicElement = xml.CreateElement("IsPublic");
                IsPublicElement.InnerText = PluginInfo.IsPublic.ToString();
                Plugin.AppendChild(IsPublicElement);

                #region VersionInfo


                XmlElement VersionElement = xml.CreateElement("VersionCode");
                VersionElement.InnerText = PluginInfo.Version;
                Plugin.AppendChild(VersionElement);

                XmlElement VersionDesElement = xml.CreateElement("VersionDescription");
                VersionDesElement.InnerText = PluginInfo.VersionSummary;
                Plugin.AppendChild(VersionDesElement);

                #endregion

                #region Action
                XmlElement ActionElement = xml.CreateElement("ActionCode");
                ActionElement.InnerText = PluginInfo.ActionCode;
                Plugin.AppendChild(ActionElement);
                XmlElement ActionDescriptionElement = xml.CreateElement("ActionDescription");
                ActionDescriptionElement.InnerText = PluginInfo.ActionSummary;
                Plugin.AppendChild(ActionDescriptionElement);
                #endregion

                #endregion

                #region  Configs
                XmlElement configListElement = xml.CreateElement("Configs");

                //    Plugins.AppendChild(configListElement);
                Plugin.AppendChild(configListElement);
                foreach (var c in PluginInfo.configList)
                {
                    XmlElement configElement = xml.CreateElement("Config");
                    configListElement.AppendChild(configElement);


                    XmlElement keyElement = xml.CreateElement("key");
                    keyElement.InnerText = c.Key1;
                    configElement.AppendChild(keyElement);

                    XmlElement valueElement = xml.CreateElement("value");
                    valueElement.InnerText = c.Value1;
                    configElement.AppendChild(valueElement);

                }
                #endregion

            }


            xml.Save(fileName);
        }


        private static XmlDocument SetXmlByConfigList(IList<ConfigInfo> list)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement configListElement = xml.CreateElement("Configs");

            xml.AppendChild(configListElement);

            foreach (var configInfo in list)
            {
                XmlElement configElement = xml.CreateElement("Config");
                configListElement.AppendChild(configElement);


                XmlElement keyElement = xml.CreateElement("key");
                keyElement.InnerText = configInfo.Key1;
                configElement.AppendChild(keyElement);

                XmlElement valueElement = xml.CreateElement("value");
                valueElement.InnerText = configInfo.Value1;
                configElement.AppendChild(valueElement);

            }

            return xml;
        }

    }
}
