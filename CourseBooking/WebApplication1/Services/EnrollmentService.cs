using CourseBooking.Api.DTOs.BookingDtos;
using CourseBooking.Api.Entities;
using CourseBooking.Api.Repositories.Interfaces;
using CourseBooking.Api.Services.Interfaces;

namespace CourseBooking.Api.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repo;
        private readonly IStudentRepository _studentRepo;
        private readonly ICourseRepository _courseRepo;

        public EnrollmentService(
            IEnrollmentRepository repo,
            IStudentRepository studentRepo,
            ICourseRepository courseRepo)
        {
            _repo = repo;
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
        }


        public async Task<EnrollmentResponseDto?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _repo.GetByIdAsync(id);
                if (e == null) return null;

                return new EnrollmentResponseDto
                {
                    Id = e.Id,
                    StudentName = e.Student.Name,
                    CourseTitle = e.Course.Title,
                    EnrolledAt = e.EnrolledAt
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<EnrollmentResponseDto>> GetAllAsync()
        {
            try
            {
                var enrollments = await _repo.GetAllAsync();

                return enrollments.Select(e => new EnrollmentResponseDto
                {
                    Id = e.Id,
                    StudentName = e.Student.Name,
                    CourseTitle = e.Course.Title,
                    EnrolledAt = e.EnrolledAt
                }).ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<EnrollmentResponseDto?> CreateAsync(EnrollmentCreateDto dto)
        {
            try
            {
                // Validation
                var student = await _studentRepo.GetByIdAsync(dto.StudentId);
                var course = await _courseRepo.GetByIdAsync(dto.CourseId);

                if (student == null || course == null)
                    throw new ArgumentException("Student or Course not found");

                // Idempotency check
                var existing = await _repo.GetByStudentAndCourseAsync(dto.StudentId, dto.CourseId);
                if (existing != null) return null;

                var enrollment = new Enrollment
                {
                    StudentId = dto.StudentId,
                    CourseId = dto.CourseId
                };

                var created = await _repo.AddAsync(enrollment);

                return new EnrollmentResponseDto
                {
                    Id = created.Id,
                    StudentName = student.Name,
                    CourseTitle = course.Title,
                    EnrolledAt = created.EnrolledAt
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var enrollment = await _repo.GetByIdAsync(id);
                if (enrollment == null) return false;

                await _repo.DeleteAsync(enrollment);
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
