using School.Data.TableModels;

namespace School.Data.DTOs
{
    public class StudentTeacherList
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public ICollection<string> Teachers { get; set; }
    }
}
