using Microsoft.AspNetCore.Mvc;
using School.Data;
using School.Data.TableModels;
using School.Services.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        // GET: api/<StudentsController>
        [HttpGet]
        public ICollection<Student> Get()
        {
            return _service.GetStudents();
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _service.GetStudentById(id);
        }

        // GET api/<StudentsController>/5
        [HttpGet("TeacherList/{id}")]
        public ActionResult GetTeacherList(int id)
        {
            return Ok(_service.GetTeachersForStudentId(id));
        }

        // POST api/<StudentsController>
        [HttpPost]
        public Student Post(string name)
        {
            return _service.CreateStudent(name);
        }

        //
        [HttpPost("Random")]
        public async Task<ActionResult> Post()
        {
            var student = await _service.CreateRandomStudent();
            if (student == null)
                return StatusCode(500);
            else
                return Ok(student);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public Student Put(int id, string name)
        {
            return _service.UpdateStudent(id, name);
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _service.DeleteStudent(id);
        }
    }
}
