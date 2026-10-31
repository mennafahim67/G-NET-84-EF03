namespace EF_core3.Models;
public class Instructor
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string? Bio { get; set; }
    public DateTime JoinedDate { get; set; }
    public List<Course> Courses { get; set; } = new();
}
