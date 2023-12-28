using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
using Client_Home.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using ClosedXML.Excel;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        private readonly Data.ConveniencestoreContext _context;
        public INotyfService _notifyService { get; }
        public AdminUsersController(Data.ConveniencestoreContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminUsers
        public async Task<IActionResult> Index()
        {
            ViewData["QuyenTruyCap"] = new SelectList(_context.Roles, "Roleid", "Description");

            List<SelectListItem> status= new List<SelectListItem>();
            status.Add(new SelectListItem() { Text = "Active", Value = "1" });
            status.Add(new SelectListItem() { Text = "Block", Value = "0" });
            ViewData["isStatus"] = status;

            var conveniencestoreContext = _context.Users.Include(a => a.Role);
            return View(await conveniencestoreContext.ToListAsync());
        }

        // GET: Admin/AdminUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/AdminUsers/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid");
            return View();
        }

        // POST: Admin/AdminUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Phone,Email,Password,Salt,Fullname,Roleid,Lastlogin,Createdate")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", user.Roleid);
            return View(user);
        }

        // GET: Admin/AdminUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", user.Roleid);
            return View(user);
        }

        // POST: Admin/AdminUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Phone,Email,Password,Salt,Fullname,Roleid,Lastlogin,Createdate")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!userExists(user.UserId))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", user.Roleid);
            return View(user);
        }

        // GET: Admin/AdminUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/AdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ConveniencestoreContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool userExists(int id)
        {
          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
        // GET: Admin/AdminUsers/ExportToExcel
        public async Task<IActionResult> ExportToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "UserId";
                worksheet.Cell(currentRow, 2).Value = "Fullname";
                worksheet.Cell(currentRow, 3).Value = "Phone";
                worksheet.Cell(currentRow, 4).Value = "Email";
                worksheet.Cell(currentRow, 5).Value = "Password";
                worksheet.Cell(currentRow, 6).Value = "Salt";
                worksheet.Cell(currentRow, 7).Value = "Active";
                worksheet.Cell(currentRow, 8).Value = "Roleid";
                worksheet.Cell(currentRow, 9).Value = "Lastlogin";
                worksheet.Cell(currentRow, 10).Value = "Createdate";

                if (_context.Users != null)
                {
                    foreach (var user in await _context.Users.ToListAsync())
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = user.UserId;
                        worksheet.Cell(currentRow, 2).Value = user.Fullname;
                        worksheet.Cell(currentRow, 3).Value = user.Phone;
                        worksheet.Cell(currentRow, 4).Value = user.Email;
                        worksheet.Cell(currentRow, 5).Value = user.Password;
                        worksheet.Cell(currentRow, 6).Value = user.Salt;
                        worksheet.Cell(currentRow, 7).Value = (XLCellValue)user.Active;
                        worksheet.Cell(currentRow, 8).Value = user.Roleid;
                        worksheet.Cell(currentRow, 9).Value = user.Lastlogin;
                        worksheet.Cell(currentRow, 10).Value = user.Createdate;

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
                        "Users.xlsx");
                }
            }
        }

    }
}
