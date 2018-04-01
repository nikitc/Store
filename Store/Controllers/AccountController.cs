using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Store.Database.Entities;
using Store.Models.Account;
using Store.Services;

namespace Store.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDataManager _dataManager;
        private const string Salt = "sflprt49fhi2";

        public AccountController(IDataManager datamanager)
        {
            _dataManager = datamanager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = Sha256Hash(model.Password);
                var user = await _dataManager.UserRepository.GetAll().FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null && user.PasswordHash == passwordHash)
                {
                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _dataManager.UserRepository.GetAll().FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    var newUser = new User
                    {
                        Email = model.Email,
                        PasswordHash = Sha256Hash(model.Password)
                    };

                    _dataManager.UserRepository.Create(newUser);
                    _dataManager.SaveChanges();

                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


        public string Sha256Hash(string password)
        {
            var crypt = new SHA256Managed();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Encoding.UTF8.GetBytes(Salt);
            var saltedValue = passwordBytes.Concat(saltBytes).ToArray();
            var crypto = crypt.ComputeHash(saltedValue);

            return crypto.Aggregate("", (current, theByte) => current + theByte.ToString("x2"));
        }
    }
}
