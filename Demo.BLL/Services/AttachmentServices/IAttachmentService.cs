using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.AttachmentServices
{
    public interface IAttachmentService
    {
        // Upload 
        public string? Upload(IFormFile file ,string FolderName);


        // Delete 
        public bool Delete(string filePath);
    }
}
