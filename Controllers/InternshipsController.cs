using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Internship_Tracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace Internship_Tracker.Controllers
{
    // Restricts access to authenticated users only
    [Authorize]
    public class InternshipsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        // Injects the database context and user manager
        public InternshipsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Displays a list of all internships
        public async Task<IActionResult> Index()
        {
            var internships = await _context.Internships.ToListAsync();
            return View(internships);
        }

        // Displays detailed information for a selected internship
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var internship = await _context.Internships
                .FirstOrDefaultAsync(m => m.InternshipId == id);

            if (internship == null) return NotFound();

            return View(internship);
        }

        // Returns the create internship form
        public IActionResult Create()
        {
            return View();
        }

        // Handles the creation of a new internship
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Company,Position,ApplicationDeadline")] Internship internship)
        {
            if (ModelState.IsValid)
            {
                _context.Internships.Add(internship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(internship);
        }

        // Returns the edit form for a selected internship
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var internship = await _context.Internships.FindAsync(id);
            if (internship == null) return NotFound();

            return View(internship);
        }

        // Handles updating an internship
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InternshipId,Company,Position,ApplicationDeadline")] Internship internship)
        {
            if (id != internship.InternshipId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternshipExists(internship.InternshipId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(internship);
        }

        // Displays the delete confirmation page
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var internship = await _context.Internships
                .FirstOrDefaultAsync(m => m.InternshipId == id);

            if (internship == null) return NotFound();

            return View(internship);
        }

        // Handles deletion after confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var internship = await _context.Internships.FindAsync(id);
            if (internship != null)
            {
                _context.Internships.Remove(internship);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Checks if an internship exists by ID
        private bool InternshipExists(int id)
        {
            return _context.Internships.Any(e => e.InternshipId == id);
        }
    }
}
