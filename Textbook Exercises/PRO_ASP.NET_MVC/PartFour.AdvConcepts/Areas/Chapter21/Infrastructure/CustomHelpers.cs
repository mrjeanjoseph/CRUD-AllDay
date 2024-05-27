using System;
using System.Web.Mvc;

namespace Chapter21.HelperMethods.Infrastructure
{
    public static class CustomHelpers
    {
        public static MvcHtmlString ListArrayItems(this HtmlHelper html, string[] list)
        {
            TagBuilder tag = new TagBuilder("ul");

            foreach (string str in list)
            {
                TagBuilder itemTag = new TagBuilder("li");
                itemTag.SetInnerText(str);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString DisplayNonSecMessage(this HtmlHelper html, string msg)
        {
            string result = String.Format("This is the message: <p>{0}</p>", msg);
            return new MvcHtmlString(result);
        }

        public static string DisplaySecMessage(this HtmlHelper html, string msg)
        {
            return String.Format("This is the message: <p>{0}</p>", msg);
             
        }

        public static MvcHtmlString DisplayEncMessage(this HtmlHelper html, string msg)
        {
            string result = String.Format("This is the message: <p>{0}</p>", html.Encode(msg));
            return new MvcHtmlString(result);

        }
    }
}