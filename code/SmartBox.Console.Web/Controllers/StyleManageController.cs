using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using SmartBox.Console.Common;
using System.IO;
using SmartBox.Console.Bo;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Web.Controllers
{
    public class StyleManageController : Controller
    {
        //
        // GET: /StyleManage/
        private const string _loginTempPath = "res_temp\\login";
        private const string _splashTempPath = "res_temp\\splash";

        public ActionResult StyleManage()
        {
            //删除临时文件夹目录||res_temp
            string tempDir = System.IO.Path.Combine(Server.MapPath("/"), "res_temp");
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);       
            return View();
        }

        public JsonResult UpLoadImage(FormCollection form)
        {
            JsonReturnMessages returnResult = new JsonReturnMessages();
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase image = Request.Files[0];
                    if (image.InputStream.Length > 1024 * 1024)
                    {
                        returnResult.IsSuccess = false;
                        returnResult.Msg = "上传图片不能超过1M.";
                    }
                    else
                    {
                        string witchImage = form["WitchImage"];
                        //读取流数据
                        Stream inputSteam = image.InputStream;
                        byte[] inputByte = new byte[inputSteam.Length];
                        inputSteam.Read(inputByte, 0, Convert.ToInt32(inputSteam.Length));
                        inputSteam.Flush();
                        inputSteam.Close();

                        if ("Top".Equals(witchImage, StringComparison.CurrentCultureIgnoreCase))
                        {
                            string topImagePath = System.IO.Path.Combine(Server.MapPath("/"), _loginTempPath + "\\TopBanner");
                            string loginDir = System.IO.Path.Combine(Server.MapPath("/"), _loginTempPath);
                            if (System.IO.File.Exists(topImagePath))//delete old top banner image
                                System.IO.File.Delete(topImagePath);
                            if (!Directory.Exists(loginDir))// if login directory not exist,create it
                                Directory.CreateDirectory(loginDir);
                            //开始上传
                            System.IO.FileStream topBannerStream = System.IO.File.Open(topImagePath, FileMode.Create);
                            topBannerStream.Write(inputByte, 0, inputByte.Length);
                            topBannerStream.Flush();
                            topBannerStream.Close();
                        }
                        else
                        {
                            string botImagePath = System.IO.Path.Combine(Server.MapPath("/"), _loginTempPath + "\\BottomBanner");
                            string loginDir = System.IO.Path.Combine(Server.MapPath("/"), _loginTempPath);
                            if (System.IO.File.Exists(botImagePath))//delete old top banner image
                                System.IO.File.Delete(botImagePath);
                            if (!Directory.Exists(loginDir))// if login directory not exist,create it
                                Directory.CreateDirectory(loginDir);
                            //开始上传
                            System.IO.FileStream bomBannerStream = System.IO.File.Open(botImagePath, FileMode.Create);
                            bomBannerStream.Write(inputByte, 0, inputByte.Length);
                            bomBannerStream.Flush();
                            bomBannerStream.Close();
                        }
                        returnResult.IsSuccess = true;
                        returnResult.Msg = image.FileName + " -- " + image.InputStream.Length.ToString();
                    }
                }
                else
                {
                    returnResult.IsSuccess = false;
                    returnResult.Msg = "请选择上传图片！";
                }
            }
            catch (Exception ex)
            {
                returnResult.IsSuccess = false;
                returnResult.Msg = ex.ToString();
            }
            return Json(returnResult, "text/html");
        }

        public JsonResult UpLoadSplash(FormCollection form)
        {
            JsonReturnMessages returnResult = new JsonReturnMessages();
            string splashTempPath = System.IO.Path.Combine(Server.MapPath("/"), _splashTempPath);
            string splashZipPath = System.IO.Path.Combine(Server.MapPath("/"), _splashTempPath + "\\splash.zip");
            string splashConfig = System.IO.Path.Combine(Server.MapPath("/"), _splashTempPath + "\\splash.xaml");
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase fb = Request.Files[0];
                    if (fb.InputStream.Length > 3 * 1024 * 1024)
                    {
                        returnResult.IsSuccess = false;
                        returnResult.Msg = "包不能超过3M.";
                    }
                    else
                    {
                        //读取流数据
                        Stream inputSteam = fb.InputStream;
                        byte[] inputByte = new byte[inputSteam.Length];
                        inputSteam.Read(inputByte, 0, Convert.ToInt32(inputSteam.Length));
                        inputSteam.Flush();
                        inputSteam.Close();
                        
                        //删除旧数据
                        if (Directory.Exists(splashTempPath))
                            Directory.Delete(splashTempPath, true);
                        //创建目录
                        Directory.CreateDirectory(splashTempPath);
                        //保存到临时文件夹
                        FileStream splashStream = System.IO.File.Open(splashZipPath, FileMode.Create);
                        splashStream.Write(inputByte, 0, Convert.ToInt32(inputByte.Length));
                        splashStream.Flush();
                        splashStream.Close();
                        //解压到临时文件夹
                        CommonMethods.Uncompress(splashZipPath, splashTempPath);//解压
                        //删除压缩包
                        System.IO.File.Delete(splashZipPath);
                        //验证是否包含xaml文件
                        if (System.IO.File.Exists(splashConfig))
                        {

                            returnResult.IsSuccess = true;
                            returnResult.Msg = "上传成功！";
                        }
                        else
                        {
                            returnResult.IsSuccess = false;
                            returnResult.Msg = "压缩包不包含splash.xaml文件！";
                            //删除上传的脏数据
                            Directory.Delete(splashTempPath, true);
                        }
                    }
                }
                else
                {
                    returnResult.IsSuccess = false;
                    returnResult.Msg = "请上传动画包！";
                }
            }
            catch (Exception ex)
            {
                returnResult.IsSuccess = false;
                returnResult.Msg = ex.ToString();
                //删除上传的脏数据
                Directory.Delete(splashTempPath, true);
            }
            return Json(returnResult, "text/html");
        }

        public JsonResult SaveStyleSetting(FormCollection form)
        {
            JsonReturnMessages returnResult = new JsonReturnMessages();
            try
            {
                var smartBox = BoFactory.GetVersionTrackBo.GetCurrentSmartBoxInfo();
                if (smartBox == null || string.IsNullOrEmpty(smartBox.FilePath))
                {
                    returnResult.IsSuccess = false;
                    returnResult.Msg = "主程序版本信息不存在！";
                }
                else
                {
                    //初始化发布器
                    var publicor = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                    //SmartBox根路径
                    string smartBoxRootPath = smartBox.FilePath;
                    if (string.IsNullOrEmpty(smartBoxRootPath) || !System.IO.Directory.Exists(smartBoxRootPath))
                        throw new Exception("SmartBox主程序未发布！");
                    //1、上传登陆界面图片
                    string loginDestPath = Path.Combine(smartBoxRootPath, "res\\login");//目的文件夹路径
                    //string loginDestPath = Path.Combine(@"D:\WorkPlace\SmartBox2\SmartBox.Console\SmartBox.Console\SmartBox.Console.Web\MainSystem\SmartBox\SmartBox_1", "res\\login");
                    string topImageDestPath = System.IO.Path.Combine(loginDestPath, "TopBanner");//目的顶部图片文件路径
                    string botImageDestPath = System.IO.Path.Combine(loginDestPath, "BottomBanner");//目的底部图片路径
                    string topImageTempPath = System.IO.Path.Combine(Server.MapPath("/"), _loginTempPath + "\\TopBanner");//临时保存的顶部图片地址
                    string botImageTempPath = System.IO.Path.Combine(Server.MapPath("/"), _loginTempPath + "\\BottomBanner");//临时保存的底部图片地址
                    if (!System.IO.Directory.Exists(loginDestPath))
                        System.IO.Directory.CreateDirectory(loginDestPath);
                    if (System.IO.File.Exists(topImageTempPath))
                    {
                        System.IO.File.Copy(topImageTempPath, topImageDestPath, true);
                        System.IO.File.Delete(topImageTempPath);//删除临时文件
                    }
                    if (System.IO.File.Exists(botImageTempPath))
                    {
                        System.IO.File.Copy(botImageTempPath, botImageDestPath, true);
                        System.IO.File.Delete(botImageTempPath);//删除临时文件
                    }
                    //2、上传登陆动画包Splash
                    string splashDestPath = Path.Combine(smartBoxRootPath, "res\\splash");                    
                    //string splashDestPath = Path.Combine(@"D:\WorkPlace\SmartBox2\SmartBox.Console\SmartBox.Console\SmartBox.Console.Web\MainSystem\SmartBox\SmartBox_1", "res\\splash");                                        
                    string splashTempPath = System.IO.Path.Combine(Server.MapPath("/"), _splashTempPath);                                       
                    if (System.IO.Directory.Exists(splashTempPath))
                    {
                        //删除原来目录，创建新的Splash目录
                        if (System.IO.Directory.Exists(splashDestPath))
                            Directory.Delete(splashDestPath, true);
                        Directory.CreateDirectory(splashDestPath);
                        Beyondbit.AutoUpdate.FileHelper.MergeCopyDir(splashTempPath, splashDestPath);
                        Directory.Delete(splashTempPath, true);//删除临时Splash目录
                    }
                    publicor.UpdateApplication(smartBoxRootPath, "smartbox");
                    returnResult.IsSuccess = true;
                    returnResult.Msg = "Success";
                }
            }
            catch (Exception ex)
            {
                returnResult.IsSuccess = false;
                returnResult.Msg = ex.Message;
            }
            return Json(returnResult);
        }

    }
}
