using System.ComponentModel.DataAnnotations;
namespace vtysproje.Models
{
    public class Advisor
    {
        [Key]

        public int AdvisorID {  get; set; }
        
        public string FullName { get; set; }

        public string Title { get; set; }

        public string Department { get; set; }

        public string Email { get; set; }

        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
