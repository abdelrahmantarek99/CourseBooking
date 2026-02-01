using CourseBooking.Api.Data;
using CourseBooking.Api.Entities;
using CourseBooking.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseBooking.Api.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _db;

        public EnrollmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Enrollment>> GetAllAsync()
        {
            try
            {
                return await _db.Enrollments
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Enrollment?> GetByIdAsync(int id)
        {
            try
            {
                return await _db.Enrollments
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Enrollment?> GetByStudentAndCourseAsync(int studentId, int courseId)
        {
            try
            {
                return await _db.Enrollments
                    .FirstOrDefaultAsync(e =>
                        e.StudentId == studentId &&
                        e.CourseId == courseId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Enrollment> AddAsync(Enrollment enrollment)
        {
            try
            {
                _db.Enrollments.Add(enrollment);
                await _db.SaveChangesAsync();
                return enrollment;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(Enrollment enrollment)
        {
            try
            {
                _db.Enrollments.Remove(enrollment);
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
