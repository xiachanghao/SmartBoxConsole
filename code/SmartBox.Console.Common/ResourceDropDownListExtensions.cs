using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Beyondbit.Framework.Biz.Resource;

namespace SmartBox.Console.Common
{
    public static class ResourceDropDownListExtensions
    {
        public static MvcHtmlString ResourceDropDownList(
        this HtmlHelper html, string name, IList<IResourceData> resourcedatas, object selectedValue, object htmlAttributes)
        {
            
            SelectList list = new SelectList(resourcedatas, "Code", "Name", selectedValue);
            return html.DropDownList(name, list, htmlAttributes);
        }
    }
}
