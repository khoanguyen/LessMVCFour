using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Optimization;

namespace LessMVCFour
{
    /// <summary>
    /// LESS Bundle Transform for MVC4 Bundle config
    /// </summary>
    public class WebLessBundleTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            var parsedContent = WebLessParser.Parse(response.Content, context.BundleVirtualPath);
            response.ContentType = MimeMapping.GetMimeMapping(".css");
            response.Content = parsedContent;
        }
    }
}
