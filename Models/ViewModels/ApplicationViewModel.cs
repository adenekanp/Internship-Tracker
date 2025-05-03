using System;
using System.ComponentModel.DataAnnotations;

namespace Internship_Tracker.Models.ViewModels
{
    /// <summary>
    /// ViewModel used to create or edit an internship application.
    /// Encapsulates form input fields and validation attributes.
    /// </summary>
    public class ApplicationViewModel
    {
        /// <summary>
        /// Selected Internship ID from the list of internships.
        /// Required field.
        /// </summary>
        [Required]
        public int InternshipId { get; set; }

        /// <summary>
        /// Selected status ID (e.g., Applied, Interview, Offer).
        /// Required field.
        /// </summary>
        [Required]
        public int StatusId { get; set; }

        /// <summary>
        /// Date the application was submitted.
        /// Required and displayed as a Date only (no time).
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateApplied { get; set; }

        /// <summary>
        /// Optional date set to remind the user to follow up on this application.
        /// Displayed as a Date only.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? ReminderDate { get; set; }
    }
}
