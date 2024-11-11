using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Dto.CourseDto
{
    public class AddCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }

        public int InstructorId { get; set; }
    }
}
