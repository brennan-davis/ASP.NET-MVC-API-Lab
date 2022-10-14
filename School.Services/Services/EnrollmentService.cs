using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Data.TableModels;

namespace School.Services.Services
{
    public interface IEnrollmentService
    {
        Enrollment CreateEnrollment(int courseId, int studentId);
        ICollection<Enrollment> GetEnrollments();
        Enrollment? GetEnrollmentById(int id);
        ICollection<Enrollment> GetEnrollmentsByStudentId(int id);
        bool DeleteEnrollment(int enrollmentId);
    }

    public class EnrollmentService : IEnrollmentService
    {
        private readonly SchoolDbContext _context;

        public EnrollmentService(SchoolDbContext context)
        {
            _context = context;
        }

        public Enrollment CreateEnrollment(int courseId, int studentId)
        {
            Enrollment enrollment = new Enrollment()
            {
                CourseId = courseId,
                StudentId = studentId
            };

            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();

            return enrollment;
        }

        public ICollection<Enrollment> GetEnrollments()
        {
            return _context.Enrollments.ToList();
        }

        public Enrollment? GetEnrollmentById(int id)
        {
            return _context.Enrollments.SingleOrDefault(e => e.Id == id);
        }

        public ICollection<Enrollment> GetEnrollmentsByStudentId(int id)
        {
            var e = _context.Enrollments.Include(e => e.Course)
                .Where(e => e.StudentId == id)
                .ToList();
            return e;
        }

        public bool DeleteEnrollment(int enrollmentId)
        {
            var enrollment = _context.Enrollments.Find(enrollmentId);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
