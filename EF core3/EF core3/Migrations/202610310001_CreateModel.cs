using System;
using EF_core3;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
namespace EF_core3.Migrations;
[DbContext(typeof(AppDbContext))]
[Migration("202610310001_CreateModel")]
public class CreateModel : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(name: "Categories", columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
            ParentCategoryId = table.Column<int>(type: "int", nullable: true)
        }, constraints: table =>
        {
            table.PrimaryKey("PK_Categories", x => x.Id);
            table.ForeignKey("FK_Categories_Categories_ParentCategoryId", x => x.ParentCategoryId, "Categories", "Id", onDelete: ReferentialAction.Restrict);
        });
        migrationBuilder.CreateTable(name: "Instructors", columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
            FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
            JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
        }, constraints: table => table.PrimaryKey("PK_Instructors", x => x.Id));
        migrationBuilder.CreateTable(name: "Students", columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
            FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
        }, constraints: table => table.PrimaryKey("PK_Students", x => x.Id));
        migrationBuilder.CreateTable(name: "Courses", columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
            Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
            Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
            Level = table.Column<int>(type: "int", nullable: false),
            IsPublished = table.Column<bool>(type: "bit", nullable: false),
            PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            InstructorId = table.Column<int>(type: "int", nullable: false),
            CategoryId = table.Column<int>(type: "int", nullable: false)
        }, constraints: table =>
        {
            table.PrimaryKey("PK_Courses", x => x.Id);
            table.ForeignKey("FK_Courses_Categories_CategoryId", x => x.CategoryId, "Categories", "Id", onDelete: ReferentialAction.Restrict);
            table.ForeignKey("FK_Courses_Instructors_InstructorId", x => x.InstructorId, "Instructors", "Id", onDelete: ReferentialAction.Restrict);
        });
        migrationBuilder.CreateTable(name: "Enrollments", columns: table => new
        {
            StudentId = table.Column<int>(type: "int", nullable: false),
            CourseId = table.Column<int>(type: "int", nullable: false),
            EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            Progress = table.Column<int>(type: "int", nullable: false),
            Status = table.Column<int>(type: "int", nullable: false),
            FinalGrade = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true)
        }, constraints: table =>
        {
            table.PrimaryKey("PK_Enrollments", x => new { x.StudentId, x.CourseId });
            table.ForeignKey("FK_Enrollments_Students_StudentId", x => x.StudentId, "Students", "Id", onDelete: ReferentialAction.Cascade);
            table.ForeignKey("FK_Enrollments_Courses_CourseId", x => x.CourseId, "Courses", "Id", onDelete: ReferentialAction.Cascade);
            table.CheckConstraint("CK_Enrollments_Progress", "[Progress] >= 0 AND [Progress] <= 100");
        });
        migrationBuilder.CreateIndex(name: "IX_Categories_ParentCategoryId", table: "Categories", column: "ParentCategoryId");
        migrationBuilder.CreateIndex(name: "IX_Courses_CategoryId", table: "Courses", column: "CategoryId");
        migrationBuilder.CreateIndex(name: "IX_Courses_InstructorId", table: "Courses", column: "InstructorId");
        migrationBuilder.CreateIndex(name: "IX_Enrollments_CourseId", table: "Enrollments", column: "CourseId");
    }
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Enrollments");
        migrationBuilder.DropTable("Courses");
        migrationBuilder.DropTable("Students");
        migrationBuilder.DropTable("Categories");
        migrationBuilder.DropTable("Instructors");
    }
}
