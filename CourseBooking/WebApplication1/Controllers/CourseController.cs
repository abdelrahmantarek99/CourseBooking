using Microsoft.AspNetCore.Mvc;
using CourseBooking.Api.DTOs.CourseDtos;
using CourseBooking.Api.Services.Interfaces;

namespace CourseBooking.Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;

        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var courses = await _service.GetAllCoursesAsync();
                return Ok(courses);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while fetching courses.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var course = await _service.GetCourseByIdAsync(id);
                if (course == null)
                    return NotFound($"Course with id {id} was not found.");

                return Ok(course);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while fetching the course.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
        {
            try
            {
                var created = await _service.CreateCourseAsync(dto);
                if (created == null)
                    return Conflict("Course with the same title and instructor already exists.");

                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while creating the course.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseCreateDto dto)
        {
            try
            {
                var updated = await _service.UpdateCourseAsync(id, dto);
                if (updated == null)
                    return NotFound($"Course with id {id} was not found.");

                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while updating the course.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteCourseAsync(id);
                if (!deleted)
                    return NotFound($"Course with id {id} was not found.");

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while deleting the course.");
            }
        }
    }
}