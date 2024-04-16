using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTracking.DataManager.Migrations
{
    /// <inheritdoc />
    public partial class Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CountEntity",
                table: "CountEntity");

            migrationBuilder.RenameTable(
                name: "CountEntity",
                newName: "Counts");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "Students",
                newName: "LetterId");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "Specialties",
                newName: "LetterId");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "RemoteAreas",
                newName: "LetterId");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "Counts",
                newName: "LetterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Counts",
                table: "Counts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EducationProfile = table.Column<string>(type: "text", nullable: false),
                    Agency = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    SpecialtyCcode = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Qualification = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacultyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Counts",
                table: "Counts");

            migrationBuilder.RenameTable(
                name: "Counts",
                newName: "CountEntity");

            migrationBuilder.RenameColumn(
                name: "LetterId",
                table: "Students",
                newName: "RecordId");

            migrationBuilder.RenameColumn(
                name: "LetterId",
                table: "Specialties",
                newName: "RecordId");

            migrationBuilder.RenameColumn(
                name: "LetterId",
                table: "RemoteAreas",
                newName: "RecordId");

            migrationBuilder.RenameColumn(
                name: "LetterId",
                table: "CountEntity",
                newName: "RecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountEntity",
                table: "CountEntity",
                column: "Id");
        }
    }
}
