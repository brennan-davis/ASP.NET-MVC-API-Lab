using School.Data;
using School.Data.DTOs;
using School.Data.TableModels;
using System.Net.Http.Json;

namespace School.Services.Services
{
    public interface IStudentService
    {
        Student CreateStudent(string name);
        ICollection<Student> GetStudents();
        Student GetStudentById(int id);
        Student UpdateStudent(int id, string newName);
        bool DeleteStudent(int id);
        Task<Student> CreateRandomStudent();
        StudentTeacherList GetTeachersForStudentId(int id);
    }

    public class StudentService : IStudentService
    {
        private readonly SchoolDbContext _context;
        private readonly HttpClient _client;

        public StudentService(SchoolDbContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

        public Student CreateStudent(string name)
        {
            var student = new Student()
            {
                Name = name
            };

            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public async Task<Student> CreateRandomStudent()
        {
            var nameFakePerson = await _client.GetFromJsonAsync<NameFakePerson>("https://api.namefake.com/");

            var student = new Student()
            {
                Name = nameFakePerson.Name
            };

            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.Find(id);
        }

        public ICollection<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public Student UpdateStudent(int id, string newName)
        {
            var student = GetStudentById(id);
            student.Name = newName;
            _context.SaveChanges();
            return student;
        }

        public bool DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public StudentTeacherList GetTeachersForStudentId(int id)
        {
            StudentTeacherList stl = _context.Students.Where(x => x.Id == id)
                                                      .Select(s => new StudentTeacherList()
                                                      {
                                                        StudentId = id,
                                                        StudentName = s.Name,
                                                        Teachers = s.Enrollments.Select(e => e.Course.Teacher).ToList()
                                                      }).FirstOrDefault();
            return stl;
        }
    }
}
