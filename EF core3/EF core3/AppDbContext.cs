using EF_core3.Models;
using Microsoft.EntityFrameworkCore;
namespace EF_core3;
public class AppDbContext : DbContext
{
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EFCore3DB;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
