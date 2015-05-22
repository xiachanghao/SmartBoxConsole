using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SmartBox.Console.Common
{
    //public delegate System.Drawing.Bitmap GetDimensionalCodeFunc(string link);
    //public delegate string GetAndroidApplicationIcoUriFunc(string packagePath, string innerAppIco);
    public interface ICreateApplicationPackage
    {
        JsonReturnMessages CreateApplicationPackage(FormCollection form, string UserUID, string TEMPPATH, string SAVEPATH, string SAVEOUTPATH, int Request_Files_Count, System.Web.HttpFileCollectionBase files);
    }
}
