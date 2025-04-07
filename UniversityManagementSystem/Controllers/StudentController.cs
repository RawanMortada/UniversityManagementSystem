using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.Dto.CourseDto;
using UniversityManagementSystem.Dto.StudentDto;
using UniversityManagementSystem.Interfaces;
using UniversityManagementSystem.Models;
using UniversityManagementSystem.Repository;

namespace UniversityManagementSystem.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetAllStudents()
        {
            var students = _mapper.Map<IEnumerable<GetAllStudentsDto>>(_studentRepository.GetAllStudents());
            return Ok(students);
        }

        [HttpGet("{studentId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
            {
                return NotFound();
            }

            var student = _mapper.Map<GetStudentByIdDto>(_studentRepository.GetStudentById(studentId));
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(201)]//created status for a successful creation
        [ProducesResponseType(400)]//bad request for validation or payment
        [ProducesResponseType(500)]//internal server error
        //the FromBody indicates that the info will be taken from the body
        public IActionResult CreateStudent([FromBody] AddStudentDto studentCreateDto)
        {
            if (studentCreateDto == null)
            {
                return BadRequest("Student data is null");
            }
            if (!studentCreateDto.PaymentStatus)
            {
                return BadRequest("Payment incomplete. Only students who paid will be enrolled.");
            }

            if (ModelState.IsValid)
            {
                var student = _mapper.Map<Student>(studentCreateDto);

                _studentRepository.AddStudent(student);
                
                var studentcourses = new List<StudentCourse>();
 
                _studentRepository.Save();

                return Ok("Student added successfully");
            }
            return BadRequest("Invalid data.");
        }

        [HttpPut("{studentId}")]
        [Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStudent(int studentId, [FromBody] UpdateStudentDto updatedStudentDto)
        {
            if (updatedStudentDto == null)
                return BadRequest(ModelState);

            if (studentId == null)
                return BadRequest(ModelState);

            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();


            var existingStudent = _studentRepository.GetStudentById(studentId);
            if (existingStudent == null)
                return NotFound();

            // Map the changes to the existing course
            _mapper.Map(updatedStudentDto, existingStudent);

            // Update the course in the repository
            if (!_studentRepository.UpdateStudent(existingStudent))
            {
                ModelState.AddModelError("", "An error occurred while updating student information.");
                return StatusCode(500, ModelState);
            }
            return Ok("Student information updated successfully.");
        }

        [HttpDelete("{studentId}")]
        [Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_studentRepository.DeleteStudent(studentId))
            {
                ModelState.AddModelError("", "An error occured while deleting.");
            }

            return Ok("Student removed successfully.");
        }

    }
}