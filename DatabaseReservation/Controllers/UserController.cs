using DatabaseReservation.Models;
using DatabaseReservation.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DatabaseReservation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace DatabaseReservation.Controllers
{
    public class UserController : Controller
    {
        // services to be used by this controller class
        private readonly IUserService _authService;
        private readonly ReservationDbContext _context;
        private readonly IFileUpload _fileUploadService;
        public UserController(IUserService authService, ReservationDbContext context, IFileUpload fileUploadService)
        {
            _authService = authService;
            _context = context;
            _fileUploadService = fileUploadService; 
        }
        public IActionResult Register()
        {
            return View();
        }
        [Authorize(Roles ="manager")]
        public IActionResult Index()
        {
            // return all users if found
            return _context.Users != null ?
                        View(_context.Users) :
                        Problem("Entity set 'ReservationDbContext.users'  is null.");
        }
        
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Login method for the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Register method for the user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(Register model,[FromForm] IFormFile formFile)
        {
            Debug.WriteLine(ModelState.IsValid);
            if (!ModelState.IsValid) 
            {
                return View(model); 
            }
            model.Role = "member";
            try
            {
                string x = await _fileUploadService.UploadFile(formFile);
                if (!x.IsNullOrEmpty())
                {
                    model.ProfilePic = x;
                }
                else
                {
                    model.ProfilePic="Default.png";
                }
            }
            catch (Exception ex)
            {
                return View(model);

            }
            var result = await _authService.RegisterAsync(model);

            if (result.StatusCode != 1)
                return View(model);

            // send an email confirmation to the user's email
            EmailOuterService Outer = new();
            var confirmation = Outer.Inner.SendEmailConfirmation(model.Email, model.LastName, model.UserName, model.Password);

            Debug.WriteLine(confirmation);
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Create more users. only admin/manager can
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Register model, string role, IFormFile formFile)
        {
            if (!ModelState.IsValid) { return View(model); }
            
            model.Role = role;
            if (formFile != null)
            {
                try
                {
                    // add image to the user table to be used as a profile pic
                    string x = await _fileUploadService.UploadFile(formFile);
                    if (!x.IsNullOrEmpty())
                    {
                        model.ProfilePic = x;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return View(model);

                }
            }
            var result = await _authService.RegisterAsync(model);
           
            TempData["msg"] = result.Message;
            
            if (result.StatusCode != 1)
                return View(model);
            return RedirectToAction(nameof(Login));
        }
        /// <summary>
        /// logout method 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            var result = await this._authService.LogoutAsync();

            TempData["msg"] = result.Message;

            return RedirectToAction(nameof(Login));

        }
        [Authorize(Roles ="manager")]
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,FirstName,LastName,Email,PhoneNumber")] ApplicationUser user, IFormFile formFile)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            // find the old user info and change them with new ones
            var oldUser = await _context.Users.FindAsync(user.Id);
            oldUser.UserName = user.UserName;
            oldUser.FirstName = user.FirstName;
            oldUser.LastName = user.LastName;
            oldUser.PhoneNumber = user.PhoneNumber;
            oldUser.Email = user.Email;
            if (formFile != null)
            {
                try
                {
                    // add image to the user table to be used as a profile pic
                    string x = await _fileUploadService.UploadFile(formFile);
                    if (!x.IsNullOrEmpty())
                    {
                        oldUser.ProfilePic = x;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return View(user);

                }
            }
            if (ModelState.IsValid)
            {
                try
                {

                    _context.Users.Update(oldUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }
        [Authorize(Roles = "manager")]
        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else if (user.UserName == "manager")
            {
                // no editing the seeded manager
                return Problem("No one is allowed to delete the manager!");
            }
            else if (user.UserName == User.Identity.Name)
            {
                return Problem("Cannot Delete the current logged in user!");
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ReservationDbContext.user' is null.");
            }
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //Change Password
        /// <summary>
        /// change password method in case a user wants a new password
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.ChangePasswordAsync(model, User.Identity.Name);
            TempData["msg"] = result.Message;
            if (result.StatusCode != 1)
                return View(model);

            return RedirectToAction(nameof(Login));

        }
    }

}