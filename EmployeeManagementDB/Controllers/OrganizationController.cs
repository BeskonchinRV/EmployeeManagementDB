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
    public class OrganizationsController : Controller
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public OrganizationsController(EmployeeManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var employeeManagementDbContext = _dbContext.Organizations;
            return View(await employeeManagementDbContext.ToListAsync());
        }
        [Authorize(Roles = "Администратор")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Inn, Ogrn, Status")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                var maxOrganizationId = _dbContext.Organizations.Max(o => (int?)o.OrganizationId) ?? 0;
                organization.OrganizationId = maxOrganizationId + 1;

                _dbContext.Add(organization);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organization);
        }
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _dbContext.Organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            return View(organization);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrganizationId,Name,Inn,Ogrn,Status")] Organization organization)
        {
            if (id != organization.OrganizationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(organization);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationExists(organization.OrganizationId))
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
            return View(organization);
        }
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _dbContext.Organizations
                .FirstOrDefaultAsync(m => m.OrganizationId == id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organization = await _dbContext.Organizations.FindAsync(id);
            if (organization != null)
            {
                _dbContext.Organizations.Remove(organization);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizationExists(int id)
        {
            return _dbContext.Organizations.Any(e => e.OrganizationId == id);
        }
    }
}
