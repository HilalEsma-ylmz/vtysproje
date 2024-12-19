using System.ComponentModel.DataAnnotations;
namespace vtysproje.Models

{
    public class Course
    {
        [Key]

        public int StudentID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int? AdvisorID { get; set; }

        public Advisor Advisor { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();

    }
}
