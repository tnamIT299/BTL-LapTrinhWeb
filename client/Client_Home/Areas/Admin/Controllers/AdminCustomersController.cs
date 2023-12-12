using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
using Client_Home.Models;
using ClosedXML.Excel;
using PagedList;
using AspNetCoreHero.ToastNotification.Abstractions;
using Client_Home.Areas.Admin.Models;
using Client_Home.Areas.DTO.Customers;
using System.Data;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using Org.BouncyCastle.Asn1.X509;
using Client_Home.Areas.Admin.DTO.Customers;
using ConveniencestoreContext = Client_Home.Data.ConveniencestoreContext;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCustomersController : Controller
    {
        private readonly ConveniencestoreContext _context;
        private  IWebHostEnvironment _webHostEnvironment;
        private readonly IAddCusFromExcel _addFromExcel;
        private readonly ILogger<AdminCustomersController> _logger;

        public INotyfService _notifyService { get; }
        public AdminCustomersController(ILogger<AdminCustomersController> logger, Data.ConveniencestoreContext context, INotyfService notifyService,IWebHostEnvironment webHostEnvironment , IAddCusFromExcel addFromExcel)
        {
            _logger = logger;
            _context = context;
            _notifyService = notifyService;
            _webHostEnvironment = webHostEnvironment;
            _addFromExcel = addFromExcel;
        }

        // GET: Admin/AdminCustomers
        public IActionResult Index(int? page)
        {
                return View();
        }

        // GET: Admin/AdminCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/AdminCustomers/
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Phone,Birthday,Gender")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }


        public IActionResult AddFromExcel()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFromExcel(IFormFile formFile)
        {
            string path = _addFromExcel.DoucumentUpload(formFile);
            DataTable dt = _addFromExcel.CustomerDataTable(path);
            _addFromExcel.ImportCustomer(dt);
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Admin/AdminCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FirstName,LastName,Email,Phone,Birthday,RewardPoints,Rank,Gender,UserId")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: Admin/AdminCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admin/AdminCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'ConveniencestoreContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
        // GET: Admin/AdminCustomers/ExportToExcel
        public async Task<IActionResult> ExportToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Customers");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Họ";
                worksheet.Cell(currentRow, 3).Value = "Tên";
                worksheet.Cell(currentRow, 4).Value = "Số điện thoại";
                worksheet.Cell(currentRow, 5).Value = "Email";
                worksheet.Cell(currentRow, 6).Value = "Ngày sinh";
                worksheet.Cell(currentRow, 7).Value = "Điểm thưởng";
                worksheet.Cell(currentRow, 8).Value = "Giới tính";
                worksheet.Cell(currentRow, 9).Value = "ID tài khoản";
                if (_context.Customers != null)
                {
                    foreach (var customer in await _context.Customers.ToListAsync())
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = customer.CustomerId;
                        worksheet.Cell(currentRow, 2).Value = customer.FirstName;
                        worksheet.Cell(currentRow, 3).Value = customer.LastName;
                        worksheet.Cell(currentRow, 4).Value = customer.Phone;
                        worksheet.Cell(currentRow, 5).Value = customer.Email;
                        worksheet.Cell(currentRow, 6).Value = customer.Birthday;
                        worksheet.Cell(currentRow, 7).Value = customer.RewardPoints;
                        worksheet.Cell(currentRow, 8).Value = (XLCellValue)customer.Gender;
                        worksheet.Cell(currentRow, 9).Value = customer.UserId;
                    }
                }
                else
                {
                    _notifyService.Warning("Không có khách hàng :(((");
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    //_notifyService.Success("Đã xuất file thành công");
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Customers.xlsx");
                }
            }
        }

       

    }
}
