namespace Beyondbit.SingleSignOn.ClientForAspNet20.SSOService
{
    using Beyondbit.SingleSignOn.ClientForAspNet20.Properties;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web.Services;
    using System.Web.Services.Description;
    using System.Web.Services.Protocols;

    [GeneratedCode("System.Web.Services", "2.0.50727.42"), WebServiceBinding(Name="WebServiceSoap", Namespace="http://tempuri.org/"), DebuggerStepThrough, DesignerCategory("code")]
    public class WebService : SoapHttpClientProtocol
    {
        private SendOrPostCallback CheckAuthorizationOperationCompleted;
        private bool useDefaultCredentialsSetExplicitly;

        public event CheckAuthorizationCompletedEventHandler CheckAuthorizationCompleted;

        public WebService()
        {
            this.Url = Settings.Default.Beyondbit_SingleSignOn_ClientForAspNet20_SSOService_WebService;
            if (this.IsLocalFileSystemWebService(this.Url))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        [SoapDocumentMethod("http://tempuri.org/CheckAuthorization", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string CheckAuthorization(string requestXml)
        {
            return (string) base.Invoke("CheckAuthorization", new object[] { requestXml })[0];
        }

        public void CheckAuthorizationAsync(string requestXml)
        {
            this.CheckAuthorizationAsync(requestXml, null);
        }

        public void CheckAuthorizationAsync(string requestXml, object userState)
        {
            if (this.CheckAuthorizationOperationCompleted == null)
            {
                this.CheckAuthorizationOperationCompleted = new SendOrPostCallback(this.OnCheckAuthorizationOperationCompleted);
            }
            base.InvokeAsync("CheckAuthorization", new object[] { requestXml }, this.CheckAuthorizationOperationCompleted, userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if ((url == null) || (url == string.Empty))
            {
                return false;
            }
            Uri uri = new Uri(url);
            return ((uri.Port >= 0x400) && (string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0));
        }

        private void OnCheckAuthorizationOperationCompleted(object arg)
        {
            if (this.CheckAuthorizationCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.CheckAuthorizationCompleted(this, new CheckAuthorizationCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        public string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly) && !this.IsLocalFileSystemWebService(value))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
    }
}

