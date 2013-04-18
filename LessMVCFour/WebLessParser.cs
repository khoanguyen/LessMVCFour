using dotless.Core;
using dotless.Core.configuration;
using dotless.Core.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LessMVCFour
{
    /// <summary>
    /// LESS Parser for Web environment
    /// </summary>
    public static class WebLessParser
    {
        /// <summary>
        /// Parse the given LESS content
        /// <br/>
        /// In case that the content contains @import directives set the virtual path of the file where the content came from
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileVirtualPath"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string Parse(string content, string fileVirtualPath = null, DotlessConfiguration config = null)
        {
            return GetEngine(fileVirtualPath, config).TransformToCss(content, fileVirtualPath);
        }

        /// <summary>
        /// Get LESS Engine for given virtual path
        /// </summary>
        /// <param name="fileVirtualPath"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static LessEngine GetEngine(string fileVirtualPath = null, DotlessConfiguration config = null)
        {
            config = config ?? DotlessConfiguration.GetDefaultWeb();            
            return new LessEngine(GetParser(fileVirtualPath, config));
        }

        /// <summary>
        /// Construct LESS Parser
        /// </summary>
        /// <param name="file"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static Parser GetParser(string file, DotlessConfiguration config)
        {
            var fullPath = HttpContext.Current.Server.MapPath(file);
            var parser = new Parser();
            var fileReader = new WebFileReader(new WebPathResolver(fullPath));
            var importer = new WebImporter(fileReader, 
                                              config.DisableUrlRewriting,
                                              config.InlineCssFiles,
                                              config.ImportAllFilesAsLess);
            parser.Importer = importer;
            return parser;
        }
    }
}
