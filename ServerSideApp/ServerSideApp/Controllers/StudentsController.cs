using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSideApp.DTOs;
using ServerSideApp.Services;
using ServerSideApp.Services.StudentService;

namespace ServerSideApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetAllStudents")]

        public async Task<ActionResult<ServiceResponse<List<StudentDTO>>>> GetAllStudents()
        {
            var res = await _studentService.GetAllStudents();

            if (res.Data != null)
            {
                return Ok(res.Data);
            }
            else
            {
                return BadRequest(res.Message);
            }
        }


        [HttpGet("GetStudentById/{id}", Name ="GetStudentById")]
        public async Task<ActionResult<ServiceResponse<StudentDTO>>> GetStudentById(int id)
        {
            var res = await _studentService.GetStudentById(id);

            if (res.Data != null)
            {
                return Ok(res.Data);
            }
            else
            {
                return BadRequest(res.Message);
            }
        }


        [HttpPost("CreateStudent")]
        public async Task<ActionResult<ServiceResponse<StudentDTO>>> CreateStudent([FromBody] StudentDTO newStudent)
        {
            var res = await _studentService.CreateStudent(newStudent);

            // -1 tells us that we have exception when we have tried to create new student.
            if(res.Data != -1)
            {
                // retunrs 201
                return CreatedAtRoute("GetStudentById", new { id = res.Data }, newStudent);
            }
            else
            {
                return BadRequest(res.Message);
            }

        }

    }
}
