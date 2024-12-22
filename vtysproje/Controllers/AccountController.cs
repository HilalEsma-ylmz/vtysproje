using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vtysproje.Data;

namespace vtysproje.Controllers
{
        public class AccountController : Controller
        {
            private readonly ApplicationDbContext _context;

            public AccountController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: Giriş ekranı
            public IActionResult Login()
            {
                return View();
            }

            // POST: Giriş işlemi
            [HttpPost]
            public async Task<IActionResult> Login(string role, string email, string password)
            {
                if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ViewBag.Message = "Lütfen tüm alanları doldurun.";
                    return View();
                }

                if (role == "Student")
                {
                    // Öğrenci kontrolü
                    var student = await _context.Students
                        .FirstOrDefaultAsync(s => s.Email == email && s.LastName == password); // Örnek: Şifre olarak soyadı kullanılıyor

                    if (student != null)
                    {
                        // Başarılı giriş, öğrenci sayfasına yönlendir
                        return RedirectToAction("CourseSelection", "Student", new { id = student.StudentID });
                    }
                }
                else if (role == "Advisor")
                {
                    // Danışman kontrolü
                    var advisor = await _context.Advisors
                        .FirstOrDefaultAsync(a => a.Email == email && a.FullName == password); // Örnek: Şifre olarak ad soyad kullanılıyor

                    if (advisor != null)
                    {
                        // Başarılı giriş, danışman sayfasına yönlendir
                        return RedirectToAction("ApproveCourses", "Advisor", new { id = advisor.AdvisorID });
                    }
                }

                // Giriş başarısız
                ViewBag.Message = "Geçersiz giriş bilgileri.";
                return View();
            }
        }

}
