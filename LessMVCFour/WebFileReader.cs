using dotless.Core.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessMVCFour
{
    class WebFileReader : IFileReader
    {
        public IPathResolver PathResolver { get; set; }

        public WebFileReader(IPathResolver resolver)
        {
            this.PathResolver = resolver;
        }

        public bool DoesFileExist(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;
            var path = PathResolver.GetFullPath(fileName);
            return File.Exists(path);
        }

        public byte[] GetBinaryFileContents(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return new byte[0];
            var fullPath = PathResolver.GetFullPath(fileName);
            using (var reader = File.OpenRead(fullPath))
            {
                var length = reader.Length;
                var result = new byte[length];
                reader.Read(result, 0, (int)length);
                return result;
            }
        }

        public string GetFileContents(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return string.Empty;
            var fullPath = PathResolver.GetFullPath(fileName);
            using (var reader = File.OpenText(fullPath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
