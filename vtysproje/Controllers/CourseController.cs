using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vtysproje.Data;
using vtysproje.Models;

namespace vtysproje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tüm derslerin listesi
        [HttpGet("getAllCourses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        // POST: Yeni ders ekle
        [HttpPost("addCourse")]
        public async Task<ActionResult<Course>> AddCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllCourses), new { id = course.CourseName }, course);
        }

        private ActionResult<Course> CreatedAtAction(string v, object value, Course course)
        {
            throw new NotImplementedException();
        }
    }
}
