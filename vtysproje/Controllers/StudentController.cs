using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vtysproje.Data;
using vtysproje.Models;

namespace vtysproje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller // Değişiklik burada: ControllerBase yerine Controller kullanıldı
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Öğrencinin mevcut ders seçimlerini döndürür (API)
        [HttpGet("getSelections/{studentId}")]
        public async Task<ActionResult<IEnumerable<CourseStudent>>> GetSelections(int studentId)
        {
            var selections = await _context.CourseStudents
                .Include(cs => cs.Course)
                .Where(cs => cs.StudentID == studentId)
                .ToListAsync();

            if (selections == null || selections.Count == 0)
            {
                return NotFound(new { Message = "No selections found for this student." });
            }

            return Ok(selections);
        }

        // POST: Öğrenci ders seçimi yapar (API)
        [HttpPost("addSelection")]
        public async Task<ActionResult<CourseStudent>> AddSelection(CourseStudent selection)
        {
            // Öğrenci var mı kontrol et
            var studentExists = await _context.Students.AnyAsync(s => s.StudentID == selection.StudentID);
            if (!studentExists)
            {
                return BadRequest(new { Message = "Invalid Student ID" });
            }

            // Ders var mı kontrol et
            var courseExists = await _context.Courses.AnyAsync(c => c.CourseName == selection.CourseName);
            if (!courseExists)
            {
                return BadRequest(new { Message = "Invalid Course Name" });
            }

            // Daha önce seçilmiş mi kontrol et
            var alreadyExists = await _context.CourseStudents.AnyAsync(cs =>
                cs.StudentID == selection.StudentID && cs.CourseName == selection.CourseName);
            if (alreadyExists)
            {
                return BadRequest(new { Message = "This course is already selected by the student." });
            }

            // Yeni seçim ekle
            _context.CourseStudents.Add(selection);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSelections), new { studentId = selection.StudentID }, selection);
        }

        // GET: Öğrencinin seçimlerini bir HTML sayfasında gösterir (MVC)
        [HttpGet("CourseSelection/{id}")]
        public IActionResult CourseSelection(int id)
        {
            var selections = _context.CourseStudents
                .Include(cs => cs.Course)
                .Where(cs => cs.StudentID == id)
                .ToList();

            if (selections == null || !selections.Any())
            {
                return NotFound("Seçimler bulunamadı.");
            }

            return View(selections); // `Views/Student/CourseSelection.cshtml` dosyasına yönlendirilir.
        }
        [HttpGet("GetStudentInfo/{id}")]
        public async Task<IActionResult> GetStudentInfo(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentID == id);

            if (student == null)
            {
                return NotFound(new { Message = "Student not found" });
            }

            return View(student); // Öğrenci bilgilerini ilgili View'a gönder
        }
    }
}
