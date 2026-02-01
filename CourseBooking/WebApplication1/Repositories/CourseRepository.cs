using CourseBooking.Api.Data;
using CourseBooking.Api.Entities;
using CourseBooking.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseBooking.Api.Repositories
{
        public class CourseRepository : ICourseRepository
        {
            private readonly ApplicationDbContext _db;

            public CourseRepository(ApplicationDbContext db)
            {
                _db = db;
            }

            public async Task<List<Course>> GetAllAsync()
            {
                try
                {
                    return await _db.Courses
                                    .Include(c => c.Instructor)
                                    .ToListAsync();
                }
                catch
                {
                    throw;
                }
            }

            public async Task<Course?> GetByIdAsync(int id)
            {
                try
                {
                    return await _db.Courses
                                    .Include(c => c.Instructor)
                                    .FirstOrDefaultAsync(c => c.Id == id);
                }
                catch
                {
                    throw;
                }
            }

            public async Task<Course> AddAsync(Course course)
            {
                try
                {
                    _db.Courses.Add(course);
                    await _db.SaveChangesAsync();

                    // Reload with Instructor included
                    return await _db.Courses
                        .Include(c => c.Instructor)
                        .FirstAsync(c => c.Id == course.Id);
                }   
                catch
                {
                    throw;
                }
            }

            public async Task UpdateAsync(Course course)
            {
                try
                {
                    _db.Courses.Update(course);
                    await _db.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }
            }

            public async Task DeleteAsync(Course course)
            {
                try
                {
                    _db.Courses.Remove(course);
                    await _db.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }
            }

            public async Task<bool> ExistsAsync(string title, int instructorId)
            {
                try
                {
                    return await _db.Courses.AnyAsync(c => c.Title == title && c.InstructorId == instructorId);
                }
                catch
                {
                    throw;
                }
            }
    }
}
