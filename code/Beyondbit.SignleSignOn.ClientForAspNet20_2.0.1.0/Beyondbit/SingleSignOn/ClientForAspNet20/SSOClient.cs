namespace Beyondbit.SingleSignOn.ClientForAspNet20
{
    using Beyondbit.SingleSignOn.ClientForAspNet20.SSOService;
    using System;
    using System.Security.Cryptography;
    using System.Web;
    using System.Xml;

    public class SSOClient : IHttpModule
    {
        private void AppBeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            if (((application != null) && this.IsVerifyPage(application)) && string.IsNullOrEmpty(UserUid))
            {
                this.OnAppBeginVerifyUser(application);
                this.VerifyUser(application);
            }
        }

        private Guid CreateGuid()
        {
            byte[] data = new byte[0x10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return new Guid(data);
        }

        public void Dispose()
        {
        }

        private SamlVerifyResponse GetVerifyResponse(Guid appTicket, Guid userTicket, HttpApplication application)
        {
            SamlVerifyResponse response = null;
            XmlDocument document = SamlVerifyRequest.CreateRequestXml(appTicket, userTicket);
            string str = new WebService().CheckAuthorization(document.InnerXml);
            if (!string.IsNullOrEmpty(str))
            {
                response = new SamlVerifyResponse(str);
            }
            return response;
        }

        public void Init(HttpApplication application)
        {
            application.AcquireRequestState += new EventHandler(this.AppBeginRequest);
        }

        protected virtual bool IsVerifyPage(HttpApplication application)
        {
            string[] excludePath = SSOClientConfig.ExcludePath;
            string str = application.Request.Path.ToLower();
            int num = -1;
            if (excludePath != null)
            {
                if (excludePath[0].ToLower() == "true")
                {
                    num = 0;
                    for (int j = excludePath.Length - 1; j > num; j--)
                    {
                        if (!string.IsNullOrEmpty(excludePath[j]))
                        {
                            if ((excludePath[j][0] == '#') && str.Contains(excludePath[j].ToLower().Remove(0, 1)))
                            {
                                return true;
                            }
                            if (str.Contains(excludePath[j].ToLower()))
                            {
                                return false;
                            }
                        }
                    }
                }
                else if (excludePath[0].ToLower() == "false")
                {
                    num = 0;
                    for (int k = excludePath.Length - 1; k > num; k--)
                    {
                        if (!string.IsNullOrEmpty(excludePath[k]))
                        {
                            if ((excludePath[k][0] == '#') && str.Contains(excludePath[k].ToLower().Remove(0, 1)))
                            {
                                return false;
                            }
                            if (str.Contains(excludePath[k].ToLower()))
                            {
                                return true;
                            }
                        }
                    }
                }
                for (int i = excludePath.Length - 1; i > num; i--)
                {
                    if (!string.IsNullOrEmpty(excludePath[i]))
                    {
                        if ((excludePath[i][0] == '#') && str.Contains(excludePath[i].ToLower().Remove(0, 1)))
                        {
                            return false;
                        }
                        if (str.Contains(excludePath[i].ToLower()))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        protected virtual void OnAppBeginVerifyUser(HttpApplication application)
        {
        }

        protected virtual void OnAppEndVerifyUser(HttpApplication application)
        {
        }

        private string RedirectUri(string absoluteUri)
        {
            string[] strArray = absoluteUri.Split(new char[] { '?' });
            if (strArray.Length < 2)
                return absoluteUri;
            string[] strArray2 = strArray[1].Split(new char[] { '&' });
            string str = strArray[0];
            int num = 0;
            for (int i = 0; i < strArray2.Length; i++)
            {
                if (!strArray2[i].Contains("Ticket="))
                {
                    if (num == 0)
                    {
                        num = 1;
                        str = str + "?";
                    }
                    else
                    {
                        str = str + "&";
                    }
                    str = str + strArray2[i];
                }
            }
            return str;
        }

        protected virtual void VerifyUser(HttpApplication application)
        {
            if (string.IsNullOrEmpty(application.Request.QueryString["Ticket"]))
            {
                //application.Response.Redirect(SSOClientConfig.SignOnUrl + "?ActionUrl=" + application.Server.UrlEncode(application.Request.Url.AbsoluteUri), true);
                string site = "";
                System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(application.Request.Url.AbsoluteUri, "http://[^/]+/");
                if (m.Captures != null && m.Captures.Count > 0)
                {
                    site = m.Captures[0].Value;
                }

                application.Response.Redirect(SSOClientConfig.SignOnUrl + "?ActionUrl=" + application.Server.UrlEncode(site), true);
            }
            Guid userTicket = new Guid(application.Request.QueryString["Ticket"]);
            Guid appTicket = this.CreateGuid();
            SamlVerifyResponse response = this.GetVerifyResponse(appTicket, userTicket, application);
            if ((response != null) && (response.AppTicket == appTicket))
            {
                this.UserName = response.UserIdentity;
                this.OnAppEndVerifyUser(application);
                //application.Response.Redirect(this.RedirectUri(application.Request.Url.AbsoluteUri), true);
                //application.Response.Redirect(this.RedirectUri(application.Request.Url.AbsoluteUri.Replace("login.aspx", "Account/Logon?ReturnUrl=%2f")), true);
                System.Web.Security.FormsAuthentication.SetAuthCookie(this.UserName, false);

                string site = "";
                System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(application.Request.Url.AbsoluteUri, "http://[^/]+/");
                if (m.Captures != null && m.Captures.Count > 0)
                {
                    site = m.Captures[0].Value;
                }
                application.Response.Redirect(this.RedirectUri(site), true);
            }
            else
            {
                string url = SSOClientConfig.SignOnUrl + "?ActionUrl=" + application.Server.UrlEncode(application.Request.Url.AbsoluteUri);
                application.Response.Redirect(url, true);
            }
        }

        public static string SignOutUri
        {
            get
            {
                return SSOClientConfig.SignOutUrl;
            }
        }

        private string UserName
        {
            get
            {
                return (HttpContext.Current.Session[SSOClientConfig.UserName] as string);
            }
            set
            {
                if (HttpContext.Current.Session[SSOClientConfig.UserName] != null)
                {
                    HttpContext.Current.Session[SSOClientConfig.UserName] = value;
                }
                else
                {
                    HttpContext.Current.Session.Add(SSOClientConfig.UserName, value);
                }
            }
        }

        public static string UserUid
        {
            get
            {
                return (HttpContext.Current.Session[SSOClientConfig.UserName] as string);
            }
        }
    }
}

