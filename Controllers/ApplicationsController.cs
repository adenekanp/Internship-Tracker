using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Internship_Tracker.Models;
using Internship_Tracker.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Internship_Tracker.Controllers
{
    // This controller handles all application-related operations for authenticated users
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        // Constructor with dependency injection for the database context and user manager
        public ApplicationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Displays a list of internship applications with filtering, sorting, and searching
        public async Task<IActionResult> Index(string searchString, string statusFilter, string sortOrder)
        {
            var userId = _userManager.GetUserId(User); // Get the currently logged-in user's ID

            // Query user's applications and include related internship and status data
            var applications = _context.Applications
                .Include(a => a.Internship)
                .Include(a => a.Status)
                .Where(a => a.UserId == userId)
                .AsQueryable();

            // Search by company name
            if (!string.IsNullOrEmpty(searchString))
                applications = applications.Where(a => a.Internship.Company.Contains(searchString));

            // Filter by application status
            if (!string.IsNullOrEmpty(statusFilter))
                applications = applications.Where(a => a.Status.Name == statusFilter);

            // Sort by date applied
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            applications = sortOrder == "date_desc"
                ? applications.OrderByDescending(a => a.DateApplied)
                : applications.OrderBy(a => a.DateApplied);

            // Pass list of statuses to the view for dropdown filters
            ViewBag.Statuses = new SelectList(await _context.Statuses.Select(s => s.Name).Distinct().ToListAsync());
            ViewBag.AllStatuses = await _context.Statuses.ToListAsync(); // For AJAX-based status updates

            return View(await applications.ToListAsync());
        }

        // Renders the form for creating a new application
        public IActionResult Create()
        {
            ViewData["InternshipId"] = new SelectList(_context.Internships, "InternshipId", "Company");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name");
            return View();
        }

        // Handles the form submission for creating a new application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var application = new Application
                {
                    UserId = _userManager.GetUserId(User),
                    InternshipId = model.InternshipId,
                    StatusId = model.StatusId,
                    DateApplied = model.DateApplied,
                    ReminderDate = model.ReminderDate
                };

                _context.Applications.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Re-populate dropdowns in case of form error
            ViewData["InternshipId"] = new SelectList(_context.Internships, "InternshipId", "Company", model.InternshipId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name", model.StatusId);
            return View(model);
        }

        // Renders the form for editing an existing application
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var application = await _context.Applications
                .Include(a => a.Internship)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (application == null) return NotFound();

            var model = new ApplicationViewModel
            {
                InternshipId = application.InternshipId,
                StatusId = application.StatusId,
                DateApplied = application.DateApplied,
                ReminderDate = application.ReminderDate
            };

            ViewData["InternshipId"] = new SelectList(_context.Internships, "InternshipId", "Company", application.InternshipId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name", application.StatusId);

            return View(model);
        }

        // Handles the form submission for editing an application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["InternshipId"] = new SelectList(_context.Internships, "InternshipId", "Company", model.InternshipId);
                ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name", model.StatusId);
                return View(model);
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null) return NotFound();

            application.InternshipId = model.InternshipId;
            application.StatusId = model.StatusId;
            application.DateApplied = model.DateApplied;
            application.ReminderDate = model.ReminderDate;
            application.UserId = _userManager.GetUserId(User);

            _context.Update(application);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Renders the confirmation view for deleting an application
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var application = await _context.Applications
                .Include(a => a.Internship)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.ApplicationId == id);

            if (application == null) return NotFound();

            return View(application);
        }

        // Handles the deletion of an application after user confirms
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
