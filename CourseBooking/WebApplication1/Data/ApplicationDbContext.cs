using Microsoft.EntityFrameworkCore; // Needed for DbContext and DbSet
using CourseBooking.Api.Entities;      

namespace CourseBooking.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Instructor> Instructors { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;
    }
}
