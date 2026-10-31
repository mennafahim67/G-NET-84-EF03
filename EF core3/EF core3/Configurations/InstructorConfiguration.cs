using EF_core3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EF_core3.Configurations;
public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Bio).HasMaxLength(500);
        builder.Property(x => x.JoinedDate).IsRequired();
        builder.HasData(
            new Instructor { Id = 1, FullName = "Ahmed Hassan", Email = "ahmed@learn.com", Bio = "Backend developer", JoinedDate = new DateTime(2024, 1, 10) },
            new Instructor { Id = 2, FullName = "Mona Adel", Email = "mona@learn.com", JoinedDate = new DateTime(2024, 2, 15) },
            new Instructor { Id = 3, FullName = "Omar Khaled", Email = "omar@learn.com", JoinedDate = new DateTime(2024, 3, 20) }
        );
    }
}
