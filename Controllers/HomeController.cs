using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FileToFolder.Models;
using Microsoft.AspNetCore.Http;

namespace FileToFolder.Controllers
{
    public class HomeController : Controller
    {
        public const string  MovieFolder = "/data/movies/";
        public IActionResult Index()
        {
            return View(new FileModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
       
        
        [HttpPost("UploadFiles")]
        [DisableRequestSizeLimit]   
        public async Task<IActionResult> UploadFiles(FileModel fileModel)
        {
            long size = fileModel.Files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = MovieFolder + fileModel.FolderName;
            Directory.CreateDirectory(filePath);

            foreach (var formFile in fileModel.Files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath+"/"+formFile.FileName, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = fileModel.Files.Count, size, filePath});
        }
    }
}