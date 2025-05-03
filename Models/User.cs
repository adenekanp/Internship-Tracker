using static System.Net.Mime.MediaTypeNames;

namespace Internship_Tracker.Models
{
    /// <summary>
    /// Represents a user in the system (not tied to ASP.NET Identity).
    /// Consider removing or refactoring if using IdentityUser instead.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Primary key for the User entity.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Email address of the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's password in plain text (not recommended).
        /// If using ASP.NET Identity, this should be removed.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property linking the user to their internship applications.
        /// </summary>
        public List<Application> Applications { get; set; }
    }
}
