using CourseBooking.Api.DTOs.InstructorDtos;
using CourseBooking.Api.Entities;
using CourseBooking.Api.Repositories.Interfaces;

namespace CourseBooking.Api.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _repo;

        public InstructorService(IInstructorRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<InstructorResponseDto>> GetAllInstructorsAsync()
        {
            try
            {
                var instructors = await _repo.GetAllAsync();
                return instructors.Select(i => new InstructorResponseDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Email = i.Email,
                    Bio = i.Bio,
                    CourseCount = i.Courses.Count
                }).ToList();
            }
            catch
            {
                throw; // exception will bubble up to controller
            }
        }

        public async Task<InstructorResponseDto?> GetInstructorByIdAsync(int id)
        {
            try
            {
                var i = await _repo.GetByIdAsync(id);
                if (i == null) return null;

                return new InstructorResponseDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Email = i.Email,
                    Bio = i.Bio,
                    CourseCount = i.Courses.Count
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<InstructorResponseDto?> CreateInstructorAsync(InstructorCreateDto dto)
        {
            try
            {
                if (await _repo.ExistsAsync(dto.Name)) return null;

                var instructor = new Instructor
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Bio = dto.Bio
                };

                var created = await _repo.AddAsync(instructor);

                return new InstructorResponseDto
                {
                    Id = created.Id,
                    Name = created.Name,
                    Email = created.Email,
                    Bio = created.Bio,
                    CourseCount = 0
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<InstructorResponseDto?> UpdateInstructorAsync(int id, InstructorCreateDto dto)
        {
            try
            {
                var instructor = await _repo.GetByIdAsync(id);
                if (instructor == null) return null;

                instructor.Name = dto.Name;
                instructor.Email = dto.Email;
                instructor.Bio = dto.Bio;

                await _repo.UpdateAsync(instructor);

                return new InstructorResponseDto
                {
                    Id = instructor.Id,
                    Name = instructor.Name,
                    Email = instructor.Email,
                    Bio = instructor.Bio,
                    CourseCount = instructor.Courses.Count
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteInstructorAsync(int id)
        {
            try
            {
                var instructor = await _repo.GetByIdAsync(id);
                if (instructor == null) return false;

                await _repo.DeleteAsync(instructor);
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
