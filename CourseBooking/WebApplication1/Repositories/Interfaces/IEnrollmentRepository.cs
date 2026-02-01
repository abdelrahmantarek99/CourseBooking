using CourseBooking.Api.Entities;

namespace CourseBooking.Api.Repositories.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllAsync();
        Task<Enrollment?> GetByIdAsync(int id);
        Task<Enrollment?> GetByStudentAndCourseAsync(int studentId, int courseId);
        Task<Enrollment> AddAsync(Enrollment enrollment);
        Task DeleteAsync(Enrollment enrollment);
    }
}
