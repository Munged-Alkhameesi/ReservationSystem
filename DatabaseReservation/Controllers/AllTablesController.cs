using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseReservation.Models;
using Microsoft.AspNetCore.Authorization;

namespace DatabaseReservation.Controllers
{
    [Authorize(Roles ="manager,staff")]
    public class AllTablesController : Controller
    {
        private readonly ReservationDbContext _context;

        public AllTablesController(ReservationDbContext context)
        {
            _context = context;
        }

        // GET: AllTables
        public async Task<IActionResult> Index()
        {
            var reservationDbContext = _context.AllTables.Include(a => a.Area);
            return View(await reservationDbContext.ToListAsync());
        }

        // GET: AllTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AllTables == null)
            {
                return NotFound();
            }

            var allTable = await _context.AllTables
                .Include(a => a.Area)
                .FirstOrDefaultAsync(m => m.TableId == id);
            if (allTable == null)
            {
                return NotFound();
            }

            return View(allTable);
        }
        [Authorize(Roles = "manager")]
        // GET: AllTables/Create
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.Areas, "AreaId", "AreaId");
            return View();
        }

        // POST: AllTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TableId,TableName,AreaId")] AllTable allTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "AreaId", "AreaId", allTable.AreaId);
            return View(allTable);
        }
        [Authorize(Roles = "manager")]
        // GET: AllTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AllTables == null)
            {
                return NotFound();
            }

            var allTable = await _context.AllTables.FindAsync(id);
            if (allTable == null)
            {
                return NotFound();
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "AreaId", "AreaId", allTable.AreaId);
            return View(allTable);
        }

        // POST: AllTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TableId,TableName,AreaId")] AllTable allTable)
        {
            if (id != allTable.TableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllTableExists(allTable.TableId))
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
            ViewData["AreaId"] = new SelectList(_context.Areas, "AreaId", "AreaId", allTable.AreaId);
            return View(allTable);
        }
        [Authorize(Roles = "manager")]
        // GET: AllTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AllTables == null)
            {
                return NotFound();
            }

            var allTable = await _context.AllTables
                .Include(a => a.Area)
                .FirstOrDefaultAsync(m => m.TableId == id);
            if (allTable == null)
            {
                return NotFound();
            }

            return View(allTable);
        }

        // POST: AllTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AllTables == null)
            {
                return Problem("Entity set 'ReservationDbContext.AllTables'  is null.");
            }
            var allTable = await _context.AllTables.FindAsync(id);
            if (allTable != null)
            {
                _context.AllTables.Remove(allTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllTableExists(int id)
        {
            return (_context.AllTables?.Any(e => e.TableId == id)).GetValueOrDefault();
        }
    }
}
