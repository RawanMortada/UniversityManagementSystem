using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Interfaces
{
    public interface IInstructorRepository
    {
        IEnumerable<Instructor> GetAllInstructors();
        Instructor GetInstructorById(int instructorId);
        bool AddInstructor(Instructor instructor);
        bool UpdateInstructor(Instructor instructor);
        bool DeleteInstructor(int instructorId);
        bool InstructorExists(int instructoId);
        bool Save();
    }
}
