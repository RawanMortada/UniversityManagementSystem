using UniversityManagementSystem.Migrations;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Interfaces
{
    public interface IStudentCourseRepository
    {
        IEnumerable<StudentCourse> GetAllEnrollments();
        IEnumerable<StudentCourse> GetAllEnrollmentsForOneStudent(int studentId);
        StudentCourse GetSingleEnrollmentForSingleStudent(int studentId, int courseId);
        bool AddEnrollment(StudentCourse enrollment);
        //used to add or modify grades for a student’s enrollment in a course.
        bool UpdateEnrollment(StudentCourse enrollment);
        bool DeleteEnrollment(int studentId, int courseId);
        //Checks if the student and course exist before enrolling.
        bool StudentExists(int studentId);
        bool CourseExists(int courseId);
        bool Save();
    }
}
