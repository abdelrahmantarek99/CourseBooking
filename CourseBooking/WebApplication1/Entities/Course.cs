namespace CourseBooking.Api.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; } = null!;
        public required int DurationHours { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Enrollment> Enrollments { get; set; } = new();
    }
}
