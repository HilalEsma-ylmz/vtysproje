using Microsoft.EntityFrameworkCore;
using vtysproje.Models;

namespace vtysproje.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Student> Students { get; set; }

        public DbSet<Advisor> Advisors { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseStudent> CourseStudent { get; set; }
    }
}
