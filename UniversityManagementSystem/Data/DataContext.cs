using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Data
{
    public class DataContext : IdentityDbContext< AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        //define context tables
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            //explicitly configuring relationships
            // sets a composite key for the StudentCourse entity,
            // combining both StudentId and CourseId.
            modelBuilder.Entity<StudentCourse>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            //Each Student can have multiple StudentCourse (to different courses).
            //Each Course can have multiple StudentCourse (from different students).
            modelBuilder.Entity<StudentCourse>()
                .HasOne(e => e.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Instructor>()
                .HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId);

            //Instructor and Course Relationship
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId);

            //seeding entities

            modelBuilder.Entity<Student>().HasData(
            new Student{
                Id = 1,
                FirstName = "Sara",
                LastName = "Ahmad",
                PhoneNumber = "0123456789",
                PaymentStatus = true,
                EnrollementYear = 2020,
                Major = "Biology",
            },
            new Student
            {
                Id = 2,
                FirstName = "Fahad",
                LastName = "Abdullah",
                PhoneNumber = "1876543289",
                PaymentStatus = true,
                EnrollementYear = 2019,
                Major = "Computer Science",
            },
            new Student
            {
                Id = 3,
                FirstName = "Yasser",
                LastName = "Reda",
                PhoneNumber = "1900124989",
                PaymentStatus = true,
                EnrollementYear = 2021,
                Major = "Politics",
            },
            new Student
            {
                Id = 4,
                FirstName = "Leena",
                LastName = "Saad",
                PhoneNumber = "7702127689",
                PaymentStatus = true,
                EnrollementYear = 2021,
                Major = "Politics",
            },
            new Student
            {
                Id = 5,
                FirstName = "Tala",
                LastName = "Abdulmajeed",
                PhoneNumber = "6890876511",
                PaymentStatus = true,
                EnrollementYear = 2023,
                Major = "History",
            },
            new Student
            {
                Id = 6,
                FirstName = "Fatima",
                LastName = "Salman",
                PhoneNumber = "6194321489",
                PaymentStatus = true,
                EnrollementYear = 2023,
                Major = "Nursing",
            },
            new Student
            {
                Id = 7,
                FirstName = "Saoud",
                LastName = "Rakan",
                PhoneNumber = "1248765489",
                PaymentStatus = true,
                EnrollementYear = 2022,
                Major = "Nursing",
            },
            new Student
            {
                Id = 8,
                FirstName = "Faris",
                LastName = "Mohammed",
                PhoneNumber = "8245995101",
                PaymentStatus = true,
                EnrollementYear = 2021,
                Major = "Software Engineering",
            });

            modelBuilder.Entity<Instructor>().HasData(
            new Instructor
            {
                Id=1,
                FirstName = "Adeel",
                LastName = "Abdullah",
                PhoneNumber = "8943216543" ,
                Email = "aabdullah.university@gmail.com"            },
            new Instructor
            {
                Id = 2,
                FirstName = "Sara",
                LastName = "Mohammed",
                PhoneNumber = "0982387612",
                Email = "smohammed.university@gmail.com"

            },
            new Instructor
            {
                Id = 3,
                FirstName = "Rand",
                LastName = "Ibrahim",
                PhoneNumber = "6709234789",
                Email = "ribrahim.university@gmail.com"
            },
            new Instructor
            {
                Id = 4,
                FirstName = "Mourad",
                LastName = "Abed",
                PhoneNumber = "6122935902",
                Email = "mabed.university@gmail.com"

            });

            modelBuilder.Entity<Course>().HasData(
            new Course
            { Id = 1,
              Name = "Statistics", 
              Credit = 2, 
              InstructorId = 1
            },
            new Course
            {
                Id = 2,
                Name = "Web Development",
                Credit = 4,
                InstructorId = 1
            },
            new Course
            {
                Id = 3,
                Name = "Public Adminstration",
                Credit = 3,
                InstructorId = 3
            },
            new Course
            {
                Id = 4,
                Name = "Pharmacology",
                Credit = 3,
                InstructorId = 4
            },
            new Course
            {
                Id = 5,
                Name = "Modern South Asia",
                Credit = 2,
                InstructorId = 2
            },
            new Course
            {
                Id = 6,
                Name = "Molecular Biology",
                Credit = 4,
                InstructorId = 4
            },
            new Course
            {
                Id = 7,
                Name = "Political Theory",
                Credit = 3,
                InstructorId = 3
            },
            new Course
            {
                Id = 8,
                Name = "Computer Architecture",
                Credit = 4,
                InstructorId = 1
            });

            modelBuilder.Entity<StudentCourse>().HasData(
            new StudentCourse
            {
                StudentId = 1,
                CourseId = 4,
                Grade = "A"
            },
            new StudentCourse
            {
                StudentId = 1,
                CourseId = 6,
                Grade = "C"
            },
            new StudentCourse
            {
                StudentId = 2,
                CourseId = 1,
                Grade = "B"
            },
            new StudentCourse
            {
                StudentId = 2,
                CourseId = 2,
                Grade = "C"
            },
            new StudentCourse
            {
                StudentId = 2,
                CourseId = 8,
                Grade = "B"
            },
            new StudentCourse
            {
                StudentId = 3,
                CourseId = 3,
                Grade = "C"
            },
            new StudentCourse
            {
                StudentId = 3,
                CourseId = 7,
                Grade = "A"
            },
            new StudentCourse
            {
                StudentId = 4,
                CourseId = 3,
                Grade = "A"
            },
            new StudentCourse
            {
                StudentId = 4,
                CourseId = 7,
                Grade = "A"
            },
            new StudentCourse
            {
                StudentId = 5,
                CourseId = 5,
                Grade = "B"
            });
        }
    }
}
