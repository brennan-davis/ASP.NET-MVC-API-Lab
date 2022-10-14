using Microsoft.AspNetCore.Mvc;
using School.Data.DTOs;
using School.Data.TableModels;
using School.Services.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }


        // GET: api/<CoursesController>
        [HttpGet]
        public ActionResult Get()
        {
            var courses = _service.GetCourses();
            return Ok(courses);
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var course = _service.GetCourseById(id);
            if (course == null) return NotFound("There is no course with an ID of " + id);
            return Ok(course);
        }

        // GET Api/Courses/Studentlist/5
        [HttpGet("StudentList/{id}")]
        public ActionResult GetStudentList(int id)
        {
            var csl = _service.GetStudentsForCourseId(id);
            if (csl == null) return NotFound("There is no course with an ID of " + id);
            return Ok(csl);
        }

        // POST api/<CoursesController>
        [HttpPost]
        public ActionResult Post(string title, string teacher)
        {
            return Ok(_service.CreateCourse(title, teacher));
        }

        // PUT api/<CoursesController>/5
        [HttpPut("Update/{courseId}")]
        public Course Put(int courseId, string? newCourseTitle, string? newCourseTeacher)
        {
            return _service.UpdateCourse(courseId, newCourseTitle, newCourseTeacher);
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("Delete/{courseId}")]
        public bool Delete(int courseId)
        {
            return _service.DeleteCourse(courseId);
        }

    }
}
