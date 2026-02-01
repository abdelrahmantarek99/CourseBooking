using Microsoft.AspNetCore.Mvc;
using CourseBooking.Api.DTOs.InstructorDtos;
using CourseBooking.Api.Services;

namespace CourseBooking.Api.Controllers
{
    [ApiController]
    [Route("api/instructors")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _service;

        public InstructorController(IInstructorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var instructors = await _service.GetAllInstructorsAsync();
                return Ok(instructors);
            }
            catch (Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while fetching instructors."
                );
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var instructor = await _service.GetInstructorByIdAsync(id);
                if (instructor == null)
                    return NotFound($"Instructor with id {id} was not found.");

                return Ok(instructor);
            }
            catch (Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while fetching the instructor."
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InstructorCreateDto dto)
        {
            try
            {
                var created = await _service.CreateInstructorAsync(dto);
                if (created == null)
                    return Conflict("Instructor with this name already exists.");

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
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while creating the instructor."
                );
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] InstructorCreateDto dto)
        {
            try
            {
                var updated = await _service.UpdateInstructorAsync(id, dto);
                if (updated == null)
                    return NotFound($"Instructor with id {id} was not found.");

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
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while updating the instructor."
                );
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteInstructorAsync(id);
                if (!deleted)
                    return NotFound($"Instructor with id {id} was not found.");

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while deleting the instructor."
                );
            }
        }
    }
}
