using CourseBooking.Api.DTOs.InstructorDtos;
namespace CourseBooking.Api.Services
{
    public interface IInstructorService
    {
        Task<List<InstructorResponseDto>> GetAllInstructorsAsync();
        Task<InstructorResponseDto?> GetInstructorByIdAsync(int id);
        Task<InstructorResponseDto?> CreateInstructorAsync(InstructorCreateDto dto);
        Task<InstructorResponseDto?> UpdateInstructorAsync(int id, InstructorCreateDto dto);
        Task<bool> DeleteInstructorAsync(int id);
    }
}
