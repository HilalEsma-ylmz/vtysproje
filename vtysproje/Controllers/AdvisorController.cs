using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vtysproje.Data;
using vtysproje.Models;

namespace vtysproje.Controllers
{
    public class AdvisorController : Controller // ControllerBase yerine Controller kullanıldı
    {
        private readonly ApplicationDbContext _context;

        public AdvisorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Danışmanın öğrencilerinin listesi
        [HttpGet]
        public async Task<IActionResult> GetStudents(int advisorId)
        {
            var students = await _context.Students
                .Where(s => s.AdvisorID == advisorId)
                .ToListAsync();

            return View(students); // Görünüm (view) döndürülüyor
        }

        // GET: Danışmanın onay bekleyen ders seçimleri
        [HttpGet]
        public async Task<IActionResult> GetPendingSelections(int advisorId)
        {
            var selections = await _context.CourseStudents
                .Include(s => s.Student)
                .Include(s => s.Course)
                .Where(s => s.Student.AdvisorID == advisorId && s.IsApproved == false)
                .ToListAsync();

            return View(selections); // Görünüm (view) döndürülüyor
        }

        // POST: Ders seçimini onayla
        [HttpPost]
        public async Task<IActionResult> ApproveSelection(int selectionId)
        {
            var selection = await _context.CourseStudents.FindAsync(selectionId);
            if (selection == null)
            {
                return NotFound(new { Message = "Selection not found" });
            }

            selection.IsApproved = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("GetPendingSelections", new { advisorId = selection.Student.AdvisorID }); // Onaydan sonra tekrar onay bekleyen dersleri getir
        }
        [HttpGet]
        public async Task<IActionResult> GetAdvisorInfo(int advisorId)
        {
            // Danışmanın verilerini getiriyoruz
            var advisor = await _context.Advisors
                .FirstOrDefaultAsync(a => a.AdvisorID == advisorId);

            if (advisor == null)
            {
                return NotFound("Danışman bulunamadı.");
            }

            return View(advisor); // Görünümü geri döndürüyoruz
        }

    }
}
