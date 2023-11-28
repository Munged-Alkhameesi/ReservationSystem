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
    public class GuestsController : Controller
    {
        private readonly ReservationDbContext _context;

        public GuestsController(ReservationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "manager")]

        // GET: Guests
        public async Task<IActionResult> Index()
        {
              return _context.Guests != null ? 
                          View(await _context.Guests.ToListAsync()) :
                          Problem("Entity set 'ReservationDbContext.Guests'  is null.");
        }

        // GET: Guests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .FirstOrDefaultAsync(m => m.GuestId == id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // GET: Guests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuestId,GuestFirstName,GuestLastName,GuestEmail,GuestPhoneNumber")] Guest guest)
        {
            // if guest exists then no need to recreate it
            if (_context.Guests.Any(g => g.GuestEmail == guest.GuestEmail))
            {
                var gs = await _context.Guests.FirstAsync(g => g.GuestEmail == guest.GuestEmail);
                return RedirectToAction("Create", "Reservations", new { id = gs.GuestId });
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(guest);
                await _context.SaveChangesAsync();
                // go to reservation and pass the guest id as well
                return RedirectToAction("Create", "Reservations", new { id = guest.GuestId });
            }
            return View(guest);
        }
        [Authorize(Roles = "manager")]

        // GET: Guests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        // POST: Guests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuestId,GuestFirstName,GuestLastName,GuestEmail,GuestPhoneNumber")] Guest guest)
        {
            if (id != guest.GuestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestExists(guest.GuestId))
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
            return View(guest);
        }
        [Authorize(Roles = "admin")]

        // GET: Guests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .FirstOrDefaultAsync(m => m.GuestId == id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // POST: Guests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Guests == null)
            {
                return Problem("Entity set 'ReservationDbContext.Guests'  is null.");
            }
            var guest = await _context.Guests.FindAsync(id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// check if a guest exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool GuestExists(int id)
        {
          return (_context.Guests?.Any(e => e.GuestId == id)).GetValueOrDefault();
        }
    }
}
