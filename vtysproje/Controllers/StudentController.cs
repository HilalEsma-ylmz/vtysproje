using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vtysproje.Data;
using vtysproje.Models;

namespace vtysproje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Öğrencinin mevcut ders seçimleri
        [HttpGet("getSelections/{studentId}")]
        public async Task<ActionResult<IEnumerable<CourseStudent>>> GetSelections(int studentId)
        {
            var selections = await _context.CourseStudent
                .Include(s => s.Course)
                .Where(s => s.StudentID == studentId)
                .ToListAsync();

            return selections;
        }

        // POST: Öğrenci ders seçimi yapar
        [HttpPost("addSelection")]
        public async Task<ActionResult<CourseStudent>> AddSelection(CourseStudent selection)
        {
            var studentExists = await _context.Students.AnyAsync(s => s.StudentID == selection.StudentID);
            var courseExists = await _context.Courses.AnyAsync(c => c.CourseCode == selection.CourseID);

            if (!studentExists || !courseExists)
            {
                return BadRequestResult(new { Message = "Invalid Student or Course ID" });
            }

            _context.CourseStudent.Add(selection);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSelections), new { studentId = selection.StudentID }, selection);
        }

        private ActionResult<CourseStudent> CreatedAtAction(string v, object value, CourseStudent selection)
        {
            throw new NotImplementedException();
        }

        private ActionResult<CourseStudent> BadRequestResult(object value)
        {
            throw new NotImplementedException();
        }
    }   
}
