using CourseBooking.Api.DTOs.BookingDtos;

namespace CourseBooking.Api.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task<EnrollmentResponseDto?> GetByIdAsync(int id);
        Task<List<EnrollmentResponseDto>> GetAllAsync();
        Task<EnrollmentResponseDto?> CreateAsync(EnrollmentCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
