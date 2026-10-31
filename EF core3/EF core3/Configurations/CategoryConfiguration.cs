using EF_core3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EF_core3.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(300);
        builder.HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories)
            .HasForeignKey(x => x.ParentCategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.HasData(
            new Category { Id = 1, Name = "Programming" },
            new Category { Id = 2, Name = "Backend", ParentCategoryId = 1 },
            new Category { Id = 3, Name = "ASP.NET", ParentCategoryId = 2 },
            new Category { Id = 4, Name = "Frontend", ParentCategoryId = 1 },
            new Category { Id = 5, Name = "Databases", ParentCategoryId = 2 },
            new Category { Id = 6, Name = "Design" }
        );
    }
}
