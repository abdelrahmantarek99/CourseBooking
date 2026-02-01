using CourseBooking.Api.DTOs.StudentDtos;
using CourseBooking.Api.Entities;
using CourseBooking.Api.Repositories.Interfaces;
using CourseBooking.Api.Services.Interfaces;

namespace CourseBooking.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<StudentResponseDto>> GetAllStudentsAsync()
        {
            try
            {
                var students = await _repo.GetAllAsync();
                return students.Select(s => new StudentResponseDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Phone = s.phone,
                    EnrollmentCount = s.Enrollments.Count
                }).ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<StudentResponseDto?> GetStudentByIdAsync(int id)
        {
            try
            {
                var s = await _repo.GetByIdAsync(id);
                if (s == null) return null;

                return new StudentResponseDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Phone = s.phone,
                    EnrollmentCount = s.Enrollments.Count
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<StudentResponseDto?> CreateStudentAsync(StudentCreateDto dto)
        {
            try
            {
                if (await _repo.ExistsAsync(dto.Email)) return null;

                var student = new Student
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    phone = dto.Phone
                };

                var created = await _repo.AddAsync(student);

                return new StudentResponseDto
                {
                    Id = created.Id,
                    Name = created.Name,
                    Email = created.Email,
                    Phone = created.phone,
                    EnrollmentCount = 0
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<StudentResponseDto?> UpdateStudentAsync(int id, StudentCreateDto dto)
        {
            try
            {
                var student = await _repo.GetByIdAsync(id);
                if (student == null) return null;

                student.Name = dto.Name;
                student.Email = dto.Email;
                student.phone = dto.Phone;

                await _repo.UpdateAsync(student);

                return new StudentResponseDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Phone = student.phone,
                    EnrollmentCount = student.Enrollments.Count
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            try
            {
                var student = await _repo.GetByIdAsync(id);
                if (student == null) return false;

                await _repo.DeleteAsync(student);
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
