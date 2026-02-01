namespace CourseBooking.Api.DTOs.StudentDtos
{
    public class StudentResponseDto
    {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Phone { get; set; } = null!;
            public int EnrollmentCount { get; set; } // optional
    }
}
