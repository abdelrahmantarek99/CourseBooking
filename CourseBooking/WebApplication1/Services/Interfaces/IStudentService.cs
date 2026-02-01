using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseBooking.Api.DTOs.StudentDtos;

namespace CourseBooking.Api.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentResponseDto>> GetAllStudentsAsync();
        Task<StudentResponseDto?> GetStudentByIdAsync(int id);
        Task<StudentResponseDto?> CreateStudentAsync(StudentCreateDto dto);
        Task<StudentResponseDto?> UpdateStudentAsync(int id, StudentCreateDto dto);
        Task<bool> DeleteStudentAsync(int id);
    }
}
