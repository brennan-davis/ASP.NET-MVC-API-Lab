using System.ComponentModel.DataAnnotations;

namespace School.Data.TableModels
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
