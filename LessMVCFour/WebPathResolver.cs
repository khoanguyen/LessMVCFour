using dotless.Core.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessMVCFour
{
    class WebPathResolver : IPathResolver
    {
        private string _dirName;

        public WebPathResolver(string currentFile)
        {
            if (!string.IsNullOrWhiteSpace(currentFile)) 
                _dirName = Path.GetDirectoryName(currentFile);
        }

        public string GetFullPath(string path)
        {
            if (_dirName == null) return path;
            return Path.Combine(_dirName, path);
        }
    }
}
