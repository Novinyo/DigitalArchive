using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Core
{
    public static class DocumentConverter
    {
        static DocumentConverter()
        {

        }
        public static string LoadImage(string base64, string folderPath, string name)
        {
            try
            {
                var fileType = base64.IndexOf("png") != -1 ? "png": 
                ((base64.IndexOf("jpeg") != -1 || base64.IndexOf("jpg") != -1) ? "jpg": "");
                
                if(fileType == "") return "";

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                byte[] bytes = Convert.FromBase64String(base64);
                var filePath = $"{folderPath}\\ProfilePictures\\{name}.{fileType}";
                File.WriteAllBytes(filePath, bytes);

                return filePath;
            }
            catch (System.Exception)
            {
                return "";
            }
        }
    }
}