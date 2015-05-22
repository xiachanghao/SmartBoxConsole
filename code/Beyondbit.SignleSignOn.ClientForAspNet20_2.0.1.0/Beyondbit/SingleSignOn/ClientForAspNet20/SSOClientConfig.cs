namespace Beyondbit.SingleSignOn.ClientForAspNet20
{
    using System;
    using System.Configuration;

    internal sealed class SSOClientConfig
    {
        public static string[] ExcludePath
        {
            get
            {
                return ConfigurationManager.AppSettings["SSO_Exclude"].Split(new char[] { ',' });
            }
        }

        public static string SignName
        {
            get
            {
                return ConfigurationManager.AppSettings["SSO_SignName"];
            }
        }

        public static string SignOnUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SSO_SignOnUrl"];
            }
        }

        public static string SignOutUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SSO_SignOutUrl"];
            }
        }

        public static string SSOService
        {
            get
            {
                return ConfigurationManager.AppSettings["SSO_Service"];
            }
        }

        public static string UserName
        {
            get
            {
                return "Beyondbit_SSO_NET";
            }
        }

        public static string VerifyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SSO_Service"];
            }
        }
    }
}

