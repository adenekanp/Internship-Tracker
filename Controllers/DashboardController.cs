using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Internship_Tracker.Models;

namespace Internship_Tracker.Controllers
{
    // Controller responsible for rendering the dashboard view with user-specific reminders
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        // Constructor with injected dependencies for database access and user identity management
        public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Displays the dashboard page, showing upcoming application reminders
        /// for the logged-in user within the next two days.
        /// </summary>
        /// <returns>The dashboard view with reminder data in ViewBag.</returns>
        public async Task<IActionResult> Index()
        {
            // Get the currently logged-in user's ID
            var userId = _userManager.GetUserId(User);

            // Define date range: today and two days from now
            var today = DateTime.Today;
            var inTwoDays = today.AddDays(2);

            // Query the database for applications with reminder dates within the range
            var upcoming = await _context.Applications
                .Include(a => a.Internship) // Include internship details
                .Where(a => a.UserId == userId && a.ReminderDate != null && a.ReminderDate >= today && a.ReminderDate <= inTwoDays)
                .ToListAsync();

            // Pass the reminders to the view using ViewBag
            ViewBag.UpcomingReminders = upcoming;

            return View();
        }
    }
}
