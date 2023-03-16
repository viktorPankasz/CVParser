using CVParser.Extensions;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CVParser.Utils
{
    public static class DocUtils
    {
        //Microsoft.Office.Interop.Word
        public static bool Doc2Text(string fileName)
        {
            if (fileName.IsNullOrEmpty())
                return false;

            if (!File.Exists(fileName))
                return false;

            string docFilePath = FilePathUtils.GetAssemblyPath();
            string txtFileName = Path.Combine(docFilePath, Path.ChangeExtension(fileName, "txt"));
            
            if (File.Exists(txtFileName))
                return true;

            docFilePath = Path.Combine(docFilePath, fileName);

            try
            {
                object missing = Type.Missing;
                Application application = new Application();
                try
                {
                    Document document = application.Documents.Open(docFilePath);
                    //object encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;
                    object noEncodingDialog = true;
                    application.ActiveDocument.SaveAs(txtFileName, WdSaveFormat.wdFormatText,
                        ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing);

                    // close word doc and word app.
                    object saveChanges = WdSaveOptions.wdDoNotSaveChanges;

                    document.Close(ref saveChanges, ref missing, ref missing);
                }
                finally
                {
                    ((_Application)application).Quit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
    }
}
