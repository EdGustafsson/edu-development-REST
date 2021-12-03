using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace edu_development_REST.Migrations
{
    public partial class NewObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Courses");

            migrationBuilder.EnsureSchema(
                name: "CourseMemberships");

            migrationBuilder.EnsureSchema(
                name: "CourseSources");

            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.EnsureSchema(
                name: "UserSources");

            migrationBuilder.CreateTable(
                name: "Course",
                schema: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseMembership",
                schema: "CourseMemberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrolledDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMembership", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseSource",
                schema: "CourseSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<int>(type: "int", nullable: false),
                    ExternalSource = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSource",
                schema: "UserSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<int>(type: "int", nullable: false),
                    ExternalSource = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSource", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Course",
                schema: "Courses");

            migrationBuilder.DropTable(
                name: "CourseMembership",
                schema: "CourseMemberships");

            migrationBuilder.DropTable(
                name: "CourseSource",
                schema: "CourseSources");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "UserSource",
                schema: "UserSources");
        }
    }
}
