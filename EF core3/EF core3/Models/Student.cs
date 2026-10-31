namespace EF_core3.Models;
public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime DateOfBirth { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new();
}
