using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using File_Sharing.Viewmodels;
using File_Sharing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace File_Sharing.Controllers
{
    public class AccountController: Controller
    {
        private Models.DataContext contextDb;
        public AccountController(Models.DataContext context)
        {
            contextDb = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel logmodel)
        {
            User user = await contextDb.Users.FirstOrDefaultAsync(x => x.Email == logmodel.Email && x.Password == logmodel.Password);
            if(user != null)
            {
                await Authenticate(logmodel.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(" ", "Invalid username or password");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register (RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = await contextDb.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
                if(newUser == null)
                {
                    contextDb.Add(new User { Email = model.Email, LasttName = model.LastName, Name = model.Name, Password = model.Password });
                    await contextDb.SaveChangesAsync();

                    await Authenticate(model.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(" ", "Invalid username or password");
                }
            }
            return View(model);
        }
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
