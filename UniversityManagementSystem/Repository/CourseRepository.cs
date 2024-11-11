using UniversityManagementSystem.Data;
using UniversityManagementSystem.Interfaces;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }
        public bool AddCourse(Course course)
        {
            if(course == null) return false;

            _context.Add(course);
            return Save();
        }

        public bool CourseExists(int courseId)
        {
            return _context.Courses.Any(c => c.Id == courseId);
        }

        public bool DeleteCourse(int courseId)
        {
            Course course = _context.Courses.Where(c => c.Id == courseId).FirstOrDefault();
            if(course == null) return false;
            _context.Courses.Remove(course);
            return Save();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.OrderBy(c => c.Id);
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Where(c => c.Id == courseId).FirstOrDefault();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateCourse(Course course)
        {
            _context.Update(course);
            return Save();
        }
    }
}
