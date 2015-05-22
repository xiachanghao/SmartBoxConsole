using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace SmartBox.Console.Common
{
    public class ImageResult : ActionResult
    {
        public ImageResult() { }
        public ImageResult(System.IO.MemoryStream image, ImageFormat format) 
        {
            Image = image;
            Format = format;
        }
        public System.IO.MemoryStream Image { get; set; }
        public ImageFormat Format { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {
            if (null == Image)
            {
                throw new ArgumentNullException("image");
            }
            //if (null == Format)
            //{
            //    throw new ArgumentNullException("format");
            //}
            //if (Format.Equals(ImageFormat.Bmp)) context.HttpContext.Response.ContentType = "image/bmp";
            //if (Format.Equals(ImageFormat.Gif)) context.HttpContext.Response.ContentType = "image/gif";
            //if (Format.Equals(ImageFormat.Icon)) context.HttpContext.Response.ContentType = "image/vnd.microsoft.icon";
            //if (Format.Equals(ImageFormat.Jpeg)) context.HttpContext.Response.ContentType = "image/jpeg";
            //if (Format.Equals(ImageFormat.Png)) context.HttpContext.Response.ContentType = "image/png";
            //if (Format.Equals(ImageFormat.Tiff)) context.HttpContext.Response.ContentType = "image/tiff";
            //if (Format.Equals(ImageFormat.Wmf)) context.HttpContext.Response.ContentType = "image/wmf";
            context.HttpContext.Response.ContentType = "image/*";
            context.HttpContext.Response.Clear();
            Image.WriteTo(context.HttpContext.Response.OutputStream);
            Image.Dispose();
        }
    }
}
