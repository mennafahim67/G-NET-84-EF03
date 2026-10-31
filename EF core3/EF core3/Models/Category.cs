namespace EF_core3.Models;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public List<Category> SubCategories { get; set; } = new();
    public List<Course> Courses { get; set; } = new();
}
