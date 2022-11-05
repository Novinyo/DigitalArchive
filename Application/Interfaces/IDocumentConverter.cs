using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDocumentConverter
    {
        public string LoadBase64String(string fileName, string folderPath);

        public string SaveImage(string base64, string folderPath, string name);
        public bool DeleteFile(string fileName, string path);
    }
}