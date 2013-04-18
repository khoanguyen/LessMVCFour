using dotless.Core.Importers;
using dotless.Core.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessMVCFour
{
    class WebImporter : Importer
    {
        public WebImporter()
            : base()
        {
        }

        public WebImporter(IFileReader reader)
            : base(reader)
        {
        }

        public WebImporter(IFileReader reader,
            bool disableUrlRewriting,
            bool inlineCssFile,
            bool importAllFilesAsLess)
            : base(reader, disableUrlRewriting, inlineCssFile, importAllFilesAsLess)
        {
        }
    }
}
