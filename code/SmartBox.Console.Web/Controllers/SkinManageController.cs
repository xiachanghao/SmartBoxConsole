using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;
using SmartBox.Console.Common;
using System.IO;
using SmartBox.Console.Web.Helper;
using System.Xml;

namespace SmartBox.Console.Web.Controllers
{
    public class SkinManageController : Controller
    {
        public ActionResult SkinManage()
        {
            //删除临时文件夹目录||res_temp
            string tempDir = System.IO.Path.Combine(Server.MapPath("/"), "res_temp");
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
            return View();
        }

        public JsonResult UpdateSkin(FormCollection form)
        {           
            //皮肤文件根目录
            string tempSkinRoot = System.IO.Path.Combine(Server.MapPath("/"), @"res_temp\themes");
            //创建解压文件临时文件夹
            string zipTempRoot = System.IO.Path.Combine(System.IO.Path.Combine(Server.MapPath("/"), @"res_temp"), Guid.NewGuid().ToString());
            System.IO.Directory.CreateDirectory(zipTempRoot);
            JsonReturnMessages result = new JsonReturnMessages();
            try
            {
                var smartBoxInfo = SmartBox.Console.Bo.BoFactory.GetVersionTrackBo.GetCurrentSmartBoxInfo();
                //SmartBox根路径
                string smartBoxRootPath = smartBoxInfo.FilePath;//@"D:\WorkPlace\SmartBox2\SmartBox.Console\SmartBox.Console\SmartBox.Console.Web\MainSystem\SmartBox\SmartBox_1"; 
                if (string.IsNullOrEmpty(smartBoxRootPath) || !System.IO.Directory.Exists(smartBoxRootPath))
                    throw new Exception("SmartBox主程序未发布！");
                //1、皮肤根路径
                string skinPubRoot = System.IO.Path.Combine(smartBoxRootPath, @"res\themes");//目的文件夹路径 
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase fb = Request.Files[0];
                    if (fb.InputStream.Length > 3 * 1024 * 1024)
                    {
                        result.IsSuccess = false;
                        result.Msg = "包不能超过3M.";
                    }
                    else
                    {
                        //皮肤标识
                        string skinCode = string.Empty;
                        string fileName = System.IO.Path.GetFileName(fb.FileName);
                        //string fileNameNoExt = System.IO.Path.GetFileNameWithoutExtension(fb.FileName);
                        //临时压缩包文件路径
                        string zipFilePath = System.IO.Path.Combine(zipTempRoot, fileName);
                        //临时文件夹中配置文件路径
                        string skinConfigPath = System.IO.Path.Combine(zipTempRoot, "Theme.xaml");
                        //读取流数据                        
                        Stream inputSteam = fb.InputStream;
                        byte[] fBytes = new byte[inputSteam.Length];
                        inputSteam.Read(fBytes, 0, Convert.ToInt32(inputSteam.Length));
                        inputSteam.Flush();
                        inputSteam.Close();
                        //保存到临时文件夹                                                
                        FileStream fileStream = new FileStream(zipFilePath, FileMode.Create);
                        fileStream.Write(fBytes, 0, fBytes.Length);
                        fileStream.Flush();
                        fileStream.Close();
                        //解压
                        CommonMethods.Uncompress(zipFilePath, zipTempRoot);
                        //删除压缩包
                        System.IO.File.Delete(zipFilePath);
                        //验证配置文件
                        if (!System.IO.File.Exists(skinConfigPath))
                        {                            
                            //删除临时解压文件
                            Directory.Delete(zipTempRoot, true);
                            throw new Exception("压缩包不包含Theme.xaml文件！");
                        }
                        else
                        {
                            //获取皮肤标识
                            StreamReader reader = new StreamReader(skinConfigPath);
                            string xmlStr = reader.ReadToEnd();
                            reader.Close();
                            reader.Dispose();
                            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                            doc.LoadXml(xmlStr);
                            XmlNodeList nodeList = doc.DocumentElement.GetElementsByTagName("sys:String");
                            if (nodeList != null && nodeList.Count > 0)
                            {
                                foreach (XmlNode node in nodeList)
                                {
                                    XmlAttribute keyAttr = (node).Attributes["x:Key"];
                                    if (keyAttr != null && keyAttr.Value.ToLower() == "themekey")
                                    {
                                        skinCode = node.InnerText.Trim();
                                        break;
                                    }
                                }
                            }
                            if (string.IsNullOrEmpty(skinCode))
                            {                                
                                //删除临时解压文件
                                Directory.Delete(zipTempRoot, true);
                                throw new Exception("Theme.xaml配置文件不包含[ThemeKey]！");
                            }
                        }
                        //临时皮肤文件夹
                        string skinTempFolder = System.IO.Path.Combine(tempSkinRoot, skinCode);
                        //验证是否已上传
                        if (System.IO.Directory.Exists(skinTempFolder))
                        {                            
                            //删除临时解压文件
                            Directory.Delete(zipTempRoot, true);
                            throw new Exception("该皮肤已经上传,请先删除后再上传！");
                        }
                        //验证是否已发布
                        //if (System.IO.Directory.Exists(System.IO.Path.Combine(skinPubRoot, skinCode)))
                        //{                            
                        //    //删除临时解压文件
                        //    Directory.Delete(zipTempRoot, true);
                        //    throw new Exception("该皮肤已经发布！");
                        //}
                        //创建皮肤临时文件夹
                        Directory.CreateDirectory(skinTempFolder);
                        Beyondbit.AutoUpdate.FileHelper.MergeCopyDir(zipTempRoot, skinTempFolder);
                        //删除解压临时文件夹
                        Directory.Delete(zipTempRoot, true);
                        result.Data = skinCode;
                        result.IsSuccess = true;
                        result.Msg = "上传成功！";
                    }
                }
                else
                    throw new Exception("请选择上传皮肤！");                
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Msg = "上传失败，"+ex.Message;
            }
            return Json(result, "text/html");
        }

        public JsonResult DeleteSkin(FormCollection form)
        {            
            string tempSkinRoot = System.IO.Path.Combine(Server.MapPath("/"), @"res_temp\themes");
            JsonReturnMessages result = new JsonReturnMessages();            
            try
            {
                string delSkinName = form["SkinName"];
                if (!string.IsNullOrEmpty(delSkinName))
                {
                    string delSkinFolder = System.IO.Path.Combine(tempSkinRoot, delSkinName);
                    if (System.IO.Directory.Exists(delSkinFolder))
                    {
                        System.IO.Directory.Delete(delSkinFolder, true);
                        result.IsSuccess = true;
                        result.Msg = "删除皮肤成功！";
                        result.Data = delSkinName;
                    }
                    else
                    {                        
                        result.Data = delSkinName;
                        throw new Exception("该皮肤还未上传！");
                    }
                }
                else                
                    throw new Exception("皮肤名称为空！");                                    
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Msg ="删除失败，" +ex.Message;
            }
            return Json(result, "text/html");
        }

        public JsonResult PublicSkin(FormCollection form)
        {
            string tempSkinRoot = System.IO.Path.Combine(Server.MapPath("/"), @"res_temp\themes");
            JsonReturnMessages result = new JsonReturnMessages();
            try
            {
                //初始化发布器
                var publicor = new Beyondbit.SmartBox.Publisher.SmartBoxPublisher();
                var smartBoxInfo = SmartBox.Console.Bo.BoFactory.GetVersionTrackBo.GetCurrentSmartBoxInfo();
                //SmartBox根路径
                string smartBoxRootPath = smartBoxInfo.FilePath;  //@"D:\WorkPlace\SmartBox2\SmartBox.Console\SmartBox.Console\SmartBox.Console.Web\MainSystem\SmartBox\SmartBox_1";   
                if (string.IsNullOrEmpty(smartBoxRootPath) || !System.IO.Directory.Exists(smartBoxRootPath))
                    throw new Exception("SmartBox主程序未发布！");
                string skinPubFolder = System.IO.Path.Combine(smartBoxRootPath, @"res\themes");
                if (!System.IO.Directory.Exists(tempSkinRoot) || System.IO.Directory.GetDirectories(tempSkinRoot).Length == 0)
                {
                    throw new Exception("未上传皮肤");                    
                }
                else
                {
                    if (!System.IO.Directory.Exists(skinPubFolder))
                        System.IO.Directory.CreateDirectory(skinPubFolder);
                    Beyondbit.AutoUpdate.FileHelper.MergeCopyDir(tempSkinRoot, skinPubFolder);
                    //删除临时数据
                    Directory.Delete(tempSkinRoot,true);
                    result.IsSuccess = true;
                    result.Msg = "发布成功！";
                }
                //更新SmartBox主程序
                publicor.UpdateApplication(smartBoxRootPath, "smartbox");
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Msg = "发布失败,"+ex.Message;
            }
            return Json(result, "text/html");
        }
    }
}
