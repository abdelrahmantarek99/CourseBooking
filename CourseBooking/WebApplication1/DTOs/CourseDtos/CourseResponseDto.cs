namespace CourseBooking.Api.DTOs.CourseDtos
{
    public class CourseResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int DurationHours { get; set; }
        public required string Description { get; set; }
        public string InstructorName { get; set; } = null!;
        public float Price { get; set; }

    }
}
