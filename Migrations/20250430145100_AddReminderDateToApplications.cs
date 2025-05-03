using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class AddReminderDateToApplications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderDate",
                table: "Applications",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReminderDate",
                table: "Applications");
        }
    }
}
