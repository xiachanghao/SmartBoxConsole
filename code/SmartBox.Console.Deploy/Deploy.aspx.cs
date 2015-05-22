using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

namespace SmartBox.Console.Deploy
{
    public partial class Deploy : System.Web.UI.Page
    {
        protected string content = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            content = Request.ContentLength.ToString();

            bool CanSave = true;

            // 从console端获取的文件名，保存路径，地址
            string saveName = Request.Form["SaveName"].ToString();
            string savePath = Request.Form["SavePath"].ToString();
            string conIp = Request.UserHostAddress;
            //从web.config中获取的保存路径，文件后缀范围，地址范围
            string configExt = ConfigurationManager.AppSettings["deployExt"].ToString();
            string configPath = ConfigurationManager.AppSettings["savePath"].ToString();
            string configIp = ConfigurationManager.AppSettings["IpRange"].ToString();

            if ((savePath.IndexOf(configPath) == -1) || (configExt.IndexOf(Path.GetExtension(saveName)) == -1))
            {
                CanSave = false;
            }
            else if (configIp.IndexOf(conIp) == -1)
            { CanSave = false; }

            //content += "\n" + saveName + "\n" + savePath + "\n" + conIp + "\n" + configExt + "\n" + configPath + "\n" + configIp + "\n" + Path.GetExtension(saveName);
            content += "\n" + Request.Form["SaveName"].ToString();

            if (CanSave)
            {
                byte[] byts = new byte[Request.InputStream.Length];
                Request.InputStream.Read(byts, 0, byts.Length);
                string req = System.Text.Encoding.Default.GetString(byts);
                req = Server.UrlDecode(req);

                int index = req.LastIndexOf("\r\n\r\n") + 4;//文件在流中的起点

                byte[] byteFile = byts.Skip(index - 0).Take(Request.ContentLength - index).ToArray();

                System.IO.File.WriteAllBytes(savePath + saveName, byteFile);


                content += "\n" + byteFile.Length.ToString();
                content += "\n" + conIp;
            }
        }
    }
}