namespace Beyondbit.SingleSignOn.ClientForAspNet20.Properties
{
    using Beyondbit.SingleSignOn.ClientForAspNet20;
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0"), CompilerGenerated]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) SettingsBase.Synchronized(new Settings()));

        [DebuggerNonUserCode, ApplicationScopedSetting, SpecialSetting(SpecialSetting.WebServiceUrl)]
        public string Beyondbit_SingleSignOn_ClientForAspNet20_SSOService_WebService
        {
            get
            {
                return SSOClientConfig.SSOService;
            }
        }

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }
    }
}

