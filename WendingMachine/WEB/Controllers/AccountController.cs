using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WEB.Controllers
{
    //Класс для иммитации бд и пользователя пользователя админа
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }

    public class AccountController : Controller
    {
        private User _adminUser = new User("admin", "secret");

        public AccountController()
        {
        }

        public async Task<ActionResult> Login(string? key)
        {
            if (string.IsNullOrWhiteSpace(key) || key != _adminUser.Password)
                return Redirect(Url.Action("Index", "Home"));

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, _adminUser.Name) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Redirect(Url.Action("Index", "Admin"));
        }


        public async Task<ActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
