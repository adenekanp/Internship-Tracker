using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Internship_Tracker.Models;

namespace Internship_Tracker.Controllers
{
    // Defines this controller as an API controller and sets the route to "api/ApplicationsApi"
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires users to be authenticated to access endpoints
    public class ApplicationsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        // Constructor with dependency injection for database context and user manager
        public ApplicationsApiController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/ApplicationsApi
        // Returns a list of the authenticated user's applications in a simplified format
        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            var userId = _userManager.GetUserId(User); // Get current logged-in user's ID
            var applications = await _context.Applications
                .Include(a => a.Internship)
                .Include(a => a.Status)
                .Where(a => a.UserId == userId)
                .Select(a => new {
                    a.ApplicationId,
                    a.DateApplied,
                    Internship = a.Internship.Company + " - " + a.Internship.Position,
                    Status = a.Status.Name
                })
                .ToListAsync();

            return Ok(applications); // Return the applications as a 200 OK response
        }

        // PUT: api/ApplicationsApi/5
        // Updates the status of an application owned by the logged-in user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] int statusId)
        {
            var userId = _userManager.GetUserId(User); // Get current logged-in user
            var application = await _context.Applications
                .FirstOrDefaultAsync(a => a.ApplicationId == id && a.UserId == userId);

            if (application == null)
                return NotFound(); // Return 404 if no matching application is found

            application.StatusId = statusId; // Update status
            await _context.SaveChangesAsync(); // Save changes to database

            return NoContent(); // Return 204 No Content
        }

        // DELETE: api/ApplicationsApi/5
        // Deletes an application owned by the authenticated user
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var application = await _context.Applications
                .FirstOrDefaultAsync(a => a.ApplicationId == id && a.UserId == userId);

            if (application == null)
                return NotFound(); // Return 404 if not found

            _context.Applications.Remove(application); // Delete application
            await _context.SaveChangesAsync(); // Save deletion to DB

            return NoContent(); // Return 204 No Content
        }
    }
}
