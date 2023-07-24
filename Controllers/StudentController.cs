
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.dto;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StudentController : ControllerBase

    {
        [HttpGet]
        public ActionResult<IEnumerable<StudentDto>> GetStudent()
        {
            return Ok(StuddentStored.StudentList);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [ProducesResponseType(200 , Type=typeof(StudentDto))]
        public ActionResult<StudentDto> GetStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var stud = StuddentStored.StudentList.FirstOrDefault(u=>u.Id == id);
            if (stud == null)
            {
                return NotFound();
            }
            return Ok(stud);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<StudentDto> CreateStudent([FromBody] StudentDto studentdto)
        {
            if (studentdto == null)
            {
                return BadRequest(studentdto);
            }
            if (studentdto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            studentdto.Id = StuddentStored.StudentList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
           
            StuddentStored.StudentList.Add(studentdto);
            return Ok(studentdto);
        }
    }
}