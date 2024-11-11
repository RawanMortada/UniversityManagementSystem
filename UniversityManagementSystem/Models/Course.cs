using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credit {  get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        //represents the many-to-many relationship where each course can have multiple students enrolled.
        public ICollection<StudentCourse> StudentCourses { get; set; } 
    }
}
