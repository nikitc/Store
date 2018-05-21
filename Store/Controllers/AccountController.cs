using System;
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
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = GetSha256Hash(model.Password);
                var user = await _dataManager.UserRepository.GetAll().FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user != null && user.PasswordHash == passwordHash)
                {
                    await Authenticate(user.Id.ToString()); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, [FromServices] IMailSender mailSender)
        {
            if (ModelState.IsValid)
            {
                var user = await _dataManager.UserRepository.GetAll().FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    
                    var newUser = new User
                    {
                        Login = model.Login,
                        Email = model.Email,
                        PasswordHash = GetSha256Hash(model.Password)
                    };

                    _dataManager.UserRepository.Create(newUser);
                    _dataManager.SaveChanges();
                    SendEmailConfirm(model.Email, newUser.Id, mailSender);
                    await Authenticate(newUser.Id.ToString());

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Пользователь с таким логином уже существует");
            }

            return View(model);
        }

        private void SendEmailConfirm(string email, int userId, IMailSender mailSender)
        {
            var userToken = new UserToken
            {
                CreatingDateTime = DateTime.Now,
                Token = GenerateToken(),
                UserId = userId
            };
            mailSender.SendStandardEmailConfirm(email, userToken.Token);
            _dataManager.UserTokenRepository.Create(userToken);
            _dataManager.SaveChanges();
        }

        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var userToken = await _dataManager.UserTokenRepository.GetAll().FirstOrDefaultAsync(tck => tck.Token == token);
            if (userToken == null)
                return BadRequest();

            userToken.User.IsEmailConfirmed = true;
            _dataManager.UserTokenRepository.Delete(userToken);
            _dataManager.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            if (email == null)
                return View();

            var model = new ResetPasswordModel { Email = email };
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel model, [FromServices] IMailSender emailSender)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _dataManager.UserRepository.GetAll().FirstOrDefault(x => x.Email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Пользователя с таким Email не существует");
                return View(model);
            }
            ResetPswd(user.Id, model.Email, emailSender);

            return Json(new { IsSuccess = true });
        }

        private void ResetPswd(int userId, string email, IMailSender emailSender)
        {
            var token = GenerateToken();
            emailSender.SendStandardEmailReset(email, token);
            var userToken = new UserToken
            {
                CreatingDateTime = DateTime.Now,
                Token = GenerateToken(),
                UserId = userId
            };
            _dataManager.UserTokenRepository.Create(userToken);
            _dataManager.SaveChanges();
        }

        [HttpGet]
        public IActionResult ResetUserPassword(string token)
        {
            if (token == null)
                return BadRequest();
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            throw new NotImplementedException();
        }

        public static string GetSha256Hash(string password)
        {
            var crypt = new SHA256Managed();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Encoding.UTF8.GetBytes(Salt);
            var saltedValue = passwordBytes.Concat(saltBytes).ToArray();
            var crypto = crypt.ComputeHash(saltedValue);

            return crypto.Aggregate("", (current, theByte) => current + theByte.ToString("x2"));
        }

        private string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
