using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Web.UI;
using System.Web;

namespace SmartBox.Console.Common
{
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// Accordions the HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="items">The items.</param>
        /// <param name="headertemplete">The headertemplete.</param>
        /// <param name="itemtemplete">The itemtemplete.</param>
        public static void AccordionHtml(this HtmlHelper html, List<AccordionItem> items, Func<AccordionItem, string> headertemplete, Func<AccordionItem, string> itemtemplete)
        {
            if (items != null)
            {
                StringBuilder buffer = new StringBuilder();
                HtmlTextWriter writer = new HtmlTextWriter(new StringWriter(buffer));
                foreach (AccordionItem item in items)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "accordionheadercontainer");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.Write(headertemplete(item));
                    writer.RenderEndTag();
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "accordionbody");
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                    foreach (AccordionItem leafitem in item.Children)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "accordionitemcontainer");
                        writer.RenderBeginTag(HtmlTextWriterTag.Li);
                        writer.Write(itemtemplete(leafitem));
                        writer.RenderEndTag();
                    }
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                HttpContext.Current.Response.Write(buffer.ToString());
            }
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name)
        {
            return CheckBoxBuilder(name, helper.ViewData[name] as IEnumerable<SelectListItem>,null);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items)
        {
            return CheckBoxBuilder(name, items,null);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items, object htmlAttributes)
        {
            return CheckBoxBuilder(name, items, htmlAttributes);
        }

        private static MvcHtmlString CheckBoxBuilder(string name, IEnumerable<SelectListItem> items,object htmlAttributes)
        {
            var str = new StringBuilder();
            var attrStr = new StringBuilder();
            if (htmlAttributes!=null)
            {
                foreach (var attribute in htmlAttributes.GetType().GetProperties())
                {
                    attrStr.AppendFormat(" {0}=\"{1}\" ",attribute.Name,attribute.GetValue(htmlAttributes,null));
                }
            }
            str.Append(@"<div class=""checkboxlist"">");
            int index = 1;
            foreach (var item in items)
            {
                
                str.Append(@"<div class=""list""><input type=""checkbox"" name=""");
                str.Append(name);
                str.Append("\" value=\"");
                str.Append(item.Value);
                str.Append("\"");
                str.Append(@" id=""");
                str.AppendFormat("{0}_{1}", name, index);
                str.Append("\"");
                if (item.Selected)
                {
                    str.Append(@" checked=""chekced""");
                }
                str.Append(attrStr.ToString());
                str.Append(" />");
                str.AppendFormat("<label for='{0}_{1}'>", name, index);
                str.Append(item.Text);
                str.Append("</label>");
                str.Append("</div>");
                index++;
            }

            str.Append("</div>");

            return MvcHtmlString.Create(str.ToString());
        }
    }
}
