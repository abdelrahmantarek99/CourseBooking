using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseBooking.Api.DTOs.CourseDtos;

namespace CourseBooking.Api.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseResponseDto>> GetAllCoursesAsync();
        Task<CourseResponseDto?> GetCourseByIdAsync(int id);
        Task<CourseResponseDto?> CreateCourseAsync(CourseCreateDto dto);
        Task<CourseResponseDto?> UpdateCourseAsync(int id, CourseCreateDto dto);
        Task<bool> DeleteCourseAsync(int id);
    }
}
