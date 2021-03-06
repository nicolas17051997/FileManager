﻿using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using File_Sharing.Models;
using System.Net.Http;
using File_Sharing.Crypt;
using File_Sharing.Viewmodels;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Extensions;
using File_Sharing.Services;
using File_Sharing.Help;

namespace File_Sharing.Controllers
{
    [Authorize]
    public class HomeController: Controller
    {
        public string Pathh = "";
        private DataContext _dbContext;
        private IWebHostEnvironment _webHostEnvir;
        public HomeController(DataContext dataContext, IWebHostEnvironment webHost)
        {
            _dbContext = dataContext;
            _webHostEnvir = webHost;
        }
        public IActionResult Index()
        {
            return View(_dbContext.FileModels.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id != null)
            {
                FileModel fileModel = await _dbContext.FileModels.FirstOrDefaultAsync(x => x.Id == id);
                if(fileModel != null)
                {
                    _dbContext.FileModels.Remove(fileModel);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            if(file != null && file.Length > 1)
            {
                string path = "/Files/" + file.FileName;
                using (var filestream = new FileStream(_webHostEnvir.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }
                var enctipted = StringCipher.Encrypt(path);
                _dbContext.FileModels.Add(new FileModel { Name = file.FileName, Path = path, UrlFile = enctipted });
                _dbContext.SaveChanges();
                
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return RedirectToAction("Index");
            
        }
        [HttpGet]
        public async Task<IActionResult> Download(string filename)
        {
            if (filename != null)
            {
                var descript = StringCipher.Decrypt(filename);

                var path = "/File_Sharing/File_Sharing/wwwroot" + descript /*"/Files/" + filename*/;
                var memory = new MemoryStream();
                using(var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, GetContentType(path), Path.GetFileName(path));
            }
            else
            {
                return Content("File with this name is not present");
                //return Content(url);
            }
        }

        private static string GetContentType(string path)
        {


            
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        [HttpGet]
        public async Task<IActionResult> SendMailLink (string filename)
        {
            return View(Pathh = filename);
        }
        [HttpPost]
        public async Task<IActionResult> SendMailLink(string emailMargin, string fileModel )
        {
            //string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            string url = (HttpContext.Request.GetDisplayUrl()).Replace("SendMailLink", "Download");

            url += fileModel;
           // var GenarateUrl = GetUrl.GetUri(request);
            EmailServices serviceSender = new EmailServices();
            await serviceSender.SendEmailLinkAsync(emailMargin, "This is your link for download photo",
                $" Pleace enter this link and go to <a href='{url}'>link for download photo</a>");
            return Content("The message was sent to the specified email " + emailMargin);
        }
        //[HttpGet]
        //public async Task<IActionResult> SendMailLink(string filename)
        //{
            
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // удаляем аутентификационные куки
            //await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
       
    }
}
