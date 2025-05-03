using System.ComponentModel.DataAnnotations;

namespace Internship_Tracker.Models
{
    /// <summary>
    /// Represents an internship application submitted by a user.
    /// Links to both the Internship and the current Status of the application.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Primary key for the application record.
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// ID of the user who submitted the application.
        /// This is tied to the Identity system.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the Internship.
        /// </summary>
        public int InternshipId { get; set; }

        /// <summary>
        /// Navigation property for the related Internship.
        /// </summary>
        public Internship Internship { get; set; } = null!;

        /// <summary>
        /// Foreign key referencing the Status of the application.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Navigation property for the related Status (e.g., Applied, Interview).
        /// </summary>
        public Status Status { get; set; } = null!;

        /// <summary>
        /// Date when the user applied for the internship.
        /// </summary>
        public DateTime DateApplied { get; set; }

        /// <summary>
        /// Optional reminder date to follow up on the application.
        /// Displayed in UI as a date only.
        /// </summary>
        [Display(Name = "Reminder Date")]
        [DataType(DataType.Date)]
        public DateTime? ReminderDate { get; set; }
    }
}
