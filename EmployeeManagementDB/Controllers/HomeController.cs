using EmployeeManagementDB.Models.DBcontext;
using EmployeeManagementDB.Models.DBModels;
using EmployeeManagementDB.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagementDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public HomeController(EmployeeManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Получаем текущий идентификатор пользователя
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Получаем данные учетной записи пользователя вместе с связанными данными
            var userAccount = await _dbContext.UserAccounts
                .Include(u => u.Employee)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.AccountId == int.Parse(userId));

            if (userAccount == null)
            {
                return NotFound();
            }

            // Создаем и заполняем модель представления
            var viewModel = new HomeViewModel
            {
                FullName = userAccount.Employee.FullName,
                DateOfBirth = userAccount.Employee.DateOfBirth,
                Status = userAccount.Employee.Status,
                RoleName = userAccount.Role?.Name,
                Login = userAccount.Login
            };

            return View(viewModel);
        }
    }
}

