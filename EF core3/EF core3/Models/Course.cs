namespace EF_core3.Models;
public enum CourseLevel { Beginner, Intermediate, Advanced }
public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public CourseLevel Level { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedDate { get; set; }
    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public List<Enrollment> Enrollments { get; set; } = new();
}
