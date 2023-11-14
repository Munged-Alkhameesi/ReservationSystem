using DatabaseReservation.Models;
using DatabaseReservation.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DatabaseReservation.Data;
using Microsoft.EntityFrameworkCore;

namespace DatabaseReservation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _authService;
        private readonly ReservationDbContext _context;

        public UserController(IUserService authService,ReservationDbContext context)
        {
            _authService = authService;
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Index()
        {
            return _authService.GetAllUsers() != null ?
                        View(_authService.GetAllUsers()) :
                        Problem("Entity set 'ReservationDbContext.users'  is null.");
        }

        public IActionResult Login()
        {
           return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            Debug.WriteLine(ModelState.IsValid);
            if (!ModelState.IsValid) { return View(model); }
            model.Role = "member";
            var result = await _authService.RegisterAsync(model);
            
            if (result.StatusCode != 1)
                return View(model);

            EmailOuterService Outer = new EmailOuterService();
            var confirmation = Outer.Inner.SendEmailConfirmation(model.Email, model.LastName, model.UserName, model.Password);

            Debug.WriteLine(confirmation);
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Register model, string role)
        {
            if (!ModelState.IsValid) { return View(model); }
            model.Role = "staff";
            var result = await _authService.RegisterAsync(model);
            TempData["msg"] = result.Message;
            if (result.StatusCode != 1)
                return View(model);
            return RedirectToAction(nameof(Login));
        }
        public async Task<IActionResult> Logout()
        {
            var result = await this._authService.LogoutAsync();

            TempData["msg"] = result.Message;

            return RedirectToAction(nameof(Login));

        }

        // GET: Sittings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _authService.GetAllUsers() == null)
            {
                return NotFound();
            }

            var user = _authService.GetAllUsers()
                .FirstOrDefault(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else if(user.UserName == "manager")
            {
                return Problem("No one is allowed to delete the manager!");
            }
            else if(user.UserName == User.Identity.Name)
            {
                return Problem("Cannot Delete the current logged in user!");
            }
            return View(user);
        }

        // POST: Sittings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_authService.GetAllUsers() == null)
            {
                return Problem("Entity set 'ReservationDbContext.user' is null.");
            }
            var user = _authService.GetAllUsers().FirstOrDefault(user => user.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
