using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.Dto.CourseDto;
using UniversityManagementSystem.Dto.StudentCourseDto;
using UniversityManagementSystem.Interfaces;
using UniversityManagementSystem.Models;
using UniversityManagementSystem.Repository;

namespace UniversityManagementSystem.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : Controller
    {
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly IMapper _mapper;

        public StudentCourseController(IStudentCourseRepository studentCourseRepository, IMapper mapper)
        {
            _studentCourseRepository = studentCourseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentCourse>))]
        public IActionResult GetAllEnrollments()
        {
            var enrollments = _mapper.Map<IEnumerable<GetAllStudentCourseDto>>(_studentCourseRepository.GetAllEnrollments());
            return Ok(enrollments);
        }


        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(StudentCourse))]
        [ProducesResponseType(400)]
        public IActionResult GetAllEnrollmentsForOneStudent(int studentId)
        {
            if (!_studentCourseRepository.StudentExists(studentId))
            {
                return NotFound();
            }

            var enrollment = _mapper.Map<IEnumerable<GetStudentCourseByIdDto>>(_studentCourseRepository.GetAllEnrollmentsForOneStudent(studentId));
            return Ok(enrollment);
        }


        [HttpPost]
        [ProducesResponseType(201)]//created status for a successful creation
        [ProducesResponseType(400)]//bad request for validation or payment
        public IActionResult AddEnrollment([FromBody] AddStudentCourseDto studentCourseDto)
        {
            if (studentCourseDto == null)
                return BadRequest("Enrollment details is null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var enrollment = _mapper.Map<StudentCourse>(studentCourseDto);
            _studentCourseRepository.AddEnrollment(enrollment);
            _studentCourseRepository.Save();

            return Ok("Student enrolled successfully");
        }

        [HttpPut("{studentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEnrollment(int studentId, int courseId, [FromBody] UpdateStudentCourseDto updatedEnrollmentDto)
        {
            if (updatedEnrollmentDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var existingEnrollment = _studentCourseRepository.GetSingleEnrollmentForSingleStudent(studentId, courseId);
            if (existingEnrollment == null)
                return NotFound();

            // Map the changes to the existing course
            _mapper.Map(updatedEnrollmentDto, existingEnrollment);

            // Update the course in the repository
            if (!_studentCourseRepository.UpdateEnrollment(existingEnrollment))
            {
                ModelState.AddModelError("", "An error occurred while updating enrollment information.");
                return StatusCode(500, ModelState);
            }

            return Ok("Enrollment information updated successfully.");
        }

        [HttpDelete("{studentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSingleEnrollment(int studentId, int courseId)
        {
            if (!_studentCourseRepository.StudentExists(studentId))
                return NotFound();
            if (!_studentCourseRepository.CourseExists(courseId))
                return NotFound();

            if (!_studentCourseRepository.DeleteEnrollment(studentId, courseId))
            {
                ModelState.AddModelError("", "An error occured while deleting.");
            }

            return Ok("Enrollment removed successfully.");
        }

    }
}
