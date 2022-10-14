using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Data.DTOs;
using School.Data.TableModels;

namespace School.Services.Services
{
    public interface ICourseService
    {
        Course CreateCourse(string title, string teacher);
        ICollection<Course> GetCourses();
        Course? GetCourseById(int id);

        CourseStudentList GetStudentsForCourseId(int id);
        Course UpdateCourse(int courseId, string? newCourseTitle, string? newCourseTeacher);
        bool DeleteCourse(int courseId);
    }

    public class CourseService : ICourseService
    {
        private readonly SchoolDbContext _context;

        public CourseService(SchoolDbContext context)
        {
            _context = context;
        }

        public Course CreateCourse(string title, string teacher)
        {
            Course course = new Course() { Title = title, Teacher = teacher };

            _context.Courses.Add(course);
            _context.SaveChanges();

            return course;
        }

        public Course? GetCourseById(int id)
        {
            return _context.Courses.SingleOrDefault(c => c.Id == id);
        }

        public ICollection<Course> GetCourses()
        {
            return _context.Courses.Include("Enrollments").ToList(); //example of eager loading
        }

        public CourseStudentList GetStudentsForCourseId(int id)
        {
            Course course = _context.Courses.SingleOrDefault(c => c.Id == id);
            ICollection<Student> students = _context.Enrollments.Where(e => e.CourseId == id)
                                                                .Select(e => e.Student)
                                                                .ToList();
            var csl = new CourseStudentList()
            {
                CourseId = course.Id,
                CourseTitle = course.Title,
                CourseTeacher = course.Teacher,
                Students = students
            };

            return csl;
        }

        public Course UpdateCourse(int courseId, string? newCourseTitle, string? newCourseTeacher)
        {
            var course = _context.Courses.SingleOrDefault(c => c.Id == courseId);
            if (newCourseTitle != null)
                course.Title = newCourseTitle;
            if (newCourseTeacher != null)
                course.Teacher = newCourseTeacher;
            _context.SaveChanges();

            return course;
        }

        public bool DeleteCourse(int courseId)
        {
            var course = _context.Courses.Find(courseId);
            if(course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
