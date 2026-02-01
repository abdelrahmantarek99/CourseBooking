using CourseBooking.Api.Data;
using CourseBooking.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseBooking.Api.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<Course> AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Course course);
        Task<bool> ExistsAsync(string title, int instructorId);

    }
}
