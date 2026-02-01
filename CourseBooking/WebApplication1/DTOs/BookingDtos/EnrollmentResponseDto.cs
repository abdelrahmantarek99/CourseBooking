namespace CourseBooking.Api.DTOs.BookingDtos
{
    public class EnrollmentResponseDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = null!;
        public string CourseTitle { get; set; } = null!;
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    }
}
