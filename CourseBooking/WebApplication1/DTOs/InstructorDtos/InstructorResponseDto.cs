namespace CourseBooking.Api.DTOs.InstructorDtos
{
    public class InstructorResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public int CourseCount { get; set; } // optional: number of courses taught
    }
}
