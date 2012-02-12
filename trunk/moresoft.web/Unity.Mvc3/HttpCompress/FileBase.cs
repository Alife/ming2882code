using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Unity.Mvc3.HttpCompress 
{
    public class FileBase : IFileBase
    {
        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    } 
}
