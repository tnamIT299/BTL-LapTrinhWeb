//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Client_Home.Data;
//using Client_Home.Models;

//namespace Client_Home.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class ProblemCustomersController : Controller
//    {
//        private readonly Data.ConveniencestoreContext _context;

//        public ProblemCustomersController(Data.ConveniencestoreContext context)
//        {
//            _context = context;
//        }

//        // GET: Admin/ProblemCustomers
//        public async Task<IActionResult> Index()
//        {
//              return _context.ProblemCustomer != null ? 
//                          View(await _context.ProblemCustomer.ToListAsync()) :
//                          Problem("Entity set 'ConveniencestoreContext.ProblemCustomer'  is null.");
//        }

//        // GET: Admin/ProblemCustomers/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.ProblemCustomer == null)
//            {
//                return NotFound();
//            }

//            var problemCustomer = await _context.ProblemCustomer
//                .FirstOrDefaultAsync(m => m.ProblemId == id);
//            if (problemCustomer == null)
//            {
//                return NotFound();
//            }

//            return View(problemCustomer);
//        }

//        // GET: Admin/ProblemCustomers/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Admin/ProblemCustomers/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("ProblemId,CustomerId,Subject,Message,Status")] ProblemCustomer problemCustomer)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(problemCustomer);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(problemCustomer);
//        }

//        // GET: Admin/ProblemCustomers/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.ProblemCustomer == null)
//            {
//                return NotFound();
//            }

//            var problemCustomer = await _context.ProblemCustomer.FindAsync(id);
//            if (problemCustomer == null)
//            {
//                return NotFound();
//            }
//            return View(problemCustomer);
//        }

//        // POST: Admin/ProblemCustomers/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("ProblemId,CustomerId,Subject,Message,Status")] ProblemCustomer problemCustomer)
//        {
//            if (id != problemCustomer.ProblemId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(problemCustomer);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ProblemCustomerExists(problemCustomer.ProblemId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(problemCustomer);
//        }

//        // GET: Admin/ProblemCustomers/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.ProblemCustomer == null)
//            {
//                return NotFound();
//            }

//            var problemCustomer = await _context.ProblemCustomer
//                .FirstOrDefaultAsync(m => m.ProblemId == id);
//            if (problemCustomer == null)
//            {
//                return NotFound();
//            }

//            return View(problemCustomer);
//        }

//        // POST: Admin/ProblemCustomers/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.ProblemCustomer == null)
//            {
//                return Problem("Entity set 'ConveniencestoreContext.ProblemCustomer'  is null.");
//            }
//            var problemCustomer = await _context.ProblemCustomer.FindAsync(id);
//            if (problemCustomer != null)
//            {
//                _context.ProblemCustomer.Remove(problemCustomer);
//            }
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ProblemCustomerExists(int id)
//        {
//          return (_context.ProblemCustomer?.Any(e => e.ProblemId == id)).GetValueOrDefault();
//        }
//    }
//}
