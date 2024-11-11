using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Data;
using UniversityManagementSystem.Interfaces;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Repository
{
    public class StudentRepository :IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }
        public bool AddStudent(Student student)
        {
            if (student == null) return false;

            // Initialize Enrollments as an empty list if it's null
            student.StudentCourses ??= new List<StudentCourse>();

            _context.Students.Add(student);
            return Save();
        }

        public bool DeleteStudent(int studentId)
        {
            Student student = _context.Students.Where(s => s.Id == studentId).FirstOrDefault();
            if (student == null) return false;
            _context.Students.Remove(student);
            return Save();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students.OrderBy(s => s.Id);
        }

        public Student GetStudentById(int studentId)
        {
            return _context.Students.Where(s => s.Id == studentId).FirstOrDefault();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool StudentExists(int studentId)
        {
            return _context.Students.Any(s => s.Id == studentId);
        }

        public bool UpdateStudent(Student student)
        {
            _context.Update(student);
            return Save();
        }
    }
}
