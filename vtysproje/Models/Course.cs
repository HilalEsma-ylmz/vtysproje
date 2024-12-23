using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vtysproje.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; } // Primary Key

        [Required]
        [MaxLength(10)] // Maksimum uzunluk 10 karakter
        public string CourseCode { get; set; } // Örneğin, "CS101"

        [Required]
        [MaxLength(100)] // Maksimum uzunluk 100 karakter
        public string CourseName { get; set; } // Dersin adı

        [Required]
        public bool IsMandatory { get; set; } // Zorunlu olup olmadığını belirtir

        [Required]
        [MaxLength(10)] // Maksimum uzunluk 10 karakter
        public string Credit { get; set; } // Kredisi

        [Required]
        [MaxLength(50)] // Maksimum uzunluk 50 karakter
        public string Department { get; set; } // Bölüm bilgisi

        // Advisor ile ilişki (Optional Foreign Key)
        public int? AdvisorID { get; set; } // Advisor'ın ID'si (nullable)

        [ForeignKey("AdvisorID")]
        public Advisor Advisor { get; set; } // Navigation Property

        // Student ile Many-to-Many İlişki
        public List<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();
    }
}
