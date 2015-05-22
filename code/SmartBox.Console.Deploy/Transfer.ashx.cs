using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text;

namespace SmartBox.Console.Deploy
{
    /// <summary>
    /// Transfer 的摘要说明
    /// </summary>
    public class Transfer : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string errMsg="";
            string ReqIP = HttpContext.Current.Request.UserHostAddress;
            string hostname = HttpContext.Current.Request.UserHostName;
            IPHostEntry ip = Dns.GetHostEntry(hostname);   

            context.Response.ContentType = "text/plain";
            var sourpath = context.Request.QueryString["path"];//源文件
            string fileName = Path.GetFileName(sourpath);

            var domain = ConfigurationManager.AppSettings["Domain"].ToString();//目的IP地址
            var port = ConfigurationManager.AppSettings["Port"].ToString();//目的端口
            var destpath = ConfigurationManager.AppSettings["Path"].ToString();//目的文件path
            string account = ConfigurationManager.AppSettings["Account"].ToString();//目标机器账号
            string passWord = ConfigurationManager.AppSettings["Pwd"].ToString();//目标机器密码

            string desFile =@"\\"+ domain + destpath;
                      
            try
            {
                StringBuilder netUseSql = new StringBuilder();
                netUseSql.Append("use");
                netUseSql.Append(desFile);
                netUseSql.Append(" ");
                netUseSql.Append(passWord);
                netUseSql.Append(@"/user:"+account);
                
                System.Diagnostics.Process.Start("net.exe", netUseSql.ToString());

                System.IO.File.Copy(sourpath, desFile + fileName, true);
                               
            }
            catch (Exception ex)
            {
                errMsg = ex.ToString();
            }

            if (errMsg == "")
            {
                context.Response.Write(desFile + fileName + " 传输成功" + '\n' + ReqIP);
            }
            else
            {
                context.Response.Write("传输失败_\n"+errMsg + '\n' + ReqIP);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}