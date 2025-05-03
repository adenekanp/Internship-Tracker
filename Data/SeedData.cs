using Internship_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Internship_Tracker
{
    // Static class for seeding initial data into the database
    public static class SeedData
    {
        // Method to seed default status entries into the database
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensures the database is created before seeding
            context.Database.EnsureCreated();

            // If any status records already exist, exit to avoid duplication
            if (context.Statuses.Any())
            {
                return;
            }

            // Define an array of default statuses for job applications
            var statuses = new[]
            {
                new Status { Name = "Pending" },
                new Status { Name = "Rejected" },
                new Status { Name = "Behavioral Interview" },
                new Status { Name = "Technical Interview" },
                new Status { Name = "Offer" }
            };

            // Add the statuses to the database and save changes
            context.Statuses.AddRange(statuses);
            context.SaveChanges();
        }
    }
}
