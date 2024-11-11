using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Interfaces
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(int courseId);
        bool AddCourse(Course course);
        bool UpdateCourse(Course course);
        bool DeleteCourse(int courseId);
        bool CourseExists(int courseId);
        bool Save();
    }
}
