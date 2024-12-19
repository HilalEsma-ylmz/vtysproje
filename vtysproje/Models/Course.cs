using System.ComponentModel.DataAnnotations;
namespace vtysproje.Models

{
    public class Course
    {
        [Key]

        public int CourseCode { get; set; }

        public string CourseName { get; set; }

        public bool IsMandatory { get; set; }

        public string Credit { get; set; }

        public string Department { get; set; }

        public int? AdvisorID { get; set; }

        public Advisor Advisor { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();

    }
}
