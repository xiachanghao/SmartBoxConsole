using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartBox.Console.Common;
using System.IO;

namespace SmartBox.Console.Web.Helper
{
    public static class AppHelper
    {

        /// <summary>
        /// Builds an Image URL
        /// </summary>
        /// <param name="imageFile">The file name of the image</param>
        public static string ImageUrl(string imageFile)
        {
            return VirtualPathUtility.ToAbsolute("~/Images/" + imageFile);
        }
        /// <summary>
        /// Builds a CSS URL
        /// </summary>
        /// <param name="cssFile">The name of the CSS file</param>
        public static string CssUrl(string cssFile)
        {
            return VirtualPathUtility.ToAbsolute("~/Themes/" + AppConfig.Theme + "/" + cssFile);
        }



        public static string JavascriptSafeString(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            return source.Replace("'", "\\u0027");
        }

        
    }
}
