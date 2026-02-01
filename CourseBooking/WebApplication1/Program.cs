using Microsoft.EntityFrameworkCore;    /// UseSqlServer
using CourseBooking.Api.Data;            /// ApplicationDbContext
using CourseBooking.Api.Entities;        /// for Course model
using CourseBooking.Api.Repositories.Interfaces;
using CourseBooking.Api.Services.Interfaces;
using CourseBooking.Api.Repositories;
using CourseBooking.Api.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:4200") // Angular dev server
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Register Repositories
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

// Register Services
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // Automatically creates DB and tables

    // Ensure fresh DB for demo
    db.Database.EnsureDeleted();   // deletes old DB if exists
    db.Database.EnsureCreated();   // creates new DB + tables from model

    if ( db.Courses!= null && !db.Courses.Any())
    {
        // Seed instructors
        if (!db.Instructors.Any())
        {
            db.Instructors.AddRange(
                new Instructor { Name = "Ahmed", Email = "ahmed@example.com", Bio = "Angular expert" },
                new Instructor { Name = "Sara", Email = "sara@example.com", Bio = "C# specialist" }
            );
            db.SaveChanges();
        }

        // Seed students
        if (!db.Students.Any())
        {
            db.Students.AddRange(
                new Student { Name = "Ali", Email = "ali@example.com", phone = "01000000001" },
                new Student { Name = "Mona", Email = "mona@example.com", phone = "01000000002" }
            );
            db.SaveChanges();
        }

        // Seed courses
        if (!db.Courses.Any())
        {
            var ahmed = db.Instructors.First(i => i.Name == "Ahmed");
            var sara = db.Instructors.First(i => i.Name == "Sara");

            db.Courses.AddRange(
                new Course
                {
                    Title = "Angular Basics",
                    InstructorId = ahmed.Id,
                    DurationHours = 10,
                    Description = "Explain basics of Angular and TypeScript",
                    Price = 100
                },
                new Course
                {
                    Title = "C# Fundamentals",
                    InstructorId = sara.Id,
                    DurationHours = 12,
                    Description = "Explain problem solving using C#",
                    Price = 120
                }
            );
            db.SaveChanges();
        }

        // Seed enrollments
        if (!db.Enrollments.Any())
        {
            var ali = db.Students.First(s => s.Name == "Ali");
            var mona = db.Students.First(s => s.Name == "Mona");

            var angularCourse = db.Courses.First(c => c.Title == "Angular Basics");
            var csharpCourse = db.Courses.First(c => c.Title == "C# Fundamentals");

            db.Enrollments.AddRange(
                new Enrollment
                {
                    StudentId = ali.Id,
                    CourseId = angularCourse.Id,
                    EnrolledAt = DateTime.UtcNow
                },
                new Enrollment
                {
                    StudentId = mona.Id,
                    CourseId = csharpCourse.Id,
                    EnrolledAt = DateTime.UtcNow
                }
            );
        }
            db.SaveChanges();

    }

}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
