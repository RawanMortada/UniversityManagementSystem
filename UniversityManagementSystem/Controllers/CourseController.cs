using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.Dto;
using UniversityManagementSystem.Dto.CourseDto;
using UniversityManagementSystem.Interfaces;
using UniversityManagementSystem.Models;
using UniversityManagementSystem.Repository;

namespace UniversityManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        public IActionResult GetAllCourses()
        {
            var courses = _mapper.Map<IEnumerable<GetAllDto>>(_courseRepository.GetAllCourses());
            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(Course))]
        [ProducesResponseType(400)]
        public IActionResult GetCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var course = _mapper.Map<GetByIdDto>(_courseRepository.GetCourseById(courseId));
            return Ok(course);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(201)]//successful creation
        [ProducesResponseType(400)]//bad request
        [ProducesResponseType(500)]//internal server error
        public IActionResult CreateCourse([FromBody] AddCourseDto courseCreateDto)
        {
            if (courseCreateDto == null)
            {
                return BadRequest("Course data is null");
            }

            if (ModelState.IsValid)
            {
                var course = _mapper.Map<Course>(courseCreateDto);

                _courseRepository.AddCourse(course);
                _courseRepository.Save();

                return Ok("Course added successfully");
            }
            return BadRequest("Invalid data.");
        }

        [HttpPut("{courseId}")]
        [Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCourse(int courseId, [FromBody] UpdateCourseDto updatedCourseDto)
        {
            if (updatedCourseDto == null)
                return BadRequest(ModelState);

            if(!_courseRepository.CourseExists(courseId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var existingCourse = _courseRepository.GetCourseById(courseId);
            if (existingCourse == null)
                return NotFound();

            // Map the changes to the existing course
            _mapper.Map(updatedCourseDto, existingCourse);

            // Update the course in the repository
            if (!_courseRepository.UpdateCourse(existingCourse))
            {
                ModelState.AddModelError("", "An error occurred while updating course information.");
                return StatusCode(500, ModelState);
            }
            return Ok("Course information updated successfully.");
        }


        [HttpDelete("{courseId}")]
        [Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_courseRepository.DeleteCourse(courseId))
            {
                ModelState.AddModelError("", "An error occured while deleting.");
            }

            return Ok("Course removed successfully.");
        }
    }


}
