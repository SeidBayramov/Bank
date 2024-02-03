using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Helpers
{
    public static class FileManager
    {
        public static bool CheckImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/") && file.Length / 1024 / 1024 <= 3;
        }

        public static string Upload(this IFormFile file, string webPath, string folderPath)
        {
            if (!Directory.Exists(webPath + folderPath))
            {
                Directory.CreateDirectory(webPath + folderPath);
            }

            string fileName = Guid.NewGuid().ToString() + file.FileName;

            string filePath = webPath + folderPath + fileName;

            using (FileStream st = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(st);
            }

            return fileName;
        }
        public static void Delete(string fileName, string webPath, string folderName)
        {
            if (File.Exists(folderName + webPath + fileName))
            {
                File.Delete(folderName + webPath);
            }
        }
    }
}