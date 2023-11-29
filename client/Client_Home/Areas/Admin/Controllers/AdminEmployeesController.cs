using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
using Client_Home.Models;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Abstractions;
using ClosedXML.Excel;
using Client_Home.Areas.Admin.DTO.Employees;
using Client_Home.Areas.Admin.DTO.Customers;
using System.Data;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminEmployeesController : Controller
    {
        private readonly Client_Home.Data.ConveniencestoreContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IAddEmployFromExcel _addFromExcel;
        private readonly ILogger<AdminEmployeesController> _logger;
        public INotyfService _notifyService { get; }
        public AdminEmployeesController(ILogger<AdminEmployeesController> logger, ConveniencestoreContext context, INotyfService notifyService, IWebHostEnvironment webHostEnvironment, IAddEmployFromExcel addFromExcel)
        {
            _logger = logger;
            _context = context;
            _notifyService = notifyService;
            _webHostEnvironment = webHostEnvironment;
            _addFromExcel = addFromExcel;
        }

        // GET: Admin/AdminEmployees
        public async Task<IActionResult> Index(int?page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var conveniencestoreContext = _context.Employees.Include(e => e.User)
                .AsNoTracking()
                .OrderByDescending(x=>x.EmployeeId);
            ViewBag.CurrentPage = pageNumber;
            PagedList.Core.IPagedList<Employee> model = new PagedList.Core.PagedList<Employee>(conveniencestoreContext, pageNumber, pageSize);

            return View(model);
        }

        // GET: Admin/AdminEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Admin/AdminEmployees/Create
        [HttpGet]
        public IActionResult Create()
        {
         
            return View();
        }

        // POST: Admin/AdminEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,UserId,FirstName,LastName,Position,DateHired,BirthDate,CitizenId,Gender,Email,PhoneNumber")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        public IActionResult AddFromExcel()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFromExcel(IFormFile formFile)
        {
            string path = _addFromExcel.DoucumentUpload(formFile);
            DataTable dt = _addFromExcel.EmployeeDataTable(path);
            _addFromExcel.ImportEmployee(dt);
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", employee.UserId);
            return View(employee);
        }

        // POST: Admin/AdminEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,UserId,FirstName,LastName,Position,DateHired,BirthDate,CitizenId,Gender,Email,PhoneNumber")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", employee.UserId);
            return View(employee);
        }

        // GET: Admin/AdminEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Admin/AdminEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'ConveniencestoreContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminCustomers/ExportToExcel
        public async Task<IActionResult> ExportToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employees");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Họ";
                worksheet.Cell(currentRow, 3).Value = "Tên";
                worksheet.Cell(currentRow, 4).Value = "Ngày sinh";
                worksheet.Cell(currentRow, 5).Value = "Giới tính";
                worksheet.Cell(currentRow, 6).Value = "CCCD";
                worksheet.Cell(currentRow, 7).Value = "Ngày tuyển ";
                worksheet.Cell(currentRow, 8).Value = "Email";
                worksheet.Cell(currentRow, 9).Value = "Số điện thoại";
                worksheet.Cell(currentRow, 10).Value = "ID tài khoản";
                if (_context.Employees != null)
                {
                    foreach (var employees in await _context.Employees.ToListAsync())
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = employees.EmployeeId;
                        worksheet.Cell(currentRow, 2).Value = employees.FirstName;
                        worksheet.Cell(currentRow, 3).Value = employees.LastName;
                        worksheet.Cell(currentRow, 4).Value = employees.BirthDate;
                        worksheet.Cell(currentRow, 5).Value = (XLCellValue)employees.Gender;
                        worksheet.Cell(currentRow, 6).Value = employees.CitizenId;
                        worksheet.Cell(currentRow, 7).Value = employees.DateHired;
                        worksheet.Cell(currentRow, 8).Value = employees.Email;
                        worksheet.Cell(currentRow, 9).Value = employees.PhoneNumber;
                        worksheet.Cell(currentRow, 10).Value = employees.UserId;
                    }
                }
                else
                {
                    _notifyService.Warning("Không có nhân viên :(((");
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    //_notifyService.Success("Đã xuất file thành công");
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Employees.xlsx");
                }
            }
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
