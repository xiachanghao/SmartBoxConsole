using Beyondbit.Push.Service.Bo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Beyondbit.Push.Service
{
    /// <summary>
    /// config 的摘要说明
    /// </summary>
    public class config : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request.QueryString["m"];
            context.Response.ContentEncoding = new UTF8Encoding(false);
            context.Response.ContentType = "application/json";

            object resp = null;

            if ("QueryChannels".Equals(method, StringComparison.CurrentCultureIgnoreCase))
            {
                resp = PushBo.Instance.QueryDeletedChannels(false);
            }
            else if ("SaveChannel".Equals(method, StringComparison.CurrentCultureIgnoreCase))
            {
                SaveChannel(context);
            }
            else if ("UpdateChannelState".Equals(method, StringComparison.CurrentCultureIgnoreCase))
            {
                PushBo.Instance.UpdateChannelState(Convert.ToInt32(context.Request.Form["Id"]), Convert.ToInt32(context.Request.Form["State"]));
                PushBrokerManager.Restart();
            }
            else if ("TestChannel".Equals(method, StringComparison.CurrentCultureIgnoreCase))
            {
                TestChannel(context);
            }
            else if ("QueryLogs".Equals(method, StringComparison.CurrentCultureIgnoreCase))
            {
                resp = PushBo.Instance.QueryLogs(Convert.ToInt32(context.Request.Form["ChannelId"]), context.Request.Form["Level"]);
            }

            if (resp != null)
            {
                string s = JsonConvert.SerializeObject(resp, Formatting.Indented);
                context.Response.Write(s);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void SaveChannel(HttpContext context)
        {
            var form = context.Request.Form;
            var channel = new Channel();
            string idStr = form["Id"];
            if (!string.IsNullOrEmpty(idStr))
                channel.Id = Convert.ToInt32(idStr);
            channel.Title = form["Title"];
            channel.ApplicationId = form["ApplicationId"];
            channel.PlatformType = (PlatformType)Enum.Parse(typeof(PlatformType), form["PlatformType"], true);
            channel.CertPassword = form["CertPassword"];
            channel.State = 1;

            var cert = context.Request.Files["Cert"];
            string certName = form["CertName"];
            if (cert == null || string.IsNullOrEmpty(cert.FileName))
            {
                PushBo.Instance.SaveChannel(channel, channel.Id == 0 || string.IsNullOrEmpty(certName));
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    byte[] bytes = new byte[cert.InputStream.Length];
                    cert.InputStream.Read(bytes, 0, bytes.Length);
                    cert.InputStream.Seek(0, SeekOrigin.Begin);
                    //cert.InputStream.CopyTo(ms);
                    channel.Cert = bytes;
                }
                channel.CertName = cert.FileName;
                PushBo.Instance.SaveChannel(channel, true);
            }
        }

        private void TestChannel(HttpContext context)
        {
            int channelId = Convert.ToInt32(context.Request.Form["ChannelId"]);
            string uids = context.Request.Form["Uids"];
            var svc = new PushService();
            var payload = new Payload();
            payload.Id = context.Request.Form["Id"];
            payload.Alert = context.Request.Form["Alert"];
            payload.Custom = context.Request.Form["Custom"];

            try
            {
                payload.Badge = Convert.ToInt32(context.Request.Form["Badge"]);
            }
            catch (FormatException)
            {
            }
            payload.Sound = context.Request.Form["Sound"];

            foreach (string uid in uids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                foreach (var subscription in PushBo.Instance.QueryDeviceSubscriptions(new List<int> { channelId }, uid))
                {
                    svc.Push(subscription, payload);
                }
            }
        }
    }
}