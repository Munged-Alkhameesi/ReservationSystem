using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseReservation.Models;
using System.Diagnostics;
using DatabaseReservation.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DatabaseReservation.Data;

namespace DatabaseReservation.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ReservationDbContext _context;
        private readonly IReservationService _IReservation;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserManager<ApplicationUser> _user;

        public ReservationsController(ReservationDbContext context, IReservationService iRes, UserManager<ApplicationUser> user, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _IReservation = iRes;
            _userManager = userManager;
            _user = user;
        }
        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return View(new List<Reservation>());
            }
            if (User.IsInRole("user"))
            {
                var user = _user.GetUserAsync(User).Result;
                var guest = _context.Guests.FirstOrDefault(g => g.GuestEmail == user.Email);
                var res = _context.Reservations.Include(r => r.GuestId == guest.GuestId);
                return View(res);
            }
            var vProductList = _IReservation.GetAllReservations();
            return View(vProductList);
        }
        [Authorize(Roles ="manager")]
        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Guest)
                .Include(r => r.Sitting)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create(int? id)
        {
            ApplicationUser? user = _user.GetUserAsync(User).Result;
            if (User.Identity.IsAuthenticated && User.IsInRole("member"))
            {
                if (_context.Guests.Any(g => g.GuestEmail == user.Email))
                {
                    id = _context.Guests.FirstOrDefault(g => g.GuestEmail == user.Email)?.GuestId;
                }
                else
                {
                    Guest guest = new()
                    {
                        GuestEmail = user.Email,
                        GuestFirstName = user.FirstName,
                        GuestLastName = user.LastName,
                        GuestPhoneNumber = int.Parse(user.PhoneNumber),
                    };
                    _context.Add(guest);
                    _context.SaveChanges();
                    id = guest.GuestId;
                }
            }
            else if (id == null)
            {
                return RedirectToAction("Create", "Guests");
            }
        
            // find the guest with that id
            var gs = _context.Guests.Find(id);
            
            // send the data to the view
            ViewData["GuestId"] = new SelectList(new[] { gs }, "GuestId", "GuestFirstName");
            ViewData["desc"] = _context.Sittings.FirstOrDefault(sitting => sitting.StartDateTime < DateTime.Now && sitting.EndDateTime > DateTime.Now)?.Description ?? _context.Sittings.ToArray()[0].Description;
            ViewData["SittingsList"] = _context.Sittings.ToArray();
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,GuestCount,ReservationSource,StartDateTime,Duration,Notes,SittingId,GuestId,ResStatus")] Reservation reservation)
        {
            // if no data is in the viewdata a new data is sent
            if (ViewData.Count == 0)
            {
                ViewData["desc"] = _context.Sittings.FirstOrDefault(sitting => sitting.StartDateTime < DateTime.Now && sitting.EndDateTime > DateTime.Now)?.Description ?? _context.Sittings.ToArray()[0].Description;
                ViewData["SittingsList"] = _context.Sittings.ToArray();
            }
            // extra verification for if the date is right or not
            var sitting = _context.Sittings.Any(sittingId => sittingId.StartDateTime < reservation.StartDateTime && sittingId.EndDateTime > reservation.StartDateTime);
            if (!sitting)
            {

                ModelState.AddModelError("StartDateTime", "please input a valid start time");
                
                // find the guest with that id
                var gs = _context.Guests.Find(reservation.GuestId);

                // send the data to the view
                ViewData["GuestId"] = new SelectList(new[] { gs }, "GuestId", "GuestFirstName");
                return View(reservation);
            }
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId", reservation.GuestId);
            ViewData["SittingId"] = new SelectList(_context.Sittings, "SittingId", "SittingId", reservation.SittingId);
            return View(reservation);
        }
        [Authorize]
        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // sending guest data and the list of sittings to view so it can be used by the JS code
            ViewData["SittingsList"] = _context.Sittings.Select(sitting => new { sitting.SittingId, sitting.StartDateTime, sitting.EndDateTime, sitting.Description }).ToArray();
            ViewData["desc"] = _context.Sittings.FirstOrDefault(sitting => sitting.StartDateTime < DateTime.Now && sitting.EndDateTime > DateTime.Now)?.Description ?? _context.Sittings.ToArray()[0].Description;

            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId", reservation.GuestId);
            ViewData["SittingId"] = new SelectList(_context.Sittings, "SittingId", "SittingId", reservation.SittingId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,GuestCount,ReservationSource,StartDateTime,Duration,Notes,SittingId,GuestId,ResStatus")] Reservation reservation)
        {
            if (ViewData.Count == 0)
            {
                ViewData["SittingsList"] = _context.Sittings.Select(sitting => new { sitting.SittingId, sitting.StartDateTime, sitting.EndDateTime, sitting.Description }).ToArray();
                ViewData["desc"] = _context.Sittings.FirstOrDefault(sitting => sitting.StartDateTime < DateTime.Now && sitting.EndDateTime > DateTime.Now)?.Description ?? _context.Sittings.ToArray()[0].Description;

            }

            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
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
            ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId", reservation.GuestId);
            ViewData["SittingId"] = new SelectList(_context.Sittings, "SittingId", "SittingId", reservation.SittingId);
            return View(reservation);
        }
        [Authorize(Roles ="manager")]
        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Guest)
                .Include(r => r.Sitting)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservations == null)
            {
                return Problem("Entity set 'ReservationDbContext.Reservations'  is null.");
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
          return (_context.Reservations?.Any(e => e.ReservationId == id)).GetValueOrDefault();
        }
    }
}
