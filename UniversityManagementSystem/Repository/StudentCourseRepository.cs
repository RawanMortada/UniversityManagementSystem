using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Data;
using UniversityManagementSystem.Interfaces;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Repository
{
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private readonly DataContext _context;

        public StudentCourseRepository(DataContext context)
        {
            _context = context;
        }

        public bool AddEnrollment(StudentCourse enrollment)
        {
            if (enrollment == null) return false;
            _context.StudentCourses.Add(enrollment);
            return Save();
        }

        public bool CourseExists(int courseId)
        {
            return _context.Courses.Any(c => c.Id == courseId);
        }

        public bool StudentExists(int studentId)
        {
            return _context.Students.Any(s => s.Id == studentId);
        }

        public bool DeleteEnrollment(int studentId, int courseId)
        {
            var studentEnrollment = _context.StudentCourses.FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);
            if(studentEnrollment == null) return false; 

            _context.StudentCourses.Remove(studentEnrollment);
            return Save();
        }

        public IEnumerable<StudentCourse> GetAllEnrollments()
        {
            return _context.StudentCourses.OrderBy(c =>  c.CourseId);
        }

        public IEnumerable<StudentCourse> GetAllEnrollmentsForOneStudent(int studentId)
        {
            return _context.StudentCourses.Where(s => s.StudentId == studentId);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateEnrollment(StudentCourse enrollment)
        {
            var existingEnrollment = _context.StudentCourses
                .FirstOrDefault(e => e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId);

            if (existingEnrollment == null)
            {
                return false;
            }

            existingEnrollment.Grade = enrollment.Grade;

            _context.Entry(existingEnrollment).State = EntityState.Modified;

            return Save();
        }

        public StudentCourse GetSingleEnrollmentForSingleStudent(int studentId, int courseId)
        {
            return _context.StudentCourses
                .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}
