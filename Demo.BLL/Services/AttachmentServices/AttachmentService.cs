using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.AttachmentServices
{
    public class AttachmentService : IAttachmentService
    {

        public string? Upload(IFormFile file, string FolderName)
        {
            List<string> allowedExtension = [".jpg", ".png", ".jpeg"];
            const int maxSize = 5_097_152; // 2 MB
            //1.Check Extension 
            var fileExtension = Path.GetExtension(file.FileName);
            if (!allowedExtension.Contains(fileExtension))
                return null;
            //2.Check Size
            if (file.Length == 0 && file.Length > maxSize)
                return null;
            //3.Get Located Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", FolderName);
            //4.Make Attachment Name Unique-- GUID
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            //5.Get File Path
            var filePath = Path.Combine(folderPath, fileName);
            //6.Create File Stream To Copy File[Unmanaged]
            using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            //7.Use Stream To Copy File
            file.CopyTo(fileStream);
            //8.Return FileName To Store In Database
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if (!File.Exists(filePath))
                return false;
            else
                File.Delete(filePath);
            return true;
        }
    }
}
