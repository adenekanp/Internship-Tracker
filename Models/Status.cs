using System.ComponentModel.DataAnnotations;

namespace Internship_Tracker.Models
{
    /// <summary>
    /// Represents the status of an internship application (e.g., Applied, Interviewing, Rejected).
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Primary key for the Status entity.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// The name or label of the status.
        /// This field is required.
        /// Examples: "Applied", "Interview", "Offer", "Rejected"
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
