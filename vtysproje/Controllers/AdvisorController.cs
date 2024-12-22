using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vtysproje.Data;
using vtysproje.Models;

namespace vtysproje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorController
    {
        private readonly ApplicationDbContext _context;

        public AdvisorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Danışmanın öğrencilerinin listesi
        [HttpGet("getStudents/{advisorId}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents(int advisorId)
        {
            var students = await _context.Students
                .Where(s => s.AdvisorID == advisorId)
                .ToListAsync();

            return students;
        }

        // GET: Danışmanın onay bekleyen ders seçimleri
        [HttpGet("getPendingSelections/{advisorId}")]
        public async Task<ActionResult<IEnumerable<CourseStudent>>> GetPendingSelections(int advisorId)
        {
            var selections = await _context.CourseStudent
                .Include(s => s.Student)
                .Include(s => s.Course)
                .Where(s => s.Student.AdvisorID == advisorId && s.IsApproved == false)
                .ToListAsync();

            return selections;
        }

        // POST: Ders seçimini onayla
        [HttpPost("approveSelection/{selectionId}")]
        public async Task<IActionResult> ApproveSelection(int selectionId)
        {
            var selection = await _context.CourseStudent.FindAsync(selectionId);
            if (selection == null)
            {
                return NotFound(new { Message = "Selection not found" });
            }

            selection.IsApproved = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private IActionResult NoContent()
        {
            throw new NotImplementedException();
        }

        private IActionResult NotFound(object value)
        {
            throw new NotImplementedException();
        }
    }
}
