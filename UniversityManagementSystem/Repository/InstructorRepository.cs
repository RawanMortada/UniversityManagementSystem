using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Data;
using UniversityManagementSystem.Interfaces;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly DataContext _context;
        public InstructorRepository(DataContext context) 
        {
            _context = context;
        }
        public bool AddInstructor(Instructor instructor)
        {
            if (instructor == null) return false;

            // Initialize Enrollments as an empty list if it's null
            instructor.Courses ??= new List<Course>();

            _context.Instructors.Add(instructor);
            return Save();
        }

        public bool DeleteInstructor(int instructorId)
        {
            Instructor instructor = _context.Instructors.Where(i => i.Id == instructorId).FirstOrDefault();
            if (instructor == null) return false;
            _context.Instructors.Remove(instructor);
            return Save();
        }

        public IEnumerable<Instructor> GetAllInstructors()
        {
            return _context.Instructors.OrderBy(i => i.Id);
        }

        public Instructor GetInstructorById(int instructorId)
        {
            return _context.Instructors.Where(i => i.Id == instructorId).FirstOrDefault();
        }

        public bool InstructorExists(int instructoId)
        {
            return _context.Instructors.Any(i => i.Id == instructoId);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateInstructor(Instructor instructor)
        {
            _context.Update(instructor);
            return Save();
        }
    }
}
