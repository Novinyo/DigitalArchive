using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Core
{
    public static class DocumentConverter
    {
        static DocumentConverter()
        {

        }
        public static string LoadBase64String(string fileName, string folderPath)
        {
            byte[] imageArray = File.ReadAllBytes(Path.Combine(folderPath, fileName));

            string base64String = Convert.ToBase64String(imageArray);


            return base64String;
        }
        public static string SaveImage(string base64, string folderPath, string name)
        {
            try
            {
                var fileType = base64.IndexOf("png") != -1 ? "png": 
                ((base64.IndexOf("jpeg") != -1 || base64.IndexOf("jpg") != -1) ? "jpg": "");
                
                if(fileType == "") return "";

                var ProfilePictures = $"{folderPath}";
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                

                Regex regex=new Regex(@"^[\w/\:.-]+;base64,");
                base64=regex.Replace(base64,string.Empty);

                byte[] bytes = Convert.FromBase64String(base64);
                var filePath = $"{folderPath}\\{name}.{fileType}";
                File.WriteAllBytes(filePath, bytes);

                return $"{name}.{fileType}";
            }
            catch (System.Exception ex)
            {
                var message = ex.Message;
                return "";
            }
        }

        public static bool DeleteFile(string fileName, string path)
        {
            try
            {
                if(File.Exists(Path.Combine(path, fileName)))
                    File.Delete(Path.Combine(path, fileName));
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}