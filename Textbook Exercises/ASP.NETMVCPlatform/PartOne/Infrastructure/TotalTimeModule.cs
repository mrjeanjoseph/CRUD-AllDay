using System.IO;
using System.Web;
using System.Web.UI;

namespace PartOne.Infrastructure
{
    public class TotalTimeModule : IHttpModule
    {
        private static float totalTime = 0;
        private static int requestCount = 0;

        public void Init(HttpApplication context)
        {
            IHttpModule module = context.Modules["Timer"];
            if(module != null && module is TimerModule)
            {
                TimerModule timer = (TimerModule)module;
                timer.RequestTimed += (src, args) =>
                {
                    totalTime += args.Duriation;
                    requestCount++;
                };
            }

            context.EndRequest += (src, args) => { context.Context.Response.Write(CreateSummary()); };
        }

        private string CreateSummary()
        {
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "table table-bordered");
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "success");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.Write("Requests");
                    htmlWriter.RenderEndTag();
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.Write(requestCount);
                    htmlWriter.RenderEndTag();
                htmlWriter.RenderEndTag();

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "success");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.Write("Total Time");
                    htmlWriter.RenderEndTag();
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.Write("{0:F5} seconds", totalTime);
                    htmlWriter.RenderEndTag();
                htmlWriter.RenderEndTag();

            htmlWriter.RenderEndTag();
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            // Maybe one day, we will do something here
        }
    }
}