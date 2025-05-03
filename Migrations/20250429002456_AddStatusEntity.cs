using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_User_UserId1",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Applications",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_UserId1",
                table: "Applications",
                newName: "IX_Applications_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Statuses_StatusId",
                table: "Applications",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Statuses_StatusId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Applications",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_StatusId",
                table: "Applications",
                newName: "IX_Applications_UserId1");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_User_UserId1",
                table: "Applications",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
