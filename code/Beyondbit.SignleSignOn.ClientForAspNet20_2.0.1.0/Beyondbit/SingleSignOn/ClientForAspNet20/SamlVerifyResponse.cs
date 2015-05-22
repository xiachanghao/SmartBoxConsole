namespace Beyondbit.SingleSignOn.ClientForAspNet20
{
    using System;
    using System.Xml;

    internal class SamlVerifyResponse
    {
        private Guid appTicket;
        private Guid serviceToken;
        private string userIdentity;
        private Guid userTicket;

        public SamlVerifyResponse(string requestXml)
        {
            XmlDocument doc = SSOXmlConvert.UnpackRequest(requestXml);
            this.ParseRequestXml(doc);
        }

        public SamlVerifyResponse(XmlDocument requestXml)
        {
            this.ParseRequestXml(requestXml);
        }

        private void ParseRequestXml(XmlDocument doc)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");
            nsmgr.AddNamespace("asrt", "urn:oasis:names:tc:SAML:2.0:assertion");
            XmlNode node2 = doc.SelectSingleNode("/samlp:Response", nsmgr).SelectSingleNode("asrt:Assertion", nsmgr);
            this.serviceToken = new Guid(node2.SelectSingleNode("@ID", nsmgr).Value);
            this.appTicket = new Guid(node2.SelectSingleNode("asrt:Subject/asrt:AppTicket", nsmgr).InnerText);
            this.userIdentity = node2.SelectSingleNode("asrt:Subject/asrt:UserIdentity", nsmgr).InnerText;
            this.userTicket = new Guid(node2.SelectSingleNode("asrt:Subject/asrt:UserTicket", nsmgr).InnerText);
        }

        public Guid AppTicket
        {
            get
            {
                return this.appTicket;
            }
        }

        public Guid ServiceToken
        {
            get
            {
                return this.serviceToken;
            }
        }

        public string UserIdentity
        {
            get
            {
                return this.userIdentity;
            }
        }

        public Guid UserTicket
        {
            get
            {
                return this.userTicket;
            }
        }
    }
}

