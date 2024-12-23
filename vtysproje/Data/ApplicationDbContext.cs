using Microsoft.EntityFrameworkCore;
using vtysproje.Models;

namespace vtysproje.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        // DbSet tanımları
        public DbSet<Student> Students { get; set; }
        public DbSet<Advisor> Advisors { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseStudent> CourseStudents { get; set; } // Tek bir DbSet tanımı yeterlidir.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Course ve CourseStudent arasındaki ilişki
            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(cs => cs.CourseID);

            // Student ve CourseStudent arasındaki ilişki
            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StudentID);
        }
    }
}

