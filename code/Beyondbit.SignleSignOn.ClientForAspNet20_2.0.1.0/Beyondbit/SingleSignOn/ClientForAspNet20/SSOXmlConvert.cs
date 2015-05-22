namespace Beyondbit.SingleSignOn.ClientForAspNet20
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Xml;

    internal static class SSOXmlConvert
    {
        public static XmlDocument GetXmlDocument(string resourceName)
        {
            XmlDocument document = new XmlDocument();
            using (StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)))
            {
                document.Load(reader);
            }
            return document;
        }

        public static XmlDocument UnpackRequest(string packedText)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(packedText);
            return document;
        }
    }
}

