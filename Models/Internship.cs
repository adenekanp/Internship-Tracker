using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Internship_Tracker.Models
{
    /// <summary>
    /// Represents an internship opportunity offered by a company.
    /// Stores basic information about the internship and tracks related applications.
    /// </summary>
    public class Internship
    {
        /// <summary>
        /// Primary key for the Internship entity.
        /// </summary>
        [Key]
        public int InternshipId { get; set; }

        /// <summary>
        /// Name of the company offering the internship.
        /// This field is required.
        /// </summary>
        [Required]
        public string Company { get; set; } = string.Empty;

        /// <summary>
        /// Title or position of the internship role.
        /// This field is required.
        /// </summary>
        [Required]
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// Deadline by which applications must be submitted.
        /// This field is required.
        /// </summary>
        [Required]
        public DateTime ApplicationDeadline { get; set; }

        /// <summary>
        /// Navigation property to list of applications submitted for this internship.
        /// Can be null if no applications exist yet.
        /// </summary>
        public List<Application>? Applications { get; set; }
    }
}
