using EmployeeManagementDB.Models.DBcontext;
using EmployeeManagementDB.Models.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagementDB.Controllers
{
    public class UserAccountsController : Controller
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public UserAccountsController(EmployeeManagementDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Index()
        {
            var employeeManagementDbContext = _dbContext.UserAccounts.Include(u => u.Employee).Include(u => u.Role);
            return View(await employeeManagementDbContext.ToListAsync());
        }
        [Authorize(Roles = "Администратор")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _dbContext.UserAccounts.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }
            var employees = _dbContext.Employees.ToList();
            ViewBag.Employees = employees;

            var roles = _dbContext.Roles.Select(r => new SelectListItem
            {
                Value = r.RoleId.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Roles = roles;
            return View(userAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,EmployeeId,Login,RoleId,Status")] UserAccount userAccount)
        {
            if (id != userAccount.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _dbContext.UserAccounts.AsNoTracking().FirstOrDefaultAsync(u => u.AccountId == id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    var userWithSameLogin = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Login == userAccount.Login && u.AccountId != id);
                    if (userWithSameLogin != null)
                    {
                        ModelState.AddModelError("Login", "Учетная запись с таким логином уже существует.");

                        // Повторное заполнение ViewBag.Roles
                        var roles = _dbContext.Roles.Select(r => new SelectListItem
                        {
                            Value = r.RoleId.ToString(),
                            Text = r.Name
                        }).ToList();
                        ViewBag.Roles = roles;

                        // Повторное заполнение ViewBag.Employees
                        var employees = _dbContext.Employees.ToList();
                        ViewBag.Employees = employees;

                        return View(userAccount);
                    }

                    // Если логин изменился, обновить LastLogin
                    if (existingUser.Login != userAccount.Login)
                    {
                        userAccount.LastLogin = DateTime.Now;
                    }
                    else
                    {
                        userAccount.LastLogin = existingUser.LastLogin;
                    }

                    _dbContext.Update(userAccount);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccountExists(userAccount.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(userAccount);
        }
        [Authorize(Roles = "Администратор")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _dbContext.UserAccounts
                .Include(u => u.Employee)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccount = await _dbContext.UserAccounts.FindAsync(id);
            if (userAccount != null)
            {
                _dbContext.UserAccounts.Remove(userAccount);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(int id)
        {
            return _dbContext.UserAccounts.Any(e => e.AccountId == id);
        }
        [Authorize(Roles = "Администратор")]
        [HttpGet]
        public IActionResult CreateUserAccount()
        {
            var employees = _dbContext.Employees.ToList();
            ViewBag.Employees = employees;

            var roles = _dbContext.Roles.Select(r => new SelectListItem
            {
                Value = r.RoleId.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAccount(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Login == userAccount.Login);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Login", "Учетная запись с таким логином уже существует.");

                    var roles = _dbContext.Roles.Select(r => new SelectListItem
                    {
                        Value = r.RoleId.ToString(),
                        Text = r.Name
                    }).ToList();
                    ViewBag.Roles = roles;

                    var employees = _dbContext.Employees.ToList();
                    ViewBag.Employees = employees;

                    return View(userAccount);
                }
                userAccount.LastLogin = DateTime.Now;

                // Хеширование пароля перед сохранением
                userAccount.Password = HashPassword(userAccount.Password);

                if (userAccount.RoleId != null)
                {
                    var role = _dbContext.Roles.FirstOrDefault(q => q.RoleId == userAccount.RoleId);
                    if (role != null)
                    {
                        userAccount.Role = role;
                    }
                    else
                    {
                        throw new Exception($"Ошибка создания роли, роль {userAccount.RoleId} не существует");
                    }
                }

                if (userAccount.EmployeeId != null)
                {
                    userAccount.Employee = await _dbContext.Employees.FindAsync(userAccount.EmployeeId);
                }

                // Найти максимальный существующий AccountId
                var maxAccountId = _dbContext.UserAccounts.Max(u => (int?)u.AccountId) ?? 0;
                userAccount.AccountId = maxAccountId + 1;

                _dbContext.UserAccounts.Add(userAccount);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(userAccount);
        }
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Преобразование пароля в байтовый массив
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Хеширование пароля
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Преобразование хэша в строку
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // Преобразование в шестнадцатеричную строку
                }
                return builder.ToString();
            }
        }
    }
}
