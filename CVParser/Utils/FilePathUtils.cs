using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CVParser.Utils
{
    public static class FilePathUtils
    {
        public static string GetAssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "");
        }
    }
}
