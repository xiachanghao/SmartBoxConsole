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
            //ɾ����ʱ�ļ���Ŀ¼||res_temp
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
                        returnResult.Msg = "�ϴ�ͼƬ���ܳ���1M.";
                    }
                    else
                    {
                        string witchImage = form["WitchImage"];
                        //��ȡ������
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
                            //��ʼ�ϴ�
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
                            //��ʼ�ϴ�
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
                    returnResult.Msg = "��ѡ���ϴ�ͼƬ��";
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
                        returnResult.Msg = "�����ܳ���3M.";
                    }
                    else
                    {
                        //��ȡ������
                        Stream inputSteam = fb.InputStream;
                        byte[] inputByte = new byte[inputSteam.Length];
                        inputSteam.Read(inputByte, 0, Convert.ToInt32(inputSteam.Length));
                        inputSteam.Flush();
                        inputSteam.Close();
                        
                        //ɾ��������
                        if (Directory.Exists(splashTempPath))
                            Directory.Delete(splashTempPath, true);
                        //����Ŀ¼
                        Directory.CreateDirectory(splashTempPath);
                        //���浽��ʱ�ļ���
                        FileStream splashStream = System.IO.File.Open(splashZipPath, FileMode.Create);
                        splashStream.Write(inputByte, 0, Convert.ToInt32(inputByte.Length));
                        splashStream.Flush();
                        splashStream.Close();
                        //��ѹ����ʱ�ļ���
                        CommonMethods.Uncompress(splashZipPath, splashTempPath);//��ѹ
                        //ɾ��ѹ����
                        System.IO.File.Delete(splashZipPath);
                        //��֤�Ƿ����xaml�ļ�
                        if (System.IO.File.Exists(splashConfig))
                        {

                            returnResult.IsSuccess = true;
                            returnResult.Msg = "�ϴ��ɹ���";
                        }
                        else
                        {
                            returnResult.IsSuccess = false;
                            returnResult.Msg = "ѹ����������splash.xaml�ļ���";
                            //ɾ���ϴ���������
                            Directory.Delete(splashTempPath, true);
                        }
                    }
                }
                else
                {
                    returnResult.IsSuccess = false;
                    returnResult.Msg = "���ϴ���������";
                }
            }
            catch (Exception ex)
            {
                returnResult.IsSuccess = false;
                returnResult.Msg = ex.ToString();
                //ɾ���ϴ���������
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
                    returnResult.Msg = "������汾��Ϣ�����ڣ�";
                }
                else
                {
                    //��ʼ��������
                    var publicor = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                    //SmartBox��·��
                    string smartBoxRootPath = smartBox.FilePath;
                    if (string.IsNullOrEmpty(smartBoxRootPath) || !System.IO.Directory.Exists(smartBoxRootPath))
                        throw new Exception("SmartBox������δ������");
                    //1���ϴ���½����ͼƬ
                    string loginDestPath = Path.Combine(smartBoxRootPath, "res\\login");//Ŀ���ļ���·��
                    //string loginDestPath = Path.Combine(@"D:\WorkPlace\SmartBox2\SmartBox.Console\SmartBox.Console\SmartBox.Console.Web\MainSystem\SmartBox\SmartBox_1", "res\\login");
                    string topImageDestPath = System.IO.Path.Combine(loginDestPath, "TopBanner");//Ŀ�Ķ���ͼƬ�ļ�·��
                    string botImageDestPath = System.IO.Path.Combine(loginDestPath, "BottomBanner");//Ŀ�ĵײ�ͼƬ·��
                    string topImageTempPath = System.IO.Path.Combine(Server.MapPath("/"), _loginTempPath + "\\TopBanner");//��ʱ����Ķ���ͼƬ��ַ
                    string botImageTempPath = System.IO.Path.Combine(Server.MapPath("/"), _loginTempPath + "\\BottomBanner");//��ʱ����ĵײ�ͼƬ��ַ
                    if (!System.IO.Directory.Exists(loginDestPath))
                        System.IO.Directory.CreateDirectory(loginDestPath);
                    if (System.IO.File.Exists(topImageTempPath))
                    {
                        System.IO.File.Copy(topImageTempPath, topImageDestPath, true);
                        System.IO.File.Delete(topImageTempPath);//ɾ����ʱ�ļ�
                    }
                    if (System.IO.File.Exists(botImageTempPath))
                    {
                        System.IO.File.Copy(botImageTempPath, botImageDestPath, true);
                        System.IO.File.Delete(botImageTempPath);//ɾ����ʱ�ļ�
                    }
                    //2���ϴ���½������Splash
                    string splashDestPath = Path.Combine(smartBoxRootPath, "res\\splash");                    
                    //string splashDestPath = Path.Combine(@"D:\WorkPlace\SmartBox2\SmartBox.Console\SmartBox.Console\SmartBox.Console.Web\MainSystem\SmartBox\SmartBox_1", "res\\splash");                                        
                    string splashTempPath = System.IO.Path.Combine(Server.MapPath("/"), _splashTempPath);                                       
                    if (System.IO.Directory.Exists(splashTempPath))
                    {
                        //ɾ��ԭ��Ŀ¼�������µ�SplashĿ¼
                        if (System.IO.Directory.Exists(splashDestPath))
                            Directory.Delete(splashDestPath, true);
                        Directory.CreateDirectory(splashDestPath);
                        Beyondbit.AutoUpdate.FileHelper.MergeCopyDir(splashTempPath, splashDestPath);
                        Directory.Delete(splashTempPath, true);//ɾ����ʱSplashĿ¼
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
