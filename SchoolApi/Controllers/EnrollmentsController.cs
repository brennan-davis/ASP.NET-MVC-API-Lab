using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Services;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentsController(IEnrollmentService service)
        {
            _service = service;
        }


        // GET: api/<EnrollmentsController>
        [HttpGet]
        public ActionResult Get()
        {
            var enrollments = _service.GetEnrollments();
            return Ok(enrollments);
        }

        // GET api/<EnrollmentsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var enrollment = _service.GetEnrollmentById(id);
            if (enrollment == null) return NotFound("There is no enrollment with an ID of " + id);
            return Ok(enrollment);
        }

        // GET api/Enrollments/student/5
        [HttpGet("student/{studentId}")]
        public ActionResult GetByStudentId(int studentId)
        {
            var enrollments = _service.GetEnrollmentsByStudentId(studentId);
            return Ok(enrollments);
        }
        // POST api/<EnrollmentsController>
        [HttpPost]
        public ActionResult Post(int courseId, int studentId)
        {
            return Ok(_service.CreateEnrollment(courseId, studentId));
        }

        //// PUT api/<EnrollmentsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EnrollmentsController>/5
        [HttpDelete("Delete/{enrollmentId}")]
        public bool Delete(int enrollmentId)
        {
            return _service.DeleteEnrollment(enrollmentId);
        }

    }
}
