using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseReservation.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace DatabaseReservation.Controllers
{
    [Authorize(Roles ="manager")]
    public class SittingsController : Controller
    {
        private readonly ReservationDbContext _context;

        public SittingsController(ReservationDbContext context)
        {
            _context = context;
        }

        // GET: Sittings
        public async Task<IActionResult> Index(string SearchString)
        {
             if(_context.Sittings== null ) {
                return Problem("Entity set 'ReservationDbContext.Sittings'  is null."); 
            }
            var sits = _context.Sittings.ToList();           
            
            Debug.WriteLine(SearchString);
            if (!String.IsNullOrEmpty(SearchString))
            {
                sits = sits.Where(s => s.Description!.Contains(SearchString)).ToList();
            }
            return View(sits);

        }

        // GET: Sittings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sittings == null)
            {
                return NotFound();
            }

            var sitting = await _context.Sittings
                .FirstOrDefaultAsync(m => m.SittingId == id);
            if (sitting == null)
            {
                return NotFound();
            }

            return View(sitting);
        }

        // GET: Sittings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sittings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SittingId,SittingType,StartDateTime,EndDateTime,Capacity")] Sitting sitting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sitting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sitting);
        }

        // GET: Sittings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sittings == null)
            {
                return NotFound();
            }

            var sitting = await _context.Sittings.FindAsync(id);
            if (sitting == null)
            {
                return NotFound();
            }
            return View(sitting);
        }

        // POST: Sittings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SittingId,SittingType,StartDateTime,EndDateTime,Capacity")] Sitting sitting)
        {
            if (id != sitting.SittingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sitting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SittingExists(sitting.SittingId))
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
            return View(sitting);
        }

        // GET: Sittings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sittings == null)
            {
                return NotFound();
            }

            var sitting = await _context.Sittings
                .FirstOrDefaultAsync(m => m.SittingId == id);
            if (sitting == null)
            {
                return NotFound();
            }

            return View(sitting);
        }

        // POST: Sittings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sittings == null)
            {
                return Problem("Entity set 'ReservationDbContext.Sittings'  is null.");
            }
            var sitting = await _context.Sittings.FindAsync(id);
            if (sitting != null)
            {
                _context.Sittings.Remove(sitting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SittingExists(int id)
        {
          return (_context.Sittings?.Any(e => e.SittingId == id)).GetValueOrDefault();
        }
    }
}
