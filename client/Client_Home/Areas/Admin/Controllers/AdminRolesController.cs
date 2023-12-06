using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
using Client_Home.Models;
using ClosedXML.Excel;
using System.Data;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRolesController : Controller
    {
        private readonly ConveniencestoreContext _context;
        public INotyfService _notifyService { get; }
        public AdminRolesController(ConveniencestoreContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminRoles
        public async Task<IActionResult> Index()
        {
              return _context.Roles != null ? 
                          View(await _context.Roles.ToListAsync()) :
                          Problem("Entity set 'ConveniencestoreContext.Roles'  is null.");
        }

        // GET: Admin/AdminRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Roleid == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Admin/AdminRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Roleid,Rolename,Description")] Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role);
                await _context.SaveChangesAsync();
                _notifyService.Success("Tạo mới quyền truy cập thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Admin/AdminRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/AdminRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Roleid,Rolename,Description")] Role role)
        {
            if (id != role.Roleid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.Roleid))
                    {
                        _notifyService.Error("Có lỗi xảy ra vui lòng kiểm tra lại!");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Admin/AdminRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Roleid == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Admin/AdminRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'ConveniencestoreContext.Roles'  is null.");
            }
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
            }
            
            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa quyền truy cập thành công");
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/AdminRoles/ExportToExcel
        public async Task<IActionResult> ExportToExcel()
        {
            
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Roles");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Roleid";
                worksheet.Cell(currentRow, 2).Value = "Rolename";
                worksheet.Cell(currentRow, 3).Value = "Description";

                if (_context.Roles != null)
                {
                    foreach (var role in await _context.Roles.ToListAsync())
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = role.Roleid;
                        worksheet.Cell(currentRow, 2).Value = role.Rolename;
                        worksheet.Cell(currentRow, 3).Value = role.Description;
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    //_notifyService.Success("Đã xuất file thành công");
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Roles.xlsx");
                }
            }

        }
        private bool RoleExists(int id)
        {
          return (_context.Roles?.Any(e => e.Roleid == id)).GetValueOrDefault();
        }
    }
}
