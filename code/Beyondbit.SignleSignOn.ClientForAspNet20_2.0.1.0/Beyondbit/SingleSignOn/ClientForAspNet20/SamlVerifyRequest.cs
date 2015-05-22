namespace Beyondbit.SingleSignOn.ClientForAspNet20
{
    using System;
    using System.Xml;

    internal class SamlVerifyRequest
    {
        public static XmlDocument CreateRequestXml(Guid appTicket, Guid userTicket)
        {
            XmlDocument xmlDocument = SSOXmlConvert.GetXmlDocument("Beyondbit.SingleSignOn.ClientForAspNet20.SamlRequest.xml");
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");
            XmlNode node = xmlDocument.SelectSingleNode("/samlp:AuthnRequest", nsmgr);
            node.SelectSingleNode("@AppTicket", nsmgr).Value = appTicket.ToString();
            node.SelectSingleNode("@AppIdentity", nsmgr).Value = string.Empty;
            node.SelectSingleNode("@AppName", nsmgr).Value = string.Empty;
            node.SelectSingleNode("@UserTicket", nsmgr).Value = userTicket.ToString();
            return xmlDocument;
        }
    }
}

