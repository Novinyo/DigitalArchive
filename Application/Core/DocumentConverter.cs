using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Application.Core
{
    public class DocumentConverter: Interfaces.IDocumentConverter
    {
        private readonly IWebHostEnvironment _hostEnv;

        public DocumentConverter(IWebHostEnvironment hostEnv)
        {
            _hostEnv = hostEnv;
        }
        public string LoadBase64String(string fileName, string folderPath)
        {
            byte[] imageArray = File.ReadAllBytes(Path.Combine(folderPath, fileName));

            string base64String = Convert.ToBase64String(imageArray);


            return base64String;
        }
        public string SaveImage(string base64, string schoolCode, string name)
        {
            try
            {
                var fileExtension = base64.IndexOf("png") != -1 ? "png": 
                ((base64.IndexOf("jpeg") != -1 || base64.IndexOf("jpg") != -1) ? "jpg": "");
                
                if(fileExtension == "") return "";

                var ProfilePictures = $"{schoolCode}";
                var path = Path.Combine(_hostEnv.ContentRootPath, "Documents\\Images", schoolCode);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                

                Regex regex=new Regex(@"^[\w/\:.-]+;base64,");
                base64=regex.Replace(base64,string.Empty);

                byte[] bytes = Convert.FromBase64String(base64);
                var filePath = $"{name}.{fileExtension}";
                var imagePath = Path.Combine(path, filePath);
                File.WriteAllBytes(imagePath, bytes);

                return $"{name}.{fileExtension}";
            }
            catch (System.Exception ex)
            {
                var message = ex.Message;
                return "";
            }
        }

        public bool DeleteFile(string fileName, string path)
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