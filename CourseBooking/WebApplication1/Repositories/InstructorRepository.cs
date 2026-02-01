using CourseBooking.Api.Data;
using CourseBooking.Api.Entities;
using CourseBooking.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseBooking.Api.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationDbContext _db;

        public InstructorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Instructor>> GetAllAsync()
        {
            try
            {
                return await _db.Instructors
                                .Include(i => i.Courses)
                                .ToListAsync();
            }
            catch
            {
                throw; // re-throw to service layer
            }
        }

        public async Task<Instructor?> GetByIdAsync(int id)
        {
            try
            {
                return await _db.Instructors
                                .Include(i => i.Courses)
                                .FirstOrDefaultAsync(i => i.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Instructor> AddAsync(Instructor instructor)
        {
            try
            {
                _db.Instructors.Add(instructor);
                await _db.SaveChangesAsync();
                return instructor;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(Instructor instructor)
        {
            try
            {
                _db.Instructors.Update(instructor);
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(Instructor instructor)
        {
            try
            {
                _db.Instructors.Remove(instructor);
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> ExistsAsync(string name)
        {
            try
            {
                return await _db.Instructors.AnyAsync(i => i.Name == name);
            }
            catch
            {
                throw;
            }
        }
    }
}
