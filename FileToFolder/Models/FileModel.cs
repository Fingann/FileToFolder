using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace FileToFolder.Models
{
    public class FileModel
    {
        public string FolderName { get; set; }
        public List<IFormFile> Files { get; set; }    
    }
}