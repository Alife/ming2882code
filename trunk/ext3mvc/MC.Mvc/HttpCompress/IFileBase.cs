using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Mvc.HttpCompress
{
    public interface IFileBase
    {
        byte[] ReadAllBytes(string path);
        string ReadAllText(string path); 
    }
}
