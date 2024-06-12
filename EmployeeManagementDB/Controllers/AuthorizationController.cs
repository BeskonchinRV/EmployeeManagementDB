using EmployeeManagementDB.Models.DBcontext;
using EmployeeManagementDB.Models.DBModels;
using EmployeeManagementDB.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using System.Data.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace EmployeeManagementDB.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public AuthorizationController(EmployeeManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizationAsync(UserAccount model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext.UserAccounts.FirstOrDefault(u => u.Login == model.Login);
                if (user != null)
                {
                    var hashpass = HashPassword(model.Password!);
                    if (hashpass == user.Password)
                    {
                        var role = _dbContext.Roles.FirstOrDefault(q => q.RoleId == user.RoleId);
                        if (role != null)
                        {
                            user.Role = role;
                            await Authenticate(user);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ViewBag.ErrorMessage = "Неверный логин или пароль";
                return View(model);
            }
            return View(model);
        }

        private async Task Authenticate(UserAccount user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
                new Claim(ClaimTypes.NameIdentifier, user.AccountId.ToString()),
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [HttpGet]
        public IActionResult AuthorizationTrue(Employee employee)
        {
            return View(employee);
        }
    }
}
