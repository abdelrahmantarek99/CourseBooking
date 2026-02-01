using CourseBooking.Api.Entities;

namespace CourseBooking.Api.Repositories.Interfaces
{
    public interface IInstructorRepository
    {
        Task<List<Instructor>> GetAllAsync();
        Task<Instructor?> GetByIdAsync(int id);
        Task<Instructor> AddAsync(Instructor instructor);
        Task UpdateAsync(Instructor instructor);
        Task DeleteAsync(Instructor instructor);
        Task<bool> ExistsAsync(string name);
    }
}