using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LessMVCFour
{
    /// <summary>
    /// LESS HttpHandler for handling .less HTTPRequest
    /// </summary>
    public class LessHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetLastModifiedFromFileDependencies();
            context.Response.Cache.SetETagFromFileDependencies();
            var file = context.Server.MapPath(context.Request.AppRelativeCurrentExecutionFilePath);
            using (var reader = File.OpenText(file))
            {
                var content = WebLessParser.Parse(reader.ReadToEnd(),
                    context.Request.AppRelativeCurrentExecutionFilePath);
                context.Response.AddHeader("Content-Type", MimeMapping.GetMimeMapping(".css"));
                context.Response.Write(content);
                context.Response.End();
            }
        }
    }
}
