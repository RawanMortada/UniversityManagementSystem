using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Interfaces
{
    public interface IStudentRepository
    {//details are defined in the repo

        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int studentId);
        bool AddStudent(Student student);
        bool UpdateStudent(Student student);
        bool DeleteStudent(int studentId);
        bool StudentExists(int studentId);
        bool Save();

    }
}
