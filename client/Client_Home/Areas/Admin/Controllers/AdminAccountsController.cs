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
    public class AdminAccountsController : Controller
    {
        private readonly ConveniencestoreContext _context;
        public INotyfService _notifyService { get; }
        public AdminAccountsController(ConveniencestoreContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminAccounts
        public async Task<IActionResult> Index()
        {
            ViewData["QuyenTruyCap"] = new SelectList(_context.Roles, "Roleid", "Description");

            List<SelectListItem> status= new List<SelectListItem>();
            status.Add(new SelectListItem() { Text = "Active", Value = "1" });
            status.Add(new SelectListItem() { Text = "Block", Value = "0" });
            ViewData["isStatus"] = status;

            var conveniencestoreContext = _context.Accounts.Include(a => a.Role);
            return View(await conveniencestoreContext.ToListAsync());
        }

        // GET: Admin/AdminAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Accountid == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/AdminAccounts/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid");
            return View();
        }

        // POST: Admin/AdminAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Accountid,Phone,Email,Password,Salt,Fullname,Roleid,Lastlogin,Createdate")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", account.Roleid);
            return View(account);
        }

        // GET: Admin/AdminAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", account.Roleid);
            return View(account);
        }

        // POST: Admin/AdminAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Accountid,Phone,Email,Password,Salt,Fullname,Roleid,Lastlogin,Createdate")] Account account)
        {
            if (id != account.Accountid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Accountid))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", account.Roleid);
            return View(account);
        }

        // GET: Admin/AdminAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Accountid == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/AdminAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'ConveniencestoreContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
          return (_context.Accounts?.Any(e => e.Accountid == id)).GetValueOrDefault();
        }
        // GET: Admin/AdminAccounts/ExportToExcel
        public async Task<IActionResult> ExportToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Accounts");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Accountid";
                worksheet.Cell(currentRow, 2).Value = "Fullname";
                worksheet.Cell(currentRow, 3).Value = "Phone";
                worksheet.Cell(currentRow, 4).Value = "Email";
                worksheet.Cell(currentRow, 5).Value = "Password";
                worksheet.Cell(currentRow, 6).Value = "Salt";
                worksheet.Cell(currentRow, 7).Value = "Active";
                worksheet.Cell(currentRow, 8).Value = "Roleid";
                worksheet.Cell(currentRow, 9).Value = "Lastlogin";
                worksheet.Cell(currentRow, 10).Value = "Createdate";

                if (_context.Accounts != null)
                {
                    foreach (var account in await _context.Accounts.ToListAsync())
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = account.Accountid;
                        worksheet.Cell(currentRow, 2).Value = account.Fullname;
                        worksheet.Cell(currentRow, 3).Value = account.Phone;
                        worksheet.Cell(currentRow, 4).Value = account.Email;
                        worksheet.Cell(currentRow, 5).Value = account.Password;
                        worksheet.Cell(currentRow, 6).Value = account.Salt;
                        worksheet.Cell(currentRow, 7).Value = (XLCellValue)account.Active;
                        worksheet.Cell(currentRow, 8).Value = account.Roleid;
                        worksheet.Cell(currentRow, 9).Value = account.Lastlogin;
                        worksheet.Cell(currentRow, 10).Value = account.Createdate;

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
                        "Accounts.xlsx");
                }
            }
        }

    }
}
