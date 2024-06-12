using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementDB.Models.DBModels;
using EmployeeManagementDB.Models.DBcontext;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagementDB.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public EmployeesController(EmployeeManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Employees.ToListAsync());
        }
        [Authorize(Roles = "Администратор")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FullName,Gender,DateOfBirth,Identifier,ContactInformation,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var maxEmployeeId = _dbContext.Employees.Max(e => (int?)e.EmployeeId) ?? 0;
                employee.EmployeeId = maxEmployeeId + 1;

                _dbContext.Add(employee);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FullName,Gender,DateOfBirth,Identifier,ContactInformation,Status")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(employee);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            return View(employee);
        }
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _dbContext.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var employee = await _dbContext.Employees.FindAsync(id);
                    if (employee == null)
                    {
                        return NotFound();
                    }

                    var userAccount = await _dbContext.UserAccounts
                        .FirstOrDefaultAsync(u => u.EmployeeId == employee.EmployeeId);

                    if (userAccount != null)
                    {
                        userAccount.EmployeeId = null;
                        _dbContext.UserAccounts.Update(userAccount);
                    }

                    _dbContext.Employees.Remove(employee);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка при удалении данных");
                }
            }
        }

        private bool EmployeeExists(int id)
        {
            return _dbContext.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
