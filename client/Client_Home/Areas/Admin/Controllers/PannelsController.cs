using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
using Client_Home.Models;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PannelsController : Controller
    {
        private readonly Data.ConveniencestoreContext _context;

        public PannelsController(Data.ConveniencestoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Pannels
        public async Task<IActionResult> Index()
        {
              return _context.Pannels != null ? 
                          View(await _context.Pannels.ToListAsync()) :
                          Problem("Entity set 'ConveniencestoreContext.Pannels'  is null.");
        }

        // GET: Admin/Pannels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pannels == null)
            {
                return NotFound();
            }

            var pannel = await _context.Pannels
                .FirstOrDefaultAsync(m => m.IdPannel == id);
            if (pannel == null)
            {
                return NotFound();
            }

            return View(pannel);
        }

        // GET: Admin/Pannels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Pannels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPannel,NamePannel,UrlPannel")] Pannel pannel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pannel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pannel);
        }

        // GET: Admin/Pannels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pannels == null)
            {
                return NotFound();
            }

            var pannel = await _context.Pannels.FindAsync(id);
            if (pannel == null)
            {
                return NotFound();
            }
            return View(pannel);
        }

        // POST: Admin/Pannels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPannel,NamePannel,UrlPannel")] Pannel pannel)
        {
            if (id != pannel.IdPannel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pannel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PannelExists(pannel.IdPannel))
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
            return View(pannel);
        }

        // GET: Admin/Pannels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pannels == null)
            {
                return NotFound();
            }

            var pannel = await _context.Pannels
                .FirstOrDefaultAsync(m => m.IdPannel == id);
            if (pannel == null)
            {
                return NotFound();
            }

            return View(pannel);
        }

        // POST: Admin/Pannels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pannels == null)
            {
                return Problem("Entity set 'ConveniencestoreContext.Pannels'  is null.");
            }
            var pannel = await _context.Pannels.FindAsync(id);
            if (pannel != null)
            {
                _context.Pannels.Remove(pannel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PannelExists(int id)
        {
          return (_context.Pannels?.Any(e => e.IdPannel == id)).GetValueOrDefault();
        }
    }
}
