using System;
using EF_core3;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
namespace EF_core3.Migrations;
[DbContext(typeof(AppDbContext))]
[Migration("202610310002_SeedData")]
public class SeedData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData("Categories", new[] { "Id", "Name", "Description", "ParentCategoryId" },
            new object[,]
            {
                { 1, "Programming", null!, null! }, { 6, "Design", null!, null! }
            });
        migrationBuilder.InsertData("Categories", new[] { "Id", "Name", "Description", "ParentCategoryId" },
            new object[,]
            {
                { 2, "Backend", null!, 1 }, { 4, "Frontend", null!, 1 }
            });
        migrationBuilder.InsertData("Categories", new[] { "Id", "Name", "Description", "ParentCategoryId" },
            new object[,]
            {
                { 3, "ASP.NET", null!, 2 }, { 5, "Databases", null!, 2 }
            });
        migrationBuilder.InsertData("Instructors", new[] { "Id", "FullName", "Email", "Bio", "JoinedDate" },
            new object[,]
            {
                { 1, "Ahmed Hassan", "ahmed@learn.com", "Backend developer", new DateTime(2024, 1, 10) },
                { 2, "Mona Adel", "mona@learn.com", null!, new DateTime(2024, 2, 15) },
                { 3, "Omar Khaled", "omar@learn.com", null!, new DateTime(2024, 3, 20) }
            });
        migrationBuilder.InsertData("Courses", new[] { "Id", "Title", "Description", "Price", "Level", "IsPublished", "PublishedDate", "InstructorId", "CategoryId" },
            new object[,]
            {
                { 1, "C# Basics", null!, 400m, 0, true, new DateTime(2025, 1, 1), 1, 2 },
                { 2, "ASP.NET Core", null!, 800m, 1, true, new DateTime(2025, 1, 5), 1, 3 },
                { 3, "SQL Server", null!, 600m, 0, true, new DateTime(2025, 2, 1), 2, 5 },
                { 4, "HTML and CSS", null!, 300m, 0, true, new DateTime(2025, 2, 10), 2, 4 },
                { 5, "Advanced EF Core", null!, 900m, 2, false, null!, 3, 3 },
                { 6, "UI Design", null!, 500m, 1, false, null!, 3, 6 }
            });
    }
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData("Courses", "Id", 1);
        migrationBuilder.DeleteData("Courses", "Id", 2);
        migrationBuilder.DeleteData("Courses", "Id", 3);
        migrationBuilder.DeleteData("Courses", "Id", 4);
        migrationBuilder.DeleteData("Courses", "Id", 5);
        migrationBuilder.DeleteData("Courses", "Id", 6);
        migrationBuilder.DeleteData("Instructors", "Id", 1);
        migrationBuilder.DeleteData("Instructors", "Id", 2);
        migrationBuilder.DeleteData("Instructors", "Id", 3);
        foreach (int id in new[] { 3, 5, 2, 4, 1, 6 }) migrationBuilder.DeleteData("Categories", "Id", id);
    }
}
