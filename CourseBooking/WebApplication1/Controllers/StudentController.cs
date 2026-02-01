using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourseBooking.Api.Data;
using CourseBooking.Api.Entities;
using CourseBooking.Api.DTOs.StudentDtos;
using CourseBooking.Api.Services.Interfaces;
namespace CourseBooking.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

         [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var students = await _service.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while fetching students.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var student = await _service.GetStudentByIdAsync(id);
                if (student == null)
                    return NotFound($"Student with id {id} was not found.");

                return Ok(student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while fetching the student.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto dto)
        {
            try
            {
                var created = await _service.CreateStudentAsync(dto);
                if (created == null)
                    return Conflict("Student with this email already exists.");

                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while creating the student.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentCreateDto dto)
        {
            try
            {
                var updated = await _service.UpdateStudentAsync(id, dto);
                if (updated == null)
                    return NotFound($"Student with id {id} was not found.");

                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while updating the student.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteStudentAsync(id);
                if (!deleted)
                    return NotFound($"Student with id {id} was not found.");

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while deleting the student.");
            }
        }
    }
}
