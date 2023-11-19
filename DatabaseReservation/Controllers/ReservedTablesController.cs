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
    public class ReservedTablesController : Controller
    {
        private readonly ReservationDbContext _context;

        public ReservedTablesController(ReservationDbContext context)
        {
            _context = context;
        }

        // GET: ReservedTables
        public async Task<IActionResult> Index()
        {
            var reservationDbContext = _context.ReservedTables.Include(r => r.Reservation).Include(r => r.Table);
            return View(await reservationDbContext.ToListAsync());
        }

        // GET: ReservedTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReservedTables == null)
            {
                return NotFound();
            }

            var reservedTable = await _context.ReservedTables
                .Include(r => r.Reservation)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(m => m.ReservedTableId == id);
            if (reservedTable == null)
            {
                return NotFound();
            }

            return View(reservedTable);
        }

        // GET: ReservedTables/Create
        public IActionResult Create()
        {           
            // send data back to view to autofill fields for the user 
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "ReservationId");
            ViewData["TableId"] = new SelectList(_context.AllTables, "TableId", "TableId");
            return View();
        }

        // POST: ReservedTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservedTableId,ReservationId,TableId")] ReservedTable reservedTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservedTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }           
            // send data back to view if the model is not valid so values still appear on the page
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "ReservationId", reservedTable.ReservationId);
            ViewData["TableId"] = new SelectList(_context.AllTables, "TableId", "TableId", reservedTable.TableId);
            return View(reservedTable);
        }

        // GET: ReservedTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReservedTables == null)
            {
                return NotFound();
            }

            var reservedTable = await _context.ReservedTables.FindAsync(id);
            if (reservedTable == null)
            {
                return NotFound();
            }
            // send data back to view to autofill fields for the user 
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "ReservationId", reservedTable.ReservationId);
            ViewData["TableId"] = new SelectList(_context.AllTables, "TableId", "TableId", reservedTable.TableId);
            return View(reservedTable);
        }

        // POST: ReservedTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservedTableId,ReservationId,TableId")] ReservedTable reservedTable)
        {
            if (id != reservedTable.ReservedTableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservedTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservedTableExists(reservedTable.ReservedTableId))
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
            // send data back to view if the model is not valid so values still appear on the page
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "ReservationId", reservedTable.ReservationId);
            ViewData["TableId"] = new SelectList(_context.AllTables, "TableId", "TableId", reservedTable.TableId);
            return View(reservedTable);
        }
        
        // GET: ReservedTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReservedTables == null)
            {
                return NotFound();
            }

            var reservedTable = await _context.ReservedTables
                .Include(r => r.Reservation)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(m => m.ReservedTableId == id);
            if (reservedTable == null)
            {
                return NotFound();
            }

            return View(reservedTable);
        }

        // POST: ReservedTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReservedTables == null)
            {
                return Problem("Entity set 'ReservationDbContext.ReservedTables'  is null.");
            }
            var reservedTable = await _context.ReservedTables.FindAsync(id);
            if (reservedTable != null)
            {
                _context.ReservedTables.Remove(reservedTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservedTableExists(int id)
        {
          return (_context.ReservedTables?.Any(e => e.ReservedTableId == id)).GetValueOrDefault();
        }
    }
}
