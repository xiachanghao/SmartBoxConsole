using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using System.Xml.Schema;
using SmartBox.Console.Common;

namespace SmartBox.Console.Web
{
    public partial class _Default : Page
    {
        public void Page_Load(object sender, System.EventArgs e)
        {
            string originalPath = Request.Path;
            HttpContext.Current.RewritePath(Request.ApplicationPath, false);
            IHttpHandler httpHandler = new MvcHttpHandler();
            httpHandler.ProcessRequest(HttpContext.Current);
            HttpContext.Current.RewritePath(originalPath, false);


        }



        private void ValidXml()
        {
            XmlTextReader tr = new XmlTextReader("HeadCount.xml");
            XmlValidatingReader vr = new XmlValidatingReader(tr);
            // XmlReader xr = 
            vr.ValidationType = ValidationType.Schema;
            vr.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);

            while (vr.Read())
            {
                //    PrintTypeInfo(vr);
                if (vr.NodeType == XmlNodeType.Element)
                {
                    // while (vr.MoveToNextAttribute())
                    //     PrintTypeInfo(vr);
                }
            }
            //    Console.WriteLine("Validation finished"); 
        }

        private void ValidationHandler(object sender, ValidationEventArgs e)
        {
            throw new NotImplementedException();
        }
    }


}
