using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
using Client_Home.Models;
using DocumentFormat.OpenXml.InkML;
using PagedList;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminInvoicesController : Controller
    {
        private readonly ConveniencestoreContext _context;

        public AdminInvoicesController(ConveniencestoreContext context)
        {
            _context = context;
        }

       

        // GET: Admin/AdminInvoices
        public IActionResult Index(int? page)
        {
            return View();
        }


        // GET: Admin/AdminInvoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Employee)
                .Include(i => i.Payment)
                .Include(i => i.Shipping)
                .Include(i => i.InvoiceDetails)
                .ThenInclude(id => id.ProductBatch)
                .ThenInclude(id => id.Product)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Admin/AdminInvoices/Create
        public async Task<IActionResult> Create()
        {
            using (var context = new ConveniencestoreContext())
            {
                var product = await context.Products.FromSqlRaw("EXEC GetProduct").ToListAsync();
                ViewBag.product = product;
            }
            
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Phone");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "LastName");
            ViewData["PaymentId"]  = new SelectList(_context.Payments, "PaymentId", "MethodName");
            ViewData["ShippingId"] = new SelectList(_context.Shippings, "ShippingId", "ShippingId");
            return View();
        }

        // POST: Admin/AdminInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,CustomerId,EmployeeId,PaymentId,ShippingId,TotalAmount,Status,CreatedDate,DeliveryCost")] Invoice invoice)
        {

            if (ModelState.IsValid)
            {
               
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var product = await _context.Products.FromSqlRaw("EXEC GetProduct").ToListAsync();
            ViewBag.product = product;

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", invoice.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "CitizenId", invoice.EmployeeId);
            ViewData["PaymentId"] =  new SelectList(_context.Payments, "PaymentId", "PaymentId", invoice.PaymentId);
            ViewData["ShippingId"] = new SelectList(_context.Shippings, "ShippingId", "ShippingId", invoice.ShippingId);

            return View(invoice);
        }

        // GET: Admin/AdminInvoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", invoice.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "CitizenId", invoice.EmployeeId);
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", invoice.PaymentId);
            ViewData["ShippingId"] = new SelectList(_context.Shippings, "ShippingId", "ShippingId", invoice.ShippingId);
            return View(invoice);
        }

        // POST: Admin/AdminInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,CustomerId,EmployeeId,PaymentId,ShippingId,TotalAmount,Status,CreatedDate,DeliveryCost")] Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", invoice.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "CitizenId", invoice.EmployeeId);
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", invoice.PaymentId);
            ViewData["ShippingId"] = new SelectList(_context.Shippings, "ShippingId", "ShippingId", invoice.ShippingId);
            return View(invoice);
        }

        // GET: Admin/AdminInvoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Employee)
                .Include(i => i.Payment)
                .Include(i => i.Shipping)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Admin/AdminInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'ConveniencestoreContext.Invoices'  is null.");
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
          return (_context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}
