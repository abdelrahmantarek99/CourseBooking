namespace CourseBooking.Api.DTOs.StudentDtos
{
    public class StudentCreateDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
