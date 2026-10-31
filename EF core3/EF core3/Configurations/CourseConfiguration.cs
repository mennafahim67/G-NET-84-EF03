using EF_core3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EF_core3.Configurations;
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.Property(x => x.Price).HasPrecision(10, 2).IsRequired();
        builder.Property(x => x.Level).IsRequired();
        builder.Property(x => x.IsPublished).IsRequired();
        builder.HasOne(x => x.Instructor).WithMany(x => x.Courses)
            .HasForeignKey(x => x.InstructorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Category).WithMany(x => x.Courses)
            .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.HasData(
            new Course { Id = 1, Title = "C# Basics", Price = 400, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2025, 1, 1), InstructorId = 1, CategoryId = 2 },
            new Course { Id = 2, Title = "ASP.NET Core", Price = 800, Level = CourseLevel.Intermediate, IsPublished = true, PublishedDate = new DateTime(2025, 1, 5), InstructorId = 1, CategoryId = 3 },
            new Course { Id = 3, Title = "SQL Server", Price = 600, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2025, 2, 1), InstructorId = 2, CategoryId = 5 },
            new Course { Id = 4, Title = "HTML and CSS", Price = 300, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2025, 2, 10), InstructorId = 2, CategoryId = 4 },
            new Course { Id = 5, Title = "Advanced EF Core", Price = 900, Level = CourseLevel.Advanced, IsPublished = false, InstructorId = 3, CategoryId = 3 },
            new Course { Id = 6, Title = "UI Design", Price = 500, Level = CourseLevel.Intermediate, IsPublished = false, InstructorId = 3, CategoryId = 6 }
        );
    }
}
