using EF_core3.Models;
using Microsoft.EntityFrameworkCore;
namespace EF_core3;
internal class Program
{
    static void Main()
    {
        try
        {
            using AppDbContext db = new AppDbContext();
            db.Database.Migrate();
            SeedStudentsAndEnrollments(db);
            AddInstructor(db);
            AddCourse(db);
            AddSeveralStudents(db);
            EnrollStudent(db);
            FindCourse(db);
            FilterCourses(db);
            ShowCategories(db);
            CourseRoster(db);
            ChangePrice(db);
            UpdateProgress(db);
            PublishCourse(db);
            Unenroll(db);
            DeleteCategory(db);
            DeleteCourse(db);
            BulkPriceRaise(db);
            BulkDelete(db);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Database error: " + ex.Message);
        }
    }
    static void SeedStudentsAndEnrollments(AppDbContext db)
    {
        if (!db.Students.Any())
        {
            db.Students.AddRange(
                new Student { FullName = "Ali Mostafa", Email = "ali@mail.com", DateOfBirth = new DateTime(2002, 1, 12) },
                new Student { FullName = "Sara Mahmoud", Email = "sara@mail.com", DateOfBirth = new DateTime(2001, 6, 8) },
                new Student { FullName = "Youssef Ahmed", Email = "youssef@mail.com", DateOfBirth = new DateTime(2003, 3, 4) },
                new Student { FullName = "Nour Adel", Email = "nour@mail.com", DateOfBirth = new DateTime(2002, 9, 20) },
                new Student { FullName = "Hana Samir", Email = "hana@mail.com", DateOfBirth = new DateTime(2004, 12, 2) }
            );
            db.SaveChanges();
        }
        if (!db.Enrollments.Any())
        {
            var students = db.Students.OrderBy(x => x.Id).Take(5).Select(x => x.Id).ToList();
            if (students.Count < 5) { Console.WriteLine("Not enough students to seed enrollments."); return; }
            db.Enrollments.AddRange(
                new Enrollment { StudentId = students[0], CourseId = 1, EnrollmentDate = new DateTime(2025, 3, 1), Progress = 20, Status = EnrollmentStatus.Active },
                new Enrollment { StudentId = students[0], CourseId = 2, EnrollmentDate = new DateTime(2025, 3, 2), Progress = 100, Status = EnrollmentStatus.Completed, FinalGrade = 90 },
                new Enrollment { StudentId = students[1], CourseId = 1, EnrollmentDate = new DateTime(2025, 3, 3), Progress = 60, Status = EnrollmentStatus.Active },
                new Enrollment { StudentId = students[1], CourseId = 3, EnrollmentDate = new DateTime(2025, 3, 4), Progress = 10, Status = EnrollmentStatus.Dropped },
                new Enrollment { StudentId = students[2], CourseId = 2, EnrollmentDate = new DateTime(2025, 3, 5), Progress = 35, Status = EnrollmentStatus.Active },
                new Enrollment { StudentId = students[2], CourseId = 4, EnrollmentDate = new DateTime(2025, 3, 6), Progress = 100, Status = EnrollmentStatus.Completed, FinalGrade = 85 },
                new Enrollment { StudentId = students[3], CourseId = 3, EnrollmentDate = new DateTime(2025, 3, 7), Progress = 45, Status = EnrollmentStatus.Active },
                new Enrollment { StudentId = students[4], CourseId = 4, EnrollmentDate = new DateTime(2025, 3, 8), Progress = 0, Status = EnrollmentStatus.Dropped }
            );
            db.SaveChanges();
        }
        Console.WriteLine("Seeding checked.");
    }
    static void AddInstructor(AppDbContext db)
    {
        var old = db.Instructors.FirstOrDefault(x => x.Email == "newinstructor@mail.com");
        if (old != null) { Console.WriteLine("Instructor already exists: " + old.Id); return; }
        var instructor = new Instructor { FullName = "Kareem Fathy", Email = "newinstructor@mail.com", JoinedDate = DateTime.Today };
        db.Instructors.Add(instructor);
        db.SaveChanges();
        Console.WriteLine("Instructor added: " + instructor.Id);
    }
    static void AddCourse(AppDbContext db)
    {
        int instructorId = 1, categoryId = 2;
        if (!db.Instructors.Any(x => x.Id == instructorId)) { Console.WriteLine("Instructor not found."); return; }
        if (!db.Categories.Any(x => x.Id == categoryId)) { Console.WriteLine("Category not found."); return; }
        if (db.Courses.Any(x => x.Title == "C# Practice")) { Console.WriteLine("Course already exists."); return; }
        var course = new Course { Title = "C# Practice", Price = 350, Level = CourseLevel.Beginner, InstructorId = instructorId, CategoryId = categoryId };
        db.Courses.Add(course);
        db.SaveChanges();
        Console.WriteLine("Course added: " + course.Id);
    }
    static void AddSeveralStudents(AppDbContext db)
    {
        if (db.Students.Any(x => x.Email == "mazen@mail.com")) { Console.WriteLine("Three students already added."); return; }
        var students = new List<Student>
        {
            new Student { FullName = "Mazen Ali", Email = "mazen@mail.com", DateOfBirth = new DateTime(2003, 5, 10) },
            new Student { FullName = "Dina Hassan", Email = "dina@mail.com", DateOfBirth = new DateTime(2002, 7, 14) },
            new Student { FullName = "Nada Omar", Email = "nada@mail.com", DateOfBirth = new DateTime(2004, 11, 21) }
        };
        db.Students.AddRange(students);
        db.SaveChanges();
        foreach (var student in students) Console.WriteLine("Student added: " + student.Id);
    }
    static void EnrollStudent(AppDbContext db)
    {
        int studentId = db.Students.Where(x => x.Email == "mazen@mail.com").Select(x => x.Id).FirstOrDefault(), courseId = 1;
        if (!db.Students.Any(x => x.Id == studentId)) { Console.WriteLine("Student not found."); return; }
        var course = db.Courses.Find(courseId);
        if (course == null) { Console.WriteLine("Course not found."); return; }
        if (!course.IsPublished) { Console.WriteLine("Course is not published."); return; }
        if (db.Enrollments.Any(x => x.StudentId == studentId && x.CourseId == courseId)) { Console.WriteLine("Student is already enrolled."); return; }
        db.Enrollments.Add(new Enrollment { StudentId = studentId, CourseId = courseId, EnrollmentDate = DateTime.Today, Progress = 0, Status = EnrollmentStatus.Active });
        db.SaveChanges();
        Console.WriteLine("Student enrolled successfully.");
    }
    static void FindCourse(AppDbContext db)
    {
        var byId = db.Courses.Find(1);
        Console.WriteLine(byId == null ? "Course not found by Id." : "By Id: " + byId.Title);
        var byTitle = db.Courses.FirstOrDefault(x => x.Title == "ASP.NET Core");
        Console.WriteLine(byTitle == null ? "Course not found by title." : "By title: " + byTitle.Title);
    }
    static void FilterCourses(AppDbContext db)
    {
        decimal min = 300, max = 850;
        var courses = db.Courses.Where(x => x.Price >= min && x.Price <= max)
            .OrderByDescending(x => x.Price).Select(x => new { x.Title, x.Level, x.Price }).ToList();
        foreach (var course in courses) Console.WriteLine($"{course.Title} - {course.Level} - {course.Price}");
    }
    static void ShowCategories(AppDbContext db)
    {
        int parentId = 1;
        Console.WriteLine("Direct sub-categories:");
        foreach (var category in db.Categories.Where(x => x.ParentCategoryId == parentId).ToList()) Console.WriteLine(category.Name);
        Console.WriteLine("Top-level categories:");
        foreach (var category in db.Categories.Where(x => x.ParentCategoryId == null).ToList()) Console.WriteLine(category.Name);
    }
    static void CourseRoster(AppDbContext db)
    {
        int courseId = 1;
        if (!db.Courses.Any(x => x.Id == courseId)) { Console.WriteLine("Course not found."); return; }
        var studentIds = db.Enrollments.Where(x => x.CourseId == courseId).Select(x => x.StudentId).ToList();
        var students = db.Students.Where(x => studentIds.Contains(x.Id)).ToList();
        Console.WriteLine("Enrolled students: " + students.Count);
        foreach (var student in students) Console.WriteLine(student.FullName);
    }
    static void ChangePrice(AppDbContext db)
    {
        var course = db.Courses.Find(2);
        if (course == null) { Console.WriteLine("Course not found."); return; }
        course.Price = 850;
        db.SaveChanges();
        Console.WriteLine("New price: " + course.Price);
    }
    static void UpdateProgress(AppDbContext db)
    {
        int studentId = db.Students.Where(x => x.Email == "mazen@mail.com").Select(x => x.Id).FirstOrDefault();
        int courseId = 1, progress = 100;
        if (progress < 0 || progress > 100) { Console.WriteLine("Progress must be between 0 and 100."); return; }
        var enrollment = db.Enrollments.Find(studentId, courseId);
        if (enrollment == null) { Console.WriteLine("Enrollment not found."); return; }
        enrollment.Progress = progress;
        if (progress == 100) enrollment.Status = EnrollmentStatus.Completed;
        else if (enrollment.Status == EnrollmentStatus.Completed) { enrollment.Status = EnrollmentStatus.Active; enrollment.FinalGrade = null; }
        db.SaveChanges();
        Console.WriteLine("Progress: " + enrollment.Progress + ", status: " + enrollment.Status);
    }
    static void PublishCourse(AppDbContext db)
    {
        var course = db.Courses.Find(5);
        if (course == null) { Console.WriteLine("Course not found."); return; }
        if (course.IsPublished) { Console.WriteLine("Course already published."); return; }
        course.IsPublished = true;
        course.PublishedDate = DateTime.Today;
        db.SaveChanges();
        Console.WriteLine("Course published on " + course.PublishedDate);
    }
    static void Unenroll(AppDbContext db)
    {
        int studentId = db.Students.Where(x => x.Email == "mazen@mail.com").Select(x => x.Id).FirstOrDefault();
        var enrollment = db.Enrollments.Find(studentId, 1);
        if (enrollment == null) { Console.WriteLine("Enrollment not found."); return; }
        db.Enrollments.Remove(enrollment);
        db.SaveChanges();
        Console.WriteLine("Student unenrolled.");
    }
    static void DeleteCategory(AppDbContext db)
    {
        var category = db.Categories.Find(1);
        if (category == null) { Console.WriteLine("Category not found."); return; }
        try
        {
            db.Categories.Remove(category);
            db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("Cannot delete parent category: " + ex.Message);
            db.Entry(category).State = EntityState.Unchanged;
        }
        int categoryId = 1;
        if (db.Categories.Any(x => x.ParentCategoryId == categoryId))
        {
            Console.WriteLine("Safe delete: category has sub-categories.");
            return;
        }
        var safeCategory = db.Categories.Find(categoryId);
        if (safeCategory == null) { Console.WriteLine("Category not found."); return; }
        if (db.Courses.Any(x => x.CategoryId == categoryId)) { Console.WriteLine("Category has courses."); return; }
        db.Categories.Remove(safeCategory);
        db.SaveChanges();
        Console.WriteLine("Category deleted.");
    }
    static void DeleteCourse(AppDbContext db)
    {
        int courseId = 4;
        var course = db.Courses.Find(courseId);
        if (course == null) { Console.WriteLine("Course not found."); return; }
        int before = db.Enrollments.Count(x => x.CourseId == courseId);
        Console.WriteLine("Enrollments before deleting course: " + before);
        db.Courses.Remove(course);
        db.SaveChanges();
        int after = db.Enrollments.Count(x => x.CourseId == courseId);
        Console.WriteLine("Enrollments after deleting course: " + after);
        Console.WriteLine("Enrollments were removed because the Course relationship uses cascade delete.");
    }
    static void BulkPriceRaise(AppDbContext db)
    {
        int count = db.Courses.Where(x => x.IsPublished && x.Level == CourseLevel.Beginner)
            .ExecuteUpdate(s => s.SetProperty(x => x.Price, x => x.Price * 1.10m));
        Console.WriteLine("Prices updated: " + count);
    }
    static void BulkDelete(AppDbContext db)
    {
        int count = db.Enrollments.Where(x => x.Status == EnrollmentStatus.Dropped).ExecuteDelete();
        Console.WriteLine("Dropped enrollments deleted: " + count);
    }
}
