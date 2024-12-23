using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace vtysproje.Models
{
    public class CourseStudent
    {
        [Key]
        public int SelectionID { get; set; } // Primary Key

        [Required]
        public int StudentID { get; set; } // Foreign Key to Students

        [Required]
        public string CourseName { get; set; } // Foreign Key to Courses

        [Required]
        public DateTime SelectionDate { get; set; } // Seçim tarihi

        

        public bool IsApproved { get; set; } = false; // Onay durumu (Varsayılan: false)

        // Navigation Properties
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("CourseID")]

        public int CourseID { get; set; }
        public virtual Course Course { get; set; }
    }
}
