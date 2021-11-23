using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace edu_development_REST.Migrations
{
    public partial class AddCourseGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                schema: "Courses",
                table: "Course");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "Courses",
                table: "Course",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                schema: "Courses",
                table: "Course",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                schema: "Courses",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Courses",
                table: "Course");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                schema: "Courses",
                table: "Course",
                column: "CourseCode");
        }
    }
}
