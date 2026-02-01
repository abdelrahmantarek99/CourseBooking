using CourseBooking.Api.Repositories.Interfaces;
using CourseBooking.Api.Data;
using CourseBooking.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseBooking.Api.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            try
            {
                return await _db.Students
                                .Include(s => s.Enrollments)
                                .ThenInclude(e => e.Course)
                                .ToListAsync();
            }
            catch
            {
                throw; // propagate exception to service
            }
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            try
            {
                return await _db.Students
                                .Include(s => s.Enrollments)
                                .ThenInclude(e => e.Course)
                                .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Student> AddAsync(Student student)
        {
            try
            {
                _db.Students.Add(student);
                await _db.SaveChangesAsync();
                return student;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(Student student)
        {
            try
            {
                _db.Students.Update(student);
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(Student student)
        {
            try
            {
                _db.Students.Remove(student);
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> ExistsAsync(string email)
        {
            try
            {
                return await _db.Students.AnyAsync(s => s.Email == email);
            }
            catch
            {
                throw;
            }
        }
    }
}
