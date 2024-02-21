using System;
using System.Web.Mvc;

namespace Ch21_HelperMethods.Infrastructure {
    public static class CustomHelpers {
        public static MvcHtmlString ListArrayItems(this HtmlHelper html, string[] list) {
            TagBuilder tag = new TagBuilder("ul");

            foreach(string str in list) {
                TagBuilder itemTag = new TagBuilder("li");
                itemTag.SetInnerText(str);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }


        public static MvcHtmlString DisplayMessage(this HtmlHelper html, string message) {
            return new MvcHtmlString(string.Format("This is the message from HTML Helper Method Encoded: <p>{0}</p>", html.Encode(message)));
            //string encodedMessage = html.Encode(message);
            //string result = string.Format("This is the message from HTML Helper Method Encoded: <p>{0}</p>", encodedMessage);
            //return new MvcHtmlString(result);
        }

        public static string DisplayMessageOld2(this HtmlHelper html, string message) {
            return String.Format("This is the message from HTML Helper Updated: <p>{0}</p>", message);
        }

        public static MvcHtmlString DisplayMessageOld(this HtmlHelper html, string message) {
            string result = string.Format("This is the message from HTML Helper: <p>{0}</p>", message);
            return new MvcHtmlString(result);
        }
    }
}