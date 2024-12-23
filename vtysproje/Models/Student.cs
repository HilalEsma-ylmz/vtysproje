using System.ComponentModel.DataAnnotations;
namespace vtysproje.Models
{
    public class Student
    {
        [Key]

        public int StudentID { get; set; }

        public string FirstName  { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int? AdvisorID { get; set; }

        public Advisor Advisor { get; set; }

        public string Password { get; set; }


        public List<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();
    }
}
