using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.Dto.CourseDto;
using UniversityManagementSystem.Dto.InstructorDto;
using UniversityManagementSystem.Interfaces;
using UniversityManagementSystem.Models;
using UniversityManagementSystem.Repository;

namespace UniversityManagementSystem.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IMapper _mapper;

        public InstructorController(IInstructorRepository instructorRepository, IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Instructor>))]
        public IActionResult GetAllInstructors()
        {
            var instructors = _mapper.Map<IEnumerable<GetAllInstructorsDto>>(_instructorRepository.GetAllInstructors());
            return Ok(instructors);
        }

        [HttpGet("{instructorId}")]
        [ProducesResponseType(200, Type = typeof(Instructor))]
        [ProducesResponseType(400)]
        public IActionResult GetInstructor(int instructorId)
        {
            if (!_instructorRepository.InstructorExists(instructorId))
            {
                return NotFound();
            }
            var instructor = _mapper.Map<GetInstructorByIdDto>(_instructorRepository.GetInstructorById(instructorId));

            return Ok(instructor);
        }

        [HttpPost]
        [ProducesResponseType(201)]//created status for a successful creation
        [ProducesResponseType(400)]//bad request for validation 
        [ProducesResponseType(500)]//internal server error
        public IActionResult CreateInstructor([FromBody] AddInstructorDto instructorCreateDto)
        {
            if (instructorCreateDto == null)
            {
                return BadRequest("Instructor data is null");
            }

            if (ModelState.IsValid)
            {
                var instructor = _mapper.Map<Instructor>(instructorCreateDto);

                _instructorRepository.AddInstructor(instructor);
                _instructorRepository.Save();

                return Ok("Instructor added successfully");
            }
            return BadRequest("Invalid data.");
        }

        [HttpPut("{instructorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateInstructor(int instructorId, [FromBody] UpdateInstructorDto updatedInstructorDto)
        {
            if (updatedInstructorDto == null)
                return BadRequest(ModelState);

            if (!_instructorRepository.InstructorExists(instructorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var existingInstructor = _instructorRepository.GetInstructorById(instructorId);
            if (existingInstructor == null)
                return NotFound();

            _mapper.Map(updatedInstructorDto, existingInstructor);

            if (!_instructorRepository.UpdateInstructor(existingInstructor))
            {
                ModelState.AddModelError("", "An error occurred while updating instructor information.");
                return StatusCode(500, ModelState);
            }
            return Ok("Instructor information updated successfully.");
        }

        [HttpDelete("{instructorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteInstructor(int instructorId)
        {
            if (!_instructorRepository.InstructorExists(instructorId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_instructorRepository.DeleteInstructor(instructorId))
            {
                ModelState.AddModelError("", "An error occured while deleting.");
            }

            return Ok("Instructor removed successfully.");
        }
    }
}
