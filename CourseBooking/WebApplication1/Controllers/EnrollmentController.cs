using Microsoft.AspNetCore.Mvc;
using CourseBooking.Api.DTOs.BookingDtos;
using CourseBooking.Api.Services.Interfaces;

namespace CourseBooking.Api.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentController(IEnrollmentService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var enrollments = await _service.GetAllAsync();
                return Ok(enrollments);
            }
            catch (Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while fetching enrollments."
                );
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var enrollment = await _service.GetByIdAsync(id);
                if (enrollment == null) return NotFound();
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnrollmentCreateDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);

                if (result == null)
                    return Conflict("Student is already enrolled in this course.");

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
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
                    "An unexpected error occurred while creating the enrollment."
                );
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);

                if (!deleted)
                    return NotFound($"Enrollment with id {id} was not found.");

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while deleting the enrollment."
                );
            }
        }
    }
}
