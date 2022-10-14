using System.ComponentModel.DataAnnotations;

namespace School.Data.TableModels
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public string Teacher { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
