using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartBox.Console.Common;
using SmartBox.Console.Bo;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Web.Controllers
{
    public class ImageManageController : Controller
    {
        public ImageResult ViewImage(string id)
        {
            //System.Drawing.Image image = BoFactory.GetVersionTrackBo.GetImage(id);
            return new ImageResult() { Image = BoFactory.GetVersionTrackBo.GetImage(id) };
        }

        public ActionResult SelectImage()
        {
            ViewData["ImageList"] = BoFactory.GetVersionTrackBo.QueryImageList();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateImage(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg="上传成功" };
            try
            {
                var file = Request.Files[0];
                string fileName = System.IO.Path.GetExtension(file.FileName).ToLower();
                if (!".jpg".Equals(fileName) &&
                    !".jpeg".Equals(fileName) &&
                    !".gif".Equals(fileName) &&
                    !".png".Equals(fileName) && 
                    !".bmp".Equals(fileName))
                {
                    throw new Exception("请上传图片文件");
                }
                BoFactory.GetVersionTrackBo.InsertImage(file.InputStream);
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
            }
            return Json(data, "text/html");
        }
    }
}
