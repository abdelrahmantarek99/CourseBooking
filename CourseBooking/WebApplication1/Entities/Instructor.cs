namespace CourseBooking.Api.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }

        public List<Course> Courses { get; set; } = new();
    }

}
