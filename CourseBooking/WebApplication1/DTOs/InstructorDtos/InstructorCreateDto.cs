namespace CourseBooking.Api.DTOs.InstructorDtos
{
    public class InstructorCreateDto
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }
    }
}
