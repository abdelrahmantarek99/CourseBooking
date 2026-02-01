namespace CourseBooking.Api.DTOs.CourseDtos
{
    public class CourseCreateDto
    {
        public required string Title { get; set; }
        public required int InstructorId { get; set; }
        public required int DurationHours { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }

    }
}
