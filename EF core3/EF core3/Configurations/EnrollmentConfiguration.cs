using EF_core3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EF_core3.Configurations;
public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasKey(x => new { x.StudentId, x.CourseId });
        builder.Property(x => x.EnrollmentDate).IsRequired();
        builder.Property(x => x.Progress).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.FinalGrade).HasPrecision(5, 2);
        builder.ToTable(x => x.HasCheckConstraint("CK_Enrollments_Progress", "[Progress] >= 0 AND [Progress] <= 100"));
        builder.HasOne(x => x.Student).WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Course).WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);
    }
}
