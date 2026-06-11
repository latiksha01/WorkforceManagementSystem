using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAnnouncementRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Employees_EmployeeId",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_EmployeeId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Announcements");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CreatedBy",
                table: "Announcements",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Employees_CreatedBy",
                table: "Announcements",
                column: "CreatedBy",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Employees_CreatedBy",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_CreatedBy",
                table: "Announcements");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Announcements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_EmployeeId",
                table: "Announcements",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Employees_EmployeeId",
                table: "Announcements",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}
