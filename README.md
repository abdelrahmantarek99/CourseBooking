CourseBooking

CourseBooking is a simple course management system with backend APIs and a frontend Angular admin panel.
It allows managing students, instructors, courses, and enrollments with full CRUD operations.

The project is split into:

Backend: .NET 9 Web API (CourseBooking.Api)
Frontend: Angular standalone admin UI (course-admin)

Features
Backend

REST APIs for:

Courses
Instructors
Students
Enrollments
Full CRUD operations
Data validation
Proper exception handling with meaningful responses
Entity Framework Core for database management
Supports idempotency checks for enrollment and course creation


Frontend (Angular Admin)

Simple admin panel for managing:
Courses (list, create, delete)
Instructors
Students
Enrollments

Forms with validation

Dropdowns populated from backend (e.g., selecting instructor by name but storing ID)
Dynamic table refresh after CRUD operations

Folder Structure
LuftBorn/
├── CourseBooking/           # Backend (.NET 9 Web API)
│   ├── Controllers/
│   ├── Services/
│   ├── DTOs/
│   ├── Entities/
│   ├── Repositories/
│   └── Program.cs
├── course-admin/            # Angular frontend
│   ├── src/app/
│   │   ├── core/
│   │   │   ├── models/
│   │   │   └── services/
│   │   └── features/
│   │       └── courses/
│   │           ├── course-list/
│   │           └── course-form/
│   └── main.ts
└── .gitignore

Setup & Run
Backend (.NET API)

Open CourseBooking.sln in Visual Studio 2022 (or VS Code with C# extension)
Restore NuGet packages
Set CourseBooking.Api as startup project
Run the project (F5 or dotnet run)

API will be available at:

http://localhost:5000

Frontend (Angular Admin UI)

Navigate to frontend folder:

cd course-admin
Install dependencies:
npm install


Run Angular app:
ng serve


Open browser at:

http://localhost:4200

Example API Endpoints
Resource	Method	Endpoint	Description
Courses	GET	/api/courses	Get all courses
Courses	GET	/api/courses/{id}	Get course by ID
Courses	POST	/api/courses	Create a course
Courses	PUT	/api/courses/{id}	Update a course
Courses	DELETE	/api/courses/{id}	Delete a course
Students	GET/POST/PUT/DELETE	CRUD for students	
Instructors	GET/POST/PUT/DELETE	CRUD for instructors	
Enrollments	GET/POST/DELETE	Manage enrollments (student-course)	
Notes

Make sure backend is running before using the frontend, as Angular fetches data from the API.
Enrollment creation ensures idempotency (same student cannot enroll twice in the same course).
When creating a course, select instructor from dropdown (ID is stored, not the name).
Technologies

Backend: .NET 9, Entity Framework Core, SQL Server

Frontend: Angular 16, TypeScript, RxJS, Standalone Components

If you want, I can also add a short README badge section for GitHub that shows build status, framework version, and license, so it looks professional.
