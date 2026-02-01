using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseBooking.Api.DTOs.CourseDtos;
using CourseBooking.Api.Services.Interfaces;
using CourseBooking.Api.Repositories.Interfaces;
using CourseBooking.Api.Entities;

namespace CourseBooking.Api.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;

        public CourseService(ICourseRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CourseResponseDto>> GetAllCoursesAsync()
        {
            try
            {
                var courses = await _repo.GetAllAsync();
                return courses.Select(c => new CourseResponseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    DurationHours = c.DurationHours,
                    Description = c.Description,
                    InstructorName = c.Instructor.Name,
                    Price = c.Price,
                }).ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CourseResponseDto?> GetCourseByIdAsync(int id)
        {
            try
            {
                var c = await _repo.GetByIdAsync(id);
                if (c == null) return null;

                return new CourseResponseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    DurationHours = c.DurationHours,
                    Description = c.Description,
                    InstructorName = c.Instructor.Name,
                    Price = c.Price,
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<CourseResponseDto?> CreateCourseAsync(CourseCreateDto dto)
        {
            try
            {
                // Idempotent: check if course exists
                if (await _repo.ExistsAsync(dto.Title, dto.InstructorId))
                    return null;

                var course = new Course
                {
                    Title = dto.Title,
                    InstructorId = dto.InstructorId,
                    DurationHours = dto.DurationHours,
                    Description = dto.Description,
                    Price = dto.Price,
                };

                var created = await _repo.AddAsync(course);
                return new CourseResponseDto
                {
                    Id = created.Id,
                    Title = created.Title,
                    DurationHours = created.DurationHours,
                    Description = created.Description,
                    InstructorName = created.Instructor.Name,
                    Price = created.Price,
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<CourseResponseDto?> UpdateCourseAsync(int id, CourseCreateDto dto)
        {
            try
            {
                var course = await _repo.GetByIdAsync(id);
                if (course == null) return null;

                // Optional: idempotency check — avoid duplicate course for same instructor/title
                if (course.Title != dto.Title &&
                    await _repo.ExistsAsync(dto.Title, dto.InstructorId))
                {
                    throw new InvalidOperationException("A course with this title and instructor already exists.");
                }

                course.Title = dto.Title;
                course.InstructorId = dto.InstructorId;
                course.DurationHours = dto.DurationHours;
                course.Description = dto.Description;
                course.Price = dto.Price;

                await _repo.UpdateAsync(course);

                return new CourseResponseDto
                {
                    Id = course.Id,
                    Title = course.Title,
                    DurationHours = course.DurationHours,
                    Description = course.Description,
                    InstructorName = course.Instructor.Name,
                    Price = course.Price,
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                var course = await _repo.GetByIdAsync(id);
                if (course == null) return false; // Not found

                await _repo.DeleteAsync(course);
                return true; // Successfully deleted
            }
            catch
            {
                throw;
            }
        }
    }
}
