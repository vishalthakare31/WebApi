
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.dto;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]                 // it can be checked Validation from the model class also like Required
                                       // but if we dont used ApiController Attributr here we need to add validation explicitly
                                       // like  if(!ModelState.IsValid) we do this explictly in CreateStudent Method
    public class StudentController : ControllerBase

    {
        [HttpGet]
        public ActionResult<IEnumerable<StudentDto>> GetStudent()
        {
            return Ok(StuddentStored.StudentList);
        }

        [HttpGet("{id:int}" ,Name ="GetStudent")]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<StudentDto> CreateStudent([FromBody] StudentDto studentdto)
        {
            
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if(StuddentStored.StudentList.FirstOrDefault(u =>u.Name.ToLower() == studentdto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Name Is alread Exist");
                return BadRequest(ModelState);
            }
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
            return CreatedAtRoute("GetStudent" , new { id =studentdto.Id}, studentdto);

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{ id:int}", Name = "DeleteStudent")]

        public ActionResult DeleteStudent(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var stud = StuddentStored.StudentList.FirstOrDefault(u => u.Id == id);
            if(stud == null)
            {
                return NotFound();
            }
            StuddentStored.StudentList.Remove(stud);
            return NoContent();

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{ id:int}", Name = "UpdateStudent")]

        public IActionResult UpdateStudent(int id, [FromBody] StudentDto studentdto)
        {
            if (studentdto == null || id != studentdto.Id)
            {
                return BadRequest();
            }
            var stud = StuddentStored.StudentList.FirstOrDefault(u => u.Id == id);
            stud.Name = studentdto.Name;
            stud.City = studentdto.City;

            return NoContent();

        }
    }
}